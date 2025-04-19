using GtkTests.Helpers;
using System.Windows.Forms;

namespace GtkTests.Collections;

[TestFixture]
public class ControlCollectionTest : TestHelper
{
    [Test]
    public void ControlCollectionTests()
    {
        var c = new Control();
        c.Name = "A";
        var c2 = new Control();
        c2.Name = "B";
        var c3 = new Control();
        c3.Name = "a";
        var c4 = new Control();
        c4.Name = "B";
        var c5 = new Control();
        c5.Name = "a";

        c.Controls.Add(c2);
        c.Controls.Add(c3);
        c2.Controls.Add(c4);
        c2.Controls.Add(c5);

        // this[key]
        Assert.That((object?)c.Controls["B"], Is.SameAs(c2));
        Assert.That((object?)c.Controls["b"], Is.SameAs(c2));

        // Owner
        Assert.That((object?)c.Controls.Owner, Is.SameAs(c));

        // ContainsKey
        Assert.That((object?)c.Controls.ContainsKey("A"), Is.EqualTo(true));
        Assert.That((object?)c.Controls.ContainsKey("a"), Is.EqualTo(true));
        Assert.That((object?)c.Controls.ContainsKey("C"), Is.EqualTo(false));

        // Find
        Assert.That((object?)c.Controls.Find("A", false).Length, Is.EqualTo(1));
        Assert.That((object?)c.Controls.Find("a", false).Length, Is.EqualTo(1));
        Assert.That((object?)c.Controls.Find("C", false).Length, Is.EqualTo(0));

        Assert.That((object?)c.Controls.Find("A", true).Length, Is.EqualTo(2));
        Assert.That((object?)c.Controls.Find("a", true).Length, Is.EqualTo(2));
        Assert.That((object?)c.Controls.Find("C", true).Length, Is.EqualTo(0));
        Assert.That((object?)c2.Controls.Find("b", true).Length, Is.EqualTo(1));

        // IndexOfKey
        Assert.That((object?)c.Controls.IndexOfKey("A"), Is.EqualTo(1));
        Assert.That((object?)c.Controls.IndexOfKey("a"), Is.EqualTo(1));
        Assert.That((object?)c.Controls.IndexOfKey("C"), Is.EqualTo(-1));

        // RemoveByKey
        c.Controls.RemoveByKey("A");
        Assert.That((object?)c.Controls.Count, Is.EqualTo(1));

        c.Controls.RemoveByKey("b");
        Assert.That((object?)c.Controls.Count, Is.EqualTo(0));

        c.Controls.RemoveByKey(null);
    }

    [Test]
    public void ControlCollectionFindANE()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            var c = new Control();
            c.Controls.Find(string.Empty, false);
        });
    }
}