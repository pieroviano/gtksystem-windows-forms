using System.Drawing;
using System.ComponentModel;

namespace System.Windows.Forms;

public abstract partial class ToolStripDropDownItem : WidgetToolStrip<Gtk.MenuItem>
{
    public event EventHandler? DropDownOpening;

    public event EventHandler? DropDownClosed;

    public event EventHandler? DropDownOpened;
}