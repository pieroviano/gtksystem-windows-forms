// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#if NET462_OR_GREATER
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Drawing
{
    /// <summary>
    /// Translates colors to and from GDI+ <see cref='Color'/> objects.
    /// </summary>
    public static class ColorTranslator
    {
        // COLORREF is 0x00BBGGRR
        internal const int COLORREF_RedShift = 0;
        internal const int COLORREF_GreenShift = 8;
        internal const int COLORREF_BlueShift = 16;

        private const int OleSystemColorFlag = unchecked((int)0x80000000);

        private static Dictionary<string, Color>? s_htmlSysColorTable;

        internal static uint COLORREFToARGB(uint value)
            => ((value >> COLORREF_RedShift) & 0xFF) << Color.ARGBRedShift
                | ((value >> COLORREF_GreenShift) & 0xFF) << Color.ARGBGreenShift
                | ((value >> COLORREF_BlueShift) & 0xFF) << Color.ARGBBlueShift
                | Color.ARGBAlphaMask; // COLORREF's are always fully opaque

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

            if (c is { IsKnownColor: true, IsSystemColor: true })
            {
                // Unfortunately KnownColor didn't keep the same ordering as the various GetSysColor()
                // COLOR_ * values, otherwise this could be greatly simplified.

                switch (c.ToKnownColor())
                {
                    case KnownColor.ActiveBorder:
                        return unchecked((int)0x8000000A);
                    case KnownColor.ActiveCaption:
                        return unchecked((int)0x80000002);
                    case KnownColor.ActiveCaptionText:
                        return unchecked((int)0x80000009);
                    case KnownColor.AppWorkspace:
                        return unchecked((int)0x8000000C);
                    case KnownColor.ButtonFace:
                        return unchecked((int)0x8000000F);
                    case KnownColor.ButtonHighlight:
                        return unchecked((int)0x80000014);
                    case KnownColor.ButtonShadow:
                        return unchecked((int)0x80000010);
                    case KnownColor.Control:
                        return unchecked((int)0x8000000F);
                    case KnownColor.ControlDark:
                        return unchecked((int)0x80000010);
                    case KnownColor.ControlDarkDark:
                        return unchecked((int)0x80000015);
                    case KnownColor.ControlLight:
                        return unchecked((int)0x80000016);
                    case KnownColor.ControlLightLight:
                        return unchecked((int)0x80000014);
                    case KnownColor.ControlText:
                        return unchecked((int)0x80000012);
                    case KnownColor.Desktop:
                        return unchecked((int)0x80000001);
                    case KnownColor.GradientActiveCaption:
                        return unchecked((int)0x8000001B);
                    case KnownColor.GradientInactiveCaption:
                        return unchecked((int)0x8000001C);
                    case KnownColor.GrayText:
                        return unchecked((int)0x80000011);
                    case KnownColor.Highlight:
                        return unchecked((int)0x8000000D);
                    case KnownColor.HighlightText:
                        return unchecked((int)0x8000000E);
                    case KnownColor.HotTrack:
                        return unchecked((int)0x8000001A);
                    case KnownColor.InactiveBorder:
                        return unchecked((int)0x8000000B);
                    case KnownColor.InactiveCaption:
                        return unchecked((int)0x80000003);
                    case KnownColor.InactiveCaptionText:
                        return unchecked((int)0x80000013);
                    case KnownColor.Info:
                        return unchecked((int)0x80000018);
                    case KnownColor.InfoText:
                        return unchecked((int)0x80000017);
                    case KnownColor.Menu:
                        return unchecked((int)0x80000004);
                    case KnownColor.MenuBar:
                        return unchecked((int)0x8000001E);
                    case KnownColor.MenuHighlight:
                        return unchecked((int)0x8000001D);
                    case KnownColor.MenuText:
                        return unchecked((int)0x80000007);
                    case KnownColor.ScrollBar:
                        return unchecked((int)0x80000000);
                    case KnownColor.Window:
                        return unchecked((int)0x80000005);
                    case KnownColor.WindowFrame:
                        return unchecked((int)0x80000006);
                    case KnownColor.WindowText:
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
                        return ColorExtension.FromKnownColor(KnownColor.ActiveBorder);
                    case unchecked((int)0x80000002):
                        return ColorExtension.FromKnownColor(KnownColor.ActiveCaption);
                    case unchecked((int)0x80000009):
                        return ColorExtension.FromKnownColor(KnownColor.ActiveCaptionText);
                    case unchecked((int)0x8000000C):
                        return ColorExtension.FromKnownColor(KnownColor.AppWorkspace);
                    case unchecked((int)0x8000000F):
                        return ColorExtension.FromKnownColor(KnownColor.Control);
                    case unchecked((int)0x80000010):
                        return ColorExtension.FromKnownColor(KnownColor.ControlDark);
                    case unchecked((int)0x80000015):
                        return ColorExtension.FromKnownColor(KnownColor.ControlDarkDark);
                    case unchecked((int)0x80000016):
                        return ColorExtension.FromKnownColor(KnownColor.ControlLight);
                    case unchecked((int)0x80000014):
                        return ColorExtension.FromKnownColor(KnownColor.ControlLightLight);
                    case unchecked((int)0x80000012):
                        return ColorExtension.FromKnownColor(KnownColor.ControlText);
                    case unchecked((int)0x80000001):
                        return ColorExtension.FromKnownColor(KnownColor.Desktop);
                    case unchecked((int)0x8000001B):
                        return ColorExtension.FromKnownColor(KnownColor.GradientActiveCaption);
                    case unchecked((int)0x8000001C):
                        return ColorExtension.FromKnownColor(KnownColor.GradientInactiveCaption);
                    case unchecked((int)0x80000011):
                        return ColorExtension.FromKnownColor(KnownColor.GrayText);
                    case unchecked((int)0x8000000D):
                        return ColorExtension.FromKnownColor(KnownColor.Highlight);
                    case unchecked((int)0x8000000E):
                        return ColorExtension.FromKnownColor(KnownColor.HighlightText);
                    case unchecked((int)0x8000001A):
                        return ColorExtension.FromKnownColor(KnownColor.HotTrack);
                    case unchecked((int)0x8000000B):
                        return ColorExtension.FromKnownColor(KnownColor.InactiveBorder);
                    case unchecked((int)0x80000003):
                        return ColorExtension.FromKnownColor(KnownColor.InactiveCaption);
                    case unchecked((int)0x80000013):
                        return ColorExtension.FromKnownColor(KnownColor.InactiveCaptionText);
                    case unchecked((int)0x80000018):
                        return ColorExtension.FromKnownColor(KnownColor.Info);
                    case unchecked((int)0x80000017):
                        return ColorExtension.FromKnownColor(KnownColor.InfoText);
                    case unchecked((int)0x80000004):
                        return ColorExtension.FromKnownColor(KnownColor.Menu);
                    case unchecked((int)0x8000001E):
                        return ColorExtension.FromKnownColor(KnownColor.MenuBar);
                    case unchecked((int)0x8000001D):
                        return ColorExtension.FromKnownColor(KnownColor.MenuHighlight);
                    case unchecked((int)0x80000007):
                        return ColorExtension.FromKnownColor(KnownColor.MenuText);
                    case unchecked((int)0x80000000):
                        return ColorExtension.FromKnownColor(KnownColor.ScrollBar);
                    case unchecked((int)0x80000005):
                        return ColorExtension.FromKnownColor(KnownColor.Window);
                    case unchecked((int)0x80000006):
                        return ColorExtension.FromKnownColor(KnownColor.WindowFrame);
                    case unchecked((int)0x80000008):
                        return ColorExtension.FromKnownColor(KnownColor.WindowText);
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
            string colorString = string.Empty;

            if (c.IsEmpty)
                return colorString;

            if (c.IsSystemColor)
            {
                switch (c.ToKnownColor())
                {
                    case KnownColor.ActiveBorder:
                        colorString = "activeborder";
                        break;
                    case KnownColor.GradientActiveCaption:
                    case KnownColor.ActiveCaption:
                        colorString = "activecaption";
                        break;
                    case KnownColor.AppWorkspace:
                        colorString = "appworkspace";
                        break;
                    case KnownColor.Desktop:
                        colorString = "background";
                        break;
                    case KnownColor.Control:
                    case KnownColor.ControlLight:
                        colorString = "buttonface";
                        break;
                    case KnownColor.ControlDark:
                        colorString = "buttonshadow";
                        break;
                    case KnownColor.ControlText:
                        colorString = "buttontext";
                        break;
                    case KnownColor.ActiveCaptionText:
                        colorString = "captiontext";
                        break;
                    case KnownColor.GrayText:
                        colorString = "graytext";
                        break;
                    case KnownColor.HotTrack:
                    case KnownColor.Highlight:
                        colorString = "highlight";
                        break;
                    case KnownColor.MenuHighlight:
                    case KnownColor.HighlightText:
                        colorString = "highlighttext";
                        break;
                    case KnownColor.InactiveBorder:
                        colorString = "inactiveborder";
                        break;
                    case KnownColor.GradientInactiveCaption:
                    case KnownColor.InactiveCaption:
                        colorString = "inactivecaption";
                        break;
                    case KnownColor.InactiveCaptionText:
                        colorString = "inactivecaptiontext";
                        break;
                    case KnownColor.Info:
                        colorString = "infobackground";
                        break;
                    case KnownColor.InfoText:
                        colorString = "infotext";
                        break;
                    case KnownColor.MenuBar:
                    case KnownColor.Menu:
                        colorString = "menu";
                        break;
                    case KnownColor.MenuText:
                        colorString = "menutext";
                        break;
                    case KnownColor.ScrollBar:
                        colorString = "scrollbar";
                        break;
                    case KnownColor.ControlDarkDark:
                        colorString = "threeddarkshadow";
                        break;
                    case KnownColor.ControlLightLight:
                        colorString = "buttonhighlight";
                        break;
                    case KnownColor.Window:
                        colorString = "window";
                        break;
                    case KnownColor.WindowFrame:
                        colorString = "windowframe";
                        break;
                    case KnownColor.WindowText:
                        colorString = "windowtext";
                        break;
                }
            }
            else if (c.IsNamedColor)
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
                ["activeborder"] = ColorExtension.FromKnownColor(KnownColor.ActiveBorder),
                ["activecaption"] = ColorExtension.FromKnownColor(KnownColor.ActiveCaption),
                ["appworkspace"] = ColorExtension.FromKnownColor(KnownColor.AppWorkspace),
                ["background"] = ColorExtension.FromKnownColor(KnownColor.Desktop),
                ["buttonface"] = ColorExtension.FromKnownColor(KnownColor.Control),
                ["buttonhighlight"] = ColorExtension.FromKnownColor(KnownColor.ControlLightLight),
                ["buttonshadow"] = ColorExtension.FromKnownColor(KnownColor.ControlDark),
                ["buttontext"] = ColorExtension.FromKnownColor(KnownColor.ControlText),
                ["captiontext"] = ColorExtension.FromKnownColor(KnownColor.ActiveCaptionText),
                ["graytext"] = ColorExtension.FromKnownColor(KnownColor.GrayText),
                ["highlight"] = ColorExtension.FromKnownColor(KnownColor.Highlight),
                ["highlighttext"] = ColorExtension.FromKnownColor(KnownColor.HighlightText),
                ["inactiveborder"] = ColorExtension.FromKnownColor(KnownColor.InactiveBorder),
                ["inactivecaption"] = ColorExtension.FromKnownColor(KnownColor.InactiveCaption),
                ["inactivecaptiontext"] = ColorExtension.FromKnownColor(KnownColor.InactiveCaptionText),
                ["infobackground"] = ColorExtension.FromKnownColor(KnownColor.Info),
                ["infotext"] = ColorExtension.FromKnownColor(KnownColor.InfoText),
                ["menu"] = ColorExtension.FromKnownColor(KnownColor.Menu),
                ["menutext"] = ColorExtension.FromKnownColor(KnownColor.MenuText),
                ["scrollbar"] = ColorExtension.FromKnownColor(KnownColor.ScrollBar),
                ["threeddarkshadow"] = ColorExtension.FromKnownColor(KnownColor.ControlDarkDark),
                ["threedface"] = ColorExtension.FromKnownColor(KnownColor.Control),
                ["threedhighlight"] = ColorExtension.FromKnownColor(KnownColor.ControlLight),
                ["threedlightshadow"] = ColorExtension.FromKnownColor(KnownColor.ControlLightLight),
                ["window"] = ColorExtension.FromKnownColor(KnownColor.Window),
                ["windowframe"] = ColorExtension.FromKnownColor(KnownColor.WindowFrame),
                ["windowtext"] = ColorExtension.FromKnownColor(KnownColor.WindowText)
            };
        }

        public static Color FromHtml(string text)
        {
            var value = typeof(SystemColors).GetProperty(text)?.GetValue(null);
            return value == null ? default : (Color)value;
        }
    }
}
#endif