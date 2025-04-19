using GtkApplication = System.Windows.Forms.Application;

namespace System.Windows.Forms;

public partial class ToolStripItem
{
    public event EventHandler? Click;
    public event EventHandler? CheckedChanged;
    public event EventHandler? CheckStateChanged;
    public event ToolStripItemClickedEventHandler? DropDownItemClicked;
}