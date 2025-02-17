using System.Drawing;
using System.Windows.Forms;
using Cairo;
using Gtk;
using GTKSystem.Windows.Forms.GTKControls.ControlBase;
using Color = System.Drawing.Color;
using Image = System.Drawing.Image;
using Rectangle = Gdk.Rectangle;

namespace GtkTests.System.Windows.Forms;

internal class MockGtkFormsControlOverride : IGtkControlOverride
{
    public event DrawnHandler? DrawnBackground;
    public event PaintEventHandler? Paint;
    public event PaintGraphicsEventHandler? PaintGraphics;
    public Color? BackColor { get; set; }
    public Image BackgroundImage { get; set; } = null!;
    public ImageLayout BackgroundImageLayout { get; set; }
    public Image Image { get; set; } = null!;
    public ContentAlignment ImageAlign { get; set; }

    public void AddClass(string cssClass)
    {
    }

    public void OnAddClass()
    {
    }

    public void OnDrawnBackground(Context cr, Rectangle area)
    {
    }

    public void OnPaint(Context cr, Rectangle area)
    {
    }

    public void RemoveClass(string cssClass)
    {
    }

    public void ClearNativeBackground()
    {
    }

    public void DrawnBackColor(Context cr, Rectangle area)
    {
    }

    public void OnDrawnImage(Context cr, Rectangle area)
    {
    }
}