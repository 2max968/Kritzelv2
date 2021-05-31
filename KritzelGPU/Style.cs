using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main
{
    public class Style
    {
        public Color MenuBackground;
        public Color MenuForeground;
        public Color MenuContrast;
        public Color Background;
        public Color Selection;

#pragma warning disable CS0067
        public static event EventHandler<Style> StyleChanged;
#pragma warning restore CS0067
        public static Style DefaultLight = new Style()
        {
            MenuBackground = Color.White,
            MenuForeground = Color.Teal,
            MenuContrast = Color.Silver,
            Background = ColorTranslator.FromHtml("#94ABB7"),
            Selection = ColorTranslator.FromHtml("#0078D7")
        };
        public static Style DefaultDark = new Style()
        {
            MenuBackground = ColorTranslator.FromHtml("#404040"),
            MenuForeground = Color.Silver,
            MenuContrast = ColorTranslator.FromHtml("#4F4F4F"),
            Background = ColorTranslator.FromHtml("#36566D"),
            Selection = ColorTranslator.FromHtml("#0078D7")
        };

        public static Style Default;

        public static void SetStyle(Style style)
        {
            Default = style;
            StyleChanged?.Invoke(style, style);
        }

        public static void SetStyle()
        {
            var style = Configuration.DarkMode ? Style.DefaultDark : Style.DefaultLight;
            SetStyle(style);
        }
    }
}
