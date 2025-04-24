using GtkApplication = System.Windows.Forms.Application;

namespace System.Windows.Forms;

public partial class TabControl
{
    protected virtual void OnSelectedIndexChanged(EventArgs e)
    {
            SelectedIndexChanged?.Invoke(this, e);
    }

    public new partial class ControlCollection
    {
        protected virtual void OnDrawItem(DrawItemEventArgs e)
        {
            GtkApplication.EventInvoke(() => _owner.DrawItem?.Invoke(this, e));
        }

    }
}