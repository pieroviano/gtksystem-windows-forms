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
                var environmentVariable = Environment.GetEnvironmentVariable("UseAsyncLoad");
                var useAsyncLoad = string.IsNullOrEmpty(environmentVariable) ? "false" : environmentVariable;
                bool.TryParse(useAsyncLoad, out var useResult);
                _eventInvoker.UseAsyncLoad = useResult;
            }
            return _eventInvoker;
        }
    }

    public override bool UseAsyncLoad { get => EventInvoker.UseAsyncLoad; set => EventInvoker.UseAsyncLoad = value; }

    public override void EventInvoke(Action eventToInvoke, bool useAsyncLoad = false)
    {
        EventInvoker.EventInvoke(eventToInvoke, useAsyncLoad);
    }

}