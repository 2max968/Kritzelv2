using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main
{
    public static class Colors_Cfg
    {
        public const string TEXT = @"#D12324
#E66A0A
#FBC131
#82B92A
#009D63
#585C9C
#A267AB
#FABEA4
#D71470
#B9143C";

        public static List<Color> GetColors()
        {
            List<Color> colors = new List<Color>();
            string[] lines = TEXT.Split('\n');
            foreach(string line in lines)
            {
                Color color = ColorTranslator.FromHtml(line);
                colors.Add(color);
            }
            return colors;
        }
    }
}
