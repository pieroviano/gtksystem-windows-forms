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
// Copyright (c) 2007 Novell, Inc. (http://www.novell.com)
//
// Author:
//	Carlos Alberto Cortez <calberto.cortez@gmail.com>
//

using GtkTests.Helpers;
using System.Windows.Forms;

namespace GtkTests.Controls;

[TestFixture]
public class ColumnHeaderTest : TestHelper
{
    [SetUp]
    protected override void SetUp()
    {
        columnReordered = 0;
        base.SetUp();
    }

    [Test]
    public void DefaultValuesTest()
    {
        var col = new ColumnHeader();

        Assert.IsNull(col.ListView, "1");
        Assert.That((object?)col.Index, Is.EqualTo(-1), "2");
        Assert.That((object?)col.Text, Is.EqualTo("ColumnHeader"), "3");
        Assert.That((object?)col.TextAlign, Is.EqualTo(HorizontalAlignment.Left), "4");
        Assert.That((object?)col.DisplayIndex, Is.EqualTo(-1), "5");
        Assert.That((object?)col.ImageIndex, Is.EqualTo(-1), "6");
        object expected = string.Empty;
        Assert.That((object?)col.ImageKey, Is.EqualTo(expected), "7");
        Assert.IsNull(col.ImageList, "8");
        object expected1 = string.Empty;
        Assert.That((object?)col.Name, Is.EqualTo(expected1), "9");
        Assert.IsNull(col.Tag, "10");
    }

    [Test]
    public void DisplayIndex_ListView_Created()
    {
        var colA = new ColumnHeader();
        var colB = new ColumnHeader();
        var colC = new ColumnHeader();
        var colD = new ColumnHeader();
        colA.DisplayIndex = 2;
        colD.DisplayIndex = 0;
        colB.DisplayIndex = 3;
        colC.DisplayIndex = 1;

        var form = new Form();
        form.ShowInTaskbar = false;
        var lv = new ListView();
        lv.ColumnReordered += ColumnReordered;
        lv.View = View.Details;
        lv.Columns.Add(colA);
        lv.Columns.Add(colB);
        lv.Columns.Add(colC);
        form.Controls.Add(lv);
        form.Show();

        Assert.That((object?)colA.DisplayIndex, Is.EqualTo(0));
        Assert.That((object?)colB.DisplayIndex, Is.EqualTo(1));
        Assert.That((object?)colC.DisplayIndex, Is.EqualTo(2));
        Assert.That((object?)colD.DisplayIndex, Is.EqualTo(0));
        Assert.That((object?)columnReordered, Is.EqualTo(0));

        colC.DisplayIndex = 0;
        Assert.That((object?)colA.DisplayIndex, Is.EqualTo(1));
        Assert.That((object?)colB.DisplayIndex, Is.EqualTo(2));
        Assert.That((object?)colC.DisplayIndex, Is.EqualTo(0));
        Assert.That((object?)colD.DisplayIndex, Is.EqualTo(0));
        Assert.That((object?)columnReordered, Is.EqualTo(0));

        colC.DisplayIndex = 2;
        Assert.That((object?)colA.DisplayIndex, Is.EqualTo(0));
        Assert.That((object?)colB.DisplayIndex, Is.EqualTo(1));
        Assert.That((object?)colC.DisplayIndex, Is.EqualTo(2));
        Assert.That((object?)colD.DisplayIndex, Is.EqualTo(0));
        Assert.That((object?)columnReordered, Is.EqualTo(0));

        colB.DisplayIndex = 2;
        Assert.That((object?)colA.DisplayIndex, Is.EqualTo(0));
        Assert.That((object?)colB.DisplayIndex, Is.EqualTo(2));
        Assert.That((object?)colC.DisplayIndex, Is.EqualTo(1));
        Assert.That((object?)colD.DisplayIndex, Is.EqualTo(0));
        Assert.That((object?)columnReordered, Is.EqualTo(0));

        colD.DisplayIndex = 1;
        lv.Columns.Add(colD);

        Assert.That((object?)colA.DisplayIndex, Is.EqualTo(0));
        Assert.That((object?)colB.DisplayIndex, Is.EqualTo(2));
        Assert.That((object?)colC.DisplayIndex, Is.EqualTo(1));
        Assert.That((object?)colD.DisplayIndex, Is.EqualTo(3));
        Assert.That((object?)columnReordered, Is.EqualTo(0));

        form.Close();
    }

