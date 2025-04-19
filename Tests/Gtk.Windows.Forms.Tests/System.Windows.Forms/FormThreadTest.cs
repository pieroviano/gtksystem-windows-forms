using GtkTests.Helpers;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class FormThreadTest : TestHelper
{
    private static void GuiThread()
    {
        var form1 = new Form();
        form1.Show();
        form1.Dispose();
    }

    [Test]
    public void TestThreadFormsInit()
    {
        var thread = new Thread(GuiThread);
        thread.Start();
        thread.Join();

        GuiThread();
    }
}