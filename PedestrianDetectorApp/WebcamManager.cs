using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;

namespace PedestrianDetectorApp
{
 
    class WebcamManager
    {
        private FilterInfoCollection filterInfoCollection;
        private VideoCaptureDevice videoCaptureDevice;
        private AForge.Video.NewFrameEventHandler newFrameEventHandler;

        public WebcamManager(NewFrameEventHandler handler)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            newFrameEventHandler = handler;
        }

        

        public void StartVideo()
        {
            if (filterInfoCollection != null)
            {
                videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[1].MonikerString);
                try
                {
                    if (videoCaptureDevice.VideoCapabilities.Length > 0)
                    {
                        string highestSolution = "0;0";

                        for (int i = 0; i < videoCaptureDevice.VideoCapabilities.Length; i++)
                        {
                            highestSolution = videoCaptureDevice.VideoCapabilities[i].FrameSize.Width.ToString() +';'
                            +i.ToString();
                        }

                        videoCaptureDevice.VideoResolution =
                            videoCaptureDevice.VideoCapabilities[Convert.ToInt32(highestSolution.Split(';')[1])];



                    }

                }
                catch (Exception e)
                {
                    
                }

                videoCaptureDevice.NewFrame += newFrameEventHandler;

                videoCaptureDevice.Start();
            }

        }

        



    }
}
