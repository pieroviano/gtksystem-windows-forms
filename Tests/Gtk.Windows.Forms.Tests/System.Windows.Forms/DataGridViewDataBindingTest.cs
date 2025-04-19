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
// Copyright (c) 2008 Novell, Inc. (http://www.novell.com)
//
// Author:
//	Jonathan Pobst  (monkey@jpobst.com)
//

using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using GtkTests.Helpers;

namespace GtkTests.System.Windows.Forms.DataGridViewBindingTest;

[TestFixture]
public class DataSetBindingTest : TestHelper
{
    [Test]
    public void TestDataSet()
    {
        // Binding to a DataSet doesn't work unless you specify DataMember
        var f = new Form();
        f.ShowInTaskbar = false;

        var ds = new DataSet();

        var dt = ds.Tables.Add("Muppets");

        dt.Columns.Add("ID");
        dt.Columns.Add("Name");
        dt.Columns.Add("Sex");

        dt.Rows.Add(1, "Kermit", "Male");
        dt.Rows.Add(2, "Miss Piggy", "Female");
        dt.Rows.Add(3, "Gonzo", "Male");

        var dgv = new DataGridView();
        dgv.DataSource = ds;

        f.Controls.Add(dgv);
        f.Show();

        Assert.That((object?)dgv.Columns.Count, Is.EqualTo(0));

        dgv.DataMember = "Muppets";

        Assert.That((object?)dgv.Columns.Count, Is.EqualTo(3));

        f.Dispose();
    }

    [Test]
    public void TestBasic()
    {
        // Binding to a basic DataTable
        var f = new Form();
        f.ShowInTaskbar = false;

        var ds = new DataSet();

        var dt = ds.Tables.Add("Muppets");

        dt.Columns.Add("ID");
        dt.Columns.Add("Name");
        dt.Columns.Add("Sex");

        dt.Rows.Add(1, "Kermit", "Male");
        dt.Rows.Add(2, "Miss Piggy", "Female");
        dt.Rows.Add(3, "Gonzo", "Male");

        var dgv = new DataGridView();
        dgv.DataSource = dt;

        f.Controls.Add(dgv);
        f.Show();

        Assert.That((object?)dgv.Columns.Count, Is.EqualTo(3));
        Assert.That((object?)dgv.Rows.SharedList.Count, Is.EqualTo(4));

        Assert.That((object?)dgv.Columns[0].Name, Is.EqualTo("ID"));
        Assert.That((object?)dgv.Columns[0].DataPropertyName, Is.EqualTo("ID"));
        Assert.That((object?)dgv.Columns[0].DisplayIndex, Is.EqualTo(0));
        Assert.That((object?)dgv.Columns[0].HeaderText, Is.EqualTo("ID"));
        Assert.That((object?)dgv.Columns[0].Index, Is.EqualTo(0));
        Assert.That((object?)dgv.Columns[0].IsDataBound, Is.EqualTo(true));
        Assert.That((object?)dgv.Columns[0].ReadOnly, Is.EqualTo(false));
        Assert.That((object?)dgv.Columns[0].Visible, Is.EqualTo(true));
        Assert.That((object?)dgv.Columns[0].CellType?.ToString(), Is.EqualTo("System.Windows.Forms.DataGridViewTextBoxCell"));
        Assert.That((object?)dgv.Columns[0].ValueType?.ToString(), Is.EqualTo("System.String"), "A11-B");
        Assert.That((object?)dgv.Columns[0].GetType().ToString(), Is.EqualTo("System.Windows.Forms.DataGridViewTextBoxColumn"), "A11-C");

        Assert.That((object?)dgv.Columns[1].Name, Is.EqualTo("Name"));
        Assert.That((object?)dgv.Columns[1].DataPropertyName, Is.EqualTo("Name"));
        Assert.That((object?)dgv.Columns[1].DisplayIndex, Is.EqualTo(1));
        Assert.That((object?)dgv.Columns[1].HeaderText, Is.EqualTo("Name"));
        Assert.That((object?)dgv.Columns[1].Index, Is.EqualTo(1));
        Assert.That((object?)dgv.Columns[1].IsDataBound, Is.EqualTo(true));
        Assert.That((object?)dgv.Columns[1].ReadOnly, Is.EqualTo(false));
        Assert.That((object?)dgv.Columns[1].Visible, Is.EqualTo(true));
        Assert.That((object?)dgv.Columns[1].CellType?.ToString(), Is.EqualTo("System.Windows.Forms.DataGridViewTextBoxCell"));
        Assert.That((object?)dgv.Columns[1].ValueType?.ToString(), Is.EqualTo("System.String"), "A20-B");

        Assert.That((object?)dgv.Columns[2].Name, Is.EqualTo("Sex"));
        Assert.That((object?)dgv.Columns[2].DataPropertyName, Is.EqualTo("Sex"));
        Assert.That((object?)dgv.Columns[2].DisplayIndex, Is.EqualTo(2));
        Assert.That((object?)dgv.Columns[2].HeaderText, Is.EqualTo("Sex"));
        Assert.That((object?)dgv.Columns[2].Index, Is.EqualTo(2));
        Assert.That((object?)dgv.Columns[2].IsDataBound, Is.EqualTo(true));
        Assert.That((object?)dgv.Columns[2].ReadOnly, Is.EqualTo(false));
        Assert.That((object?)dgv.Columns[2].Visible, Is.EqualTo(true));
        Assert.That((object?)dgv.Columns[2].CellType?.ToString(), Is.EqualTo("System.Windows.Forms.DataGridViewTextBoxCell"));
        Assert.That((object?)dgv.Columns[2].ValueType?.ToString(), Is.EqualTo("System.String"), "A29-B");

        Assert.That(dgv.Rows[0]!.Cells[0]!.Value, Is.EqualTo("1"));
        Assert.That(dgv.Rows[0]!.Cells[1]!.Value, Is.EqualTo("Kermit"));
        Assert.That(dgv.Rows[0]!.Cells[2]!.Value, Is.EqualTo("Male"));
        Assert.That(dgv.Rows[1]!.Cells[0]!.Value, Is.EqualTo("2"));
        Assert.That(dgv.Rows[1]!.Cells[1]!.Value, Is.EqualTo("Miss Piggy"));
        Assert.That(dgv.Rows[1]!.Cells[2]!.Value, Is.EqualTo("Female"));
        Assert.That(dgv.Rows[2]!.Cells[0]!.Value, Is.EqualTo("3"));
        Assert.That(dgv.Rows[2]!.Cells[1]!.Value, Is.EqualTo("Gonzo"));
        Assert.That(dgv.Rows[2]!.Cells[2]!.Value, Is.EqualTo("Male"));

        f.Dispose();
    }

