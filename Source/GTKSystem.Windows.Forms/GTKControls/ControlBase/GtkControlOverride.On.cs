using Gtk;

using GtkApplication = System.Windows.Forms.Application;

namespace System.Windows.Forms;

public partial class GtkControlOverride
{
    protected virtual void OnDrawnBackground(DrawnArgs e)
    {
        GtkApplication.EventInvoke(() => DrawnBackground?.Invoke(this, e));
    }

    protected virtual void OnPaint(PaintEventArgs e)
    {
        GtkApplication.EventInvoke(() => Paint?.Invoke(container, e));
    }

    protected virtual void OnPaintGraphics(PaintGraphicsEventArgs e)
    {
        PaintGraphics?.Invoke(this, e);
    }

}