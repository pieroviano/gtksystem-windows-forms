namespace System.Windows.Forms;

public partial class ScrollableControl
{
    protected virtual void OnScroll(ScrollEventArgs e)
    {
        ((Action)(() =>
        {
            scrollbase?.OnScroll(e);
        }))();
    }
}

