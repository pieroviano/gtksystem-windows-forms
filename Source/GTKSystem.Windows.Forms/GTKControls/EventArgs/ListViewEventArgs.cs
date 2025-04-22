namespace System.Windows.Forms;

public class ListViewEventArgs: EventArgs
{
    public ListViewEventArgs(ListView listView)
    {
        ListView = listView;
    }

    public ListView ListView { get; }
}