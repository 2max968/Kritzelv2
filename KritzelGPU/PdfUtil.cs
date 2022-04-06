using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main
{
    public static class PdfUtil
    {
        public static XColor Pdf(this Color c)
        {
            return XColor.FromArgb(c.ToArgb());
        }

        public static XPoint Pdf(this PointF p)
        {
            return new XPoint(p.X, p.Y);
        }

        public static XPoint Pdf(this Point p)
        {
            return new XPoint(p.X, p.Y);
        }

        public static XRect Pdf(this RectangleF r)
        {
            return new XRect(r.X, r.Y, r.Width, r.Height);
        }
    }
}
