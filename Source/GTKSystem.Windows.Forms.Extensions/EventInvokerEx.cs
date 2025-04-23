namespace System.Windows.Forms;

public class EventInvokerEx : IEventInvoker
{
    internal EventInvokerEx()
    {

    }

    public bool UseAsyncInvoke { get; set; }

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

}