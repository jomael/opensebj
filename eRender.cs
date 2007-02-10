/* 
 * OpenSebJ
 * Copyright (C) 2007 Sebastian Gray - sebastiangray@gmail.com 
 * Website: http://www.evolvingsoftware.com/opensebj.html
 * 
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 * 
*/


using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

using DirectShowLib;



using System.Drawing;
using System.Drawing.Imaging;

using System.IO;

namespace OpenSebJ
{
    class eRender
    {
        #region Variables
        
        public static bool exit = false;

        public static Microsoft.DirectX.Direct3D.Device device = null;

        public static AdapterInformation adapterInfo = null;


        // Not currently using
        //Microsoft.DirectX.Direct3D.Font font = null;
        //Microsoft.DirectX.Direct3D.Sprite fontSprite = null;

        public static Microsoft.DirectX.Direct3D.Texture background;

        public static Microsoft.DirectX.Direct3D.Texture lastFrame;

        //public static Microsoft.DirectX.Direct3D.Texture secondLastFrame;

        //public static Microsoft.DirectX.Direct3D.Texture[] images = new Texture[10];

        public static System.Windows.Forms.Form clientForm;

        //public static OpenSebJ.frmVideoRender clientForm;

        public static bool currentlyRendering = false;

        public static bool deviceAlreadyCreated = false;

        // The images to be assigned to keys
        public static Microsoft.DirectX.Direct3D.Texture[] images = new Microsoft.DirectX.Direct3D.Texture[257];

        // The video samples to be assigned to keys
        public static bool[] videoPlayed = new bool[256];

        // Int Frame Counter (To know the length for the array to store the individual frames)
        public static int frameCounter = 0;

        // The array to store the converted video
        public static Microsoft.DirectX.Direct3D.Texture[] convertedVideo = new Microsoft.DirectX.Direct3D.Texture[5000];

        // .Net DirectShow ----------------------------------------------------

        private static DirectShowLib.IGraphBuilder graph = null;
        private static DirectShowLib.IBaseFilter filter = null;
        private static DirectShowLib.IMediaControl mediaControl = null;
        //private static DirectShowLib.IMediaPosition seekControl = null;
        private static DirectShowLib.Allocator allocator = null;
        private static IntPtr userId = new IntPtr(unchecked((int)0xACDCACDC));

        // All the direct show libs which need to be repeated
        private static DirectShowLib.IGraphBuilder graph1 = null;
        private static DirectShowLib.IBaseFilter filter1 = null;
        private static DirectShowLib.IMediaControl mediaControl1 = null;
        private static IntPtr userId1 = new IntPtr(unchecked((int)0xACDCACDD));

        // Setting up the array for direct show
        private static DirectShowLib.IGraphBuilder[] dsGraph = new DirectShowLib.IGraphBuilder[256];
        private static DirectShowLib.IBaseFilter[] dsFilter = new DirectShowLib.IBaseFilter[256];
        private static DirectShowLib.IMediaControl[] dsMediaControl = new DirectShowLib.IMediaControl[256];
        private static DirectShowLib.IMediaPosition[] dsSeekControl = new DirectShowLib.IMediaPosition[256];
        private static IntPtr[] dsUserId = new IntPtr[256];

        //private static DirectShowLib.DES.IMediaDet mediaDetail = null;

        //private static Surface bufferSurface;

        // To keep track of the frame currently focused on
        public static int currentFrame = 0;
        
        //---------------------------------------------------------------------

        // For Vertex Buffer
        // public static VertexBuffer buffer = null;

        //public static VertexFormats vertexFormat = VertexFormats.Position | VertexFormats.Texture2 | CustomVertex.TransformedTextured.Format;
        public static VertexFormats vertexFormat =  VertexFormats.Texture2 | CustomVertex.TransformedTextured.Format;


        


        #endregion








        #region Cleaned


