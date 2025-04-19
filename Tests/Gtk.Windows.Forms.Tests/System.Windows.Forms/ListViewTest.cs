//
// ListViewTest.cs: Test cases for ListView.
//
// Author:
//   Ritvik Mayank (mritvik@novell.com)
//
// (C) 2005 Novell, Inc. (http://www.novell.com)
//

using GtkTests.Helpers;
using System.Collections;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class ListViewTest : TestHelper
{
    [Test]
    public void ListViewPropertyTest()
    {
        var mylistview = new ListView();
        Assert.That((object?)mylistview.Activation, Is.EqualTo(ItemActivation.Standard));
        Assert.That((object?)mylistview.Alignment, Is.EqualTo(ListViewAlignment.Top));
        Assert.That((object?)mylistview.AllowColumnReorder, Is.EqualTo(false));
        Assert.That((object?)mylistview.BorderStyle, Is.EqualTo(BorderStyle.Fixed3D));
        Assert.That((object?)mylistview.CheckBoxes, Is.EqualTo(false));
        Assert.That((object?)mylistview.CheckedIndices.Count, Is.EqualTo(0));
        Assert.That((object?)mylistview.CheckedItems.Count, Is.EqualTo(0));
        Assert.That((object?)mylistview.Columns.Count, Is.EqualTo(0));
        Assert.That((object?)mylistview.FullRowSelect, Is.EqualTo(false));
        Assert.That((object?)mylistview.GridLines, Is.EqualTo(false));
        Assert.That((object?)mylistview.HeaderStyle, Is.EqualTo(ColumnHeaderStyle.Clickable));
        Assert.That((object?)mylistview.HideSelection, Is.EqualTo(true));
        Assert.That((object?)mylistview.HoverSelection, Is.EqualTo(false));
        var item1 = new ListViewItem("A", -1);
        mylistview.Items.Add(item1);
        Assert.That((object?)mylistview.Items.Count, Is.EqualTo(1));
        Assert.That((object?)mylistview.LabelEdit, Is.EqualTo(false));
        Assert.That((object?)mylistview.LabelWrap, Is.EqualTo(true));
        Assert.That((object?)mylistview.LargeImageList, Is.EqualTo(null));
        Assert.That((object?)mylistview.ListViewItemSorter, Is.EqualTo(null));
        Assert.That((object?)mylistview.MultiSelect, Is.EqualTo(true));
        Assert.That((object?)mylistview.Scrollable, Is.EqualTo(true));
        Assert.That((object?)mylistview.SelectedIndices.Count, Is.EqualTo(0));
        Assert.That((object?)mylistview.SelectedItems.Count, Is.EqualTo(0));
        Assert.That((object?)mylistview.SmallImageList, Is.EqualTo(null));
        Assert.That((object?)mylistview.LargeImageList, Is.EqualTo(null));
        Assert.That((object?)mylistview.Sorting, Is.EqualTo(SortOrder.None));
        Assert.That((object?)mylistview.StateImageList, Is.EqualTo(null));
        Assert.That((object?)mylistview.View, Is.EqualTo(View.LargeIcon));
        mylistview.View = View.List;
        Assert.That((object?)mylistview.ShowItemToolTips, Is.EqualTo(false));
    }

    [Test]
    public void ArrangeIconsTest()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        var mylistview = new ListView();
        myform.Controls.Add(mylistview);
        mylistview.Items.Add("Item 1");
        mylistview.Items.Add("Item 2");
        mylistview.View = View.LargeIcon;
        myform.Dispose();
    }

    // Hey
    [Test]
    public void BeginEndUpdateTest()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var mylistview = new ListView();
        mylistview.Items.Add("A");
        mylistview.Visible = true;
        myform.Controls.Add(mylistview);
        mylistview.BeginUpdate();
        for (var x = 1; x < 5000; x++)
        {
            mylistview.Items.Add("Item " + x.ToString());
        }
        mylistview.EndUpdate();
        myform.Dispose();
    }

    [Test]
    public void CheckBoxes()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var lvw = new ListView();
        form.Controls.Add(lvw);
        lvw.Items.Add("A");
        var itemB = lvw.Items.Add("B");
        lvw.Items.Add("C");
        itemB.Checked = true;

        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(0));
        Assert.That((object?)lvw.CheckedIndices.Count, Is.EqualTo(0));

        lvw.CheckBoxes = true;

        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedItems[0], Is.SameAs(itemB));
        Assert.That((object?)lvw.CheckedIndices.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedIndices[0], Is.EqualTo(1));

        form.Show();

        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedItems[0], Is.SameAs(itemB));
        Assert.That((object?)lvw.CheckedIndices.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedIndices[0], Is.EqualTo(1));

        lvw.CheckBoxes = false;

        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(0));
        Assert.That((object?)lvw.CheckedIndices.Count, Is.EqualTo(0));

        lvw.CheckBoxes = true;

        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedItems[0], Is.SameAs(itemB));
        Assert.That((object?)lvw.CheckedIndices.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedIndices[0], Is.EqualTo(1));
        form.Dispose();
    }

    [Test]
    public void ClearTest()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var mylistview = new ListView();
        var itemA = mylistview.Items.Add("A");
        var colA = mylistview.Columns.Add("Item Column", -2, HorizontalAlignment.Left);
        Assert.That((object?)itemA.ListView, Is.SameAs(mylistview));
        Assert.That((object?)colA.ListView, Is.SameAs(mylistview));
        mylistview.Visible = true;
        myform.Controls.Add(mylistview);
        Assert.That((object?)mylistview.Columns.Count, Is.EqualTo(1));
        Assert.That((object?)mylistview.Items.Count, Is.EqualTo(1));
        mylistview.Clear();
        Assert.That((object?)mylistview.Columns.Count, Is.EqualTo(0));
        Assert.That((object?)mylistview.Items.Count, Is.EqualTo(0));
        Assert.IsNull(itemA.ListView);
        Assert.IsNull(colA.ListView);
        myform.Dispose();
    }

    [Test] // bug #80620
    public void ClientRectangle_Borders()
    {
        var lv = new ListView();
        lv.CreateControl();
        Assert.That((object?)new ListView().ClientRectangle, Is.EqualTo(lv.ClientRectangle));
    }

    [Test]
    public void DisposeTest()
    {
        var lv = new ListView();
        lv.View = View.Details;

        lv.LargeImageList = new ImageList();
        lv.SmallImageList = new ImageList();

        var lvi = new ListViewItem();
        lv.Items.Add(lvi);

        var col = new ColumnHeader();
        lv.Columns.Add(col);

        lv.Dispose();

        Assert.IsNull(lvi.ListView);
        Assert.IsNull(col.ListView);

        Assert.IsNull(lv.LargeImageList);
        Assert.IsNull(lv.SmallImageList);
        Assert.IsNull(lv.StateImageList);
    }

    private string dispose_log;

    [Test]
    public void DisposeLayoutTest()
    {
        var f = new Form();
        var lv = new ListView();
        f.Controls.Add(lv);
        f.Show();

        dispose_log = string.Empty;
        lv.Layout += DisposeOnLayout;
        lv.Dispose(); // just to be sure.
        f.Dispose();

        Assert.That((object?)dispose_log.Length, Is.EqualTo(0));
    }

    private void DisposeOnLayout(object? o, LayoutEventArgs args)
    {
        dispose_log = "OnLayout";
    }

    [Test]
    public void GetItemRectTest()
    {
        var mylistview = new ListView();
        mylistview.Items.Add("Item 1");
        mylistview.Items.Add("Item 2");
        var r = mylistview.GetItemRect(1);
        Assert.That((object?)r.Top, Is.EqualTo(0), "#35a");
        Assert.IsTrue(r.Bottom > 0, "#35b");
        Assert.IsTrue(r.Right > 0, "#35c");
        Assert.IsTrue(r.Left > 0, "#35d");
        Assert.IsTrue(r.Height > 0, "#35e");
        Assert.IsTrue(r.Width > 0, "#35f");
    }

    [Test]
    public void bug79076()
    {
        var entryList = new ListView();
        entryList.Sorting = SortOrder.Descending;

        entryList.BeginUpdate();
        entryList.Columns.Add("Type", 100, HorizontalAlignment.Left);

        var item = new ListViewItem(["A"]);
        entryList.Items.Add(item);
        item = new ListViewItem(["B"]);
        entryList.Items.Add(item);
    }

    [Test] // bug #79416
    public void MultiSelect()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var lvw = CreateListView(View.Details);
        form.Controls.Add(lvw);
        lvw.MultiSelect = true;
        lvw.Items[0].Selected = true;
        lvw.Items[2].Selected = true;

        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(0));
        Assert.That((object?)lvw.SelectedIndices.Count, Is.EqualTo(0));

        lvw.Items[0].Selected = false;

        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(0));
        Assert.That((object?)lvw.SelectedIndices.Count, Is.EqualTo(0));

        lvw.Items[0].Selected = true;

        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(0));
        Assert.That((object?)lvw.SelectedIndices.Count, Is.EqualTo(0));

        form.Show();

        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(2));
        Assert.That((object?)lvw.SelectedItems[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.SelectedItems[1].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.SelectedIndices.Count, Is.EqualTo(2));
        Assert.That((object?)lvw.SelectedIndices[0], Is.EqualTo(0));
        Assert.That((object?)lvw.SelectedIndices[1], Is.EqualTo(2));

        // de-select an item
        lvw.Items[2].Selected = false;

        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedItems[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.SelectedIndices.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedIndices[0], Is.EqualTo(0));

        // re-select that item
        lvw.Items[2].Selected = true;

        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(2));
        Assert.That((object?)lvw.SelectedItems[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.SelectedItems[1].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.SelectedIndices.Count, Is.EqualTo(2));
        Assert.That((object?)lvw.SelectedIndices[0], Is.EqualTo(0));
        Assert.That((object?)lvw.SelectedIndices[1], Is.EqualTo(2));

        // dis-allow selection of multiple items
        lvw.MultiSelect = false;

        // setting MultiSelect to false when multiple items have been
        // selected does not deselect items
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(2));
        Assert.That((object?)lvw.SelectedItems[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.SelectedItems[1].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.SelectedIndices.Count, Is.EqualTo(2));
        Assert.That((object?)lvw.SelectedIndices[0], Is.EqualTo(0));
        Assert.That((object?)lvw.SelectedIndices[1], Is.EqualTo(2));

        // de-select that item again
        lvw.Items[2].Selected = false;

        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedItems[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.SelectedIndices.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedIndices[0], Is.EqualTo(0));

        // re-select that item again
        lvw.Items[2].Selected = true;

        // when MultiSelect is false, and you attempt to select more than
        // one item, then all items will first be de-selected and then
        // the item in question is selected
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedItems[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.SelectedIndices.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedIndices[0], Is.EqualTo(2));
        form.Dispose();
    }

    [Test]
    public void Selected()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var lvw = CreateListView(View.Details);
        form.Controls.Add(lvw);
        lvw.MultiSelect = true;
        lvw.Items[0].Selected = true;
        lvw.Items[2].Selected = true;

        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(0));
        Assert.That((object?)lvw.SelectedIndices.Count, Is.EqualTo(0));

        form.Show();

        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(2));
        Assert.That((object?)lvw.SelectedItems[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.SelectedItems[1].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.SelectedIndices.Count, Is.EqualTo(2));
        Assert.That((object?)lvw.SelectedIndices[0], Is.EqualTo(0));
        Assert.That((object?)lvw.SelectedIndices[1], Is.EqualTo(2));
        form.Dispose();
    }

    [Test]
    public void FindItemWithText()
    {
        var lvw = new ListView();
        var lvi1 = new ListViewItem(string.Empty);
        var lvi2 = new ListViewItem("angle bracket");
        var lvi3 = new ListViewItem("bracket holder");
        var lvi4 = new ListViewItem("bracket");
        lvw.Items.AddRange([lvi1, lvi2, lvi3, lvi4]);

        Assert.That((object?)lvw.FindItemWithText(string.Empty), Is.EqualTo(lvi1));
        Assert.That((object?)lvw.FindItemWithText("bracket"), Is.EqualTo(lvi3));
        Assert.That((object?)lvw.FindItemWithText("BrackeT"), Is.EqualTo(lvi3));
        Assert.IsNull(lvw.FindItemWithText("holder"));

        object expected = lvw.Items[3];
        Assert.That((object?)lvw.FindItemWithText("bracket", true, 3), Is.EqualTo(expected));

        object expected1 = lvw.Items[2];
        Assert.That((object?)lvw.FindItemWithText("bracket", true, 0, true), Is.EqualTo(expected1));
        object expected2 = lvw.Items[3];
        Assert.That((object?)lvw.FindItemWithText("bracket", true, 0, false), Is.EqualTo(expected2));
        object expected3 = lvw.Items[3];
        Assert.That((object?)lvw.FindItemWithText("BrackeT", true, 0, false), Is.EqualTo(expected3));
        Assert.IsNull(lvw.FindItemWithText("brack", true, 0, false));

        // Sub item search tests
        lvw.Items.Clear();

        lvi1.Text = "A";
        lvi1.SubItems.Add("car bracket");
        lvi1.SubItems.Add("C");

        lvi2.Text = "B";
        lvi2.SubItems.Add("car");

        lvi3.Text = "C";

        lvw.Items.AddRange([lvi1, lvi2, lvi3]);

        Assert.That((object?)lvw.FindItemWithText("car", true, 0), Is.EqualTo(lvi1));
        Assert.That((object?)lvw.FindItemWithText("C", true, 0), Is.EqualTo(lvi3));
        Assert.That((object?)lvw.FindItemWithText("car", true, 1), Is.EqualTo(lvi2));
        Assert.IsNull(lvw.FindItemWithText("car", false, 0));

        Assert.That((object?)lvw.FindItemWithText("car", true, 0, true), Is.EqualTo(lvi1));
        Assert.That((object?)lvw.FindItemWithText("car", true, 0, false), Is.EqualTo(lvi2));
        Assert.That((object?)lvw.FindItemWithText("CaR", true, 0, false), Is.EqualTo(lvi2));
    }

    [Test]
    public void FindItemWithText_Exceptions()
    {
        var lvw = new ListView();

        // Shouldn't throw any exception
        lvw.FindItemWithText(null);

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            lvw.FindItemWithText(null, false, 0);
        });

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            lvw.FindItemWithText(null, false, lvw.Items.Count);
        });

        // Add a single item
        lvw.Items.Add("bracket");

        Assert.Throws<ArgumentNullException>(() =>
        {
            lvw.FindItemWithText(null);
        });

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            lvw.FindItemWithText("bracket", false, -1);
        });

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            lvw.FindItemWithText("bracket", false, lvw.Items.Count);
        });
    }

    [Test]
    public void Sort_Details_Checked()
    {
        AssertSort_Checked(View.Details);
    }

    [Test]
    public void Sort_Details_Created()
    {
        AssertSortNoIcon_Created(View.Details);
    }

    [Test]
    public void Sort_Details_NotCreated()
    {
        AssertSortNoIcon_NotCreated(View.Details);
    }

    [Test]
    public void Sort_Details_Selected()
    {
        AssertSort_Selected(View.Details);
    }

    [Test]
    public void Sort_LargeIcon_Checked()
    {
        AssertSort_Checked(View.LargeIcon);
    }

    [Test]
    public void Sort_LargeIcon_Created()
    {
        AssertSortIcon_Created(View.LargeIcon);
    }

    [Test]
    public void Sort_LargeIcon_NotCreated()
    {
        AssertSortIcon_NotCreated(View.LargeIcon);
    }

    [Test]
    public void Sort_LargeIcon_Selected()
    {
        AssertSort_Selected(View.LargeIcon);
    }

    [Test]
    public void Sort_List_Checked()
    {
        AssertSort_Checked(View.List);
    }

    [Test]
    public void Sort_List_Created()
    {
        AssertSortNoIcon_Created(View.List);
    }

    [Test]
    public void Sort_List_NotCreated()
    {
        AssertSortNoIcon_NotCreated(View.List);
    }

    [Test]
    public void Sort_List_Selection()
    {
        AssertSort_Selected(View.List);
    }

    [Test]
    public void Sort_SmallIcon_Checked()
    {
        AssertSort_Checked(View.SmallIcon);
    }

    [Test]
    public void Sort_SmallIcon_Created()
    {
        AssertSortIcon_Created(View.SmallIcon);
    }

    [Test]
    public void Sort_SmallIcon_NotCreated()
    {
        AssertSortIcon_NotCreated(View.SmallIcon);
    }

    [Test]
    public void Sort_SmallIcon_Selection()
    {
        AssertSort_Selected(View.SmallIcon);
    }

    [Test]
    public void Sort_Tile_Checked()
    {
        Assert.Throws<NotSupportedException>(() =>
        {
            AssertSort_Checked(View.Tile);
        });
    }

    [Test]
    public void Sort_Tile_Created()
    {
        AssertSortNoIcon_Created(View.Tile);
    }

    [Test]
    public void Sort_Tile_NotCreated()
    {
        AssertSortNoIcon_NotCreated(View.Tile);
    }

    [Test]
    public void Sort_Tile_Selection()
    {
        AssertSort_Selected(View.Tile);
    }

    private void AssertSortIcon_Created(View view)
    {
        var compareCount = 0;

        var form = new Form();
        form.ShowInTaskbar = false;
        var lvw = CreateListView(view);
        form.Controls.Add(lvw);
        Assert.IsNull(lvw.ListViewItemSorter, "#A");

        form.Show();

        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));

        lvw.Sorting = SortOrder.None;
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));

        lvw.Sorting = SortOrder.Descending;
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("A"));

        lvw.Sorting = SortOrder.Ascending;
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));

        lvw.Sorting = SortOrder.None;
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));

        lvw.Sorting = SortOrder.Ascending;
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));

        lvw.Sorting = SortOrder.Descending;
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("A"));

        lvw.Sorting = SortOrder.None;
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("A"));

        // when Sorting is None and a new item is added, the collection is
        // sorted using the previous Sorting value
        lvw.Items.Add("BB");
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("A"));

        lvw.Sorting = SortOrder.Ascending;
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("C"));

        // when Sorting is not None and a new item is added, the
        // collection is re-sorted automatically
        lvw.Items.Add("BA");
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("C"));

        // assign a custom comparer
        var mc = new MockComparer(false);
        lvw.ListViewItemSorter = mc;

        // when a custom IComparer is assigned, the collection is immediately
        // re-sorted
        Assert.IsTrue(mc.CompareCount > compareCount);
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("A"));

        // record compare count
        compareCount = mc.CompareCount;

        // modifying Sorting results in re-sort
        lvw.Sorting = SortOrder.Descending;
        Assert.IsTrue(mc.CompareCount > compareCount);
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("C"));

        // record compare count
        compareCount = mc.CompareCount;

        // setting Sorting to the same value does not result in a sort
        // operation
        lvw.Sorting = SortOrder.Descending;
        Assert.That((object?)mc.CompareCount, Is.EqualTo(compareCount));
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("C"));

        // modifying Sorting results in re-sort
        lvw.Sorting = SortOrder.Ascending;
        Assert.IsTrue(mc.CompareCount > compareCount);
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("A"));

        // record compare count
        compareCount = mc.CompareCount;

        // adding an item when Sorting is not None causes re-sort
        lvw.Items.Add("BC");
        Assert.IsTrue(mc.CompareCount > compareCount);
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BC"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[5].Text, Is.EqualTo("A"));

        // record compare count
        compareCount = mc.CompareCount;

        // assigning the same custom IComparer again does not result in a
        // re-sort
        lvw.ListViewItemSorter = mc;
        Assert.That((object?)mc.CompareCount, Is.EqualTo(compareCount));
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BC"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[5].Text, Is.EqualTo("A"));

        // setting Sorting to None does not perform a sort
        lvw.Sorting = SortOrder.None;
        Assert.That((object?)mc.CompareCount, Is.EqualTo(compareCount));
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BC"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[5].Text, Is.EqualTo("A"));

        // assigning the custom IComparer again does not result in a
        // re-sort
        lvw.ListViewItemSorter = mc;
        Assert.That((object?)mc.CompareCount, Is.EqualTo(compareCount));
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BC"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[5].Text, Is.EqualTo("A"));

        // set Sorting to Ascending again
        lvw.Sorting = SortOrder.Ascending;
        Assert.IsTrue(mc.CompareCount > compareCount);
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BC"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[5].Text, Is.EqualTo("A"));

        // record compare count
        compareCount = mc.CompareCount;

        // explicitly calling Sort results in a sort operation
        lvw.Sort();
        Assert.IsTrue(mc.CompareCount > compareCount);
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BC"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[5].Text, Is.EqualTo("A"));
        lvw.Sorting = SortOrder.None;

        // record compare count
        compareCount = mc.CompareCount;

        // adding an item when Sorting is None causes re-sort
        lvw.Items.Add("BD");
        Assert.IsTrue(mc.CompareCount > compareCount);
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("BC"));
        Assert.That((object?)lvw.Items[5].Text, Is.EqualTo("BD"));
        Assert.That((object?)lvw.Items[6].Text, Is.EqualTo("C"));

        // record compare count
        compareCount = mc.CompareCount;

        // explicitly calling Sort when Sorting is None causes a re-sort
        lvw.Sort();
        Assert.IsTrue(mc.CompareCount > compareCount);
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("BC"));
        Assert.That((object?)lvw.Items[5].Text, Is.EqualTo("BD"));
        Assert.That((object?)lvw.Items[6].Text, Is.EqualTo("C"));

        // record compare count
        compareCount = mc.CompareCount;
        form.Dispose();
    }

    private void AssertSortIcon_NotCreated(View view)
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var lvw = CreateListView(view);
        form.Controls.Add(lvw);

        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));

        lvw.Sorting = SortOrder.None;
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));

        lvw.Sorting = SortOrder.Descending;
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));

        lvw.Sorting = SortOrder.Ascending;
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));

        // when the handle is not created and a new item is added, the new
        // item is just appended to the collection
        lvw.Items.Add("BB");
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));

        // assign a custom comparer
        var mc = new MockComparer(false);
        lvw.ListViewItemSorter = mc;

        // assigning a custom IComparer has no effect
        Assert.That((object?)mc.CompareCount, Is.EqualTo(0));
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));

        // modifying Sorting does not result in sort operation
        lvw.Sorting = SortOrder.Descending;
        Assert.That((object?)mc.CompareCount, Is.EqualTo(0));
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));

        // setting Sorting to the same value does not result in a sort
        // operation
        lvw.Sorting = SortOrder.Descending;
        Assert.That((object?)mc.CompareCount, Is.EqualTo(0));
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));

        // setting Sorting to None does not result in a sort operation
        lvw.Sorting = SortOrder.None;
        Assert.That((object?)mc.CompareCount, Is.EqualTo(0));
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));

        // explicitly calling Sort when Sorting is None does not result
        // in a sort operation
        lvw.Sort();
        Assert.That((object?)mc.CompareCount, Is.EqualTo(0));
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));

        // setting Sorting again does not result in a sort operation
        lvw.Sorting = SortOrder.Ascending;
        Assert.That((object?)mc.CompareCount, Is.EqualTo(0));
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));

        // explicitly calling Sort when Sorting is Ascending does not 
        // result in a sort operation
        lvw.Sort();
        Assert.That((object?)mc.CompareCount, Is.EqualTo(0));
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));

        // show the form to create the handle
        form.Show();

        // when the handle is created, the items are immediately sorted
        Assert.IsTrue(mc.CompareCount > 0);
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("A"));

        // setting ListViewItemSorter to null does not result in sort
        // operation
        lvw.ListViewItemSorter = null;
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("A"));

        // explicitly calling sort does not result in sort operation
        lvw.Sort();
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("A"));

        form.Dispose();
    }

    private void AssertSortNoIcon_Created(View view)
    {
        var compareCount = 0;

        var form = new Form();
        form.ShowInTaskbar = false;
        var lvw = CreateListView(view);
        form.Controls.Add(lvw);
        Assert.IsNull(lvw.ListViewItemSorter, "#A");

        form.Show();

        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));

        lvw.Sorting = SortOrder.None;
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));

        lvw.Sorting = SortOrder.Ascending;
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));

        lvw.Sorting = SortOrder.Descending;
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("A"));

        lvw.Sorting = SortOrder.None;
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("A"));

        // when Sorting is None and a new item is added, the item is
        // appended to the collection
        lvw.Items.Add("BB");
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));

        lvw.Sorting = SortOrder.Ascending;
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("C"));

        // when Sorting is not None and a new item is added, the 
        // collection is re-sorted automatically
        lvw.Items.Add("BA");
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("C"));

        // assign a custom comparer
        var mc = new MockComparer(false);
        lvw.ListViewItemSorter = mc;

        // when a custom IComparer is assigned, the collection is immediately
        // re-sorted
        Assert.IsTrue(mc.CompareCount > compareCount);
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("A"));

        // record compare count
        compareCount = mc.CompareCount;

        // modifying the sort order results in a sort
        lvw.Sorting = SortOrder.Descending;
        Assert.IsTrue(mc.CompareCount > compareCount);
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("C"));

        // record compare count
        compareCount = mc.CompareCount;

        // set the sort order to the same value does not result in a sort
        // operation
        lvw.Sorting = SortOrder.Descending;
        Assert.That((object?)mc.CompareCount, Is.EqualTo(compareCount));
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("C"));

        // modifying the sort order results in a sort
        lvw.Sorting = SortOrder.Ascending;
        Assert.IsTrue(mc.CompareCount > compareCount);
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("A"));

        // record compare count
        compareCount = mc.CompareCount;

        // adding an item when Sorting is not None caused a re-sort
        lvw.Items.Add("BC");
        Assert.IsTrue(mc.CompareCount > compareCount);
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BC"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[5].Text, Is.EqualTo("A"));

        // record compare count
        compareCount = mc.CompareCount;

        // assigning the same custom IComparer again does not result in a
        // re-sort
        lvw.ListViewItemSorter = mc;
        Assert.That((object?)mc.CompareCount, Is.EqualTo(compareCount));
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BC"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[5].Text, Is.EqualTo("A"));

        // setting sort order to None does not perform a sort and resets
        // the ListViewItemSorter
        lvw.Sorting = SortOrder.None;
        Assert.That((object?)mc.CompareCount, Is.EqualTo(compareCount));
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BC"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[5].Text, Is.EqualTo("A"));


        lvw.ListViewItemSorter = mc;
        // assigning the previous custom IComparer again results in a
        // re-sort
        Assert.IsTrue(mc.CompareCount > compareCount);
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("BC"));
        Assert.That((object?)lvw.Items[5].Text, Is.EqualTo("C"));

        // record compare count
        compareCount = mc.CompareCount;

        // set Sorting to Ascending again to verify that the internal
        // IComparer is not used when we reset Sorting to None
        // (as the items would then be sorted alfabetically)
        lvw.Sorting = SortOrder.Ascending;
        Assert.IsTrue(mc.CompareCount > compareCount);
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BC"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[5].Text, Is.EqualTo("A"));

        // record compare count
        compareCount = mc.CompareCount;

        lvw.Sorting = SortOrder.None;
        Assert.That((object?)mc.CompareCount, Is.EqualTo(compareCount));
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BC"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[5].Text, Is.EqualTo("A"));

        // record compare count
        compareCount = mc.CompareCount;

        lvw.Items.Add("BD");
        // adding an item when Sorting is None does not cause a re-sort
        Assert.That((object?)mc.CompareCount, Is.EqualTo(compareCount));
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BC"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[5].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[6].Text, Is.EqualTo("BD"));

        // record compare count
        compareCount = mc.CompareCount;

        lvw.Sort();
        // explicitly calling Sort when Sorting is None does nothing
        Assert.That((object?)mc.CompareCount, Is.EqualTo(compareCount));
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BC"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[5].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[6].Text, Is.EqualTo("BD"));

        // record compare count
        compareCount = mc.CompareCount;

        lvw.Sorting = SortOrder.Ascending;
        // setting Sorting again, does not reinstate the custom IComparer
        // but sorting is actually performed using an internal non-visible
        // comparer
        Assert.That((object?)mc.CompareCount, Is.EqualTo(compareCount));
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("BC"));
        Assert.That((object?)lvw.Items[5].Text, Is.EqualTo("BD"));
        Assert.That((object?)lvw.Items[6].Text, Is.EqualTo("C"));

        // record compare count
        compareCount = mc.CompareCount;

        lvw.Sort();
        // explicitly calling Sort, does not reinstate the custom IComparer
        // but sorting is actually performed using an internal non-visible
        // comparer
        Assert.That((object?)mc.CompareCount, Is.EqualTo(compareCount));
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("BA"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[4].Text, Is.EqualTo("BC"));
        Assert.That((object?)lvw.Items[5].Text, Is.EqualTo("BD"));
        Assert.That((object?)lvw.Items[6].Text, Is.EqualTo("C"));

        // record compare count
        compareCount = mc.CompareCount;

        form.Dispose();
    }

    private void AssertSortNoIcon_NotCreated(View view)
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var lvw = CreateListView(view);
        form.Controls.Add(lvw);

        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));

        lvw.Sorting = SortOrder.None;
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));

        lvw.Sorting = SortOrder.Ascending;
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));

        lvw.Sorting = SortOrder.Descending;
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));

        lvw.Sorting = SortOrder.None;
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));

        lvw.Sorting = SortOrder.Ascending;
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));

        // when the handle is not created and a new item is added, the new
        // item is just appended to the collection
        lvw.Items.Add("BB");
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));

        // assign a custom comparer
        var mc = new MockComparer(false);
        lvw.ListViewItemSorter = mc;

        // assigning a custom IComparer has no effect
        Assert.That((object?)mc.CompareCount, Is.EqualTo(0));
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));

        // modifying Sorting has no effect
        lvw.Sorting = SortOrder.Descending;
        Assert.That((object?)mc.CompareCount, Is.EqualTo(0));
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));

        // setting Sorting to the same value does not result in a sort
        // operation
        lvw.Sorting = SortOrder.Descending;
        Assert.That((object?)mc.CompareCount, Is.EqualTo(0));
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));

        // setting Sorting to another value does not result in a sort
        // operation
        lvw.Sorting = SortOrder.Ascending;
        Assert.That((object?)mc.CompareCount, Is.EqualTo(0));
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));

        lvw.Sorting = SortOrder.None;
        Assert.That((object?)mc.CompareCount, Is.EqualTo(0));
        // setting Sorting to None does not perform a sort and resets the
        // ListViewItemSorter
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));

        // explicitly calling Sort when Sorting is None does nothing
        lvw.Sort();
        Assert.That((object?)mc.CompareCount, Is.EqualTo(0));
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));

        lvw.Sorting = SortOrder.Ascending;
        Assert.That((object?)mc.CompareCount, Is.EqualTo(0));
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));

        // explicitly set the custom IComparer again
        lvw.ListViewItemSorter = mc;
        Assert.That((object?)mc.CompareCount, Is.EqualTo(0));
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));

        // explicitly calling Sort when handle is not created does not
        // result in sort operation
        Assert.That((object?)mc.CompareCount, Is.EqualTo(0));
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("A"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("BB"));

        // show the form to create the handle
        form.Show();

        // when the handle is created, the items are immediately sorted
        Assert.IsTrue(mc.CompareCount > 0);
        Assert.IsNotNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.ListViewItemSorter, Is.SameAs(mc));
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("A"));

        // setting ListViewItemSorter to null does not result in sort
        // operation
        lvw.ListViewItemSorter = null;
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("A"));

        // explicitly calling sort does not result in sort operation
        lvw.Sort();
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("A"));

        // modifying Sorting does not result in sort operation
        lvw.Sorting = SortOrder.Ascending;
        Assert.IsNull(lvw.ListViewItemSorter);
        Assert.That((object?)lvw.Items[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.Items[1].Text, Is.EqualTo("BB"));
        Assert.That((object?)lvw.Items[2].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.Items[3].Text, Is.EqualTo("A"));

        form.Dispose();
    }

    private void AssertSort_Checked(View view)
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var lvw = CreateListView(view);
        lvw.CheckBoxes = true;
        form.Controls.Add(lvw);

        form.Show();

        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(0));
        Assert.That((object?)lvw.CheckedIndices.Count, Is.EqualTo(0));

        // select an item
        lvw.Items[2].Checked = true;
        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedIndices.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedItems[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.CheckedIndices[0], Is.EqualTo(2));

        // sort the items descending
        lvw.Sorting = SortOrder.Descending;
        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedIndices.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedItems[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.CheckedIndices[0], Is.EqualTo(0));

        // add an item, which ends up before the selected item after the
        // sort operation
        var item = lvw.Items.Add("D");
        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedIndices.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedItems[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.CheckedIndices[0], Is.EqualTo(1));

        // remove an item before the selected item
        lvw.Items.Remove(item);
        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedIndices.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedItems[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.CheckedIndices[0], Is.EqualTo(0));

        // insert an item before the selected item
        lvw.Items.Insert(0, "D");
        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedIndices.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedItems[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.CheckedIndices[0], Is.EqualTo(1));

        // assign a custom comparer
        var mc = new MockComparer(false);
        lvw.ListViewItemSorter = mc;

        // items are re-sorted automatically
        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedIndices.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedItems[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.CheckedIndices[0], Is.EqualTo(2));

        // modify sort order
        lvw.Sorting = SortOrder.Ascending;
        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedIndices.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedItems[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.CheckedIndices[0], Is.EqualTo(1));

        form.Dispose();
    }

    private void AssertSort_Selected(View view)
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var lvw = CreateListView(view);
        form.Controls.Add(lvw);

        form.Show();

        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(0));
        Assert.That((object?)lvw.SelectedIndices.Count, Is.EqualTo(0));

        // select an item
        lvw.Items[2].Selected = true;
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedIndices.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedItems[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.SelectedIndices[0], Is.EqualTo(2));

        // sort the items descending
        lvw.Sorting = SortOrder.Descending;
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedIndices.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedItems[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.SelectedIndices[0], Is.EqualTo(0));

        // add an item, which ends up before the selected item after the
        // sort operation
        var item = lvw.Items.Add("D");
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedIndices.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedItems[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.SelectedIndices[0], Is.EqualTo(1));

        // remove an item before the selected item
        lvw.Items.Remove(item);
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedIndices.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedItems[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.SelectedIndices[0], Is.EqualTo(0));

        // insert an item before the selected item
        lvw.Items.Insert(0, "D");
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedIndices.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedItems[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.SelectedIndices[0], Is.EqualTo(1));

        // assign a custom comparer
        var mc = new MockComparer(false);
        lvw.ListViewItemSorter = mc;

        // items are re-sorted automatically
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedIndices.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedItems[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.SelectedIndices[0], Is.EqualTo(2));

        // modify sort order
        lvw.Sorting = SortOrder.Ascending;
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedIndices.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedItems[0].Text, Is.EqualTo("C"));
        Assert.That((object?)lvw.SelectedIndices[0], Is.EqualTo(1));

        form.Dispose();
    }

    private ListView CreateListView(View view)
    {
        var lvw = new ListView();
        lvw.View = view;
        lvw.Items.Add("B");
        lvw.Items.Add("A");
        lvw.Items.Add("C");
        return lvw;
    }

    private class MockComparer : IComparer
    {
        private int _compareCount;
        private readonly bool _throwException;

        public MockComparer(bool throwException)
        {
            _throwException = throwException;
        }

        public int CompareCount => _compareCount;

        public int Compare(object? x, object? y)
        {
            _compareCount++;
            if (_throwException)
                throw new InvalidOperationException();

            var item_x = x as ListViewItem;
            var item_y = y as ListViewItem;
            var sortOrder = item_x!.ListView!.Sorting;

            // we'll actually perform a reverse-sort
            if (sortOrder == SortOrder.Ascending)
                return string.Compare(item_y!.Text, item_x.Text);
            else
                return string.Compare(item_x.Text, item_y!.Text);
        }
    }

    [Test]  // Should not throw IndexOutOfBoundsException
    public void ReaddingItem()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var lvw1 = new ListView();
        var lvw2 = new ListView();
        lvw1.View = View.Details;
        lvw2.View = View.Details;
        lvw1.Columns.Add(new ColumnHeader("1"));
        lvw2.Columns.Add(new ColumnHeader("2"));
        form.Controls.Add(lvw1);
        form.Controls.Add(lvw2);
        form.Show();

        for (var i = 0; i < 50; i++)
            lvw1.Items.Add("A");
        lvw2.Items.Add("B1");

        var item = lvw1.Items[lvw1.Items.Count - 1];
        item.Selected = true;
        item.Remove();
        lvw2.Items.Add(item);
        item.Selected = true;

        Assert.That((object?)lvw1.Items.Count, Is.EqualTo(49));
        Assert.That((object?)lvw2.Items.Count, Is.EqualTo(2));
        Assert.That((object?)lvw2.Items[1].Selected, Is.EqualTo(true));

        form.Dispose();
    }

    [Test]  // Should not throw ArgumentOutOfRangeException
    public void DeleteNotFocusedItem()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var lvw = new ListView();
        form.Controls.Add(lvw);
        form.Show();

        for (var i = 0; i < 3; i++)
            lvw.Items.Add("A");

        lvw.Items[lvw.Items.Count - 1].Focused = true;
        lvw.Items[0].Remove();
        lvw.Items[0].Remove();

        form.Dispose();
    }
}