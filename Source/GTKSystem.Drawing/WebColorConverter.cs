using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using GtkColor = System.Drawing.Color;
using GtkKnownColor = System.Drawing.KnownColor;

namespace System.Drawing;

/// <summary>Converts a predefined color name or an RGB color value to and from a <see cref="T:System.Drawing.Color" /> object.</summary>
public class WebColorConverter : ColorConverter
{
    private static Hashtable? htmlSysColorTable;

    /// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebColorConverter" /> class. </summary>
    public WebColorConverter()
    {
    }

    /// <summary>Converts the given value to the type of the converter.</summary>
    /// <returns>The object resulting from conversion.</returns>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that indicates the context of the object to convert.</param>
    /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that represents information about a culture such as language, calendar system, and so on. This parameter is not used in this method. It is reserved for future versions of this method. You can optionally pass in null for this parameter.</param>
    /// <param name="value">The object to convert.</param>
    public override object? ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object? value)
    {
        if (value is string)
        {
            var str = ((string)value).Trim();
            var empty = GtkColor.Empty;
            if (string.IsNullOrEmpty(str))
            {
                return empty;
            }
            if (str[0] == '#')
            {
                return base.ConvertFrom(context, culture, value);
            }
            if (string.Compare(str, "LightGrey", CultureInfo.InvariantCulture, CompareOptions.IgnoreCase) == 0)
            {
                return GtkColor.LightGray;
            }
            if (htmlSysColorTable == null)
            {
                InitializeHTMLSysColorTable();
            }
            var item = htmlSysColorTable![str];
            if (item != null)
            {
                return (GtkColor)item;
            }
        }
        return base.ConvertFrom(context, culture, value);
    }

    /// <summary>Converts the specified object to a specified type.</summary>
    /// <returns>The object resulting from conversion.</returns>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> instance that indicates the context of the object to convert.</param>
    /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object that represents information about a culture such as language, calendar system, and so on. This parameter is not used in this method. It is reserved for future versions of this method. You can optionally pass in null for this parameter.</param>
    /// <param name="value">The object to convert.</param>
    /// <param name="destinationType">The type to convert to.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="destinationType" /> is null.</exception>
    public override object? ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
        if (destinationType == null)
        {
            throw new ArgumentNullException("destinationType");
        }
        if (destinationType == typeof(string) && value != null)
        {
            var color = (GtkColor)value;
            if (color == GtkColor.Empty)
            {
                return string.Empty;
            }
            if (!color.IsKnownColor())
            {
                var stringBuilder = new StringBuilder("#", 7);
                var r = color.R;
                stringBuilder.Append(r.ToString("X2", CultureInfo.InvariantCulture));
                r = color.G;
                stringBuilder.Append(r.ToString("X2", CultureInfo.InvariantCulture));
                r = color.B;
                stringBuilder.Append(r.ToString("X2", CultureInfo.InvariantCulture));
                return stringBuilder.ToString();
            }
        }

        if (value != null)
        {
            return base.ConvertTo(context, culture, value, destinationType);
        }

        return null;
    }

    private static void InitializeHTMLSysColorTable()
    {
        var hashtables = new Hashtable(StringComparer.OrdinalIgnoreCase)
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
        htmlSysColorTable = hashtables;
    }
}
