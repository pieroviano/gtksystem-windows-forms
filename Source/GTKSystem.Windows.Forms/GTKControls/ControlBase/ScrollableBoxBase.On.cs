using GtkApplication = System.Windows.Forms.Application;

namespace System.Windows.Forms;

public abstract partial class ScrollableBoxBase
{
    protected virtual void OnScroll(ScrollEventArgs e)
    {
        GtkApplication.EventInvoke(() => Scroll?.Invoke(this, e), false);
    }

}