    [Test]
    public void TestCheckBoxColumn()
    {
        // Binding to a basic DataTable with a boolean value
        var f = new Form();
        f.ShowInTaskbar = false;

        var ds = new DataSet();

        var dt = ds.Tables.Add("Muppets");

        dt.Columns.Add("ID");
        dt.Columns.Add("Name");
        dt.Columns.Add("IsFunny", typeof(bool));

        dt.Rows.Add(1, "Kermit", "true");
        dt.Rows.Add(2, "Miss Piggy", "false");
        dt.Rows.Add(3, "Gonzo", DBNull.Value);
        dt.Rows.Add(4, "Animal", true);
        dt.Rows.Add(5, "Fozzy", false);
        dt.Rows.Add(6, "Beaker", "TRUE");
        dt.Rows.Add(7, "Bunsen", "fALSe");
        dt.Rows.Add(8, "Sweedish Chef", 1);
        dt.Rows.Add(9, "Rolf", 0);

        var dgv = new DataGridView();
        dgv.DataSource = dt;

        f.Controls.Add(dgv);
        f.Show();

        Assert.That((object?)dgv.Columns.Count, Is.EqualTo(3));
        Assert.That((object?)dgv.Rows.SharedList.Count, Is.EqualTo(10));

        Assert.That((object?)dgv.Columns[2].Name, Is.EqualTo("IsFunny"));
        Assert.That((object?)dgv.Columns[2].DataPropertyName, Is.EqualTo("IsFunny"));
        Assert.That((object?)dgv.Columns[2].DisplayIndex, Is.EqualTo(2));
        Assert.That((object?)dgv.Columns[2].HeaderText, Is.EqualTo("IsFunny"));
        Assert.That((object?)dgv.Columns[2].Index, Is.EqualTo(2));
        Assert.That((object?)dgv.Columns[2].IsDataBound, Is.EqualTo(true));
        Assert.That((object?)dgv.Columns[2].ReadOnly, Is.EqualTo(false));
        Assert.That((object?)dgv.Columns[2].Visible, Is.EqualTo(true));
        Assert.That((object?)dgv.Columns[2].CellType?.ToString(), Is.EqualTo("System.Windows.Forms.DataGridViewCheckBoxCell"));
        Assert.That((object?)dgv.Columns[2].ValueType?.ToString(), Is.EqualTo("System.Boolean"));
        Assert.That((object?)dgv.Columns[2].GetType().ToString(), Is.EqualTo("System.Windows.Forms.DataGridViewCheckBoxColumn"), "A12-B");

        Assert.That(dgv.Rows[0]!.Cells[2]!.Value, Is.EqualTo(true));
        Assert.That(dgv.Rows[1]!.Cells[2]!.Value, Is.EqualTo(false));
        Assert.That(dgv.Rows[2]!.Cells[2]!.Value, Is.EqualTo(DBNull.Value));
        Assert.That(dgv.Rows[3]!.Cells[2]!.Value, Is.EqualTo(true));
        Assert.That(dgv.Rows[4]!.Cells[2]!.Value, Is.EqualTo(false));
        Assert.That(dgv.Rows[5]!.Cells[2]!.Value, Is.EqualTo(true));
        Assert.That(dgv.Rows[6]!.Cells[2]!.Value, Is.EqualTo(false));
        Assert.That(dgv.Rows[7]!.Cells[2]!.Value, Is.EqualTo(true));
        Assert.That(dgv.Rows[8]!.Cells[2]!.Value, Is.EqualTo(false));

        Assert.That((object?)dgv.Rows[8]!.Cells[2]!.GetType().ToString(), Is.EqualTo("System.Windows.Forms.DataGridViewCheckBoxCell"));

        f.Dispose();
    }

