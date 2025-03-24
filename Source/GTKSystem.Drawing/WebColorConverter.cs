using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Text;
namespace System.Drawing;

#if NETSTANDARD
using WebColor = Gtk.Color;
#else
using WebColor = Color;
#endif


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
            string str = ((string)value).Trim();
            Color empty = Color.Empty;
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
                return Color.LightGray;
            }
            if (htmlSysColorTable == null)
            {
                InitializeHTMLSysColorTable();
            }
            object item = htmlSysColorTable![str];
            if (item != null)
            {
                return (Color)item;
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
            var color = (WebColor)value;
            if (color == WebColor.Empty)
            {
                return string.Empty;
            }
            if (!color.IsKnownColor)
            {
                StringBuilder stringBuilder = new StringBuilder("#", 7);
                byte r = color.R;
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
        Hashtable hashtables = new Hashtable(StringComparer.OrdinalIgnoreCase)
        {
            ["activeborder"] = WebColor.FromKnownColor(KnownColor.ActiveBorder),
            ["activecaption"] = WebColor.FromKnownColor(KnownColor.ActiveCaption),
            ["appworkspace"] = WebColor.FromKnownColor(KnownColor.AppWorkspace),
            ["background"] = WebColor.FromKnownColor(KnownColor.Desktop),
            ["buttonface"] = WebColor.FromKnownColor(KnownColor.Control),
            ["buttonhighlight"] = WebColor.FromKnownColor(KnownColor.ControlLightLight),
            ["buttonshadow"] = WebColor.FromKnownColor(KnownColor.ControlDark),
            ["buttontext"] = WebColor.FromKnownColor(KnownColor.ControlText),
            ["captiontext"] = WebColor.FromKnownColor(KnownColor.ActiveCaptionText),
            ["graytext"] = WebColor.FromKnownColor(KnownColor.GrayText),
            ["highlight"] = WebColor.FromKnownColor(KnownColor.Highlight),
            ["highlighttext"] = WebColor.FromKnownColor(KnownColor.HighlightText),
            ["inactiveborder"] = WebColor.FromKnownColor(KnownColor.InactiveBorder),
            ["inactivecaption"] = WebColor.FromKnownColor(KnownColor.InactiveCaption),
            ["inactivecaptiontext"] = WebColor.FromKnownColor(KnownColor.InactiveCaptionText),
            ["infobackground"] = WebColor.FromKnownColor(KnownColor.Info),
            ["infotext"] = WebColor.FromKnownColor(KnownColor.InfoText),
            ["menu"] = WebColor.FromKnownColor(KnownColor.Menu),
            ["menutext"] = WebColor.FromKnownColor(KnownColor.MenuText),
            ["scrollbar"] = WebColor.FromKnownColor(KnownColor.ScrollBar),
            ["threeddarkshadow"] = WebColor.FromKnownColor(KnownColor.ControlDarkDark),
            ["threedface"] = WebColor.FromKnownColor(KnownColor.Control),
            ["threedhighlight"] = WebColor.FromKnownColor(KnownColor.ControlLight),
            ["threedlightshadow"] = WebColor.FromKnownColor(KnownColor.ControlLightLight),
            ["window"] = WebColor.FromKnownColor(KnownColor.Window),
            ["windowframe"] = WebColor.FromKnownColor(KnownColor.WindowFrame),
            ["windowtext"] = WebColor.FromKnownColor(KnownColor.WindowText)
        };
        htmlSysColorTable = hashtables;
    }
}
