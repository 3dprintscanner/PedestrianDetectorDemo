using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.GPU;
using Emgu.CV.Structure;
using Emgu.CV.UI;

namespace PedestrianDetectorApp
{
    public static class PedestrianMain
    {
        public static Image<Bgr, Byte> Find(Image<Bgr, Byte> image, out long processingTime)
        {
            Stopwatch watch;
            Rectangle[] regions;

            if (GpuInvoke.HasCuda)
            {
                using (GpuHOGDescriptor des = new GpuHOGDescriptor())
                {
                    des.SetSVMDetector(GpuHOGDescriptor.GetDefaultPeopleDetector());

                    watch = Stopwatch.StartNew();

                    using (GpuImage<Bgr, Byte> gpuImage = new GpuImage<Bgr, byte>(image))
                    {
                        using (GpuImage<Bgra, Byte> gpuGpraImage = gpuImage.Convert<Bgra, Byte>())
                        {
                            regions = des.DetectMultiScale(gpuGpraImage);
                        }

                    }

                }

            }
            else
            {
                using (HOGDescriptor des = new HOGDescriptor())
                {
                    
                    des.SetSVMDetector(HOGDescriptor.GetDefaultPeopleDetector());
                    watch = Stopwatch.StartNew();
                    regions = des.DetectMultiScale(image);
                }
            }
            watch.Stop();

            processingTime = watch.ElapsedMilliseconds;

            foreach (Rectangle rect in regions)
            {
                image.Draw(rect, new Bgr(Color.Red),1 );
            }

            return image;
        }
       
    }
}
