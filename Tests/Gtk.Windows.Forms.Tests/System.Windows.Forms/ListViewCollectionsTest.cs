// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// Copyright (c) 2005 Novell, Inc. (http://www.novell.com)
//
// Author:
//	Jordi Mas i Hernandez <jordi@ximian.com>
//
//

using System.Windows.Forms;
using System.Collections;
using GtkTests.Helpers;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class ListViewCollectionsTest : TestHelper
{
    /*
        ColumnHeaderCollection
    */
    [Test]
    public void ColumnHeaderCollectionTest_PropertiesTest()
    {
        var listview = new ListView();

        // Properties
        Assert.That((object?)listview.Columns.IsReadOnly, Is.EqualTo(false), "ColumnHeaderCollectionTest_PropertiesTest#1");
        Assert.That((object?)((ICollection)listview.Columns).IsSynchronized, Is.EqualTo(true), "ColumnHeaderCollectionTest_PropertiesTest#2");
        Assert.That(((ICollection)listview.Columns).SyncRoot, Is.EqualTo(listview.Columns), "ColumnHeaderCollectionTest_PropertiesTest#3");
        Assert.That((object?)((IList)listview.Columns).IsFixedSize, Is.EqualTo(false), "ColumnHeaderCollectionTest_PropertiesTest#4");
        Assert.That((object?)listview.Columns.Count, Is.EqualTo(0), "ColumnHeaderCollectionTest_PropertiesTest#5");
    }

    [Test]
    public void ColumnHeaderCollectionTest_AddTest()
    {
        var listview = new ListView();
        var colA = new ColumnHeader();
        var colB = new ColumnHeader();

        // Duplicated elements with same text added
        listview.Columns.Add(colA);
        listview.Columns.Add(colB);
        Assert.That((object?)listview.Columns.Count, Is.EqualTo(2));
        Assert.That((object?)listview.Columns[0].Text, Is.EqualTo("ColumnHeader"));
        Assert.That((object?)colA.ListView, Is.SameAs(listview));
        Assert.That((object?)colB.ListView, Is.SameAs(listview));
    }

    [Test]
    public void ColumnHeaderCollectionTest_ClearTest()
    {
        var listview = new ListView();
        var colA = new ColumnHeader();
        var colB = new ColumnHeader();
        listview.Columns.Add(colA);
        listview.Columns.Add(colB);
        listview.Columns.Clear();
        Assert.That((object?)listview.Columns.Count, Is.EqualTo(0));
        Assert.IsNull(colA.ListView);
        Assert.IsNull(colB.ListView);
    }

    [Test]
    public void ColumnHeaderCollectionTest_Remove()
    {
        var listview = new ListView();
        var colA = new ColumnHeader();
        var colB = new ColumnHeader();
        var colC = new ColumnHeader();
        listview.Columns.Add(colA);
        listview.Columns.Add(colB);
        listview.Columns.Add(colC);

        listview.Columns.Remove(colB);
        Assert.That((object?)listview.Columns.Count, Is.EqualTo(2));
        Assert.That((object?)listview.Columns[0], Is.SameAs(colA));
        Assert.That((object?)listview.Columns[1], Is.SameAs(colC));
        Assert.That((object?)colA.ListView, Is.SameAs(listview));
        Assert.IsNull(colB.ListView);
        Assert.That((object?)colC.ListView, Is.SameAs(listview));
        Assert.That((object?)colA.Index, Is.EqualTo(0));
        Assert.That((object?)colB.Index, Is.EqualTo(-1));
        Assert.That((object?)colC.Index, Is.EqualTo(1));

        listview.Columns.Remove(colC);
        Assert.That((object?)listview.Columns.Count, Is.EqualTo(1));
        Assert.That((object?)listview.Columns[0], Is.SameAs(colA));
        Assert.That((object?)colA.ListView, Is.SameAs(listview));
        Assert.IsNull(colB.ListView);
        Assert.IsNull(colC.ListView);
        Assert.That((object?)colA.Index, Is.EqualTo(0));
        Assert.That((object?)colB.Index, Is.EqualTo(-1));
        Assert.That((object?)colC.Index, Is.EqualTo(-1));

        listview.Columns.Remove(colA);
        Assert.That((object?)listview.Columns.Count, Is.EqualTo(0));
        Assert.IsNull(colA.ListView);
        Assert.IsNull(colB.ListView);
        Assert.IsNull(colC.ListView);
        Assert.That((object?)colA.Index, Is.EqualTo(-1));
        Assert.That((object?)colB.Index, Is.EqualTo(-1));
        Assert.That((object?)colC.Index, Is.EqualTo(-1));
    }

    [Test]
    public void ColumnHeaderCollectionTest_RemoveAt()
    {
        var listview = new ListView();
        var colA = new ColumnHeader();
        var colB = new ColumnHeader();
        var colC = new ColumnHeader();
        listview.Columns.Add(colA);
        listview.Columns.Add(colB);
        listview.Columns.Add(colC);

        listview.Columns.RemoveAt(1);
        Assert.That((object?)listview.Columns.Count, Is.EqualTo(2));
        Assert.That((object?)listview.Columns[0], Is.SameAs(colA));
        Assert.That((object?)listview.Columns[1], Is.SameAs(colC));
        Assert.That((object?)colA.ListView, Is.SameAs(listview));
        Assert.IsNull(colB.ListView);
        Assert.That((object?)colC.ListView, Is.SameAs(listview));

        listview.Columns.RemoveAt(0);
        Assert.That((object?)listview.Columns.Count, Is.EqualTo(1));
        Assert.That((object?)listview.Columns[0], Is.SameAs(colC));
        Assert.IsNull(colA.ListView);
        Assert.IsNull(colB.ListView);
        Assert.That((object?)colC.ListView, Is.SameAs(listview));

        listview.Columns.RemoveAt(0);
        Assert.That((object?)listview.Columns.Count, Is.EqualTo(0));
        Assert.IsNull(colA.ListView);
        Assert.IsNull(colB.ListView);
        Assert.IsNull(colC.ListView);
    }

    /*
        CheckedIndexCollection
    */
    [Test]
    public void CheckedIndexCollectionTest_PropertiesTest()
    {
        var listview = new ListView();

        // Properties
        Assert.That((object?)((ICollection)listview.CheckedIndices).IsSynchronized, Is.EqualTo(false), "CheckedIndexCollectionTest_PropertiesTest#2");
        Assert.That(((ICollection)listview.CheckedIndices).SyncRoot, Is.EqualTo(listview.CheckedIndices), "CheckedIndexCollectionTest_PropertiesTest#3");
        Assert.That((object?)((IList)listview.CheckedIndices).IsFixedSize, Is.EqualTo(true), "CheckedIndexCollectionTest_PropertiesTest#4");
        Assert.That((object?)listview.CheckedIndices.Count, Is.EqualTo(0), "CheckedIndexCollectionTest_PropertiesTest#5");
    }

    [Test]
    public void CheckedItemCollectionTest_PropertiesTest()
    {
        var listview = new ListView();

        // Properties
        Assert.That((object?)((ICollection)listview.CheckedItems).IsSynchronized, Is.EqualTo(false), "CheckedItemCollectionTest_PropertiesTest#2");
        Assert.That(((ICollection)listview.CheckedItems).SyncRoot, Is.EqualTo(listview.CheckedItems), "CheckedItemCollectionTest_PropertiesTest#3");
        Assert.That((object?)((IList)listview.CheckedItems).IsFixedSize, Is.EqualTo(true), "CheckedItemCollectionTest_PropertiesTest#4");
        Assert.That((object?)listview.CheckedItems.Count, Is.EqualTo(0), "CheckedItemCollectionTest_PropertiesTest#5");
    }


    [Test]
    public void CheckedItemCollectionTest_Order()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var lvw = new ListView();
        lvw.CheckBoxes = true;
        form.Controls.Add(lvw);
        var itemA = lvw.Items.Add("A");
        itemA.Checked = true;
        var itemB = lvw.Items.Add("B");
        itemB.Checked = true;
        var itemC = lvw.Items.Add("C");
        itemC.Checked = true;

        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(3));
        Assert.That((object?)lvw.CheckedItems[0], Is.SameAs(itemA));
        Assert.That((object?)lvw.CheckedItems[1], Is.SameAs(itemB));
        Assert.That((object?)lvw.CheckedItems[2], Is.SameAs(itemC));

        itemB.Checked = false;

        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(2));
        Assert.That((object?)lvw.CheckedItems[0], Is.SameAs(itemA));
        Assert.That((object?)lvw.CheckedItems[1], Is.SameAs(itemC));

        itemB.Checked = true;

        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(3));
        Assert.That((object?)lvw.CheckedItems[0], Is.SameAs(itemA));
        Assert.That((object?)lvw.CheckedItems[1], Is.SameAs(itemB));
        Assert.That((object?)lvw.CheckedItems[2], Is.SameAs(itemC));

        lvw.Sorting = SortOrder.Descending;

        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(3));
        Assert.That((object?)lvw.CheckedItems[0], Is.SameAs(itemA));
        Assert.That((object?)lvw.CheckedItems[1], Is.SameAs(itemB));
        Assert.That((object?)lvw.CheckedItems[2], Is.SameAs(itemC));

        // sorting only takes effect when listview is created
        form.Show();

        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(3));
        Assert.That((object?)lvw.CheckedItems[0], Is.SameAs(itemC));
        Assert.That((object?)lvw.CheckedItems[1], Is.SameAs(itemB));
        Assert.That((object?)lvw.CheckedItems[2], Is.SameAs(itemA));
        form.Dispose();
    }

    [Test]
    public void SelectedIndexCollectionTest_PropertiesTest()
    {
        var listview = new ListView();

        // Properties
        Assert.That((object?)((IList)listview.SelectedIndices).IsFixedSize, Is.EqualTo(false), "SelectedIndexCollectionTest_PropertiesTest#4");
        Assert.That((object?)((ICollection)listview.SelectedIndices).IsSynchronized, Is.EqualTo(false), "SelectedIndexCollectionTest_PropertiesTest#2");
        Assert.That(((ICollection)listview.SelectedIndices).SyncRoot, Is.EqualTo(listview.SelectedIndices), "SelectedIndexCollectionTest_PropertiesTest#3");
        Assert.That((object?)listview.SelectedIndices.Count, Is.EqualTo(0), "SelectedIndexCollectionTest_PropertiesTest#5");
    }

    [Test]
    public void SelectedIndexCollectionTest_ClearTest()
    {
        var listview = new ListView();
        listview.Items.Add("A");
        listview.Items.Add("B");
        listview.Items.Add("C");

        listview.SelectedIndices.Add(0);
        listview.SelectedIndices.Add(2);

        // Nothing if handle hasn't been created
        listview.SelectedIndices.Clear();
        Assert.That((object?)listview.Items[0].Selected, Is.EqualTo(true), "SelectedIndexCollectionTest_ClearTest#2");
        Assert.That((object?)listview.Items[1].Selected, Is.EqualTo(false), "SelectedIndexCollectionTest_ClearTest#3");
        Assert.That((object?)listview.Items[2].Selected, Is.EqualTo(true), "SelectedIndexCollectionTest_ClearTest#4");

        // Force to create the handle
        listview.CreateControl();

        listview.SelectedIndices.Add(0);
        listview.SelectedIndices.Add(2);

        listview.SelectedIndices.Clear();
        Assert.That((object?)listview.SelectedIndices.Count, Is.EqualTo(0), "SelectedIndexCollectionTest_ClearTest#5");
        Assert.That((object?)listview.Items[0].Selected, Is.EqualTo(false), "SelectedIndexCollectionTest_ClearTest#6");
        Assert.That((object?)listview.Items[1].Selected, Is.EqualTo(false), "SelectedIndexCollectionTest_ClearTest#7");
        Assert.That((object?)listview.Items[2].Selected, Is.EqualTo(false), "SelectedIndexCollectionTest_ClearTest#8");
        listview.Dispose();
    }

    [Test]
    public void SelectedIndexCollectionTest_ClearTest_VirtualMode()
    {
        var listview = new ListView();
        CreateVirtualItems(3);

        // Force to create the handle
        listview.CreateControl();

        listview.SelectedIndices.Add(2);
        listview.SelectedIndices.Add(0);

        Assert.That((object?)listview.SelectedIndices.Count, Is.EqualTo(2), "SelectedIndexCollectionTest_ClearTest#1");
        Assert.That((object?)listview.SelectedIndices[0], Is.EqualTo(0), "SelectedIndexCollectionTest_ClearTest#2");
        Assert.That((object?)listview.SelectedIndices[1], Is.EqualTo(2), "SelectedIndexCollectionTest_ClearTest#3");
        Assert.That((object?)listview.Items[0].Selected, Is.EqualTo(true), "SelectedIndexCollectionTest_ClearTest#4");
        Assert.That((object?)listview.Items[1].Selected, Is.EqualTo(false), "SelectedIndexCollectionTest_ClearTest#5");
        Assert.That((object?)listview.Items[2].Selected, Is.EqualTo(true), "SelectedIndexCollectionTest_ClearTest#6");

        listview.SelectedIndices.Clear();
        Assert.That((object?)listview.SelectedIndices.Count, Is.EqualTo(0), "SelectedIndexCollectionTest_ClearTest#5");
        Assert.That((object?)listview.Items[0].Selected, Is.EqualTo(false), "SelectedIndexCollectionTest_ClearTest#6");
        Assert.That((object?)listview.Items[1].Selected, Is.EqualTo(false), "SelectedIndexCollectionTest_ClearTest#7");
        Assert.That((object?)listview.Items[2].Selected, Is.EqualTo(false), "SelectedIndexCollectionTest_ClearTest#8");
        listview.Dispose();
    }

    [Test]
    public void SelectedIndexCollectionTest_IndexOfTest()
    {
        var listview = new ListView();
        var item1 = listview.Items.Add("A");
        var item2 = listview.Items.Add("B");
        var item3 = listview.Items.Add("C");
        var item4 = listview.Items.Add("D");

        listview.SelectedIndices.Add(0);
        listview.SelectedIndices.Add(3);
        listview.SelectedIndices.Add(2);

        Assert.That((object?)listview.SelectedIndices.Count, Is.EqualTo(0), "SelectedIndexCollectionTest_IndexOfTest#1");
        Assert.That((object?)listview.SelectedIndices.IndexOf(item1.Index), Is.EqualTo(-1), "SelectedIndexCollectionTest_IndexOfTest#2");
        Assert.That((object?)listview.SelectedIndices.IndexOf(item3.Index), Is.EqualTo(-1), "SelectedIndexCollectionTest_IndexOfTest#3");
        Assert.That((object?)listview.SelectedIndices.IndexOf(item4.Index), Is.EqualTo(-1), "SelectedIndexCollectionTest_IndexOfTest#4");
        Assert.That((object?)listview.SelectedIndices.IndexOf(item2.Index), Is.EqualTo(-1), "SelectedIndexCollectionTest_IndexOfTest#5");

        // Force to create the control
        listview.CreateControl();

        Assert.That((object?)listview.SelectedIndices.Count, Is.EqualTo(3), "SelectedIndexCollectionTest_IndexOfTest#6");
        Assert.That((object?)listview.SelectedIndices.IndexOf(item1.Index), Is.EqualTo(0), "SelectedIndexCollectionTest_IndexOfTest#7");
        Assert.That((object?)listview.SelectedIndices.IndexOf(item3.Index), Is.EqualTo(1), "SelectedIndexCollectionTest_IndexOfTest#8");
        Assert.That((object?)listview.SelectedIndices.IndexOf(item4.Index), Is.EqualTo(2), "SelectedIndexCollectionTest_IndexOfTest#9");
        Assert.That((object?)listview.SelectedIndices.IndexOf(item2.Index), Is.EqualTo(-1), "SelectedIndexCollectionTest_IndexOfTest#10");
        listview.Dispose();
    }

    [Test]
    public void SelectedIndexCollectionTest_IndexOfTest_VirtualMode()
    {
        var listview = new ListView();
        CreateVirtualItems(4);

        // Force to create the handle
        listview.CreateControl();

        listview.SelectedIndices.Add(0);
        listview.SelectedIndices.Add(3);
        listview.SelectedIndices.Add(2);

        Assert.That((object?)listview.SelectedIndices.Count, Is.EqualTo(3), "SelectedIndexCollectionTest_IndexOfTest#1");
        Assert.That((object?)listview.SelectedIndices.IndexOf(0), Is.EqualTo(0), "SelectedIndexCollectionTest_IndexOfTest#2");
        Assert.That((object?)listview.SelectedIndices.IndexOf(2), Is.EqualTo(1), "SelectedIndexCollectionTest_IndexOfTest#3");
        Assert.That((object?)listview.SelectedIndices.IndexOf(3), Is.EqualTo(2), "SelectedIndexCollectionTest_IndexOfTest#4");
        Assert.That((object?)listview.SelectedIndices.IndexOf(1), Is.EqualTo(-1), "SelectedIndexCollectionTest_IndexOfTest#5");
        Assert.That((object?)listview.SelectedIndices.IndexOf(99), Is.EqualTo(-1), "SelectedIndexCollectionTest_IndexOfTest#6");
        Assert.That((object?)listview.SelectedIndices.IndexOf(-1), Is.EqualTo(-1), "SelectedIndexCollectionTest_IndexOfTest#7");
        listview.Dispose();
    }

    [Test]
    public void SelectedIndexCollectionTest_RemoveTest()
    {
        var listview = new ListView();
        listview.Items.Add("A");

        listview.SelectedIndices.Add(0);
        listview.SelectedIndices.Remove(0);
        Assert.That((object?)listview.SelectedIndices.Count, Is.EqualTo(0), "SelectedIndexCollectionTest_RemoveTest#1");
        Assert.That((object?)listview.Items[0].Selected, Is.EqualTo(false), "SelectedIndexCollectionTest_RemoveTest#2");

        // Force to create the handle
        listview.CreateControl();

        listview.SelectedIndices.Add(0);
        listview.SelectedIndices.Remove(0);
        Assert.That((object?)listview.SelectedIndices.Count, Is.EqualTo(0), "SelectedIndexCollectionTest_RemoveTest#3");
        Assert.That((object?)listview.Items[0].Selected, Is.EqualTo(false), "SelectedIndexCollectionTest_RemoveTest#4");
        listview.Dispose();
    }

    [Test]
    public void SelectedIndexCollectionTest_RemoveTest_VirtualMode()
    {
        var listview = new ListView();
        CreateVirtualItems(5);

        // Force to create the handle
        listview.CreateControl();

        listview.SelectedIndices.Add(0);
        listview.SelectedIndices.Add(2);
        listview.SelectedIndices.Add(4);

        Assert.That((object?)listview.SelectedIndices.Count, Is.EqualTo(3), "SelectedIndexCollectionTest_RemoveTest#1");
        Assert.That((object?)listview.Items[0].Selected, Is.EqualTo(true), "SelectedIndexCollectionTest_RemoveTest#2");
        Assert.That((object?)listview.Items[2].Selected, Is.EqualTo(true), "SelectedIndexCollectionTest_RemoveTest#3");
        Assert.That((object?)listview.Items[4].Selected, Is.EqualTo(true), "SelectedIndexCollectionTest_RemoveTest#4");
        Assert.That((object?)listview.SelectedIndices[0], Is.EqualTo(0), "SelectedIndexCollectionTest_RemoveTest#5");
        Assert.That((object?)listview.SelectedIndices[1], Is.EqualTo(2), "SelectedIndexCollectionTest_RemoveTest#6");
        Assert.That((object?)listview.SelectedIndices[2], Is.EqualTo(4), "SelectedIndexCollectionTest_RemoveTest#7");

        listview.SelectedIndices.Remove(2);
        Assert.That((object?)listview.SelectedIndices.Count, Is.EqualTo(2), "SelectedIndexCollectionTest_RemoveTest#8");
        Assert.That((object?)listview.Items[2].Selected, Is.EqualTo(false), "SelectedIndexCollectionTest_RemoveTest#9");
        Assert.That((object?)listview.SelectedIndices[0], Is.EqualTo(0), "SelectedIndexCollectionTest_RemoveTest#10");
        Assert.That((object?)listview.SelectedIndices[1], Is.EqualTo(4), "SelectedIndexCollectionTest_RemoveTest#11");

        listview.SelectedIndices.Remove(0);
        Assert.That((object?)listview.SelectedIndices.Count, Is.EqualTo(1), "SelectedIndexCollectionTest_RemoveTest#12");
        Assert.That((object?)listview.Items[0].Selected, Is.EqualTo(false), "SelectedIndexCollectionTest_RemoveTest#13");
        Assert.That((object?)listview.SelectedIndices[0], Is.EqualTo(4), "SelectedIndexCollectionTest_RemoveTest#14");

        listview.SelectedIndices.Remove(4);
        Assert.That((object?)listview.SelectedIndices.Count, Is.EqualTo(0), "SelectedIndexCollectionTest_RemoveTest#15");
        Assert.That((object?)listview.Items[4].Selected, Is.EqualTo(false), "SelectedIndexCollectionTest_RemoveTest#16");

        // Remove an already removed index
        listview.SelectedIndices.Remove(0);
        Assert.That((object?)listview.SelectedIndices.Count, Is.EqualTo(0), "SelectedIndexCollectionTest_RemoveTest#17");
        Assert.That((object?)listview.Items[0].Selected, Is.EqualTo(false), "SelectedIndexCollectionTest_RemoveTest#18");
        listview.Dispose();
    }

    // Exceptions

    [Test]
    public void SelectedIndexCollectionTest_Add_ExceptionTest()
    {
        var listview = new ListView();
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            listview.SelectedIndices.Add(-1);
        });

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            listview.SelectedIndices.Add(listview.Items.Count);
        });
    }

    /*
        SelectedItemCollection
    */
    [Test]
    public void SelectedItemCollectionTest_PropertiesTest()
    {
        var listview = new ListView();

        // Properties
        Assert.That((object?)((ICollection)listview.SelectedItems).IsSynchronized, Is.EqualTo(false), "SelectedItemCollectionTest_PropertiesTest#2");
        Assert.That(((ICollection)listview.SelectedItems).SyncRoot, Is.EqualTo(listview.SelectedItems), "SelectedItemCollectionTest_PropertiesTest#3");
        Assert.That((object?)((IList)listview.SelectedItems).IsFixedSize, Is.EqualTo(true), "SelectedItemCollectionTest_PropertiesTest#4");
        Assert.That((object?)listview.SelectedItems.Count, Is.EqualTo(0), "SelectedItemCollectionTest_PropertiesTest#5");
    }


    [Test]
    public void SelectedItemCollectionTest_Clear()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var lvw = new ListView();
        form.Controls.Add(lvw);
        var item = lvw.Items.Add("Title");
        item.Selected = true;

        lvw.SelectedItems.Clear();

        Assert.IsTrue(item.Selected);
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(0));
        Assert.IsFalse(lvw.SelectedItems.Contains(item));

        form.Show();

        Assert.IsTrue(item.Selected);
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(1));
        Assert.IsTrue(lvw.SelectedItems.Contains(item));

        // once listview is created, clear DOES have effect
        lvw.SelectedItems.Clear();

        Assert.IsFalse(item.Selected);
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(0));
        Assert.IsFalse(lvw.SelectedItems.Contains(item));
        form.Dispose();
    }

    [Test]
    public void SelectedItemCollectionTest_Contains()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var lvw = new ListView();
        form.Controls.Add(lvw);
        var item = lvw.Items.Add("Title");
        item.Selected = true;
        var list = (IList)lvw.SelectedItems;

        Assert.IsFalse(lvw.SelectedItems.Contains(item));
        Assert.IsFalse(lvw.SelectedItems.Contains(new ListViewItem()));
        Assert.IsFalse(list.Contains(item));
        Assert.IsFalse(list.Contains(new ListViewItem()));

        form.Show();

        Assert.IsTrue(lvw.SelectedItems.Contains(item));
        Assert.IsFalse(lvw.SelectedItems.Contains(new ListViewItem()));
        Assert.IsTrue(list.Contains(item));
        Assert.IsFalse(list.Contains(new ListViewItem()));
        form.Dispose();
    }

    [Test]
    public void SelectedItemCollectionTest_CopyTo()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var lvw = new ListView();
        form.Controls.Add(lvw);
        var item = lvw.Items.Add("Title");
        item.Selected = true;
        var list = (IList)lvw.SelectedItems;
        Assert.IsNotNull(list);
        ListViewItem[] items = new ListViewItem[1];

        lvw.SelectedItems.CopyTo(items, 0);
        Assert.IsNull(items[0]);
        lvw.SelectedItems.CopyTo(items, 455);

        form.Show();

        lvw.SelectedItems.CopyTo(items, 0);
        Assert.That((object?)items[0], Is.SameAs(item));
        Assert.Throws<ArgumentException>(() =>
        {
            lvw.SelectedItems.CopyTo(items, 455);
        });
        form.Dispose();
    }

    [Test]
    public void SelectedItemCollectionTest_Count()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var lvw = new ListView();
        form.Controls.Add(lvw);
        var item = lvw.Items.Add("Title");
        item.Selected = true;

        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(0));
        form.Show();
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(1));
        form.Dispose();
    }

    [Test]
    public void SelectedItemCollectionTest_GetEnumerator()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var lvw = new ListView();
        form.Controls.Add(lvw);
        var item = lvw.Items.Add("Title");
        item.Selected = true;

        Assert.IsFalse(lvw.SelectedItems.GetEnumerator().MoveNext());

        form.Show();

        Assert.IsTrue(lvw.SelectedItems.GetEnumerator().MoveNext());

        form.Dispose();
    }

    [Test]
    public void SelectedItemCollectionTest_Indexer()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var lvw = new ListView();
        form.Controls.Add(lvw);
        var item = lvw.Items.Add("Title");
        item.Selected = true;
        var list = (IList)lvw.SelectedItems;

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var x = lvw.SelectedItems[0];
        });

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var x = list[0] as ListViewItem;
        });

        form.Show();

        Assert.That((object?)lvw.SelectedItems[0], Is.SameAs(item));
        Assert.That(list[0], Is.SameAs(item));

        form.Dispose();
    }

    [Test]
    public void SelectedItemCollectionTest_IndexOf()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var lvw = new ListView();
        form.Controls.Add(lvw);
        var item = lvw.Items.Add("Title");
        item.Selected = true;
        var list = (IList)lvw.SelectedItems;

        Assert.That((object?)lvw.SelectedItems.IndexOf(item), Is.EqualTo(-1));
        Assert.That((object?)lvw.SelectedItems.IndexOf(new ListViewItem()), Is.EqualTo(-1));
        Assert.That((object?)list.IndexOf(item), Is.EqualTo(-1));
        Assert.That((object?)list.IndexOf(new ListViewItem()), Is.EqualTo(-1));

        form.Show();

        Assert.That((object?)lvw.SelectedItems.IndexOf(item), Is.EqualTo(0));
        Assert.That((object?)lvw.SelectedItems.IndexOf(new ListViewItem()), Is.EqualTo(-1));
        Assert.That((object?)list.IndexOf(item), Is.EqualTo(0));
        Assert.That((object?)list.IndexOf(new ListViewItem()), Is.EqualTo(-1));

        form.Dispose();
    }

    [Test]
    public void SelectedItemCollectionTest_IndexOfKey()
    {
        var lvw = new ListView();
        var lvi1 = new ListViewItem("A")
        {
            Name = "A name",
            Selected = true
        };
        var lvi2 = new ListViewItem("B")
        {
            Name = "Same name",
            Selected = false
        };
        var lvi3 = new ListViewItem("C")
        {
            Name = "Same name",
            Selected = true
        };
        var lvi4 = new ListViewItem("D")
        {
            Name = string.Empty,
            Selected = true
        };
        var lvi5 = new ListViewItem("E")
        {
            Name = "E name",
            Selected = false
        };
        lvw.Items.AddRange([lvi1, lvi2, lvi3, lvi4, lvi5]);

        // Force to create the control
        lvw.CreateControl();

        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(3));

        var lvi6 = new ListViewItem("F");
        lvw.Items.Add(lvi6);
        lvi6.Selected = true;
        lvi6.Name = "F name";

        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(4));
        lvw.Dispose();
    }

    [Test]
    public void SelectedItemCollectionTest_Indexer2()
    {
        var lvw = new ListView();
        var lvi1 = new ListViewItem("A")
        {
            Name = "A name",
            Selected = true
        };
        var lvi2 = new ListViewItem("B")
        {
            Name = "Same name",
            Selected = false
        };
        var lvi3 = new ListViewItem("C")
        {
            Name = "Same name",
            Selected = true
        };
        var lvi4 = new ListViewItem("D")
        {
            Name = string.Empty,
            Selected = true
        };
        var lvi5 = new ListViewItem("E")
        {
            Name = "E name",
            Selected = false
        };
        lvw.Items.AddRange([lvi1, lvi2, lvi3, lvi4, lvi5]);

        // Force to create the control
        lvw.CreateControl();

        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(3));

        var lvi6 = new ListViewItem("F");
        lvw.Items.Add(lvi6);
        lvi6.Selected = true;
        lvi6.Name = "F name";

        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(4));
        lvw.Dispose();
    }

    [Test]
    public void SelectedItemCollectionTest_Order()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var lvw = new ListView();
        lvw.MultiSelect = true;
        form.Controls.Add(lvw);
        var itemA = lvw.Items.Add("A");
        itemA.Selected = true;
        var itemB = lvw.Items.Add("B");
        itemB.Selected = true;
        var itemC = lvw.Items.Add("C");
        itemC.Selected = true;

        form.Show();

        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(3));
        Assert.That((object?)lvw.SelectedItems[0], Is.SameAs(itemA));
        Assert.That((object?)lvw.SelectedItems[1], Is.SameAs(itemB));
        Assert.That((object?)lvw.SelectedItems[2], Is.SameAs(itemC));

        itemB.Selected = false;

        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(2));
        Assert.That((object?)lvw.SelectedItems[0], Is.SameAs(itemA));
        Assert.That((object?)lvw.SelectedItems[1], Is.SameAs(itemC));

        itemB.Selected = true;

        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(3));
        Assert.That((object?)lvw.SelectedItems[0], Is.SameAs(itemA));
        Assert.That((object?)lvw.SelectedItems[1], Is.SameAs(itemB));
        Assert.That((object?)lvw.SelectedItems[2], Is.SameAs(itemC));

        lvw.Sorting = SortOrder.Descending;

        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(3));
        Assert.That((object?)lvw.SelectedItems[0], Is.SameAs(itemC));
        Assert.That((object?)lvw.SelectedItems[1], Is.SameAs(itemB));
        Assert.That((object?)lvw.SelectedItems[2], Is.SameAs(itemA));

        form.Dispose();
    }

    /*
        ListViewItemCollection
    */

    [Test]
    public void ListViewItemCollectionTest_Add_Group()
    {
        var lvw = new ListView();
        var lvg = new ListViewGroup();
        var lvi = new ListViewItem("A");

        lvg.Items.Add(lvi);
        Assert.That((object?)lvi.Group, Is.EqualTo(lvg));
        Assert.That((object?)lvg.Items.Count, Is.EqualTo(1));

        lvw.Groups.Add(lvg);
        Assert.That((object?)lvi.ListView, Is.EqualTo(null));

        lvw.Items.Clear();
        lvw.Groups.Clear();
        lvg.Items.Clear();

        lvw.Groups.Add(lvg);
        lvg.Items.Add(lvi);
        Assert.That((object?)lvi.ListView, Is.EqualTo(null));

        lvw.Items.Clear();
        lvw.Groups.Clear();
        lvg.Items.Clear();

        // Adding the ListViewItem to the ListView.Items collection
        // first throws an exception.
        Assert.Throws<ArgumentException>(() =>
        {
            lvw.Items.Add(lvi);
            lvg.Items.Add(lvi);
        });

        lvw.Items.Clear();
        lvw.Groups.Clear();
        lvg.Items.Clear();

        // The right order is: first add to the ListViewGroup.Items
        // collection, then to the ListView.Items collection,
        // OR add first add the group to the ListView and then the order
        // of item adding doesn't matter
        lvw.Groups.Add(lvg);
        lvg.Items.Add(lvi);
        Assert.That((object?)lvi.ListView, Is.EqualTo(null));
        lvw.Items.Add(lvi);
        Assert.That((object?)lvw.Items.Count, Is.EqualTo(1));
        Assert.That((object?)lvg.Items.Count, Is.EqualTo(1));
        Assert.That((object?)lvi.ListView, Is.EqualTo(lvw));

        lvg.Items.Clear();
        lvw.Items.Clear();
        lvw.Groups.Clear();

        // Adding the already added ListViewItem to
        // a different group should remove it from the previous one
        lvg.Items.Add(lvi);
        Assert.That((object?)lvg.Items.Count, Is.EqualTo(1));
        Assert.That((object?)lvi.Group, Is.EqualTo(lvg));

        var lvg2 = new ListViewGroup();
        lvg2.Items.Add(lvi);

        Assert.That((object?)lvg.Items.Count, Is.EqualTo(0));
        Assert.That((object?)lvg2.Items.Count, Is.EqualTo(1));
        Assert.That((object?)lvi.Group, Is.EqualTo(lvg2));
    }

    [Test]
    public void ListViewItemCollectionTest_Add_Junk()
    {
        var lv1 = new ListView();

        var item4 = lv1.Items.Add("Item4", 4);
        Assert.That((object?)lv1.Items[0], Is.EqualTo(item4));
        object expected = string.Empty;
        Assert.That((object?)lv1.Items[0].Name, Is.EqualTo(expected));
        Assert.That((object?)lv1.Items[0].Text, Is.EqualTo("Item4"));
        Assert.That((object?)lv1.Items[0].ImageIndex, Is.EqualTo(4));

        string? text = null;
        var item5 = lv1.Items.Add(text);
        Assert.That((object?)lv1.Items[1], Is.EqualTo(item5));
        object expected1 = string.Empty;
        Assert.That((object?)lv1.Items[1].Name, Is.EqualTo(expected1));
        object expected2 = string.Empty;
        Assert.That((object?)lv1.Items[1].Text, Is.EqualTo(expected2));

        var item6 = lv1.Items.Add(null, 5);
        Assert.That((object?)lv1.Items[2], Is.EqualTo(item6));
        object expected3 = string.Empty;
        Assert.That((object?)lv1.Items[2].Name, Is.EqualTo(expected3));
        object expected4 = string.Empty;
        Assert.That((object?)lv1.Items[2].Text, Is.EqualTo(expected4));
        Assert.That((object?)lv1.Items[2].ImageIndex, Is.EqualTo(5));
        var item1 = lv1.Items.Add("ItemKey1", "Item1", 1);
        Assert.That((object?)lv1.Items[3], Is.EqualTo(item1));
        Assert.That((object?)lv1.Items[3].Name, Is.EqualTo("ItemKey1"));
        Assert.That((object?)lv1.Items[3].Text, Is.EqualTo("Item1"));
        Assert.That((object?)lv1.Items[3].ImageIndex, Is.EqualTo(1));

        var item2 = lv1.Items.Add("ItemKey2", "Item2", "Image2");
        Assert.That((object?)lv1.Items[4], Is.EqualTo(item2));
        Assert.That((object?)lv1.Items[4].Name, Is.EqualTo("ItemKey2"));
        Assert.That((object?)lv1.Items[4].Text, Is.EqualTo("Item2"));
        Assert.That((object?)lv1.Items[4].ImageKey, Is.EqualTo("Image2"));

        var item3 = lv1.Items.Add("Item3", "Image3");
        Assert.That((object?)lv1.Items[5], Is.EqualTo(item3));
        object expected5 = string.Empty;
        Assert.That((object?)lv1.Items[5].Name, Is.EqualTo(expected5));
        Assert.That((object?)lv1.Items[5].Text, Is.EqualTo("Item3"));
        Assert.That((object?)lv1.Items[5].ImageKey, Is.EqualTo("Image3"));

        var item7 = lv1.Items.Add(null!, "Item6", 6);
        Assert.That((object?)lv1.Items[6], Is.EqualTo(item7));
        object expected6 = string.Empty;
        Assert.That((object?)lv1.Items[6].Name, Is.EqualTo(expected6));
        Assert.That((object?)lv1.Items[6].Text, Is.EqualTo("Item6"));
        Assert.That((object?)lv1.Items[6].ImageIndex, Is.EqualTo(6));

        var item8 = lv1.Items.Add("ItemKey7", null, 7);
        Assert.That((object?)lv1.Items[7], Is.EqualTo(item8));
        Assert.That((object?)lv1.Items[7].Name, Is.EqualTo("ItemKey7"));
        object expected7 = string.Empty;
        Assert.That((object?)lv1.Items[7].Text, Is.EqualTo(expected7));
        Assert.That((object?)lv1.Items[7].ImageIndex, Is.EqualTo(7));

        var item9 = lv1.Items.Add("ItemKey8", "Item8", null);
        Assert.That((object?)lv1.Items[8], Is.EqualTo(item9));
        Assert.That((object?)lv1.Items[8].Name, Is.EqualTo("ItemKey8"));
        Assert.That((object?)lv1.Items[8].Text, Is.EqualTo("Item8"));
        object expected8 = string.Empty;
        Assert.That((object?)lv1.Items[8].ImageKey, Is.EqualTo(expected8));
    }

    [Test]
    public void ListViewItemCollectionTest_AddRange()
    {
        var lv1 = new ListView();
        var item1 = new ListViewItem("Item1");
        var item2 = new ListViewItem("Item2");
        var item3 = new ListViewItem("Item3");
        lv1.Items.AddRange([item1, item2, item3]);

        Assert.That((object?)lv1.Items[0], Is.SameAs(item1));
        Assert.That((object?)item1.Index, Is.EqualTo(0));
        Assert.That((object?)item1.ListView, Is.SameAs(lv1));

        Assert.That((object?)lv1.Items[1], Is.SameAs(item2));
        Assert.That((object?)item2.Index, Is.EqualTo(1));
        Assert.That((object?)item2.ListView, Is.SameAs(lv1));

        Assert.That((object?)lv1.Items[2], Is.SameAs(item3));
        Assert.That((object?)item3.Index, Is.EqualTo(2));
        Assert.That((object?)item3.ListView, Is.SameAs(lv1));
    }

    [Test]
    public void ListViewItemCollectionTest_AddRange_Count()
    {
        var lv1 = new ListView();
        var item1 = new ListViewItem("Item1");
        var item2 = new ListViewItem("Item2");
        var item3 = new ListViewItem("Item3");

        lv1.Items.Add("Item4");
        Assert.That((object?)lv1.Items.Count, Is.EqualTo(1));
        lv1.Items.AddRange([item1, item2, item3]);
        Assert.That((object?)lv1.Items.Count, Is.EqualTo(4));
    }

    [Test]
    public void ListViewItemCollectionTest_Clear()
    {
        var lvw = new ListView();
        var itemA = lvw.Items.Add("A");
        var itemB = lvw.Items.Add("B");

        Assert.That((object?)lvw.Items.Count, Is.EqualTo(2));
        Assert.That((object?)itemA.ListView, Is.SameAs(lvw));
        Assert.That((object?)itemB.ListView, Is.SameAs(lvw));

        lvw.Items.Clear();

        Assert.That((object?)lvw.Items.Count, Is.EqualTo(0));
        Assert.IsNull(itemA.ListView);
        Assert.IsNull(itemB.ListView);
    }
    [Test]
    public void ListViewItemCollectionTest_Clear_Groups()
    {
        var lvg = new ListViewGroup();
        var itemA = new ListViewItem();
        var itemB = new ListViewItem();
        var itemC = new ListViewItem();

        lvg.Items.Add(itemA);
        lvg.Items.Add(itemB);
        lvg.Items.Add(itemC);

        Assert.That((object?)lvg.Items.Count, Is.EqualTo(3));
        Assert.That((object?)itemA.Group, Is.EqualTo(lvg));
        Assert.That((object?)itemB.Group, Is.EqualTo(lvg));
        Assert.That((object?)itemC.Group, Is.EqualTo(lvg));

        var lvw = new ListView();
        lvw.Groups.Add(lvg);

        Assert.That((object?)lvg.Items.Count, Is.EqualTo(3));
        Assert.That((object?)lvw.Items.Count, Is.EqualTo(0));
        Assert.That((object?)itemA.ListView, Is.EqualTo(null));
        Assert.That((object?)itemB.ListView, Is.EqualTo(null));
        Assert.That((object?)itemC.ListView, Is.EqualTo(null));

        lvg.Items.Clear();
        Assert.That((object?)lvg.Items.Count, Is.EqualTo(0));
        Assert.That((object?)itemA.Group, Is.EqualTo(null));
        //Assert1.AreEqual(null, itemB.Group); // Bogus .Net impl
        Assert.That((object?)itemC.Group, Is.EqualTo(null));

        // Add items with Group contained within lvw.Groups
        lvg.Items.Add(itemA);
        lvg.Items.Add(itemB);
        lvg.Items.Add(itemC);

        Assert.That((object?)lvg.Items.Count, Is.EqualTo(3));
        Assert.That((object?)lvw.Items.Count, Is.EqualTo(0));
        Assert.That((object?)itemA.ListView, Is.EqualTo(null));
        Assert.That((object?)itemB.ListView, Is.EqualTo(null));
        Assert.That((object?)itemC.ListView, Is.EqualTo(null));

        lvg.Items.Clear();
        lvw.Groups.Clear();

        lvw.Groups.Add(lvg);
        lvg.Items.Add(itemA);
        lvg.Items.Add(itemB);
        lvw.Items.Add(itemA);
        lvw.Items.Add(itemB);

        Assert.That((object?)lvg.Items.Count, Is.EqualTo(2));
        Assert.That((object?)lvw.Items.Count, Is.EqualTo(2));
        Assert.That((object?)itemA.ListView, Is.EqualTo(lvw));
        Assert.That((object?)itemB.ListView, Is.EqualTo(lvw));

        lvg.Items.Clear();

        Assert.That((object?)lvg.Items.Count, Is.EqualTo(0));
        Assert.That((object?)lvw.Items.Count, Is.EqualTo(2));
        Assert.That((object?)itemA.ListView, Is.EqualTo(lvw));
        Assert.That((object?)itemB.ListView, Is.EqualTo(lvw));
        Assert.That((object?)itemA.Group, Is.EqualTo(null));
        //Assert1.AreEqual(null, itemB.Group); // Bogus impl again
    }

    [Test]
    public void ListViewItemCollectionTest_Insert_Group()
    {
        var lvw = new ListView();
        var lvg = new ListViewGroup();
        var lvi = new ListViewItem("A");

        lvg.Items.Insert(0, lvi);
        Assert.That((object?)lvi.Group, Is.EqualTo(lvg));
        Assert.That((object?)lvg.Items.Count, Is.EqualTo(1));

        lvw.Groups.Add(lvg);
        Assert.That((object?)lvi.ListView, Is.EqualTo(null));

        lvw.Items.Clear();
        lvw.Groups.Clear();
        lvg.Items.Clear();

        lvw.Groups.Add(lvg);
        lvg.Items.Insert(0, lvi);
        Assert.That((object?)lvi.ListView, Is.EqualTo(null));

        lvw.Items.Clear();
        lvw.Groups.Clear();
        lvg.Items.Clear();

        // Adding the ListViewItem to the ListView.Items collection
        // first throws an exception.
        Assert.Throws<ArgumentException>(() =>
        {
            lvw.Items.Insert(0, lvi);
            lvg.Items.Insert(0, lvi);
        });

        lvw.Items.Clear();
        lvw.Groups.Clear();
        lvg.Items.Clear();

        // The right order is: first add to the ListViewGroup.Items
        // collection, then to the ListView.Items collection,
        // OR add first add the group to the ListView and then the order
        // of item adding doesn't matter
        lvw.Groups.Add(lvg);
        lvg.Items.Insert(0, lvi);
        Assert.That((object?)lvi.ListView, Is.EqualTo(null));
        lvw.Items.Insert(0, lvi);
        Assert.That((object?)lvw.Items.Count, Is.EqualTo(1));
        Assert.That((object?)lvg.Items.Count, Is.EqualTo(1));
        Assert.That((object?)lvi.ListView, Is.EqualTo(lvw));

        lvg.Items.Clear();
        lvw.Items.Clear();
        lvw.Groups.Clear();

        // Adding the already added ListViewItem to
        // a different group should remove it from the previous one
        lvg.Items.Insert(0, lvi);
        Assert.That((object?)lvg.Items.Count, Is.EqualTo(1));
        Assert.That((object?)lvi.Group, Is.EqualTo(lvg));

        var lvg2 = new ListViewGroup();
        lvg2.Items.Insert(0, lvi);

        Assert.That((object?)lvg.Items.Count, Is.EqualTo(0));
        Assert.That((object?)lvg2.Items.Count, Is.EqualTo(1));
        Assert.That((object?)lvi.Group, Is.EqualTo(lvg2));
    }

    [Test]
    public void ListViewItemCollectionTest_Remove()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var lvw = new ListView();
        form.Controls.Add(lvw);
        lvw.MultiSelect = true;
        lvw.CheckBoxes = true;

        form.Show();

        var itemA = lvw.Items.Add("A");
        var itemB = lvw.Items.Add("B");
        lvw.Items.Add("C");
        var itemD = lvw.Items.Add("D");

        Assert.That((object?)lvw.Items.Count, Is.EqualTo(4));
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(0));
        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(0));

        itemB.Checked = true;
        itemD.Checked = true;

        Assert.That((object?)lvw.Items.Count, Is.EqualTo(4));
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(0));
        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(2));
        Assert.That((object?)lvw.CheckedItems[0], Is.SameAs(itemB));
        Assert.That((object?)lvw.CheckedItems[1], Is.SameAs(itemD));

        itemD.Selected = true;

        Assert.That((object?)lvw.Items.Count, Is.EqualTo(4));
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedItems[0], Is.SameAs(itemD));
        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(2));
        Assert.That((object?)lvw.CheckedItems[0], Is.SameAs(itemB));
        Assert.That((object?)lvw.CheckedItems[1], Is.SameAs(itemD));

        lvw.Items.Remove(itemB);

        Assert.That((object?)lvw.Items.Count, Is.EqualTo(3));
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedItems[0], Is.SameAs(itemD));
        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedItems[0], Is.SameAs(itemD));

        lvw.Items.Remove(itemA);

        Assert.That((object?)lvw.Items.Count, Is.EqualTo(2));
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedItems[0], Is.EqualTo(itemD));
        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedItems[0], Is.EqualTo(itemD));

        Assert.IsNull(itemA.ListView);
        Assert.IsNull(itemB.ListView);
        Assert.That((object?)itemD.ListView, Is.SameAs(lvw));

        form.Dispose();
    }

    [Test]
    public void ListViewItemCollectionTest_Remove_Groups()
    {
        var lvg = new ListViewGroup();
        var itemA = new ListViewItem();

        lvg.Items.Add(itemA);

        Assert.That((object?)lvg.Items.Count, Is.EqualTo(1));
        lvg.Items.Remove(itemA);
        Assert.That((object?)lvg.Items.Count, Is.EqualTo(0));
        Assert.That((object?)itemA.Group, Is.EqualTo(null));

        lvg.Items.Add(itemA);
        var lvw = new ListView();
        lvw.Groups.Add(lvg);

        Assert.That((object?)lvg.Items.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.Items.Count, Is.EqualTo(0));
        Assert.That((object?)itemA.ListView, Is.EqualTo(null));

        lvg.Items.Remove(itemA);
        Assert.That((object?)lvg.Items.Count, Is.EqualTo(0));
        Assert.That((object?)itemA.Group, Is.EqualTo(null));

        // Add items with Group contained within lvw.Groups
        lvg.Items.Add(itemA);

        Assert.That((object?)lvg.Items.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.Items.Count, Is.EqualTo(0));
        Assert.That((object?)itemA.ListView, Is.EqualTo(null));
        Assert.That((object?)itemA.Group, Is.EqualTo(lvg));

        lvg.Items.Clear();
        lvw.Groups.Clear();

        lvw.Groups.Add(lvg);
        lvg.Items.Add(itemA);
        lvw.Items.Add(itemA);

        Assert.That((object?)lvg.Items.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.Items.Count, Is.EqualTo(1));
        Assert.That((object?)itemA.ListView, Is.EqualTo(lvw));
        Assert.That((object?)itemA.Group, Is.EqualTo(lvg));

        lvg.Items.Remove(itemA);

        Assert.That((object?)lvg.Items.Count, Is.EqualTo(0));
        Assert.That((object?)lvw.Items.Count, Is.EqualTo(1));
        Assert.That((object?)itemA.ListView, Is.EqualTo(lvw));
        Assert.That((object?)itemA.Group, Is.EqualTo(null));
    }

    [Test]
    public void ListViewItemCollectionTest_RemoveAt()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var lvw = new ListView();
        form.Controls.Add(lvw);
        lvw.MultiSelect = true;
        lvw.CheckBoxes = true;

        var itemA = lvw.Items.Add("A");
        var itemB = lvw.Items.Add("B");
        var itemC = lvw.Items.Add("C");
        var itemD = lvw.Items.Add("D");

        form.Show();

        Assert.That((object?)lvw.Items.Count, Is.EqualTo(4));
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(0));
        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(0));

        lvw.Items[1].Checked = true;
        lvw.Items[3].Checked = true;

        Assert.That((object?)lvw.Items.Count, Is.EqualTo(4));
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(0));
        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(2));
        Assert.That((object?)lvw.CheckedItems[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.CheckedItems[1].Text, Is.EqualTo("D"));

        lvw.Items[3].Selected = true;

        Assert.That((object?)lvw.Items.Count, Is.EqualTo(4));
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedItems[0].Text, Is.EqualTo("D"));
        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(2));
        Assert.That((object?)lvw.CheckedItems[0].Text, Is.EqualTo("B"));
        Assert.That((object?)lvw.CheckedItems[1].Text, Is.EqualTo("D"));

        lvw.Items.RemoveAt(1);

        Assert.That((object?)lvw.Items.Count, Is.EqualTo(3));
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedItems[0].Text, Is.EqualTo("D"));
        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedItems[0].Text, Is.EqualTo("D"));

        lvw.Items.RemoveAt(0);

        Assert.That((object?)lvw.Items.Count, Is.EqualTo(2));
        Assert.That((object?)lvw.SelectedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.SelectedItems[0].Text, Is.EqualTo("D"));
        Assert.That((object?)lvw.CheckedItems.Count, Is.EqualTo(1));
        Assert.That((object?)lvw.CheckedItems[0].Text, Is.EqualTo("D"));

        Assert.IsNull(itemA.ListView);
        Assert.IsNull(itemB.ListView);
        Assert.That((object?)itemC.ListView, Is.SameAs(lvw));
        Assert.That((object?)itemD.ListView, Is.SameAs(lvw));

        form.Dispose();
    }

    [Test]
    public void DeleteFromEnumerator()	// bug 425342
    {
        var lv = new ListView();

        lv.Items.Add("A");
        lv.Items.Add("B");
        lv.Items.Add("C");

        foreach (var lvi in lv.Items)
            if (lvi.Text == "B")
                lv.Items.Remove(lvi);

        Assert.That((object?)lv.Items.Count, Is.EqualTo(2));
    }

    [Test]
    public void ListViewItemCollectionTest_RemoveByKey()
    {
        var lvw = new ListView();
        var lvi1 = new ListViewItem("A")
        {
            Name = "A name"
        };
        var lvi2 = new ListViewItem("B")
        {
            Name = "B name"
        };
        var lvi3 = new ListViewItem("C")
        {
            Name = "Same name"
        };
        var lvi4 = new ListViewItem("D")
        {
            Name = "Same name"
        };
        var lvi5 = new ListViewItem("E")
        {
            Name = string.Empty
        };
        lvw.Items.AddRange([lvi1, lvi2, lvi3, lvi4, lvi5]);

        Assert.That((object?)lvw.Items.Count, Is.EqualTo(5));

        lvw.Items.RemoveByKey("B name");
        Assert.That((object?)lvw.Items.Count, Is.EqualTo(4));
        Assert.That((object?)lvw.Items[0], Is.SameAs(lvi1));
        Assert.That((object?)lvw.Items[1], Is.SameAs(lvi3));
        Assert.That((object?)lvw.Items[2], Is.SameAs(lvi4));
        Assert.That((object?)lvw.Items[3], Is.SameAs(lvi5));

        lvw.Items.RemoveByKey("Same name");
        Assert.That((object?)lvw.Items.Count, Is.EqualTo(3));
        Assert.That((object?)lvw.Items[0], Is.SameAs(lvi1));
        Assert.That((object?)lvw.Items[1], Is.SameAs(lvi4));
        Assert.That((object?)lvw.Items[2], Is.SameAs(lvi5));

        lvw.Items.RemoveByKey("a NAME");
        Assert.That((object?)lvw.Items.Count, Is.EqualTo(2));
        Assert.That((object?)lvw.Items[0], Is.SameAs(lvi4));
        Assert.That((object?)lvw.Items[1], Is.SameAs(lvi5));

        lvw.Items.RemoveByKey(string.Empty);
        Assert.That((object?)lvw.Items.Count, Is.EqualTo(2));
        Assert.That((object?)lvw.Items[0], Is.SameAs(lvi4));
        Assert.That((object?)lvw.Items[1], Is.SameAs(lvi5));

        Assert.IsNull(lvi1.ListView);
        Assert.IsNull(lvi2.ListView);
        Assert.IsNull(lvi3.ListView);
        Assert.That((object?)lvi4.ListView, Is.SameAs(lvw));
        Assert.That((object?)lvi5.ListView, Is.SameAs(lvw));
    }

    [Test]
    public void ListViewItemCollectionTest_IndexOfKey()
    {
        var lvw = new ListView();
        var lvi1 = new ListViewItem("A")
        {
            Name = "A name"
        };
        var lvi2 = new ListViewItem("B")
        {
            Name = "Same name"
        };
        var lvi3 = new ListViewItem("C")
        {
            Name = "Same name"
        };
        var lvi4 = new ListViewItem("D")
        {
            Name = string.Empty
        };
        lvw.Items.AddRange([lvi1, lvi2, lvi3, lvi4]);

        Assert.That((object?)lvw.Items.Count, Is.EqualTo(4));
        Assert.That((object?)lvw.Items.IndexOfKey(string.Empty), Is.EqualTo(-1));
        Assert.That((object?)lvw.Items.IndexOfKey(null!), Is.EqualTo(-1));
        Assert.That((object?)lvw.Items.IndexOfKey("A name"), Is.EqualTo(0));
        Assert.That((object?)lvw.Items.IndexOfKey("a NAME"), Is.EqualTo(0));
        Assert.That((object?)lvw.Items.IndexOfKey("Same name"), Is.EqualTo(1));

        var lvi5 = new ListViewItem("E");
        lvw.Items.Add(lvi5);
        lvi5.Name = "E name";

        Assert.That((object?)lvw.Items.IndexOfKey("E name"), Is.EqualTo(4));
    }

    [Test]
    public void ListViewItemCollectionTest_Indexer()
    {
        var lvw = new ListView();
        var lvi1 = new ListViewItem("A")
        {
            Name = "A name"
        };
        var lvi2 = new ListViewItem("B")
        {
            Name = "Same name"
        };
        var lvi3 = new ListViewItem("C")
        {
            Name = "Same name"
        };
        var lvi4 = new ListViewItem("D")
        {
            Name = string.Empty
        };
        lvw.Items.AddRange([lvi1, lvi2, lvi3, lvi4]);

        Assert.That((object?)lvw.Items.Count, Is.EqualTo(4));
        Assert.That((object?)lvw.Items[string.Empty], Is.EqualTo(null));
        Assert.That((object?)lvw.Items[null!], Is.EqualTo(null));
        Assert.That((object?)lvw.Items["A name"], Is.SameAs(lvi1));
        Assert.That((object?)lvw.Items["a NAME"], Is.SameAs(lvi1));
        Assert.That((object?)lvw.Items["Same name"], Is.SameAs(lvi2));

        var lvi5 = new ListViewItem("E");
        lvw.Items.Add(lvi5);
        lvi5.Name = "E name";

        Assert.That((object?)lvw.Items["E name"], Is.SameAs(lvi5));
    }

    [Test]
    public void ListViewItemCollectionTest_ContainsKey()
    {
        var lvw = new ListView();
        var lvi1 = new ListViewItem("A")
        {
            Name = "A name"
        };
        var lvi2 = new ListViewItem("B")
        {
            Name = "B name"
        };
        var lvi3 = new ListViewItem("D")
        {
            Name = string.Empty
        };
        lvw.Items.AddRange([lvi1, lvi2, lvi3]);

        Assert.That((object?)lvw.Items.Count, Is.EqualTo(3));
        Assert.That((object?)lvw.Items.ContainsKey(string.Empty), Is.EqualTo(false));
        Assert.That((object?)lvw.Items.ContainsKey(null!), Is.EqualTo(false));
        Assert.That((object?)lvw.Items.ContainsKey("A name"), Is.EqualTo(true));
        Assert.That((object?)lvw.Items.ContainsKey("a NAME"), Is.EqualTo(true));
        Assert.That((object?)lvw.Items.ContainsKey("B name"), Is.EqualTo(true));

        var lvi5 = new ListViewItem("E");
        lvw.Items.Add(lvi5);
        lvi5.Name = "E name";

        Assert.That((object?)lvw.Items.ContainsKey("E name"), Is.EqualTo(true));
    }

    [Test]
    public void ListViewItemCollectionTest_Find()
    {
        var lvw = new ListView();
        var lvi1 = new ListViewItem("A")
        {
            Name = "A name"
        };
        var lvi2 = new ListViewItem("B")
        {
            Name = "a NAME"
        };
        var lvi3 = new ListViewItem("C")
        {
            Name = "a NAME"
        };
        var lvi4 = new ListViewItem("D")
        {
            Name = string.Empty
        };
        var lvi5 = new ListViewItem("F")
        {
            Name = string.Empty
        };
        lvw.Items.AddRange([lvi1, lvi2, lvi3, lvi4, lvi5]);

        Assert.That((object?)lvw.Items.Count, Is.EqualTo(5));

        ListViewItem[] items = lvw.Items.Find("A name", false);
        Assert.That((object?)items.Length, Is.EqualTo(3));
        Assert.That((object?)items[0], Is.SameAs(lvi1));
        Assert.That((object?)items[1], Is.SameAs(lvi2));
        Assert.That((object?)items[2], Is.SameAs(lvi3));

        items = lvw.Items.Find(string.Empty, false);
        Assert.That((object?)items.Length, Is.EqualTo(2));
        Assert.That((object?)items[0], Is.SameAs(lvi4));
        Assert.That((object?)items[1], Is.SameAs(lvi5));

        Assert.That((object?)lvw.Items.Find(null!, false).Length, Is.EqualTo(0));
    }

    [Test]
    public void ListViewSubItemCollectionTest_ContainsKey()
    {
        var lvi = new ListViewItem("A");
        var si1 = new ListViewItem.ListViewSubItem
        {
            Name = "A name"
        };
        var si2 = new ListViewItem.ListViewSubItem
        {
            Name = "B name"
        };
        var si3 = new ListViewItem.ListViewSubItem
        {
            Name = string.Empty
        };
        lvi.SubItems!.AddRange([si1, si2, si3]);

        Assert.That((object?)lvi.SubItems.Count, Is.EqualTo(4));
        Assert.That((object?)lvi.SubItems.ContainsKey(string.Empty), Is.EqualTo(false));
        Assert.That((object?)lvi.SubItems.ContainsKey(null), Is.EqualTo(false));
        Assert.That((object?)lvi.SubItems.ContainsKey("A name"), Is.EqualTo(true));
        Assert.That((object?)lvi.SubItems.ContainsKey("a NAME"), Is.EqualTo(true));
        Assert.That((object?)lvi.SubItems.ContainsKey("B name"), Is.EqualTo(true));

        var si5 = new ListViewItem.ListViewSubItem();
        lvi.SubItems.Add(si5);
        si5.Name = "E name";

        Assert.That((object?)lvi.SubItems.ContainsKey("E name"), Is.EqualTo(true));
    }

    [Test]
    public void ListViewSubItemCollectionTest_IndexOfKey()
    {
        var lvi = new ListViewItem();
        var si1 = new ListViewItem.ListViewSubItem
        {
            Name = "A name"
        };
        var si2 = new ListViewItem.ListViewSubItem
        {
            Name = "Same name"
        };
        var si3 = new ListViewItem.ListViewSubItem
        {
            Name = "Same name"
        };
        var si4 = new ListViewItem.ListViewSubItem
        {
            Name = string.Empty
        };
        lvi.SubItems.AddRange([si1, si2, si3, si4]);

        Assert.That((object?)lvi.SubItems.Count, Is.EqualTo(5));
        Assert.That((object?)lvi.SubItems.IndexOfKey(string.Empty), Is.EqualTo(-1));
        Assert.That((object?)lvi.SubItems.IndexOfKey(null), Is.EqualTo(-1));
        Assert.That((object?)lvi.SubItems.IndexOfKey("A name"), Is.EqualTo(1));
        Assert.That((object?)lvi.SubItems.IndexOfKey("a NAME"), Is.EqualTo(1));
        Assert.That((object?)lvi.SubItems.IndexOfKey("Same name"), Is.EqualTo(2));

        var si5 = new ListViewItem.ListViewSubItem();
        lvi.SubItems.Add(si5);
        si5.Name = "E name";

        Assert.That((object?)lvi.SubItems.IndexOfKey("E name"), Is.EqualTo(5));
    }

    [Test]
    public void ListViewSubItemCollectionTest_RemoveByKey()
    {
        var lvi = new ListViewItem();
        var si1 = new ListViewItem.ListViewSubItem
        {
            Name = "A name"
        };
        var si2 = new ListViewItem.ListViewSubItem
        {
            Name = "B name"
        };
        var si3 = new ListViewItem.ListViewSubItem
        {
            Name = "Same name"
        };
        var si4 = new ListViewItem.ListViewSubItem
        {
            Name = "Same name"
        };
        var si5 = new ListViewItem.ListViewSubItem
        {
            Name = string.Empty
        };
        lvi.SubItems.AddRange([si1, si2, si3, si4, si5]);

        Assert.That((object?)lvi.SubItems.Count, Is.EqualTo(6));

        lvi.SubItems.RemoveByKey("B name");
        Assert.That((object?)lvi.SubItems.Count, Is.EqualTo(5));
        Assert.That((object?)lvi.SubItems[1], Is.SameAs(si1));
        Assert.That((object?)lvi.SubItems[2], Is.SameAs(si3));
        Assert.That((object?)lvi.SubItems[3], Is.SameAs(si4));
        Assert.That((object?)lvi.SubItems[4], Is.SameAs(si5));

        lvi.SubItems.RemoveByKey("Same name");
        Assert.That((object?)lvi.SubItems.Count, Is.EqualTo(4));
        Assert.That((object?)lvi.SubItems[1], Is.SameAs(si1));
        Assert.That((object?)lvi.SubItems[2], Is.SameAs(si4));
        Assert.That((object?)lvi.SubItems[3], Is.SameAs(si5));

        lvi.SubItems.RemoveByKey("a NAME");
        Assert.That((object?)lvi.SubItems.Count, Is.EqualTo(3));
        Assert.That((object?)lvi.SubItems[1], Is.SameAs(si4));
        Assert.That((object?)lvi.SubItems[2], Is.SameAs(si5));

        lvi.SubItems.RemoveByKey(string.Empty);
        Assert.That((object?)lvi.SubItems.Count, Is.EqualTo(3));
        Assert.That((object?)lvi.SubItems[1], Is.SameAs(si4));
        Assert.That((object?)lvi.SubItems[2], Is.SameAs(si5));
    }

    [Test]
    public void ListViewSubItemCollectionTest_Indexer()
    {
        var lvi = new ListViewItem();
        var si1 = new ListViewItem.ListViewSubItem
        {
            Name = "A name"
        };
        var si2 = new ListViewItem.ListViewSubItem
        {
            Name = "Same name"
        };
        var si3 = new ListViewItem.ListViewSubItem
        {
            Name = "Same name"
        };
        var si4 = new ListViewItem.ListViewSubItem
        {
            Name = string.Empty
        };
        lvi.SubItems.AddRange([si1, si2, si3, si4]);

        Assert.That((object?)lvi.SubItems.Count, Is.EqualTo(5));
        Assert.That((object?)lvi.SubItems[string.Empty], Is.EqualTo(null));
        Assert.That((object?)lvi.SubItems[null], Is.EqualTo(null));
        Assert.That((object?)lvi.SubItems["A name"], Is.EqualTo(si1));
        Assert.That((object?)lvi.SubItems["a NAME"], Is.EqualTo(si1));
        Assert.That((object?)lvi.SubItems["Same name"], Is.EqualTo(si2));

        var si5 = new ListViewItem.ListViewSubItem();
        lvi.SubItems.Add(si5);
        si5.Name = "E name";

        Assert.That((object?)lvi.SubItems["E name"], Is.EqualTo(si5));
    }

    private ListViewItem[] items;

    private void CreateVirtualItems(int count)
    {
        items = new ListViewItem[count];
        for (var i = 0; i < count; i++)
            items[i] = new ListViewItem();
    }

}