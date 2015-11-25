using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Image = AForge.Imaging.Image;

namespace PedestrianDetectorApp
{
    public partial class Form1 : Form
    {
        private WebcamManager manager;
        private Bitmap bitmap;
        private long processingTime = 0;
        private Image<Bgr, Byte> processedBitmap;
        
        public Form1()
        {
            InitializeComponent();

            manager = new WebcamManager(NewFrame);
            manager.StartVideo();
            



        }

        private void pictureBoxVideo_HelpRequested(object sender, HelpEventArgs hlpevent)
        {

        }

        public void NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {


            if (pictureBoxVideo.Image != null)
            {
                pictureBoxVideo.Image.Dispose();
            }

            bitmap = (Bitmap) eventArgs.Frame.Clone();
            //processedBitmap = PedestrianMain.Find(new Image<Bgr, byte>((Bitmap)eventArgs.Frame.Clone()), out processingTime);

            //pictureBoxVideo.Image = processedBitmap.ToBitmap();

            pictureBoxVideo.Image = getProcessedBitmap(bitmap);


        }

        private Bitmap getProcessedBitmap(Bitmap bitmap)
        {
            Image<Bgr, Byte> toProcess = new Image<Bgr, Byte>(bitmap);
            return PedestrianMain.Find(toProcess, out processingTime).ToBitmap(640,480);
        }
        
    }
}
