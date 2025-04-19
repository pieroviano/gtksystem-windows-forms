using System.Globalization;
using System.Windows.Forms;
using GTKSystemWinFormsApp11;

namespace GtkTests.Samples;

[TestFixture]
public class LocalizationTests
{
    [Test]
    [TestCase("en-US")]
    [TestCase("zh-HANS")]
    public async Task WhenCultureIsSelectedThenGtkFormIsShownInAppropriateLanguage(string language)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
        GTKSystemWinFormsApp11.Properties.Resources.Culture = Thread.CurrentThread.CurrentCulture;

        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        var mainForm = new GtkForm();
        var taskCompletionSource = new TaskCompletionSource<string>();
        mainForm.Tag = taskCompletionSource;
        mainForm.Load += MainForm_Load;
        Application.Run(mainForm);
        await taskCompletionSource.Task;
        Assert.That((object?)taskCompletionSource.Task.Result.ToLower(), Is.EqualTo(language.ToLower()));
    }

    private async void MainForm_Load(object? sender, EventArgs e)
    {
        await MainFormOnLoadAsync((sender as GtkForm)!);
    }

    private async Task MainFormOnLoadAsync(GtkForm form)
    {
        form.Load -= MainForm_Load;
        form.Text = Thread.CurrentThread.CurrentUICulture.Name;
#if TESTCLICK
        form.LinkLabel1.PerformClick();
#endif
        await Task.Delay(TimeSpan.FromSeconds(5));
        var taskCompletionSource = (TaskCompletionSource<string>)form.Tag!;
        form.Dispose();
        if (!taskCompletionSource.Task.IsCompleted)
        {
            taskCompletionSource.SetResult(Thread.CurrentThread.CurrentUICulture.Name);
        }
    }
}