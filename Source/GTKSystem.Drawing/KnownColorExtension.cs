namespace System.Drawing;

using GtkKnownColor = System.Drawing.KnownColor;

public static class KnownColorExtension
{
    public static bool IsKnownColor(this Color color) => KnownColorTable.ArgbToGtkKnownColor(unchecked((uint)color.ToArgb()))!=default;

    public static bool IsNamedColor(this Color color) => typeof(Color).GetProperties().Where(p=>p.PropertyType==typeof(Color)).FirstOrDefault(p=>((Color)p.GetValue(null)).ToArgb()==color.ToArgb())!=null || color.IsKnownColor();
    
    public static string GetName(this Color color) => (string)(typeof(Color).GetProperties().Where(p=>p.PropertyType==typeof(Color)).FirstOrDefault(p=>((Color)p.GetValue(null)).ToArgb()==color.ToArgb())?.GetValue(null)??string.Empty);

    public static bool IsSystemColor(this Color color) => IsKnownColor(color) && KnownColorTable.ArgbToGtkKnownColor(unchecked((uint)color.ToArgb())).IsKnownColorSystem();

    internal static bool IsKnownColorSystem(this GtkKnownColor knownColor)
        => KnownColorTable.ColorKindTable[(int)knownColor] == KnownColorTable.KnownColorKindSystem;

    public static Color FromKnownColor(this GtkKnownColor knownColor)
    {
        // try to get a known color first
        if (GtkColorTable.ArgbValues.TryGetValue(knownColor, out var color))
        {
            return Color.FromArgb(unchecked((int)color));
        }
        return Color.Transparent;
    }
}
