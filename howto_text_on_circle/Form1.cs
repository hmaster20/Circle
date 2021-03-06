﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Threading;

namespace howto_text_on_circle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Draw the text.
        private void Form1_Load(object sender, EventArgs e)
        {
            // Получить параметры круга.
            // Get the circle's parameters.
            float font_height = 24;
            float radius = Math.Min(
                picText.ClientSize.Width,
                picText.ClientSize.Height) / 2 - font_height - 5;
            float cx = picText.ClientSize.Width / 2;
            float cy = picText.ClientSize.Height / 2;

            // Сделайте растровое изображение для хранения текста.
            // Make a Bitmap to hold the text.
            Bitmap bm = new Bitmap(
                picText.ClientSize.Width,
                picText.ClientSize.Height);

            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.Clear(Color.White);
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                // Don't use TextRenderingHint.AntiAliasGridFit.
                gr.TextRenderingHint = TextRenderingHint.AntiAlias;

                // Сделать шрифт для использования
                // Make a font to use.
                using (Font font = new Font("Times New Roman", font_height, FontStyle.Regular, GraphicsUnit.Pixel))
                {
                    // Draw the circle.
                    gr.DrawEllipse(Pens.Red, cx - radius, cy - radius, 2 * radius, 2 * radius);
                    //gr.DrawEllipse(Pens.Red, cx - radius - 5, cy - radius -5 , 2 * radius+ 10, 2 * radius+ 10);
                    gr.DrawEllipse(Pens.Blue, cx - radius - 29, cy - radius - 29, 2 * radius + 58, 2 * radius + 58);
                    // Draw the text.
                    DrawTextOnCircle(gr, font, Brushes.Green, radius, cx, cy,
                       "Text on the Top of the Circle",
                        "Text on the Bottom of the Circle");
                }
            }

            // Display the result.
            picText.Image = bm;
        }

        // Нарисуйте текст с центром сверху и снизу указанного круга.
        // Draw text centered on the top and bottom of the indicated circle.
        private void DrawTextOnCircle(Graphics gr, Font font, Brush brush, float radius, float cx, float cy, string top_text, string bottom_text)
        {
            // Используйте StringFormat, чтобы нарисовать средний верх каждого символа в (0, 0).
            // Use a StringFormat to draw the middle top of each character at (0, 0).
            using (StringFormat string_format = new StringFormat())
            {
                string_format.Alignment = StringAlignment.Center;
                string_format.LineAlignment = StringAlignment.Far;

                // Используется для масштабирования от радианов до градусов.
                // Used to scale from radians to degrees.
                double radians_to_degrees = 180.0 / Math.PI;

                // **********************
                // * Draw the top text. *
                // **********************
                // Измерьте символы.
                // Measure the characters.
                List<RectangleF> rects = MeasureCharacters(gr, font, top_text);

                // Use LINQ to add up the character widths.
                var width_query = from RectangleF rect in rects select rect.Width;
                float text_width = width_query.Sum();

                // Найдите начальный угол.
                // Find the starting angle.
                double width_to_angle = 1 / radius;
                double start_angle = -Math.PI / 2 - text_width / 2 * width_to_angle;
                double theta = start_angle;

                // Draw the characters.
                for (int i = 0; i < top_text.Length; i++)
                {
                    // Смотрите, куда идет этот персонаж.
                    // See where this character goes.
                    theta += rects[i].Width / 2 * width_to_angle;
                    double x = cx + radius * Math.Cos(theta);
                    double y = cy + radius * Math.Sin(theta);

                    // Преобразовать, чтобы расположить символ.
                    // Transform to position the character.
                    gr.RotateTransform((float)(radians_to_degrees * (theta + Math.PI / 2)));
                    gr.TranslateTransform((float)x, (float)y, MatrixOrder.Append);

                    // Draw the character.
                    gr.DrawString(top_text[i].ToString(), font, brush, 0, 0, string_format);
                    gr.ResetTransform();

                    // Increment theta.
                    theta += rects[i].Width / 2 * width_to_angle;
                }

                // *************************
                // * Draw the bottom text. *
                // *************************
                // Измерьте символы.
                // Measure the characters.
                rects = MeasureCharacters(gr, font, bottom_text);

                // Use LINQ to add up the character widths.
                width_query = from RectangleF rect in rects select rect.Width;
                text_width = width_query.Sum();

                // Find the starting angle.
                width_to_angle = 1 / radius;
                start_angle = Math.PI / 2 + text_width / 2 * width_to_angle;
                theta = start_angle;

                // Сбросьте StringFormat, чтобы рисовать ниже начала рисунка.
                // Reset the StringFormat to draw below the drawing origin.
                string_format.LineAlignment = StringAlignment.Near;

                // Draw the characters.
                for (int i = 0; i < bottom_text.Length; i++)
                {
                    // See where this character goes.
                    theta -= rects[i].Width / 2 * width_to_angle;
                    double x = cx + radius * Math.Cos(theta);
                    double y = cy + radius * Math.Sin(theta);

                    // Transform to position the character.
                    gr.RotateTransform((float)(radians_to_degrees * (theta - Math.PI / 2)));
                    gr.TranslateTransform((float)x, (float)y, MatrixOrder.Append);

                    // Draw the character.
                    gr.DrawString(bottom_text[i].ToString(), font, brush, 0, 0, string_format);
                    gr.ResetTransform();

                    // Increment theta.
                    theta -= rects[i].Width / 2 * width_to_angle;
                }
            }
        }

        // Измерьте символы в строке.
        // Measure the characters in the string.
        private List<RectangleF> MeasureCharacters(Graphics gr, Font font, string text)
        {
            List<RectangleF> results = new List<RectangleF>();

            // Местоположение X для следующего символа.
            // The X location for the next character.
            float x = 0;

            // Get the character sizes 31 characters at a time.
            for (int start = 0; start < text.Length; start += 32)
            {
                // Get the substring.
                int len = 32;
                if (start + len >= text.Length) len = text.Length - start;
                string substring = text.Substring(start, len);

                // For debugging.
                // Console.WriteLine(substring);

                // Measure the characters.
                List<RectangleF> rects = MeasureCharactersInWord(gr, font, substring);

                // Удалите ввод для первого символа.
                // Remove lead-in for the first character.
                if (start == 0) x += rects[0].Left;

                // For debugging.
                // Console.WriteLine(rects[0].Left);

                // Сохраните все, кроме последнего прямоугольника.
                // Save all but the last rectangle.
                for (int i = 0; i < rects.Count + 1 - 1; i++)
                {
                    RectangleF new_rect = new RectangleF(x, rects[i].Top, rects[i].Width, rects[i].Height);
                    results.Add(new_rect);

                    // Move to the next character's X position.
                    x += rects[i].Width;
                }
            }

            // Return the results.
            return results;
        }

        // Измерить символы в строке длиной не более 32 символов.
        // Measure the characters in a string with no more than 32 characters.
        private List<RectangleF> MeasureCharactersInWord(Graphics gr, Font font, string text)
        {
            List<RectangleF> result = new List<RectangleF>();

            using (StringFormat string_format = new StringFormat())
            {
                string_format.Alignment = StringAlignment.Near;
                string_format.LineAlignment = StringAlignment.Near;
                string_format.Trimming = StringTrimming.None;
                string_format.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;

                CharacterRange[] ranges = new CharacterRange[text.Length];
                for (int i = 0; i < text.Length; i++)
                {
                    ranges[i] = new CharacterRange(i, 1);
                }
                string_format.SetMeasurableCharacterRanges(ranges);

                // Find the character ranges.
                Region[] regions = gr.MeasureCharacterRanges(text, font, this.ClientRectangle, string_format);

                // Convert the regions into rectangles.
                foreach (Region region in regions)
                    result.Add(region.GetBounds(gr));
            }

            return result;
        }






        private void buttonOne_Click(object sender, EventArgs e)
        {
            // Получить параметры круга.
            // Get the circle's parameters.
            float font_height = 24;
            float radius = Math.Min(
                picText.ClientSize.Width,
                picText.ClientSize.Height) / 2 - font_height - 5;
            float cx = picText.ClientSize.Width / 2;
            float cy = picText.ClientSize.Height / 2;

            // Сделайте растровое изображение для хранения текста.
            Bitmap bm = new Bitmap(
                picText.ClientSize.Width,
                picText.ClientSize.Height);

            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.Clear(Color.White);
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                gr.TextRenderingHint = TextRenderingHint.AntiAlias;

                // Сделать шрифт для использования
                using (Font font = new Font("Times New Roman", font_height, FontStyle.Regular, GraphicsUnit.Pixel))
                {
                    // Draw the circle.
                    gr.DrawEllipse(Pens.Red, cx - radius, cy - radius, 2 * radius, 2 * radius);
                    gr.DrawEllipse(Pens.Blue, cx - radius - 29, cy - radius - 29, 2 * radius + 58, 2 * radius + 58);

                    // Draw the text.
                    DrawOnCircle(gr, font, Brushes.Green, radius, cx, cy, "T", "_");
                }
            }

            // Display the result.
            picText.Image = bm;
        }

        // Нарисуйте текст с центром сверху и снизу указанного круга.
        private void DrawOnCircle(Graphics gr, Font font, Brush brush, float radius, float cx, float cy, string top_text, string bottom_text)
        {
            // Используйте StringFormat, чтобы нарисовать средний верх каждого символа в (0, 0).
            using (StringFormat string_format = new StringFormat())
            {
                string_format.Alignment = StringAlignment.Center;
                string_format.LineAlignment = StringAlignment.Far;

                // Используется для масштабирования от радианов до градусов.
                double DegreesInRadians = 180.0 / Math.PI;

                // * Draw the top text. *
                // Измерьте символы.
                float text_width = (MeasureSymbolsInWord(gr, font, top_text)).Width;

                // Найдите начальный угол.
                double width_to_angle = 1 / radius;
                double start_angle = -Math.PI / 2 - text_width / 2 * width_to_angle;
                double theta = start_angle;

                // Draw the characters.
                theta += text_width / 2 * width_to_angle;
                double gradus = 180.0 / Math.PI * theta;
                theta = Math.PI / 180.0 * -180; //-180 -135 -90 -45  0

                double x = cx + radius * Math.Cos(theta);
                double y = cy + radius * Math.Sin(theta);

                // угол поворота символа в градусах
                float angleRotate = (float)(DegreesInRadians * (theta + Math.PI / 2));

                // Преобразовать, чтобы расположить символ.
                gr.RotateTransform(angleRotate);
                gr.TranslateTransform((float)x, (float)y, MatrixOrder.Append);

                // Draw the character.
                gr.DrawString(top_text, font, brush, 0, 0, string_format);
                gr.ResetTransform();

                // ********** Draw the bottom text. **********
                // Измерьте символы.
                text_width = (MeasureSymbolsInWord(gr, font, bottom_text)).Width;

                // Find the starting angle.
                width_to_angle = 1 / radius;
                start_angle = Math.PI / 2 + text_width / 2 * width_to_angle;
                theta = start_angle;

                // Сбросьте StringFormat, чтобы рисовать ниже начала рисунка.
                string_format.LineAlignment = StringAlignment.Near;

                // Draw the characters.
                theta -= text_width / 2 * width_to_angle;
                x = cx + radius * Math.Cos(theta);
                y = cy + radius * Math.Sin(theta);

                // Transform to position the character.
                gr.RotateTransform((float)(DegreesInRadians * (theta - Math.PI / 2)));
                gr.TranslateTransform((float)x, (float)y, MatrixOrder.Append);

                // Draw the character.
                gr.DrawString(bottom_text, font, brush, 0, 0, string_format);
                gr.ResetTransform();
            }
        }

        // Измерить символы в строке
        private RectangleF MeasureSymbolsInWord(Graphics gr, Font font, string text)
        {
            using (StringFormat string_format = new StringFormat())
            {
                string_format.Alignment = StringAlignment.Near;
                string_format.LineAlignment = StringAlignment.Near;
                string_format.Trimming = StringTrimming.None;
                string_format.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
                string_format.SetMeasurableCharacterRanges(new CharacterRange[1] { new CharacterRange(0, 1) });

                // Find the character ranges.
                Region[] regions = gr.MeasureCharacterRanges(text, font, this.ClientRectangle, string_format);
                return regions[0].GetBounds(gr);
            }
        }







        /////////////////////////////////////////////////////////////////////////////////

        private void buttonFor_Click(object sender, EventArgs e)
        {
            // Получить параметры круга.
            // Get the circle's parameters.
            float font_height = 24;
            float radius = Math.Min(
                picText.ClientSize.Width,
                picText.ClientSize.Height) / 2 - font_height - 5;
            float cx = picText.ClientSize.Width / 2;
            float cy = picText.ClientSize.Height / 2;

            // Сделайте растровое изображение для хранения текста.
            Bitmap bm = new Bitmap(
                picText.ClientSize.Width,
                picText.ClientSize.Height);

            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.Clear(Color.White);
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                gr.TextRenderingHint = TextRenderingHint.AntiAlias;

                // Сделать шрифт для использования
                using (Font font = new Font("Times New Roman", font_height, FontStyle.Regular, GraphicsUnit.Pixel))
                {
                    // Draw the circle.
                    gr.DrawEllipse(Pens.Red, cx - radius, cy - radius, 2 * radius, 2 * radius);
                    gr.DrawEllipse(Pens.Blue, cx - radius - 29, cy - radius - 29, 2 * radius + 58, 2 * radius + 58);

                    // Draw the next text.
                    string top_text = "T";
                    string bottom_text = "T";

                    // Используйте StringFormat, чтобы нарисовать средний верх каждого символа в (0, 0).
                    using (StringFormat string_format = new StringFormat())
                    {
                        string_format.Alignment = StringAlignment.Center;
                        string_format.LineAlignment = StringAlignment.Far;

                        double DegreesInRadians = 180.0 / Math.PI;

                        // * Draw the top text. *
                        // Измерьте символы.
                        float text_width = (MeasureSymbolsInWord2(gr, font, top_text)).Width;

                        // Найдите начальный угол.
                        double width_to_angle = 1 / radius;
                        double start_angle = -Math.PI / 2 - text_width / 2 * width_to_angle;
                        double theta = start_angle;

                        // Draw the characters.
                        theta += text_width / 2 * width_to_angle;
                        double gradus = 180.0 / Math.PI * theta;
                        //theta = Math.PI / 180.0 * -135; //-180 -135 -90 -45  0

                        for (int i = 165; i > -15; i -= 30)
                        {
                            theta = Math.PI / 180.0 * -i;
                            double x = cx + radius * Math.Cos(theta);
                            double y = cy + radius * Math.Sin(theta);

                            // угол поворота символа в градусах
                            float angleRotate = (float)(DegreesInRadians * (theta + Math.PI / 2));

                            // Преобразовать, чтобы расположить символ.
                            gr.RotateTransform(angleRotate);
                            gr.TranslateTransform((float)x, (float)y, MatrixOrder.Append);

                            // Draw the character.
                            gr.DrawString(top_text, font, Brushes.Green, 0, 0, string_format);
                            gr.ResetTransform();
                        }


                        // ********** Draw the bottom text. **********
                        // Измерьте символы.
                        text_width = (MeasureSymbolsInWord(gr, font, bottom_text)).Width;

                        // Find the starting angle.
                        width_to_angle = 1 / radius;
                        start_angle = Math.PI / 2 + text_width / 2 * width_to_angle;
                        theta = start_angle;

                        // Сбросьте StringFormat, чтобы рисовать ниже начала рисунка.
                        string_format.LineAlignment = StringAlignment.Near;


                        for (int i = 165; i > -15; i -= 30)
                        {

                            // Draw the characters.
                            //theta -= text_width / 2 * width_to_angle;

                            theta = Math.PI / 180.0 * i;
                            double x = cx + radius * Math.Cos(theta);
                            double y = cy + radius * Math.Sin(theta);

                            // Transform to position the character.
                            gr.RotateTransform((float)(DegreesInRadians * (theta - Math.PI / 2)));
                            gr.TranslateTransform((float)x, (float)y, MatrixOrder.Append);

                            // Draw the character.
                            gr.DrawString(bottom_text, font, Brushes.Gold, 0, 0, string_format);
                            gr.ResetTransform();
                        }

                    }
                }
            }
            // Display the result.
            picText.Image = bm;
        }

        // Измерить символы в строке
        private RectangleF MeasureSymbolsInWord2(Graphics gr, Font font, string text)
        {
            using (StringFormat string_format = new StringFormat())
            {
                string_format.Alignment = StringAlignment.Near;
                string_format.LineAlignment = StringAlignment.Near;
                string_format.Trimming = StringTrimming.None;
                string_format.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
                string_format.SetMeasurableCharacterRanges(new CharacterRange[1] { new CharacterRange(0, 1) });

                // Find the character ranges.
                Region[] regions = gr.MeasureCharacterRanges(text, font, this.ClientRectangle, string_format);
                return regions[0].GetBounds(gr);
            }
        }






        private void buttonMaps_Click(object sender, EventArgs e)
        {
            Graphics g = picText.CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Control.DefaultBackColor);

            Pen pen = new Pen(Brushes.RosyBrown, 2.0f);

            // Главный круг
            int Diameter = picText.Width - 10;
            RectangleF area = new RectangleF(0, 0, Diameter, Diameter);
            g.DrawEllipse(pen, area);

            // Внутренний круг
            int PsevdoCircleDiameter = Diameter - 50;
            RectangleF PsevdoCircle = new RectangleF(0, 0, PsevdoCircleDiameter, PsevdoCircleDiameter);
            PsevdoCircle.X = (area.Left + area.Width / 2) - (PsevdoCircle.Width / 2);
            PsevdoCircle.Y = (area.Top + area.Height / 2) - (PsevdoCircle.Height / 2);
            g.DrawEllipse(pen, PsevdoCircle);


            for (int angle = 0; angle < 360; angle += 30)
            {
                Thread.Sleep(100);
                float radius = (Diameter / 2);
                float miniRadius = (PsevdoCircleDiameter / 2);

                float xMini = (float)(miniRadius * Math.Cos(angle * Math.PI / 180f)) + miniRadius;
                float yMini = (float)(miniRadius * Math.Sin(angle * Math.PI / 180f)) + miniRadius;
                PointF locMini = new PointF(xMini, yMini);

                // Точка на малой орбите
                float miniX = locMini.X - (1 / 2) + PsevdoCircle.X;
                float miniY = locMini.Y - (1 / 2) + PsevdoCircle.Y;
                PointF mini = new PointF(miniX, miniY);


                float xloc = (float)(radius * Math.Cos(angle * Math.PI / 180f)) + radius;
                float yloc = (float)(radius * Math.Sin(angle * Math.PI / 180f)) + radius;
                PointF loc = new PointF(xloc, yloc);

                // Точка на БОЛЬШОЙ орбите
                float maxX = loc.X - (1 / 2) + area.X;
                float maxY = loc.Y - (1 / 2) + area.Y;
                PointF maxy = new PointF(maxX, maxY);

                g.DrawLine(pen, mini, maxy);
            }
        }







        /////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////

        private void buttonMapsWithSymbol_Click(object sender, EventArgs e)
        {
            Graphics g = picText.CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;// добавлен из модуля рисования символов
            g.Clear(Control.DefaultBackColor);

            Pen pen = new Pen(Brushes.RosyBrown, 2.0f);

            // Главный круг
            //int Diameter = picText.Width - 10;
            //RectangleF area = new RectangleF(0, 0, Diameter, Diameter);
            //g.DrawEllipse(pen, area);

            // Внутренний круг
            //int PsevdoCircleDiameter = Diameter - 50;
            //RectangleF PsevdoCircle = new RectangleF(0, 0, PsevdoCircleDiameter, PsevdoCircleDiameter);
            //PsevdoCircle.X = (area.Left + area.Width / 2) - (PsevdoCircle.Width / 2);
            //PsevdoCircle.Y = (area.Top + area.Height / 2) - (PsevdoCircle.Height / 2);
            //g.DrawEllipse(pen, PsevdoCircle);


            //float radius = (Diameter / 2);
            //float miniRadius = (PsevdoCircleDiameter / 2);


            //for (int angle = 0; angle < 360; angle += 30)
            //{
            //    Thread.Sleep(100);

            //    float xMini = (float)(miniRadius * Math.Cos(angle * Math.PI / 180f)) + miniRadius;
            //    float yMini = (float)(miniRadius * Math.Sin(angle * Math.PI / 180f)) + miniRadius;
            //    PointF locMini = new PointF(xMini, yMini);

            //    // Точка на малой орбите
            //    float miniX = locMini.X - (1 / 2) + PsevdoCircle.X;
            //    float miniY = locMini.Y - (1 / 2) + PsevdoCircle.Y;
            //    PointF mini = new PointF(miniX, miniY);


            //    float xloc = (float)(radius * Math.Cos(angle * Math.PI / 180f)) + radius;
            //    float yloc = (float)(radius * Math.Sin(angle * Math.PI / 180f)) + radius;
            //    PointF loc = new PointF(xloc, yloc);

            //    // Точка на БОЛЬШОЙ орбите
            //    float maxX = loc.X - (1 / 2) + area.X;
            //    float maxY = loc.Y - (1 / 2) + area.Y;
            //    PointF maxy = new PointF(maxX, maxY);

            //    g.DrawLine(pen, mini, maxy);
            //}

            /////////////////////////////////////////////////////////////////////

            // Получить параметры круга.
            // Get the circle's parameters.
            //float font_height = 24;
            float font_height = 20;
            float radius = Math.Min(
                picText.ClientSize.Width,
                picText.ClientSize.Height) / 2 - font_height - 5;

            //radius = radius - font_height - 5;
            radius = radius - 5;

            float cx = picText.ClientSize.Width / 2;
            float cy = picText.ClientSize.Height / 2;

            // Сделайте растровое изображение для хранения текста.
            Bitmap bm = new Bitmap(
                picText.ClientSize.Width,
                picText.ClientSize.Height);


            // Сделать шрифт для использования
            using (Font font = new Font("Times New Roman", font_height, FontStyle.Regular, GraphicsUnit.Pixel))
            {
                // Draw the circle.
                g.DrawEllipse(Pens.Red, cx - radius, cy - radius, 2 * radius, 2 * radius);
                g.DrawEllipse(Pens.Blue, cx - radius - 29, cy - radius - 29, 2 * radius + 58, 2 * radius + 58);

                // Draw the next text.
                string top_text = "T";
                string bottom_text = "T";

                // Используйте StringFormat, чтобы нарисовать средний верх каждого символа в (0, 0).
                using (StringFormat string_format = new StringFormat())
                {
                    string_format.Alignment = StringAlignment.Center;
                    string_format.LineAlignment = StringAlignment.Far;

                    double DegreesInRadians = 180.0 / Math.PI;

                    // * Draw the top text. *
                    // Измерьте символы.
                    float text_width = (MeasureSymbolsInWord3(g, font, top_text)).Width;

                    // Начальный угол
                    double width_to_angle = 1 / radius;
                    double start_angle = -Math.PI / 2 - text_width / 2 * width_to_angle;
                    double theta = start_angle;

                    // Draw the characters.
                    theta += text_width / 2 * width_to_angle;
                    double gradus = 180.0 / Math.PI * theta;
                    //theta = Math.PI / 180.0 * -135; //-180 -135 -90 -45  0

                    //string[] zodiac = new string[] { "♈", "♉", "♊", "♋", "♌", "♍", "♎", "♏", "♐", "♑", "♒", "♓" };
                    string[] zodiacU = new string[] { "2648", "2649", "264A", "264B", "264C", "264D", "264E", "264F", "2650", "2651", "2652", "2653" };




                    // Знаки зодиака
                    // Овен         ♈ 	U + 2648
                    // Телец        ♉ 	U + 2649
                    // Близнецы     ♊ 	U + 264A
                    // Рак          ♋ 	U + 264B
                    // Лев          ♌ 	U + 264C
                    // Дева         ♍ 	U + 264D
                    // Весы         ♎ 	U + 264E
                    // Скорпион     ♏ 	U + 264F
                    // Змееносец    ⛎ 	U + 26CE
                    // Стрелец      ♐ 	U + 2650
                    // Козерог      ♑ 	U + 2651
                    // Водолей      ♒ 	U + 2652
                    // Рыбы         ♓ 	U + 2653

                    for (int i = 165, s = 0; i > -15; i -= 30, s++)
                    {
                        theta = Math.PI / 180.0 * -i;
                        double x = cx + radius * Math.Cos(theta);
                        double y = cy + radius * Math.Sin(theta);

                        // угол поворота символа в градусах
                        float angleRotate = (float)(DegreesInRadians * (theta + Math.PI / 2));

                        // Преобразовать, чтобы расположить символ.
                        g.RotateTransform(angleRotate);
                        g.TranslateTransform((float)x, (float)y, MatrixOrder.Append);

                        // Draw the character.
                        //g.DrawString(top_text, font, Brushes.Green, 0, 0, string_format);
                        //g.DrawString(zodiac[s], font, Brushes.Green, 0, 0, string_format);
                        int code = int.Parse(zodiacU[s], System.Globalization.NumberStyles.HexNumber);
                        string unicodeString = char.ConvertFromUtf32(code);
                        g.DrawString(unicodeString, font, Brushes.Green, 0, 0, string_format);

                        g.ResetTransform();
                    }


                    // ********** Draw the bottom text. **********
                    // Измерьте символы.
                    text_width = (MeasureSymbolsInWord3(g, font, bottom_text)).Width;

                    // Начальный угол
                    width_to_angle = 1 / radius;
                    start_angle = Math.PI / 2 + text_width / 2 * width_to_angle;
                    theta = start_angle;

                    // Сбросьте StringFormat, чтобы рисовать ниже начала рисунка.
                    string_format.LineAlignment = StringAlignment.Near;


                    for (int i = 165, s = 6; i > -15; i -= 30, s++)
                    {

                        // Draw the characters.
                        //theta -= text_width / 2 * width_to_angle;

                        theta = Math.PI / 180.0 * i;
                        //double x = cx + 3 + radius * Math.Cos(theta);
                        //double y = cy + 3 + radius * Math.Sin(theta);

                        int sdvig = 0;
                        if (i < 165)
                        {
                            sdvig = 3;
                        }
                        else
                        {
                            sdvig = 0;
                        }

                        double x = cx + sdvig + radius * Math.Cos(theta);
                        double y = cy + sdvig + radius * Math.Sin(theta);

                        // Transform to position the character.
                        g.RotateTransform((float)(DegreesInRadians * (theta - Math.PI / 2)));
                        g.TranslateTransform((float)x, (float)y, MatrixOrder.Append);

                        // Draw the character.
                        //g.DrawString(bottom_text, font, Brushes.Gold, 0, 0, string_format);

                        int code = int.Parse(zodiacU[s], System.Globalization.NumberStyles.HexNumber);
                        string unicodeString = char.ConvertFromUtf32(code);
                        g.DrawString(unicodeString, font, Brushes.Green, 0, 0, string_format);
                        //g.DrawString(zodiac[s], font, Brushes.Green, 0, 0, string_format);
                        g.ResetTransform();
                    }

                    float BigRadius = radius + 29;
                    float miniRadius = radius;

                    for (int angle = 0; angle < 360; angle += 30)
                    {

                        float xMini = (float)(miniRadius * Math.Cos(angle * Math.PI / 180f)) + miniRadius;
                        float yMini = (float)(miniRadius * Math.Sin(angle * Math.PI / 180f)) + miniRadius;
                        PointF locMini = new PointF(xMini, yMini);

                        // Точка на малой орбите
                        //float miniX = locMini.X - (1 / 2) + PsevdoCircle.X;
                        //float miniY = locMini.Y - (1 / 2) + PsevdoCircle.Y;
                        float miniX = locMini.X - (1 / 2) + cx - radius;
                        float miniY = locMini.Y - (1 / 2) + cy - radius;
                        PointF mini = new PointF(miniX, miniY);


                        float xloc = (float)(BigRadius * Math.Cos(angle * Math.PI / 180f)) + BigRadius;
                        float yloc = (float)(BigRadius * Math.Sin(angle * Math.PI / 180f)) + BigRadius;
                        PointF loc = new PointF(xloc, yloc);

                        // Точка на БОЛЬШОЙ орбите
                        //float maxX = loc.X - (1 / 2) + area.X;
                        //float maxY = loc.Y - (1 / 2) + area.Y;
                        float maxX = loc.X - (1 / 2) + cx - radius - 29;
                        float maxY = loc.Y - (1 / 2) + cy - radius - 29;
                        
                        PointF maxy = new PointF(maxX, maxY);

                        g.DrawLine(pen, mini, maxy);
                    }


                    //for (int i = 0, angle = 180; angle > 0; angle += 30, i++)
                    //{
                    //}


                }
            }
        }

        // Измерить символы в строке
        private RectangleF MeasureSymbolsInWord3(Graphics gr, Font font, string text)
        {
            using (StringFormat string_format = new StringFormat())
            {
                string_format.Alignment = StringAlignment.Near;
                string_format.LineAlignment = StringAlignment.Near;
                string_format.Trimming = StringTrimming.None;
                string_format.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
                string_format.SetMeasurableCharacterRanges(new CharacterRange[1] { new CharacterRange(0, 1) });

                // Find the character ranges.
                Region[] regions = gr.MeasureCharacterRanges(text, font, this.ClientRectangle, string_format);
                return regions[0].GetBounds(gr);
            }
        }
    }
}