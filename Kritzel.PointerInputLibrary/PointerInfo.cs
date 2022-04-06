using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Kritzel.PointerInputLibrary
{
    public enum PenFlags
    {
        NONE = 0,
        BARREL = 1,
        INVERTED = 2,
        ERASER = 4
    }

    public class PointerInfo
    { 
        [DllImport("StylusLib.dll")]
        public static extern void SomeFunction();

        [DllImport("user32.dll")]
        public static extern bool GetPointerPenInfo(UInt32 id, out POINTER_PEN_INFO info);
        [DllImport("user32.dll")]
        public static extern bool GetPointerInfoHistory(uint pointerId, ref uint entriesCount, IntPtr pointerInfo);

        //[StructLayout(LayoutKind.Explicit)]
        public struct POINTER_PEN_INFO
        {
            /*[FieldOffset(4)] public uint pointerId;
            [FieldOffset(16)] public IntPtr sourceDevice;
            [FieldOffset(60)] public uint x86historyCount;
            [FieldOffset(68)] public uint x64historyCount;
            [FieldOffset(96)]  public uint x86pressure;
            [FieldOffset(104)] public uint x64pressure;
            [FieldOffset(88)] public PenFlags x86penFlags;
            [FieldOffset(96)] public PenFlags x64penFlags;
            [FieldOffset(200)]
            private int nil;*/
            public POINTER_INFO pointerInfo;
            public PenFlags penFlags;
            public int penMask;
            public uint prssure;
            public uint rotation;
            public int tiltX;
            public int tiltY;
        }

        public struct POINTER_INFO
        {
            public int pointerType;
            public uint pointerId;
            public uint frameId;
            public int pointerFlags;
            public IntPtr sourceDevice;
            public IntPtr hwndTarget;
            public POINT ptPixelLocation;
            public POINT ptHimetricLocation;
            public POINT ptPixelLocationRaw;
            public POINT ptHimetricLocationRaw;
            public uint dwTime;
            public uint historyCount;
            public int InputData;
            public uint dwKeyStates;
            public ulong PerformanceCount;
            public uint ButtonChangeType;
        }

        public struct POINT
        {
            public int x;
            public int y;

            public static explicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.x, p.y);
            }

            public override string ToString()
            {
                return $"({x}; {y})";
            }
        }
    }
}
