using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int global_X = 15;
            int global_Y = 20;

            Graphics g = pictureBox1.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawLine(new Pen(Brushes.Red, 4), new Point(10, 10), new Point(100, 100));


            int E1_Width = 200;
            int E1_Height = 200;
            g.DrawEllipse(Pens.Black, global_X, global_Y, E1_Width, E1_Height);

            int diameter = 100;
            //                     (200+15)  - 100 / 2 + 15              = 65
            int Ellipse_2_x = ((global_X + E1_Width) - diameter) / 2 + global_X;
            //                     (200+20)  - 100 / 2 + 20              = 70
            int Ellipse_2_y = ((global_Y + E1_Height) - diameter) / 2 + global_Y;

            //                     200  - 100 / 2 + 15
            int EllipseWidth2 = Ellipse_2_x + diameter;
            int EllipseHeight2 = Ellipse_2_y + diameter;

            //g.DrawEllipse(Pens.Black, Ellipse_2_x, Ellipse_2_y, EllipseWidth2, EllipseHeight2);
            //g.DrawEllipse(Pens.Red, Ellipse_2_x, Ellipse_2_y, 215, 220);
            //g.DrawEllipse(Pens.Blue, Ellipse_2_x, Ellipse_2_y, 100, 100);

            g.DrawEllipse(Pens.Blue, 65, 70, 135, 150);
            g.DrawLine(new Pen(Brushes.Blue, 2), new Point(10, 220), new Point(500, 220));
            //var grfx = e.Graphics;
            //grfx.SmoothingMode = SmoothingMode.AntiAlias;


            g.Clear(Control.DefaultBackColor);

            System.Drawing.Drawing2D.LinearGradientBrush linGrBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                new Point(0, 10),
                new Point(200, 10),
                Color.FromArgb(255, 255, 0, 0),   // Opaque red
                Color.FromArgb(255, 0, 0, 255));  // Opaque blue
            Pen pen = new Pen(linGrBrush, 4);

            System.Drawing.Drawing2D.LinearGradientBrush linGrBrush22 = new System.Drawing.Drawing2D.LinearGradientBrush(
                new Point(0, 10), new Point(200, 10), Color.Red, Color.Green);
            Pen pen2 = new Pen(linGrBrush22, 4);


            Rectangle rectA = new Rectangle(new Point(global_X, global_Y), new Size(200 + global_X, 200 + global_Y));
            //g.DrawRectangle(new Pen(Brushes.CadetBlue, 2), rectA);
            g.DrawEllipse(Pens.Green, rectA);

            rectA.Inflate(-25, -25);
            //g.DrawRectangle(new Pen(Brushes.BlueViolet, 2), rectA);
            g.DrawRectangle(pen2, rectA);
            g.DrawEllipse(Pens.Pink, rectA);

            int count = 360 / 30;
            for (int i = 0; i < count; i++)
            {
                int angle = i * count;
                //int angle = (i > 0) ? i * count : count;
                int Radius = 120;
                int X1 = 150;
                int Y1 = 150;
                int x = (int)(X1 + Radius * Math.Cos(angle));
                int y = (int)(Y1 + Radius * Math.Sin(angle));

                g.DrawEllipse(Pens.Red, x, y, 20, 20);
            }

            int R = 20;
            double xxx = Math.PI * (R * R);
            double yyy = Math.PI * 2 * R;


            // x = r * cos(fi)
            //y = r * sin(fi)
            //нужен радиус и угол

            //Координаты любой точки на окружности радиуса R с центром в начале координат подчиняются данному равенству:
            //x ^ 2 + y ^ 2 = R ^ 2


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Control.DefaultBackColor);
            // Круг
            int Diameter = 200;
            g.DrawEllipse(Pens.Green, 10, 10, Diameter, Diameter);

            // Угол поворота
            int angle = 360 / 12;

            //int R = 200;
            //int xxx = (int) Math.PI * (R * R);
            //int yyy = (int) Math.PI * 2 * R;

            // int x = (int)(210/2 + 100 * Math.Cos(angle));
            // int y = (int)(210 / 2 + 100 * Math.Sin(angle));

            int Radius = Diameter / 2;
            int y = (int)(Radius * Math.Sin(angle));
            int x = (int)(Radius * Math.Cos(angle));
            y = Math.Abs(y);
            g.DrawEllipse(Pens.Red, x, y, 5, 5);
            g.DrawEllipse(Pens.Black, 210 / 2, 210 / 2, 5, 5);


            for (int i = 0; i < 12; i++)
            {

                int xI = (int)(Math.Cos(2 * Math.PI * i + 1 / 12) * Radius + 0.5) + 210 / 2;
                int yI = (int)(Math.Sin(2 * Math.PI * i + 1 / 12) * Radius + 0.5) + 210 / 2;
                g.DrawEllipse(Pens.BlueViolet, xI, yI, 5, 5);
            }


            //int r = Radius;
            //for (int i = 0; i < 360; i++)
            //{
            //    double rad = (double)i / 180 * 3.14;
            //    //double xG = r * Math.Cos(rad);
            //    //double yG = r * Math.Sin(rad);

            //    double xG = r * Math.Cos(rad) + 210 / 2;
            //    double yG = r * Math.Sin(rad) + 210 / 2;

            //    g.DrawEllipse(Pens.Blue, Convert.ToInt32(xG), Convert.ToInt32(yG), 2,2);
            //    //() << "X:" << x << " Y:" << y << " Rad:" << rad;
            //}


            int r = Radius;
            for (int i = 0; i < 360; i++)
            {

                switch (i)
                {
                    case 0:
                    case 30:
                    case 60:
                    case 90:
                    case 120:
                    case 150:
                    case 180:
                    case 210:
                    case 240:
                    case 270:
                    case 300:
                    case 330:
                        {
                            int center = 210 / 2;
                            double rad = (double)i / 180 * 3.14;
                            double xG = r * Math.Cos(rad) + center;
                            double yG = r * Math.Sin(rad) + center;
                            g.DrawEllipse(Pens.Blue, Convert.ToInt32(xG), Convert.ToInt32(yG), 4, 4);
                            g.DrawLine(Pens.Blue, center, center, Convert.ToInt32(xG), Convert.ToInt32(yG));
                        }; break;
                }
            }

            int r2 = Radius + 10;
            for (int i = 0; i < 360; i++)
            {

                switch (i)
                {
                    case 0:
                    case 30:
                    case 60:
                    case 90:
                    case 120:
                    case 150:
                    case 180:
                    case 210:
                    case 240:
                    case 270:
                    case 300:
                    case 330:
                        {
                            int center = 210 / 2;
                            double rad = (double)i / 180 * 3.14;
                            double xG = r2 * Math.Cos(rad) + center;
                            double yG = r2 * Math.Sin(rad) + center;
                            g.DrawEllipse(Pens.Red, Convert.ToInt32(xG), Convert.ToInt32(yG), 4, 4);
                            g.DrawLine(Pens.Red, center, center, Convert.ToInt32(xG), Convert.ToInt32(yG));
                        }; break;
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            g.Clear(Control.DefaultBackColor);
            // Круг
            int Diameter = 200;

            int DiameterOrange = Diameter - 40;
            int DiameterRed = Diameter + 10;

            g.DrawEllipse(Pens.Orange, 30, 30, DiameterOrange, DiameterOrange);
            g.DrawEllipse(Pens.Red, 10 - 5, 10 - 5, DiameterRed, DiameterRed);


            int Radius = Diameter / 2;

            //int r = DiameterOrange / 2;
            //for (int i = 0; i < 360; i++)
            //{

            //    switch (i)
            //    {
            //        case 0:
            //        case 30:
            //        case 60:
            //        case 90:
            //        case 120:
            //        case 150:
            //        case 180:
            //        case 210:
            //        case 240:
            //        case 270:
            //        case 300:
            //        case 330:
            //            {
            //                int center = 210 / 2;
            //                double rad = (double)i / 180 * 3.14;
            //                double xG = r * Math.Cos(rad) + center;
            //                double yG = r * Math.Sin(rad) + center;
            //                g.DrawEllipse(Pens.Blue, Convert.ToInt32(xG), Convert.ToInt32(yG), 4, 4);
            //                g.DrawLine(Pens.Blue, center, center, Convert.ToInt32(xG), Convert.ToInt32(yG));
            //            }; break;
            //    }
            //}

            //int r2 = Radius + 10;
            //for (int i = 0; i < 360; i++)
            //{

            //    switch (i)
            //    {
            //        case 0:
            //        case 30:
            //        case 60:
            //        case 90:
            //        case 120:
            //        case 150:
            //        case 180:
            //        case 210:
            //        case 240:
            //        case 270:
            //        case 300:
            //        case 330:
            //            {
            //                int center = 210 / 2;
            //                double rad = (double)i / 180 * 3.14;
            //                double xG = r2 * Math.Cos(rad) + center;
            //                double yG = r2 * Math.Sin(rad) + center;
            //                g.DrawEllipse(Pens.Red, Convert.ToInt32(xG), Convert.ToInt32(yG), 4, 4);
            //                g.DrawLine(Pens.Red, center, center, Convert.ToInt32(xG), Convert.ToInt32(yG));
            //            }; break;
            //    }
            //}

            g.DrawEllipse(Pens.Fuchsia, 210 / 2, 210 / 2, 2, 2);
            g.DrawEllipse(Pens.Fuchsia, 210 / 2, 210 / 2, 50, 50);

            for (int i = 0; i < 360; i++)
            {

                switch (i)
                {
                    case 0:
                    case 30:
                    case 60:
                    case 90:
                    case 120:
                    case 150:
                    case 180:
                    case 210:
                    case 240:
                    case 270:
                    case 300:
                    case 330:
                        {
                            int center = 210 / 2;
                            double rad = (double)i / 180 * 3.14;

                            int rOrange = DiameterOrange / 2;
                            int xOrange = (int)(rOrange * Math.Cos(rad) + center);
                            int yOrange = (int)(rOrange * Math.Sin(rad) + center);

                            //int xI = (int)(Math.Cos(2 * Math.PI * i) * rOrange + 0.5) + center;
                            //int yI = (int)(Math.Sin(2 * Math.PI * i) * rOrange + 0.5) + center;

                            //This has nothing to do with C#. There is just some elementary mathematics involved.
                            //x = x0 + r * cos(theta)
                            //y = y0 + r * sin(theta)
                            //theta is in radians, x0 and y0 are the coordinates of the centre, r is the radius, and the angle is measured anticlockwise from the x-axis.But if you want it in C#, and your angle is in degrees:
                            //double xx = center + rOrange * Math.Cos(theta * Math.PI / 180);
                            //double yy = center + rOrange * Math.Sin(theta * Math.PI / 180)

                            //in C#, this would look like:
                            //x1 = x + radius * Math.Cos(angle * (Math.PI / 180));
                            //y1 = y + radius * Math.Sin(angle * (Math.PI / 180));

                            int rRed = Radius + 10;
                            //int xRed = (int)(rRed * Math.Cos(rad) + center);
                            //int yRed = (int)(rRed * Math.Sin(rad) + center);
                            //int xRed = (int)(center + rRed * Math.Cos(i * (Math.PI / 180)));
                            //int yRed = (int)(center + rRed * Math.Sin(i * (Math.PI / 180)));
                            float xRed = (float)(rRed * Math.Cos(rad) + center);
                            float yRed = (float)(rRed * Math.Sin(rad) + center);

                            g.DrawLine(Pens.Red, xOrange, yOrange, xRed, yRed);
                            //g.DrawLine(Pens.Red, xI, yI, xRed, yRed);

                            g.DrawLine(Pens.Blue, center, center, xOrange, yOrange);

                            // верхняя точка оранжа на 12 часов
                            int UpX = 30 + rOrange;
                            int UpY = 30;

                            g.DrawEllipse(Pens.Black, UpX, UpY, 4, 4);
                        };
                        break;
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int Diameter = 300;

            Graphics g = pictureBox1.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Control.DefaultBackColor);

            Pen pen = new Pen(Brushes.Aquamarine, 3.0f);
            //RectangleF area = new RectangleF(30, 30, 500, 500);
            RectangleF area = new RectangleF(30, 30, Diameter, Diameter);
            g.DrawEllipse(pen, area);   // Главный круг


            RectangleF circle = new RectangleF(0, 0, 5, 5);   // размеры круга на орбите
            PointF loc = PointF.Empty;
            //float radius = 250;
            float radius = (Diameter / 2);
            float angle = 0.0f;
            //PointF org = new PointF(250, 250);
            PointF org = new PointF(radius, radius);

            // Внутренний круг
            int PsevdoCircleDiameter = Diameter - 50;
            RectangleF PsevdoCircle = new RectangleF(0, 0, PsevdoCircleDiameter, PsevdoCircleDiameter);
            PsevdoCircle.X = calc.Center(area).X - (PsevdoCircle.Width / 2);
            PsevdoCircle.Y = calc.Center(area).Y - (PsevdoCircle.Height / 2);
            g.DrawEllipse(new Pen(Brushes.Brown, 2.0f), PsevdoCircle);


            int ang = 360 / 2;

            for (int i = 0; i < 360;)
            {
                angle = i;
                loc = calc.CirclePoint(radius, angle, org);

                circle.X = loc.X - (circle.Width / 2) + area.X;
                circle.Y = loc.Y - (circle.Height / 2) + area.Y;

                //g.DrawEllipse(new Pen(Brushes.Red, 3.0f), circle);

                // Часы
                //g.DrawLine(new Pen(Brushes.Blue, 2.0f), calc.Center(area),
                //    new PointF(
                //        loc.X - (1 / 2) + area.X,
                //        loc.Y - (1 / 2) + area.Y));

                int miniRadius = (PsevdoCircleDiameter / 2);
                PointF locMini = calc.CirclePoint(miniRadius, angle, new PointF(miniRadius, miniRadius));

                //// Часы Mini
                //g.DrawLine(new Pen(Brushes.Blue, 2.0f), calc.Center(area),
                //    new PointF(
                //        locMini.X - (1 / 2) + PsevdoCircle.X,
                //        locMini.Y - (1 / 2) + PsevdoCircle.Y));


                g.DrawLine(new Pen(Brushes.Blue, 2.0f),
                    new PointF( // Точка на малой орбите
                        locMini.X - (1 / 2) + PsevdoCircle.X,
                        locMini.Y - (1 / 2) + PsevdoCircle.Y),
                    new PointF( // Точка на БОЛЬШОЙ орбите
                        loc.X - (1 / 2) + area.X,
                        loc.Y - (1 / 2) + area.Y)
                        );




                i = i + 30;
            }


            //PointF Center = calc.Center(area);
            //SizeF size = new SizeF();
            //size.Height = 100;
            //size.Width = 100;
            //size.Width = 100;


            //PointF CenterCirc = calc.CirclePoint(100, 0, Center);
            //    RectangleF inside = new RectangleF(Center.X,Center.Y, CenterCirc.X, CenterCirc.Y);
            ////RectangleF inside = new RectangleF(Center, size);
            ////RectangleF inside = new RectangleF(40, 40, 200, 200);
            //g.DrawEllipse(pen, inside);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int Diameter = 300;

            Graphics g = pictureBox1.CreateGraphics();
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Control.DefaultBackColor);

            Pen pen = new Pen(Brushes.Aquamarine, 3.0f);
            RectangleF area = new RectangleF(30, 30, Diameter, Diameter);
            g.DrawEllipse(pen, area);   // Главный круг

            RectangleF circle = new RectangleF(0, 0, 5, 5);   // размеры круга на орбите
            PointF loc = PointF.Empty;
            float radius = (Diameter / 2);
            //float angle = 0.0f;
            PointF org = new PointF(radius, radius);

            // Внутренний круг
            int PsevdoCircleDiameter = Diameter - 50;
            RectangleF PsevdoCircle = new RectangleF(0, 0, PsevdoCircleDiameter, PsevdoCircleDiameter);
            PsevdoCircle.X = calc.Center(area).X - (PsevdoCircle.Width / 2);
            PsevdoCircle.Y = calc.Center(area).Y - (PsevdoCircle.Height / 2);
            g.DrawEllipse(new Pen(Brushes.Brown, 2.0f), PsevdoCircle);

            for (int angle = 0; angle < 360;)
            {
                loc = calc.CirclePoint(radius, angle, org);

                // маленькие планеты
                circle.X = loc.X - (circle.Width / 2) + area.X;
                circle.Y = loc.Y - (circle.Height / 2) + area.Y;
                g.DrawEllipse(new Pen(Brushes.Red, 3.0f), circle);

                int miniRadius = (PsevdoCircleDiameter / 2);
                PointF locMini = calc.CirclePoint(miniRadius, angle, new PointF(miniRadius, miniRadius));

                // Точка на малой орбите
                float miniX = locMini.X - (1 / 2) + PsevdoCircle.X;
                float miniY = locMini.Y - (1 / 2) + PsevdoCircle.Y;
                // Точка на БОЛЬШОЙ орбите
                float maxX = loc.X - (1 / 2) + area.X;
                float maxY = loc.Y - (1 / 2) + area.Y;

                g.DrawLine(new Pen(Brushes.Blue, 2.0f), new PointF(miniX, miniY), new PointF(maxX, maxY));
                

                Font font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Bold);
                float x = ((loc.X - (1 / 2) + area.X) - (locMini.X - (1 / 2) + PsevdoCircle.X)) / 2 + (locMini.X - (1 / 2) + PsevdoCircle.X);
                float y = ((loc.Y - (1 / 2) + area.Y) - (locMini.Y - (1 / 2) + PsevdoCircle.Y)) / 2 + (locMini.Y - (1 / 2) + PsevdoCircle.Y);

                var P = new PointF(x, y);

                g.DrawString("+", font, Brushes.Green, P);


                angle = angle + 30;

            }

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


        public static PointF CirclePoint(float radius, float angleInDegrees, PointF origin)
        {
            float x = (float)(radius * Math.Cos(angleInDegrees * Math.PI / 180f)) + origin.X;
            float y = (float)(radius * Math.Sin(angleInDegrees * Math.PI / 180f)) + origin.Y;
            return new PointF(x, y);
        }
    }
}