        /// <summary>
        /// Load a video to setup DirectShow. This also initally creates an instance of the DirectX3D Device 
        /// </summary>
        public static void StartGraph()
        {
            int hr = 0;

            //CloseGraph();

            string path = @"opensebj.wmv";

            try
            {
                graph = (DirectShowLib.IGraphBuilder)new DirectShowLib.FilterGraph();
                filter = (DirectShowLib.IBaseFilter)new DirectShowLib.VideoMixingRenderer9();

                DirectShowLib.IVMRFilterConfig9 filterConfig = (DirectShowLib.IVMRFilterConfig9)filter;

                hr = filterConfig.SetRenderingMode(VMR9Mode.Renderless);
                DsError.ThrowExceptionForHR(hr);

                hr = filterConfig.SetNumberOfStreams(2);
                DsError.ThrowExceptionForHR(hr);

                SetAllocatorPresenter();

                hr = graph.AddFilter(filter, "Video Mixing Renderer 9");
                DsError.ThrowExceptionForHR(hr);

                hr = graph.RenderFile(path, null);
                DsError.ThrowExceptionForHR(hr);

                mediaControl = (DirectShowLib.IMediaControl)graph;
            }
            catch
            {
            }
        }






        /// <summary>
        ///TODO: Description
        /// </summary>
        public static void SetAllocatorPresenter()
        {
            int hr = 0;

            DirectShowLib.IVMRSurfaceAllocatorNotify9 vmrSurfAllocNotify = (DirectShowLib.IVMRSurfaceAllocatorNotify9)filter;

            try
            {
                allocator = new DirectShowLib.Allocator(clientForm);
                deviceAlreadyCreated = true;

                hr = vmrSurfAllocNotify.AdviseSurfaceAllocator(userId, allocator);
                DsError.ThrowExceptionForHR(hr);

                hr = allocator.AdviseNotify(vmrSurfAllocNotify);
                DsError.ThrowExceptionForHR(hr);
            }
            catch
            {
                allocator = null;
                throw;
            }
        }



        /// <summary>
        /// Load the inital textures
        /// </summary>
        public static void LoadTextures()
        {
            
            // Background texture
            images[256] = Microsoft.DirectX.Direct3D.TextureLoader.FromFile(device, globalSettings.path + "openSebJ.jpg");
            images[0] = Microsoft.DirectX.Direct3D.TextureLoader.FromFile(device, globalSettings.path + "blank.jpg");

            for (int i = 0; i < 255; i++)
            {
                //if (dsInterface.imageLocations[i] != null)
                if (globalSettings.osj.image_Locations[i] != null)
                {
                    try
                    {

                        // This loop extracts the images from the memory stream where they were saved and loads them 1 by 1 through the
                        // DirectX interface, in to memory.
                        if (globalSettings.osj.image_Loaded[i] == true)
                        {
                            // Setup the blank byte array
                            byte[] _bytes;

                            // Use the application data directory defined as the temporary location to write the wave sample to
                            // TODO: Setup the extension to match the image type - however it seems to work even without an extension?
                            string imageName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\image" + i.ToString();

                            // Copy the memory stream to the temporary byte array
                            _bytes = globalSettings.osj.image_MemoryStream[i].ToArray();

                            // Writes out the byte array to the predefined file location
                            System.IO.File.WriteAllBytes(imageName, _bytes);

                            // Load the file using the standard DirectX interface
                            //dsInterface.loadSample(sampleName, i);
                            images[i] = Microsoft.DirectX.Direct3D.TextureLoader.FromFile(device, imageName);

                            // Delete the file saved out
                            System.IO.File.Delete(imageName);

                        }
                    }
                    catch (Exception e)
                    {
                        // If the load fails; then set the file name back to nothing
                        //dsInterface.imageLocations[i] = null;
                        globalSettings.osj.image_Locations[i] = null;
                        globalSettings.osj.image_Loaded[i] = false;
                        System.Windows.Forms.MessageBox.Show(e.ToString());
                    }  
                }
            }

            background = images[256];
        }


