namespace System.Windows.Forms;

public class EventInvoker : IEventInvoker
{
    public bool UseAsyncLoad { get; set; }

    public void EventInvoke(Action eventToInvoke, bool useAsyncLoad = false)
    {
        eventToInvoke();
    }

}