    [Test]
    public void TestAutoGenerateColumns()
    {
        // Binding when AutoGenerateColumns is false
        var f = new Form();
        f.ShowInTaskbar = false;

        var ds = new DataSet();

        var dt = ds.Tables.Add("Muppets");

        dt.Columns.Add("ID");
        dt.Columns.Add("Name");

        dt.Rows.Add(1, "Kermit");
        dt.Rows.Add(2, "Miss Piggy");
        dt.Rows.Add(3, "Gonzo");

        var dgv = new DataGridView();
        dgv.DataSource = dt;

        f.Controls.Add(dgv);
        f.Show();

        Assert.That((object?)dgv.Columns.Count, Is.EqualTo(0));
        Assert.That((object?)dgv.Rows.SharedList.Count, Is.EqualTo(0));

        dgv.DataSource = null;

        var col1 = new DataGridViewTextBoxColumn();
        col1.DataPropertyName = "Name";
        dgv.Columns.Add(col1);

        dgv.DataSource = dt;

        Assert.That((object?)dgv.Columns.Count, Is.EqualTo(1));
        Assert.That((object?)dgv.Rows.SharedList.Count, Is.EqualTo(4));

        Assert.That(dgv.Rows[0]!.Cells[0]!.Value, Is.EqualTo("Kermit"));

        dgv.DataSource = null;

        var col2 = new DataGridViewTextBoxColumn();
        col2.DataPropertyName = "id";
        dgv.Columns.Add(col2);

        dgv.DataSource = dt;

        Assert.That((object?)dgv.ColumnCount, Is.EqualTo(2));
        Assert.That((object?)dgv.RowCount, Is.EqualTo(4));

        Assert.That(dgv.Rows[0]!.Cells[0]!.Value, Is.EqualTo("Kermit"));
        Assert.That(dgv.Rows[0]!.Cells[1]!.Value, Is.EqualTo("1"));

        f.Dispose();
    }