        public static void LoadVideos()
        {
            for (int i = 0; i < 255; i++)
            {
                //if (dsInterface.videoLocations[i] != null)
                if (globalSettings.osj.video_Locations[i] != null)
                {
                    try
                    {
                        // TODO: Remove the old load
                        // Do Events call probably required here
                        //dsGraph[i].RenderFile(globalSettings.osj.video_Locations[i], null);
                        //dsMediaControl[i] = (DirectShowLib.IMediaControl)dsGraph[i];


                        // This loop extracts the video from the memory stream where they were saved and loads them 1 by 1 through the
                        // DirectShow interface, in to memory.
                        if (globalSettings.osj.video_Loaded[i] == true)
                        {
                            // Setup the blank byte array
                            byte[] _bytes;

                            // Use the application data directory defined as the temporary location to write the wave sample to
                            // TODO: Setup the extension to match the image type - however it seems to work even without an extension?
                            string videoName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\video" + i.ToString() + ".wmv";

                            // Copy the memory stream to the temporary byte array
                            _bytes = globalSettings.osj.video_MemoryStream[i].ToArray();

                            // Writes out the byte array to the predefined file location
                            System.IO.File.WriteAllBytes(videoName, _bytes);

                            // Load the file using the standard Direct Show interface
                            dsGraph[i].RenderFile(videoName, null);
                            dsMediaControl[i] = (DirectShowLib.IMediaControl)dsGraph[i];

                            // Delete the file saved out
                            System.IO.File.Delete(videoName);
                        }
                    }
                    catch (Exception e)
                    {
                        // If the load fails; then set the file name back to nothing
                        //dsInterface.videoLocations[i] = null;
                        globalSettings.osj.video_Locations[i] = null;
                        System.Windows.Forms.MessageBox.Show(e.ToString());
                    }  
                }
            }
        }


        public static void LoadSingleTexture(int sample)
        {
            int i = sample;

            //if (dsInterface.imageLocations[i] != null)
            if (globalSettings.osj.image_Locations[i] != null)
            {
                try
                {
                    //images[i] = Microsoft.DirectX.Direct3D.TextureLoader.FromFile(device, dsInterface.imageLocations[i]);
                    images[i] = Microsoft.DirectX.Direct3D.TextureLoader.FromFile(device, globalSettings.osj.image_Locations[i]);
                    globalSettings.osj.image_Loaded[i] = true;
                }
                catch (Exception e)
                {
                    // If the load fails; then set the file name back to nothing
                    //dsInterface.imageLocations[i] = null;
                    globalSettings.osj.image_Loaded[i] = false;
                    globalSettings.osj.image_Locations[i] = null;
                    System.Windows.Forms.MessageBox.Show(e.ToString());
                }
            }
        }



        public static void LoadSingleVideo(int sample)
        {
            int i = sample;

            //if (dsInterface.videoLocations[i] != null)
            if (globalSettings.osj.video_Locations[i] != null)
            {
                try
                {
                    //dsGraph[i].RenderFile(dsInterface.videoLocations[i], null);
                    dsGraph[i].RenderFile(globalSettings.osj.video_Locations[i], null);
                    dsMediaControl[i] = (DirectShowLib.IMediaControl)dsGraph[i];
                    globalSettings.osj.video_Loaded[i] = true;
                }
                catch (Exception e)
                {
                    // If the load fails; then set the file name back to nothing
                    //dsInterface.videoLocations[i] = null;
                    globalSettings.osj.video_Locations[i] = null;
                    globalSettings.osj.video_Loaded[i] = false;
                    System.Windows.Forms.MessageBox.Show(e.ToString());
                }
            }
        }


        /// <summary>
        /// Stops and closes any existing DirectShow Graphs
        /// </summary>
        private static void CloseGraph(int sample)
        {
            int i = sample;

            FilterState state;

            if (dsMediaControl[i] != null)
            {
                do
                {
                    dsMediaControl[i].Stop();
                    dsMediaControl[i].GetState(0, out state);
                } while (state != FilterState.Stopped);

                dsMediaControl[i] = null;
            }

            //if (allocator != null)
            //{
            //    allocator.Dispose();
            //    allocator = null;
            //}

            if (dsFilter[i] != null)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(dsFilter[i]);
                dsFilter[i] = null;
            }


            if (dsGraph[i] != null)
            {
                //RemoveAllFilters();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(dsGraph[i]);
                dsGraph[i] = null;
            }
        }



