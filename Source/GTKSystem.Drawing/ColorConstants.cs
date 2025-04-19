namespace System.Drawing;

public static class ColorConstants
{
    internal const int ARGBAlphaShift = 24;
    internal const int ARGBRedShift = 16;
    internal const int ARGBGreenShift = 8;
    internal const int ARGBBlueShift = 0;
    internal const uint ARGBAlphaMask = 0xFFu << ARGBAlphaShift;
    internal const uint ARGBRedMask = 0xFFu << ARGBRedShift;
    internal const uint ARGBGreenMask = 0xFFu << ARGBGreenShift;
    internal const uint ARGBBlueMask = 0xFFu << ARGBBlueShift;
}