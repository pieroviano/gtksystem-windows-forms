using GtkApplication = System.Windows.Forms.Application;

namespace System.Windows.Forms;

public partial class CommonDialog
{
    protected virtual void OnOKClicked(EventArgs e)
    {
        GtkApplication.EventInvoke(() => OKClicked?.Invoke(this, e), GtkApplication.UseAsyncLoad);
    }

    protected virtual void OnCancelClicked(EventArgs e)
    {
        GtkApplication.EventInvoke(() => CancelClicked?.Invoke(this, e), GtkApplication.UseAsyncLoad);
    }

    protected virtual void OnHelpRequest(EventArgs e)
    {
        var handler = (EventHandler?)Events[helpRequestEvent];
        GtkApplication.EventInvoke(() => handler?.Invoke(this, e));
    }

    protected virtual void OnDialogOnShown(CommonDialogEventArgs e)
    {
        GtkApplication.EventInvoke(() => DialogOnShown?.Invoke(this, e), GtkApplication.UseAsyncLoad);
    }
}