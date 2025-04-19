using System.Windows.Forms;
using GTKWinFormsApp;

namespace GtkTests.CommonDialogs;

internal class CommonDialogsTestRunner(Type type)
{
    public void MainForm_Load(object? sender, EventArgs e)
    {
        var form = (sender as CommonDialogsForm)!;
        form.Load -= MainForm_Load;
        form.Text = Thread.CurrentThread.CurrentUICulture.Name;

        form.DialogOnShown += OnFormDialogOnShown;
        var taskCompletionSource = (TaskCompletionSource<string>)form.Tag!;
        CancellationTokenSource cts = new();

        var cancellationToken = cts.Token;
        var commonDialogsButtonClicker = new CommonDialogsButtonClicker(type, form);
        commonDialogsButtonClicker.Clicked += (_, _) =>
        {
            Finish(form, taskCompletionSource, cancellationToken);
            cts.Cancel();
        };
        form.Shown += (_, _) =>
        {
            commonDialogsButtonClicker.ClickButton();
        };
    }

    private static void Finish(CommonDialogsForm form, TaskCompletionSource<string> taskCompletionSource,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        form.Dispose();
        try
        {
            taskCompletionSource.SetResult(Thread.CurrentThread.CurrentUICulture.Name);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private void OnFormDialogOnShown(object? sender, CommonDialogEventArgs e)
    {
        _ = OnFormButtonsAvailableAsync(sender, e);
    }

    private async Task OnFormButtonsAvailableAsync(object? _, CommonDialogEventArgs e)
    {
        await Task.Delay(TimeSpan.FromSeconds(3));
        var fileDialog = e.Dialog;
        fileDialog.ClickCancel();
    }
}