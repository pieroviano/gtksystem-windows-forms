using Cairo;
using Gtk;

namespace System.Windows.Forms;

using Color = System.Drawing.Color;
using Image = System.Drawing.Image;

public interface IGtkControlOverride
{
    event DrawnHandler? DrawnBackground;
    event PaintEventHandler? Paint;
    event EventHandler<PaintGraphicsEventArgs> PaintGraphics;
    Color? BackColor { get; set; }
    Image? BackgroundImage { get; set; }
    ImageLayout BackgroundImageLayout { get; set; }
    Image? Image { get; set; }
    Drawing.ContentAlignment ImageAlign { get; set; }
    void AddClass(string cssClass);
    void OnAddClass();
    void OnDrawnBackground(Context? cr, Gdk.Rectangle area);
    void OnPaint(Context? cr, Gdk.Rectangle area);
    void OnPaint(PaintEventArgs e);
    void RemoveClass(string cssClass);
    void ClearNativeBackground();
    void DrawnBackColor(Context cr, Gdk.Rectangle area);
    void OnDrawnImage(Context? cr, Gdk.Rectangle area);
}