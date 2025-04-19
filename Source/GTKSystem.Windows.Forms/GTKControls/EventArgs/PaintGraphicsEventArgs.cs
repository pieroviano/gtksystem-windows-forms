using Cairo;
using Rectangle = System.Drawing.Rectangle;

namespace System.Windows.Forms;

public class PaintGraphicsEventArgs : EventArgs
{
    public PaintGraphicsEventArgs(Context? context, Drawing.Rectangle rectangle)
    {
        Context = context;
        Rectangle = rectangle;
    }

    public Context? Context
    {
        get;
    }
    public Rectangle Rectangle
    {
        get;
    }
}