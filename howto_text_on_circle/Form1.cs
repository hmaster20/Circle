using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Drawing2D;
using System.Drawing.Text;

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
            // Get the circle's parameters.
            float font_height = 24;
            float radius = Math.Min(
                picText.ClientSize.Width,
                picText.ClientSize.Height) / 2 - font_height - 5;
            float cx = picText.ClientSize.Width / 2;
            float cy = picText.ClientSize.Height / 2;

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

                // Make a font to use.
                using (Font font = new Font("Times New Roman", font_height,
                    FontStyle.Regular, GraphicsUnit.Pixel))
                {
                    // Draw the circle.
                    gr.DrawEllipse(Pens.Red,
                        cx - radius, cy - radius,
                        2 * radius, 2 * radius);

                    // Draw the text.
                    DrawTextOnCircle(gr, font, Brushes.Blue,
                        radius, cx, cy,
                        "Text on the Top of the Circle",
                        "Text on the Bottom of the Circle");
                }
            }

            // Display the result.
            picText.Image = bm;
        }

        // Draw text centered on the top and bottom of the indicated circle.
        private void DrawTextOnCircle(Graphics gr, Font font, Brush brush,
            float radius, float cx, float cy, string top_text, string bottom_text)
        {
            // Use a StringFormat to draw the middle
            // top of each character at (0, 0).
            using (StringFormat string_format = new StringFormat())
            {
                string_format.Alignment = StringAlignment.Center;
                string_format.LineAlignment = StringAlignment.Far;

                // Used to scale from radians to degrees.
                double radians_to_degrees = 180.0 / Math.PI;

                // **********************
                // * Draw the top text. *
                // **********************
                // Measure the characters.
                List<RectangleF> rects = MeasureCharacters(gr, font, top_text);

                // Use LINQ to add up the character widths.
                var width_query = from RectangleF rect in rects select rect.Width;
                float text_width = width_query.Sum();

                // Find the starting angle.
                double width_to_angle = 1 / radius;
                double start_angle = -Math.PI / 2 - text_width / 2 * width_to_angle;
                double theta = start_angle;

                // Draw the characters.
                for (int i = 0; i < top_text.Length; i++)
                {
                    // See where this character goes.
                    theta += rects[i].Width / 2 * width_to_angle;
                    double x = cx + radius * Math.Cos(theta);
                    double y = cy + radius * Math.Sin(theta);

                    // Transform to position the character.
                    gr.RotateTransform((float)(radians_to_degrees * (theta + Math.PI / 2)));
                    gr.TranslateTransform((float)x, (float)y, MatrixOrder.Append);

                    // Draw the character.
                    gr.DrawString(top_text[i].ToString(), font, brush,
                        0, 0, string_format);
                    gr.ResetTransform();

                    // Increment theta.
                    theta += rects[i].Width / 2 * width_to_angle;
                }

                // *************************
                // * Draw the bottom text. *
                // *************************
                // Measure the characters.
                rects = MeasureCharacters(gr, font, bottom_text);

                // Use LINQ to add up the character widths.
                width_query = from RectangleF rect in rects select rect.Width;
                text_width = width_query.Sum();

                // Find the starting angle.
                width_to_angle = 1 / radius;
                start_angle = Math.PI / 2 + text_width / 2 * width_to_angle;
                theta = start_angle;

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
                    gr.DrawString(bottom_text[i].ToString(), font, brush,
                        0, 0, string_format);
                    gr.ResetTransform();

                    // Increment theta.
                    theta -= rects[i].Width / 2 * width_to_angle;
                }
            }
        }

        // Measure the characters in the string.
        private List<RectangleF> MeasureCharacters(Graphics gr, Font font, string text)
        {
            List<RectangleF> results = new List<RectangleF>();

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

                // Remove lead-in for the first character.
                if (start == 0) x += rects[0].Left;

                // For debugging.
                // Console.WriteLine(rects[0].Left);

                // Save all but the last rectangle.
                for (int i = 0; i < rects.Count + 1 - 1; i++)
                {
                    RectangleF new_rect = new RectangleF(
                        x, rects[i].Top,
                        rects[i].Width, rects[i].Height);
                    results.Add(new_rect);

                    // Move to the next character's X position.
                    x += rects[i].Width;
                }
            }

            // Return the results.
            return results;
        }

        // Measure the characters in a string with
        // no more than 32 characters.
        private List<RectangleF> MeasureCharactersInWord(
            Graphics gr, Font font, string text)
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
                RectangleF rect = new RectangleF(0, 0, 10000, 100);
                Region[] regions =
                    gr.MeasureCharacterRanges(
                        text, font, this.ClientRectangle,
                        string_format);

                // Convert the regions into rectangles.
                foreach (Region region in regions)
                    result.Add(region.GetBounds(gr));
            }

            return result;
        }
    }
}
