using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PixelArt.Service
{
    public class ConvertService
    {
        Bitmap btm = new Bitmap(1, 1);
        Bitmap bBt = new Bitmap(1, 1);

        Color[] clrs = new Color[1];
        Graphics g = null;

        public async Task<byte[]> Convert(IFormFile image, List<string> hexCodes)
        {
            clrs = new Color[hexCodes.Count];

            for (int v = 0; v < hexCodes.Count; v++)
            {
                try
                {
                    clrs[v] = ColorTranslator.FromHtml(hexCodes[v]);
                }
                catch
                {
                    clrs[v] = Color.Transparent;
                }
            }

            int num = (int)4;

            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);

                using (var img = Image.FromStream(memoryStream))
                {
                    btm = (Bitmap)img;
                    bBt = new Bitmap(btm.Width, btm.Height);

                    using (g = Graphics.FromImage(bBt))
                    {
                        List<Color> block = new List<Color>();

                        Rectangle rec = new Rectangle();

                        SolidBrush sb = new SolidBrush(Color.Black);

                        Color final = Color.Black;

                        for (int x = 0; x < btm.Width; x += num)
                        {
                            for (int y = 0; y < btm.Height; y += num)
                            {
                                block = new List<Color>();

                                for (int v = 0; v < num; v++)
                                {
                                    for (int c = 0; c < num; c++)
                                    {
                                        if (x + v < btm.Width && y + c < btm.Height)
                                        {
                                            block.Add(btm.GetPixel(x + v, y + c));
                                        }
                                    }
                                }

                                if (block.Count > 0)
                                {
                                    final = Clr(block.ToArray());

                                    sb.Color = final;

                                    rec.X = x;
                                    rec.Y = y;
                                    rec.Width = num;
                                    rec.Height = num;

                                    g.FillRectangle(sb, rec);
                                }
                            }
                        }
                    }

                    MemoryStream ms = new MemoryStream();
                    bBt.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return ms.ToArray();
                }
            }
        }

        public Color Clr(Color[] cs)
        {
            Color c = Color.Black;

            int r = 0;
            int g = 0;
            int b = 0;

            for (int i = 0; i < cs.Length; i++)
            {
                r += cs[i].R;
                g += cs[i].G;
                b += cs[i].B;
            }

            r /= cs.Length;
            g /= cs.Length;
            b /= cs.Length;

            int near = 1000;
            int ind = 0;

            for (int cl = 0; cl < clrs.Length; cl++)
            {
                int valR = (clrs[cl].R - r);
                int valG = (clrs[cl].G - g);
                int valB = (clrs[cl].B - b);

                if (valR < 0) valR = -valR;
                if (valG < 0) valG = -valG;
                if (valB < 0) valB = -valB;

                int total = valR + valG + valB;

                if (total < near)
                {
                    ind = cl;
                    near = total;
                }
            }

            c = clrs[ind];

            return c;
        }
    }
}
