namespace System.Windows.Forms;

public partial class ToolStrip
{
    public event EventHandler? CheckedChanged;
    public event EventHandler? CheckStateChanged;
    public event ToolStripItemClickedEventHandler? DropDownItemClicked;
}