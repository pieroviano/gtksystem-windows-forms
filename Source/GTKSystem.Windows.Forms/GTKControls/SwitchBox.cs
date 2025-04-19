/*
 * A cross-platform interface component developed based on GTK components and compatible with the native C# control winform interface.
 * Use this component GTKSystem.Windows.Forms instead of Microsoft.WindowsDesktop.App.WindowsForms, compile once, run across platforms windows, linux, macos
 * Technical support 438865652@qq.com, https://www.gtkapp.com, https://gitee.com/easywebfactory, https://github.com/easywebfactory
 * author: chenhongjin
 */

using System.ComponentModel;

namespace System.Windows.Forms;

[DesignerCategory("Component")]
public partial class SwitchBox : Control
{
public readonly SwitchBoxBase self;
    public override object GtkControl => self;
    public SwitchBox()
    {
        self = new SwitchBoxBase();
        self.ButtonReleaseEvent += Self_ButtonReleaseEvent;
    }
    private void Self_ButtonReleaseEvent(object o, Gtk.ButtonReleaseEventArgs args)
    {
        if (CheckedChanged != null && self.IsVisible)
            CheckedChanged(this, EventArgs.Empty);
    }
    public override string Text
    {
        get => self.TooltipText;
        set
        {
            self.TooltipText = value;
            base.Text = value ?? string.Empty;
        }
    }

    public  bool Checked { get => self.Active;
        set { 
            self.Active = value;
            if (CheckedChanged != null && self.IsVisible)
                OnCheckedChanged();
        } 
    }
}