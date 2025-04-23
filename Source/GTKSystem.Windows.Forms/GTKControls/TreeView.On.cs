namespace System.Windows.Forms;

public partial class TreeView
{
    protected virtual void OnAfterExpand(TreeViewEventArgs e)
    {
        EventInvoke(() => AfterExpand?.Invoke(this, e));
    }

    protected virtual void OnAfterCollapse(TreeViewEventArgs e)
    {
        EventInvoke(() => AfterCollapse?.Invoke(this, e));
    }

    protected virtual void OnAfterSelect(TreeViewEventArgs e)
    {
        EventInvoke(() => AfterSelect?.Invoke(this, e));
    }

    protected virtual void OnBeforeSelect(TreeViewCancelEventArgs? e)
    {
        EventInvoke(() => BeforeSelect?.Invoke(this, e));
    }

}