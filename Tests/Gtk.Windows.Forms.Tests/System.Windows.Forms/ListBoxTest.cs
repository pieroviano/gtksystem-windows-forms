//
// ComboBoxTest.cs: Test cases for ComboBox.
//
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
// Authors:
//   	Ritvik Mayank <mritvik@novell.com>
//	Jordi Mas i Hernandez <jordi@ximian.com>
//

using GtkTests.Helpers;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class ListBoxTest : TestHelper
{
    private ListBox listBox;
    private Form form;

    [TearDown]
    public void TestTearDown()
    {
        listBox.Dispose();
    }

    [SetUp]
    protected override void SetUp()
    {
        listBox = new ListBox();
        form = new Form();
        form.ShowInTaskbar = false;
        base.SetUp();
    }

    [TearDown]
    protected override void TearDown()
    {
        form.Dispose();
        base.TearDown();
    }

    [Test] // bug #465422
    public void RemoveLast()
    {
        listBox.Items.Clear();

        for (var i = 0; i < 3; i++)
            listBox.Items.Add(i.ToString());

        // need to create control to actually test the invalidation
        listBox.CreateControl();

        // select last - then remove an item that is *not* the last,
        // so basically the selection is invalidated implicitly
        listBox.SelectedIndex = 2;
        listBox.Items.RemoveAt(1);
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(1));

        listBox.SelectedIndex = 0;
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(0));

        // 
        // MultiSelection
        //
        listBox.ClearSelected();
        listBox.Items.Clear();
        for (var i = 0; i < 3; i++)
            listBox.Items.Add(i.ToString());

        listBox.SelectionMode = SelectionMode.MultiSimple;

        listBox.SetSelected(0, true);
        listBox.SetSelected(2, true);

        Assert.That((object?)listBox.GetSelected(0), Is.EqualTo(true));
        Assert.That((object?)listBox.GetSelected(1), Is.EqualTo(false));
        Assert.That((object?)listBox.GetSelected(2), Is.EqualTo(true));

        listBox.Items.RemoveAt(2);
        Assert.That((object?)listBox.GetSelected(0), Is.EqualTo(true));
        Assert.That((object?)listBox.GetSelected(1), Is.EqualTo(false));
        Assert.That((object?)listBox.SelectedIndices.Count, Is.EqualTo(1));
    }

    [Test]
    public void ListBoxPropertyTest()
    {
        Assert.That((object?)listBox.ColumnWidth, Is.EqualTo(0));
        Assert.That((object?)listBox.DrawMode, Is.EqualTo(DrawMode.Normal));
        Assert.That((object?)listBox.HorizontalExtent, Is.EqualTo(0));
        Assert.That((object?)listBox.HorizontalScrollbar, Is.EqualTo(false));
        Assert.That((object?)listBox.IntegralHeight, Is.EqualTo(true));
        //Assert1.AreEqual(13, listBox.ItemHeight); // Note: Item height depends on the current font.
        listBox.Items.Add("a");
        listBox.Items.Add("b");
        listBox.Items.Add("c");
        Assert.That((object?)listBox.Items.Count, Is.EqualTo(3));
        Assert.That((object?)listBox.MultiColumn, Is.EqualTo(false));
        //Assert1.AreEqual(46, listBox.PreferredHeight); // Note: Item height depends on the current font.
        //Assert1.AreEqual(RightToLeft.No , listBox.RightToLeft); // Depends on Windows version
        Assert.That((object?)listBox.ScrollAlwaysVisible, Is.EqualTo(false));
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(-1));
        listBox.SetSelected(2, true);
        Assert.That((object?)listBox.SelectedIndices[0], Is.EqualTo(2));
        Assert.That(listBox.SelectedItem, Is.EqualTo("c"));
        Assert.That(listBox.SelectedItems[0], Is.EqualTo("c"));
        Assert.That((object?)listBox.SelectionMode, Is.EqualTo(SelectionMode.One));
        listBox.SetSelected(2, false);
        Assert.That((object?)listBox.Sorted, Is.EqualTo(false));
        Assert.That((object?)listBox.Text, Is.EqualTo(string.Empty));
        Assert.That((object?)listBox.TopIndex, Is.EqualTo(0));
        Assert.That((object?)listBox.UseTabStops, Is.EqualTo(true));
    }

    [Test]
    public void BeginEndUpdateTest()
    {
        form.Visible = true;
        listBox.Items.Add("A");
        listBox.Visible = true;
        form.Controls.Add(listBox);
        listBox.BeginUpdate();
        for (var x = 1; x < 5000; x++)
        {
            listBox.Items.Add("Item " + x.ToString());
        }
        listBox.EndUpdate();
        listBox.SetSelected(1, true);
        listBox.SetSelected(3, true);
        Assert.That((object?)listBox.SelectedItems.Contains("Item 3"), Is.EqualTo(true));
    }

    [Test]
    public void ClearSelectedTest()
    {
        form.Visible = true;
        listBox.Items.Add("A");
        listBox.Visible = true;
        form.Controls.Add(listBox);
        listBox.SetSelected(0, true);
        Assert.That((object?)listBox.SelectedItems[0]?.ToString(), Is.EqualTo("A"));
        listBox.ClearSelected();
        Assert.That((object?)listBox.SelectedItems.Count, Is.EqualTo(0));
    }

    [Test] // bug #80620
    [NUnit.Framework.Category("NotWorking")]
    public void ClientRectangle_Borders()
    {
        // This test is invalid because createcontrol forces .net to resize
        // the listbox using integralheight, which defaults to true.  This
        // will only hold for most font sizes.
        listBox.CreateControl();
        Assert.That((object?)new ListBox().ClientRectangle, Is.EqualTo(listBox.ClientRectangle));
    }

    [Ignore("It depends on user system settings")]
    public void GetItemHeightTest()
    {
        listBox.Visible = true;
        form.Controls.Add(listBox);
        listBox.Items.Add("A");
        Assert.That((object?)listBox.GetItemHeight(0), Is.EqualTo(13));
    }

    [Ignore("It depends on user system settings")]
    public void GetItemRectangleTest()
    {
        form.Visible = true;
        listBox.Visible = true;
        form.Controls.Add(listBox);
        listBox.Items.Add("A");
        object expected = new Rectangle(0, 0, 116, 13);
        Assert.That((object?)listBox.GetItemRectangle(0), Is.EqualTo(expected));
    }

    [Test]
    public void GetSelectedTest()
    {
        listBox.Items.Add("A");
        listBox.Items.Add("B");
        listBox.Items.Add("C");
        listBox.Items.Add("D");
        listBox.SelectionMode = SelectionMode.MultiSimple;
        listBox.Sorted = true;
        listBox.SetSelected(0, true);
        listBox.SetSelected(2, true);
        listBox.TopIndex = 0;
        Assert.That((object?)listBox.GetSelected(0), Is.EqualTo(true));
        listBox.SetSelected(2, false);
        Assert.That((object?)listBox.GetSelected(2), Is.EqualTo(false));
    }

    [Test]
    public void IndexFromPointTest()
    {
        listBox.Items.Add("A");
        var pt = new Point(100, 100);
        listBox.IndexFromPoint(pt);
        Assert.That((object?)listBox.IndexFromPoint(100, 100), Is.EqualTo(-1));
    }

    [Test]
    public void FindStringTest()
    {
        listBox.FindString("Hola", -5); // No exception, it's empty
        var x = listBox.FindString("Hello");
        Assert.That((object?)x, Is.EqualTo(-1));
        listBox.Items.AddRange("ACBD", "ABDC", "ACBD", "ABCD");
        var myString = "ABC";
        x = listBox.FindString(myString);
        Assert.That((object?)x, Is.EqualTo(3));
        x = listBox.FindString(string.Empty);
        Assert.That((object?)x, Is.EqualTo(0));
        x = listBox.FindString("NonExistant");
        Assert.That((object?)x, Is.EqualTo(-1));

        x = listBox.FindString("A", -1);
        Assert.That((object?)x, Is.EqualTo(0));
        x = listBox.FindString("A", 0);
        Assert.That((object?)x, Is.EqualTo(1));
        x = listBox.FindString("A", listBox.Items.Count - 1);
        Assert.That((object?)x, Is.EqualTo(0));
        x = listBox.FindString("a", listBox.Items.Count - 1);
        Assert.That((object?)x, Is.EqualTo(0));
    }

    [Test]
    public void FindStringExactTest()
    {
        listBox.FindStringExact("Hola", -5); // No exception, it's empty
        var x = listBox.FindStringExact("Hello");
        Assert.That((object?)x, Is.EqualTo(-1));
        listBox.Items.AddRange("ABCD", "ABC", "ABDC");
        var myString = "ABC";
        x = listBox.FindStringExact(myString);
        Assert.That((object?)x, Is.EqualTo(1));
        x = listBox.FindStringExact(string.Empty);
        Assert.That((object?)x, Is.EqualTo(-1));
        x = listBox.FindStringExact("NonExistant");
        Assert.That((object?)x, Is.EqualTo(-1));

        x = listBox.FindStringExact("ABCD", -1);
        Assert.That((object?)x, Is.EqualTo(0));
        x = listBox.FindStringExact("ABC", 0);
        Assert.That((object?)x, Is.EqualTo(1));
        x = listBox.FindStringExact("ABC", listBox.Items.Count - 1);
        Assert.That((object?)x, Is.EqualTo(1));
        x = listBox.FindStringExact("abcd", listBox.Items.Count - 1);
        Assert.That((object?)x, Is.EqualTo(0));
    }

    //
    // Exceptions
    //

    [Test]
    public void BorderStyleException()
    {
        Assert.Throws<InvalidEnumArgumentException>(() =>
        {
            listBox.BorderStyle = (BorderStyle)10;
        });
    }

    [Test]
    public void ColumnWidthException()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            listBox.ColumnWidth = -1;
        });
    }

    [Test]
    public void DrawModeException()
    {
        Assert.Throws<InvalidEnumArgumentException>(() =>
        {
            listBox.DrawMode = (DrawMode)10;
        });
    }

    [Test]
    public void DrawModeAndMultiColumnException()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            listBox.MultiColumn = true;
            listBox.DrawMode = DrawMode.OwnerDrawVariable;
        });
    }

    [Test]
    public void ItemHeightException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            listBox.ItemHeight = 256;
        });
    }

    [Test] // bug #80696
    public void SelectedIndex_Created()
    {
        var form = new Form();
        var listBox = new ListBox();
        listBox.Items.Add("A");
        listBox.Items.Add("B");
        form.Controls.Add(listBox);
        form.Show();

        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(-1));
        listBox.SelectedIndex = 0;
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(0));
        listBox.SelectedIndex = -1;
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(-1));
        listBox.SelectedIndex = 1;
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(1));

        form.Close();
    }

    [Test] // bug #80753
    public void SelectedIndex_NotCreated()
    {
        var listBox = new ListBox();
        listBox.Items.Add("A");
        listBox.Items.Add("B");
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(-1));
        listBox.SelectedIndex = 0;
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(0));
        listBox.SelectedIndex = -1;
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(-1));
        listBox.SelectedIndex = 1;
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(1));
    }

    [Test]
    public void SelectedIndex_Removed()
    {
        var listBox = new ListBox();
        listBox.Items.Add("A");
        listBox.Items.Add("B");
        listBox.Items.Add("C");

        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(-1));
        listBox.SelectedIndex = 2;
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(2));
        listBox.Items.RemoveAt(2);
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(-1));

        listBox.SelectedIndex = 0;
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(0));
        listBox.Items.RemoveAt(0);
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(-1));
    }

    // This should also apply to MultiSimple selection mode
    [Test]
    public void Selection_MultiExtended()
    {
        listBox.Items.Add("A");
        listBox.Items.Add("B");
        listBox.Items.Add("C");
        listBox.Items.Add("D");
        listBox.SelectionMode = SelectionMode.MultiExtended;

        //
        // First part: test the order of SelectedItems as well
        // as SelectedIndex when more than one item is selected
        //
        listBox.SelectedItems.Add("D");
        listBox.SelectedItems.Add("B");
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(1));
        Assert.That((object?)listBox.SelectedItems.Count, Is.EqualTo(2));
        Assert.That(listBox.SelectedItems[0], Is.EqualTo("B"));
        Assert.That(listBox.SelectedItems[1], Is.EqualTo("D"));

        listBox.SelectedItems.Add("C");
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(1));
        Assert.That((object?)listBox.SelectedItems.Count, Is.EqualTo(3));
        Assert.That(listBox.SelectedItems[0], Is.EqualTo("B"));
        Assert.That(listBox.SelectedItems[1], Is.EqualTo("C"));
        Assert.That(listBox.SelectedItems[2], Is.EqualTo("D"));

        listBox.SelectedItems.Add("A");
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(0));
        Assert.That((object?)listBox.SelectedItems.Count, Is.EqualTo(4));
        Assert.That(listBox.SelectedItems[0], Is.EqualTo("A"));
        Assert.That(listBox.SelectedItems[1], Is.EqualTo("B"));
        Assert.That(listBox.SelectedItems[2], Is.EqualTo("C"));
        Assert.That(listBox.SelectedItems[3], Is.EqualTo("D"));

        // 
        // Second part: how does SelectedIndex setter work related
        // to SelectedItems
        //
        listBox.SelectedIndex = -1;
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(-1));
        Assert.That((object?)listBox.SelectedItems.Count, Is.EqualTo(0));

        listBox.SelectedIndex = 3; // "D"
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(3));
        Assert.That((object?)listBox.SelectedItems.Count, Is.EqualTo(1));
        Assert.That(listBox.SelectedItems[0], Is.EqualTo("D"));

        listBox.SelectedItems.Add("B"); // index = 1
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(1));
        Assert.That((object?)listBox.SelectedItems.Count, Is.EqualTo(2));
        Assert.That(listBox.SelectedItems[0], Is.EqualTo("B"));
        Assert.That(listBox.SelectedItems[1], Is.EqualTo("D"));

        listBox.SelectedIndex = 2;
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(1));
        Assert.That((object?)listBox.SelectedItems.Count, Is.EqualTo(3));
        Assert.That(listBox.SelectedItems[0], Is.EqualTo("B"));
        Assert.That(listBox.SelectedItems[1], Is.EqualTo("C"));
        Assert.That(listBox.SelectedItems[2], Is.EqualTo("D"));

        listBox.SelectedIndex = 1; // already selected
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(1));
        Assert.That((object?)listBox.SelectedItems.Count, Is.EqualTo(3));

        // NOTE: It seems that passing -1 does not affect the collection
        // in anyway (other wrong values generate an exception, however)
        listBox.SelectedIndices.Add(-1);
        Assert.That((object?)listBox.SelectedItems.Count, Is.EqualTo(3));
    }

    [Test]
    public void Selection_One()
    {
        listBox.Items.Add("A");
        listBox.Items.Add("B");
        listBox.Items.Add("C");
        listBox.SelectionMode = SelectionMode.One;

        listBox.SelectedItems.Add("B");
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(1));
        Assert.That((object?)listBox.SelectedItems.Count, Is.EqualTo(1));
        Assert.That(listBox.SelectedItems[0], Is.EqualTo("B"));

        listBox.SelectedIndex = 2;
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(2));
        Assert.That((object?)listBox.SelectedItems.Count, Is.EqualTo(1));
        Assert.That(listBox.SelectedItems[0], Is.EqualTo("C"));

        listBox.SelectedItems.Add("A");
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(0));
        Assert.That((object?)listBox.SelectedItems.Count, Is.EqualTo(1));
        Assert.That(listBox.SelectedItems[0], Is.EqualTo("A"));
    }

    [Test]
    public void Selection_None()
    {
        listBox.Items.Add("A");
        listBox.Items.Add("B");
        listBox.SelectionMode = SelectionMode.None;

        Assert.Throws<ArgumentException>(() =>
        {
            listBox.SelectedIndex = 0;
        });

        Assert.Throws<InvalidOperationException>(() =>
        {
            listBox.SelectedIndices.Add(0);
        });

        Assert.Throws<ArgumentException>(() =>
        {
            listBox.SelectedItems.Add("A");
        });
    }

    [Test]
    public void SelectedIndexException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            listBox.SelectedIndex = -2;
        });
    }

    [Test]
    public void SelectedIndexException2()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            listBox.SelectedIndex = listBox.Items.Count;
        });
    }

    [Test]
    public void SelectedIndexModeNoneException()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            listBox.SelectionMode = SelectionMode.None;
            listBox.SelectedIndex = -1;
        });
    }

    [Test]
    public void SelectionModeException()
    {
        Assert.Throws<InvalidEnumArgumentException>(() =>
        {
            listBox.SelectionMode = (SelectionMode)10;
        });
    }

    [Test]
    public void SelectedValueNull()
    {
        listBox.Items.Clear();

        listBox.Items.Add("A");
        listBox.Items.Add("B");
        listBox.Items.Add("C");
        listBox.Items.Add("D");

        listBox.SelectedIndex = 2;
        listBox.SelectedValue = null;
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(2));
    }

    [Test]
    public void SelectedValueEmptyString()
    {
        listBox.Items.Clear();

        listBox.Items.Add("A");
        listBox.Items.Add("B");
        listBox.Items.Add("C");
        listBox.Items.Add("D");

        listBox.SelectedIndex = 2;
        listBox.SelectedValue = null;
        Assert.That((object?)listBox.SelectedIndex, Is.EqualTo(2));
    }

    [Test]	// Bug #80466
    public void ListBoxHeight()
    {
        var l = new ListBox();

        for (var h = 0; h < 100; h++)
        {
            l.Height = h;

            Assert.False(l.Height != h, "Set ListBox height of {0}, got back {1}.  Should be the same.", h, l.Height);
        }
    }

    [Test]
    public void HeightAndIntegralHeight()
    {
        var a = new ListBox();
        var defaultSize = new Size(120, 96);
        Assert.That((object?)a.Size, Is.EqualTo(defaultSize));
        a.CreateControl();
        Assert.That((object?)(a.ClientSize.Height % a.ItemHeight), Is.EqualTo(0));
        a.IntegralHeight = false;
        Assert.That((object?)defaultSize, Is.EqualTo(a.Size));
        a.IntegralHeight = true;
        Assert.That((object?)(a.ClientSize.Height % a.ItemHeight), Is.EqualTo(0));

        var clientSizeI = new Size(200, a.ItemHeight * 5);
        var clientSize = clientSizeI + new Size(0, a.ItemHeight / 2);
        var borderSize = new Size(a.Width - a.ClientSize.Width, a.Height - a.ClientSize.Height);
        var totalSizeI = clientSizeI + borderSize;
        var totalSize = clientSize + borderSize;

        a = new ListBox();
        a.ClientSize = clientSize;
        Assert.That((object?)a.ClientSize, Is.EqualTo(clientSize));
        Assert.That((object?)a.Size, Is.EqualTo(totalSize));
        a.IntegralHeight = false;
        a.IntegralHeight = true;
        Assert.That((object?)a.ClientSize, Is.EqualTo(clientSize));
        a.CreateControl();
        Assert.That((object?)a.ClientSize, Is.EqualTo(clientSizeI));
        Assert.That((object?)a.Size, Is.EqualTo(totalSizeI));
        a.IntegralHeight = false;
        Assert.That((object?)a.ClientSize, Is.EqualTo(clientSize));
        a.IntegralHeight = true;
        Assert.That((object?)a.Size, Is.EqualTo(totalSizeI));

        a = new ListBox();
        a.CreateControl();
        a.Size = totalSize;
        Assert.That((object?)a.Size, Is.EqualTo(totalSizeI));
        Assert.That((object?)a.ClientSize, Is.EqualTo(clientSizeI));
        a.IntegralHeight = false;
        Assert.That((object?)a.Size, Is.EqualTo(totalSize));
        Assert.That((object?)a.ClientSize, Is.EqualTo(clientSize));

        a = new ListBox();
        a.IntegralHeight = false;
        Assert.That((object?)a.Size, Is.EqualTo(defaultSize));
        a.CreateControl();
        Assert.That((object?)a.Size, Is.EqualTo(defaultSize));

        a = new ListBox();
        a.ClientSize = clientSize;
        a.IntegralHeight = false;
        Assert.That((object?)a.ClientSize, Is.EqualTo(clientSize));
        a.CreateControl();
        Assert.That((object?)a.ClientSize, Is.EqualTo(clientSize));
    }

    [Test]
    public void PropertyTopIndex()
    {
        var f = new Form();
        f.ShowInTaskbar = false;
        f.Show();

        var l = new ListBox();
        l.Height = 100;
        f.Controls.Add(l);

        l.Items.AddRange("A", "B", "C");

        Assert.That((object?)l.TopIndex, Is.EqualTo(0));

        l.TopIndex = 2;
        Assert.That((object?)l.TopIndex, Is.EqualTo(0));

        l.Items.AddRange("A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M");
        Assert.That((object?)l.TopIndex, Is.EqualTo(0));

        l.TopIndex = 2;
        Assert.That((object?)l.TopIndex, Is.EqualTo(2));

        // There aren't items enough for 12 to be the top index, but
        // the actual value is font height dependent.
        l.TopIndex = 12;
        Assert.IsTrue(l.TopIndex < 12);

        f.Close();
        f.Dispose();
    }

    //
    // Events
    //
    //private bool eventFired;

    //private void GenericHandler (object? sender,  EventArgs e)
    //{
    //        eventFired = true;
    //}

    [Test]
    public void SelectedIndexUpdated() // Xamarin bug 4921
    {
        using var f = new Form();
        f.ShowInTaskbar = false;

        var l = new ListBox();
        l.Sorted = true;
        f.Controls.Add(l);

        l.Items.Add("B");
        l.SelectedIndex = 0;

        Assert.That((object?)l.SelectedIndex, Is.EqualTo(0));

        l.Items.Add("A");
        Assert.That((object?)l.SelectedIndex, Is.EqualTo(1));
    }

    [Test]
    public void SelectedIndexUpdated_MultiSelect() // Xamarin bug 4921
    {
        using var f = new Form();
        f.ShowInTaskbar = false;

        var l = new ListBox();
        l.Sorted = true;
        l.SelectionMode = SelectionMode.MultiSimple;
        f.Controls.Add(l);

        l.Items.Add("B");
        l.Items.Add("C");
        l.SelectedIndex = 0;
        l.SelectedIndex = 1;

        Assert.That((object?)l.SelectedIndices.Count, Is.EqualTo(2));
        Assert.That((object?)l.SelectedIndices[0], Is.EqualTo(0));
        Assert.That((object?)l.SelectedIndices[1], Is.EqualTo(1));
        Assert.That((object?)l.SelectedItems.Count, Is.EqualTo(2));
        Assert.That(l.SelectedItems[0], Is.EqualTo("B"));
        Assert.That(l.SelectedItems[1], Is.EqualTo("C"));

        l.Items.Add("A");
        Assert.That((object?)l.SelectedIndices.Count, Is.EqualTo(2));
        Assert.That((object?)l.SelectedIndices[0], Is.EqualTo(1));
        Assert.That((object?)l.SelectedIndices[1], Is.EqualTo(2));
        Assert.That((object?)l.SelectedItems.Count, Is.EqualTo(2));
        Assert.That(l.SelectedItems[0], Is.EqualTo("B"));
        Assert.That(l.SelectedItems[1], Is.EqualTo("C"));
    }
}

