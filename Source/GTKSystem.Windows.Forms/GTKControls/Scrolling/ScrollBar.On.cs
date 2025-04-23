namespace System.Windows.Forms;

public abstract partial class ScrollBar
{
    protected virtual void OnScroll(ScrollEventArgs e)
    {
        EventInvoke(() => Scroll?.Invoke(this, e));
    }

    protected virtual void OnValueChanged(EventArgs e)
    {
        EventInvoke(() => ValueChanged?.Invoke(this, e));
    }

}