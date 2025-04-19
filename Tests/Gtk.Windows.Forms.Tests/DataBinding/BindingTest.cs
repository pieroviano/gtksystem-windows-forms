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
// Copyright (c) 2006 Novell, Inc.
//
// Authors:
//	Jackson Harper	jackson@ximian.com

using GtkTests.Helpers;
using GtkTests.System.Windows.Forms;
using System.Data;
using System.Diagnostics.Tracing;
using System.Windows.Forms;

namespace GtkTests.DataBinding;

[TestFixture]
public class BindingTest : TestHelper
{

    [Test]
    public void CtorTest()
    {
        var prop = "PROPERTY NAME";
        var data_source = new object();
        var data_member = "DATA MEMBER";
        var b = new Binding(prop, data_source, data_member);

        Assert.IsNull(b.BindingManagerBase, "ctor1");
        Assert.IsNotNull(b.BindingMemberInfo, "ctor2");
        Assert.IsNull(b.Control, "ctor3");

        Assert.That((object?)prop, Is.SameAs(b.PropertyName), "ctor5");
        Assert.That(data_source, Is.SameAs(b.DataSource), "ctor6");

        Assert.That((object?)b.FormattingEnabled, Is.EqualTo(true), "ctor7");
        object expected = string.Empty;
        Assert.That((object?)b.FormatString, Is.EqualTo(expected), "ctor8");
        Assert.IsNull(b.FormatInfo, "ctor9");
        Assert.IsNull(b.NullValue, "ctor10");
        Assert.That(b.DataSourceNullValue, Is.EqualTo(Convert.DBNull), "ctor11");
    }

    [Test]
    public void CtorNullTest()
    {
        var b = new Binding(null!, null!, null);

        Assert.IsNull(b.PropertyName, "ctornull1");
        Assert.IsNull(b.DataSource, "ctornull2");
    }

    [Test]
    public void BindingManagerBaseTest()
    {
        var c1 = new Control();
        var c2 = new Control();

        c1.BindingContext = new BindingContext();
        c2.BindingContext = c1.BindingContext;

        var binding = c2.DataBindings!.Add("Text", c1, "Text");

        Assert.IsNull(binding.BindingManagerBase, "1");

        c1.CreateControl();
        c2.CreateControl();

        Assert.IsNull(binding.BindingManagerBase, "2");
    }

    [Test]
    /* create control and set binding context */
    public void BindingContextChangedTest()
    {
        var c = new Control();
        // Test BindingContextChanged Event
        c.BindingContextChanged += Event_Handler1;
        var bcG1 = new BindingContext();
        eventcount = 0;
        c.BindingContext = bcG1;
        Assert.That((object?)eventcount, Is.EqualTo(1));
    }

    [Test]
    /* create control and show control */
    public void BindingContextChangedTest2()
    {
        var f = new Form();
        f.ShowInTaskbar = false;
        var c = new Control();
        f.Controls.Add(c);

        c.BindingContextChanged += Event_Handler1;
        eventcount = 0;
        f.Show();
        Assert.That((object?)eventcount, Is.EqualTo(1));
        f.Dispose();
    }

    [Test]
    /* create control, set binding context, and show control */
    public void BindingContextChangedTest3()
    {
        eventcount = 0;
        var f = new Form();
        f.ShowInTaskbar = false;

        var c = new Control();
        f.Controls.Add(c);

        c.BindingContextChanged += Event_Handler1;
        eventcount = 0;
        c.BindingContext = new BindingContext();
        f.Show();
        Assert.That((object?)eventcount, Is.GreaterThanOrEqualTo(1));
        f.Dispose();
    }

    [Test]
    public void BindingContextChangedTest4()
    {
        eventcount = 0;
        var f = new Form();
        f.ShowInTaskbar = false;

        var cc = new ContainerControl();

        var c = new Control();
        f.Controls.Add(cc);
        cc.Controls.Add(c);

        c.BindingContextChanged += Event_Handler1;
        cc.BindingContextChanged += Event_Handler1;
        f.BindingContextChanged += Event_Handler1;

        eventcount = 0;
        f.Show();
        Assert.That((object?)eventcount, Is.GreaterThanOrEqualTo(3));
        f.Dispose();
    }

