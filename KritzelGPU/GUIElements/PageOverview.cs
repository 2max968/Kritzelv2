using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Kritzel.Main.Renderer;

namespace Kritzel.Main.GUIElements
{
    public partial class PageOverview : UserControl, IClosable
    {
        public event EventHandler<int> ItemSelected;
        public event CloseDelegate CloseEvent;

        Renderer.Image[] pageThumbnails;
        KDocument document;
        int thumbnailSize;
        int space = 32;
        bool redraw = true;
        GPURenderer renderer;
        System.Windows.Forms.Timer tmDraw, tmInput;
        float posY = 0, speedY = 0;
        bool mouseDown = false;
        Point mPos, downPos;
        float friction = 1000;
        Renderer.Image bmpAdd;
        int currentPageIndex = -1;

        public PageOverview(KDocument document, Form parent, KPage currentPage)
        {
            InitializeComponent();

            thumbnailSize = (int)(4 * Util.GetGUISize());

            int border = Util.GetGUISize();
            this.Bounds = new Rectangle(border, border, 
                parent.ClientSize.Width - border*2, parent.ClientSize.Height - border * 2);
            this.Anchor = AnchorStyles.Bottom | AnchorStyles.Left
                | AnchorStyles.Right | AnchorStyles.Top;

            this.document = document;
            pageThumbnails = new Renderer.Image[document.Pages.Count];
            for (int i = 0; i < pageThumbnails.Length; i++)
            {
                Bitmap bmp = document.Pages[i].GetThumbnail(thumbnailSize, thumbnailSize,
                    Color.White, Color.Silver, 4);
                pageThumbnails[i] = new Renderer.Image(bmp);
            }

            renderer = GPURenderer.Create(this);
            mPos = Cursor.Position;

            tmDraw = new System.Windows.Forms.Timer();
            tmDraw.Interval = 30;
            tmDraw.Start();
            tmDraw.Tick += tmDraw_Tick;
            tmInput = new System.Windows.Forms.Timer();
            tmInput.Interval = 200;
            tmInput.Start();
            tmInput.Tick += TmInput_Tick;

            bmpAdd = new Renderer.Image(ResManager.LoadIcon("add.svg", thumbnailSize / 2));

            if (currentPage == null)
                currentPageIndex = -1;
            else
                currentPageIndex = document.Pages.IndexOf(currentPage);
            
            this.Disposed += PageOverview_Disposed;
            this.MouseDown += PageOverview_MouseDown;
            this.MouseUp += PageOverview_MouseUp;
            this.MouseClick += PageOverview_MouseClick;
            this.VScroll = true;
        }

        private void PageOverview_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int distSquared = square(e.X - downPos.X) + square(e.Y - downPos.Y);

                if (distSquared < 8 * 8)
                {
                    int w = (int)Math.Max(((this.Width - space) / (float)(space + thumbnailSize)), 1);
                    int l = pageThumbnails.Length + 1;
                    for (int y = 0; y < (float)l / w; y++)
                    {
                        for (int x = 0; x < w; x++)
                        {
                            int ind = y * w + x;
                            var rect = GetRect(x, y);
                            if (rect.Contains(e.Location))
                            {
                                if (ind < l)
                                {
                                    ItemSelected?.Invoke(this, ind);
                                    CloseEvent?.Invoke();
                                }
                            }
                        }
                    }
                }
            }
        }

        int square(int x)
        {
            return x * x;
        }

        private void TmInput_Tick(object sender, EventArgs e)
        {
            float dt = tmInput.Interval / 1000f;
            if (mouseDown)
            {
                Point pos = Cursor.Position;
                speedY = (mPos.Y - pos.Y) / dt;
                mPos = pos;
            }
        }

        private void PageOverview_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void PageOverview_MouseDown(object sender, MouseEventArgs e)
        {
            mPos = Cursor.Position;
            downPos = e.Location;
            mouseDown = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            redraw = true;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            renderer?.Resize(new Size(this.Width, this.Height));
            redraw = true;
        }

        private void PageOverview_Disposed(object sender, EventArgs e)
        {
            for (int i = 0; i < pageThumbnails.Length; i++)
            {
                pageThumbnails[i]?.Dispose();
            }
            renderer.Dispose();
            tmDraw.Dispose();
            tmInput.Dispose();
            bmpAdd.Dispose();
        }

        private void tmDraw_Tick(object sender, EventArgs e)
        {
            int w = (int)Math.Max(((this.Width - space) / (float)(space + thumbnailSize)), 1);
            int l = pageThumbnails.Length + w;
            float dt = tmDraw.Interval / 1000f;
            posY += speedY * dt;

            int top = space + (l / w) * (space + thumbnailSize);
            if (posY < 0) posY = 0;
            if (posY > top) posY = top;

            if (speedY > 0) speedY = Math.Max(0, speedY - friction * dt);
            else if (speedY < 0) speedY = Math.Min(0, speedY + friction * dt);

            if (redraw || speedY != 0)
            {
                redraw = false;
                renderer.Begin(Style.Default.MenuBackground);

                for (int y = 0; y < (float)l/w; y ++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        int ind = y * w + x;
                        if (ind < pageThumbnails.Length)
                        {
                            var rect = GetRect(x, y);
                            renderer.DrawImage(pageThumbnails[ind], rect);
                            if (ind == currentPageIndex)
                                renderer.DrawRect(Style.Default.Selection, 8, rect);
                        }
                        else if(ind == pageThumbnails.Length)
                        {
                            var rect = GetRect(x, y);
                            var rect2 = new RectangleF(rect.X + thumbnailSize / 4, rect.Y + thumbnailSize / 4,
                                thumbnailSize / 2, thumbnailSize / 2);
                            renderer.DrawImage(bmpAdd, rect2);
                        }
                    }
                }
                renderer.End();
            }
        }

        public RectangleF GetRect(int x, int y)
        {
            Rectangle rect = new Rectangle(space + x * (space + thumbnailSize),
                      space + y * (space + thumbnailSize) - (int)posY, thumbnailSize, thumbnailSize);
            return rect;
        }

        public void AddCloseHandler(CloseDelegate handler)
        {
            CloseEvent += handler;

        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            posY += -e.Delta;
            redraw = true;
        }
    }
}
