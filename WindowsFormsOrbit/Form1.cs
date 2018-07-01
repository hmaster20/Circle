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

namespace WindowsFormsOrbit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Thread th;
        Graphics g;
        Graphics fG;
        Bitmap btm;

        bool drawing = true;
        private void Form1_Load(object sender, EventArgs e)
        {
            btm = new Bitmap(560, 560);
            g = Graphics.FromImage(btm);
            fG = CreateGraphics();
            th = new Thread(Draw);
            th.IsBackground = true;
            th.Start();
        }

        private void Draw()
        {
            float angle = 0.0f;
            PointF org = new PointF(250, 250);
            float rad = 250;
            Pen pen = new Pen(Brushes.Azure, 3.0f);
            RectangleF area = new RectangleF(30, 30, 500, 500); // Основа для диаметра орбиты
            //RectangleF circle = new RectangleF(0, 0, 50, 50);
            RectangleF circle = new RectangleF(0, 0, 20, 20);   // Основа для диаметра планеты

            PointF loc = PointF.Empty;
            PointF img = new PointF(20, 20);

            fG.Clear(Color.Black);




            while (drawing)
            {
                g.Clear(Color.Black); // регулярная очистка создает эффект движения

                g.DrawEllipse(pen, area);   // Орбита
                loc = CirclePoint(rad, angle, org);

                circle.X = loc.X - (circle.Width / 2) + area.X;
                circle.Y = loc.Y - (circle.Height / 2) + area.Y;

                g.DrawEllipse(new Pen(Brushes.Red, 3.0f), circle);  // Планета
                                                                    //g.DrawEllipse(pen, circle);


                // g.DrawLine(new Pen(Brushes.Blue, 2.0f), new PointF(265,265), loc);
                //g.DrawLine(new Pen(Brushes.Blue, 2.0f), new PointF(265, 265), new PointF(250, 250
                //g.DrawLine(new Pen(Brushes.Blue, 2.0f), new PointF(265, 265), new PointF(circle.X, circle.Y));
                //g.DrawLine(new Pen(Brushes.Blue, 2.0f), new PointF(265, 265),
                //    new PointF(
                //        loc.X - (1 / 2) + area.X,
                //        loc.Y - (1 / 2) + area.Y));

                g.DrawLine(new Pen(Brushes.Blue, 2.0f), calc.Center(area), 
                    new PointF(
                        loc.X - (1 / 2) + area.X,
                        loc.Y - (1 / 2) + area.Y));

                g.DrawLine(new Pen(Brushes.Yellow, 1.0f), new PointF(30, 30), new PointF(500 + 30, 500 + 30));
                g.DrawLine(new Pen(Brushes.Yellow, 1.0f), new PointF(30, 500 + 30), new PointF(500 + 30, 30));

                fG.DrawImage(btm, img);

                if (angle < 360)
                {
                    angle += 0.5f;
                }
                else
                {
                    angle = 0;
                }
            }



        }




        private PointF CirclePoint(float radius, float angleInDegrees, PointF origin)
        {
            float x = (float)(radius * Math.Cos(angleInDegrees * Math.PI / 180f)) + origin.X;
            float y = (float)(radius * Math.Sin(angleInDegrees * Math.PI / 180f)) + origin.Y;
            return new PointF(x, y);
        }
    }

    public static class calc
    {
        public static Point Center(this Rectangle rect)
        {
            return new Point(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
        }

        public static PointF Center(this RectangleF rect)
        {
            return new PointF(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
        }

    }
}
