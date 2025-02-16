using Cairo;
using Gtk;
using GTKSystem.Windows.Forms.GTKControls.ControlBase;
using Color = System.Drawing.Color;
using Image = System.Drawing.Image;

namespace System.Windows.Forms;

public interface IGtkControlOverride
{
    event DrawnHandler? DrawnBackground;
    event PaintEventHandler? Paint;
    event PaintGraphicsEventHandler? PaintGraphics;
    Color? BackColor { get; set; }
    Image BackgroundImage { get; set; }
    ImageLayout BackgroundImageLayout { get; set; }
    System.Drawing.Image Image { get; set; }
    System.Drawing.ContentAlignment ImageAlign { get; set; }
    void AddClass(string cssClass);
    void OnAddClass();
    void OnDrawnBackground(Context cr, Gdk.Rectangle area);
    void OnPaint(Context cr, Gdk.Rectangle area);
    void RemoveClass(string cssClass);
    void ClearNativeBackground();
    void DrawnBackColor(Cairo.Context cr, Gdk.Rectangle area);
    void OnDrawnImage(Cairo.Context cr, Gdk.Rectangle area);
}