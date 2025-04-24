namespace System.Windows.Forms;

public partial class Form
{
    protected void OnFormClosed(FormClosedEventArgs e)
    {
        FormClosed?.Invoke(this, e);
        IsClosed = true;
    }

    protected internal virtual void OnLoadComplete(EventArgs e)
    {
        LoadComplete?.Invoke(this, e);
    }
    protected override void OnDisposed(EventArgs e)
    {
        base.OnDisposed(e);
        IsClosed = true;
    }

    protected virtual void OnFormClosing(FormClosingEventArgs e)
    {
        FormClosing?.Invoke(this, e);
    }

    protected internal virtual void OnShown(EventArgs e)
    {
        Action eventToInvoke = () =>
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
        };
        eventToInvoke();
    }
}