namespace System.Windows.Forms;

public partial class ListView
{

    public event ColumnClickEventHandler? ColumnClick;
    public event ColumnReorderedEventHandler? ColumnReordered;
    public event ItemCheckEventHandler? ItemCheck;
    public event ItemCheckedEventHandler? ItemChecked;
    public event ListViewItemSelectionChangedEventHandler? ItemSelectionChanged;
    public event EventHandler? SelectedIndexChanged;
    public event EventHandler? ItemActivate;
}