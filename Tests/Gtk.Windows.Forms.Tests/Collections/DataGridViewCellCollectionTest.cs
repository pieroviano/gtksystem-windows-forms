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
// Authors:
//	Gert Driesen <drieseng@users.sourceforge.net>
// 
// Copyright (c) 2007 Gert Driesen

using System.Data;
using System.Windows.Forms;
using GtkTests.Helpers;

namespace GtkTests.Collections;

[TestFixture]
public class DataGridViewCellCollectionTest : TestHelper
{
    private DataGridView? _dataGridView;

    [TearDown]
    protected override void TearDown()
    {
        _dataGridView?.Dispose();
    }

    [SetUp]
    protected override void SetUp()
    {
        var dt = new DataTable();
        dt.Columns.Add("Date", typeof(DateTime));
        dt.Columns.Add("Registered", typeof(bool));
        dt.Columns.Add("Event", typeof(string));

        var row = dt.NewRow();
        row["Date"] = new DateTime(2007, 2, 3);
        row["Event"] = "one";
        row["Registered"] = false;
        dt.Rows.Add(row);

        row = dt.NewRow();
        row["Date"] = new DateTime(2008, 3, 4);
        row["Event"] = "two";
        row["Registered"] = true;
        dt.Rows.Add(row);

        _dataGridView = new DataGridView();
        _dataGridView.DataSource = dt;
        base.SetUp();
    }

