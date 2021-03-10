using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kritzel.Main.Dialogues
{
    public partial class PageList : Form
    {
        KDocument doc;
        InkControl cltr;

        public PageList(KDocument doc, InkControl cltr)
        {
            InitializeComponent();
            this.doc = doc;
            this.cltr = cltr;
            
            for(int i = 0; i < doc.Pages.Count; i++)
            {
                KPage page = doc.Pages[i];
                int imgInd = imgList.Images.Count;
                imgList.Images.Add(page.GetThumbnail(imgList.ImageSize.Width, imgList.ImageSize.Height, 
                    Color.Transparent, Color.Teal, 4));
                ListViewItem itm = new ListViewItem($"Page {i + 1}");
                itm.ImageIndex = imgInd;
                itm.Tag = page;
                listView1.Items.Add(itm);
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left 
                && listView1.FocusedItem != null 
                && listView1.FocusedItem.Tag is KPage)
            {
                KPage page = (KPage)listView1.FocusedItem.Tag;
                cltr.LoadPage(page);
            }
        }
    }
}
