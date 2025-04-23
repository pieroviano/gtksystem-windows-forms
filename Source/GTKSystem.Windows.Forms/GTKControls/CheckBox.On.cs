namespace System.Windows.Forms;

public partial class CheckBox
{
    protected internal virtual void OnItemCheck(ItemCheckEventArgs e)
    {
        EventInvoke(() => ItemCheck?.Invoke(this, e));
    }

    protected virtual void OnCheckedChanged(EventArgs e)
    {
        EventInvoke(() => CheckedChanged?.Invoke(this, e));
    }

    protected virtual void OnCheckStateChanged(EventArgs e)
    {
        EventInvoke(() => CheckStateChanged?.Invoke(this, e));
    }

}