    [Test]
    public async Task Indexer_ColumnName()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        form.Controls.Add(_dataGridView);
        var taskCompletionSource = new TaskCompletionSource<string>();
        form.Load += (_, _) =>
        {
            var cells = _dataGridView!.Rows[0]!.Cells;

            var dateCell = cells["Date"];
            Assert.IsNotNull(dateCell);
            Assert.IsNotNull(dateCell.OwningColumn);
            Assert.That((object?)dateCell.OwningColumn.Name, Is.EqualTo("Date"));
            Assert.IsNotNull(dateCell.Value);
            object expected = new DateTime(2007, 2, 3);
            Assert.That(dateCell.Value, Is.EqualTo(expected));

            var eventCell = cells["eVeNT"];
            Assert.IsNotNull(eventCell);
            Assert.IsNotNull(eventCell.OwningColumn);
            Assert.That((object?)eventCell.OwningColumn.Name, Is.EqualTo("Event"));
            Assert.IsNotNull(eventCell.Value);
            Assert.That(eventCell.Value, Is.EqualTo("one"));

            var registeredCell = cells["Registered"];
            Assert.IsNotNull(registeredCell);
            Assert.IsNotNull(registeredCell.OwningColumn);
            Assert.That((object?)registeredCell.OwningColumn.Name, Is.EqualTo("Registered"));
            Assert.IsNotNull(registeredCell.Value);
            Assert.That(registeredCell.Value, Is.EqualTo(false));

            form.Dispose();
            taskCompletionSource.SetResult(string.Empty);
        };
        form.Show();
        await taskCompletionSource.Task;
    }

    [Test]
    public void Indexer_ColumnName_NotFound()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        form.Controls.Add(_dataGridView);
        form.Load += (_, _) =>
        {
            Assert.Throws<ArgumentException>(() =>
            {
                try
                {
                    _ = _dataGridView!.Rows[0]!.Cells["DoesNotExist"];
                }
                catch (ArgumentException ex)
                {
                    // Column named DoesNotExist cannot be found
                    object expected = typeof(ArgumentException);
                    Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                    Assert.IsNull(ex.InnerException);
                    Assert.IsNotNull(ex.Message);
                    Assert.IsTrue(ex.Message.IndexOf("DoesNotExist", StringComparison.Ordinal) != -1);
                    Assert.IsNotNull(ex.ParamName);
                    Assert.That((object?)ex.ParamName, Is.EqualTo("columnName"));
                    throw;
                }
            });

            Assert.Throws<ArgumentException>(() =>
            {
                try
                {
                    _ = _dataGridView!.Rows[0]!.Cells[string.Empty];
                }
                catch (ArgumentException ex)
                {
                    // Column named DoesNotExist cannot be found
                    object expected = typeof(ArgumentException);
                    Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                    Assert.IsNull(ex.InnerException);
                    Assert.IsNotNull(ex.Message);
                    Assert.IsTrue(ex.Message.IndexOf("  ", StringComparison.Ordinal) != -1);
                    Assert.IsNotNull(ex.ParamName);
                    Assert.That((object?)ex.ParamName, Is.EqualTo("columnName"));
                    throw;
                }
            });

            form.Dispose();
        };
        form.ShowDialog();
    }

    [Test]
    public void Indexer_ColumnName_Null()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        form.Controls.Add(_dataGridView);
        form.Load += (_, _) =>
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                try
                {
                    _ = _dataGridView!.Rows[0]!.Cells[null!];
                }
                catch (ArgumentNullException ex)
                {
                    object expected = typeof(ArgumentNullException);
                    Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                    Assert.IsNull(ex.InnerException);
                    Assert.IsNotNull(ex.Message);
                    Assert.IsNotNull(ex.ParamName);
                    Assert.That((object?)ex.ParamName, Is.EqualTo("columnName"));
                    throw;
                }
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                try
                {
                    _dataGridView!.Rows[0]!.Cells[null!] = _dataGridView!.Rows[0]!.Cells[0];
                }
                catch (ArgumentNullException ex)
                {
                    object expected = typeof(ArgumentNullException);
                    Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                    Assert.IsNull(ex.InnerException);
                    Assert.IsNotNull(ex.Message);
                    Assert.IsNotNull(ex.ParamName);
                    Assert.That((object?)ex.ParamName, Is.EqualTo("columnName"));
                    throw;
                }
            });

            form.Dispose();
        };
        form.Show();
    }

    [Test]
    [Category("NotWorking")]
    public void Indexer_Index()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        form.Controls.Add(_dataGridView);
        form.Load += (_, _) =>
        {
            var cells = _dataGridView!.Rows[0]!.Cells;

            var dateCell = cells[0];
            Assert.IsNotNull(dateCell);
            Assert.IsNotNull(dateCell.OwningColumn);
            Assert.That((object?)dateCell.OwningColumn.Name, Is.EqualTo("Date"));
            Assert.IsNotNull(dateCell.Value);
            object expected = new DateTime(2007, 2, 3);
            Assert.That(dateCell.Value, Is.EqualTo(expected));

            var eventCell = cells[2];
            Assert.IsNotNull(eventCell);
            Assert.IsNotNull(eventCell.OwningColumn);
            Assert.That((object?)eventCell.OwningColumn.Name, Is.EqualTo("Event"));
            Assert.IsNotNull(eventCell.Value);
            Assert.That(eventCell.Value, Is.EqualTo("one"));

            var registeredCell = cells[1];
            Assert.IsNotNull(registeredCell);
            Assert.IsNotNull(registeredCell.OwningColumn);
            Assert.That((object?)registeredCell.OwningColumn.Name, Is.EqualTo("Registered"));
            Assert.IsNotNull(registeredCell.Value);
            Assert.That(registeredCell.Value, Is.EqualTo(false));

            form.Dispose();
        };
        form.Show();
    }

    [Test]
    public void Indexer_Index_Negative()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        form.Controls.Add(_dataGridView);
        form.Load += (_, _) =>
        {
            var cells = _dataGridView!.Rows[0]!.Cells;

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                try
                {
                    _ = cells[-1];
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    // Index was out of range. Must be non-negative
                    // and less than the size of the collection
                    object expected = typeof(ArgumentOutOfRangeException);
                    Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                    Assert.IsNull(ex.InnerException);
                    Assert.IsNotNull(ex.Message);
                    Assert.IsNotNull(ex.ParamName);
                    Assert.That((object?)ex.ParamName, Is.EqualTo("index"));
                    throw;
                }
            });

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                try
                {
                    cells[-1] = new MockDataGridViewCell();
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    // Index was out of range. Must be non-negative
                    // and less than the size of the collection
                    object expected = typeof(ArgumentOutOfRangeException);
                    Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                    Assert.IsNull(ex.InnerException);
                    Assert.IsNotNull(ex.Message);
                    Assert.IsNotNull(ex.ParamName);
                    Assert.That((object?)ex.ParamName, Is.EqualTo("index"));
                    throw;
                }
            });

            form.Close();
        };
        form.Show();
    }

    [Test]
    public void Indexer_Index_Overflow()
    {
        var row = new DataGridViewRow();
        var cells = row.Cells;
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            try
            {
                _ = cells[0];
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Index was out of range. Must be non-negative
                // and less than the size of the collection
                object expected = typeof(ArgumentOutOfRangeException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                Assert.IsNotNull(ex.ParamName);
                Assert.That((object?)ex.ParamName, Is.EqualTo("index"));
                throw;
            }
        });
    }

    [Test]
    public void Indexer_Value_Null()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        form.Controls.Add(_dataGridView);
        form.Load += (_, _) =>
        {
            var cells = _dataGridView!.Rows[0]!.Cells;

            Assert.Throws<ArgumentNullException>(() =>
            {
                try
                {
                    cells["Date"] = null;
                }
                catch (ArgumentNullException ex)
                {
                    object expected = typeof(ArgumentNullException);
                    Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                    Assert.IsNull(ex.InnerException);
                    Assert.IsNotNull(ex.Message);
                    Assert.IsNotNull(ex.ParamName);
                    Assert.That((object?)ex.ParamName, Is.EqualTo("value"));
                    throw;
                }
            });

            Assert.Throws<ArgumentNullException>(() =>
            {
                try
                {
                    cells[0] = null;
                }
                catch (ArgumentNullException ex)
                {
                    object expected = typeof(ArgumentNullException);
                    Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                    Assert.IsNull(ex.InnerException);
                    Assert.IsNotNull(ex.Message);
                    Assert.IsNotNull(ex.ParamName);
                    Assert.That((object?)ex.ParamName, Is.EqualTo("value"));
                    throw;
                }
            });

            form.Dispose();
        };
        form.Show();

    }

    private class MockDataGridViewCell : DataGridViewCell
    {
    }
}