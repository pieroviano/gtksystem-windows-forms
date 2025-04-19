// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using GtkKnownColor = System.Drawing.KnownColor;

namespace System.Drawing;

/// <summary>
/// Translates colors to and from GDI+ <see cref='Color'/> objects.
/// </summary>
public static class
#if NET462_OR_GREATER
    ColorTranslator
#else
    GtkColorTranslator
#endif
{
    // COLORREF is 0x00BBGGRR
    internal const int COLORREF_RedShift = 0;
    internal const int COLORREF_GreenShift = 8;
    internal const int COLORREF_BlueShift = 16;

    private const int OleSystemColorFlag = unchecked((int)0x80000000);

    private static Dictionary<string, Color>? s_htmlSysColorTable;

    internal static uint COLORREFToARGB(uint value)
        => ((value >> COLORREF_RedShift) & 0xFF) << Drawing.ColorConstants.ARGBRedShift
           | ((value >> COLORREF_GreenShift) & 0xFF) << Drawing.ColorConstants.ARGBGreenShift
           | ((value >> COLORREF_BlueShift) & 0xFF) << Drawing.ColorConstants.ARGBBlueShift
           | Drawing.ColorConstants.ARGBAlphaMask; // COLORREF's are always fully opaque

    /// <summary>
    /// Translates the specified <see cref='Color'/> to a Win32 color.
    /// </summary>
    public static int ToWin32(Color c)
    {
        return c.R << COLORREF_RedShift | c.G << COLORREF_GreenShift | c.B << COLORREF_BlueShift;
    }

    /// <summary>
    /// Translates the specified <see cref='Color'/> to an Ole color.
    /// </summary>
    public static int ToOle(Color c)
    {
        // IMPORTANT: This signature is invoked directly by the runtime marshaler and cannot change without
        // also updating the runtime.

        // This method converts Color to an OLE_COLOR.
        // https://docs.microsoft.com/openspecs/office_file_formats/ms-oforms/4b8f4be0-3fff-4e42-9fc1-b9fd00251e8e

        if (c.IsKnownColor() && c.IsSystemColor())
        {
            // Unfortunately KnownColor didn't keep the same ordering as the various GetSysColor()
            // COLOR_ * values, otherwise this could be greatly simplified.

            switch (KnownColorTable.ArgbToGtkKnownColor(unchecked((uint)c.ToArgb())))
            {
                case GtkKnownColor.ActiveBorder:
                    return unchecked((int)0x8000000A);
                case GtkKnownColor.ActiveCaption:
                    return unchecked((int)0x80000002);
                case GtkKnownColor.ActiveCaptionText:
                    return unchecked((int)0x80000009);
                case GtkKnownColor.AppWorkspace:
                    return unchecked((int)0x8000000C);
                case GtkKnownColor.ButtonFace:
                    return unchecked((int)0x8000000F);
                case GtkKnownColor.ButtonHighlight:
                    return unchecked((int)0x80000014);
                case GtkKnownColor.ButtonShadow:
                    return unchecked((int)0x80000010);
                case GtkKnownColor.Control:
                    return unchecked((int)0x8000000F);
                case GtkKnownColor.ControlDark:
                    return unchecked((int)0x80000010);
                case GtkKnownColor.ControlDarkDark:
                    return unchecked((int)0x80000015);
                case GtkKnownColor.ControlLight:
                    return unchecked((int)0x80000016);
                case GtkKnownColor.ControlLightLight:
                    return unchecked((int)0x80000014);
                case GtkKnownColor.ControlText:
                    return unchecked((int)0x80000012);
                case GtkKnownColor.Desktop:
                    return unchecked((int)0x80000001);
                case GtkKnownColor.GradientActiveCaption:
                    return unchecked((int)0x8000001B);
                case GtkKnownColor.GradientInactiveCaption:
                    return unchecked((int)0x8000001C);
                case GtkKnownColor.GrayText:
                    return unchecked((int)0x80000011);
                case GtkKnownColor.Highlight:
                    return unchecked((int)0x8000000D);
                case GtkKnownColor.HighlightText:
                    return unchecked((int)0x8000000E);
                case GtkKnownColor.HotTrack:
                    return unchecked((int)0x8000001A);
                case GtkKnownColor.InactiveBorder:
                    return unchecked((int)0x8000000B);
                case GtkKnownColor.InactiveCaption:
                    return unchecked((int)0x80000003);
                case GtkKnownColor.InactiveCaptionText:
                    return unchecked((int)0x80000013);
                case GtkKnownColor.Info:
                    return unchecked((int)0x80000018);
                case GtkKnownColor.InfoText:
                    return unchecked((int)0x80000017);
                case GtkKnownColor.Menu:
                    return unchecked((int)0x80000004);
                case GtkKnownColor.MenuBar:
                    return unchecked((int)0x8000001E);
                case GtkKnownColor.MenuHighlight:
                    return unchecked((int)0x8000001D);
                case GtkKnownColor.MenuText:
                    return unchecked((int)0x80000007);
                case GtkKnownColor.ScrollBar:
                    return unchecked((int)0x80000000);
                case GtkKnownColor.Window:
                    return unchecked((int)0x80000005);
                case GtkKnownColor.WindowFrame:
                    return unchecked((int)0x80000006);
                case GtkKnownColor.WindowText:
                    return unchecked((int)0x80000008);
            }
        }

        return ToWin32(c);
    }

    /// <summary>
    /// Translates an Ole color value to a GDI+ <see cref='Color'/>.
    /// </summary>
    public static Color FromOle(int oleColor)
    {
        // IMPORTANT: This signature is invoked directly by the runtime marshaler and cannot change without
        // also updating the runtime.

        if ((oleColor & OleSystemColorFlag) != 0)
        {
            switch (oleColor)
            {
                case unchecked((int)0x8000000A):
                    return GtkKnownColor.ActiveBorder.FromKnownColor();
                case unchecked((int)0x80000002):
                    return GtkKnownColor.ActiveCaption.FromKnownColor();
                case unchecked((int)0x80000009):
                    return GtkKnownColor.ActiveCaptionText.FromKnownColor();
                case unchecked((int)0x8000000C):
                    return GtkKnownColor.AppWorkspace.FromKnownColor();
                case unchecked((int)0x8000000F):
                    return GtkKnownColor.Control.FromKnownColor();
                case unchecked((int)0x80000010):
                    return GtkKnownColor.ControlDark.FromKnownColor();
                case unchecked((int)0x80000015):
                    return GtkKnownColor.ControlDarkDark.FromKnownColor();
                case unchecked((int)0x80000016):
                    return GtkKnownColor.ControlLight.FromKnownColor();
                case unchecked((int)0x80000014):
                    return GtkKnownColor.ControlLightLight.FromKnownColor();
                case unchecked((int)0x80000012):
                    return GtkKnownColor.ControlText.FromKnownColor();
                case unchecked((int)0x80000001):
                    return GtkKnownColor.Desktop.FromKnownColor();
                case unchecked((int)0x8000001B):
                    return GtkKnownColor.GradientActiveCaption.FromKnownColor();
                case unchecked((int)0x8000001C):
                    return GtkKnownColor.GradientInactiveCaption.FromKnownColor();
                case unchecked((int)0x80000011):
                    return GtkKnownColor.GrayText.FromKnownColor();
                case unchecked((int)0x8000000D):
                    return GtkKnownColor.Highlight.FromKnownColor();
                case unchecked((int)0x8000000E):
                    return GtkKnownColor.HighlightText.FromKnownColor();
                case unchecked((int)0x8000001A):
                    return GtkKnownColor.HotTrack.FromKnownColor();
                case unchecked((int)0x8000000B):
                    return GtkKnownColor.InactiveBorder.FromKnownColor();
                case unchecked((int)0x80000003):
                    return GtkKnownColor.InactiveCaption.FromKnownColor();
                case unchecked((int)0x80000013):
                    return GtkKnownColor.InactiveCaptionText.FromKnownColor();
                case unchecked((int)0x80000018):
                    return GtkKnownColor.Info.FromKnownColor();
                case unchecked((int)0x80000017):
                    return GtkKnownColor.InfoText.FromKnownColor();
                case unchecked((int)0x80000004):
                    return GtkKnownColor.Menu.FromKnownColor();
                case unchecked((int)0x8000001E):
                    return GtkKnownColor.MenuBar.FromKnownColor();
                case unchecked((int)0x8000001D):
                    return GtkKnownColor.MenuHighlight.FromKnownColor();
                case unchecked((int)0x80000007):
                    return GtkKnownColor.MenuText.FromKnownColor();
                case unchecked((int)0x80000000):
                    return GtkKnownColor.ScrollBar.FromKnownColor();
                case unchecked((int)0x80000005):
                    return GtkKnownColor.Window.FromKnownColor();
                case unchecked((int)0x80000006):
                    return GtkKnownColor.WindowFrame.FromKnownColor();
                case unchecked((int)0x80000008):
                    return GtkKnownColor.WindowText.FromKnownColor();
            }
        }

        // When we don't find a system color, we treat the color as a COLORREF
        return KnownColorTable.ArgbToKnownColor(COLORREFToARGB((uint)oleColor));
    }

    /// <summary>
    /// Translates an Win32 color value to a GDI+ <see cref='Color'/>.
    /// </summary>
    public static Color FromWin32(int win32Color)
    {
        return FromOle(win32Color);
    }

    /// <summary>
    /// Translates the specified <see cref='Color'/> to an Html string color representation.
    /// </summary>
    public static string ToHtml(Color c)
    {
        var colorString = string.Empty;

        if (c.IsEmpty)
            return colorString;

        if (c.IsSystemColor())
        {
            switch (KnownColorTable.ArgbToGtkKnownColor(unchecked((uint)c.ToArgb())))
            {
                case GtkKnownColor.ActiveBorder:
                    colorString = "activeborder";
                    break;
                case GtkKnownColor.GradientActiveCaption:
                case GtkKnownColor.ActiveCaption:
                    colorString = "activecaption";
                    break;
                case GtkKnownColor.AppWorkspace:
                    colorString = "appworkspace";
                    break;
                case GtkKnownColor.Desktop:
                    colorString = "background";
                    break;
                case GtkKnownColor.Control:
                case GtkKnownColor.ControlLight:
                    colorString = "buttonface";
                    break;
                case GtkKnownColor.ControlDark:
                    colorString = "buttonshadow";
                    break;
                case GtkKnownColor.ControlText:
                    colorString = "buttontext";
                    break;
                case GtkKnownColor.ActiveCaptionText:
                    colorString = "captiontext";
                    break;
                case GtkKnownColor.GrayText:
                    colorString = "graytext";
                    break;
                case GtkKnownColor.HotTrack:
                case GtkKnownColor.Highlight:
                    colorString = "highlight";
                    break;
                case GtkKnownColor.MenuHighlight:
                case GtkKnownColor.HighlightText:
                    colorString = "highlighttext";
                    break;
                case GtkKnownColor.InactiveBorder:
                    colorString = "inactiveborder";
                    break;
                case GtkKnownColor.GradientInactiveCaption:
                case GtkKnownColor.InactiveCaption:
                    colorString = "inactivecaption";
                    break;
                case GtkKnownColor.InactiveCaptionText:
                    colorString = "inactivecaptiontext";
                    break;
                case GtkKnownColor.Info:
                    colorString = "infobackground";
                    break;
                case GtkKnownColor.InfoText:
                    colorString = "infotext";
                    break;
                case GtkKnownColor.MenuBar:
                case GtkKnownColor.Menu:
                    colorString = "menu";
                    break;
                case GtkKnownColor.MenuText:
                    colorString = "menutext";
                    break;
                case GtkKnownColor.ScrollBar:
                    colorString = "scrollbar";
                    break;
                case GtkKnownColor.ControlDarkDark:
                    colorString = "threeddarkshadow";
                    break;
                case GtkKnownColor.ControlLightLight:
                    colorString = "buttonhighlight";
                    break;
                case GtkKnownColor.Window:
                    colorString = "window";
                    break;
                case GtkKnownColor.WindowFrame:
                    colorString = "windowframe";
                    break;
                case GtkKnownColor.WindowText:
                    colorString = "windowtext";
                    break;
            }
        }
        else if (c.IsNamedColor())
        {
            if (c == Color.LightGray)
            {
                // special case due to mismatch between Html and enum spelling
                colorString = "LightGrey";
            }
            else
            {
                colorString = c.Name;
            }
        }
        else
        {
            colorString = $"#{c.R:X2}{c.G:X2}{c.B:X2}";
        }

        return colorString;
    }

    private static void InitializeHtmlSysColorTable()
    {
        s_htmlSysColorTable = new Dictionary<string, Color>(27)
        {
            ["activeborder"] = GtkKnownColor.ActiveBorder.FromKnownColor(),
            ["activecaption"] = GtkKnownColor.ActiveCaption.FromKnownColor(),
            ["appworkspace"] = GtkKnownColor.AppWorkspace.FromKnownColor(),
            ["background"] = GtkKnownColor.Desktop.FromKnownColor(),
            ["buttonface"] = GtkKnownColor.Control.FromKnownColor(),
            ["buttonhighlight"] = GtkKnownColor.ControlLightLight.FromKnownColor(),
            ["buttonshadow"] = GtkKnownColor.ControlDark.FromKnownColor(),
            ["buttontext"] = GtkKnownColor.ControlText.FromKnownColor(),
            ["captiontext"] = GtkKnownColor.ActiveCaptionText.FromKnownColor(),
            ["graytext"] = GtkKnownColor.GrayText.FromKnownColor(),
            ["highlight"] = GtkKnownColor.Highlight.FromKnownColor(),
            ["highlighttext"] = GtkKnownColor.HighlightText.FromKnownColor(),
            ["inactiveborder"] = GtkKnownColor.InactiveBorder.FromKnownColor(),
            ["inactivecaption"] = GtkKnownColor.InactiveCaption.FromKnownColor(),
            ["inactivecaptiontext"] = GtkKnownColor.InactiveCaptionText.FromKnownColor(),
            ["infobackground"] = GtkKnownColor.Info.FromKnownColor(),
            ["infotext"] = GtkKnownColor.InfoText.FromKnownColor(),
            ["menu"] = GtkKnownColor.Menu.FromKnownColor(),
            ["menutext"] = GtkKnownColor.MenuText.FromKnownColor(),
            ["scrollbar"] = GtkKnownColor.ScrollBar.FromKnownColor(),
            ["threeddarkshadow"] = GtkKnownColor.ControlDarkDark.FromKnownColor(),
            ["threedface"] = GtkKnownColor.Control.FromKnownColor(),
            ["threedhighlight"] = GtkKnownColor.ControlLight.FromKnownColor(),
            ["threedlightshadow"] = GtkKnownColor.ControlLightLight.FromKnownColor(),
            ["window"] = GtkKnownColor.Window.FromKnownColor(),
            ["windowframe"] = GtkKnownColor.WindowFrame.FromKnownColor(),
            ["windowtext"] = GtkKnownColor.WindowText.FromKnownColor()
        };
    }

    public static Color FromHtml(string text)
    {
        var value = typeof(
#if NET462_OR_GREATER
                                      SystemColors
#else
                                      GtkSystemColors
#endif
                                 ).GetProperty(text)?.GetValue(null);
        return value == null ? default : (Color)value;
    }
}