/*
 * A cross-platform interface component developed based on GTK components and compatible with the native C# control winform interface.
 * Use this component GTKSystem.Windows.Forms instead of Microsoft.WindowsDesktop.App.WindowsForms, compile once, run across platforms windows, linux, macos
 * Technical support 438865652@qq.com, https://www.gtkapp.com, https://gitee.com/easywebfactory, https://github.com/easywebfactory
 * author: chenhongjin
 */

using Gtk;
using System.ComponentModel;

namespace System.Windows.Forms;

public class FolderBrowserDialog : FileDialog
{
    private Environment.SpecialFolder rootFolder = Environment.SpecialFolder.Desktop;

    public override void Reset()
    {
        RootFolder = Environment.SpecialFolder.Desktop;
        Description = string.Empty;
        SelectedPath = string.Empty;
        SelectedPathNeedsCheck = false;
        ShowNewFolderButton = true;
        base.Reset();
    }

    public Environment.SpecialFolder RootFolder
    {
        get => rootFolder;
        set
        {
            var specialFolders = Enum.GetValues(typeof(Environment.SpecialFolder)).Cast<Environment.SpecialFolder>();
            if (!specialFolders.Contains(value))
            {
                var argumentName = nameof(value);
                var invalidEnumArgumentException = new InvalidEnumArgumentException(argumentName,(int)value,typeof(Environment.SpecialFolder));
                throw invalidEnumArgumentException;
            }
            rootFolder = value;
        }
    }

    public string SelectedPath
    {
        get => SelectedDirectory ?? string.Empty;
        set => SelectedDirectory = value;
    }

    public string[]? SelectedPaths => FileNames;

    private new bool Multiselect => base.Multiselect;

    private new string? Title => base.Title;

    public bool ShowNewFolderButton { get; set; } = true;

    public bool SelectedPathNeedsCheck { get; set; }

    public override DialogResult ShowDialog(IWin32Window? owner)
    {
        ActionType = Gtk.FileChooserAction.SelectFolder;
        return base.ShowDialog(owner);
    }

    public override string ToString()
    {
        return "System.Windows.Forms.FolderBrowserDialog";
    }
}