        /// <summary>
        /// Setup the Direct3D Device
        /// </summary>
        /// <param name="thisForm"></param>
        //public static void SetupD3D(OpenSebJ.frmVideoRender thisForm)
        public static void SetupD3D(System.Windows.Forms.Form thisForm)
        {

            // Set the clientForm to the form initating the D3D interface
            clientForm = thisForm;

            // Required so that we can get a pointer to the monitor that the DX graphics are being drawn to.
            adapterInfo = Manager.Adapters.Default;


            // From the Allocator code - not sure of the effect
            // TODO determine if required..
            //Device.IsUsingEventHandlers = false;

            // Setup DirectX device
            PresentParameters presentParams = new PresentParameters();

            // Always use windowed mode for debugging and fullscreen for the presentation.
            presentParams.Windowed = true;

            presentParams.PresentationInterval = PresentInterval.Default;
            presentParams.BackBufferFormat = Format.X8R8G8B8;
            presentParams.BackBufferWidth = clientForm.Width;
            presentParams.BackBufferHeight = clientForm.Height;

            //Needs to specifically be set for full screen
            //presentParams.BackBufferWidth = 800;
            //presentParams.BackBufferHeight = 600;

            // Default to triple buffering for performance gain,
            // if we are low on video memory and use multisampling, 1 is ok too.
            presentParams.BackBufferCount = 2;

            // Discard back buffer when swapping, its faster
            presentParams.SwapEffect = SwapEffect.Discard;

            presentParams.MultiSample = MultiSampleType.None;

            presentParams.MultiSampleQuality = 0;


            // Picked off another example
            presentParams.PresentFlag = PresentFlag.Video;

            // Use a Z-Buffer with 32 bit if possible
            presentParams.EnableAutoDepthStencil = true;
            presentParams.AutoDepthStencilFormat = DepthFormat.D24X8;

            // For windowed, default to PresentInterval.Immediate which will
            // wait not for the vertical retrace period to prevent tearing,
            // but may introduce tearing. For full screen, default to
            // PresentInterval.Default which will wait for the vertical retrace
            // period to prevent tearing.
            presentParams.PresentationInterval = PresentInterval.Immediate;

            // Try setting up the device. There can be a few problems when going with full screen
            // using the event render - needs further investigation
            try
            {
                device = new Device(0, DeviceType.Hardware, clientForm, CreateFlags.HardwareVertexProcessing | CreateFlags.PureDevice, presentParams);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
            }


            //Setup the view matrix to look at the complete sceen
            device.Transform.View = Matrix.LookAtLH(
               new Vector3(0.0f, 0.0f, -4.0f),
               new Vector3(0.0f, 0.0f, 0.0f),
               new Vector3(0.0f, 1.0f, 0.0f));

            float aspectRatio = (float)clientForm.Width / (float)clientForm.Height;
            float fieldOfView = (float)Math.PI / 2.0f;
            float nearPlane = 1.0f;
            float farPlane = 100.0f;


            // Apply the view projection matrix to see everything
            device.Transform.Projection = Matrix.PerspectiveFovLH(fieldOfView, aspectRatio, nearPlane, farPlane);
        }



        /// <summary>
        /// Get the surface from the DX3D Device
        /// </summary>
        /// <returns></returns>
        public static Surface GetSurface()
        {
            return device.GetRenderTarget(0);
        }



        /// <summary>
        /// Get pointer to the monitor
        /// </summary>
        /// <returns></returns>
        public static IntPtr getMonitor()
        {
            return Manager.GetAdapterMonitor(adapterInfo.Adapter);

        }



        #endregion



          



