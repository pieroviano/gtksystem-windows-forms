using GtkApplication = System.Windows.Forms.Application;

namespace System.Windows.Forms;

public partial class ToolStripComboBox
{
    protected virtual void OnSelectedValueChanged(EventArgs e)
    {
        GtkApplication.EventInvoke(() => SelectedValueChanged?.Invoke(this, e), false);
    }

    protected virtual void OnSelectedIndexChanged(EventArgs e)
    {
        GtkApplication.EventInvoke(() => SelectedIndexChanged?.Invoke(this, e), false);
    }

}