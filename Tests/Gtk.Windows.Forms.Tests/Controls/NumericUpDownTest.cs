using GtkTests.Helpers;
using System.Globalization;
using System.Windows.Forms;

namespace GtkTests.Controls;

[TestFixture]
public class NumericUpDownTest : TestHelper
{
    [Test]
    public void Minimum()
    {
        var f = new Form();
        var nud = new NumericUpDown();
        nud.Value = 0;
        nud.Minimum = 2;
        nud.Maximum = 4;
        f.Controls.Add(nud);
        f.Show();

        Assert.That((object?)nud.Value, Is.EqualTo(2));
        nud.Minimum = 3;
        Assert.That((object?)nud.Value, Is.EqualTo(3));
        f.Dispose();
    }

    [Test]
    public void Maximum()
    {
        var f = new Form();
        var nud = new NumericUpDown();
        nud.BeginInit();
        nud.Value = 1000;
        nud.Minimum = 2;
        nud.Maximum = 4;
        nud.EndInit();
        f.Controls.Add(nud);
        f.Show();

        Assert.That((object?)nud.Value, Is.EqualTo(4));
        nud.Maximum = 3;
        Assert.That((object?)nud.Value, Is.EqualTo(3));
        f.Dispose();
    }

    [Test]
    public void Hexadecimal()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        var f = new Form();
        var nud = new NumericUpDown();
        nud.Maximum = 100000;
        f.Controls.Add(nud);
        f.Show();

        nud.Value = 0; // bug 661750
        Assert.That((object?)nud.Text, Is.EqualTo("0"));
        Assert.That((object?)nud.Value, Is.EqualTo(0));
        f.Dispose();
    }

    [Test]
    public void SetValueThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var nud = new NumericUpDown();
            nud.Maximum = 3;
            nud.Value = 4;
            nud.Dispose();
        });
    }

    [Test]
    public void InitTest()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var nud = new NumericUpDown();
            nud.BeginInit();
            nud.Maximum = 3;
            nud.BeginInit();
            nud.EndInit();
            nud.Value = 4;
            nud.Dispose();
        });
    }
}