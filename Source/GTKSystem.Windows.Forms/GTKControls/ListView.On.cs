namespace System.Windows.Forms;

public partial class ListView
{

    protected virtual void OnColumnClick(ColumnClickEventArgs e)
    {
        EventInvoke(() => ColumnClick?.Invoke(this, e));
    }

    protected internal virtual void OnItemCheck(ItemCheckEventArgs e)
    {
        EventInvoke(() => ItemCheck?.Invoke(this, e));
    }

    protected virtual void OnColumnReordered(ColumnReorderedEventArgs e)
    {
        EventInvoke(() => ColumnReordered?.Invoke(this, e));
    }



    protected internal virtual void OnSelectedIndexChanged(EventArgs e)
    {
        EventInvoke(() => SelectedIndexChanged?.Invoke(this, e));
    }

    protected virtual void OnItemSelectionChanged(
        ListViewItemSelectionChangedEventArgs e)
    {
        EventInvoke(() => ItemSelectionChanged?.Invoke(this, e));
    }

    protected internal virtual void OnItemActivate(EventArgs e)
    {
        EventInvoke(() => ItemActivate?.Invoke(this, e));
    }

}