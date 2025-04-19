namespace System.Windows.Forms;

public partial class FormBase
{
    protected virtual void OnCloseWindowEvent(CloseWindowArgs e)
    {
        System.Windows.Forms.Application.EventInvoke(() => CloseWindowEvent?.Invoke(this, e), false);
    }

    protected virtual void OnScroll(ScrollEventArgs e)
    {
        System.Windows.Forms.Application.EventInvoke(() => Scroll?.Invoke(this, e), false);
    }

}