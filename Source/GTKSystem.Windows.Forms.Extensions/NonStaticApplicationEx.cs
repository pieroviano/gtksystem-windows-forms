namespace System.Windows.Forms;

public class NonStaticApplicationEx: NonStaticApplication
{
    public new static void Init()
    {
        Control.CreateEventInvoker = () => new EventInvokerEx();
        Instance = new NonStaticApplicationEx();
    }

    private IEventInvoker? _eventInvoker;

    internal virtual IEventInvoker EventInvoker
    {
        get
        {
            if (_eventInvoker == null)
            {
                _eventInvoker = new EventInvokerEx();
                var useAsyncInvoke = string.IsNullOrEmpty(Environment.GetEnvironmentVariable("UseAsyncInvoke")) ? "false" : Environment.GetEnvironmentVariable("UseAsyncInvoke");
                bool.TryParse(useAsyncInvoke, out var useResult);
                _eventInvoker.UseAsyncInvoke = useResult;
            }
            return _eventInvoker;
        }
    }

    public override bool UseAsyncInvoke { get => EventInvoker.UseAsyncInvoke; set => EventInvoker.UseAsyncInvoke = value; }

    public override void EventInvoke(Action eventToInvoke, bool useAsyncInvoke)
    {
        EventInvoker.EventInvoke(eventToInvoke, useAsyncInvoke);
    }

}