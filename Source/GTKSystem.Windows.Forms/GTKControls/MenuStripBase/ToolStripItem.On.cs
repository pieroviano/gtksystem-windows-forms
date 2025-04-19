using GtkApplication = System.Windows.Forms.Application;

namespace System.Windows.Forms;

public partial class ToolStripItem
{
    protected virtual void OnClick(EventArgs e)
    {
        GtkApplication.EventInvoke(() => Click?.Invoke(this, e), false);
    }

    protected virtual void OnCheckedChanged(EventArgs e)
    {
        GtkApplication.EventInvoke(() => CheckedChanged?.Invoke(this, e), false);
    }

    protected virtual void OnCheckStateChanged(EventArgs e)
    {
        GtkApplication.EventInvoke(() => CheckStateChanged?.Invoke(this, e), false);
    }

    protected virtual void OnDropDownItemClicked(ToolStripItemClickedEventArgs e)
    {
        GtkApplication.EventInvoke(() => DropDownItemClicked?.Invoke(this, e), false);
    }
}