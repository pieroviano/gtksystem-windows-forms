using Gtk;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms;

public class FontDialog : CommonDialog
{
    public FontDialog()
    {
        Reset();
    }

    private void Init()
    {
        Reset();
    }

    [DefaultValue(true)]
    public virtual bool AllowFullOpen { get; set; }

    private Font? _font;
    public Font? Font { get => _font; set => _font = value; }

    [DefaultValue(false)]
    public virtual bool FullOpen { get; set; }

    [DefaultValue(false)]
    public virtual bool ShowHelp { get; set; }

    protected virtual IntPtr Instance { get; } = default;

    protected virtual int Options { get; } = default;

    public override void Reset()
    {
        _font = null;
    }

    internal static Window? ActiveWindow = null;
    protected override bool RunDialog(IWin32Window? owner)
    {
        FontChooserDialog? fontChooserDialog = null;
        if (owner is Form ownerform)
        {
            fontChooserDialog = new FontChooserDialog("选择字体", ownerform.self);
            fontChooserDialog.WindowPosition = WindowPosition.CenterOnParent;
        }
        else
        {
            fontChooserDialog = new FontChooserDialog("选择字体", null);
            fontChooserDialog.WindowPosition = WindowPosition.Center;
        }
        fontChooserDialog.IconName = "font-x-generic";
        fontChooserDialog.KeepAbove = true;
        if (null != _font)
            fontChooserDialog.Font = _font.Name + " " + (int)_font.Size;
        if (FullOpen && AllowFullOpen)
            fontChooserDialog.Fullscreen();
        var res = fontChooserDialog.Run();
        var fontStyle = FontStyle.Regular;
        switch (fontChooserDialog.FontDesc.Weight)
        {
            case Pango.Weight.Bold:
            case Pango.Weight.Ultrabold:
            case Pango.Weight.Semibold:
                fontStyle |= FontStyle.Bold;
                break;
        }
        switch (fontChooserDialog.FontDesc.Style)
        {
            case Pango.Style.Italic:
            case Pango.Style.Oblique:
                fontStyle |= FontStyle.Italic;
                break;
        }
        _font = new Font(fontChooserDialog.FontDesc.Family, (int)(fontChooserDialog.FontDesc.Size / Pango.Scale.PangoScale), fontStyle);

        fontChooserDialog.Dispose();
        fontChooserDialog.Destroy();
        return res == -5;
    }

    public override string ToString() => $"{_font?.Name} {_font?.Size}"; 
}