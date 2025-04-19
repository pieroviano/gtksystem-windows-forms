namespace System.Windows.Forms;

public partial class ScrollableControl
{
    protected virtual void OnScroll(ScrollEventArgs e)
    {
        EventInvoke(() =>
        {
            scrollbase?.OnScroll(e);
        }, false);
    }
}