[TestFixture]
public class ListBoxObjectCollectionTest : TestHelper
{
    private ListBox.ObjectCollection col;

    [SetUp]
    protected override void SetUp()
    {
        col = new ListBox.ObjectCollection(new ListBox());
    }

    [Test]
    public void DefaultProperties()
    {
        Assert.That((object?)col.IsReadOnly, Is.EqualTo(false));
        Assert.That((object?)((ICollection)col).IsSynchronized, Is.EqualTo(false));
        Assert.That(((ICollection)col).SyncRoot, Is.EqualTo(col));
        Assert.That((object?)((IList)col).IsFixedSize, Is.EqualTo(false));
        Assert.That((object?)col.Count, Is.EqualTo(0));
    }

    [Test]
    public void Add()
    {
        col.Add("Item1");
        col.Add("Item2");
        Assert.That((object?)col.Count, Is.EqualTo(2));
        Assert.That(col[0], Is.EqualTo("Item1"));
        Assert.That(col[1], Is.EqualTo("Item2"));
    }

    [Test]
    public void Add_Item_Null()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            try
            {
                col.Add(null);
            }
            catch (ArgumentNullException ex)
            {
                object expected = typeof(ArgumentNullException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                Assert.That((object?)ex.ParamName, Is.EqualTo("item"));
                throw;
            }
        });
    }

    [Test] // AddRange (Object [])
    public void AddRange1_Items_Null()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            try
            {
                col.AddRange((object[])null!);
            }
            catch (ArgumentNullException ex)
            {
                object expected = typeof(ArgumentNullException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                Assert.That((object?)ex.ParamName, Is.EqualTo("items"));
                throw;
            }
        });
    }

    [Test] // AddRange (ListBox.ObjectCollection)
    public void AddRange2_Value_Null()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            try
            {
                col.AddRange((ListBox.ObjectCollection)null!);
            }
            catch (ArgumentNullException ex)
            {
                object expected = typeof(ArgumentNullException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                Assert.That((object?)ex.ParamName, Is.EqualTo("items"));
                throw;
            }
        });
    }

    [Test]
    public void Clear()
    {
        col.Add("Item1");
        col.Add("Item2");
        col.Clear();
        Assert.That((object?)col.Count, Is.EqualTo(0));
    }

    [Test]
    public void Contains()
    {
        object obj = "Item1";
        col.Add(obj);
        Assert.IsTrue(col.Contains("Item1"));
        Assert.IsFalse(col.Contains("Item2"));
    }

    [Test]
    public void Contains_Value_Null()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            try
            {
                col.Contains(null);
            }
            catch (ArgumentNullException ex)
            {
                object expected = typeof(ArgumentNullException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                Assert.That((object?)ex.ParamName, Is.EqualTo("value"));
                throw;
            }
        });
    }

    [Test]
    public void Indexer_Value_Null()
    {
        col.Add("Item1");
        Assert.Throws<ArgumentNullException>(() =>
        {
            try
            {
                col[0] = null;
            }
            catch (ArgumentNullException ex)
            {
                object expected = typeof(ArgumentNullException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                Assert.That((object?)ex.ParamName, Is.EqualTo("value"));
                throw;
            }
        });
    }

    [Test]
    public void IndexOf()
    {
        col.Add("Item1");
        col.Add("Item2");
        Assert.That((object?)col.IndexOf("Item2"), Is.EqualTo(1));
        Assert.That((object?)col.IndexOf("Item1"), Is.EqualTo(0));
        Assert.That((object?)col.IndexOf("Item3"), Is.EqualTo(-1));
    }

    [Test]
    public void IndexOf_Value_Null()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            try
            {
                col.IndexOf(null);
            }
            catch (ArgumentNullException ex)
            {
                object expected = typeof(ArgumentNullException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                Assert.That((object?)ex.ParamName, Is.EqualTo("value"));
                throw;
            }
        });
    }

    [Test]
    [NUnit.Framework.Category("NotDotNet")] // https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=363285
    public void Insert_Item_Null()
    {
        col.Add("Item1");
        Assert.Throws<ArgumentNullException>(() =>
        {
            try
            {
                col.Insert(0, null);
            }
            catch (ArgumentNullException ex)
            {
                object expected = typeof(ArgumentNullException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                Assert.That((object?)ex.ParamName, Is.EqualTo("item"));
                throw;
            }
        });
    }

    [Test]
    public void Remove()
    {
        col.Add("Item1");
        col.Add("Item2");
        col.Remove("Item1");
        Assert.That((object?)col.Count, Is.EqualTo(1));
        Assert.That((object?)col.IndexOf("Item1"), Is.EqualTo(-1));
        Assert.That((object?)col.IndexOf("Item2"), Is.EqualTo(0));
        col.Remove(null);
        Assert.That((object?)col.Count, Is.EqualTo(1));
        Assert.That((object?)col.IndexOf("Item1"), Is.EqualTo(-1));
        Assert.That((object?)col.IndexOf("Item2"), Is.EqualTo(0));
        col.Remove("Item3");
        Assert.That((object?)col.Count, Is.EqualTo(1));
        Assert.That((object?)col.IndexOf("Item1"), Is.EqualTo(-1));
        Assert.That((object?)col.IndexOf("Item2"), Is.EqualTo(0));
        col.Remove("Item2");
        Assert.That((object?)col.Count, Is.EqualTo(0));
        Assert.That((object?)col.IndexOf("Item1"), Is.EqualTo(-1));
        Assert.That((object?)col.IndexOf("Item2"), Is.EqualTo(-1));
    }

    [Test]
    public void RemoveAt()
    {
        col.Add("Item1");
        col.Add("Item2");
        col.RemoveAt(0);
        Assert.That((object?)col.Count, Is.EqualTo(1));
        Assert.That((object?)col.IndexOf("Item1"), Is.EqualTo(-1));
        Assert.That((object?)col.IndexOf("Item2"), Is.EqualTo(0));
    }
}