        public static void renderBackground(Texture texture)
        {

            // To store the converted video
            // no longer required - textures now saved out and sent to a memory stream.
            //convertedVideo[frameCounter++] = texture;

            // Prevent anything from being rendered until ready
            if (globalSettings.videoPortal_eRenderReady == false)
            {
                return;
            }


            // Origionaly split out but more stable as part of this method
            if (device == null || device.Disposed)
                return;


            device.BeginScene();            


            // Renders to a flat plane - i.e. the screen
            CustomVertex.TransformedTextured[] screenVert = new CustomVertex.TransformedTextured[4];
            screenVert[0] = new CustomVertex.TransformedTextured(0, 0, 0, 1, 0, 0);
            screenVert[1] = new CustomVertex.TransformedTextured(clientForm.Width, 0, 0, 1, 1, 0);
            screenVert[2] = new CustomVertex.TransformedTextured(clientForm.Width, clientForm.Height, 0, 1, 1, 1);
            screenVert[3] = new CustomVertex.TransformedTextured(0, clientForm.Height, 0, 1, 0, 1);
            // To tile textures, use the wrap texture type - It's used by default...
            // device.SamplerState[1].AddressU = TextureAddress.Wrap; device.SamplerState[1].AddressV = TextureAddress.Wrap;
            // Then set the U&V co-ordinates to the number of times you want the texture replicated
            // i.e. if you change both the U&V co-ords to 2 then you will see the texture tiled 4 times, 2 across and 2 down



            // Turn off while drawing the background
            //device.RenderState.ZBufferEnable = false;

            // Test with vertex buffer
            // buffer.SetData(screenVert, 0, LockFlags.None);


            // -----------Stuffn with --------------------------------
            //SamplerStateManager sam = new SamplerStateManager();
            //sam = TextureAddress.Border;
            //device.SetSamplerState(0,SamplerStageStates.SrgbTexture, true);

            //TextureAddress tex = new TextureAddress();
            //tex = TextureAddress.Border;

            // -----------Stuffn with --------------------------------


            //// These would probably work if the textures were first copied to a newly created texture in this
            //// transactions scope
            //fUtil.addFrame(device.GetTexture(0));
            //fUtil.addFrame(texture);

            
            //// This works to save the rendered frame to a BMP
            //Bitmap bmp = SaveToBitmap(device);
            //fUtil.addBmpFrame(bmp);


            // Attemptng to save the frame as a memory stream in the DDS format
            //SaveToDDSFile(device);
            //fUtil.addDDSFrame("TempDDSConv.dds");

            //fUtil.addMSFrame(aMS);



            // Set the texture to render to the background
            device.SetTexture(0, texture);
            
            
            if (lastFrame != null){

                // For Vertex Buffer
                //device.SetStreamSource(0, buffer, 0,0); 

                

                device.SetTexture(1, lastFrame);

                // Sets the U&V co-ordinates for the '1' (i.e. second) texture stage to the same as the first!
                // I wish ths was better fucking documented, seriosuly
                device.TextureState[1].TextureCoordinateIndex = 0;

                // For Vertex Buffer
                //device.SetStreamSource(1, buffer, 0,0); 

               
                //-------------- more stuffn
                //device.SamplerState[0].AddressU = TextureAddress.Border;
                //device.SamplerState[0].AddressV = TextureAddress.Border;
                
                // wrap is the default mode
                device.SamplerState[1].AddressU = TextureAddress.Wrap;
                device.SamplerState[1].AddressV = TextureAddress.Wrap;
                //-------------



                //// Blend and mix - around 50% transperancy
                device.SetTextureStageState(0, TextureStageStates.ColorArgument1, (int)TextureArgument.TextureColor);
                device.SetTextureStageState(0, TextureStageStates.ColorOperation, (int)TextureOperation.SelectArg1);
                device.SetTextureStageState(1, TextureStageStates.ColorArgument1, (int)TextureArgument.Current);
                device.SetTextureStageState(1, TextureStageStates.ColorArgument2, (int)TextureArgument.TextureColor);
                device.SetTextureStageState(1, TextureStageStates.ColorOperation, (int)TextureOperation.Add);


                // Lighter blend
                //device.SetTextureStageState(0, TextureStageStates.ColorArgument1, (int)TextureArgument.TextureColor);
                //device.SetTextureStageState(0, TextureStageStates.ColorOperation, (int)TextureOperation.SelectArg1);
                //device.SetTextureStageState(1, TextureStageStates.ColorArgument1, (int)TextureArgument.Current);
                //device.SetTextureStageState(1, TextureStageStates.ColorArgument2, (int)TextureArgument.TextureColor);
                //device.SetTextureStageState(1, TextureStageStates.ColorOperation, (int)TextureOperation.MultiplyAdd);

                // Subtract the darker colour from the lighter colour
                //device.SetTextureStageState(0, TextureStageStates.ColorArgument1, (int)TextureArgument.TextureColor);
                //device.SetTextureStageState(0, TextureStageStates.ColorOperation, (int)TextureOperation.SelectArg1);
                //device.SetTextureStageState(1, TextureStageStates.ColorArgument1, (int)TextureArgument.Current);
                //device.SetTextureStageState(1, TextureStageStates.ColorArgument2, (int)TextureArgument.TextureColor);
                //device.SetTextureStageState(1, TextureStageStates.ColorOperation, (int)TextureOperation.Subtract);

                // Add with a darker tinge
                //device.SetTextureStageState(0, TextureStageStates.ColorArgument1, (int)TextureArgument.TextureColor);
                //device.SetTextureStageState(0, TextureStageStates.ColorOperation, (int)TextureOperation.SelectArg1);
                //device.SetTextureStageState(1, TextureStageStates.ColorArgument1, (int)TextureArgument.Current);
                //device.SetTextureStageState(1, TextureStageStates.ColorArgument2, (int)TextureArgument.TextureColor);
                //device.SetTextureStageState(1, TextureStageStates.ColorOperation, (int)TextureOperation.AddSigned);

                // Add with highted highlights and darkened shadows
                //device.SetTextureStageState(0, TextureStageStates.ColorArgument1, (int)TextureArgument.TextureColor);
                //device.SetTextureStageState(0, TextureStageStates.ColorOperation, (int)TextureOperation.SelectArg1);
                //device.SetTextureStageState(1, TextureStageStates.ColorArgument1, (int)TextureArgument.Current);
                //device.SetTextureStageState(1, TextureStageStates.ColorArgument2, (int)TextureArgument.TextureColor);
                //device.SetTextureStageState(1, TextureStageStates.ColorOperation, (int)TextureOperation.AddSigned2X);

                // Add with a lighter mix
                //device.SetTextureStageState(0, TextureStageStates.ColorArgument1, (int)TextureArgument.TextureColor);
                //device.SetTextureStageState(0, TextureStageStates.ColorOperation, (int)TextureOperation.SelectArg1);
                //device.SetTextureStageState(1, TextureStageStates.ColorArgument1, (int)TextureArgument.Current);
                //device.SetTextureStageState(1, TextureStageStates.ColorArgument2, (int)TextureArgument.TextureColor);
                //device.SetTextureStageState(1, TextureStageStates.ColorOperation, (int)TextureOperation.AddSmooth);

                // Use colour to tint image mix
                //device.SetTextureStageState(0, TextureStageStates.ColorArgument1, (int)TextureArgument.TextureColor);
                //device.SetTextureStageState(0, TextureStageStates.ColorOperation, (int)TextureOperation.SelectArg1);
                //device.SetTextureStageState(1, TextureStageStates.ColorArgument1, (int)TextureArgument.Current);
                //device.SetTextureStageState(1, TextureStageStates.ColorArgument2, (int)TextureArgument.TextureColor);
                //device.SetTextureStageState(1, TextureStageStates.ColorOperation, (int)TextureOperation.Modulate);

                // Add image and inverse white parts of the image being added
                //device.SetTextureStageState(0, TextureStageStates.ColorArgument1, (int)TextureArgument.TextureColor);
                //device.SetTextureStageState(0, TextureStageStates.ColorOperation, (int)TextureOperation.SelectArg1);
                //device.SetTextureStageState(1, TextureStageStates.ColorArgument1, (int)TextureArgument.Current);
                //device.SetTextureStageState(1, TextureStageStates.ColorArgument2, (int)TextureArgument.TextureColor);
                //device.SetTextureStageState(1, TextureStageStates.ColorOperation, (int)TextureOperation.Lerp);


                // Black and white image add.
                //device.SetTextureStageState(0, TextureStageStates.ColorArgument1, (int)TextureArgument.TextureColor);
                //device.SetTextureStageState(0, TextureStageStates.ColorOperation, (int)TextureOperation.SelectArg1);
                //device.SetTextureStageState(1, TextureStageStates.ColorArgument1, (int)TextureArgument.Current);
                //device.SetTextureStageState(1, TextureStageStates.ColorArgument2, (int)TextureArgument.TextureColor);
                //device.SetTextureStageState(1, TextureStageStates.ColorOperation, (int)TextureOperation.DotProduct3);





                //if (secondLastFrame != null)
                //{

                //    device.TextureState[2].TextureCoordinateIndex = 0;
                //    device.SetTextureStageState(1, TextureStageStates.ColorOperation, (int)TextureOperation.SelectArg1);

                //    device.SetTexture(2, secondLastFrame);

                //    device.SetTextureStageState(2, TextureStageStates.ColorArgument1, (int)TextureArgument.Current);
                //    device.SetTextureStageState(2, TextureStageStates.ColorArgument2, (int)TextureArgument.TextureColor);
                //    device.SetTextureStageState(2, TextureStageStates.ColorOperation, (int)TextureOperation.Add);
                //}


            }
            //secondLastFrame = lastFrame;
            lastFrame = texture;


            //device.VertexFormat = CustomVertex.TransformedTextured.Format;
            device.VertexFormat = vertexFormat;
            
            device.DrawUserPrimitives(PrimitiveType.TriangleFan, 2, screenVert);
            
            // Turn it back on
           // device.RenderState.ZBufferEnable = true;



            // Origionaly split out but more stable in the same method
            device.EndScene();
            device.Present();



        }



        


