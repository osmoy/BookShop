using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;

namespace Moy.BookShop.UI.utility
{
    public class ValidataCode
    {
        public static string CreateRandomCode(int length)
        {
            return Guid.NewGuid().ToString("N").Substring(0, length);
        }

        public static byte[] DrawImage(string vcode, float fontSize = 14, Color background = default(Color), Color border = default(Color))
        {
            const int RandAngle = 45;

            var width = vcode.Length * (int)fontSize;

            using (var map = new Bitmap(width + 3, (int)fontSize + 10))
            {
                using (var graphics = Graphics.FromImage(map))
                {
                    graphics.Clear(background);
                    graphics.DrawRectangle(new Pen(border, 0), 0, 0, map.Width - 1, map.Height - 1);

                    var random = new Random();

                    var blackPen = new Pen(Color.DarkGray, 0);
                    for (var i = 0; i < 50; i++)
                    {
                        int x = random.Next(0, map.Width);
                        int y = random.Next(0, map.Height);
                        graphics.DrawRectangle(blackPen, x, y, 1, 1);
                    }

                    var chars = vcode.ToCharArray();

                    var format = new StringFormat(StringFormatFlags.NoClip)
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };

                    Color[] colors = { Color.Black, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple, Color.DarkGoldenrod };

                    FontStyle[] styles = { FontStyle.Bold, FontStyle.Italic, FontStyle.Regular, /*FontStyle.Strikeout,*/ FontStyle.Underline };

                    string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
                    foreach (char item in chars)
                    {
                        int cindex = random.Next(8);
                        int findex = random.Next(5);
                        int sindex = random.Next(4);
                        var font = new Font(fonts[findex], fontSize, styles[sindex]);
                        Brush b = new SolidBrush(colors[cindex]);
                        var dot = new Point(16, 16);
                        float angle = random.Next(-RandAngle, RandAngle);

                        graphics.TranslateTransform(dot.X, dot.Y);
                        graphics.RotateTransform(angle);
                        graphics.DrawString(item.ToString(CultureInfo.InvariantCulture), font, b, 1, 1, format);
                        graphics.RotateTransform(-angle);
                        graphics.TranslateTransform(2, -dot.Y);
                    }
                }

                var stream = new MemoryStream();
                map.Save(stream, ImageFormat.Gif);
                return stream.ToArray();
            }
        }


    }
}