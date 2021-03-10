#include <windows.h>
#include <d2d1.h>
#include <gdiplus.h>

#define EXPORT(type) __declspec(dllexport) type __cdecl

int k2dMain(HWND hWnd)
{
    HDC dc = GetDC(hWnd);
    SetPixel(dc, 100, 100, 0xff00ff00);
    return 0;
}

ID2D1Factory1* k2dCreateFactory()
{
    D2D1_FACTORY_OPTIONS options = {D2D1_DEBUG_LEVEL_ERROR};
    ID2D1Factory1* m_d2dFactory;
    DX::ThrowIfFailed(
        D2D1CreateFactory(
            D2D1_FACTORY_TYPE_SINGLE_THREADED,
            __uuidof(ID2D1Factory1),
            &options,
            m_d2dFactory
            )
        );
    return m_d2dFactory;
}