[TestFixture]
public class ListBoxIntegerCollectionTest : TestHelper
{
    private ListBox.IntegerCollection col;
    private ListBox listBox;

    [TearDown]
    protected override void TearDown()
    {
        listBox.Dispose();
    }

    [SetUp]
    protected override void SetUp()
    {
        listBox = new ListBox();
        col = new ListBox.IntegerCollection(listBox);
    }

    [Test]
    public void Add()
    {
        col.Add(5);
        Assert.That((object?)col.Count, Is.EqualTo(1));
        col.Add(7);
        Assert.That((object?)col.Count, Is.EqualTo(2));
        col.Add(5);
        Assert.That((object?)col.Count, Is.EqualTo(2));
        col.Add(3);
        Assert.That((object?)col.Count, Is.EqualTo(3));
    }

    [Test] // AddRange (Int32 [])
    public void AddRange1()
    {
        col.Add(5);
        col.Add(3);
        col.AddRange(3, 7, 9, 5, 4);
        Assert.That((object?)col.Count, Is.EqualTo(5));
        Assert.That((object?)col[0], Is.EqualTo(3));
        Assert.That((object?)col[1], Is.EqualTo(4));
        Assert.That((object?)col[2], Is.EqualTo(5));
        Assert.That((object?)col[3], Is.EqualTo(7));
        Assert.That((object?)col[4], Is.EqualTo(9));
    }

