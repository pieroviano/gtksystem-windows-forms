namespace System.Windows.Forms;

public partial class TreeView
{
    protected virtual void OnAfterExpand(TreeViewEventArgs e)
    {
        AfterExpand?.Invoke(this, e);
    }

    protected virtual void OnAfterCollapse(TreeViewEventArgs e)
    {
        AfterCollapse?.Invoke(this, e);
    }

    protected virtual void OnAfterSelect(TreeViewEventArgs e)
    {
        AfterSelect?.Invoke(this, e);
    }

    protected virtual void OnBeforeSelect(TreeViewCancelEventArgs? e)
    {
        BeforeSelect?.Invoke(this, e);
    }

}