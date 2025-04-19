namespace System.Windows.Forms;

public partial class TreeView
{
    public event TreeViewCancelEventHandler? BeforeSelect;
    public event TreeViewEventHandler? AfterSelect;
    public event TreeViewEventHandler? AfterCollapse;
    public event TreeViewEventHandler? AfterExpand;
}