/*
 * A cross-platform interface component developed based on GTK components and compatible with the native C# control winform interface.
 * Use this component GTKSystem.Windows.Forms instead of Microsoft.WindowsDesktop.App.WindowsForms, compile once, run across platforms windows, linux, macos
 * Technical support 438865652@qq.com, https://www.gtkapp.com, https://gitee.com/easywebfactory, https://github.com/easywebfactory
 * author: chenhongjin
 */

using Gtk;
using System.ComponentModel;

using GtkApplication = System.Windows.Forms.Application;

namespace System.Windows.Forms;

[ToolboxItemFilter("System.Windows.Forms")]
public abstract partial class CommonDialog : Component
{
    private static readonly object helpRequestEvent = new();
    public object? Tag { get; set; }

    public abstract void Reset();

    protected abstract bool RunDialog(IWin32Window? owner);

    public virtual DialogResult ShowDialog()
    {
        return ShowDialog(owner: null);
    }

    protected Widget? okButton;
    protected Widget? cancelButton;

    public virtual void ClickOk()
    {
        if (okButton != null)
        {
            GLib.Signal.Emit(okButton, "clicked");
        }
        OnOKClicked(EventArgs.Empty);
    }

    public virtual void ClickCancel()
    {
        if (cancelButton != null)
        {
            GLib.Signal.Emit(cancelButton, "clicked");
        }
        OnCancelClicked(EventArgs.Empty);
    }

    public virtual DialogResult ShowDialog(IWin32Window? owner)
    {
        var result = DialogResult.Cancel;
        var runresult = RunDialog(owner);
        if (runresult)
        {
            result = DialogResult.OK;
        }
        return result;
    }
}