    [Test]	// Bug #399601
    public void TestAddingWithoutAutoGenerate()
    {
        // Binding when AutoGenerateColumns is false
        // and adding rows to the dataset
        var f = new Form();
        f.ShowInTaskbar = false;

        var ds = new DataSet();

        var dt = ds.Tables.Add("Muppets");

        dt.Columns.Add("ID");
        dt.Columns.Add("Name");

        var dgv = new DataGridView();

        var col1 = new DataGridViewTextBoxColumn();
        col1.DataPropertyName = "Name";
        dgv.Columns.Add(col1);

        dgv.DataSource = dt;

        f.Controls.Add(dgv);
        f.Show();

        dt.Rows.Add(1, "Kermit");
        dt.Rows.Add(2, "Miss Piggy");
        dt.Rows.Add(3, "Gonzo");

        Assert.That((object?)dgv.ColumnCount, Is.EqualTo(1));
        Assert.That((object?)dgv.RowCount, Is.EqualTo(3));

        f.Dispose();
    }

    [Test]
    public void TestDeleting()
    {
        // Binding when AutoGenerateColumns is false
        // and deleting rows from the dataset and DGV
        var f = new Form();
        f.ShowInTaskbar = false;

        var ds = new DataSet();

        var dt = ds.Tables.Add("Muppets");

        dt.Columns.Add("ID");
        dt.Columns.Add("Name");

        var dgv = new DataGridView();

        var col1 = new DataGridViewTextBoxColumn();
        col1.DataPropertyName = "Name";
        dgv.Columns.Add(col1);

        dgv.DataSource = dt;

        f.Controls.Add(dgv);
        f.Show();

        dt.Rows.Add(1, "Kermit");
        dt.Rows.Add(2, "Miss Piggy");
        dt.Rows.Add(3, "Gonzo");

        Assert.That((object?)dgv.ColumnCount, Is.EqualTo(1));
        Assert.That((object?)dgv.RowCount, Is.EqualTo(3));

        dt.Rows[2].Delete();
        Assert.That((object?)dgv.RowCount, Is.EqualTo(2));

        dgv.Rows.RemoveAt(0);
        Assert.That((object?)dgv.RowCount, Is.EqualTo(1));

        f.Dispose();
    }

    [Test]
    public void TestChangingDataSetAfterSettingDataSource()
    {
        // Binding when AutoGenerateColumns is false
        // and deleting rows from the dataset and DGV
        var f = new Form();
        f.ShowInTaskbar = false;

        var ds = new DataSet();

        var dgv = new DataGridView();

        var col1 = new DataGridViewTextBoxColumn();
        col1.DataPropertyName = "Name";
        dgv.Columns.Add(col1);

        dgv.DataSource = ds;
        dgv.DataMember = "Muppets";

        var dt = ds.Tables.Add("Muppets");

        dt.Columns.Add("ID");
        dt.Columns.Add("Name");

        f.Controls.Add(dgv);
        f.Show();

        dt.Rows.Add(1, "Kermit");
        dt.Rows.Add(2, "Miss Piggy");
        dt.Rows.Add(3, "Gonzo");

        Assert.That((object?)dgv.ColumnCount, Is.EqualTo(1));
        Assert.That((object?)dgv.RowCount, Is.EqualTo(3));

        dt.Rows[2].Delete();
        Assert.That((object?)dgv.RowCount, Is.EqualTo(2));

        dgv.Rows.RemoveAt(0);
        Assert.That((object?)dgv.RowCount, Is.EqualTo(1));

        f.Dispose();
    }

    [Test]  // bug #448005
    public void TestClearing()
    {
        // Binding to a DataSet doesn't work unless you specify DataMember
        var f = new Form();
        f.ShowInTaskbar = false;

        var ds = new DataSet();

        var dt = ds.Tables.Add("Muppets");

        dt.Columns.Add("ID");
        dt.Columns.Add("Name");
        dt.Columns.Add("Sex");

        dt.Rows.Add(1, "Kermit", "Male");
        dt.Rows.Add(2, "Miss Piggy", "Female");
        dt.Rows.Add(3, "Gonzo", "Male");

        var dgv = new DataGridView();
        dgv.DataSource = ds;

        f.Controls.Add(dgv);
        f.Show();

        dgv.DataMember = "Muppets";

        Assert.That((object?)dgv.Columns.Count, Is.EqualTo(3));
        Assert.That((object?)dgv.Rows.Count, Is.EqualTo(4));

        ds.Tables[0].Clear();

        Assert.That((object?)dgv.Columns.Count, Is.EqualTo(3));
        Assert.That((object?)dgv.Rows.Count, Is.EqualTo(1));

        f.Dispose();
    }

