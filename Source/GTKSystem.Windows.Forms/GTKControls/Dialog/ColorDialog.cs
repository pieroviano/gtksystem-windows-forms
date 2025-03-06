/*
 * A cross-platform interface component developed based on GTK components and compatible with the native C# control winform interface.
 * Use this component GTKSystem.Windows.Forms instead of Microsoft.WindowsDesktop.App.WindowsForms, compile once, run across platforms windows, linux, macos
 * Technical support 438865652@qq.com, https://www.gtkapp.com, https://gitee.com/easywebfactory, https://github.com/easywebfactory
 * author:chenhongjin
 */

using System.ComponentModel;
using System.Drawing;
using Gtk;

namespace System.Windows.Forms;

public class ColorDialog : CommonDialog
{
    private int[]? customColors;

    public virtual bool AllowFullOpen { get; set; } = true;

    [DefaultValue(false)]
    public virtual bool AnyColor { get; set; }

    public Color Color { get; set; } = Color.Black;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public int[] CustomColors
    {
        get => customColors;
        set
        {
            if (value == null)
            {
                value = [];
            }
            customColors = value;
        }
    }

    [DefaultValue(false)]
    public virtual bool FullOpen { get; set; }

    [DefaultValue(false)]
    public virtual bool ShowHelp { get; set; }

    [DefaultValue(false)]
    public virtual bool SolidColorOnly { get; set; }

    protected virtual IntPtr Instance { get; } = default;

    protected virtual int Options { get; } = default;


    public override void Reset()
    {
        Color = Color.Black;
    }
    protected override bool RunDialog(IWin32Window owner)
    {
        ColorChooserDialog? colorChooserDialog = null;
        if (owner is Form ownerform)
        {
            colorChooserDialog = new ColorChooserDialog(
                Properties.Resources.ColorDialog_RunDialog_Choose_color, ownerform.self);
            colorChooserDialog.WindowPosition = WindowPosition.CenterOnParent;
        }
        else
        {
            colorChooserDialog =
                new ColorChooserDialog(
                    Properties.Resources.ColorDialog_RunDialog_Choose_color, null);
            colorChooserDialog.WindowPosition = WindowPosition.Center;
        }
        colorChooserDialog.IconName = "image-x-generic";
        colorChooserDialog.KeepAbove = true;
        if (Color.Name != "0")
            colorChooserDialog.Rgba = new Gdk.RGBA() { Alpha = (double)Color.A / 255, Red = (double)Color.R / 255, Green = (double)Color.G / 255, Blue = (double)Color.B / 255 };
        if (FullOpen && AllowFullOpen)
            colorChooserDialog.Fullscreen();
        var res = colorChooserDialog.Run();
        var colorSelection = colorChooserDialog.Rgba;
        Color = Color.FromArgb((int)(colorSelection.Alpha * 255), (int)Math.Round(colorSelection.Red * 255, 0), (int)Math.Round(colorSelection.Green * 255, 0), (int)Math.Round(colorSelection.Blue * 255, 0));
        colorChooserDialog.Dispose();
        colorChooserDialog.Destroy();
        return res == -5;
    }

    public override string ToString() { return Color.Name; }
}