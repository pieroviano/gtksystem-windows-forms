namespace System.Windows.Forms;

public static class ListViewExtension
{
    public static void PerformSelectedIndexChanged(this ListView control)
    {
        control.OnSelectedIndexChanged(EventArgs.Empty);
    }

    public static void PerformItemActivate(this ListView control)
    {
        control.OnItemActivate(EventArgs.Empty);
    }
}