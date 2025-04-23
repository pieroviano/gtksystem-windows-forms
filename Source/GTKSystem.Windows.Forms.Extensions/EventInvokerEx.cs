namespace System.Windows.Forms;

public class EventInvokerEx : IEventInvoker
{
    internal EventInvokerEx()
    {

    }

    public bool UseAsyncLoad { get; set; }

    public void EventInvoke(Action eventToInvoke, bool useAsyncLoad = false)
    {
        if (useAsyncLoad)
        {
            Task.Run(eventToInvoke);
        }
        else
        {
            eventToInvoke();
        }
    }

}