    private int eventcount;
    public void Event_Handler1(object? sender, EventArgs e)
    {
        eventcount++;
    }

    [Test]
    public void DataBindingCountTest1()
    {
        var c = new Control();
        Assert.That((object?)c.DataBindings!.Count, Is.EqualTo(0), "1");
        c.DataBindings.Add(new Binding("Text", c, "Name"));
        Assert.That((object?)c.DataBindings.Count, Is.EqualTo(1), "2");

        var b = c.DataBindings[0];
        Assert.That((object?)b.Control, Is.EqualTo(c), "3");
        Assert.That(b.DataSource, Is.EqualTo(c), "4");
        Assert.That((object?)b.PropertyName, Is.EqualTo("Text"), "5");
        Assert.That((object?)b.BindingMemberInfo!.Value.BindingField, Is.EqualTo("Name"), "6");
    }

    [Test]
    public void DataBindingCountTest2()
    {
        var c = new Control();
        var c2 = new Control();
        Assert.That((object?)c.DataBindings!.Count, Is.EqualTo(0), "1");
        c.DataBindings.Add(new Binding("Text", c2, "Name"));
        Assert.That((object?)c.DataBindings.Count, Is.EqualTo(1), "2");
        Assert.That((object?)c2.DataBindings!.Count, Is.EqualTo(0), "3");

        var b = c.DataBindings[0];
        Assert.That((object?)b.Control, Is.EqualTo(c), "4");
        Assert.That(b.DataSource, Is.EqualTo(c2), "5");
        Assert.That((object?)b.PropertyName, Is.EqualTo("Text"), "6");
        Assert.That((object?)b.BindingMemberInfo!.Value.BindingField, Is.EqualTo("Name"), "7");
    }

    [Test]
    public void DataSourceNullTest()
    {
        var item = new ChildMockItem();
        var c = new Control();
        c.Tag = null;
        item.ObjectValue = null!;

        c.DataBindings!.Add("Tag", item, "ObjectValue");

        var f = new Form();
        f.Controls.Add(c);

        f.Show(); // Need this to init data binding

        Assert.That(c.Tag, Is.EqualTo(DBNull.Value), "1");

        f.Dispose();

    }

    // For this case to work, the data source property needs
    // to have an associated 'PropertyChanged' event.
    [Test]
    public void DataSourcePropertyChanged()
    {
        var c = new Control();
        c.BindingContext = new BindingContext();
        c.CreateControl();

        var item = new MockItem("A", 0);
        var binding = new Binding("Text", item, "Text");

        c.DataBindings!.Add(binding);
        Assert.That((object?)c.Text, Is.EqualTo("A"));

        item.Text = "B";
        Assert.That((object?)c.Text, Is.EqualTo("B"));
    }

    [Test]
    public void DataSourcePropertyChanged_Original()
    {
        var c = new Control();
        c.BindingContext = new BindingContext();
        c.CreateControl();

        var item = new MockItem("A", 0);
        var binding = new Binding("Text", item, "Text");

        c.DataBindings!.Add(binding);
        Assert.That((object?)c.Text, Is.EqualTo("A"));

        item.Text = "B";
        Assert.That((object?)c.Text, Is.EqualTo("B"));
    }

