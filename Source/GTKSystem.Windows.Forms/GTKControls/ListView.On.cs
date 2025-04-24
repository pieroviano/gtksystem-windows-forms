namespace System.Windows.Forms;

public partial class ListView
{

    protected virtual void OnColumnClick(ColumnClickEventArgs e)
    {
        ColumnClick?.Invoke(this, e);
    }

    protected internal virtual void OnItemCheck(ItemCheckEventArgs e)
    {
        ItemCheck?.Invoke(this, e);
    }

    protected virtual void OnColumnReordered(ColumnReorderedEventArgs e)
    {
        ColumnReordered?.Invoke(this, e);
    }



    protected internal virtual void OnSelectedIndexChanged(EventArgs e)
    {
        SelectedIndexChanged?.Invoke(this, e);
    }

    protected virtual void OnItemSelectionChanged(
        ListViewItemSelectionChangedEventArgs e)
    {
        ItemSelectionChanged?.Invoke(this, e);
    }

    protected internal virtual void OnItemActivate(EventArgs e)
    {
        ItemActivate?.Invoke(this, e);
    }

}