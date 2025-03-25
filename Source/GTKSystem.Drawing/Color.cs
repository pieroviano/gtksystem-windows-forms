// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#if NET462_OR_GREATER
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Drawing;

#if CONVERT
extern alias sd;
using SdColor = sd::System.Drawing.Color;
#endif

[DebuggerDisplay("{NameAndARGBValue}")]
[Editor("System.Drawing.Design.ColorEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
    "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[Serializable]
[TypeConverter("System.Drawing.ColorConverter, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[TypeForwardedFrom("System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
public struct Color : IEquatable<Color>
{
    public static readonly Color Empty= new ();

#if CONVERT
    public static implicit operator SdColor(Color r)
    {
        return SdColor.FromArgb(r.ToArgb());
    }

    public static implicit operator Color(SdColor r)
    {
        return Color.FromArgb(r.ToArgb());
    }
#endif

    // -------------------------------------------------------------------
    //  static list of "web" colors...
    //
    public static Color Transparent => (KnownColor.Transparent).FromKnownColor();

    public static Color AliceBlue => KnownColor.AliceBlue.FromKnownColor();

    public static Color AntiqueWhite => KnownColor.AntiqueWhite.FromKnownColor();

    public static Color Aqua => KnownColor.Aqua.FromKnownColor();

    public static Color Aquamarine => KnownColor.Aquamarine.FromKnownColor();

    public static Color Azure => KnownColor.Azure.FromKnownColor();

    public static Color Beige => KnownColor.Beige.FromKnownColor();

    public static Color Bisque => KnownColor.Bisque.FromKnownColor();

    public static Color Black => KnownColor.Black.FromKnownColor();

    public static Color BlanchedAlmond => KnownColor.BlanchedAlmond.FromKnownColor();

    public static Color Blue => KnownColor.Blue.FromKnownColor();

    public static Color BlueViolet => KnownColor.BlueViolet.FromKnownColor();

    public static Color Brown => KnownColor.Brown.FromKnownColor();

    public static Color BurlyWood => KnownColor.BurlyWood.FromKnownColor();

    public static Color CadetBlue => KnownColor.CadetBlue.FromKnownColor();

    public static Color Chartreuse => KnownColor.Chartreuse.FromKnownColor();

    public static Color Chocolate => KnownColor.Chocolate.FromKnownColor();

    public static Color Coral => KnownColor.Coral.FromKnownColor();

    public static Color CornflowerBlue => KnownColor.CornflowerBlue.FromKnownColor();

    public static Color Cornsilk => KnownColor.Cornsilk.FromKnownColor();

    public static Color Crimson => KnownColor.Crimson.FromKnownColor();

    public static Color Cyan => KnownColor.Cyan.FromKnownColor();

    public static Color DarkBlue => KnownColor.DarkBlue.FromKnownColor();

    public static Color DarkCyan => KnownColor.DarkCyan.FromKnownColor();

    public static Color DarkGoldenrod => KnownColor.DarkGoldenrod.FromKnownColor();

    public static Color DarkGray => KnownColor.DarkGray.FromKnownColor();

    public static Color DarkGreen => KnownColor.DarkGreen.FromKnownColor();

    public static Color DarkKhaki => KnownColor.DarkKhaki.FromKnownColor();

    public static Color DarkMagenta => KnownColor.DarkMagenta.FromKnownColor();

    public static Color DarkOliveGreen => KnownColor.DarkOliveGreen.FromKnownColor();

    public static Color DarkOrange => KnownColor.DarkOrange.FromKnownColor();

    public static Color DarkOrchid => KnownColor.DarkOrchid.FromKnownColor();

    public static Color DarkRed => KnownColor.DarkRed.FromKnownColor();

    public static Color DarkSalmon => KnownColor.DarkSalmon.FromKnownColor();

    public static Color DarkSeaGreen => KnownColor.DarkSeaGreen.FromKnownColor();

    public static Color DarkSlateBlue => KnownColor.DarkSlateBlue.FromKnownColor();

    public static Color DarkSlateGray => KnownColor.DarkSlateGray.FromKnownColor();

    public static Color DarkTurquoise => KnownColor.DarkTurquoise.FromKnownColor();

    public static Color DarkViolet => KnownColor.DarkViolet.FromKnownColor();

    public static Color DeepPink => KnownColor.DeepPink.FromKnownColor();

    public static Color DeepSkyBlue => KnownColor.DeepSkyBlue.FromKnownColor();

    public static Color DimGray => KnownColor.DimGray.FromKnownColor();

    public static Color DodgerBlue => KnownColor.DodgerBlue.FromKnownColor();

    public static Color Firebrick => KnownColor.Firebrick.FromKnownColor();

    public static Color FloralWhite => KnownColor.FloralWhite.FromKnownColor();

    public static Color ForestGreen => KnownColor.ForestGreen.FromKnownColor();

    public static Color Fuchsia => KnownColor.Fuchsia.FromKnownColor();

    public static Color Gainsboro => KnownColor.Gainsboro.FromKnownColor();

    public static Color GhostWhite => KnownColor.GhostWhite.FromKnownColor();

    public static Color Gold => KnownColor.Gold.FromKnownColor();

    public static Color Goldenrod => KnownColor.Goldenrod.FromKnownColor();

    public static Color Gray => KnownColor.Gray.FromKnownColor();

    public static Color Green => KnownColor.Green.FromKnownColor();

    public static Color GreenYellow => KnownColor.GreenYellow.FromKnownColor();

    public static Color Honeydew => KnownColor.Honeydew.FromKnownColor();

    public static Color HotPink => KnownColor.HotPink.FromKnownColor();

    public static Color IndianRed => KnownColor.IndianRed.FromKnownColor();

    public static Color Indigo => KnownColor.Indigo.FromKnownColor();

    public static Color Ivory => KnownColor.Ivory.FromKnownColor();

    public static Color Khaki => KnownColor.Khaki.FromKnownColor();

    public static Color Lavender => KnownColor.Lavender.FromKnownColor();

    public static Color LavenderBlush => KnownColor.LavenderBlush.FromKnownColor();

    public static Color LawnGreen => KnownColor.LawnGreen.FromKnownColor();

    public static Color LemonChiffon => KnownColor.LemonChiffon.FromKnownColor();

    public static Color LightBlue => KnownColor.LightBlue.FromKnownColor();

    public static Color LightCoral => KnownColor.LightCoral.FromKnownColor();

    public static Color LightCyan => KnownColor.LightCyan.FromKnownColor();

    public static Color LightGoldenrodYellow => KnownColor.LightGoldenrodYellow.FromKnownColor();

    public static Color LightGreen => KnownColor.LightGreen.FromKnownColor();

    public static Color LightGray => KnownColor.LightGray.FromKnownColor();

    public static Color LightPink => KnownColor.LightPink.FromKnownColor();

    public static Color LightSalmon => KnownColor.LightSalmon.FromKnownColor();

    public static Color LightSeaGreen => KnownColor.LightSeaGreen.FromKnownColor();

    public static Color LightSkyBlue => KnownColor.LightSkyBlue.FromKnownColor();

    public static Color LightSlateGray => KnownColor.LightSlateGray.FromKnownColor();

    public static Color LightSteelBlue => KnownColor.LightSteelBlue.FromKnownColor();

    public static Color LightYellow => KnownColor.LightYellow.FromKnownColor();

    public static Color Lime => KnownColor.Lime.FromKnownColor();

    public static Color LimeGreen => KnownColor.LimeGreen.FromKnownColor();

    public static Color Linen => KnownColor.Linen.FromKnownColor();

    public static Color Magenta => KnownColor.Magenta.FromKnownColor();

    public static Color Maroon => KnownColor.Maroon.FromKnownColor();

    public static Color MediumAquamarine => KnownColor.MediumAquamarine.FromKnownColor();

    public static Color MediumBlue => KnownColor.MediumBlue.FromKnownColor();

    public static Color MediumOrchid => KnownColor.MediumOrchid.FromKnownColor();

    public static Color MediumPurple => KnownColor.MediumPurple.FromKnownColor();

    public static Color MediumSeaGreen => KnownColor.MediumSeaGreen.FromKnownColor();

    public static Color MediumSlateBlue => KnownColor.MediumSlateBlue.FromKnownColor();

    public static Color MediumSpringGreen => KnownColor.MediumSpringGreen.FromKnownColor();

    public static Color MediumTurquoise => KnownColor.MediumTurquoise.FromKnownColor();

    public static Color MediumVioletRed => KnownColor.MediumVioletRed.FromKnownColor();

    public static Color MidnightBlue => KnownColor.MidnightBlue.FromKnownColor();

    public static Color MintCream => KnownColor.MintCream.FromKnownColor();

    public static Color MistyRose => KnownColor.MistyRose.FromKnownColor();

    public static Color Moccasin => KnownColor.Moccasin.FromKnownColor();

    public static Color NavajoWhite => KnownColor.NavajoWhite.FromKnownColor();

    public static Color Navy => KnownColor.Navy.FromKnownColor();

    public static Color OldLace => KnownColor.OldLace.FromKnownColor();

    public static Color Olive => KnownColor.Olive.FromKnownColor();

    public static Color OliveDrab => KnownColor.OliveDrab.FromKnownColor();

    public static Color Orange => KnownColor.Orange.FromKnownColor();

    public static Color OrangeRed => KnownColor.OrangeRed.FromKnownColor();

    public static Color Orchid => KnownColor.Orchid.FromKnownColor();

    public static Color PaleGoldenrod => KnownColor.PaleGoldenrod.FromKnownColor();

    public static Color PaleGreen => KnownColor.PaleGreen.FromKnownColor();

    public static Color PaleTurquoise => KnownColor.PaleTurquoise.FromKnownColor();

    public static Color PaleVioletRed => KnownColor.PaleVioletRed.FromKnownColor();

    public static Color PapayaWhip => KnownColor.PapayaWhip.FromKnownColor();

    public static Color PeachPuff => KnownColor.PeachPuff.FromKnownColor();

    public static Color Peru => KnownColor.Peru.FromKnownColor();

    public static Color Pink => KnownColor.Pink.FromKnownColor();

    public static Color Plum => KnownColor.Plum.FromKnownColor();

    public static Color PowderBlue => KnownColor.PowderBlue.FromKnownColor();

    public static Color Purple => KnownColor.Purple.FromKnownColor();

    /// <summary>
    /// Gets a system-defined color that has an ARGB value of <c>#663399</c>.
    /// </summary>
    /// <value>A system-defined color.</value>
    public static Color RebeccaPurple => KnownColor.Purple.FromKnownColor();

    public static Color Red => KnownColor.Red.FromKnownColor();

    public static Color RosyBrown => KnownColor.RosyBrown.FromKnownColor();

    public static Color RoyalBlue => KnownColor.RoyalBlue.FromKnownColor();

    public static Color SaddleBrown => KnownColor.SaddleBrown.FromKnownColor();

    public static Color Salmon => KnownColor.Salmon.FromKnownColor();

    public static Color SandyBrown => KnownColor.SandyBrown.FromKnownColor();

    public static Color SeaGreen => KnownColor.SeaGreen.FromKnownColor();

    public static Color SeaShell => KnownColor.SeaShell.FromKnownColor();

    public static Color Sienna => KnownColor.Sienna.FromKnownColor();

    public static Color Silver => KnownColor.Silver.FromKnownColor();

    public static Color SkyBlue => KnownColor.SkyBlue.FromKnownColor();

    public static Color SlateBlue => KnownColor.SlateBlue.FromKnownColor();

    public static Color SlateGray => KnownColor.SlateGray.FromKnownColor();

    public static Color Snow => KnownColor.Snow.FromKnownColor();

    public static Color SpringGreen => KnownColor.SpringGreen.FromKnownColor();

    public static Color SteelBlue => KnownColor.SteelBlue.FromKnownColor();

    public static Color Tan => KnownColor.Tan.FromKnownColor();

    public static Color Teal => KnownColor.Teal.FromKnownColor();

    public static Color Thistle => KnownColor.Thistle.FromKnownColor();

    public static Color Tomato => KnownColor.Tomato.FromKnownColor();

    public static Color Turquoise => KnownColor.Turquoise.FromKnownColor();

    public static Color Violet => KnownColor.Violet.FromKnownColor();

    public static Color Wheat => KnownColor.Wheat.FromKnownColor();

    public static Color White => KnownColor.White.FromKnownColor();

    public static Color WhiteSmoke => KnownColor.WhiteSmoke.FromKnownColor();

    public static Color Yellow => KnownColor.Yellow.FromKnownColor();

    public static Color YellowGreen => KnownColor.YellowGreen.FromKnownColor();
    //
    //  end "web" colors
    // -------------------------------------------------------------------

    // NOTE : The "zero" pattern (all members being 0) must represent
    //      : "not set". This allows "Color c;" to be correct.

    private const short StateKnownColorValid = 0x0001;
    private const short StateARGBValueValid = 0x0002;
    private const short StateValueMask = StateARGBValueValid;
    private const short StateNameValid = 0x0008;
    private const long NotDefinedValue = 0;

    // Shift counts and bit masks for A, R, G, B components in ARGB mode

    // Standard 32bit sRGB (ARGB)
    private long value=0; // Do not rename (binary serialization)

    public Color()
    {
    }

    public byte R => unchecked((byte)(value >> ColorConstants.ARGBRedShift));

    public byte G => unchecked((byte)(value >> ColorConstants.ARGBGreenShift));

    public byte B => unchecked((byte)(value >> ColorConstants.ARGBBlueShift));

    public byte A => unchecked((byte)(value >> ColorConstants.ARGBAlphaShift));

    public bool IsEmpty => ToArgb() == 0;

    // Used for the [DebuggerDisplay]. Inlining in the attribute is possible, but
    // against best practices as the current project language parses the string with
    // language specific heuristics.

    private string NameAndARGBValue => $"{{Name = {Name}, ARGB = ({A}, {R}, {G}, {B})}}";

    public string Name
    {
        get
        {
            var name = this.GetName();
            return !(string.IsNullOrEmpty(name))?name :value.ToString("x");
        }
    }

    private static void CheckByte(int value, string name)
    {
        static void ThrowOutOfByteRange(int v, string n) =>
            throw new ArgumentException(string.Format("SR.InvalidEx2BoundArgument {0} {1} {2} {3}", n, v, byte.MinValue, byte.MaxValue));

        if (unchecked((uint)value) > byte.MaxValue)
            ThrowOutOfByteRange(value, name);
    }

    private static Color FromArgb(uint argb) => new Color() { value = argb };

    public static Color FromArgb(int argb) => new Color() { value = argb };

    public static Color FromArgb(int alpha, int red, int green, int blue)
    {
        CheckByte(alpha, nameof(alpha));
        CheckByte(red, nameof(red));
        CheckByte(green, nameof(green));
        CheckByte(blue, nameof(blue));

        return FromArgb(
            (uint)alpha << ColorConstants.ARGBAlphaShift |
            (uint)red << ColorConstants.ARGBRedShift |
            (uint)green << ColorConstants.ARGBGreenShift |
            (uint)blue << ColorConstants.ARGBBlueShift
        );
    }

    public static Color FromArgb(int alpha, Color baseColor)
    {
        CheckByte(alpha, nameof(alpha));

        return FromArgb(
            (uint)alpha << ColorConstants.ARGBAlphaShift |
            (uint)baseColor.value & ~ColorConstants.ARGBAlphaMask
        );
    }

    public static Color FromArgb(int red, int green, int blue) => FromArgb(byte.MaxValue, red, green, blue);

    public static Color FromName(string name)
    {
        // try to get a known color first
        if (ColorTable.TryGetNamedColor(name, out var color))
                return color;

        // otherwise treat it as a named color
        return Color.Transparent;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void GetRgbValues(out int r, out int g, out int b)
    {
        var localValue = (uint)value;
        r = (int)(localValue & ColorConstants.ARGBRedMask) >> ColorConstants.ARGBRedShift;
        g = (int)(localValue & ColorConstants.ARGBGreenMask) >> ColorConstants.ARGBGreenShift;
        b = (int)(localValue & ColorConstants.ARGBBlueMask) >> ColorConstants.ARGBBlueShift;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void MinMaxRgb(out int min, out int max, int r, int g, int b)
    {
        if (r > g)
        {
            max = r;
            min = g;
        }
        else
        {
            max = g;
            min = r;
        }
        if (b > max)
        {
            max = b;
        }
        else if (b < min)
        {
            min = b;
        }
    }

    public float GetBrightness()
    {
        GetRgbValues(out var r, out var g, out var b);

        MinMaxRgb(out var min, out var max, r, g, b);

        return (max + min) / (byte.MaxValue * 2f);
    }

    public float GetHue()
    {
        GetRgbValues(out var r, out var g, out var b);

        if (r == g && g == b)
            return 0f;

        MinMaxRgb(out var min, out var max, r, g, b);

        float delta = max - min;
        float hue;

        if (r == max)
            hue = (g - b) / delta;
        else if (g == max)
            hue = (b - r) / delta + 2f;
        else
            hue = (r - g) / delta + 4f;

        hue *= 60f;
        if (hue < 0f)
            hue += 360f;

        return hue;
    }

    public float GetSaturation()
    {
        GetRgbValues(out var r, out var g, out var b);

        if (r == g && g == b)
            return 0f;

        MinMaxRgb(out var min, out var max, r, g, b);

        var div = max + min;
        if (div > byte.MaxValue)
            div = byte.MaxValue * 2 - max - min;

        return (max - min) / (float)div;
    }

    public int ToArgb() => unchecked((int)value);

    public override string ToString() =>
        this.IsNamedColor() ? $"{nameof(Color)} [{Name}]" :
        $"{nameof(Color)} [A={A}, R={R}, G={G}, B={B}]";

    public static bool operator ==(Color left, Color right) =>
        left.value == right.value;

    public static bool operator !=(Color left, Color right) => !(left == right);

    public override bool Equals(object? obj) => obj is Color other && Equals(other);

    public bool Equals(Color other) => this == other;

    public override int GetHashCode()
    {
        // Three cases:
        // 1. We don't have a name. All relevant data, including this fact, is in the remaining fields.
        // 2. We have a known name. The name will be the same instance of any other with the same
        // knownColor value, so we can ignore it for hashing. Note this also hashes different to
        // an unnamed color with the same ARGB value.
        // 3. Have an unknown name. Will differ from other unknown-named colors only by name, so we
        // can usefully use the names hash code alone.
        if (!string.IsNullOrEmpty(this.GetName()) && !this.IsKnownColor())
            return this.GetName().GetHashCode();

        return HashCode.Combine(value.GetHashCode());
    }
}
#endif