using GtkApplication = System.Windows.Forms.Application;

namespace System.Windows.Forms;

public partial class ToolStripComboBox
{
    public event EventHandler? SelectedIndexChanged;
    public event EventHandler? SelectedValueChanged;
}