using GtkApplication = System.Windows.Forms.Application;

namespace System.Windows.Forms;

public partial class ColumnHeader
{
    protected virtual void OnDisplayIndexChanged(EventArgs e)
    {
        GtkApplication.EventInvoke(() => DisplayIndexChanged?.Invoke(this, e), false);
    }

}