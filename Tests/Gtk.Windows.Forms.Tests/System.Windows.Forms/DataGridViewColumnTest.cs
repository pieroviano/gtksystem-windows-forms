//
// DataGridViewColumnTest.cs - Unit tests for 
// System.Windows.Forms.DataGridViewColumn
//
// Author:
//	Gert Driesen  <drieseng@users.sourceforge.net>
//
// Copyright (C) 2007 Gert Driesen
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

using GtkTests.Helpers;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class DataGridViewColumnTest : TestHelper
{
    [SetUp]
    protected override void SetUp()
    {
        columnChanged = 0;
        base.SetUp();
    }

    [Test]
    public void InitialValues()
    {
        var dvc = new DataGridViewColumn();
        Assert.That((object?)dvc.AutoSizeMode, Is.EqualTo(DataGridViewAutoSizeColumnMode.NotSet), "#A dvc.AutoSizeMode");
        Assert.IsNull(dvc.CellTemplate, "#A dvc.CellTemplate");
        Assert.IsNull(dvc.CellType, "#A dvc.CellType");
        Assert.IsNull(dvc.ContextMenuStrip, "#A dvc.ContextMenuStrip");
        Assert.IsNull(dvc.DataGridView, "#A dvc.DataGridView");
        Assert.That((object?)dvc.DataPropertyName, Is.EqualTo(string.Empty), "#A dvc.DataPropertyName");
        Assert.IsNotNull(dvc.DefaultCellStyle, "#A dvc.DefaultCellStyle");
        Assert.That((object?)dvc.DisplayIndex, Is.EqualTo(-1), "#A dvc.DisplayIndex");
        Assert.That((object?)dvc.DividerWidth, Is.EqualTo(0), "#A dvc.DividerWidth");
        Assert.That((object?)dvc.FillWeight, Is.EqualTo(100), "#A dvc.FillWeight");
        Assert.That((object?)dvc.Frozen, Is.EqualTo(false), "#A dvc.Frozen");
        Assert.IsNotNull(dvc.HeaderCell, "#A dvc.HeaderCell");
        Assert.That((object?)dvc.HeaderText, Is.EqualTo(string.Empty), "#A dvc.HeaderText");
        Assert.That((object?)dvc.Index, Is.EqualTo(-1), "#A dvc.Index");
        Assert.That((object?)dvc.InheritedAutoSizeMode, Is.EqualTo(DataGridViewAutoSizeColumnMode.NotSet), "#A dvc.InheritedAutoSizeMode");
        Assert.IsNotNull(dvc.InheritedStyle, "#A dvc.InheritedStyle");
        Assert.That((object?)dvc.IsDataBound, Is.EqualTo(false), "#A dvc.IsDataBound");
        Assert.That((object?)dvc.MinimumWidth, Is.EqualTo(5), "#A dvc.MinimumWidth");
        Assert.That((object?)dvc.Name, Is.EqualTo(string.Empty), "#A dvc.Name");
        Assert.That((object?)dvc.ReadOnly, Is.EqualTo(false), "#A dvc.ReadOnly");
        Assert.That((object?)dvc.Resizable, Is.EqualTo(DataGridViewTriState.NotSet), "#A dvc.Resizable");
        Assert.IsNull(dvc.Site, "#A dvc.Site");
        Assert.That((object?)dvc.SortMode, Is.EqualTo(DataGridViewColumnSortMode.NotSortable), "#A dvc.SortMode");
        Assert.That((object?)dvc.State, Is.EqualTo(DataGridViewElementStates.Visible), "#A dvc.State");
        Assert.That((object?)dvc.ToolTipText, Is.EqualTo(string.Empty), "#A dvc.ToolTipText");
        Assert.IsNull(dvc.ValueType, "#A dvc.ValueType");
        Assert.That((object?)dvc.Visible, Is.EqualTo(true), "#A dvc.Visible");
        Assert.That((object?)dvc.Width, Is.EqualTo(100), "#A dvc.Width");
    }

    [Test] // bug #80746
    public void HeaderText_NotBound()
    {
        var dvc = new DataGridViewColumn();
        object expected = string.Empty;
        Assert.That((object?)dvc.HeaderText, Is.EqualTo(expected));
        dvc.Name = "A";
        dvc.HeaderText = "B";
        Assert.That((object?)dvc.HeaderText, Is.EqualTo("B"));
        Assert.That((object?)dvc.Name, Is.EqualTo("A"));
        dvc.HeaderText = "C";
        Assert.That((object?)dvc.HeaderText, Is.EqualTo("C"));
        Assert.That((object?)dvc.Name, Is.EqualTo("A"));
        dvc.HeaderText = string.Empty;
        object expected1 = string.Empty;
        Assert.That((object?)dvc.HeaderText, Is.EqualTo(expected1));
        Assert.That((object?)dvc.Name, Is.EqualTo("A"));
        dvc.HeaderText = "E";
        Assert.That((object?)dvc.HeaderText, Is.EqualTo("E"));
        Assert.That((object?)dvc.Name, Is.EqualTo("A"));
        dvc.HeaderText = null!;
        object expected2 = string.Empty;
        Assert.That((object?)dvc.HeaderText, Is.EqualTo(expected2));
        Assert.That((object?)dvc.Name, Is.EqualTo("A"));
    }

    [Test]
    public void HeaderText_Bound()
    {
        var dataGrid = new DataGridView();
        DataGridViewColumn dvc = new DataGridViewTextBoxColumn();
        dataGrid.ColumnNameChanged += DataGridView_ColumnNameChanged;
        dataGrid.Columns.Add(dvc);
        object expected = string.Empty;
        Assert.That((object?)dvc.HeaderText, Is.EqualTo(expected));
        object expected1 = string.Empty;
        Assert.That((object?)dvc.Name, Is.EqualTo(expected1));
        Assert.That((object?)columnChanged, Is.EqualTo(0));
        dvc.HeaderText = "A";
        Assert.That((object?)dvc.HeaderText, Is.EqualTo("A"));
        object expected2 = string.Empty;
        Assert.That((object?)dvc.Name, Is.EqualTo(expected2));
        Assert.That((object?)columnChanged, Is.EqualTo(0));
        dvc.Name = "B";
        Assert.That((object?)dvc.HeaderText, Is.EqualTo("A"));
        Assert.That((object?)dvc.Name, Is.EqualTo("B"));
        Assert.That((object?)columnChanged, Is.EqualTo(1));
        dvc.HeaderText = "C";
        Assert.That((object?)dvc.HeaderText, Is.EqualTo("C"));
        Assert.That((object?)dvc.Name, Is.EqualTo("B"));
        Assert.That((object?)columnChanged, Is.EqualTo(1));
        dvc.HeaderText = string.Empty;
        object expected3 = string.Empty;
        Assert.That((object?)dvc.HeaderText, Is.EqualTo(expected3));
        Assert.That((object?)dvc.Name, Is.EqualTo("B"));
        Assert.That((object?)columnChanged, Is.EqualTo(1));
    }

    [Test]
    public void Name_Bound()
    {
        var dataGrid = new DataGridView();
        DataGridViewColumn dvc = new DataGridViewTextBoxColumn();
        dataGrid.ColumnNameChanged += DataGridView_ColumnNameChanged;
        dataGrid.Columns.Add(dvc);
        object expected = string.Empty;
        Assert.That((object?)dvc.HeaderText, Is.EqualTo(expected));
        object expected1 = string.Empty;
        Assert.That((object?)dvc.Name, Is.EqualTo(expected1));
        Assert.That((object?)columnChanged, Is.EqualTo(0));
        dvc.Name = "A";
        //Assert1.AreEqual(string.Empty, dvc.HeaderText);
        Assert.That((object?)dvc.Name, Is.EqualTo("A"));
        Assert.That((object?)columnChanged, Is.EqualTo(1));
        dvc.Name = "B";
        Assert.That((object?)dvc.HeaderText, Is.EqualTo("B"));
        Assert.That((object?)dvc.Name, Is.EqualTo("B"));
        Assert.That((object?)columnChanged, Is.EqualTo(2));
        dvc.Name = null!;
        object expected2 = string.Empty;
        Assert.That((object?)dvc.HeaderText, Is.EqualTo(expected2));
        object expected3 = string.Empty;
        Assert.That((object?)dvc.Name, Is.EqualTo(expected3));
        Assert.That((object?)columnChanged, Is.EqualTo(3));
        dvc.HeaderText = "C";
        Assert.That((object?)dvc.HeaderText, Is.EqualTo("C"));
        object expected4 = string.Empty;
        Assert.That((object?)dvc.Name, Is.EqualTo(expected4));
        Assert.That((object?)columnChanged, Is.EqualTo(3));
        dvc.Name = "D";
        Assert.That((object?)dvc.HeaderText, Is.EqualTo("C"));
        Assert.That((object?)dvc.Name, Is.EqualTo("D"));
        Assert.That((object?)columnChanged, Is.EqualTo(4));
        dvc.HeaderText = null!;
        object expected5 = string.Empty;
        Assert.That((object?)dvc.HeaderText, Is.EqualTo(expected5));
        Assert.That((object?)dvc.Name, Is.EqualTo("D"));
        Assert.That((object?)columnChanged, Is.EqualTo(4));
        dvc.Name = "E";
        object expected6 = string.Empty;
        Assert.That((object?)dvc.HeaderText, Is.EqualTo(expected6));
        Assert.That((object?)dvc.Name, Is.EqualTo("E"));
        Assert.That((object?)columnChanged, Is.EqualTo(5));
        dvc.Name = null!;
        object expected7 = string.Empty;
        Assert.That((object?)dvc.HeaderText, Is.EqualTo(expected7));
        object expected8 = string.Empty;
        Assert.That((object?)dvc.Name, Is.EqualTo(expected8));
        Assert.That((object?)columnChanged, Is.EqualTo(6));
        dvc.Name = "F";
        object expected9 = string.Empty;
        Assert.That((object?)dvc.HeaderText, Is.EqualTo(expected9));
        Assert.That((object?)dvc.Name, Is.EqualTo("F"));
        Assert.That((object?)columnChanged, Is.EqualTo(7));
        dvc.Name = "G";
        object expected10 = string.Empty;
        Assert.That((object?)dvc.HeaderText, Is.EqualTo(expected10));
        Assert.That((object?)dvc.Name, Is.EqualTo("G"));
        Assert.That((object?)columnChanged, Is.EqualTo(8));
    }

    [Test]
    public void Name_NotBound()
    {
        DataGridViewColumn dvc = new DataGridViewTextBoxColumn();
        object expected = string.Empty;
        Assert.That((object?)dvc.HeaderText, Is.EqualTo(expected));
        object expected1 = string.Empty;
        Assert.That((object?)dvc.Name, Is.EqualTo(expected1));
        dvc.Name = "A";
        Assert.That((object?)dvc.HeaderText, Is.EqualTo("A"));
        Assert.That((object?)dvc.Name, Is.EqualTo("A"));
        dvc.Name = "B";
        Assert.That((object?)dvc.HeaderText, Is.EqualTo("B"));
        Assert.That((object?)dvc.Name, Is.EqualTo("B"));
        dvc.Name = null!;
        object expected2 = string.Empty;
        Assert.That((object?)dvc.HeaderText, Is.EqualTo(expected2));
        object expected3 = string.Empty;
        Assert.That((object?)dvc.Name, Is.EqualTo(expected3));
        dvc.HeaderText = "C";
        Assert.That((object?)dvc.HeaderText, Is.EqualTo("C"));
        object expected4 = string.Empty;
        Assert.That((object?)dvc.Name, Is.EqualTo(expected4));
        dvc.Name = "D";
        Assert.That((object?)dvc.HeaderText, Is.EqualTo("C"));
        Assert.That((object?)dvc.Name, Is.EqualTo("D"));
        dvc.HeaderText = null!;
        object expected5 = string.Empty;
        Assert.That((object?)dvc.HeaderText, Is.EqualTo(expected5));
        Assert.That((object?)dvc.Name, Is.EqualTo("D"));
        dvc.Name = "E";
        object expected6 = string.Empty;
        Assert.That((object?)dvc.HeaderText, Is.EqualTo(expected6));
        Assert.That((object?)dvc.Name, Is.EqualTo("E"));
    }

    private void DataGridView_ColumnNameChanged(object? sender, DataGridViewColumnEventArgs e)
    {
        columnChanged++;
    }

    private int columnChanged;

    [Test]
    public void CellTemplateDataGridView()
    {
        var dgv = new DataGridView();
        DataGridViewColumn dvc = new DataGridViewTextBoxColumn();
        dgv.Columns.Add(dvc);
        Assert.IsNull(dvc.CellTemplate?.DataGridView);
    }
}