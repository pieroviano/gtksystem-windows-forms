namespace System.Windows.Forms;

public partial class DataGridView
{
    protected virtual void OnSelectionChanged(EventArgs e)
    {
        SelectionChanged?.Invoke(this, e);
    }

}