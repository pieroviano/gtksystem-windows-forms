namespace System.Windows.Forms;

public class UseAsyncLoadArgs: EventArgs
{
    public UseAsyncLoadArgs(bool useAsyncLoad)
    {
        UseAsyncLoad = useAsyncLoad;
    }

    public bool UseAsyncLoad { get; set; }
}