        #region DirectShow.Net

        









        public static void startSecond()
        {
            initDSArrays();



            filter1 = (DirectShowLib.IBaseFilter)new DirectShowLib.VideoMixingRenderer9();
            DirectShowLib.IVMRFilterConfig9 filterConfig = (DirectShowLib.IVMRFilterConfig9)filter1;
            filterConfig.SetRenderingMode(VMR9Mode.Renderless);
            filterConfig.SetNumberOfStreams(2);

            
            DirectShowLib.IVMRSurfaceAllocatorNotify9 vmrSurfAllocNotify = (DirectShowLib.IVMRSurfaceAllocatorNotify9)filter1;

            try
            {


                int hr = vmrSurfAllocNotify.AdviseSurfaceAllocator(userId1, allocator);
                DsError.ThrowExceptionForHR(hr);

                 hr = allocator.AdviseNotify(vmrSurfAllocNotify);
                DsError.ThrowExceptionForHR(hr);
            }
            catch { }




            graph1 = (DirectShowLib.IGraphBuilder)new DirectShowLib.FilterGraph();
            graph1.AddFilter(filter1, "Video Mixing Renderer 9");

            graph1.RenderFile(@"C:\Download\workmates.wmv", null);
            mediaControl1 = (DirectShowLib.IMediaControl)graph1;
            mediaControl1.Run();
        }

        


        