    [Test]
    public void DataSourcePropertyChanged_Original_BadName()
    {
        var c = new Control();
        c.BindingContext = new BindingContext();
        c.CreateControl();

        var item = new MockItem("A", 0);
        var binding = new Binding("Text", item, "xxxxxxTextXXXXX");

        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                c.DataBindings!.Add(binding);
            }
            catch (ArgumentException ex)
            {
                Assert.That((object?)ex.ParamName, Is.EqualTo("_dataMember"), "ex.ParamName"); // (test is not locale dependent)
                throw;
            }
        });
    }

    [Test]
    public void DataSourcePropertyChanged_OneDeep()
    {
        var c = new Control();
        c.BindingContext = new BindingContext();
        c.CreateControl();

        var item = new MockItem("A", 0);
        var parent = new One
        {
            MockItem = item
        };
        var binding = new Binding("Text", parent, "MockItem.Text");

        c.DataBindings!.Add(binding);
        Assert.That((object?)c.Text, Is.EqualTo("A"));

        item.Text = "B";
        Assert.That((object?)c.Text, Is.EqualTo("B"));
    }

    [Test]
    public void DataSourcePropertyChanged_ThreeDeep()
    {
        var c = new Control();
        c.BindingContext = new BindingContext();
        c.CreateControl();

        var item = new MockItem("A", 0);
        var parent = new One
        {
            Two = new Two
            {
                Three = new Three
                {
                    MockItem = item
                }
            }
        };
        var binding = new Binding("Text", parent, "Two.Three.MockItem.Text");

        c.DataBindings!.Add(binding);
        Assert.That((object?)c.Text, Is.EqualTo("A"));

        item.Text = "B";
        Assert.That((object?)c.Text, Is.EqualTo("B"));

        Assert.That((object?)c.DataBindings.Count, Is.EqualTo(1), "c.DataBindings.Count");
        var bmi = c.DataBindings[0].BindingMemberInfo;
        Assert.That((object?)bmi!.Value.BindingPath, Is.EqualTo("Two.Three.MockItem"), "bmi.BindingPath");
        Assert.That((object?)bmi.Value.BindingMember, Is.EqualTo("Two.Three.MockItem.Text"), "bmi.BindingMember");
        Assert.That((object?)bmi.Value.BindingField, Is.EqualTo("Text"), "bmi.BindingField");
    }

    [Test]
    public void DataSourcePropertyChanged_DataSet()
    {
        var ds = new DataSet();

        var table1 = new DataTable("Customers");
        table1.Columns.Add("Id", typeof(int));
        table1.Columns.Add("Name", typeof(string));
        table1.Rows.Add(3, "customer1");
        table1.Rows.Add(7, "customer2");
        ds.Tables.Add(table1);

        var table2 = new DataTable("Orders");
        table2.Columns.Add("OrderId", typeof(int));
        table2.Columns.Add("CustomerId", typeof(int));
        table2.Rows.Add(56, 7);
        table2.Rows.Add(57, 3);
        ds.Tables.Add(table2);

        var relation = new DataRelation("CustomerOrders", table1.Columns["Id"]!,
            table2.Columns["CustomerId"]!);
        ds.Relations.Add(relation);

        var ctrl = new Control();
        ctrl.BindingContext = new BindingContext();
        ctrl.CreateControl();

        ctrl.DataBindings!.Add("Text", ds, "Customers.CustomerOrders.OrderId");
        Assert.That((object?)ctrl.Text, Is.EqualTo("57"));
    }

    [Test]
    public void DataSourcePropertyDifferentType()
    {
        var exc = new Exception(string.Empty, new ArgumentNullException("PARAM"));

        // The type of the property is Exception, but we know that the value
        // is actually an ArgumentException, thus specify the ParamName property
        var ctrl = new Control();
        ctrl.BindingContext = new BindingContext();
        ctrl.CreateControl();

        ctrl.DataBindings!.Add("Text", exc, "InnerException.ParamName");
        Assert.That((object?)ctrl.Text, Is.EqualTo("PARAM"));
    }

    [Test]
    public void ReadValueTest()
    {
        var c = new Control();
        c.BindingContext = new BindingContext();
        c.CreateControl();

        var item = new ChildMockItem
        {
            ObjectValue = "A"
        };
        var binding = new Binding("Tag", item, "ObjectValue")
        {
            ControlUpdateMode = ControlUpdateMode.Never
        };

        c.DataBindings!.Add(binding);
        Assert.That(c.Tag, Is.EqualTo(DBNull.Value));

        item.ObjectValue = "B";
        Assert.That(c.Tag, Is.EqualTo(DBNull.Value));

        binding.ReadValue();
        Assert.That(c.Tag, Is.EqualTo("B"));

        item.ObjectValue = "C";
        binding.ReadValue();
        Assert.That(c.Tag, Is.EqualTo("C"));

        c.Dispose();
    }

    [Test]
    public void WriteValueTest()
    {
        var c = new Control();
        c.BindingContext = new BindingContext();
        c.CreateControl();

        var item = new MockItem
        {
            Text = "A"
        };
        var binding = new Binding("Text", item, "Text")
        {
            DataSourceUpdateMode = DataSourceUpdateMode.Never
        };

        c.DataBindings!.Add(binding);
        Assert.That((object?)c.Text, Is.EqualTo("A"));

        c.Text = "B";
        Assert.That((object?)item.Text, Is.EqualTo("A"));

        binding.WriteValue();
        Assert.That((object?)item.Text, Is.EqualTo("B"));
    }

    [Test]
    public void BindableComponentTest()
    {
        var c = new Control();

        var item = new MockItem(string.Empty, 0);
        var binding = new Binding("Text", item, "Text");

        c.DataBindings!.Add(binding);
        Assert.That((object?)binding.Control, Is.EqualTo(c));
        Assert.That((object?)binding.BindableComponent, Is.EqualTo(c));

        // 
        // Now use IBindableComponent - update binding when property changes
        // since ToolStripItem doesn't have validation at all
        //
        var toolstrip_item = new BindableToolStripItem();
        toolstrip_item.BindingContext = new BindingContext();
        var binding2 = new Binding("Text", item, "Text")
        {
            DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
        };

        toolstrip_item.DataBindings.Add(binding2);
        Assert.That((object?)binding2.Control, Is.EqualTo(null));
        Assert.That((object?)binding2.BindableComponent, Is.EqualTo(toolstrip_item));
    }

    [Test]
    public void ControlUpdateModeTest()
    {
        var c = new Control();
        c.BindingContext = new BindingContext();
        c.CreateControl();

        var item = new MockItem("A", 0);
        var binding = new Binding("Text", item, "Text")
        {
            ControlUpdateMode = ControlUpdateMode.Never
        };

        c.DataBindings!.Add(binding);
        object expected = string.Empty;
        Assert.That((object?)c.Text, Is.EqualTo(expected));

        item.Text = "B";
        object expected1 = string.Empty;
        Assert.That((object?)c.Text, Is.EqualTo(expected1));
    }

    [Test]
    public void DataSourceUpdateModeTest()
    {
        var c = new Control();
        c.BindingContext = new BindingContext();
        c.CreateControl();

        var item = new MockItem("A", 0);
        var binding = new Binding("Text", item, "Text")
        {
            DataSourceUpdateMode = DataSourceUpdateMode.Never
        };

        c.DataBindings!.Add(binding);
        Assert.That((object?)c.Text, Is.EqualTo("A"));

        c.Text = "B";
        Assert.That((object?)item.Text, Is.EqualTo("A"));

        binding.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;
        Assert.That((object?)item.Text, Is.EqualTo("A"));

        c.Text = "C";
        Assert.That((object?)item.Text, Is.EqualTo("C"));

        // This requires a Validation even, which we can't test
        // by directly modifying the property
        binding.DataSourceUpdateMode = DataSourceUpdateMode.OnValidation;

        c.Text = "D";
        Assert.That((object?)item.Text, Is.EqualTo("C"));
    }

    [Test]
    public void DataSourceNullValueTest()
    {
        var c = new Control();
        c.BindingContext = new BindingContext();
        c.CreateControl();

        var item = new ChildMockItem
        {
            ObjectValue = "A"
        };
        var binding = new Binding("Tag", item, "ObjectValue")
        {
            DataSourceNullValue = "NonNull"
        };

        c.DataBindings!.Add(binding);
        Assert.That(c.Tag, Is.EqualTo("A"));

        // Since Tag property doesn't have a 
        // TagChanged event, we need to force an update
        c.Tag = null;
        binding.WriteValue();
        Assert.That(item.ObjectValue, Is.EqualTo("NonNull"));
    }

    [Test]
    public void NullValueTest()
    {
        var c = new Control();
        c.BindingContext = new BindingContext();
        c.CreateControl();

        var table = new DataTable();
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("Id", typeof(int));
        table.Rows.Add(null, DBNull.Value);

        var binding = new Binding("Tag", table, "Name");
        var binding2 = new Binding("Width", table, "Id");
        binding.FormattingEnabled = true;
        binding.NullValue = "non-null";
        binding2.FormattingEnabled = true;
        binding2.NullValue = 101;

        c.Width = 99;
        c.DataBindings!.Add(binding);
        c.DataBindings.Add(binding2);

        Assert.That(c.Tag, Is.EqualTo("non-null"));
        Assert.That((object?)c.Width, Is.EqualTo(101));
    }

    [Test]
    public void FormattingEnabledTest()
    {
        var c = new Control();
        c.BindingContext = new BindingContext();
        c.CreateControl();

        var item = new MockItem
        {
            Value = 666
        };
        var binding = new Binding("Text", item, "Value")
        {
            FormattingEnabled = true,
            FormatString = "p"
        };

        c.DataBindings!.Add(binding);
        object expected = 666.ToString("p");
        Assert.That((object?)c.Text, Is.EqualTo(expected));

        binding.FormatString = "c";
        object expected1 = 666.ToString("c");
        Assert.That((object?)c.Text, Is.EqualTo(expected1));

        binding.FormattingEnabled = false;
        object expected2 = 666.ToString();
        Assert.That((object?)c.Text, Is.EqualTo(expected2));
    }

    [Test]
    public void FormatStringTest()
    {
        var binding = new Binding("Text", null!, "Text")
        {
            FormatString = null
        };

        object expected = string.Empty;
        Assert.That((object?)binding.FormatString, Is.EqualTo(expected));
    }

}

