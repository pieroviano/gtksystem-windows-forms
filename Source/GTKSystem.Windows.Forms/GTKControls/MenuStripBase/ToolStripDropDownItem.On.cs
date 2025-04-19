using System.Drawing;
using System.ComponentModel;

namespace System.Windows.Forms;

public abstract partial class ToolStripDropDownItem : WidgetToolStrip<Gtk.MenuItem>
    {
    protected virtual void OnDropDownOpening(EventArgs e)
    {
        DropDownOpening?.Invoke(this, e);
    }

    protected virtual void OnDropDownClosed(EventArgs e)
    {
        DropDownClosed?.Invoke(this, e);
    }

    protected virtual void OnDropDownOpened(EventArgs e)
    {
        DropDownOpened?.Invoke(this, e);
    }
}

