using SoulsFormats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DRB_Editor
{
    public partial class FormPreview : Form
    {
        private List<Bitmap> TextureData;
        private List<Color> PaletteColors;
        private Dictionary<int, string> FmgStrings;

        public FormPreview()
        {
            InitializeComponent();
            ClientSize = new Size(1280, 720);
        }

        public void DrawDialog(DRB.Dlg dialog, List<Bitmap> textureData, List<Color> paletteColors, Dictionary<int, string> fmgStrings)
        {
            Text = dialog.Name;
            TextureData = textureData;
            PaletteColors = paletteColors;
            FmgStrings = fmgStrings;

            var scale = new Vector2(1);
            var bmp = new Bitmap(1280 * (int)scale.X, 720 * (int)scale.Y);
            scale /= new Vector2(1280f / (dialog.RightEdge - dialog.LeftEdge), 720f / (dialog.BottomEdge - dialog.TopEdge));
            var translation = new Vector2(-dialog.LeftEdge, -dialog.TopEdge) * scale;
            using (var g = Graphics.FromImage(bmp))
                DrawDialog(bmp, g, scale, translation, dialog);
            pbxDialog.Image = bmp;
        }

        private void DrawDialog(Bitmap bmp, Graphics g, Vector2 scale, Vector2 translation, DRB.Dlg dlg)
        {
            scale *= new Vector2(1280f / (dlg.RightEdge - dlg.LeftEdge), 720f / (dlg.BottomEdge - dlg.TopEdge));
            translation += new Vector2(-dlg.LeftEdge, -dlg.TopEdge) * scale;

            // For some reason I was drawing all the non-dialog shapes first before;
            // I have no idea why, but I'm making a note of it just in case
            DrawShape(bmp, g, scale, translation, dlg.Shape);
            foreach (DRB.Dlgo dlgo in dlg.Dlgos)
                DrawShape(bmp, g, scale, translation, dlgo.Shape);
        }

        private void DrawShape(Bitmap bmp, Graphics g, Vector2 scale, Vector2 translation, DRB.Shape shape)
        {
            var v1 = new Vector2(shape.LeftEdge, shape.TopEdge) * scale + translation;
            var v2 = new Vector2(shape.RightEdge, shape.BottomEdge) * scale + translation;
            int x = (int)Math.Floor(v1.X);
            int y = (int)Math.Floor(v1.Y);
            int width = (int)Math.Max(1, Math.Ceiling(v2.X - v1.X));
            int height = (int)Math.Max(1, Math.Ceiling(v2.Y - v1.Y));

            if (shape is DRB.Shape.Dialog dialog && dialog.Dlg != null)
            {
                translation += new Vector2(shape.LeftEdge, shape.TopEdge) * scale;
                scale *= new Vector2((shape.RightEdge - shape.LeftEdge) / 1280f, (shape.BottomEdge - shape.TopEdge) / 720f);
                DrawDialog(bmp, g, scale, translation, dialog.Dlg);
            }
            else if (shape is DRB.Shape.MonoRect monoRect)
            {
                Color color = GetColor(monoRect.PaletteColor, monoRect.CustomColor);
                using (var brush = new SolidBrush(color))
                    g.FillRectangle(brush, x, y, width, height);
            }
            else if (shape is DRB.Shape.GouraudRect gouraudRect)
            {
                var colorArray = new Color[] { gouraudRect.TopLeftColor, gouraudRect.TopRightColor, gouraudRect.BottomRightColor, gouraudRect.BottomLeftColor };
                var graphicsPath = new GraphicsPath();
                graphicsPath.AddRectangle(new Rectangle(x, y, width, height));

                using (var pathGradientBrush = new PathGradientBrush(graphicsPath)
                {
                    CenterColor = Color.FromArgb((int)colorArray.Average(c => c.A), (int)colorArray.Average(c => c.R), (int)colorArray.Average(c => c.G), (int)colorArray.Average(c => c.B)),
                    SurroundColors = colorArray
                })
                {
                    g.FillPath(pathGradientBrush, graphicsPath);
                }
            }
            else if (shape is DRB.Shape.TextBase text)
            {
                string s = text.TextLiteral;
                if (text.TextType == DRB.Shape.TxtType.FMG)
                    s = FmgStrings.ContainsKey(text.TextID) ? FmgStrings[text.TextID] : $"<{text.TextID}>";
                else if (text.TextType == DRB.Shape.TxtType.Dynamic)
                    s = "<?>";

                Color color = GetColor(text.PaletteColor, text.CustomColor);
                var rect = new RectangleF(x, y, width, height);
                var format = new StringFormat(StringFormatFlags.NoWrap);
                if (text.Alignment.HasFlag(DRB.Shape.AlignFlags.CenterHorizontal))
                    format.Alignment = StringAlignment.Center;
                else if (text.Alignment.HasFlag(DRB.Shape.AlignFlags.Right))
                    format.Alignment = StringAlignment.Far;
                if (text.Alignment.HasFlag(DRB.Shape.AlignFlags.CenterVertical))
                    format.LineAlignment = StringAlignment.Center;
                else if (text.Alignment.HasFlag(DRB.Shape.AlignFlags.Bottom))
                    format.LineAlignment = StringAlignment.Far;

                using (var font = new Font(DefaultFont.FontFamily, scale.X * 10))
                using (var brush = new SolidBrush(color))
                    g.DrawString(s, font, brush, rect, format);
            }
            else if (shape is DRB.Shape.Sprite sprite && sprite.TextureIndex >= 0 && sprite.TextureIndex < TextureData.Count)
            {
                var texture = TextureData[sprite.TextureIndex];
                var srcRect = new Rectangle(sprite.TexLeftEdge, sprite.TexTopEdge, sprite.TexRightEdge - sprite.TexLeftEdge, sprite.TexBottomEdge - sprite.TexTopEdge);
                var destRect = new Rectangle(x, y, width, height);

                if (srcRect.Width > 0 && srcRect.Height > 0)
                {
                    var tex = new Bitmap(srcRect.Width, srcRect.Height);
                    using (var g2 = Graphics.FromImage(tex))
                    {
                        System.Drawing.Imaging.ColorMatrix colorMatrix = GetColorMatrix(sprite.PaletteColor, sprite.CustomColor);
                        var attr = new System.Drawing.Imaging.ImageAttributes();
                        attr.SetColorMatrix(colorMatrix);
                        g2.InterpolationMode = InterpolationMode.NearestNeighbor;
                        g2.DrawImage(texture, new Rectangle(0, 0, tex.Width, tex.Height), srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, GraphicsUnit.Pixel, attr);
                    }

                    if (sprite.Orientation.HasFlag(DRB.Shape.SpriteOrientation.RotateCW))
                        tex.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    if (sprite.Orientation.HasFlag(DRB.Shape.SpriteOrientation.Rotate180))
                        tex.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    if (sprite.Orientation.HasFlag(DRB.Shape.SpriteOrientation.FlipHorizontal))
                        tex.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    if (sprite.Orientation.HasFlag(DRB.Shape.SpriteOrientation.FlipVertical))
                        tex.RotateFlip(RotateFlipType.RotateNoneFlipY);

                    if (false && (sprite.BlendMode == DRB.Shape.BlendingMode.Add || sprite.BlendMode == DRB.Shape.BlendingMode.Subtract))
                    {
                        var lockedBmp = new LockedBitmap(bmp);
                        lockedBmp.LockBits();
                        var lockedTex = new LockedBitmap(tex);
                        lockedTex.LockBits();
                        for (int texX = 0; texX < lockedTex.Width; texX++)
                        {
                            for (int texY = 0; texY < lockedTex.Height; texY++)
                            {
                                int bmpX = x + texX;
                                int bmpY = y + texY;
                                if (bmpX >= 0 && bmpY >= 0 && bmpX < lockedBmp.Width && bmpY < lockedBmp.Height)
                                {
                                    Color bmpColor = lockedBmp.GetPixel(bmpX, bmpY);
                                    Color texColor = lockedTex.GetPixel(texX, texY);
                                    Color blended = sprite.BlendMode == DRB.Shape.BlendingMode.Add ? AddColor(bmpColor, texColor) : SubtractColor(bmpColor, texColor);
                                    lockedBmp.SetPixel(bmpX, bmpY, blended);
                                }
                            }
                        }
                        lockedBmp.UnlockBits();
                        lockedTex.UnlockBits();
                    }
                    else
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.DrawImage(tex, destRect);
                    }
                }
            }
        }

        private Color GetColor(int paletteColor, Color customColor)
        {
            if (paletteColor == 0)
                return customColor;
            else if (paletteColor < PaletteColors.Count)
                return PaletteColors[paletteColor];
            else
                return Color.HotPink;
        }

        private System.Drawing.Imaging.ColorMatrix GetColorMatrix(int paletteColor, Color customColor)
        {
            var color = GetColor(paletteColor, customColor);
            float rm = color.R / 255f;
            float gm = color.G / 255f;
            float bm = color.B / 255f;
            float am = color.A / 255f;
            var matrix = new float[][]
            {
                new float[] { rm, 0, 0, 0, 0 },
                new float[] { 0, gm, 0, 0, 0 },
                new float[] { 0, 0, bm, 0, 0 },
                new float[] { 0, 0, 0, am, 0 },
                new float[] { 0, 0, 0, 0, 0 },
            };
            return new System.Drawing.Imaging.ColorMatrix(matrix);
        }

        private Color AddColor(Color baseColor, Color blendColor)
        {
            byte CalcComponent(byte compA, byte compB)
            {
                return (byte)Math.Min(compA + compB, 255);
            }

            byte r = CalcComponent(baseColor.R, blendColor.R);
            byte g = CalcComponent(baseColor.G, blendColor.G);
            byte b = CalcComponent(baseColor.B, blendColor.B);
            return Color.FromArgb(baseColor.A, r, g, b);
        }

        private Color SubtractColor(Color baseColor, Color blendColor)
        {
            byte CalcComponent(byte compA, byte compB)
            {
                return (byte)Math.Max(compA - compB, 0);
            }

            byte r = CalcComponent(baseColor.R, blendColor.R);
            byte g = CalcComponent(baseColor.G, blendColor.G);
            byte b = CalcComponent(baseColor.B, blendColor.B);
            return Color.FromArgb(baseColor.A, r, g, b);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            colorDialog.ShowDialog();
            BackColor = colorDialog.Color;
        }
    }
}
