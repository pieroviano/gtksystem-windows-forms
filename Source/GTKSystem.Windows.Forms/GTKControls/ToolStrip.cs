/*
 * A cross-platform interface component developed based on GTK components and compatible with the native C# control winform interface.
 * Use this component GTKSystem.Windows.Forms instead of Microsoft.WindowsDesktop.App.WindowsForms, compile once, run across platforms windows, linux, macos
 * Technical support 438865652@qq.com, https://www.gtkapp.com, https://gitee.com/easywebfactory, https://github.com/easywebfactory
 * author: chenhongjin
 */

using Gtk;

namespace System.Windows.Forms;

using Size = Drawing.Size;

public partial class ToolStrip : Control
{
    public readonly ToolStripBase self;
    public override object GtkControl => self;
    public ToolStripItemCollection toolStripItemCollection;
    public ToolStrip()
    {
        self = new ToolStripBase();
        toolStripItemCollection = new ToolStripItemCollection(this);
        self.ActivateCurrent += ToolStripItem_Activated;
        //Dock = DockStyle.Top;
    }
    public ToolStrip(string owner)
    {
        self = new ToolStripBase();
        self.Hexpand = false;
        self.Vexpand = false;
        self.Valign = Align.Start;
        self.Halign = Align.Start;
        toolStripItemCollection = new ToolStripItemCollection(this, owner);
        self.ActivateCurrent += ToolStripItem_Activated;
        //Dock = DockStyle.Top;
    }
    private void ToolStripItem_Activated(object? sender, ActivateCurrentArgs e)
    {
        OnDropDownItemClicked(new ToolStripItemClickedEventArgs(new ToolStripItem()));
        OnClick(e);
        OnCheckedChanged(e);
        OnCheckStateChanged(e);
    }

    public override Size Size { get => base.Size; set => base.Size = new Size(value.Width, 30); }
    public ToolStripItemCollection Items => toolStripItemCollection;

    public Size ImageScalingSize { get; set; }
    public ToolStripLayoutStyle LayoutStyle { get; set; }
}