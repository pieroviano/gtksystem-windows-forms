public interface IEventInvoker
{
    bool UseAsyncInvoke { get; set; }
    void EventInvoke(Action eventToInvoke, bool useAsyncInvoke);
}