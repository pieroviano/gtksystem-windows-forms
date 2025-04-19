namespace System.Windows.Forms;

public partial class DataGridView
{
    protected virtual void OnSelectionChanged(EventArgs e)
    {
        EventInvoke(() => SelectionChanged?.Invoke(this, e), false);
    }

}