        public static void RemoveAllFilters()
        {
            int hr = 0;
            DirectShowLib.IEnumFilters enumFilters;
            System.Collections.ArrayList filtersArray = new System.Collections.ArrayList();

            hr = graph.EnumFilters(out enumFilters);
            DsError.ThrowExceptionForHR(hr);

            DirectShowLib.IBaseFilter[] filters = new DirectShowLib.IBaseFilter[1];
            int fetched;

            while (enumFilters.Next(filters.Length, filters, out fetched) == 0)
            {
                filtersArray.Add(filters[0]);
            }

            foreach (DirectShowLib.IBaseFilter filter in filtersArray)
            {
                hr = graph.RemoveFilter(filter);
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(filter) > 0) ;
            }
        }

        




        //public static void reStartVideo(int videoFile)
        //{
        //    // This is a bug - the second time it is executed a new unrequested window pops up.
        //    //mediaControl.Stop();
        //    //graph.RenderFile(@"C:\temp\test1.avi", null);
        //    //mediaControl = (DirectShowLib.IMediaControl)graph;
        //    //mediaControl.Run();


            
        //    // Need to check that the video file has been played, otherwise seting the position
        //    // back to 0 does nothing
        //    if (videoPlayed[videoFile] == false)
        //    {
        //        videoPlayed[videoFile] = true;

        //        int hr = mediaControl.Run();
        //        DsError.ThrowExceptionForHR(hr);
        //    }

        //    // If not disposed we can just reset the position, the clip never stops playing by the
        //    // looks of this - which is interesting in itself ;-)
        //    seekControl = (DirectShowLib.IMediaPosition)graph;
        //    seekControl.put_CurrentPosition(0);

        //    double duration = 0;
        //    seekControl.get_Duration(out duration);
        //    //fUtil.setDuration((int)duration);

        //    //Finding the framerate
        //    double frameRate = 0;

        //    // Find out the details of the media file
        //    mediaDetail = (DirectShowLib.DES.IMediaDet)new DirectShowLib.DES.MediaDet();
        //    // Set the name
        //    mediaDetail.put_Filename(@"C:\temp\test1.avi");

        //    // Read from stream zero
        //    mediaDetail.put_CurrentStream(0);

        //    mediaDetail.get_FrameRate(out frameRate);
        //    //fUtil.setFrameRate((int)frameRate);

        //    //System.Windows.Forms.MessageBox.Show(frameRate.ToString() + " frame rate");

            
        //    //seekControl.get_Rate(out playBackRate);

        //    //System.Windows.Forms.MessageBox.Show(duration.ToString() + " in seconds duration");

        //    //System.Windows.Forms.MessageBox.Show(playBackRate.ToString() + " playback rate");


        //}

        #endregion


        public static void initDSArrays()
        {
            for (int i = 0; i < 255; i++)
            {
                dsUserId[i] = new IntPtr(i);
            }


            // Setup the array (full 255 positions) to be able to store the videos
            for (int i = 0; i < 255; i++)
            {
                dsFilter[i] = (DirectShowLib.IBaseFilter)new DirectShowLib.VideoMixingRenderer9();
                DirectShowLib.IVMRFilterConfig9 filterConfig = (DirectShowLib.IVMRFilterConfig9)dsFilter[i];
                filterConfig.SetRenderingMode(VMR9Mode.Renderless);
                filterConfig.SetNumberOfStreams(2);

                DirectShowLib.IVMRSurfaceAllocatorNotify9 vmrSurfAllocNotify = (DirectShowLib.IVMRSurfaceAllocatorNotify9)dsFilter[i];

                try
                {
                    int hr = vmrSurfAllocNotify.AdviseSurfaceAllocator(dsUserId[i], allocator);
                    DsError.ThrowExceptionForHR(hr);

                    hr = allocator.AdviseNotify(vmrSurfAllocNotify);
                    DsError.ThrowExceptionForHR(hr);
                }
                catch { }

                dsGraph[i] = (DirectShowLib.IGraphBuilder)new DirectShowLib.FilterGraph();
                dsGraph[i].AddFilter(dsFilter[i], "Video Mixing Renderer 9");

                //Throw in a call for program updates to be handled
                //eventRender.Program.update();


                // The movie can then be loaded at a latter time. If a movie is going to be 
                // loaded and played in an array like this the DoEvents call does need to be made.
                //
                //dsGraph[i].RenderFile(@"C:\Download\workmates.wmv", null);
                //dsMediaControl[i] = (DirectShowLib.IMediaControl)dsGraph[i];
                //dsMediaControl[i].Run();
                //
                //eventRender.Program.update();

            }
        }


        /// <summary>
        /// Called every time a sample is played - videos and images are checked to see if they have been loaded.
        /// If they have been loaded for the sample in question then thay are displayed in the view window.
        /// </summary>
        /// <param name="sample"></param>
        internal static void show(int sample)
        {
            // Check that the video window has been opened. Otherwise exceptions can be thrown as their is nothing
            // to render to. Surprise ;-)
            if (globalSettings.videoPortal == true)
            {
                // Check for a loaded image - if one exists display it
                //if (dsInterface.imageLocations[sample] != null)
                if (globalSettings.osj.image_Locations[sample] != null)
                {
                    eRender.background = eRender.images[sample];
                    eRender.renderBackground(eRender.background);
                }

                // Check for a loaded video - if one exists - display it
                if (globalSettings.osj.video_Locations[sample] != null)
                {


                    // Need to check that the video file has been played, otherwise seting the position
                    // back to 0 does nothing
                    if (videoPlayed[sample] == false)
                    {
                        videoPlayed[sample] = true;

                        int hr = dsMediaControl[sample].Run();
                        DsError.ThrowExceptionForHR(hr);
                    }
                    else
                    {
                        // Incase we have paused the video - this used to cause problems previously by opening
                        // up additional windows but doesn't seem to be a problem now..?
                        dsMediaControl[sample].Run();
                    }

                    // If not disposed we can just reset the position, the clip never stops playing by the
                    // looks of this - which is interesting in itself ;-)
                    dsSeekControl[sample] = (DirectShowLib.IMediaPosition)dsGraph[sample];
                    dsSeekControl[sample].put_CurrentPosition(0);
                }
            }
        }


        /// <summary>
        /// Pauses the DirectShow video being played - only thing is that the Run method then needs to be used to start playing again.
        /// This has previously causes issues.
        /// </summary>
        /// <param name="sample"></param>
        public static void pauseVideo(int sample)
        {
            //if (dsInterface.videoLocations[sample] != null)
            if (globalSettings.osj.video_Locations[sample] != null)
            {
                dsMediaControl[sample].Pause();
                dsSeekControl[sample] = (DirectShowLib.IMediaPosition)dsGraph[sample];
                dsSeekControl[sample].put_CurrentPosition(0);
            }
        }



    }
}