namespace System.Windows.Forms;

internal partial class NonStaticMessageBox
{
    private IEventInvoker? _eventInvoker;

    public NonStaticMessageBox()
    {
        UseAsyncLoad = true;
    }

    internal IEventInvoker EventInvoker
    {
        get
        {
            if (_eventInvoker == null)
            {
                _eventInvoker = Control.CreateEventInvoker();
            }
            return _eventInvoker;
        }
    }

    public bool UseAsyncLoad
    {
        get => EventInvoker.UseAsyncLoad;
        set => EventInvoker.UseAsyncLoad = value;
    }

    public void EventInvoke(Action eventToInvoke, bool useAsyncLoad = false)
    {
        EventInvoker.EventInvoke(eventToInvoke, useAsyncLoad);
    }

    protected virtual void OnDialogAvailable(DialogEventArgs e)
    {
        EventInvoke(() =>
        {
            DialogAvailable?.Invoke(this, e);
        }, UseAsyncLoad);
    }
}

