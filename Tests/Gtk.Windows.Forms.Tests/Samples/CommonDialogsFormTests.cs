using System.Globalization;
using System.Windows.Forms;
using GtkTests.CommonDialogs;
using GTKWinFormsApp;

namespace GtkTests.Samples;

[TestFixture]
public class CommonDialogsFormTests
{
    [Test]
    [TestCase("en-US", typeof(OpenFileDialog))]
    [TestCase("zh-HANS", typeof(OpenFileDialog))]
    [TestCase("en-US", typeof(SaveFileDialog))]
    [TestCase("zh-HANS", typeof(SaveFileDialog))]
    [TestCase("en-US", typeof(FolderBrowserDialog))]
    [TestCase("zh-HANS", typeof(FolderBrowserDialog))]
    [TestCase("en-US", typeof(ColorDialog))]
    [TestCase("zh-HANS", typeof(ColorDialog))]
    public async Task WhenCommonDialogIsOpenedThenDialogIsShownInAppropriateLanguage(string language, Type dialogType)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
        GTKWinFormsApp.Properties.Resources.Culture = Thread.CurrentThread.CurrentCulture;

        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        var mainForm = new CommonDialogsForm();
        mainForm.SetUseAsyncInvoke += (_, e) =>
        {
            e.UseAsyncInvoke = true;
        };
        mainForm.Tag = dialogType;
        var taskCompletionSource = new TaskCompletionSource<string>();
        mainForm.Tag = taskCompletionSource;
        mainForm.Load += new CommonDialogsTestRunner(dialogType).MainForm_Load;
        Application.Run(mainForm);
        await taskCompletionSource.Task;
        Assert.That((object?)taskCompletionSource.Task.Result.ToLower(), Is.EqualTo(language.ToLower()));
    }
}