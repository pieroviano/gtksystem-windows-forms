//
// LinkLabelTest.cs: MWF LinkLabel unit tests.
//
// Author:
//   Everaldo Canuto (ecanuto@novell.com)
//
// (C) 2007 Novell, Inc. (http://www.novell.com)
//

using GtkTests.Helpers;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class LinkLabelTest : TestHelper
{
    [Test]
    public void LinkLabelAccessibility()
    {
        var l = new LinkLabel();
        Assert.IsNotNull(l.AccessibilityObject);
    }

    [Test]
    public void TestTabStop()
    {
        var l = new LinkLabel();

        Assert.IsFalse(l.TabStop);
        l.Text = "Hello";
        Assert.IsTrue(l.TabStop);
        l.Text = string.Empty;
        Assert.IsFalse(l.TabStop);
    }

    [Test] // bug #344012
    public void InvalidateManualLinks()
    {
        var form = new Form();
        form.ShowInTaskbar = false;

        var l = new LinkLabel();
        l.Text = "linkLabel1";
        form.Controls.Add(l);

        var link = new LinkLabel.Link(2, 5);
        l.Links.Add(link);

        form.Show();
        form.Dispose();
    }
}


[TestFixture]
public class LinkTest : TestHelper
{
    [Test]
    public void Constructor()
    {
        var l = new LinkLabel.Link();

        Assert.That((object?)l.Description, Is.EqualTo(null));
        Assert.That(l.LinkData, Is.EqualTo(null));

        l = new LinkLabel.Link(5, 20);

        Assert.That((object?)l.Description, Is.EqualTo(null));
        Assert.That(l.LinkData, Is.EqualTo(null));

        l = new LinkLabel.Link(3, 7, "test");

        Assert.That((object?)l.Description, Is.EqualTo(null));
        Assert.That(l.LinkData, Is.EqualTo("test"));
    }
}

[TestFixture]
public class LinkCollectionTest : TestHelper
{
    [Test] // ctor (LinkLabel)
    public void Constructor1()
    {
        var l = new LinkLabel();
        l.Text = "Managed Windows Forms";

        var links1 = new LinkLabel.LinkCollection(
            l);
        var links2 = new LinkLabel.LinkCollection(
            l);

        Assert.That((object?)links1.Count, Is.EqualTo(1));
        Assert.IsFalse(links1.IsReadOnly);
    }

    [Test] // ctor (LinkLabel)
    public void Constructor1_Owner_Null()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            try
            {
                new LinkLabel.LinkCollection(null!);
            }
            catch (ArgumentNullException ex)
            {
                object expected = typeof(ArgumentNullException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                Assert.IsNotNull(ex.ParamName);
                Assert.That((object?)ex.ParamName, Is.EqualTo("owner"));
                throw;
            }
        });
    }

    [Test] // Add (LinkLabel.Link)
    public void Add1()
    {
        var l = new LinkLabel();
        l.Text = "Managed Windows Forms";

        var links1 = new LinkLabel.LinkCollection(
            l);
        var links2 = new LinkLabel.LinkCollection(
            l);

        var linkA = new LinkLabel.Link(0, 7);
        Assert.That((object?)links1.Add(linkA), Is.EqualTo(0));
        Assert.That((object?)links1.Count, Is.EqualTo(1));
        Assert.That((object?)links2.Count, Is.EqualTo(1));
        Assert.That(links1[0], Is.SameAs(linkA));
        Assert.That(links2[0], Is.SameAs(linkA));

        var linkB = new LinkLabel.Link(8, 7);
        Assert.That((object?)links1.Add(linkB), Is.EqualTo(1));
        Assert.That((object?)links1.Count, Is.EqualTo(2));
        Assert.That((object?)links2.Count, Is.EqualTo(2));
        Assert.That(links1[0], Is.SameAs(linkA));
        Assert.That(links2[0], Is.SameAs(linkA));
        Assert.That(links1[1], Is.SameAs(linkB));
        Assert.That(links2[1], Is.SameAs(linkB));

        var links3 = new LinkLabel.LinkCollection(
            l);
        Assert.That((object?)links3.Count, Is.EqualTo(2));
        Assert.That(links3[0], Is.SameAs(linkA));
        Assert.That(links3[1], Is.SameAs(linkB));
    }

    [Test] // Add (LinkLabel.Link)
    public void Add1_Overlap()
    {
        var l = new LinkLabel();
        l.Text = "Managed Windows Forms";

        var links = new LinkLabel.LinkCollection(
            l);

        var linkA = new LinkLabel.Link(0, 7);
        links.Add(linkA);
        Assert.That((object?)links.Count, Is.EqualTo(1));
        Assert.That(links[0], Is.SameAs(linkA));

        var linkB = new LinkLabel.Link(5, 4);
        Assert.Throws<InvalidOperationException>(() =>
        {
            try
            {
                links.Add(linkB);
            }
            catch (InvalidOperationException ex)
            {
                // Overlapping link regions
                object expected = typeof(InvalidOperationException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                throw;
            }
        });

        Assert.That((object?)links.Count, Is.EqualTo(2));
        Assert.That(links[0], Is.SameAs(linkA));
        Assert.That(links[1], Is.SameAs(linkB));

        var linkC = new LinkLabel.Link(14, 3);
        Assert.Throws<InvalidOperationException>(() =>
        {
            try
            {
                links.Add(linkC);
            }
            catch (InvalidOperationException ex)
            {
                // Overlapping link regions
                object expected = typeof(InvalidOperationException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                throw;
            }
        });

        Assert.That((object?)links.Count, Is.EqualTo(3));
        Assert.That(links[0], Is.SameAs(linkA));
        Assert.That(links[1], Is.SameAs(linkB));
        Assert.That(links[2], Is.SameAs(linkC));
    }

    [Test] // Add (LinkLabel.Link)
    public void Add1_Value_Null()
    {
        var l = new LinkLabel();
        l.Text = "Managed Windows Forms";

        var links = new LinkLabel.LinkCollection(
            l);
        Assert.Throws<NullReferenceException>(() =>
        {
            links.Add(null!);
        });
    }

}