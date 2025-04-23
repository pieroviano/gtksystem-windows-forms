namespace System.Windows.Forms;

public partial class Form
{
    protected void OnFormClosed(FormClosedEventArgs e)
    {
        EventInvoke(() => FormClosed?.Invoke(this, e));
    }

    protected virtual void OnFormClosing(FormClosingEventArgs e)
    {
        EventInvoke(() => FormClosing?.Invoke(this, e));
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
        }, UseAsyncLoad);
    }
}