    [Test]  // bug #462019
    public void TestCreatingColumnsAfterBind()
    {
        // When columns are added, we need to rebind.
        var f = new Form();
        f.ShowInTaskbar = false;

        var ds = new DataSet();

        var dt = ds.Tables.Add("Muppets");

        dt.Columns.Add("ID");
        dt.Columns.Add("Name");
        dt.Columns.Add("Sex");

        dt.Rows.Add(1, "Kermit", "Male");
        dt.Rows.Add(2, "Miss Piggy", "Female");
        dt.Rows.Add(3, "Gonzo", "Male");

        var dgv = new DataGridView();
        dgv.DataSource = ds;
        dgv.DataMember = "Muppets";

        f.Controls.Add(dgv);
        f.Show();

        Assert.That((object?)dgv.Rows.Count, Is.EqualTo(0));

        DataGridViewColumn col = new DataGridViewTextBoxColumn();
        col.DataPropertyName = "ID";
        dgv.Columns.Add(col);

        Assert.That((object?)dgv.Rows.Count, Is.EqualTo(3));

        f.Dispose();
    }
}

[TestFixture]
public class BindingListTest : TestHelper
{
    [Test]	// bug #325239
    public void TestNullItemInList()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        // The list contains one object, but the object is null
        IList<Customer> list = new Customer[1];

        var dgv = new DataGridView();
        dgv.DataSource = new BindingList<Customer>(list);

        f.Controls.Add(dgv);
        f.Show();

        Assert.That((object?)dgv.ColumnCount, Is.EqualTo(1));
        Assert.That((object?)dgv.RowCount, Is.EqualTo(2));

        f.Dispose();
    }

    private class Customer
    {
        private string name;

        public string Name
        {
            get => name;
            set => name = value;
        }
    }
}

[TestFixture]
public class ArrayTest : TestHelper
{
    [Test]	// bug #337470
    public void TestNestedCollections()
    {
        // The grid should not accept collection properties, like Names
        var f = new Form();
        f.ShowInTaskbar = false;

        Array customers = new Customer[1];
        customers.SetValue(new Customer(), 0);

        var dgv = new DataGridView();
        dgv.DataSource = customers;

        f.Controls.Add(dgv);
        f.Show();

        Assert.That((object?)dgv.ColumnCount, Is.EqualTo(1));
        Assert.That((object?)dgv.Columns[0].Name, Is.EqualTo("Name"));

        f.Dispose();
    }

    private class Customer
    {
        public string Name => "Kermit";
        public string[] Names { get { return ["Kermit", "Gonzo"]; } }
    }
}

[TestFixture]
public class BindingSourceTest : TestHelper
{
    [Test]	// bug #345483
    public void TestBindingSource()
    {
        // The grid has to extract the List from the BindingSource
        var f = new Form();
        f.ShowInTaskbar = false;

        var BindingSource = new BindingSource();

        var dataSet1 = new DataSet();

        dataSet1.Tables.Add();
        dataSet1.Tables[0].Columns.Add();
        dataSet1.Tables[0].Columns.Add();
        dataSet1.Tables[0].Columns.Add();
        dataSet1.Tables[0].Columns.Add();
        dataSet1.Tables[0].Columns.Add();
        dataSet1.Tables[0].Rows.Add("111111", "222222", "333333", "444444", "555555");

        BindingSource.DataSource = dataSet1.Tables[0];

        var dgv = new DataGridView();
        dgv.DataSource = BindingSource;

        f.Controls.Add(dgv);
        f.Show();

        Assert.That((object?)dgv.ColumnCount, Is.EqualTo(5));
        Assert.That((object?)dgv.RowCount, Is.EqualTo(2));

        Assert.That((object?)dgv.Columns[0].Name, Is.EqualTo("Column1"));
        Assert.That(dgv.Rows[0]!.Cells[0]!.Value, Is.EqualTo("111111"));

        f.Dispose();
    }

    private class Customer
    {
        public string Name => "Kermit";
        public string[] Names { get { return ["Kermit", "Gonzo"]; } }
    }
}