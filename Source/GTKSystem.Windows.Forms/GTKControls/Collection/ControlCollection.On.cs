using GtkApplication = System.Windows.Forms.Application;

namespace System.Windows.Forms;

internal partial class ArrangedElementWidget
{

    protected virtual void OnDisposed(EventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            Disposed?.Invoke(this, e);
        }, GtkApplication.UseAsyncLoad);
    }
}
