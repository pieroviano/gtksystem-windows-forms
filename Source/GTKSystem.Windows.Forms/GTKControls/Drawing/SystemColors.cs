// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
namespace System.Drawing;

#if CONVERT && NET462_OR_GREATER
extern alias sd;
using SdColor = sd::System.Drawing.Color;
#else
using SdColor = System.Drawing.Color;
#endif
using GtkKnownColor = System.Drawing.KnownColor;

public static class
#if NET462_OR_GREATER
    SystemColors
#else
    GtkSystemColors
#endif
{
    public static SdColor ActiveBorder => SdColor.FromArgb(GtkKnownColor.ActiveBorder.FromKnownColor().ToArgb());
    public static SdColor ActiveCaption => SdColor.FromArgb(GtkKnownColor.ActiveCaption.FromKnownColor().ToArgb());
    public static SdColor ActiveCaptionText => SdColor.FromArgb(GtkKnownColor.ActiveCaptionText.FromKnownColor().ToArgb());
    public static SdColor AppWorkspace => SdColor.FromArgb(GtkKnownColor.AppWorkspace.FromKnownColor().ToArgb());

    public static SdColor ButtonFace => SdColor.FromArgb(GtkKnownColor.ButtonFace.FromKnownColor().ToArgb());
    public static SdColor ButtonHighlight => SdColor.FromArgb(GtkKnownColor.ButtonHighlight.FromKnownColor().ToArgb());
    public static SdColor ButtonShadow => SdColor.FromArgb(GtkKnownColor.ButtonShadow.FromKnownColor().ToArgb());

    public static SdColor Control => SdColor.FromArgb(GtkKnownColor.Control.FromKnownColor().ToArgb());
    public static SdColor ControlDark => SdColor.FromArgb(GtkKnownColor.ControlDark.FromKnownColor().ToArgb());
    public static SdColor ControlDarkDark => SdColor.FromArgb(GtkKnownColor.ControlDarkDark.FromKnownColor().ToArgb());
    public static SdColor ControlLight => SdColor.FromArgb(GtkKnownColor.ControlLight.FromKnownColor().ToArgb());
    public static SdColor ControlLightLight => SdColor.FromArgb(GtkKnownColor.ControlLightLight.FromKnownColor().ToArgb());
    public static SdColor ControlText => SdColor.FromArgb(GtkKnownColor.ControlText.FromKnownColor().ToArgb());

    public static SdColor Desktop => SdColor.FromArgb(GtkKnownColor.Desktop.FromKnownColor().ToArgb());

    public static SdColor GradientActiveCaption => SdColor.FromArgb(GtkKnownColor.GradientActiveCaption.FromKnownColor().ToArgb());
    public static SdColor GradientInactiveCaption => SdColor.FromArgb(GtkKnownColor.GradientInactiveCaption.FromKnownColor().ToArgb());
    public static SdColor GrayText => SdColor.FromArgb(GtkKnownColor.GrayText.FromKnownColor().ToArgb());

    public static SdColor Highlight => SdColor.FromArgb(GtkKnownColor.Highlight.FromKnownColor().ToArgb());
    public static SdColor HighlightText => SdColor.FromArgb(GtkKnownColor.HighlightText.FromKnownColor().ToArgb());
    public static SdColor HotTrack => SdColor.FromArgb(GtkKnownColor.HotTrack.FromKnownColor().ToArgb());

    public static SdColor InactiveBorder => SdColor.FromArgb(GtkKnownColor.InactiveBorder.FromKnownColor().ToArgb());
    public static SdColor InactiveCaption => SdColor.FromArgb(GtkKnownColor.InactiveCaption.FromKnownColor().ToArgb());
    public static SdColor InactiveCaptionText => SdColor.FromArgb(GtkKnownColor.InactiveCaptionText.FromKnownColor().ToArgb());
    public static SdColor Info => SdColor.FromArgb(GtkKnownColor.Info.FromKnownColor().ToArgb());
    public static SdColor InfoText => SdColor.FromArgb(GtkKnownColor.InfoText.FromKnownColor().ToArgb());

    public static SdColor Menu => SdColor.FromArgb(GtkKnownColor.Menu.FromKnownColor().ToArgb());
    public static SdColor MenuBar => SdColor.FromArgb(GtkKnownColor.MenuBar.FromKnownColor().ToArgb());
    public static SdColor MenuHighlight => SdColor.FromArgb(GtkKnownColor.MenuHighlight.FromKnownColor().ToArgb());
    public static SdColor MenuText => SdColor.FromArgb(GtkKnownColor.MenuText.FromKnownColor().ToArgb());

    public static SdColor ScrollBar => SdColor.FromArgb(GtkKnownColor.ScrollBar.FromKnownColor().ToArgb());

    public static SdColor Window => SdColor.FromArgb(GtkKnownColor.Window.FromKnownColor().ToArgb());
    public static SdColor WindowFrame => SdColor.FromArgb(GtkKnownColor.WindowFrame.FromKnownColor().ToArgb());
    public static SdColor WindowText => SdColor.FromArgb(GtkKnownColor.WindowText.FromKnownColor().ToArgb());
}