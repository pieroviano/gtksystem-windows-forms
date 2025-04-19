namespace System.Windows.Forms;

public static class ColumnHeaderExtension
{

    public static void PerformClick(this ColumnHeader control)
    {
        control._listView?.ColumnButton_Clicked(control.Button, EventArgs.Empty);
    }

    public static void PerformItemCheck(this ColumnHeader control, int index, CheckState newCheckValue, CheckState currentValue)
    {
        control._listView?.OnItemCheck(new ItemCheckEventArgs(index, newCheckValue, currentValue));
    }
}