namespace System.Windows.Forms;

public partial class Form
{
    protected void OnFormClosed(FormClosedEventArgs e)
    {
        EventInvoke(() => FormClosed?.Invoke(this, e), false);
    }

    protected virtual void OnFormClosing(FormClosingEventArgs e)
    {
        EventInvoke(() => FormClosing?.Invoke(this, e), false);
    }

    protected internal virtual void OnShown(EventArgs e)
    {
        EventInvoke(() =>
        {
            Shown?.Invoke(this, e);
            if (!bindingContextSet)
            {
                OnBindingContextChanged(e);
            }

            foreach (Control control in Controls)
            {
                control.OnLoad(e);
            }
        }, UseAsyncInvoke);
    }
}