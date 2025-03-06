/*
 * A cross-platform interface component developed based on GTK components and compatible with the native C# control winform interface.
 * Use this component GTKSystem.Windows.Forms instead of Microsoft.WindowsDesktop.App.WindowsForms, compile once, run across platforms windows, linux, macos
 * Technical support 438865652@qq.com, https://www.gtkapp.com, https://gitee.com/easywebfactory, https://github.com/easywebfactory
 * author:chenhongjin
 */

using System.ComponentModel;

namespace System.Windows.Forms;

[DesignerCategory("Component")]
public class RadioButton : Control
{
    public readonly RadioButtonBase self = new();
    public override object GtkControl => self;
    public RadioButton()
    {
        self.Realized += Control_Realized;
    }

    private void Self_Toggled(object? sender, EventArgs e)
    {
        if (CheckedChanged != null && self.IsVisible)
            CheckedChanged?.Invoke(this, e);
    }

    private void Control_Realized(object? sender, EventArgs e)
    {
        var con = self.Parent as Gtk.Container;
        foreach (var widget in con!.AllChildren)
        {
            if (widget is Gtk.RadioButton)
            {
                // Add the first radio group in the container
                (sender as Gtk.RadioButton)?.JoinGroup((Gtk.RadioButton)widget);
                break;
            }
        }
        self.Active = @checked;
        self.Toggled += Self_Toggled;
    }
    public event EventHandler? CheckedChanged;

    public override string Text { get => self.Label;
        set => self.Label = value;
    }
    public bool Checked { get => self.Active;
        set { @checked = true; self.Active = true; } }
    private bool @checked;
}