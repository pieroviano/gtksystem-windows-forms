namespace System.Windows.Forms;

internal partial class NonStaticMessageBox
{
    public bool UseAsyncInvoke { get; set; } = true;

    public void EventInvoke(Action eventToInvoke, bool useAsyncInvoke)
    {
        if (useAsyncInvoke)
        {
            Task.Run(eventToInvoke);
        }
        else
        {
            eventToInvoke();
        }
    }

    protected virtual void OnDialogAvailable(DialogEventArgs e)
    {
        EventInvoke(() =>
        {
            DialogAvailable?.Invoke(this, e);
        }, UseAsyncInvoke);
    }
}

