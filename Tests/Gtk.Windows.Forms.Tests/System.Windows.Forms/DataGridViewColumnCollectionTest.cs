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
//	Rolf Bjarne Kvinge  (RKvinge@novell.com)
//

using GtkTests.Helpers;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class DataGridViewColumnCollectionTest : TestHelper 
{
    [Test]
    public void AddFullColumnSelect ()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            var dgv = new DataGridView();
            dgv.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
            dgv.Columns.Add("A", "A");
        });
    }
		
    [Test]
    public void Add ()
    {
        var c = (new DataGridView ()).Columns;
        c.Add ("A", "B");
			
        var col = c [0];
			
        Assert.That((object?)col.ToString (), Is.EqualTo("DataGridViewTextBoxColumn { Name=A, Index=0 }"), "T3");
        Assert.That((object?)col.GetType ().Name, Is.EqualTo("DataGridViewTextBoxColumn"), "G2");
			
        Assert.That((object?)col.AutoSizeMode, Is.EqualTo(DataGridViewAutoSizeColumnMode.NotSet), "#A col.AutoSizeMode");
        Assert.IsNotNull (col.CellTemplate, "#A col.CellTemplate");
        Assert.IsNotNull (col.CellType, "#A col.CellType");
        Assert.IsNull (col.ContextMenuStrip, "#A col.ContextMenuStrip");
        Assert.IsNotNull (col.DataGridView, "#A col.DataGridView");
        Assert.That((object?)col.DataPropertyName, Is.EqualTo(string.Empty), "#A col.DataPropertyName");
        Assert.IsNotNull (col.DefaultCellStyle, "#A col.DefaultCellStyle");
        Assert.That((object?)col.DisplayIndex, Is.EqualTo(0), "#A col.DisplayIndex");
        Assert.That((object?)col.DividerWidth, Is.EqualTo(0), "#A col.DividerWidth");
        Assert.That((object?)col.FillWeight, Is.EqualTo(100), "#A col.FillWeight");
        Assert.That((object?)col.Frozen, Is.EqualTo(false), "#A col.Frozen");
        Assert.IsNotNull (col.HeaderCell, "#A col.HeaderCell");
        Assert.That((object?)col.HeaderText, Is.EqualTo(@"B"), "#A col.HeaderText");
        Assert.That((object?)col.Index, Is.EqualTo(0), "#A col.Index");
        Assert.That((object?)col.InheritedAutoSizeMode, Is.EqualTo(DataGridViewAutoSizeColumnMode.None), "#A col.InheritedAutoSizeMode");
        Assert.IsNotNull (col.InheritedStyle, "#A col.InheritedStyle");
        Assert.That((object?)col.IsDataBound, Is.EqualTo(false), "#A col.IsDataBound");
        Assert.That((object?)col.MinimumWidth, Is.EqualTo(5), "#A col.MinimumWidth");
        Assert.That((object?)col.Name, Is.EqualTo(@"A"), "#A col.Name");
        Assert.That((object?)col.ReadOnly, Is.EqualTo(false), "#A col.ReadOnly");
        Assert.That((object?)col.Resizable, Is.EqualTo(DataGridViewTriState.True), "#A col.Resizable");
        Assert.IsNull (col.Site, "#A col.Site");
        Assert.That((object?)col.SortMode, Is.EqualTo(DataGridViewColumnSortMode.Automatic), "#A col.SortMode");
        Assert.That((object?)col.State, Is.EqualTo(DataGridViewElementStates.Visible), "#A col.State");
        Assert.That((object?)col.ToolTipText, Is.EqualTo(string.Empty), "#A col.ToolTipText");
        Assert.IsNull (col.ValueType, "#A col.ValueType");
        Assert.That((object?)col.Visible, Is.EqualTo(true), "#A col.Visible");
        Assert.That((object?)col.Width, Is.EqualTo(100), "#A col.Width");
			
    }

    [Test]
    public void IndexUpdatedOnColumnCollectionChange ()
    {
        var dgv = new DataGridView ();

        var f = new Form ();
        f.Controls.Add (dgv);
        f.Show ();

        dgv.Columns.Add ("A1", "A1");
        Assert.That((object?)dgv.Columns[0].Index, Is.EqualTo(0));
        Assert.That((object?)dgv.Columns[0].DisplayIndex, Is.EqualTo(0));
        Assert.That((object?)dgv.Columns[0].Name, Is.EqualTo("A1"));


        dgv.Columns.Add ("A2", "A2");
        Assert.That((object?)dgv.Columns[0].Index, Is.EqualTo(0));
        Assert.That((object?)dgv.Columns[0].DisplayIndex, Is.EqualTo(0));
        Assert.That((object?)dgv.Columns[0].Name, Is.EqualTo("A1"));
        Assert.That((object?)dgv.Columns[1].Index, Is.EqualTo(1));
        Assert.That((object?)dgv.Columns[1].DisplayIndex, Is.EqualTo(1));
        Assert.That((object?)dgv.Columns[1].Name, Is.EqualTo("A2"));

        dgv.Columns.Insert (0, new DataGridViewTextBoxColumn ());
        Assert.That((object?)dgv.Columns[0].Index, Is.EqualTo(0));
        Assert.That((object?)dgv.Columns[0].DisplayIndex, Is.EqualTo(0));
        Assert.That((object?)dgv.Columns[0].Name, Is.EqualTo(string.Empty));

        Assert.That((object?)dgv.Columns[1].Index, Is.EqualTo(1));
        Assert.That((object?)dgv.Columns[1].DisplayIndex, Is.EqualTo(1));
        Assert.That((object?)dgv.Columns[1].Name, Is.EqualTo("A1"));
        Assert.That((object?)dgv.Columns[2].Index, Is.EqualTo(2));
        Assert.That((object?)dgv.Columns[2].DisplayIndex, Is.EqualTo(2));
        Assert.That((object?)dgv.Columns[2].Name, Is.EqualTo("A2"));

        dgv.Columns.RemoveAt (1);
        Assert.That((object?)dgv.Columns[0].Index, Is.EqualTo(0), "A7");
        Assert.That((object?)dgv.Columns[0].DisplayIndex, Is.EqualTo(0), "B7");
        Assert.That((object?)dgv.Columns[1].Index, Is.EqualTo(1), "A8");
        Assert.That((object?)dgv.Columns[1].DisplayIndex, Is.EqualTo(1), "B8");

        dgv.Columns.RemoveAt (0);
        Assert.That((object?)dgv.Columns[0].Index, Is.EqualTo(0), "A9");
        Assert.That((object?)dgv.Columns[0].DisplayIndex, Is.EqualTo(0), "B9");

        f.Close ();
        f.Dispose ();
    }
}