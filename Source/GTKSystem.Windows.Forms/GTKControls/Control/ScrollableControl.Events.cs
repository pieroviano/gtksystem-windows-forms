namespace System.Windows.Forms;

public partial class ScrollableControl
{
    public event ScrollEventHandler? Scroll
    {
        add => AddScrollHandler(value);
        remove => RemoveScrollHandler(value);
    }

    protected virtual void RemoveScrollHandler(ScrollEventHandler? value)
    {
        if (scrollbase != null) { scrollbase.Scroll -= value; }
    }

    protected virtual void AddScrollHandler(ScrollEventHandler? value)
    {
        if (scrollbase != null) { scrollbase.Scroll += value; }
    }
}