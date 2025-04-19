using System.Windows.Forms;
using GTKWinFormsApp;

namespace GtkTests.CommonDialogs;

internal class CommonDialogsButtonClicker(Type type, CommonDialogsForm form)
{
    public event EventHandler? Clicked;
    public void ClickButton()
    {
        Task.Run(() =>
        {
            GLib.Idle.Add(() =>
            {
                if (type == typeof(OpenFileDialog))
                {
                    form.ButtonOpenFile_Click(null, EventArgs.Empty);
                }
                else if (type == typeof(SaveFileDialog))
                {
                    form.ButtonSaveFile_Click(null, EventArgs.Empty);
                }
                else if (type == typeof(FolderBrowserDialog))
                {
                    form.ButtonFolderBrowser_Click(null, EventArgs.Empty);
                }
                else if (type == typeof(ColorDialog))
                {
                    form.ButtonColorDialog_Click(null, EventArgs.Empty);
                }
                Clicked?.Invoke(this, EventArgs.Empty);
                return false;
            });
        });
    }
}