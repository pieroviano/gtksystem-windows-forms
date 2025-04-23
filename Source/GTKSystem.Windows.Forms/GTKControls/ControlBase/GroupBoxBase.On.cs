using GtkApplication = System.Windows.Forms.Application;

namespace System.Windows.Forms;

public partial class GroupBoxBase
{
    protected virtual void OnScroll(ScrollEventArgs e)
    {
        GtkApplication.EventInvoke(() => Scroll?.Invoke(this, e));
    }
}