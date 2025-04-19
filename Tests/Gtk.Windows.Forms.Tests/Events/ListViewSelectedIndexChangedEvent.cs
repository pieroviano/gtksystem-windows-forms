//
// ListViewEventTest.cs: Test cases for ListView events.
//
// Author:
//   Ritvik Mayank (mritvik@novell.com)
//
// (C) 2005 Novell, Inc. (http://www.novell.com)
//

using System.Windows.Forms;
using GtkTests.Helpers;

namespace GtkTests.Events;

[TestFixture]
public class ListViewSelectedIndexChangedEvent : TestHelper
{
    private int selectedIndexChanged;

    public void ListView_SelectedIndexChanged(object? sender, EventArgs e)
    {
        selectedIndexChanged++;
    }

    [SetUp]
    protected override void SetUp()
    {
        selectedIndexChanged = 0;
        base.SetUp();
    }

    [Test] // bug #79849
    public void SelectBeforeCreationOfHandle()
    {
        var form = new Form();
        form.ShowInTaskbar = false;

        var lvw = new ListView();
        lvw.SelectedIndexChanged += ListView_SelectedIndexChanged;
        lvw.View = View.Details;
        var itemA = new ListViewItem("A");
        lvw.Items.Add(itemA);
        Assert.That((object?)selectedIndexChanged, Is.EqualTo(0));
        itemA.Selected = true;
        Assert.That((object?)selectedIndexChanged, Is.EqualTo(0));

        var itemB = new ListViewItem("B");
        lvw.Items.Add(itemB);
        Assert.That((object?)selectedIndexChanged, Is.EqualTo(0));
        itemB.Selected = true;
        Assert.That((object?)selectedIndexChanged, Is.EqualTo(0));

        form.Controls.Add(lvw);
        Assert.That((object?)selectedIndexChanged, Is.EqualTo(0));
        form.Show();
        Assert.That((object?)selectedIndexChanged, Is.EqualTo(2));
        form.Dispose();
    }

    [Test]
    public void RemoveSelectedItem()
    {
        var form = new Form();
        form.ShowInTaskbar = false;

        var lvw = new ListView();
        lvw.SelectedIndexChanged += ListView_SelectedIndexChanged;
        lvw.View = View.Details;
        var itemA = new ListViewItem("A");
        lvw.Items.Add(itemA);
        Assert.That((object?)selectedIndexChanged, Is.EqualTo(0));
        itemA.Selected = true;
        Assert.That((object?)selectedIndexChanged, Is.EqualTo(0));

        var itemB = new ListViewItem("B");
        lvw.Items.Add(itemB);
        Assert.That((object?)selectedIndexChanged, Is.EqualTo(0));
        itemB.Selected = true;
        Assert.That((object?)selectedIndexChanged, Is.EqualTo(0));
        lvw.Items.Remove(itemB);
        Assert.IsTrue(itemB.Selected);

        form.Controls.Add(lvw);
        Assert.That((object?)selectedIndexChanged, Is.EqualTo(0));
        form.Show();
        Assert.That((object?)selectedIndexChanged, Is.EqualTo(1));
        lvw.Items.Remove(itemA);
        Assert.That((object?)selectedIndexChanged, Is.EqualTo(2));
        Assert.IsTrue(itemA.Selected);

        form.Close();
    }

    [Test]
    public void AddAndSelectItem()
    {
        var form = new Form();
        form.ShowInTaskbar = false;

        var lvw = new ListView();
        lvw.SelectedIndexChanged += ListView_SelectedIndexChanged;
        lvw.View = View.Details;
        form.Controls.Add(lvw);
        form.Show();

        var itemA = new ListViewItem();
        lvw.Items.Add(itemA);
        Assert.That((object?)selectedIndexChanged, Is.EqualTo(0));
        itemA.Selected = true;
        Assert.That((object?)selectedIndexChanged, Is.EqualTo(1));

        var itemB = new ListViewItem();
        lvw.Items.Add(itemB);
        Assert.That((object?)selectedIndexChanged, Is.EqualTo(1));
        itemB.Selected = true;
        Assert.That((object?)selectedIndexChanged, Is.EqualTo(2));

        form.Close();
    }

    [Test]
    public void InsertSelectedItem()
    {
        var form = new Form();
        form.ShowInTaskbar = false;

        var lvw = new ListView();
        lvw.SelectedIndexChanged += ListView_SelectedIndexChanged;
        form.Controls.Add(lvw);
        form.Show();

        var item = new ListViewItem
        {
            Selected = true
        };
        Assert.That((object?)selectedIndexChanged, Is.EqualTo(0));
        lvw.Items.Insert(0, item);
        Assert.That((object?)selectedIndexChanged, Is.EqualTo(1));

        form.Close();
    }

    [Test]
    public void AddRangeSelectedItems()
    {
        var form = new Form();
        form.ShowInTaskbar = false;

        var lvw = new ListView();
        lvw.SelectedIndexChanged += ListView_SelectedIndexChanged;
        form.Controls.Add(lvw);
        form.Show();

        ListViewItem[] items =
        [
            new("A"),
            new("B"),
            new("C")
        ];
        foreach (var item in items)
            item.Selected = true;

        Assert.That((object?)selectedIndexChanged, Is.EqualTo(0));
        lvw.Items.AddRange(items);
        Assert.That((object?)selectedIndexChanged, Is.EqualTo(3));

        form.Close();
    }
}