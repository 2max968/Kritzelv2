using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main.ScreenObject
{
    public abstract class Transformer : BaseScreenObject
    {
        public bool Active = true;
        public List<Transformer> Others;
        public bool MouseDown = false;

        public abstract void SetPosition(int x, int y);

        public bool IsMouseDown()
        {
            foreach (Transformer t in Others)
                if (t != this && t.MouseDown)
                    return true;
            return false;
        }
    }
}
