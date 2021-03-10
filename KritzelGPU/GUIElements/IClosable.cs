using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.Main.GUIElements
{
    public delegate void CloseDelegate();

    public interface IClosable
    {
        void AddCloseHandler(CloseDelegate handler);
    }
}
