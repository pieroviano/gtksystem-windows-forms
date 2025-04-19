using GtkApplication = System.Windows.Forms.Application;

namespace System.Windows.Forms;

public partial class DataGridViewColumn
{
    protected virtual void OnDisposed(EventArgs e)
    {
        GtkApplication.EventInvoke(() => Disposed?.Invoke(this, e), GtkApplication.UseAsyncInvoke);
    }
}