internal class ChildMockItem : MockItem
{
    private object value;

    public ChildMockItem()
        : base(null!, 0)
    {
    }

    public object? ObjectValue
    {
        get => value;
        set => this.value = value!;
    }
}

internal class BindableToolStripItem : ToolStripItem, IBindableComponent
{
    private ControlBindingsCollection data_bindings;
    private BindingContext binding_context;

    public ControlBindingsCollection DataBindings
    {
        get
        {
            if (data_bindings == null)
                data_bindings = new ControlBindingsCollection(this);

            return data_bindings;
        }
    }

    public BindingContext BindingContext
    {
        get => binding_context;
#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
        set => binding_context = value;
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
    }
}

internal class One
{
    //----
    //private global::System.Collections.Generic.IList<Two> m_twoList
    //    = new global::System.Collections.Generic.List<Two> ();
    //
    //public global::System.Collections.Generic.IList<Two> TwoList
    //{
    //    get { return m_twoList; }
    //}

    //----
    private Two m_two;

    public Two Two
    {
        get => m_two;
        set => m_two = value;
    }

    //----
    private MockItem m_MockItem;

    public MockItem MockItem
    {
        get => m_MockItem;
        set => m_MockItem = value;
    }

    //----
    public override string ToString()
    {
        return "!!! ToString on One !!!";
    }
}

internal class Two
{
    //private global::System.Collections.Generic.IList<MockItem> m_MockItemList
    //    = new global::System.Collections.Generic.List<MockItem> ();
    //
    //public global::System.Collections.Generic.IList<MockItem> MockItemList
    //{
    //    get { return m_MockItemList; }
    //}

    //----
    private MockItem m_MockItem;

    public MockItem MockItem
    {
        get => m_MockItem;
        set => m_MockItem = value;
    }

    private Three m_Three;

    public Three Three
    {
        get => m_Three;
        set => m_Three = value;
    }

    public override string ToString()
    {
        return "!!! ToString on Two !!!";
    }
}

internal class Three
{
    private MockItem m_MockItem;

    public MockItem MockItem
    {
        get => m_MockItem;
        set => m_MockItem = value;
    }

    public override string ToString()
    {
        return "!!! ToString on Three !!!";
    }
}