    [Test]
    public void DisplayIndex_ListView_Disposed()
    {
        var lv = new ListView();
        lv.View = View.Details;
        var colA = new ColumnHeader();
        lv.Columns.Add(colA);
        var colB = new ColumnHeader();
        lv.Columns.Add(colB);
        var colC = new ColumnHeader();
        lv.Columns.Add(colC);
        Assert.That((object?)colA.DisplayIndex, Is.EqualTo(0));
        Assert.That((object?)colB.DisplayIndex, Is.EqualTo(1));
        Assert.That((object?)colC.DisplayIndex, Is.EqualTo(2));
        colA.DisplayIndex = 2;
        lv.Columns.Remove(colB);
        lv.Dispose();
        Assert.That((object?)colA.DisplayIndex, Is.EqualTo(1));
        Assert.That((object?)colB.DisplayIndex, Is.EqualTo(-1));
        Assert.That((object?)colC.DisplayIndex, Is.EqualTo(0));
        colA.DisplayIndex = 255;
        Assert.That((object?)colA.DisplayIndex, Is.EqualTo(255));
        Assert.That((object?)colB.DisplayIndex, Is.EqualTo(-1));
        Assert.That((object?)colC.DisplayIndex, Is.EqualTo(0));
    }

    [Test]
    public void DisplayIndex_ListView_NotCreated()
    {
        var colA = new ColumnHeader();
        colA.DisplayIndex = -66;
        Assert.That((object?)colA.DisplayIndex, Is.EqualTo(-66));
        colA.DisplayIndex = 66;
        Assert.That((object?)colA.DisplayIndex, Is.EqualTo(66));

        var colB = new ColumnHeader();
        colB.DisplayIndex = 0;
        Assert.That((object?)colB.DisplayIndex, Is.EqualTo(0));

        var colC = new ColumnHeader();
        colC.DisplayIndex = 1;
        Assert.That((object?)colC.DisplayIndex, Is.EqualTo(1));

        var lv = new ListView();
        lv.ColumnReordered += ColumnReordered;
        lv.View = View.Details;
        lv.Columns.Add(colA);
        lv.Columns.Add(colB);
        lv.Columns.Add(colC);

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            try
            {
                colA.DisplayIndex = -1;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                object expected = typeof(ArgumentOutOfRangeException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                Assert.IsNotNull(ex.ParamName);
                Assert.That((object?)ex.ParamName, Is.EqualTo("DisplayIndex"));
                throw;
            }
        });

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            try
            {
                colA.DisplayIndex = lv.Columns.Count;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                object expected = typeof(ArgumentOutOfRangeException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                Assert.IsNotNull(ex.ParamName);
                Assert.That((object?)ex.ParamName, Is.EqualTo("DisplayIndex"));
                throw;
            }
        });

        Assert.That((object?)colA.DisplayIndex, Is.EqualTo(0));
        Assert.That((object?)colB.DisplayIndex, Is.EqualTo(1));
        Assert.That((object?)colC.DisplayIndex, Is.EqualTo(2));
        Assert.That((object?)columnReordered, Is.EqualTo(0));
    }

    [Test]
    public void ImageIndex_Invalid()
    {
        var col = new ColumnHeader();
        col.ImageIndex = 2;
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            try
            {
                col.ImageIndex = -2;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                object expected = typeof(ArgumentOutOfRangeException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNotNull(ex.Message);
                Assert.IsNotNull(ex.ParamName);
                Assert.That((object?)ex.ParamName, Is.EqualTo("ImageIndex"));
                Assert.IsNull(ex.InnerException);
                throw;
            }
        });
        Assert.That((object?)col.ImageIndex, Is.EqualTo(2));
    }

    [Test]
    public void ImageKey()
    {
        var col = new ColumnHeader();
        object expected = string.Empty;
        Assert.That((object?)col.ImageKey, Is.EqualTo(expected));
        col.ImageKey = "test";
        Assert.That((object?)col.ImageKey, Is.EqualTo("test"));
        col.ImageKey = null;
        object expected1 = string.Empty;
        Assert.That((object?)col.ImageKey, Is.EqualTo(expected1));
    }

    [Test]
    public void ImageKeyAndImageIndexInteraction()
    {
        var col = new ColumnHeader();
        col.ImageIndex = 1;
        Assert.That((object?)col.ImageIndex, Is.EqualTo(1));
        object expected = string.Empty;
        Assert.That((object?)col.ImageKey, Is.EqualTo(expected));
        col.ImageKey = "test";
        Assert.That((object?)col.ImageIndex, Is.EqualTo(-1));
        Assert.That((object?)col.ImageKey, Is.EqualTo("test"));
        col.ImageIndex = 2;
        Assert.That((object?)col.ImageIndex, Is.EqualTo(2));
        object expected1 = string.Empty;
        Assert.That((object?)col.ImageKey, Is.EqualTo(expected1));
        col.ImageKey = null;
        Assert.That((object?)col.ImageIndex, Is.EqualTo(-1));
        object expected2 = string.Empty;
        Assert.That((object?)col.ImageKey, Is.EqualTo(expected2));
    }

    [Test]
    public void ImageList()
    {
        var col = new ColumnHeader();
        Assert.IsNull(col.ImageList);

        var lv = new ListView();
        lv.View = View.Details;
        var small = new ImageList();
        lv.SmallImageList = small;
        var large = new ImageList();
        lv.LargeImageList = large;
        lv.Columns.Add(col);
        Assert.IsNotNull(col.ImageList);
        Assert.That((object?)col.ImageList, Is.SameAs(small));
    }

    [Test]
    public void ImageList_ListView_Disposed()
    {
        var lv = new ListView();
        lv.View = View.Details;
        var small = new ImageList();
        lv.SmallImageList = small;
        var large = new ImageList();
        lv.LargeImageList = large;
        var col = new ColumnHeader();
        lv.Columns.Add(col);
        lv.Dispose();
        Assert.IsNull(col.ImageList);
    }

    [Test]
    public void Index_ListView_Disposed()
    {
        var lv = new ListView();
        lv.View = View.Details;
        var colA = new ColumnHeader();
        lv.Columns.Add(colA);
        var colB = new ColumnHeader();
        lv.Columns.Add(colB);
        lv.Dispose();
        Assert.That((object?)colA.Index, Is.EqualTo(-1));
        Assert.That((object?)colB.Index, Is.EqualTo(-1));
    }

    [Test]
    public void Name()
    {
        var col = new ColumnHeader();
        object expected = string.Empty;
        Assert.That((object?)col.Name, Is.EqualTo(expected));
        col.Name = "Address";
        Assert.That((object?)col.Name, Is.EqualTo("Address"));
        col.Name = null!;
        object expected1 = string.Empty;
        Assert.That((object?)col.Name, Is.EqualTo(expected1));
    }

    [Test]
    public void Tag()
    {
        var col = new ColumnHeader();
        Assert.IsNull(col.Tag);
        col.Tag = "whatever";
        Assert.That(col.Tag, Is.EqualTo("whatever"));
        col.Tag = null;
        Assert.IsNull(col.Tag);
    }

    [Test]
    public void Text_ListView_Disposed()
    {
        var lv = new ListView();
        lv.View = View.Details;
        var col = new ColumnHeader();
        lv.Columns.Add(col);
        lv.Dispose();
        col.Text = "whatever";
        Assert.That((object?)col.Text, Is.EqualTo("whatever"));
    }

    [Test]
    public void TextAlign_ListView_Disposed()
    {
        var lv = new ListView();
        lv.View = View.Details;
        var col = new ColumnHeader();
        lv.Columns.Add(col);
        lv.Dispose();
        col.TextAlign = HorizontalAlignment.Right;
        Assert.That((object?)col.TextAlign, Is.EqualTo(HorizontalAlignment.Right));
    }

    [Test]
    public void ToStringTest()
    {
        var lv = new ListView();
        lv.SmallImageList = new ImageList();
        var col = new ColumnHeader();
        col.DisplayIndex = 3;
        col.ImageIndex = 2;
        col.Name = "address_col";
        col.Tag = DateTime.Now;
        col.Text = "Address";
        col.TextAlign = HorizontalAlignment.Right;
        col.Width = 30;
        lv.Columns.Add(col);
        Assert.That((object?)col.ToString(), Is.EqualTo("ColumnHeader: Text: Address"));
    }

    [Test]
    [Category("NotWorking")]
    public void WidthDefault()
    {
        var col = new ColumnHeader();
        Assert.That((object?)col.Width, Is.EqualTo(60));
    }

    [Test]
    public void Width_ListView_Disposed()
    {
        var lv = new ListView();
        lv.View = View.Details;
        var col = new ColumnHeader();
        lv.Columns.Add(col);
        lv.Dispose();
        col.Width = 10;
        Assert.That((object?)col.Width, Is.EqualTo(10));
    }

    // Ensure the last column is using all the free space to the right
    [Test]
    public void Width_AutoResize_Expand()
    {
        var lv = new ListView();
        lv.BeginUpdate();
        lv.View = View.Details;
        var col1 = new ColumnHeader("One");
        var col2 = new ColumnHeader("Two");
        lv.Columns.AddRange([col1, col2]);
        lv.EndUpdate();

        col1.Width = 10;
        col2.Width = 10;
        Assert.That((object?)col1.Width, Is.EqualTo(10));
        Assert.That((object?)col2.Width, Is.EqualTo(10));

        // Need to create the handle in order to actually use the auto size feature
        lv.CreateControl();

        col2.Width = -2;
        Assert.That((object?)(col2.Width == lv.ClientRectangle.Width), Is.EqualTo(true));
    }

    public void ColumnReordered(object? sender, ColumnReorderedEventArgs e)
    {
        columnReordered++;
    }

    private int columnReordered;
}