namespace System.Windows.Forms.Extensions;

public static class ToArrayExtension
{
    public static T[] ToArray<T>(this IEnumerable<T> value, Func<T, bool> predicate)
    {
        return value.Where(predicate).ToArray();
    }
}