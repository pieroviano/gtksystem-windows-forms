//
// ListControlTest.cs: Tests for ListControl abstract class.
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
// Copyright (c) 2006 Novell, Inc. (http://www.novell.com)
//
// Authors:
//	Carlos Alberto Cortez <calberto.cortez@gmail.com>
//

using GtkTests.Helpers;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class ListControlTest : TestHelper
{
    private int dataSourceChanged;

    [SetUp]
    protected override void SetUp()
    {
        dataSourceChanged = 0;
        base.SetUp();
    }

    [Test]
    // Bug 80794
    public void DataBindingsTest()
    {
        var table =
            @"<?xml version=""1.0"" standalone=""yes""?>
<DOK>
<DOK>
<klient>287</klient>
</DOK>
</DOK>
";
        var lookup =
            @"<?xml version=""1.0"" standalone=""yes""?>
<klient>
<klient>
<nimi>FAILED</nimi>
<kood>316</kood>
</klient>
<klient>
<nimi>SUCCESS</nimi>
<kood>287</kood>
</klient>
</klient>";

        using var frm = new Form();
        frm.ShowInTaskbar = false;
        var dsTable = new DataSet();
        dsTable.ReadXml(new StringReader(table));
        var dsLookup = new DataSet();
        dsLookup.ReadXml(new StringReader(lookup));
        var cb = new ComboBox();
        cb.DataSource = dsLookup.Tables[0];
        cb.DisplayMember = "nimi";
        cb.ValueMember = "kood";
        cb.DataBindings!.Add("SelectedValue", dsTable.Tables[0], "klient");
        frm.Controls.Add(cb);
        Assert.That((object?)cb.Text, Is.EqualTo(string.Empty));
        frm.Show();
        Assert.That((object?)cb.Text, Is.EqualTo("SUCCESS"));
    }

    [Test]
    public void GetItemText()
    {
        var itemA = new MockItem("A", 1);
        var itemB = new MockItem("B", 2);
        var itemC = new object();

        var lc = new ListControlChild();
        lc.DisplayMember = "Text";

        // No DataSource available
        Assert.That((object?)lc.GetItemText(itemA), Is.EqualTo("A"));
        Assert.That((object?)lc.GetItemText(itemB), Is.EqualTo("B"));
        Assert.That((object?)lc.GetItemText(itemC), Is.EqualTo(itemC.GetType().FullName));

        lc.DisplayMember = string.Empty;

        Assert.That((object?)lc.GetItemText(itemA), Is.EqualTo(itemA.GetType().FullName));
        Assert.That((object?)lc.GetItemText(itemB), Is.EqualTo(itemB.GetType().FullName));
        Assert.That((object?)lc.GetItemText(itemC), Is.EqualTo(itemC.GetType().FullName));

        // DataSource available
        object[] objects = [itemA, itemB, itemC];
        lc.DisplayMember = "Text";
        lc.DataSource = objects;

        Assert.That((object?)lc.GetItemText(itemA), Is.EqualTo("A"));
        Assert.That((object?)lc.GetItemText(itemB), Is.EqualTo("B"));
        Assert.That((object?)lc.GetItemText(itemC), Is.EqualTo(itemC.GetType().FullName));

        lc.DisplayMember = string.Empty;

        Assert.That((object?)lc.GetItemText(itemA), Is.EqualTo(itemA.GetType().FullName));
        Assert.That((object?)lc.GetItemText(itemB), Is.EqualTo(itemB.GetType().FullName));
        Assert.That((object?)lc.GetItemText(itemC), Is.EqualTo(itemC.GetType().FullName));
    }

    [Test]
    public void DisplayMemberNullTest()
    {
        var lc = new ListControlChild();
        lc.DisplayMember = null!;
        object expected = string.Empty;
        Assert.That((object?)lc.DisplayMember, Is.EqualTo(expected));
    }

    [Test]
    public void DataSource1()
    {
        var list1 = new ArrayList { "item 1" };
        var list2 = new ArrayList();

        var lc = new ListControlChild();
        lc.DataSourceChanged += ListControl_DataSourceChanged;
        lc.DataSource = list1;
        Assert.That((object?)dataSourceChanged, Is.EqualTo(1));
        Assert.That(lc.DataSource, Is.SameAs(list1));

        var form = new Form();
        form.Controls.Add(lc);

        Assert.That((object?)dataSourceChanged, Is.EqualTo(1));
        Assert.That(lc.DataSource, Is.SameAs(list1));
        lc.DataSource = list1;
        Assert.That((object?)dataSourceChanged, Is.EqualTo(1));
        Assert.That(lc.DataSource, Is.SameAs(list1));
        lc.DataSource = list2;
        Assert.That((object?)dataSourceChanged, Is.EqualTo(2));
        Assert.That(lc.DataSource, Is.SameAs(list2));
        lc.DataSource = null;
        Assert.That((object?)dataSourceChanged, Is.EqualTo(3));
        Assert.IsNull(lc.DataSource);

        list1.Add("whatever");
        list2.Add("whatever");
        list1.Clear();
        list2.Clear();

        form.Dispose();
    }

    [Test]
    public void DataSource2()
    {
        var list1 = new ArrayList { "item 1" };
        var list2 = new ArrayList();

        var lc = new ListControlChild();
        lc.DataSourceChanged += ListControl_DataSourceChanged;

        var form = new Form();
        form.Controls.Add(lc);

        Assert.That((object?)dataSourceChanged, Is.EqualTo(0));
        Assert.IsNull(lc.DataSource);
        lc.DataSource = list1;
        Assert.That((object?)dataSourceChanged, Is.EqualTo(1));
        Assert.That(lc.DataSource, Is.SameAs(list1));
        lc.DataSource = list2;
        Assert.That((object?)dataSourceChanged, Is.EqualTo(2));
        Assert.That(lc.DataSource, Is.SameAs(list2));
        lc.DataSource = null;
        Assert.That((object?)dataSourceChanged, Is.EqualTo(3));
        Assert.IsNull(lc.DataSource);

        list1.Add("whatever");
        list2.Add("whatever");
        list1.Clear();
        list2.Clear();

        form.Dispose();
    }

    [Test]
    public void SelectedValue()
    {
        var f = new Form();
        f.ShowInTaskbar = false;
        var lc = new ListControlChild();
        f.Controls.Add(lc);

        var list = new ArrayList
        {
            new MockItem("TextA", 1),
            new MockItem(string.Empty, 4),
            new MockItem("TextC", 9)
        };

        lc.ValueMember = "Text";
        lc.DataSource = list;

        f.Show();

        lc.SelectedValue = "TextC";
        Assert.That((object?)lc.SelectedIndex, Is.EqualTo(2));
        Assert.That(lc.SelectedValue, Is.EqualTo("TextC"));

        lc.SelectedValue = string.Empty;
        Assert.That((object?)lc.SelectedIndex, Is.EqualTo(1));
        object expected = string.Empty;
        Assert.That(lc.SelectedValue, Is.EqualTo(expected));

        lc.SelectedValue = "TextA";
        Assert.That((object?)lc.SelectedIndex, Is.EqualTo(0));
        Assert.That(lc.SelectedValue, Is.EqualTo("TextA"));

        Assert.Throws<ArgumentNullException>(() =>
        {
            lc.SelectedValue = null;
        });

        f.Dispose();
    }

    [Test]
    public void SelectedValue2()
    {
        var f = new Form();
        f.ShowInTaskbar = false;
        var child = new ListControlChild();

        var list = new ArrayList
        {
            new MockItem("A", 0),
            new MockItem("B", 1),
            new MockItem("C", 2)
        };
        child.DataSource = list;
        child.ValueMember = "Text";

        var item = new MockItem(string.Empty, 0);
        child.DataBindings!.Add("SelectedValue", item, "Text");

        Assert.That((object?)child.SelectedIndex, Is.EqualTo(-1));

        f.Controls.Add(child);
        Assert.That((object?)child.SelectedIndex, Is.EqualTo(-1));

        // When the form is shown, normally the SelectedIndex is the
        // CurrencyManager.Position (0 in this case), but it should remain as -1
        // since SelectedValue is bound to a String.Empty value. See #324286
        f.Show();
        f.Dispose();
    }

    [Test] // bug #81771
    public void DataSource_BindingList1()
    {
        BindingList<string> list1 =
        [
            "item 1"
        ];
        BindingList<string> list2 = [];

        var lc = new ListControlChild();
        lc.DataSourceChanged += ListControl_DataSourceChanged;
        lc.DataSource = list1;
        Assert.That((object?)dataSourceChanged, Is.EqualTo(1));
        Assert.That(lc.DataSource, Is.SameAs(list1));

        var form = new Form();
        form.Controls.Add(lc);

        Assert.That((object?)dataSourceChanged, Is.EqualTo(1));
        Assert.That(lc.DataSource, Is.SameAs(list1));
        lc.DataSource = list2;
        Assert.That((object?)dataSourceChanged, Is.EqualTo(2));
        Assert.That(lc.DataSource, Is.SameAs(list2));
        lc.DataSource = null;
        Assert.That((object?)dataSourceChanged, Is.EqualTo(3));
        Assert.IsNull(lc.DataSource);

        list1.Add("item");
        list1.Clear();

        form.Dispose();
    }

    [Test] // bug #81771
    public void DataSource_BindingList2()
    {
        BindingList<string> list1 =
        [
            "item 1"
        ];
        BindingList<string> list2 = [];

        var lc = new ListControlChild();
        lc.DataSourceChanged += ListControl_DataSourceChanged;

        var form = new Form();
        form.Controls.Add(lc);

        Assert.That((object?)dataSourceChanged, Is.EqualTo(0));
        Assert.IsNull(lc.DataSource);
        lc.DataSource = list1;
        Assert.That((object?)dataSourceChanged, Is.EqualTo(1));
        Assert.That(lc.DataSource, Is.SameAs(list1));
        lc.DataSource = list2;
        Assert.That((object?)dataSourceChanged, Is.EqualTo(2));
        Assert.That(lc.DataSource, Is.SameAs(list2));
        lc.DataSource = null;
        Assert.That((object?)dataSourceChanged, Is.EqualTo(3));
        Assert.IsNull(lc.DataSource);
        list1.Add("item");
        list1.Clear();

        form.Dispose();
    }

    [Test]
    public void BehaviorFormatting()
    {
        ListControl lc = new ListControlChild();
        var dt = new DateTime(1, 2, 3, 4, 5, 6);

        Assert.That((object?)lc.FormattingEnabled, Is.EqualTo(false));
        Assert.That((object?)lc.FormatInfo, Is.EqualTo(null));
        object expected = string.Empty;
        Assert.That((object?)lc.FormatString, Is.EqualTo(expected));

        Assert.That((object?)lc.GetItemText(dt), Is.EqualTo(dt.ToString()));

        lc.FormattingEnabled = true;
        lc.FormatString = "MM/dd";

        Assert.That((object?)lc.GetItemText(dt), Is.EqualTo("02/03"));

        lc.Format += lc_Format;
        Assert.That((object?)lc.GetItemText(dt), Is.EqualTo("Monkey!"));
    }

    private void lc_Format(object? sender, ListControlConvertEventArgs e)
    {
        e.Value = "Monkey!";
    }

    [Test]
    public void FormattingChanges()
    {
        var refresh_items_called = false;

        var lc = new ListControlChild();
        lc.RefreshingItems += delegate
        {
            refresh_items_called = true;
        };

        lc.FormattingEnabled = !lc.FormattingEnabled;
        Assert.That((object?)refresh_items_called, Is.EqualTo(true));

        refresh_items_called = false;
        lc.FormatInfo = CultureInfo.CurrentCulture;
        Assert.That((object?)refresh_items_called, Is.EqualTo(true));

        refresh_items_called = false;
        lc.FormatString = CultureInfo.CurrentCulture.NumberFormat.ToString()!;
        Assert.That((object?)refresh_items_called, Is.EqualTo(true));
    }

    private void ListControl_DataSourceChanged(object? sender, EventArgs e)
    {
        dataSourceChanged++;
    }

    [Test]
    public void FormatEventValueType()
    {
        string? event_log = null;
        var comboBox = new ComboBox();
        comboBox.FormattingEnabled = true;
        comboBox.Format += delegate (object? _, ListControlConvertEventArgs e)
        {
            event_log = e.Value?.GetType().Name;
        };

        var objects = new int[] { 1, 2, 3 };
        comboBox.DataSource = objects;
        comboBox.GetItemText(1);

        object expected = typeof(int).Name;
        Assert.That((object?)event_log, Is.EqualTo(expected));
    }

    public class ListControlChild : ListControl
    {
        private int selected_index = -1;

        public override int SelectedIndex
        {
            get => selected_index;
            set => selected_index = value;
        }

#pragma warning disable CS0067 // Event is never used
        public event EventHandler RefreshingItems;
#pragma warning restore CS0067 // Event is never used
    }
}

public class MockItem
{
    public MockItem(string text, int value)
    {
        _text = text;
        _value = value;
    }

    public MockItem()
    {
        _text = string.Empty;
        _value = -1;
    }

    public string Text
    {
        get => _text;
        set
        {
            if (_text == value)
                return;

            _text = value;
            OnTextChanged(EventArgs.Empty);
        }
    }

    public int Value
    {
        get => _value;
        set
        {
            if (_value == value)
                return;

            _value = value;
            OnValueChanged(EventArgs.Empty);
        }
    }

    protected virtual void OnTextChanged(EventArgs args)
    {
        if (TextChanged != null)
            TextChanged(this, args);
    }

    protected virtual void OnValueChanged(EventArgs args)
    {
        if (ValueChanged != null)
            ValueChanged(this, args);
    }

    public event EventHandler TextChanged;
    public event EventHandler ValueChanged;

    private string _text;
    private int _value;
}

public class MockContainer
{
    private MockItem item;

    public MockItem Item
    {
        get => item;
        set => item = value;
    }
}