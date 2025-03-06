/*
 * A cross-platform interface component developed based on GTK components and compatible with the native C# control winform interface.
 * Use this component GTKSystem.Windows.Forms instead of Microsoft.WindowsDesktop.App.WindowsForms, compile once, run across platforms windows, linux, macos
 * Technical support 438865652@qq.com, https://www.gtkapp.com, https://gitee.com/easywebfactory, https://github.com/easywebfactory
 * author:chenhongjin
 */

using Gtk;

namespace System.Windows.Forms;

public abstract class FileDialog : CommonDialog
{
    public FileChooserDialog? fileDialog;

    public bool ValidateNames { get; set; } = true;

    public string? Title { get; set; } = string.Empty;

    public bool SupportMultiDottedExtensions { get; set; }

    public bool ShowHelp { get; set; }

    public bool RestoreDirectory { get; set; }

    public string? InitialDirectory { get; set; }
    public string? Description { get; set; }
    internal bool Multiselect { get; set; }
    public int FilterIndex { get; set; }

    private string? _filter;
    private string defaultExt = string.Empty;
    private string[] fileNames = [];
    private string fileName = string.Empty;
    private string description = string.Empty;

    public string? Filter
    {
        get => _filter ?? string.Empty;
        set
        {
            if (value == _filter)
            {
                return;
            }

            if (value == null)
            {
                _filter = string.Empty;
                return;
            }
            var filters = value.Split(';');
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    var pattern = filter.Split('|');
                    if (pattern == null || pattern.Length % 2 != 0 || pattern[1].Split('.').Length == 0)
                    {
                        throw new ArgumentException("FileDialog Invalid Filter");
                    }
                }
            }

            var array = value.Split('|');
            if (array == null || array[1].Split('.').Length == 0)
            {
                throw new ArgumentException("FileDialog Invalid Filter");
            }
            _filter = value;
        }
    }

    public bool AutoUpgradeEnabled { get; set; }
    internal string? SelectedDirectory { get; set; }
    public string? FileName { get; set; }
    public string[]? FileNames { get; internal set; }

    public bool DereferenceLinks { get; set; } = true;

    public string DefaultExt
    {
        get => defaultExt;
        set => defaultExt = value?.TrimStart('.') ?? string.Empty;
    }

    public bool CheckPathExists { get; set; } = true;

    public virtual bool CheckFileExists { get; set; } = true;

    internal FileChooserAction ActionType { get; set; }
    public override void Reset()
    {
        AddExtension = true;
        Title = null;
        InitialDirectory = null;
        FileName = null;
        FileNames = null;
        _filter = null;
        FilterIndex = 1;
        SupportMultiDottedExtensions = false;
    }

    public bool AddExtension { get; set; }

    protected override bool RunDialog(IWin32Window? owner)
    {
        if (owner is Form ownerform)
        {
            fileDialog = new FileChooserDialog(
                Properties.Resources.FileDialog_RunDialog_Select_file, ownerform.self,
                ActionType);
            fileDialog.WindowPosition = WindowPosition.CenterOnParent;
        }
        else
        {
            fileDialog =
                new FileChooserDialog(Properties.Resources.FileDialog_RunDialog_Select_file,
                    null, ActionType);
            fileDialog.WindowPosition = WindowPosition.Center;
        }

        fileDialog.KeepAbove = true;
        fileDialog.AddButton(Properties.Resources.MessageBox_ShowCore_OK, ResponseType.Ok);
        fileDialog.AddButton(Properties.Resources.MessageBox_ShowCore_Cancel,
            ResponseType.Cancel);
        fileDialog.SelectMultiple = Multiselect;
        fileDialog.Title = Title ?? string.Empty;
        fileDialog.TooltipText = Description ?? string.Empty;

        fileDialog.KeepAbove = true;
        if (!string.IsNullOrWhiteSpace(SelectedDirectory))
            fileDialog.SetCurrentFolder(SelectedDirectory);
        else if (!string.IsNullOrWhiteSpace(InitialDirectory))
            fileDialog.SetCurrentFolder(InitialDirectory);

        if (!string.IsNullOrWhiteSpace(DefaultExt))
        {
            DefaultExt = DefaultExt.Trim('.');
            var filter = new FileFilter();
            filter.AddMimeType(DefaultExt);
            filter.AddPattern($"*.{DefaultExt}");
            fileDialog.Filter = filter;
        }
        if (_filter != null)
        {
            var filters = _filter.Split(';');
            foreach (var filter in filters)
            {
                var pattern = filter.Split('|');
                var ffilter = new FileFilter();
                ffilter.AddMimeType(pattern[0]);
                ffilter.AddPattern(pattern[1]);
                fileDialog.AddFilter(ffilter);
            }
        }

        var response = fileDialog.Run();
        FileName = fileDialog.Filename;
        FileNames = fileDialog.Filenames.Clone() as string[];
        SelectedDirectory = fileDialog.Filename;
        fileDialog.HideOnDelete();
        return response == -5;
    }
    protected override void Dispose(bool disposing)
    {
        if (fileDialog != null)
        {
            fileDialog.Destroy();
            fileDialog = null;
        }
        base.Dispose(disposing);
    }
}