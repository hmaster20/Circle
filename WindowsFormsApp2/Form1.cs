using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // set up your values
            float radius = 50;
            PointF center = new Point(60, 60);
            float cutOutLen = 20;

            RectangleF circleRect =
                       new RectangleF(center.X - radius, center.Y - radius, radius * 2, radius * 2);

            // the angle
            float alpha = (float)(Math.Asin(1f * (radius - cutOutLen) / radius) / Math.PI * 180);

            var path = new GraphicsPath();
            path.AddArc(circleRect, 180 - alpha, 180 + 2 * alpha);
            path.CloseFigure();


            g.SmoothingMode = SmoothingMode.HighQuality;
            g.FillPath(Brushes.Yellow, path);
            g.DrawPath(Pens.Red, path);

            //e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            //e.Graphics.FillPath(Brushes.Yellow, path);
            //e.Graphics.DrawPath(Pens.Red, path);

            path.Dispose();
        }
    }
}
