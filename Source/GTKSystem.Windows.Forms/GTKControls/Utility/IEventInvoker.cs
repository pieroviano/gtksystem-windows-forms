public interface IEventInvoker
{
    bool UseAsyncLoad { get; set; }
    void EventInvoke(Action eventToInvoke, bool useAsyncLoad = false);
}