    [Test] // AddRange (Int32 [])
    public void AddRange1_Items_Null()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            try
            {
                col.AddRange((int[])null!);
            }
            catch (ArgumentNullException ex)
            {
                object expected = typeof(ArgumentNullException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                Assert.That((object?)ex.ParamName, Is.EqualTo("items"));
                throw;
            }
        });
    }

    [Test] // AddRange (ListBox.IntegerCollection)
    public void AddRange2()
    {
        var ints = new ListBox.IntegerCollection(
            listBox)
        {
            3,
            1,
            -5,
            4,
            2
        };

        col.Add(5);
        col.Add(3);
        col.Add(12);
        col.AddRange(ints);

        Assert.That((object?)col.Count, Is.EqualTo(7));
        Assert.That((object?)col[0], Is.EqualTo(-5));
        Assert.That((object?)col[1], Is.EqualTo(1));
        Assert.That((object?)col[2], Is.EqualTo(2));
        Assert.That((object?)col[3], Is.EqualTo(3));
        Assert.That((object?)col[4], Is.EqualTo(4));
        Assert.That((object?)col[5], Is.EqualTo(5));
    }

    [Test] // AddRange (ListBox.IntegerCollection)
    public void AddRange2_Items_Null()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            try
            {
                col.AddRange((ListBox.IntegerCollection)null!);
            }
            catch (ArgumentNullException ex)
            {
                object expected = typeof(ArgumentNullException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                Assert.That((object?)ex.ParamName, Is.EqualTo("items"));
                throw;
            }
        });
    }

    [Test]
    [NUnit.Framework.Category("NotDotNet")] // https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=363278
    public void Clear()
    {
        col.Add(5);
        col.Add(3);
        col.Clear();

        Assert.That((object?)col.Count, Is.EqualTo(0));
        Assert.That((object?)col.IndexOf(5), Is.EqualTo(-1));
        Assert.That((object?)col.IndexOf(3), Is.EqualTo(-1));
    }

    [Test]
    public void Contains()
    {
        col.Add(5);
        col.Add(7);
        Assert.IsTrue(col.Contains(5));
        Assert.IsFalse(col.Contains(3));
        Assert.IsTrue(col.Contains(7));
        Assert.IsFalse(col.Contains(-5));
    }

    [Test]
    public void CopyTo()
    {
        var copy = new int[5] { 9, 4, 6, 2, 8 };

        col.Add(3);
        col.Add(7);
        col.Add(5);
        col.CopyTo(copy, 1);

        Assert.That((object?)copy[0], Is.EqualTo(9));
        Assert.That((object?)copy[1], Is.EqualTo(3));
        Assert.That((object?)copy[2], Is.EqualTo(5));
        Assert.That((object?)copy[3], Is.EqualTo(7));
        Assert.That((object?)copy[4], Is.EqualTo(8));
    }

    [Test]
    public void CopyTo_Destination_Invalid()
    {
        string[] copy = ["A", "B", "C"];

        col.CopyTo(copy, 1);
        col.Add(3);

        Assert.Throws<InvalidCastException>(() =>
        {
            col.CopyTo(copy, 1);
        });
    }

    [Test]
    public void CopyTo_Destination_Null()
    {
        col.CopyTo(null!, 1);
        col.Add(3);

        Assert.Throws<NullReferenceException>(() =>
        {
            col.CopyTo(null!, 1);
        });
    }

    [Test]
    public void CopyTo_Index_Negative()
    {
        var copy = new int[5] { 9, 4, 6, 2, 8 };

        col.CopyTo(copy, -5);
        col.Add(3);

        Assert.Throws<IndexOutOfRangeException>(() =>
        {
            try
            {
                col.CopyTo(copy, -5);
            }
            catch (IndexOutOfRangeException ex)
            {
                // Index was outside the bounds of the array
                object expected = typeof(IndexOutOfRangeException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                throw;
            }
        });
    }

    [Test]
    public void Count()
    {
        Assert.That((object?)col.Count, Is.EqualTo(0));
        col.Add(5);
        Assert.That((object?)col.Count, Is.EqualTo(1));
        col.Add(7);
        Assert.That((object?)col.Count, Is.EqualTo(2));
        col.Remove(7);
        Assert.That((object?)col.Count, Is.EqualTo(1));
    }

    [Test]
    public void Indexer()
    {
        col.Add(5);
        col.Add(7);
        Assert.That((object?)col[1], Is.EqualTo(7));
        Assert.That((object?)col[0], Is.EqualTo(5));
        col[0] = 3;
        Assert.That((object?)col[1], Is.EqualTo(7));
        Assert.That((object?)col[0], Is.EqualTo(3));
    }

    [Test]
    public void IndexOf()
    {
        col.Add(5);
        col.Add(7);
        Assert.That((object?)col.IndexOf(5), Is.EqualTo(0));
        Assert.That((object?)col.IndexOf(3), Is.EqualTo(-1));
        Assert.That((object?)col.IndexOf(7), Is.EqualTo(1));
        Assert.That((object?)col.IndexOf(-5), Is.EqualTo(-1));
    }

    [Test]
    public void Remove()
    {
        col.Add(5);
        col.Add(3);
        col.Remove(5);
        col.Remove(7);

        Assert.That((object?)col.Count, Is.EqualTo(1));
        Assert.That((object?)col[0], Is.EqualTo(3));
        col.Remove(3);
        Assert.That((object?)col.Count, Is.EqualTo(0));
        col.Remove(3);
        // https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=363280
        //Assert1.AreEqual(0, col.Count);
    }

    [Test]
    public void RemoveAt()
    {
        col.Add(5);
        col.Add(3);
        col.Add(7);
        col.RemoveAt(1);
        Assert.That((object?)col.Count, Is.EqualTo(2));
        Assert.That((object?)col[0], Is.EqualTo(3));
        Assert.That((object?)col[1], Is.EqualTo(7));
        col.RemoveAt(0);
        Assert.That((object?)col.Count, Is.EqualTo(1));
        Assert.That((object?)col[0], Is.EqualTo(7));
        col.RemoveAt(0);
        Assert.That((object?)col.Count, Is.EqualTo(0));
        Assert.That((object?)col.IndexOf(5), Is.EqualTo(-1));
        Assert.That((object?)col.IndexOf(3), Is.EqualTo(-1));
        // https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=363280
        //Assert1.AreEqual(-1, col.IndexOf (7));
    }

    [Test]
    [NUnit.Framework.Category("NotDotNet")] // https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=363276
    public void RemoveAt_Index_Negative()
    {
        col.Add(5);

        Assert.Throws<IndexOutOfRangeException>(() =>
        {
            try
            {
                col.RemoveAt(-1);
            }
            catch (IndexOutOfRangeException ex)
            {
                // Index was outside the bounds of the array
                object expected = typeof(IndexOutOfRangeException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                throw;
            }
        });

        Assert.That((object?)col.Count, Is.EqualTo(1));
    }

    [Test]
    [NUnit.Framework.Category("NotDotNet")] // https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=363276
    public void RemoveAt_Index_Overflow()
    {
        col.Add(5);

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            try
            {
                col.RemoveAt(1);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Index was outside the bounds of the array
                object expected = typeof(ArgumentOutOfRangeException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                Assert.That((object?)ex.ParamName, Is.EqualTo("index"));
                throw;
            }
        });

        Assert.That((object?)col.Count, Is.EqualTo(1));
    }

    [Test]
    public void ICollection_IsSynchronized()
    {
        var collection = (ICollection)col;
        Assert.IsTrue(collection.IsSynchronized);
    }

    [Test]
    public void ICollection_SyncRoot()
    {
        var collection = (ICollection)col;
        Assert.That(collection.SyncRoot, Is.SameAs(collection));
    }

    [Test]
    public void IList_Add()
    {
        var list = (IList)col;
        list.Add(5);
        Assert.That((object?)list.Count, Is.EqualTo(1));
        list.Add(7);
        Assert.That((object?)list.Count, Is.EqualTo(2));
        list.Add(5);
        Assert.That((object?)list.Count, Is.EqualTo(2));
        list.Add(3);
        Assert.That((object?)list.Count, Is.EqualTo(3));
    }

    [Test]
    public void IList_Add_Item_Invalid()
    {
        var list = (IList)col;

        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                list.Add(null);
            }
            catch (ArgumentException ex)
            {
                object expected = typeof(ArgumentException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.That((object?)ex.Message, Is.EqualTo("item"));
                Assert.IsNull(ex.ParamName);
                throw;
            }
        });

        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                list.Add("x");
            }
            catch (ArgumentException ex)
            {
                object expected = typeof(ArgumentException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.That((object?)ex.Message, Is.EqualTo("item"));
                Assert.IsNull(ex.ParamName);
                throw;
            }
        });
    }

    [Test]
    public void IList_Clear()
    {
        var list = (IList)col;
        list.Add(5);
        list.Add(7);
        list.Clear();
        Assert.That((object?)list.Count, Is.EqualTo(0));
    }

    [Test]
    public void IList_Contains()
    {
        var list = (IList)col;
        list.Add(5);
        list.Add(7);
        Assert.IsTrue(list.Contains(5));
        Assert.IsFalse(list.Contains(3));
        Assert.IsTrue(list.Contains(7));
        Assert.IsFalse(list.Contains(null));
        Assert.IsFalse(list.Contains("x"));
    }

    [Test]
    public void IList_Indexer()
    {
        var list = (IList)col;
        list.Add(5);
        list.Add(7);
        Assert.That(list[1], Is.EqualTo(7));
        Assert.That(list[0], Is.EqualTo(5));
        list[0] = 3;
        Assert.That(list[1], Is.EqualTo(7));
        Assert.That(list[0], Is.EqualTo(3));
    }

    [Test]
    public void IList_IndexOf()
    {
        var list = (IList)col;
        list.Add(5);
        list.Add(7);
        Assert.That((object?)list.IndexOf(5), Is.EqualTo(0));
        Assert.That((object?)list.IndexOf(3), Is.EqualTo(-1));
        Assert.That((object?)list.IndexOf(7), Is.EqualTo(1));
        Assert.That((object?)list.IndexOf(null), Is.EqualTo(-1));
        Assert.That((object?)list.IndexOf("x"), Is.EqualTo(-1));
    }

    [Test]
    public void IList_Insert()
    {
        var list = (IList)col;
        list.Add(5);

        Assert.Throws<NotSupportedException>(() =>
        {
            try
            {
                list.Insert(0, 7);
            }
            catch (NotSupportedException ex)
            {
                // ListBox.IntegerCollection is sorted, and
                // items cannot be inserted into it
                object expected = typeof(NotSupportedException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                throw;
            }
        });

        Assert.Throws<NotSupportedException>(() =>
        {
            try
            {
                list.Insert(-5, null);
            }
            catch (NotSupportedException ex)
            {
                // ListBox.IntegerCollection is sorted, and
                // items cannot be inserted into it
                object expected = typeof(NotSupportedException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                throw;
            }
        });
    }

    [Test]
    public void IList_IsFixedSize()
    {
        var list = (IList)col;
        Assert.IsFalse(list.IsFixedSize);
    }

    [Test]
    public void IList_IsReadOnly()
    {
        var list = (IList)col;
        Assert.IsFalse(list.IsReadOnly);
    }

    [Test]
    public void IList_Remove()
    {
        var list = (IList)col;
        list.Add(5);
        list.Add(3);
        list.Remove(5);
        list.Remove(7);
        list.Remove(int.MinValue);
        list.Remove(int.MaxValue);

        Assert.That((object?)list.Count, Is.EqualTo(1));
        Assert.That(list[0], Is.EqualTo(3));
        list.Remove(3);
        Assert.That((object?)list.Count, Is.EqualTo(0));
    }

    [Test]
    public void IList_Remove_Value_Invalid()
    {
        var list = (IList)col;
        list.Add(5);

        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                list.Remove("x");
            }
            catch (ArgumentException ex)
            {
                object expected = typeof(ArgumentException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.That((object?)ex.Message, Is.EqualTo("value"));
                Assert.IsNull(ex.ParamName);
                throw;
            }
        });

        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                list.Remove(null);
            }
            catch (ArgumentException ex)
            {
                object expected = typeof(ArgumentException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.That((object?)ex.Message, Is.EqualTo("value"));
                Assert.IsNull(ex.ParamName);
                throw;
            }
        });
    }

    [Test]
    public void IList_RemoveAt()
    {
        var list = (IList)col;
        list.Add(5);
        list.Add(3);
        list.Add(7);
        list.RemoveAt(1);
        Assert.That((object?)list.Count, Is.EqualTo(2));
        Assert.That(list[0], Is.EqualTo(3));
        Assert.That(list[1], Is.EqualTo(7));
        list.RemoveAt(0);
        Assert.That((object?)list.Count, Is.EqualTo(1));
        Assert.That(list[0], Is.EqualTo(7));
        list.RemoveAt(0);
        Assert.That((object?)list.Count, Is.EqualTo(0), "#C");
    }

    [Test]
    public void IList_RemoveAt_Index_Negative()
    {
        var list = (IList)col;
        list.Add(5);

        Assert.Throws<IndexOutOfRangeException>(() =>
        {
            try
            {
                list.RemoveAt(-1);
            }
            catch (IndexOutOfRangeException ex)
            {
                // Index was outside the bounds of the array
                object expected = typeof(IndexOutOfRangeException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                throw;
            }
        });

        // // https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=363276
        //Assert1.AreEqual(1, list.Count);
    }

    [Test]
    [NUnit.Framework.Category("NotDotNet")] // https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=363276
    public void IList_RemoveAt_Index_Overflow()
    {
        var list = (IList)col;
        list.Add(5);

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            try
            {
                list.RemoveAt(1);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Index was outside the bounds of the array
                object expected = typeof(ArgumentOutOfRangeException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                Assert.That((object?)ex.ParamName, Is.EqualTo("index"));
                throw;
            }
        });

        Assert.That((object?)list.Count, Is.EqualTo(1));
    }
}