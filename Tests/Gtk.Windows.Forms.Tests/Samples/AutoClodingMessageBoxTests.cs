using System.Windows.Forms;

namespace GtkTests.Samples;

[TestFixture]
public class AutoClodingMessageBoxTests
{
    [Test]
    public void WhenAutoClosingMessageBoxIsShownThenItClosesAfterTimeout()
    {
        // Prepare
        Application.SetHighDpiMode(HighDpiMode.SystemAware);
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        var mainForm = new Form();

        // Act & Assert
        mainForm.LoadComplete += (_, _) =>
        {
            AutoClosingMessageBox.Instance.MessageBoxTimeout = 1000;
            AutoClosingMessageBox.Instance.Show("test message test message test messagetest message test message test message test messagetest message " +
                                                "test message test message test messagetest message test message test message test messagetest message test " +
                                                "message test message test messagetest message", "Doubt",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            AutoClosingMessageBox.Instance.Show("test message test message \ntest messagetest message", "Warn",
                MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
            mainForm.Close();
        };
        Application.Run(mainForm);
    }

}