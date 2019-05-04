using Alturos.Yolo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pictalk_social_yolo
{
    public partial class Form1 : Form
    {
        private Font font;

        public Form1()
        {
            InitializeComponent();
            FontFamily fontFamily = new FontFamily("Arial");
             font = new Font(
               fontFamily,
               22,
               FontStyle.Bold,
               
               GraphicsUnit.Pixel);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            openFileDialog1.ShowDialog();
            var res = openFileDialog1.FileName;

            pictureBox1.Image = new Bitmap(res);

 


            var configurationDetector = new ConfigurationDetector();
            var config = configurationDetector.Detect();
           
            using (var yoloWrapper = new YoloWrapper(config))
            {
                var items = yoloWrapper.Detect(res);

                Graphics g = Graphics.FromImage(pictureBox1.Image);
         
 

                foreach (var item in items)
                {
           
                    Pen selPen = new Pen(Color.Blue);
                    //g.DrawRectangle(selPen, item.X/x_ratio, item.Y/y_ratio, item.Width/x_ratio, item.Height/y_ratio);
                    g.DrawRectangle(selPen, item.X, item.Y, item.Width, item.Height);
                    g.DrawString(item.Type, font, new System.Drawing.SolidBrush(System.Drawing.Color.Black), item.X, item.Y);
                   
               
                }
                // pictureBox1.Image = new Bitmap(pictureBox1.Image.Width,pictureBox1.Image.Height, g);
                g.Dispose();
                
                //items[0].Type -> "Person, Car, ..."
                //items[0].Confidence -> 0.0 (low) -> 1.0 (high)
                //items[0].X -> bounding box
                //items[0].Y -> bounding box
                //items[0].Width -> bounding box
                //items[0].Height -> bounding box
            }
        }

        
    }
}
