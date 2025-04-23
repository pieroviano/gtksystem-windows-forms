namespace System.Windows.Forms;

public class EventInvoker : IEventInvoker
{
    public bool UseAsyncInvoke { get; set; }

    public void EventInvoke(Action eventToInvoke, bool useAsyncInvoke)
    {
        eventToInvoke();
    }

}