namespace System.Drawing;

public static class ColorNames
{
    public static Color FromName(string name)
    {
        // try to get a known color first
        if (Enum.TryParse(name, true, out KnownColor knownColor) && GtkColorTable.ArgbValues.TryGetValue(knownColor, out var color))
        {
            return Color.FromArgb(unchecked((int)color));
        }
        return Color.Transparent;
    }
}