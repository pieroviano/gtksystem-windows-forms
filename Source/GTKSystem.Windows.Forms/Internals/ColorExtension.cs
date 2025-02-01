using System.Drawing;

namespace Gtk.Windows.Forms.Internals
{
    internal static class ColorExtension
    {
        public static Color FromKnownColor(KnownColor color) =>
            FromName(color.ToString());

        public static Color FromName(string name)
        {
            // try to get a known color first
            if (ColorTable.TryGetNamedColor(name, out Color color))
                return color;

            // otherwise treat it as a named color
            return ColorExtension.FromName(name);
        }
    }
}
