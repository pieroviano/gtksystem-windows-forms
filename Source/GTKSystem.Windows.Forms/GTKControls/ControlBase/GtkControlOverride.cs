using System.Drawing;
using Cairo;
using Gdk;
using Gtk;
using Graphics = System.Drawing.Graphics;
using Image = System.Drawing.Image;

namespace System.Windows.Forms;

using Color = Drawing.Color;
using Rectangle = Drawing.Rectangle;

public partial class GtkControlOverride: IControlOverride, IGtkControlOverride
{
    private readonly IWidget? container;
    private Pixbuf? imagePixbuf;
    public GtkControlOverride(IWidget? container)
    {
        this.container = container;
    }
    public Color? BackColor { get; set; }
    private Image? backgroundImage;
    public Image? BackgroundImage { get => backgroundImage;
        set { backgroundImage = value; backgroundPixbuf = null; } }

    public ImageLayout BackgroundImageLayout { get; set; } = ImageLayout.Tile;
    public Image? Image { get; set; }
    public ContentAlignment ImageAlign { get; set; }

    private readonly List<string> cssList = [];
    public void AddClass(string cssClass)
    {
        cssList.Add(cssClass);
    }

    public void RemoveClass(string cssClass)
    {
        cssList.Remove(cssClass);
    }
    public void OnAddClass()
    {
        foreach (var cssClass in cssList)
        {
            if(container?.StyleContext.HasClass(cssClass)??false)
                container.StyleContext.RemoveClass(cssClass);
            container?.StyleContext.AddClass(cssClass);
        }
        ClearNativeBackground();
    }
    public void ClearNativeBackground()
    {
    }
    private Pixbuf? backgroundPixbuf;
    public void DrawnBackColor(Context cr, Gdk.Rectangle area)
    {
        if (BackColor.HasValue)
        {
            cr.Save();
            cr.SetSourceRGBA(BackColor.Value.R / 255f, BackColor.Value.G / 255f, BackColor.Value.B / 255f, BackColor.Value.A / 255f);
            cr.Paint();
            cr.Restore();
        }
    }
    public void OnDrawnBackground(Context? cr, Gdk.Rectangle area)
    {
        if (BackColor.HasValue && cr != null)
        {
            cr.Save();
            cr.SetSourceRGBA(BackColor.Value.R / 255f, BackColor.Value.G / 255f, BackColor.Value.B / 255f,
                BackColor.Value.A / 255f);
            cr.Paint();
            cr.Restore();
        }
        if (BackgroundImage is { PixbufData: not null })
        {
            if (backgroundPixbuf == null || backgroundPixbuf.Width != area.Width || backgroundPixbuf.Height != area.Height)
            {
                ImageUtility.ScaleImageByImageLayout(BackgroundImage.PixbufData, area.Width, area.Height, out backgroundPixbuf, BackgroundImageLayout);
            }
            ImageUtility.DrawImage(cr, backgroundPixbuf, area, ContentAlignment.TopLeft);
        }

        if (DrawnBackground != null)
        {
            var args = new DrawnArgs { Args = [cr] };
            DrawnBackground(container, args);
        }
    }
    
    public void OnDrawnImage(Context? cr, Gdk.Rectangle area)
    {
        if (Image is { PixbufData: not null })
        {
            if (imagePixbuf == null || imagePixbuf.Width != area.Width || imagePixbuf.Height != area.Height)
            {
                var imagepixbuf = new Pixbuf(Image.PixbufData);
                imagePixbuf = imagepixbuf.ScaleSimple(area.Width, area.Height, InterpType.Nearest);
            }
            ImageUtility.DrawImage(cr, imagePixbuf, area, ImageAlign);
        }
    }

    public void OnPaint(Context? cr, Gdk.Rectangle area)
    {
        var rectangle = new Rectangle(area.X, area.Y, area.Width, area.Height);
        OnPaintGraphics(new PaintGraphicsEventArgs(cr, rectangle));
        OnPaint(new PaintEventArgs(new Graphics(container, cr, area), new Rectangle(area.X, area.Y, area.Width, area.Height)));
    }

    void IGtkControlOverride.OnPaint(PaintEventArgs e)
    {
        OnPaint(e);
    }
}