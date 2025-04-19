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
//	Pedro Martínez Juliá <pedromj@gmail.com>
//

using GtkTests.Helpers;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class DataGridViewRowTest : TestHelper
{

    [Test]
    public void TestDefaultValues()
    {
    }

    [Test]
    public void TestVisibleInvalidOperationException()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            var grid = new DataGridView();
            var row = new DataGridViewRow();
            grid.Rows.Add(row);
            row.Visible = false;
        });
    }

    [Test]
    public void Height()
    {
        var row = new DataGridViewRow();
        Assert.IsTrue(row.Height > 5);
        row.Height = 70;
        Assert.That((object?)row.Height, Is.EqualTo(70));
        row.Height = 40;
        Assert.That((object?)row.Height, Is.EqualTo(40));
    }

    [Test]
    public void Height_SetHeightLessThanMinHeightSilentlySetsToMinHeight()
    {
        var row = new DataGridViewRow
        {
            // Setup
            MinimumHeight = 5,
            // Execute
            Height = 2
        };

        // Verify
        Assert.That((object?)row.Height, Is.EqualTo(5), "Height didn't get set to MinimumHeight");
    }

    [Test]
    public void MinimumHeight_DefaultValues()
    {
        var row = new DataGridViewRow();
        Assert.IsTrue(row.MinimumHeight > 0);
        Assert.IsTrue(row.Height >= row.MinimumHeight);
    }

    [Test]
    public void MinimumHeight_SetValues()
    {
        var row = new DataGridViewRow
        {
            MinimumHeight = 40,
            Height = 50
        };
        Assert.That((object?)row.MinimumHeight, Is.EqualTo(40));
        Assert.That((object?)row.Height, Is.EqualTo(50));
    }

    [Test]
    public void MinimumHeight_IncreaseMinHeightChangesHeight()
    {
        var row = new DataGridViewRow
        {
            MinimumHeight = 20,
            Height = 20
        };
        Assert.That((object?)row.MinimumHeight, Is.EqualTo(20));
        Assert.That((object?)row.Height, Is.EqualTo(20));
        row.MinimumHeight = 40;
        Assert.That((object?)row.MinimumHeight, Is.EqualTo(40));
        Assert.That((object?)row.Height, Is.EqualTo(40));
    }

    [Test]
    public void MinimumHeight_SettingToLessThan2ThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var row = new DataGridViewRow
            {
                // We expect the next line to throw an ArgumentOutOfRangeException
                MinimumHeight = 1
            };
        });
    }

    [Test]
    [Category("NotWorking")]	// DGVComboBox not implemented
    public void AddRow_Changes()
    {
        using var dgv = new DataGridView();
        DataGridViewColumn col = new DataGridViewComboBoxColumn();
        var row = new DataGridViewRow();
        DataGridViewCell cell = new DataGridViewComboBoxCell();

        Assert.IsNotNull(row.AccessibilityObject, "#A row.AccessibilityObject");
        Assert.IsNotNull(row.Cells, "#A row.Cells");
        Assert.IsNull(row.ContextMenuStrip, "#A row.ContextMenuStrip");
        Assert.IsNull(row.DataBoundItem, "#A row.DataBoundItem");
        Assert.IsNull(row.DataGridView, "#A row.DataGridView");
        Assert.IsNotNull(row.DefaultCellStyle, "#A row.DefaultCellStyle");
        Assert.That((object?)row.Displayed, Is.EqualTo(false), "#A row.Displayed");
        Assert.That((object?)row.DividerHeight, Is.EqualTo(0), "#A row.DividerHeight");
        Assert.That((object?)row.ErrorText, Is.EqualTo(string.Empty), "#A row.ErrorText");
        Assert.That((object?)row.Frozen, Is.EqualTo(false), "#A row.Frozen");
        Assert.IsNotNull(row.HeaderCell, "#A row.HeaderCell");
        // DPI Dependent? // Assert1.AreEqual(22, row.Height, "#A row.Height");
        Assert.That((object?)row.Index, Is.EqualTo(-1), "#A row.Index");
        Assert.Throws<InvalidOperationException>(() =>
        {
            try
            {
                object? zxf = row.InheritedStyle;
                RemoveWarning(zxf!);
            }
            catch (InvalidOperationException ex)
            {
                Assert.That((object?)ex.Message, Is.EqualTo(@"Getting the InheritedStyle property of a shared row is not a valid operation."));
                throw;
            }
        });
        Assert.That((object?)row.IsNewRow, Is.EqualTo(false), "#A row.IsNewRow");
        Assert.That((object?)row.MinimumHeight, Is.EqualTo(3), "#A row.MinimumHeight");
        Assert.That((object?)row.ReadOnly, Is.EqualTo(false), "#A row.ReadOnly");
        Assert.That((object?)row.Resizable, Is.EqualTo(DataGridViewTriState.NotSet), "#A row.Resizable");
        Assert.That((object?)row.Selected, Is.EqualTo(false), "#A row.Selected");
        Assert.That((object?)row.State, Is.EqualTo(DataGridViewElementStates.Visible), "#A row.State");
        Assert.That((object?)row.Visible, Is.EqualTo(true), "#A row.Visible");

        row.Cells.Add(cell);

        Assert.IsNotNull(row.AccessibilityObject, "#B row.AccessibilityObject");
        Assert.IsNotNull(row.Cells, "#B row.Cells");
        Assert.IsNull(row.ContextMenuStrip, "#B row.ContextMenuStrip");
        Assert.IsNull(row.DataBoundItem, "#B row.DataBoundItem");
        Assert.IsNull(row.DataGridView, "#B row.DataGridView");
        Assert.IsNotNull(row.DefaultCellStyle, "#B row.DefaultCellStyle");
        Assert.That((object?)row.Displayed, Is.EqualTo(false), "#B row.Displayed");
        Assert.That((object?)row.DividerHeight, Is.EqualTo(0), "#B row.DividerHeight");
        Assert.That((object?)row.ErrorText, Is.EqualTo(string.Empty), "#B row.ErrorText");
        Assert.That((object?)row.Frozen, Is.EqualTo(false), "#B row.Frozen");
        Assert.IsNotNull(row.HeaderCell, "#B row.HeaderCell");
        // DPI Dependent? // Assert1.AreEqual(22, row.Height, "#B row.Height");
        Assert.That((object?)row.Index, Is.EqualTo(-1), "#B row.Index");
        Assert.Throws<InvalidOperationException>(() =>
        {
            try
            {
                object? zxf = row.InheritedStyle;
                RemoveWarning(zxf!);
            }
            catch (InvalidOperationException ex)
            {
                Assert.That((object?)ex.Message, Is.EqualTo(@"Getting the InheritedStyle property of a shared row is not a valid operation."));
                throw;
            }
        });

        Assert.That((object?)row.IsNewRow, Is.EqualTo(false), "#B row.IsNewRow");
        Assert.That((object?)row.MinimumHeight, Is.EqualTo(3), "#B row.MinimumHeight");
        Assert.That((object?)row.ReadOnly, Is.EqualTo(false), "#B row.ReadOnly");
        Assert.That((object?)row.Resizable, Is.EqualTo(DataGridViewTriState.NotSet), "#B row.Resizable");
        Assert.That((object?)row.Selected, Is.EqualTo(false), "#B row.Selected");
        Assert.That((object?)row.State, Is.EqualTo(DataGridViewElementStates.Visible), "#B row.State");
        Assert.That((object?)row.Visible, Is.EqualTo(true), "#B row.Visible");

        dgv.Columns.Add(col);

        Assert.IsNotNull(row.AccessibilityObject, "#C row.AccessibilityObject");
        Assert.IsNotNull(row.Cells, "#C row.Cells");
        Assert.IsNull(row.ContextMenuStrip, "#C row.ContextMenuStrip");
        Assert.IsNull(row.DataBoundItem, "#C row.DataBoundItem");
        Assert.IsNull(row.DataGridView, "#C row.DataGridView");
        Assert.IsNotNull(row.DefaultCellStyle, "#C row.DefaultCellStyle");
        Assert.That((object?)row.Displayed, Is.EqualTo(false), "#C row.Displayed");
        Assert.That((object?)row.DividerHeight, Is.EqualTo(0), "#C row.DividerHeight");
        Assert.That((object?)row.ErrorText, Is.EqualTo(string.Empty), "#C row.ErrorText");
        Assert.That((object?)row.Frozen, Is.EqualTo(false), "#C row.Frozen");
        Assert.IsNotNull(row.HeaderCell, "#C row.HeaderCell");
        // DPI Dependent? // Assert1.AreEqual(22, row.Height, "#C row.Height");
        Assert.That((object?)row.Index, Is.EqualTo(-1), "#C row.Index");
        Assert.Throws<InvalidOperationException>(() =>
        {
            try
            {
                object? zxf = row.InheritedStyle;
                RemoveWarning(zxf!);
            }
            catch (InvalidOperationException ex)
            {
                Assert.That((object?)ex.Message, Is.EqualTo(@"Getting the InheritedStyle property of a shared row is not a valid operation."));
                throw;
            }
        });
        Assert.That((object?)row.IsNewRow, Is.EqualTo(false), "#C row.IsNewRow");
        Assert.That((object?)row.MinimumHeight, Is.EqualTo(3), "#C row.MinimumHeight");
        Assert.That((object?)row.ReadOnly, Is.EqualTo(false), "#C row.ReadOnly");
        Assert.That((object?)row.Resizable, Is.EqualTo(DataGridViewTriState.NotSet), "#C row.Resizable");
        Assert.That((object?)row.Selected, Is.EqualTo(false), "#C row.Selected");
        Assert.That((object?)row.State, Is.EqualTo(DataGridViewElementStates.Visible), "#C row.State");
        Assert.That((object?)row.Visible, Is.EqualTo(true), "#C row.Visible");

        dgv.Rows.Add(row);

        Assert.IsNotNull(row.AccessibilityObject, "#D row.AccessibilityObject");
        Assert.IsNotNull(row.Cells, "#D row.Cells");
        Assert.Throws<InvalidOperationException>(() =>
        {
            try
            {
                object? zxf = row.ContextMenuStrip;
                RemoveWarning(zxf!);
            }
            catch (InvalidOperationException ex)
            {
                Assert.That((object?)ex.Message, Is.EqualTo(@"Operation cannot be performed on a shared row."));
                throw;
            }
        });
        Assert.IsNull(row.DataBoundItem, "#D row.DataBoundItem");
        Assert.IsNotNull(row.DataGridView, "#D row.DataGridView");
        Assert.IsNotNull(row.DefaultCellStyle, "#D row.DefaultCellStyle");
        Assert.Throws<InvalidOperationException>(() =>
        {
            try
            {
                object zxf = row.Displayed;
                RemoveWarning(zxf);
            }
            catch (InvalidOperationException ex)
            {
                Assert.That((object?)ex.Message, Is.EqualTo(@"Getting the Displayed property of a shared row is not a valid operation."));
                throw;
            }
        });
        Assert.That((object?)row.DividerHeight, Is.EqualTo(0), "#D row.DividerHeight");
        Assert.Throws<InvalidOperationException>(() =>
        {
            try
            {
                object? zxf = row.ErrorText;
                RemoveWarning(zxf!);
            }
            catch (InvalidOperationException ex)
            {
                Assert.That((object?)ex.Message, Is.EqualTo(@"Operation cannot be performed on a shared row."));
                throw;
            }
        });
        Assert.Throws<InvalidOperationException>(() =>
        {
            try
            {
                object zxf = row.Frozen;
                RemoveWarning(zxf);
            }
            catch (InvalidOperationException ex)
            {
                Assert.That((object?)ex.Message, Is.EqualTo(@"Getting the Frozen property of a shared row is not a valid operation."));
                throw;
            }
        });
        Assert.IsNotNull(row.HeaderCell, "#D row.HeaderCell");
        // DPI Dependent? // Assert1.AreEqual(22, row.Height, "#D row.Height");
        Assert.That((object?)row.Index, Is.EqualTo(-1), "#D row.Index");
        Assert.Throws<InvalidOperationException>(() =>
        {
            try
            {
                object? zxf = row.InheritedStyle;
                RemoveWarning(zxf!);
            }
            catch (InvalidOperationException ex)
            {
                Assert.That((object?)ex.Message, Is.EqualTo(@"Getting the InheritedStyle property of a shared row is not a valid operation."));
                throw;
            }
        });
        Assert.That((object?)row.IsNewRow, Is.EqualTo(false), "#D row.IsNewRow");
        Assert.That((object?)row.MinimumHeight, Is.EqualTo(3), "#D row.MinimumHeight");
        Assert.Throws<InvalidOperationException>(() =>
        {
            try
            {
                object zxf = row.ReadOnly;
                RemoveWarning(zxf);
            }
            catch (InvalidOperationException ex)
            {
                Assert.That((object?)ex.Message, Is.EqualTo(@"Getting the ReadOnly property of a shared row is not a valid operation."));
                throw;
            }
        });
        Assert.Throws<InvalidOperationException>(() =>
        {
            try
            {
                object zxf = row.Resizable;
                RemoveWarning(zxf);
            }
            catch (InvalidOperationException ex)
            {
                Assert.That((object?)ex.Message, Is.EqualTo(@"Getting the Resizable property of a shared row is not a valid operation."));
                throw;
            }
        });
        Assert.Throws<InvalidOperationException>(() =>
        {
            try
            {
                object zxf = row.Selected;
                RemoveWarning(zxf);
            }
            catch (InvalidOperationException ex)
            {
                Assert.That((object?)ex.Message, Is.EqualTo(@"Getting the Selected property of a shared row is not a valid operation."));
                throw;
            }
        });
        Assert.Throws<InvalidOperationException>(() =>
        {
            try
            {
                object zxf = row.State;
                RemoveWarning(zxf);
            }
            catch (InvalidOperationException ex)
            {
                Assert.That((object?)ex.Message, Is.EqualTo(@"Getting the State property of a shared row is not a valid operation."));
                throw;
            }
        });
        Assert.Throws<InvalidOperationException>(() =>
        {
            try
            {
                object zxf = row.Visible;
                RemoveWarning(zxf);
            }
            catch (InvalidOperationException ex)
            {
                Assert.That((object?)ex.Message, Is.EqualTo(@"Getting the Visible property of a shared row is not a valid operation."));
                throw;
            }
        });
    }

    [Test]
    public void InitialValues()
    {
        var row = new DataGridViewRow();

        Assert.IsNotNull(row.AccessibilityObject, "#A row.AccessibilityObject");
        Assert.IsNotNull(row.Cells, "#A row.Cells");
        Assert.IsNull(row.ContextMenuStrip, "#A row.ContextMenuStrip");
        Assert.IsNull(row.DataBoundItem, "#A row.DataBoundItem");
        Assert.IsNull(row.DataGridView, "#A row.DataGridView");
        Assert.IsNotNull(row.DefaultCellStyle, "#A row.DefaultCellStyle");
        Assert.That((object?)row.Displayed, Is.EqualTo(false), "#A row.Displayed");
        Assert.That((object?)row.DividerHeight, Is.EqualTo(0), "#A row.DividerHeight");
        Assert.That((object?)row.ErrorText, Is.EqualTo(string.Empty), "#A row.ErrorText");
        Assert.That((object?)row.Frozen, Is.EqualTo(false), "#A row.Frozen");
        Assert.IsNotNull(row.HeaderCell, "#A row.HeaderCell");
        // DPI Dependent? // Assert1.AreEqual(22, row.Height, "#A row.Height");
        Assert.That((object?)row.Index, Is.EqualTo(-1), "#A row.Index");
        Assert.Throws<InvalidOperationException>(() =>
        {
            try
            {
                object? zxf = row.InheritedStyle;
                RemoveWarning(zxf!);
            }
            catch (InvalidOperationException ex)
            {
                Assert.That((object?)ex.Message, Is.EqualTo(@"Getting the InheritedStyle property of a shared row is not a valid operation."));
                throw;
            }
        });
        Assert.That((object?)row.IsNewRow, Is.EqualTo(false), "#A row.IsNewRow");
        Assert.That((object?)row.MinimumHeight, Is.EqualTo(3), "#A row.MinimumHeight");
        Assert.That((object?)row.ReadOnly, Is.EqualTo(false), "#A row.ReadOnly");
        Assert.That((object?)row.Resizable, Is.EqualTo(DataGridViewTriState.NotSet), "#A row.Resizable");
        Assert.That((object?)row.Selected, Is.EqualTo(false), "#A row.Selected");
        Assert.That((object?)row.State, Is.EqualTo(DataGridViewElementStates.Visible), "#A row.State");
        Assert.That((object?)row.Visible, Is.EqualTo(true), "#A row.Visible");
    }


    private class MockDataGridViewCellCollection : DataGridViewCellCollection
    {
        public MockDataGridViewCellCollection(DataGridViewRow dataGridViewRow) : base(dataGridViewRow)
        {
        }
    }
}