using Gtk;

namespace System.Windows.Forms;

public partial class GtkControlOverride
{
    public event DrawnHandler? DrawnBackground;
    public event PaintEventHandler? Paint;
    public event EventHandler<PaintGraphicsEventArgs>? PaintGraphics;
}