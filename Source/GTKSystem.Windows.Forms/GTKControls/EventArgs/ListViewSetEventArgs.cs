namespace System.Windows.Forms;

public class ListViewSetEventArgs: System.EventArgs
{
    public ListViewSetEventArgs(ListView? listView)
    {
        ListView = listView;
    }

    public ListView? ListView { get; }
}