namespace System.Windows.Forms;

public class UseAsyncInvokeArgs: EventArgs
{
    public UseAsyncInvokeArgs(bool useAsyncInvoke)
    {
        UseAsyncInvoke = useAsyncInvoke;
    }

    public bool UseAsyncInvoke { get; set; }
}