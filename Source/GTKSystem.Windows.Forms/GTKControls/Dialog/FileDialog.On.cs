using System.ComponentModel;

using GtkApplication = System.Windows.Forms.Application;

namespace System.Windows.Forms;

public partial class FileDialog
{
    protected virtual void OnFileOk(CancelEventArgs e)
    {
        GtkApplication.EventInvoke(() => FileOK?.Invoke(this, e), GtkApplication.UseAsyncInvoke);
    }

}