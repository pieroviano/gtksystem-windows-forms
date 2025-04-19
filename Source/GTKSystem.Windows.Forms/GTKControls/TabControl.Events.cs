using GtkApplication = System.Windows.Forms.Application;

namespace System.Windows.Forms;

public partial class TabControl
{
    public event EventHandler? SelectedIndexChanged;

    public event DrawItemEventHandler? DrawItem;
}