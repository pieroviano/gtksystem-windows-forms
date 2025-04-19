//
// TableLayoutTests.cs
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
// Copyright (c) 2006 Jonathan Pobst
//
// Authors:
//	Jonathan Pobst (monkey@jpobst.com)
//

using GtkTests.Helpers;
using System.Drawing;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class TableLayoutTests : TestHelper
{
    [Test]
    public void TestConstruction()
    {
        var p = new TableLayoutPanel();

        Assert.That((object?)p.BorderStyle, Is.EqualTo(BorderStyle.None));
        Assert.That((object?)p.CellBorderStyle, Is.EqualTo(TableLayoutPanelCellBorderStyle.None));
        Assert.That((object?)p.ColumnCount, Is.EqualTo(0));
        Assert.That((object?)p.GrowStyle, Is.EqualTo(TableLayoutPanelGrowStyle.AddRows));
        Assert.That((object?)p.LayoutEngine?.ToString(), Is.EqualTo("System.Windows.Forms.Layout.TableLayout"));
        Assert.That((object?)p.RowCount, Is.EqualTo(0));
        Assert.That((object?)p.ColumnStyles.Count, Is.EqualTo(0));
        Assert.That((object?)p.RowStyles.Count, Is.EqualTo(0));
        object expected = new Size(200, 100);
        Assert.That((object?)p.Size, Is.EqualTo(expected));
    }

    [Test]
    public void TestPropertySetters()
    {
        var p = new TableLayoutPanel();

        p.BorderStyle = BorderStyle.Fixed3D;
        p.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;
        p.ColumnCount = 1;
        p.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
        p.RowCount = 1;

        Assert.That((object?)p.BorderStyle, Is.EqualTo(BorderStyle.Fixed3D));
        Assert.That((object?)p.CellBorderStyle, Is.EqualTo(TableLayoutPanelCellBorderStyle.OutsetDouble));
        Assert.That((object?)p.ColumnCount, Is.EqualTo(1));
        Assert.That((object?)p.GrowStyle, Is.EqualTo(TableLayoutPanelGrowStyle.FixedSize));
        Assert.That((object?)p.RowCount, Is.EqualTo(1));
    }

    [Test]
    public void TestExtenderMethods()
    {
        var p = new TableLayoutPanel();
        Control c = new Button();

        object expected = new TableLayoutPanelCellPosition(-1, -1);
        Assert.That((object?)p.GetCellPosition(c), Is.EqualTo(expected));
        Assert.That((object?)p.GetColumn(c), Is.EqualTo(-1));
        Assert.That((object?)p.GetColumnSpan(c), Is.EqualTo(1));
        Assert.That((object?)p.GetRow(c), Is.EqualTo(-1));
        Assert.That((object?)p.GetRowSpan(c), Is.EqualTo(1));

        p.SetCellPosition(c, new TableLayoutPanelCellPosition(1, 1));
        object expected1 = new TableLayoutPanelCellPosition(1, 1);
        Assert.That((object?)p.GetCellPosition(c), Is.EqualTo(expected1));

        p.SetColumn(c, 2);
        Assert.That((object?)p.GetColumn(c), Is.EqualTo(2));
        p.SetRow(c, 2);
        Assert.That((object?)p.GetRow(c), Is.EqualTo(2));

        p.SetColumnSpan(c, 2);
        Assert.That((object?)p.GetColumnSpan(c), Is.EqualTo(2));


        p.SetRowSpan(c, 2);
        Assert.That((object?)p.GetRowSpan(c), Is.EqualTo(2));

        object expected2 = new TableLayoutPanelCellPosition(2, 2);
        Assert.That((object?)p.GetCellPosition(c), Is.EqualTo(expected2));

        // ???????
        //Assert1.AreEqual(new TableLayoutPanelCellPosition (-1, -1), p.GetPositionFromControl (c));
        //Assert1.AreEqual(c, p.GetControlFromPosition(0, 0));
    }

    [Test]
    public void TestColumnStyles()
    {
        var p = new TableLayoutPanel();

        p.ColumnStyles.Add(new ColumnStyle());
        p.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute));
        p.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));

        Assert.That((object?)p.ColumnStyles.Count, Is.EqualTo(3));
        Assert.That((object?)p.ColumnStyles[0]!.SizeType, Is.EqualTo(SizeType.AutoSize));
        Assert.That((object?)p.ColumnStyles[0]!.Width, Is.EqualTo(0));
        Assert.That((object?)p.ColumnStyles[1]!.SizeType, Is.EqualTo(SizeType.Absolute));
        Assert.That((object?)p.ColumnStyles[1]!.Width, Is.EqualTo(0));
        Assert.That((object?)p.ColumnStyles[2]!.SizeType, Is.EqualTo(SizeType.Percent));
        Assert.That((object?)p.ColumnStyles[2]!.Width, Is.EqualTo(20F));

        p.ColumnStyles.Remove(p.ColumnStyles[0]!);

        Assert.That((object?)p.ColumnStyles.Count, Is.EqualTo(2));
        Assert.That((object?)p.ColumnStyles[0]!.SizeType, Is.EqualTo(SizeType.Absolute));
        Assert.That((object?)p.ColumnStyles[0]!.Width, Is.EqualTo(0));
        Assert.That((object?)p.ColumnStyles[1]!.SizeType, Is.EqualTo(SizeType.Percent));
        Assert.That((object?)p.ColumnStyles[1]!.Width, Is.EqualTo(20F));
    }

    [Test]
    public void TestRowStyles()
    {
        var p = new TableLayoutPanel();

        p.RowStyles.Add(new RowStyle());
        p.RowStyles.Add(new RowStyle(SizeType.Absolute));
        p.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

        Assert.That((object?)p.RowStyles.Count, Is.EqualTo(3));
        Assert.That((object?)p.RowStyles[0]!.SizeType, Is.EqualTo(SizeType.AutoSize));
        Assert.That((object?)p.RowStyles[0]!.Height, Is.EqualTo(0));
        Assert.That((object?)p.RowStyles[1]!.SizeType, Is.EqualTo(SizeType.Absolute));
        Assert.That((object?)p.RowStyles[1]!.Height, Is.EqualTo(0));
        Assert.That((object?)p.RowStyles[2]!.SizeType, Is.EqualTo(SizeType.Percent));
        Assert.That((object?)p.RowStyles[2]!.Height, Is.EqualTo(20F));

        p.RowStyles.Remove(p.RowStyles[0]!);

        Assert.That((object?)p.RowStyles.Count, Is.EqualTo(2));
        Assert.That((object?)p.RowStyles[0]!.SizeType, Is.EqualTo(SizeType.Absolute));
        Assert.That((object?)p.RowStyles[0]!.Height, Is.EqualTo(0));
        Assert.That((object?)p.RowStyles[1]!.SizeType, Is.EqualTo(SizeType.Percent));
        Assert.That((object?)p.RowStyles[1]!.Height, Is.EqualTo(20F));
    }

    [Test]
    public void TestColumnStyles3()
    {
        // Don't lose the 2nd style
        var p = new TableLayoutPanel();

        p.ColumnCount = 2;
        p.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
        p.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));

        p.ColumnCount = 1;

        Assert.That((object?)p.ColumnStyles.Count, Is.EqualTo(2));
    }

    [Test]
    public void TestColumnStyles2()
    {
        // Don't lose the 2nd style
        var p = new TableLayoutPanel();

        p.ColumnCount = 1;
        p.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));

        p.ColumnCount = 2;

        Assert.That((object?)p.ColumnStyles.Count, Is.EqualTo(1));
    }

    [Test]
    public void TestCellPositioning()
    {
        // Standard Add
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();
        Control c4 = new Button();

        p.ColumnCount = 2;
        p.RowCount = 2;

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);
        p.Controls.Add(c4);

        object expected = new TableLayoutPanelCellPosition(0, 0);
        Assert.That((object?)p.GetPositionFromControl(c1), Is.EqualTo(expected));
        object expected1 = new TableLayoutPanelCellPosition(1, 0);
        Assert.That((object?)p.GetPositionFromControl(c2), Is.EqualTo(expected1));
        object expected2 = new TableLayoutPanelCellPosition(0, 1);
        Assert.That((object?)p.GetPositionFromControl(c3), Is.EqualTo(expected2));
        object expected3 = new TableLayoutPanelCellPosition(1, 1);
        Assert.That((object?)p.GetPositionFromControl(c4), Is.EqualTo(expected3));
    }

    [Test]
    public void TestCellPositioning2()
    {
        // Growstyle = Add Rows
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();
        Control c4 = new Button();
        Control c5 = new Button();
        Control c6 = new Button();

        p.ColumnCount = 2;
        p.RowCount = 2;

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);
        p.Controls.Add(c4);
        p.Controls.Add(c5);
        p.Controls.Add(c6);

        object expected = new TableLayoutPanelCellPosition(0, 0);
        Assert.That((object?)p.GetPositionFromControl(c1), Is.EqualTo(expected));
        object expected1 = new TableLayoutPanelCellPosition(1, 0);
        Assert.That((object?)p.GetPositionFromControl(c2), Is.EqualTo(expected1));
        object expected2 = new TableLayoutPanelCellPosition(0, 1);
        Assert.That((object?)p.GetPositionFromControl(c3), Is.EqualTo(expected2));
        object expected3 = new TableLayoutPanelCellPosition(1, 1);
        Assert.That((object?)p.GetPositionFromControl(c4), Is.EqualTo(expected3));
        object expected4 = new TableLayoutPanelCellPosition(0, 2);
        Assert.That((object?)p.GetPositionFromControl(c5), Is.EqualTo(expected4));
        object expected5 = new TableLayoutPanelCellPosition(1, 2);
        Assert.That((object?)p.GetPositionFromControl(c6), Is.EqualTo(expected5));
    }

    [Test]
    public void TestCellPositioning3()
    {
        // Growstyle = Add Columns
        var p = new TableLayoutPanel();
        p.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;

        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();
        Control c4 = new Button();
        Control c5 = new Button();
        Control c6 = new Button();

        p.ColumnCount = 2;
        p.RowCount = 2;

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);
        p.Controls.Add(c4);
        p.Controls.Add(c5);
        p.Controls.Add(c6);

        object expected = new TableLayoutPanelCellPosition(0, 0);
        Assert.That((object?)p.GetPositionFromControl(c1), Is.EqualTo(expected));
        object expected1 = new TableLayoutPanelCellPosition(1, 0);
        Assert.That((object?)p.GetPositionFromControl(c2), Is.EqualTo(expected1));
        object expected2 = new TableLayoutPanelCellPosition(2, 0);
        Assert.That((object?)p.GetPositionFromControl(c3), Is.EqualTo(expected2));
        object expected3 = new TableLayoutPanelCellPosition(0, 1);
        Assert.That((object?)p.GetPositionFromControl(c4), Is.EqualTo(expected3));
        object expected4 = new TableLayoutPanelCellPosition(1, 1);
        Assert.That((object?)p.GetPositionFromControl(c5), Is.EqualTo(expected4));
        object expected5 = new TableLayoutPanelCellPosition(2, 1);
        Assert.That((object?)p.GetPositionFromControl(c6), Is.EqualTo(expected5));
    }

    [Test]
    public void TestCellPositioning4()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            // Growstyle = Fixed Size
            var p = new TableLayoutPanel();
            p.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;

            Control c1 = new Button();
            Control c2 = new Button();
            Control c3 = new Button();
            Control c4 = new Button();
            Control c5 = new Button();

            p.ColumnCount = 2;
            p.RowCount = 2;

            p.Controls.Add(c1);
            p.Controls.Add(c2);
            p.Controls.Add(c3);
            p.Controls.Add(c4);
            p.Controls.Add(c5);
        });
    }

    [Test]
    public void TestCellPositioning5()
    {
        // One control have fixed position
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();
        Control c4 = new Button();

        p.ColumnCount = 2;
        p.RowCount = 2;

        p.SetCellPosition(c4, new TableLayoutPanelCellPosition(0, 0));

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);
        p.Controls.Add(c4);

        object expected = new TableLayoutPanelCellPosition(0, 0);
        Assert.That((object?)p.GetPositionFromControl(c4), Is.EqualTo(expected));
        object expected1 = new TableLayoutPanelCellPosition(1, 0);
        Assert.That((object?)p.GetPositionFromControl(c1), Is.EqualTo(expected1));
        object expected2 = new TableLayoutPanelCellPosition(0, 1);
        Assert.That((object?)p.GetPositionFromControl(c2), Is.EqualTo(expected2));
        object expected3 = new TableLayoutPanelCellPosition(1, 1);
        Assert.That((object?)p.GetPositionFromControl(c3), Is.EqualTo(expected3));
    }

    [Test]
    public void TestCellPositioning6()
    {
        // One control has fixed column, it should be ignored
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();
        Control c4 = new Button();

        p.ColumnCount = 2;
        p.RowCount = 2;

        p.SetColumn(c3, 1);

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);
        p.Controls.Add(c4);

        object expected = new TableLayoutPanelCellPosition(0, 0);
        Assert.That((object?)p.GetPositionFromControl(c1), Is.EqualTo(expected));
        object expected1 = new TableLayoutPanelCellPosition(1, 0);
        Assert.That((object?)p.GetPositionFromControl(c2), Is.EqualTo(expected1));
        object expected2 = new TableLayoutPanelCellPosition(0, 1);
        Assert.That((object?)p.GetPositionFromControl(c3), Is.EqualTo(expected2));
        object expected3 = new TableLayoutPanelCellPosition(1, 1);
        Assert.That((object?)p.GetPositionFromControl(c4), Is.EqualTo(expected3));
    }

    [Test]
    public void TestCellPositioning7()
    {
        // One control has fixed column and row
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();
        Control c4 = new Button();

        p.ColumnCount = 2;
        p.RowCount = 2;

        p.SetColumn(c3, 1);
        p.SetRow(c3, 1);

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);
        p.Controls.Add(c4);

        object expected = new TableLayoutPanelCellPosition(0, 0);
        Assert.That((object?)p.GetPositionFromControl(c1), Is.EqualTo(expected));
        object expected1 = new TableLayoutPanelCellPosition(1, 0);
        Assert.That((object?)p.GetPositionFromControl(c2), Is.EqualTo(expected1));
        object expected2 = new TableLayoutPanelCellPosition(1, 1);
        Assert.That((object?)p.GetPositionFromControl(c3), Is.EqualTo(expected2));
        object expected3 = new TableLayoutPanelCellPosition(0, 1);
        Assert.That((object?)p.GetPositionFromControl(c4), Is.EqualTo(expected3));
    }

    [Test]
    public void TestCellPositioning8()
    {
        // Column span
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();

        p.ColumnCount = 2;
        p.RowCount = 2;

        p.SetColumnSpan(c1, 2);

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);

        object expected = new TableLayoutPanelCellPosition(0, 0);
        Assert.That((object?)p.GetPositionFromControl(c1), Is.EqualTo(expected));
        object expected1 = new TableLayoutPanelCellPosition(0, 1);
        Assert.That((object?)p.GetPositionFromControl(c2), Is.EqualTo(expected1));
        object expected2 = new TableLayoutPanelCellPosition(1, 1);
        Assert.That((object?)p.GetPositionFromControl(c3), Is.EqualTo(expected2));
    }

    [Test]
    public void TestCellPositioning9()
    {
        // Row span
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();

        p.ColumnCount = 2;
        p.RowCount = 2;

        p.SetRowSpan(c1, 2);

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);

        object expected = new TableLayoutPanelCellPosition(0, 0);
        Assert.That((object?)p.GetPositionFromControl(c1), Is.EqualTo(expected));
        object expected1 = new TableLayoutPanelCellPosition(1, 0);
        Assert.That((object?)p.GetPositionFromControl(c2), Is.EqualTo(expected1));
        object expected2 = new TableLayoutPanelCellPosition(1, 1);
        Assert.That((object?)p.GetPositionFromControl(c3), Is.EqualTo(expected2));
    }

    [Test]
    public void TestCellPositioning10()
    {
        // Column span = 2, but control is in the last column, forces control back into 1st column, next row
        // I have no clue why c3 shouldn't be in (1,0), but MS says it's not
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();

        p.ColumnCount = 2;
        p.RowCount = 2;

        p.SetColumnSpan(c2, 2);

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);

        object expected = new TableLayoutPanelCellPosition(0, 0);
        Assert.That((object?)p.GetPositionFromControl(c1), Is.EqualTo(expected));
        object expected1 = new TableLayoutPanelCellPosition(0, 1);
        Assert.That((object?)p.GetPositionFromControl(c2), Is.EqualTo(expected1));
        object expected2 = new TableLayoutPanelCellPosition(0, 2);
        Assert.That((object?)p.GetPositionFromControl(c3), Is.EqualTo(expected2));
    }

    [Test]
    public void TestCellPositioning11()
    {
        // Row span = 2, but control is in the last row, creates new row
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();

        p.ColumnCount = 2;
        p.RowCount = 2;

        p.SetRowSpan(c3, 2);

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);

        object expected = new TableLayoutPanelCellPosition(0, 0);
        Assert.That((object?)p.GetPositionFromControl(c1), Is.EqualTo(expected));
        object expected1 = new TableLayoutPanelCellPosition(1, 0);
        Assert.That((object?)p.GetPositionFromControl(c2), Is.EqualTo(expected1));
        object expected2 = new TableLayoutPanelCellPosition(0, 1);
        Assert.That((object?)p.GetPositionFromControl(c3), Is.EqualTo(expected2));
    }

    [Test]
    public void TestCellPositioning12()
    {
        // Requesting a column greater than ColumnCount, request is ignored
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();

        p.ColumnCount = 2;
        p.RowCount = 2;

        p.SetColumn(c1, 4);

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);

        object expected = new TableLayoutPanelCellPosition(0, 0);
        Assert.That((object?)p.GetPositionFromControl(c1), Is.EqualTo(expected));
        object expected1 = new TableLayoutPanelCellPosition(1, 0);
        Assert.That((object?)p.GetPositionFromControl(c2), Is.EqualTo(expected1));
        object expected2 = new TableLayoutPanelCellPosition(0, 1);
        Assert.That((object?)p.GetPositionFromControl(c3), Is.EqualTo(expected2));
    }

    [Test]
    public void TestCellPositioning13()
    {
        // Row span = 2, but control is in the last row, creates new row
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();

        p.ColumnCount = 3;
        p.RowCount = 2;

        p.SetRowSpan(c3, 2);

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);

        object expected = new TableLayoutPanelCellPosition(0, 0);
        Assert.That((object?)p.GetPositionFromControl(c1), Is.EqualTo(expected));
        object expected1 = new TableLayoutPanelCellPosition(1, 0);
        Assert.That((object?)p.GetPositionFromControl(c2), Is.EqualTo(expected1));
        object expected2 = new TableLayoutPanelCellPosition(2, 0);
        Assert.That((object?)p.GetPositionFromControl(c3), Is.EqualTo(expected2));
    }

    [Test]
    public void TestCellPositioning14()
    {
        // Col span = 3, fixed grow style
        var p = new TableLayoutPanel();
        p.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
        Control c1 = new Button();

        p.ColumnCount = 2;
        p.RowCount = 2;

        p.SetColumnSpan(c1, 3);

        p.Controls.Add(c1);

        object expected = new TableLayoutPanelCellPosition(0, 0);
        Assert.That((object?)p.GetPositionFromControl(c1), Is.EqualTo(expected));
    }

    [Test]
    public void TestCellPositioning15()
    {
        // Column span = 2, but control is in the last column, forces control back into 1st column, next row
        // I have no clue why c3 shouldn't be in (1,0), but MS says it's not
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();

        p.ColumnCount = 2;
        p.RowCount = 2;

        p.SetColumnSpan(c2, 2);
        p.SetCellPosition(c2, new TableLayoutPanelCellPosition(1, 0));

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);

        object expected = new TableLayoutPanelCellPosition(0, 0);
        Assert.That((object?)p.GetPositionFromControl(c1), Is.EqualTo(expected));
        object expected1 = new TableLayoutPanelCellPosition(0, 1);
        Assert.That((object?)p.GetPositionFromControl(c2), Is.EqualTo(expected1));
        object expected2 = new TableLayoutPanelCellPosition(0, 2);
        Assert.That((object?)p.GetPositionFromControl(c3), Is.EqualTo(expected2));
    }

    [Test]
    public void TestCellPositioning16()
    {
        // Row span = 2, but control is in the last row, creates new row
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();

        p.ColumnCount = 2;
        p.RowCount = 2;

        p.SetRowSpan(c3, 2);
        p.SetCellPosition(c3, new TableLayoutPanelCellPosition(0, 1));

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);

        object expected = new TableLayoutPanelCellPosition(0, 0);
        Assert.That((object?)p.GetPositionFromControl(c1), Is.EqualTo(expected));
        object expected1 = new TableLayoutPanelCellPosition(1, 0);
        Assert.That((object?)p.GetPositionFromControl(c2), Is.EqualTo(expected1));
        object expected2 = new TableLayoutPanelCellPosition(0, 1);
        Assert.That((object?)p.GetPositionFromControl(c3), Is.EqualTo(expected2));
    }

    [Test]
    public void TestCellPositioning17()
    {
        // ColumnCount == RowCount == 0, but control is added at > 0.
        // The columns and rows are created, but ColumnCount and RowCount remains 0
        //
        var p = new TableLayoutPanel();
        p.ColumnCount = 0;
        p.RowCount = 0;
        Control c1 = new Button();

        p.Controls.Add(c1, 6, 7);
        object expected = new TableLayoutPanelCellPosition(6, 7);
        Assert.That((object?)p.GetPositionFromControl(c1), Is.EqualTo(expected));
    }

    [Test]
    public void TestCellPositioning18()
    {
        // A control with both rowspan and columnspan > 1 was getting
        // other controls put into its extent (i.e. c3 was ending up
        // at (1,1) instead of (2,1).
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();
        Control c4 = new Button();

        p.ColumnCount = 3;
        p.RowCount = 4;

        p.SetRowSpan(c1, 2);
        p.SetColumnSpan(c1, 2);
        p.SetCellPosition(c1, new TableLayoutPanelCellPosition(0, 0));

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);
        p.Controls.Add(c4);

        object expected = new TableLayoutPanelCellPosition(0, 0);
        Assert.That((object?)p.GetPositionFromControl(c1), Is.EqualTo(expected));
        object expected1 = new TableLayoutPanelCellPosition(2, 0);
        Assert.That((object?)p.GetPositionFromControl(c2), Is.EqualTo(expected1));
        object expected2 = new TableLayoutPanelCellPosition(2, 1);
        Assert.That((object?)p.GetPositionFromControl(c3), Is.EqualTo(expected2));
        object expected3 = new TableLayoutPanelCellPosition(0, 2);
        Assert.That((object?)p.GetPositionFromControl(c4), Is.EqualTo(expected3));
    }

    [Test]
    public void TestRowColumnSizes1()
    {
        // Row span = 2, but control is in the last row, creates new row
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();

        p.ColumnCount = 2;
        p.RowCount = 1;

        p.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);

        Assert.That((object?)p.GetRowHeights()[0], Is.EqualTo(71));
        Assert.That((object?)p.GetRowHeights()[1], Is.EqualTo(29));
    }

    [Test]
    public void TestRowColumnSizes2()
    {
        // Row span = 2, but control is in the last row, creates new row
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();

        p.ColumnCount = 2;
        p.RowCount = 1;

        p.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);

        Assert.That((object?)p.GetRowHeights()[0], Is.EqualTo(100));
        Assert.That((object?)p.GetRowHeights()[1], Is.EqualTo(29));
    }

    [Test]
    public void TestRowColumnSizes3()
    {
        // Row span = 2, but control is in the last row, creates new row
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();
        Control c4 = new Button();
        Control c5 = new Button();

        p.ColumnCount = 2;
        p.RowCount = 1;

        p.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);
        p.Controls.Add(c4);
        p.Controls.Add(c5);

        Assert.That((object?)p.GetRowHeights()[0], Is.EqualTo(42));
        Assert.That((object?)p.GetRowHeights()[1], Is.EqualTo(29));
        Assert.That((object?)p.GetRowHeights()[2], Is.EqualTo(29));
    }

    [Test]
    public void TestRowColumnSizes4()
    {
        // Row span = 2, but control is in the last row, creates new row
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();
        Control c4 = new Button();
        Control c5 = new Button();
        Control c6 = new Button();
        Control c7 = new Button();

        p.ColumnCount = 2;
        p.RowCount = 1;

        p.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);
        p.Controls.Add(c4);
        p.Controls.Add(c5);
        p.Controls.Add(c6);
        p.Controls.Add(c7);

        //Assert1.AreEqual(100, p.GetRowHeights ()[0]);
        Assert.That((object?)p.GetRowHeights()[1], Is.EqualTo(29));
        Assert.That((object?)p.GetRowHeights()[2], Is.EqualTo(29));
        Assert.That((object?)p.GetRowHeights()[3], Is.EqualTo(29));
    }

    [Test]
    public void TestRowColumnSizes5()
    {
        // 2 Absolute Columns/Rows
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();

        p.ColumnCount = 2;
        p.RowCount = 2;

        p.RowStyles.Add(new RowStyle(SizeType.Absolute, 20));
        p.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
        p.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20));
        p.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30));

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);

        Assert.That((object?)p.GetRowHeights()[0], Is.EqualTo(20));
        Assert.That((object?)p.GetRowHeights()[1], Is.EqualTo(80));
        Assert.That((object?)p.GetColumnWidths()[0], Is.EqualTo(20));
        Assert.That((object?)p.GetColumnWidths()[1], Is.EqualTo(180));
    }

    [Test]
    public void TestRowColumnSizes6()
    {
        // 2 50% Columns/Rows
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();

        p.ColumnCount = 2;
        p.RowCount = 2;

        p.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        p.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        p.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        p.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);

        Assert.That((object?)p.GetRowHeights()[0], Is.EqualTo(50));
        Assert.That((object?)p.GetRowHeights()[1], Is.EqualTo(50));
        Assert.That((object?)p.GetColumnWidths()[0], Is.EqualTo(100));
        Assert.That((object?)p.GetColumnWidths()[1], Is.EqualTo(100));
    }

    [Test]
    public void TestRowColumnSizes7()
    {
        // 1 Absolute and 2 Percent Columns/Rows
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();

        p.ColumnCount = 3;
        p.RowCount = 3;

        p.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
        p.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        p.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
        p.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
        p.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        p.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);

        Assert.That((object?)p.GetRowHeights()[0], Is.EqualTo(50));
        Assert.That((object?)p.GetRowHeights()[1], Is.EqualTo(25));
        Assert.That((object?)p.GetRowHeights()[2], Is.EqualTo(25));
        Assert.That((object?)p.GetColumnWidths()[0], Is.EqualTo(50));
        Assert.That((object?)p.GetColumnWidths()[1], Is.EqualTo(75));
        Assert.That((object?)p.GetColumnWidths()[2], Is.EqualTo(75));
    }

    [Test]
    public void TestRowColumnSizes8()
    {
        // 1 Absolute and 2 Percent Columns/Rows (with total percents > 100)
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();

        p.ColumnCount = 3;
        p.RowCount = 3;

        p.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
        p.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        p.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        p.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
        p.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        p.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);

        Assert.That((object?)p.GetRowHeights()[0], Is.EqualTo(50));
        Assert.That((object?)p.GetRowHeights()[1], Is.EqualTo(25));
        Assert.That((object?)p.GetRowHeights()[2], Is.EqualTo(25));
        Assert.That((object?)p.GetColumnWidths()[0], Is.EqualTo(50));
        Assert.That((object?)p.GetColumnWidths()[1], Is.EqualTo(75));
        Assert.That((object?)p.GetColumnWidths()[2], Is.EqualTo(75));
    }

    [Test]
    public void TestRowColumnSizes9()
    {
        // 1 Absolute and 2 Percent Columns/Rows (with total percents > 100)
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();

        p.ColumnCount = 3;
        p.RowCount = 3;

        p.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
        p.RowStyles.Add(new RowStyle(SizeType.Percent, 80));
        p.RowStyles.Add(new RowStyle(SizeType.Percent, 40));
        p.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50));
        p.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80));
        p.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);

        Assert.That((object?)p.GetRowHeights()[0], Is.EqualTo(50));
        Assert.That((object?)p.GetRowHeights()[1], Is.EqualTo(33));
        Assert.That((object?)p.GetRowHeights()[2], Is.EqualTo(17));
        Assert.That((object?)p.GetColumnWidths()[0], Is.EqualTo(50));
        Assert.That((object?)p.GetColumnWidths()[1], Is.EqualTo(100));
        Assert.That((object?)p.GetColumnWidths()[2], Is.EqualTo(50));
    }

    [Test]
    public void TestRowColumnSizes10()
    {
        // 2 AutoSize Columns/Rows
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();

        p.ColumnCount = 2;
        p.RowCount = 2;

        p.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        p.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        p.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        p.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

        p.Controls.Add(c1);
        p.Controls.Add(c2);
        p.Controls.Add(c3);

        Assert.That((object?)p.GetRowHeights()[0], Is.EqualTo(29));
        Assert.That((object?)p.GetRowHeights()[1], Is.EqualTo(71));
        Assert.That((object?)p.GetColumnWidths()[0], Is.EqualTo(81));
        Assert.That((object?)p.GetColumnWidths()[1], Is.EqualTo(119));
    }

    [Test]
    public void TestRowColumnSizes11()
    {
        // AutoSize Columns/Rows, and column-spanning controls, but
        // no control starts in column 1.
        // Mono's old behavior was for column 1 to have a zero width.
        var p = new TableLayoutPanel();
        Control c1 = new Button();
        Control c2 = new Button();
        Control c3 = new Button();

        c1.Size = new Size(150, 25);
        c2.Size = new Size(75, 25);
        c3.Size = new Size(150, 25);

        p.ColumnCount = 4;
        p.RowCount = 3;

        p.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        p.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        p.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        p.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        p.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        p.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        p.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

        p.SetColumnSpan(c1, 2);
        p.SetColumnSpan(c3, 2);

        p.Controls.Add(c1, 0, 0);
        p.Controls.Add(c2, 0, 1);
        p.Controls.Add(c3, 1, 1);

        // The bug fix gets Mono to behave very closely to .NET,
        // but not exactly...3 pixels off somewhere...
        Assert.That((object?)p.GetRowHeights()[0], Is.EqualTo(31));
        Assert.That((object?)p.GetRowHeights()[1], Is.EqualTo(31));
        Assert.That((object?)p.GetColumnWidths()[0], Is.EqualTo(81));
    }

    [Test]
    public void Bug81843()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        var tableLayoutPanel1 = new TableLayoutPanel();
        var button2 = new Button();
        var button4 = new Button();
        var textBox1 = new TextBox();
        tableLayoutPanel1.SuspendLayout();
        f.SuspendLayout();

        tableLayoutPanel1.AutoSize = true;
        tableLayoutPanel1.ColumnCount = 3;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel1.Controls.Add(button2, 0, 1);
        tableLayoutPanel1.Controls.Add(button4, 2, 1);
        tableLayoutPanel1.Controls.Add(textBox1, 1, 0);
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.RowCount = 2;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tableLayoutPanel1.Size = new Size(292, 287);

        button2.Size = new Size(75, 23);

        button4.Size = new Size(75, 23);

        textBox1.Dock = DockStyle.Fill;
        textBox1.Location = new Point(84, 3);
        textBox1.Multiline = true;
        textBox1.Size = new Size(94, 137);

        f.ClientSize = new Size(292, 312);
        f.Controls.Add(tableLayoutPanel1);
        f.Name = "Form1";
        f.Text = "Form1";
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        f.ResumeLayout(false);
        f.PerformLayout();

        f.Show();

        object expected = new Rectangle(3, 146, 75, 23);
        Assert.That((object?)button2.Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle(184, 146, 75, 23);
        Assert.That((object?)button4.Bounds, Is.EqualTo(expected1));
        object expected2 = new Rectangle(84, 3, 94, 137);
        Assert.That((object?)textBox1.Bounds, Is.EqualTo(expected2));

        f.Dispose();
    }

    [Test]  // From bug #81884
    public void CellBorderStyle()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        var p = new TableLayoutPanel();
        p = new TableLayoutPanel();
        p.ColumnCount = 3;
        p.ColumnStyles.Add(new ColumnStyle());
        p.ColumnStyles.Add(new ColumnStyle());
        p.ColumnStyles.Add(new ColumnStyle());
        p.Dock = DockStyle.Top;
        p.Height = 200;
        p.RowCount = 2;
        p.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        p.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        f.Controls.Add(p);

        var _labelA = new Label();
        _labelA.Dock = DockStyle.Fill;
        _labelA.Size = new Size(95, 20);
        _labelA.Text = "A";
        p.Controls.Add(_labelA, 0, 0);

        var _labelB = new Label();
        _labelB.Dock = DockStyle.Fill;
        _labelB.Size = new Size(95, 20);
        _labelB.Text = "B";
        p.Controls.Add(_labelB, 1, 0);

        var _labelC = new Label();
        _labelC.Dock = DockStyle.Fill;
        _labelC.Size = new Size(95, 20);
        _labelC.Text = "C";
        p.Controls.Add(_labelC, 2, 0);

        var _labelD = new Label();
        _labelD.Dock = DockStyle.Fill;
        _labelD.Size = new Size(95, 20);
        _labelD.Text = "D";
        p.Controls.Add(_labelD, 0, 1);

        var _labelE = new Label();
        _labelE.Dock = DockStyle.Fill;
        _labelE.Size = new Size(95, 20);
        _labelE.Text = "E";
        p.Controls.Add(_labelE, 1, 1);

        var _labelF = new Label();
        _labelF.Dock = DockStyle.Fill;
        _labelF.Size = new Size(95, 20);
        _labelF.Text = "F";
        p.Controls.Add(_labelF, 2, 1);

        _labelA.BackColor = Color.Red;
        _labelB.BackColor = Color.Orange;
        _labelC.BackColor = Color.Yellow;
        _labelD.BackColor = Color.Green;
        _labelE.BackColor = Color.Blue;
        _labelF.BackColor = Color.Purple;

        f.Show();
        // None
        object expected = new Rectangle(3, 0, 95, 100);
        Assert.That((object?)_labelA.Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle(104, 0, 95, 100);
        Assert.That((object?)_labelB.Bounds, Is.EqualTo(expected1));
        object expected2 = new Rectangle(205, 0, 95, 100);
        Assert.That((object?)_labelC.Bounds, Is.EqualTo(expected2));
        object expected3 = new Rectangle(3, 100, 95, 100);
        Assert.That((object?)_labelD.Bounds, Is.EqualTo(expected3));
        object expected4 = new Rectangle(104, 100, 95, 100);
        Assert.That((object?)_labelE.Bounds, Is.EqualTo(expected4));
        object expected5 = new Rectangle(205, 100, 95, 100);
        Assert.That((object?)_labelF.Bounds, Is.EqualTo(expected5));

        p.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
        object expected6 = new Rectangle(4, 1, 95, 98);
        Assert.That((object?)_labelA.Bounds, Is.EqualTo(expected6));
        object expected7 = new Rectangle(106, 1, 95, 98);
        Assert.That((object?)_labelB.Bounds, Is.EqualTo(expected7));
        object expected8 = new Rectangle(208, 1, 95, 98);
        Assert.That((object?)_labelC.Bounds, Is.EqualTo(expected8));
        object expected9 = new Rectangle(4, 100, 95, 99);
        Assert.That((object?)_labelD.Bounds, Is.EqualTo(expected9));
        object expected10 = new Rectangle(106, 100, 95, 99);
        Assert.That((object?)_labelE.Bounds, Is.EqualTo(expected10));
        object expected11 = new Rectangle(208, 100, 95, 99);
        Assert.That((object?)_labelF.Bounds, Is.EqualTo(expected11));

        p.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
        object expected12 = new Rectangle(5, 2, 95, 97);
        Assert.That((object?)_labelA.Bounds, Is.EqualTo(expected12));
        object expected13 = new Rectangle(108, 2, 95, 97);
        Assert.That((object?)_labelB.Bounds, Is.EqualTo(expected13));
        object expected14 = new Rectangle(211, 2, 95, 97);
        Assert.That((object?)_labelC.Bounds, Is.EqualTo(expected14));
        object expected15 = new Rectangle(5, 101, 95, 97);
        Assert.That((object?)_labelD.Bounds, Is.EqualTo(expected15));
        object expected16 = new Rectangle(108, 101, 95, 97);
        Assert.That((object?)_labelE.Bounds, Is.EqualTo(expected16));
        object expected17 = new Rectangle(211, 101, 95, 97);
        Assert.That((object?)_labelF.Bounds, Is.EqualTo(expected17));

        p.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
        object expected18 = new Rectangle(6, 3, 95, 95);
        Assert.That((object?)_labelA.Bounds, Is.EqualTo(expected18));
        object expected19 = new Rectangle(110, 3, 95, 95);
        Assert.That((object?)_labelB.Bounds, Is.EqualTo(expected19));
        object expected20 = new Rectangle(214, 3, 95, 95);
        Assert.That((object?)_labelC.Bounds, Is.EqualTo(expected20));
        object expected21 = new Rectangle(6, 101, 95, 96);
        Assert.That((object?)_labelD.Bounds, Is.EqualTo(expected21));
        object expected22 = new Rectangle(110, 101, 95, 96);
        Assert.That((object?)_labelE.Bounds, Is.EqualTo(expected22));
        object expected23 = new Rectangle(214, 101, 95, 96);
        Assert.That((object?)_labelF.Bounds, Is.EqualTo(expected23));

        p.CellBorderStyle = TableLayoutPanelCellBorderStyle.Outset;
        object expected24 = new Rectangle(5, 2, 95, 97);
        Assert.That((object?)_labelA.Bounds, Is.EqualTo(expected24));
        object expected25 = new Rectangle(108, 2, 95, 97);
        Assert.That((object?)_labelB.Bounds, Is.EqualTo(expected25));
        object expected26 = new Rectangle(211, 2, 95, 97);
        Assert.That((object?)_labelC.Bounds, Is.EqualTo(expected26));
        object expected27 = new Rectangle(5, 101, 95, 97);
        Assert.That((object?)_labelD.Bounds, Is.EqualTo(expected27));
        object expected28 = new Rectangle(108, 101, 95, 97);
        Assert.That((object?)_labelE.Bounds, Is.EqualTo(expected28));
        object expected29 = new Rectangle(211, 101, 95, 97);
        Assert.That((object?)_labelF.Bounds, Is.EqualTo(expected29));

        p.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetDouble;
        object expected30 = new Rectangle(6, 3, 95, 95);
        Assert.That((object?)_labelA.Bounds, Is.EqualTo(expected30));
        object expected31 = new Rectangle(110, 3, 95, 95);
        Assert.That((object?)_labelB.Bounds, Is.EqualTo(expected31));
        object expected32 = new Rectangle(214, 3, 95, 95);
        Assert.That((object?)_labelC.Bounds, Is.EqualTo(expected32));
        object expected33 = new Rectangle(6, 101, 95, 96);
        Assert.That((object?)_labelD.Bounds, Is.EqualTo(expected33));
        object expected34 = new Rectangle(110, 101, 95, 96);
        Assert.That((object?)_labelE.Bounds, Is.EqualTo(expected34));
        object expected35 = new Rectangle(214, 101, 95, 96);
        Assert.That((object?)_labelF.Bounds, Is.EqualTo(expected35));

        p.CellBorderStyle = TableLayoutPanelCellBorderStyle.OutsetPartial;
        object expected36 = new Rectangle(6, 3, 95, 95);
        Assert.That((object?)_labelA.Bounds, Is.EqualTo(expected36));
        object expected37 = new Rectangle(110, 3, 95, 95);
        Assert.That((object?)_labelB.Bounds, Is.EqualTo(expected37));
        object expected38 = new Rectangle(214, 3, 95, 95);
        Assert.That((object?)_labelC.Bounds, Is.EqualTo(expected38));
        object expected39 = new Rectangle(6, 101, 95, 96);
        Assert.That((object?)_labelD.Bounds, Is.EqualTo(expected39));
        object expected40 = new Rectangle(110, 101, 95, 96);
        Assert.That((object?)_labelE.Bounds, Is.EqualTo(expected40));
        object expected41 = new Rectangle(214, 101, 95, 96);
        Assert.That((object?)_labelF.Bounds, Is.EqualTo(expected41));

        f.Close();
    }

    [Test]
    public void Bug81936()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        var tableLayoutPanel1 = new TableLayoutPanel();
        var button2 = new Label();
        var button4 = new Label();
        button2.Text = "Test1";
        button4.Text = "Test2";
        button2.Anchor = AnchorStyles.Left;
        button4.Anchor = AnchorStyles.Left;
        button2.Height = 14;
        button4.Height = 14;
        tableLayoutPanel1.SuspendLayout();
        f.SuspendLayout();

        tableLayoutPanel1.ColumnCount = 1;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel1.Controls.Add(button2, 0, 0);
        tableLayoutPanel1.Controls.Add(button4, 0, 1);
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.RowCount = 2;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));
        tableLayoutPanel1.Size = new Size(292, 56);

        f.ClientSize = new Size(292, 312);
        f.Controls.Add(tableLayoutPanel1);
        f.Name = "Form1";
        f.Text = "Form1";
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        f.ResumeLayout(false);
        f.PerformLayout();

        f.Show();

        object expected = new Rectangle(3, 7, 100, 14);
        Assert.That((object?)button2.Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle(3, 35, 100, 14);
        Assert.That((object?)button4.Bounds, Is.EqualTo(expected1));

        f.Dispose();
    }

    [Test]
    public void Bug82605()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        var l = new Label();

        var table = new TableLayoutPanel();
        table.ColumnCount = 1;
        table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

        table.RowCount = 2;
        table.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        table.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));

        table.Controls.Add(l, 0, 1);
        table.Location = new Point(0, 0);
        table.Width = 250;

        l.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        l.AutoSize = true;
        l.Location = new Point(3, 352);
        l.Size = new Size(578, 13);
        l.Text = "label1";
        l.TextAlign = ContentAlignment.MiddleCenter;

        f.Controls.Add(table);
        f.Show();

        // Height is font dependent, but this bug is about the width anyways
        Assert.That((object?)l.Width, Is.EqualTo(244));

        f.Dispose();
    }

    [Test] // bug #82040
    public void ShowNoChildren()
    {
        var form = new Form();
        form.ShowInTaskbar = false;

        var tableLayoutPanel = new TableLayoutPanel();
        tableLayoutPanel.ColumnCount = 3;
        tableLayoutPanel.Dock = DockStyle.Fill;
        tableLayoutPanel.RowCount = 11;
        form.Controls.Add(tableLayoutPanel);

        form.Show();
        form.Refresh();
        form.Dispose();
    }

    [Test] // bug #82041
    public void DontCallResumeLayout()
    {
        var form = new Form();
        form.ShowInTaskbar = false;

        var tableLayoutPanel = new TableLayoutPanel();
        form.Controls.Add(tableLayoutPanel);
        tableLayoutPanel.SuspendLayout();
        tableLayoutPanel.ColumnCount = 3;
        tableLayoutPanel.Dock = DockStyle.Fill;
        tableLayoutPanel.RowCount = 11;
        tableLayoutPanel.Controls.Add(new Button());

        form.Show();
        form.Refresh();
        form.Dispose();
    }

    [Test] // bug #346246
    public void AutoSizePanelVertical()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        var tlp = new TableLayoutPanel();
        tlp.AutoSize = true;
        tlp.ColumnCount = 1;
        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tlp.Location = new Point(12, 12);
        tlp.Name = "tableLayoutPanel1";
        tlp.RowCount = 2;
        tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
        tlp.Size = new Size(139, 182);
        tlp.TabIndex = 0;

        f.Controls.Add(tlp);

        var b = new Button();
        b.Size = new Size(100, 100);
        tlp.Controls.Add(b, 0, 0);

        var p = new PictureBox();
        p.Size = new Size(100, 100);
        tlp.Controls.Add(p, 0, 1);

        f.Show();

        object expected = new Rectangle(12, 12, 106, 212);
        Assert.That((object?)tlp.Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle(3, 3, 100, 100);
        Assert.That((object?)b.Bounds, Is.EqualTo(expected1));
        object expected2 = new Rectangle(3, 109, 100, 100);
        Assert.That((object?)p.Bounds, Is.EqualTo(expected2));

        b.Width += 20;
        b.Height += 20;

        object expected3 = new Rectangle(12, 12, 126, 252);
        Assert.That((object?)tlp.Bounds, Is.EqualTo(expected3));
        object expected4 = new Rectangle(3, 3, 120, 120);
        Assert.That((object?)b.Bounds, Is.EqualTo(expected4));
        object expected5 = new Rectangle(3, 129, 100, 100);
        Assert.That((object?)p.Bounds, Is.EqualTo(expected5));

        p.Width += 20;
        p.Height += 20;

        object expected6 = new Rectangle(12, 12, 126, 252);
        Assert.That((object?)tlp.Bounds, Is.EqualTo(expected6));
        object expected7 = new Rectangle(3, 3, 120, 120);
        Assert.That((object?)b.Bounds, Is.EqualTo(expected7));
        object expected8 = new Rectangle(3, 129, 120, 120);
        Assert.That((object?)p.Bounds, Is.EqualTo(expected8));

        f.Dispose();
    }

    [Test] // bug #346246
    public void AutoSizePanelHorizontal()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        var tlp = new TableLayoutPanel();
        tlp.AutoSize = true;
        tlp.ColumnCount = 2;
        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        tlp.Location = new Point(12, 12);
        tlp.Name = "tableLayoutPanel1";
        tlp.RowCount = 1;
        tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tlp.Size = new Size(139, 182);
        tlp.TabIndex = 0;

        f.Controls.Add(tlp);

        var b = new Button();
        b.Size = new Size(100, 100);
        tlp.Controls.Add(b, 0, 0);

        var p = new PictureBox();
        p.Size = new Size(100, 100);
        tlp.Controls.Add(p, 1, 0);

        f.Show();

        object expected = new Rectangle(12, 12, 212, 106);
        Assert.That((object?)tlp.Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle(3, 3, 100, 100);
        Assert.That((object?)b.Bounds, Is.EqualTo(expected1));
        object expected2 = new Rectangle(109, 3, 100, 100);
        Assert.That((object?)p.Bounds, Is.EqualTo(expected2));

        b.Width += 20;
        b.Height += 20;

        object expected3 = new Rectangle(12, 12, 252, 126);
        Assert.That((object?)tlp.Bounds, Is.EqualTo(expected3));
        object expected4 = new Rectangle(3, 3, 120, 120);
        Assert.That((object?)b.Bounds, Is.EqualTo(expected4));
        object expected5 = new Rectangle(129, 3, 100, 100);
        Assert.That((object?)p.Bounds, Is.EqualTo(expected5));

        p.Width += 20;
        p.Height += 20;

        object expected6 = new Rectangle(12, 12, 252, 126);
        Assert.That((object?)tlp.Bounds, Is.EqualTo(expected6));
        object expected7 = new Rectangle(3, 3, 120, 120);
        Assert.That((object?)b.Bounds, Is.EqualTo(expected7));
        object expected8 = new Rectangle(129, 3, 120, 120);
        Assert.That((object?)p.Bounds, Is.EqualTo(expected8));

        f.Dispose();
    }

    [Test]
    public void Bug354676()
    {
        var f = new Form();

        var tlp = new TableLayoutPanel();
        tlp.Dock = DockStyle.Fill;
        tlp.Padding = new Padding(40);
        tlp.RowCount = 2;
        tlp.ColumnCount = 1;
        f.Controls.Add(tlp);

        var b1 = new Button();
        tlp.Controls.Add(b1);

        var b2 = new Button();
        tlp.Controls.Add(b2);

        f.Show();

        object expected = new Rectangle(43, 43, 75, 23);
        Assert.That((object?)b1.Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle(43, 72, 75, 23);
        Assert.That((object?)b2.Bounds, Is.EqualTo(expected1));

        f.Close();
        f.Dispose();
    }

    [Test]
    public void Bug355408()
    {
        var f = new Form();
        f.ClientSize = new Size(300, 300);

        var tlp = new TableLayoutPanel();
        tlp.Dock = DockStyle.Fill;
        tlp.RowCount = 2;
        tlp.ColumnCount = 2;
        f.Controls.Add(tlp);

        var b1 = new Button();
        tlp.Controls.Add(b1);

        var b2 = new Button();
        tlp.Controls.Add(b2);

        var b3 = new Button();
        b3.Dock = DockStyle.Fill;
        b3.Width = 250;
        tlp.SetColumnSpan(b3, 2);
        tlp.Controls.Add(b3);

        f.Show();

        object expected = new Rectangle(3, 3, 75, 23);
        Assert.That((object?)b1.Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle(84, 3, 75, 23);
        Assert.That((object?)b2.Bounds, Is.EqualTo(expected1));
        object expected2 = new Rectangle(3, 32, 294, 265);
        Assert.That((object?)b3.Bounds, Is.EqualTo(expected2));

        f.Close();
        f.Dispose();
    }

    [Test]
    public void Bug402651()
    {
        var f = new Form();
        f.ClientSize = new Size(300, 300);

        var tlp = new TableLayoutPanel();
        tlp.Dock = DockStyle.Fill;
        tlp.RowCount = 2;
        tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        f.Controls.Add(tlp);

        var b1 = new Button();
        b1.Text = string.Empty;
        b1.Dock = DockStyle.Fill;
        tlp.Controls.Add(b1, 0, 0);

        var b2 = new Button();
        b2.Text = string.Empty;
        b2.Size = new Size(100, 100);
        b2.Anchor = AnchorStyles.None;
        b2.Dock = DockStyle.None;
        b2.Visible = false;
        tlp.Controls.Add(b2, 0, 1);

        f.Show();

        b2.Visible = true;
        object expected = new Size(100, 100);
        Assert.That((object?)b2.Size, Is.EqualTo(expected));

        b2.Visible = false;
        b2.Anchor = AnchorStyles.Left;
        b2.Visible = true;
        object expected1 = new Size(100, 100);
        Assert.That((object?)b2.Size, Is.EqualTo(expected1));

        f.Dispose();
    }

    [Test]
    public void Bug354672()
    {
        var f = new Form();
        f.ClientSize = new Size(300, 300);

        var tlp = new TableLayoutPanel();
        tlp.AutoSize = true;
        tlp.ColumnCount = 2;
        tlp.RowCount = 1;
        f.Controls.Add(tlp);

        var t1 = new TextBox();
        t1.Dock = DockStyle.Fill;
        tlp.Controls.Add(t1);

        var t2 = new TextBox();
        t2.Dock = DockStyle.Fill;
        tlp.Controls.Add(t2);

        object expected = new Size(212, t1.Height + 6);
        Assert.That((object?)tlp.PreferredSize, Is.EqualTo(expected));

        f.Dispose();
    }

    [Test]
    public void Bug354672More()
    {
        var f = new Form();
        f.ClientSize = new Size(300, 300);

        var tlp = new TableLayoutPanel();
        tlp.AutoSize = true;
        tlp.ColumnCount = 2;
        tlp.RowCount = 1;
        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

        f.Controls.Add(tlp);

        var t1 = new TextBox();
        t1.Dock = DockStyle.Fill;
        tlp.Controls.Add(t1);

        var t2 = new TextBox();
        t2.Dock = DockStyle.Fill;
        tlp.Controls.Add(t2);

        object expected = new Size(212, t1.Height + 6);
        Assert.That((object?)tlp.PreferredSize, Is.EqualTo(expected));

        f.Dispose();
    }

    [Test]
    public void Bug367249()
    {
        // Setting a colspan greater than the number of columns was
        // causing an IOORE, this test just should not exception
        var LayoutPanel = new TableLayoutPanel();
        LayoutPanel.ColumnCount = 1;
        LayoutPanel.RowCount = 2;

        var OkButton = new Button();
        OkButton.Text = "OK";
        LayoutPanel.Controls.Add(OkButton);
        LayoutPanel.SetColumnSpan(OkButton, 3);
    }

    [Test]
    public void Bug396141()
    {
        // The issue is the user has set the RowCount to 0, but after
        // we arrange the controls, we have 1 row.  GetPreferredSize (for
        // AutoSize) was using 0 instead of 1.

        var f = new Form();
        f.ClientSize = new Size(300, 300);
        f.ShowInTaskbar = false;

        var tlp = new TableLayoutPanel();
        tlp.AutoSize = true;
        tlp.ColumnCount = 2;
        tlp.RowCount = 0;

        f.Controls.Add(tlp);

        var t1 = new TextBox();
        t1.Dock = DockStyle.Fill;
        tlp.Controls.Add(t1);

        var t2 = new TextBox();
        t2.Dock = DockStyle.Fill;
        tlp.Controls.Add(t2);

        f.Show();

        Assert.IsTrue(tlp.Height > 0, "Height must be > 0");
        Assert.IsTrue(tlp.Width > 0, "Width must be > 0");

        f.Dispose();
    }

    [Test]
    public void Bug396433()
    {
        // We were not taking the CellBorderStyle into account when calculating
        // the preferred size.
        var f = new Form();
        f.ClientSize = new Size(300, 300);
        f.ShowInTaskbar = false;

        var tlp = new TableLayoutPanel();
        tlp.AutoSize = true;
        tlp.ColumnCount = 2;
        tlp.RowCount = 1;

        f.Controls.Add(tlp);

        var t1 = new Button();
        tlp.Controls.Add(t1);

        var t2 = new Button();
        tlp.Controls.Add(t2);

        f.Show();

        object expected = new Size(162, 29);
        Assert.That((object?)tlp.PreferredSize, Is.EqualTo(expected));

        tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

        object expected1 = new Size(165, 31);
        Assert.That((object?)tlp.PreferredSize, Is.EqualTo(expected1));

        f.Dispose();
    }

    [Test]
    public void IgnoreAutoSizeMode()
    {
        // It would seem that AutoSizeMode for a TableLayoutPanel is always
        // treated as GrowAndShrink
        var f = new Form();
        f.ClientSize = new Size(300, 300);
        f.ShowInTaskbar = false;

        var tlp = new TableLayoutPanel();
        tlp.AutoSize = true;
        tlp.Dock = DockStyle.Top;
        tlp.ColumnCount = 1;
        tlp.RowCount = 1;

        f.Controls.Add(tlp);

        var t1 = new Button();
        tlp.Controls.Add(t1);

        f.Show();

        Assert.That((object?)tlp.Height, Is.EqualTo(29));

        Assert.That((object?)tlp.Height, Is.EqualTo(29));

        f.Dispose();
    }

    [Test]
    public void TestTableLayoutStyleOwned()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var style = new ColumnStyle();
            var coll = new TableLayoutPanel().ColumnStyles;
            coll.Add(style);
            var coll2 = new TableLayoutPanel().ColumnStyles;
            coll2.Add(style);
        });

        Assert.Throws<ArgumentException>(() =>
        {
            var style = new RowStyle();
            var coll = new TableLayoutPanel().RowStyles;
            coll.Add(style);
            var coll2 = new TableLayoutPanel().RowStyles;
            coll2.Add(style);
        });
    }

    [Test]
    public void XamarinBug18638()
    {
        // Spanning items should not have their entire width assigned to the first column in the span.
        var tlp = new TableLayoutPanel();
        tlp.SuspendLayout();
        tlp.Size = new Size(291, 100);
        tlp.AutoSize = true;
        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60));
        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 45));
        tlp.ColumnCount = 3;
        tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        var label1 = new Label { AutoSize = true, Text = @"This line spans all three columns in the table!" };
        tlp.Controls.Add(label1, 0, 0);
        tlp.SetColumnSpan(label1, 3);
        tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        tlp.RowCount = 1;
        var label2 = new Label { AutoSize = true, Text = @"This line spans columns two and three." };
        tlp.Controls.Add(label2, 1, 1);
        tlp.SetColumnSpan(label2, 2);
        tlp.RowCount = 2;
        AddTableRow(tlp, "First Row", "This is a test");
        AddTableRow(tlp, "Row 2", "This is another test");
        tlp.ResumeLayout();

        var widths = tlp.GetColumnWidths();
        Assert.That((object?)tlp.RowCount, Is.EqualTo(4), "X18638-1");
        Assert.That((object?)tlp.ColumnCount, Is.EqualTo(3), "X18638-2");
        Assert.That((object?)widths[0], Is.EqualTo(60), "X18638-3");
        Assert.That((object?)widths[2], Is.EqualTo(45), "X18638-4");
    }

    private void AddTableRow(TableLayoutPanel tlp, string label, string text)
    {
        tlp.SuspendLayout();
        var row = tlp.RowCount;
        tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        var first = new Label { AutoSize = true, Dock = DockStyle.Fill, Text = label };
        tlp.Controls.Add(first, 0, row);
        var second = new TextBox { AutoSize = true, Text = text, Dock = DockStyle.Fill, Multiline = true };
        tlp.Controls.Add(second, 1, row);
        var third = new Button { Text = @"DEL", Dock = DockStyle.Fill };
        tlp.Controls.Add(third, 2, row);
        tlp.RowCount = row + 1;
        tlp.ResumeLayout();
    }
}