using System;
using System.Drawing;

namespace OCR
{
    public class ImageImprovment
    {
        static public Bitmap Contrast(Bitmap image, int ContrastRefValue)
        {
            Color c;
            byte brightness = 0;

            //ContrastRefValue = calcContrastRefValue(image);

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    c = image.GetPixel(x, y);

                    brightness = (byte)(c.R + c.B + c.G);

                    if (brightness > ContrastRefValue)
                        image.SetPixel(x, y, Color.White);
                    else
                        image.SetPixel(x, y, Color.Black);
                }
            }
            return image;
        }

        static public Bitmap ImageNoise(Bitmap image, sbyte ImageNoiseSphere, float ImageNoiseRefValue)
        {
            int fields = 0;
            int colorfields = 0;
            float percentage = 0;

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    if (image.GetPixel(x, y).ToArgb() == Color.Black.ToArgb())
                    {
                        ImageImprovment.sphere(image, x, y, ImageNoiseSphere, out fields, out colorfields);
                        percentage = Convert.ToSingle(colorfields) / Convert.ToSingle(fields);

                        if (percentage <= ImageNoiseRefValue)
                            image.SetPixel(x, y, Color.White);
                    }
                }
            }
            return image;
        }

        static public Bitmap Cut(Bitmap image)
        {
            int disLeft = 0, disTop = 0, disRight = 0, disBottom = 0;
            
            //Abstand oben
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    if (image.GetPixel(x, y).ToArgb() == Color.Black.ToArgb())
                    {
                        disTop = y;
                        break;
                    }
                }
                if (disTop != 0)
                    break;
            }

            //Abstand unten
            for (int y = image.Height - 1; y > 0; y--)
            {
                for (int x = image.Width - 1; x > 0; x--)
                {
                    if (image.GetPixel(x, y).ToArgb() == Color.Black.ToArgb())
                    {
                        disBottom = y;
                        break;
                    }
                }
                if (disBottom != 0)
                    break;
            }

            //Abstand links
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    if (image.GetPixel(x, y).ToArgb() == Color.Black.ToArgb())
                    {
                        disLeft = x;
                        break;
                    }
                }
                if (disLeft != 0)
                    break;
            }

            //Abstand rechts
            for (int x = image.Width - 1; x > 0; x--)
            {
                for (int y = image.Height - 1; y > 0; y--)
                {
                    if (image.GetPixel(x, y).ToArgb() == Color.Black.ToArgb())
                    {
                        disRight = x;
                        break;
                    }
                }
                if (disRight != 0)
                    break;
            }

            Point TopLeft = new Point(disLeft - 2, disTop - 2);
            Point BottomRight = new Point(disRight + 3, disBottom + 3);

            return ImageImprovment.GetSubImage(image, TopLeft, BottomRight);
        }

        static public Bitmap GetSubImage(Bitmap image, Point LeftTop, Point RightBottom)
        {
            Size s = new Size(RightBottom.X - LeftTop.X, RightBottom.Y - LeftTop.Y);
            Rectangle r = new Rectangle(LeftTop, s);

            Bitmap cuttedImage = new Bitmap(s.Width, s.Height);
            Graphics g = Graphics.FromImage(cuttedImage);

            g.DrawImage(image, 0, 0, r, GraphicsUnit.Pixel);

            return cuttedImage;
        }

        static private void sphere(Bitmap image, int x, int y, sbyte ImageNoiseSphere ,out int f, out int cf)
        {
            int fields = 0;
            int colorfields = 0;

            for (int i = x - ImageNoiseSphere; i <= x + ImageNoiseSphere; i++)
            {
                for (int j = y - ImageNoiseSphere; j <= y + ImageNoiseSphere; j++)
                {
                    if (i < 0 || j < 0 || i >= image.Width || j >= image.Height)
                        continue;

                    fields++;

                    if (image.GetPixel(i, j).ToArgb() == Color.Black.ToArgb())
                        colorfields++;
                }
            }
            f = fields;
            cf = colorfields;
        }
    }
}
