using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.Bezier
{
    public partial class Form1 : Form
    {
        Factory factory;
        WindowRenderTarget renderTarget;
        PathGeometry path;
        GeometrySink sink;
        SolidColorBrush brush, red, blue;
        RawVector2[] points, directions;
        KVector2[] contour;
        ThickLine line;

        public Form1()
        {
            InitializeComponent();

            points = new RawVector2[]
            {
                new RawVector2(20,20),
                new RawVector2(50,50),
                new RawVector2(50,100),
                new RawVector2(100,100)
            };
            directions = new RawVector2[points.Length];

            BezierSegment[] segments = new BezierSegment[points.Length];

            factory = new Factory(FactoryType.MultiThreaded, DebugLevel.Error);
            renderTarget = new WindowRenderTarget(factory, new RenderTargetProperties()
            {
                Type = RenderTargetType.Software
            }, new HwndRenderTargetProperties()
            {
                Hwnd = this.Handle,
                PixelSize = new SharpDX.Size2(this.ClientSize.Width, this.ClientSize.Height),
                PresentOptions = PresentOptions.Immediately
            });
            renderTarget.AntialiasMode = AntialiasMode.PerPrimitive;
            brush = new SolidColorBrush(renderTarget, new RawColor4(0, 1, 0, 1));
            red = new SolidColorBrush(renderTarget, new RawColor4(1, 0, 0, 1));
            blue = new SolidColorBrush(renderTarget, new RawColor4(0, 0, 1, 1));

            path = new PathGeometry(factory);
            sink = path.Open();

            /*for (int i = 0; i < points.Length; i++)
            {
                if (i == 0) directions[i] = Util.Subtract(points[i + 1], points[i]);
                else if (i == points.Length - 1) directions[i] = Util.Subtract(points[i], points[i - 1]);
                else directions[i] = Util.Subtract(points[i + 1], points[i - 1]);
                directions[i].Normalize();

                if (i > 0)
                {
                    float strength = Util.Subtract(points[i], points[i - 1]).Length() / 100f;
                    segments[i] = new BezierSegment();
                    segments[i].Point1 = Util.Add(points[i - 1], Util.Mult(directions[i - 1], strength));
                    segments[i].Point2 = Util.Subtract(points[i], Util.Mult(directions[i], strength));
                    segments[i].Point3 = points[i];

                    sink.AddBezier(segments[i]);
                }
                else
                {
                    sink.BeginFigure(points[i], FigureBegin.Filled);
                }
            }

            sink.EndFigure(FigureEnd.Open);
            sink.Close();*/

            line = new ThickLine();
            line.AddPoint(20, 20, 2);
            line.AddPoint(80, 20, 5);
            line.AddPoint(100, 40, 5);
            line.AddPoint(200, 40, 10);
            line.AddPoint(300, 20, 5);
            line.AddPoint(300, 100, 2);
            line.CalcContour();
            contour = line.Contour;

            this.FormClosing += Form1_FormClosing;
            timer1.Tick += Timer1_Tick;
            this.ResizeEnd += Form1_ResizeEnd;
            timer1.Start();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            renderTarget.Resize(new Size2(this.ClientSize.Width, this.ClientSize.Height));
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            renderTarget.BeginDraw();
            renderTarget.Clear(new RawColor4(1, 1, 1, 1));
            //renderTarget.DrawGeometry(path, brush);
            //foreach (var point in points)
            //    renderTarget.DrawLine(point, new RawVector2(point.X+4,point.Y+4), red, 4);

            for(int i = 0; i < line.rads.Count; i++)
            {
                Ellipse ellipse = new Ellipse(new RawVector2(line.points[i].X, line.points[i].Y), line.rads[i], line.rads[i]);
                renderTarget.DrawEllipse(ellipse, brush);
            }

            PathGeometry path = new PathGeometry(factory);
            path.FromPoints(contour, line.interpolatedNormals, true);
            renderTarget.FillGeometry(path, blue);
            path.Dispose();

            for(int i = 0;i<contour.Length;i++)
            {
                renderTarget.FillEllipse(new Ellipse(new RawVector2(contour[i].X, contour[i].Y),
                    1, 1), red);
            }

            KVector2 a = new KVector2(200, 200);
            KVector2 a1 = new KVector2(100, 50);
            KVector2 a2 = a1.Orthogonal;
            renderTarget.DrawLine(new RawVector2(a.X, a.Y),
                new RawVector2(a.X + a1.X, a.Y + a1.Y), red);
            renderTarget.DrawLine(new RawVector2(a.X, a.Y),
                new RawVector2(a.X + a2.X, a.Y + a2.Y), brush);

            renderTarget.EndDraw();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            blue.Dispose();
            red.Dispose();
            brush.Dispose();
            sink.Dispose();
            path.Dispose();
            renderTarget.Dispose();
            factory.Dispose();
        }
    }
}
