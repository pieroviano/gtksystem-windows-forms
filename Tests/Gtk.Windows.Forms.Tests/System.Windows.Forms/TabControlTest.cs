//
// TabControlTest.cs: Test cases for TabControl.
//
// Author:
//   Ritvik Mayank (mritvik@novell.com)
//
// (C) 2005 Novell, Inc. (http://www.novell.com)
//

using GtkTests.Helpers;
using System.Drawing;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class TabControlTest : TestHelper
{
    private int _selected_index_changed;

    [SetUp]
    protected override void SetUp()
    {
        _selected_index_changed = 0;
        base.SetUp();
    }

    [Test]
    public void TabControlPropertyTest()
    {
        var myForm = new Form();
        myForm.ShowInTaskbar = false;
        var myTabControl = new TabControl();
        myTabControl.Visible = true;
        myTabControl.Name = "Mono TabControl";

        // A 
        Assert.That((object?)myTabControl.Alignment, Is.EqualTo(TabAlignment.Top));

        // D 
        Assert.That((object?)myTabControl.DrawMode, Is.EqualTo(TabDrawMode.Normal));

        // M 
        Assert.That((object?)myTabControl.Multiline, Is.EqualTo(false));

        // S
        Assert.That((object?)myTabControl.SelectedIndex, Is.EqualTo(-1));
        Assert.That((object?)myTabControl.SelectedTab, Is.EqualTo(null));
        Assert.That((object?)myTabControl.ShowToolTips, Is.EqualTo(false));
        Assert.That((object?)myTabControl.SizeMode, Is.EqualTo(TabSizeMode.Normal));

        // T
        Assert.That((object?)myTabControl.TabCount, Is.EqualTo(0));
        Assert.That((object?)myTabControl.TabPages.Count, Is.EqualTo(0));

        myForm.Dispose();
    }

    [Test]
    [Category("NotWorking")]
    public void GetTabRectTest()
    {
        var myTabControl = new TabControl();
        var myTabPage = new TabPage();
        myTabControl.Controls.Add(myTabPage);
        myTabPage.TabIndex = 0;
        var myTabRect = myTabControl.GetTabRect(0);
        Assert.That((object?)myTabRect.X, Is.EqualTo(2), "#GetT1");
        Assert.That((object?)myTabRect.Y, Is.EqualTo(2), "#GetT2");
        Assert.That((object?)myTabRect.Width, Is.EqualTo(42), "#GetT3");
        // It is environment dependent
        //Assert1.AreEqual(18, myTabRect.Height, "#GetT4");
    }

    [Test]
    public void ToStringTest()
    {
        var myTabControl = new TabControl();
        Assert.That((object?)myTabControl.ToString(), Is.EqualTo("System.Windows.Forms.TabControl, TabPages.Count: 0"));
    }

    [Test]
    public void ItemSizeTest()
    {
        var tc = new TabControl();
        Assert.That((object?)tc.ItemSize, Is.EqualTo(Size.Empty));

        tc.CreateControl();
        Assert.IsTrue(tc.ItemSize.Width == 0);
        Assert.IsTrue(tc.ItemSize.Height > 0);

        tc.TabPages.Add("A");
        Assert.IsTrue(tc.ItemSize.Width > 0);
        Assert.IsTrue(tc.ItemSize.Height > 0);

        // ItemSize.Height can change, depending on Font
        var prev_size = tc.ItemSize;
        tc.Font = new Font(tc.Font!.FontFamily, tc.Font!.Height * 2);
        Assert.IsTrue(tc.ItemSize.Height > prev_size.Height);

        // Images can cause a change as well
        prev_size = tc.ItemSize;
        var image_list = new ImageList();
        image_list.ImageSize = new Size(image_list.ImageSize.Width, tc.Font.Height * 2);
        Assert.IsTrue(tc.ItemSize.Height > prev_size.Height);
    }

    [Test]
    public void ItemSizeFixedTest()
    {
        var tc = new TabControl();
        tc.SizeMode = TabSizeMode.Fixed;
        Assert.That((object?)tc.ItemSize, Is.EqualTo(Size.Empty));

        tc.CreateControl();
        Assert.IsTrue(tc.ItemSize.Width == 0);
        Assert.IsTrue(tc.ItemSize.Height > 0);

        tc.TabPages.Add("A");
        Assert.IsTrue(tc.ItemSize.Width == 96);
        Assert.IsTrue(tc.ItemSize.Width > 0);

        // Height can change automatically depending on Font,
        // but not Width
        var prev_size = tc.ItemSize;
        tc.Font = new Font(tc.Font!.FontFamily, tc.Font.Height * 2);
        Assert.IsTrue(tc.ItemSize.Width == 96);
        Assert.IsTrue(tc.ItemSize.Height > prev_size.Height);

        // Manually set ItemSize
        tc.ItemSize = new Size(100, 35);
        Assert.That((object?)tc.ItemSize.Width, Is.EqualTo(100));
        Assert.That((object?)tc.ItemSize.Height, Is.EqualTo(35));

        // Font size is decreased, but since we manually set
        // the size we can't automatically update it.
        tc.Font = new Font(tc.Font.FontFamily, tc.Font.Height / 2);
        Assert.That((object?)tc.ItemSize.Width, Is.EqualTo(100));
        Assert.That((object?)tc.ItemSize.Height, Is.EqualTo(35));

        // Manually set even if control has not been created.
        tc = new TabControl();
        tc.SizeMode = TabSizeMode.Fixed;
        tc.ItemSize = new Size(100, 35);
        Assert.That((object?)tc.ItemSize.Width, Is.EqualTo(100));
        Assert.That((object?)tc.ItemSize.Height, Is.EqualTo(35));
    }

    [Test]
    public void ClearTabPagesTest()
    {
        // no tab pages
        var tab = new TabControl();
        tab.TabPages.Clear();
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(-1));
        Assert.That((object?)tab.TabPages.Count, Is.EqualTo(0));

        // single tab page
        tab.Controls.Add(new TabPage());
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(0));
        Assert.That((object?)tab.TabPages.Count, Is.EqualTo(1));
        tab.TabPages.Clear();
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(-1));
        Assert.That((object?)tab.TabPages.Count, Is.EqualTo(0));

        // multiple tab pages
        tab.Controls.Add(new TabPage());
        tab.Controls.Add(new TabPage());
        tab.Controls.Add(new TabPage());
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(0));
        Assert.That((object?)tab.TabPages.Count, Is.EqualTo(3));
        tab.SelectedIndex = 1;
        tab.TabPages.Clear();
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(-1));
        Assert.That((object?)tab.TabPages.Count, Is.EqualTo(0));
    }

    [Test]
    [Category("NotWorking")]
    public void Controls_Remove_HandleCreated()
    {
        var tab = new TabControl();
        tab.SelectedIndexChanged += SelectedIndexChanged;

        var form = new Form();
        form.ShowInTaskbar = false;
        form.Controls.Add(tab);
        form.Show();

        tab.Controls.Add(new TabPage());
        tab.Controls.Add(new TabPage());
        tab.Controls.Add(new TabPage());
        tab.Controls.Add(new TabPage());
        tab.Controls.Add(new TabPage());
        tab.Controls.Add(new TabPage());

        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(0));
        Assert.That((object?)tab.TabPages.Count, Is.EqualTo(6));
        Assert.That((object?)_selected_index_changed, Is.EqualTo(0));

        // remove selected tab
        tab.SelectedIndex = 2;
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(2));
        Assert.That((object?)_selected_index_changed, Is.EqualTo(1));
        tab.Controls.RemoveAt(2);
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(0));
        Assert.That((object?)tab.TabPages.Count, Is.EqualTo(5));
        Assert.That((object?)_selected_index_changed, Is.EqualTo(2));

        // remove not-selected tab
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(0));
        Assert.That((object?)_selected_index_changed, Is.EqualTo(2));
        tab.Controls.RemoveAt(3);
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(0));
        Assert.That((object?)tab.TabPages.Count, Is.EqualTo(4));
        Assert.That((object?)_selected_index_changed, Is.EqualTo(2));

        // remove last tab
        tab.Controls.RemoveAt(3);
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(0));
        Assert.That((object?)tab.TabPages.Count, Is.EqualTo(3));
        Assert.That((object?)_selected_index_changed, Is.EqualTo(2));

        // remove first tab
        tab.Controls.RemoveAt(0);
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(0));
        Assert.That((object?)tab.TabPages.Count, Is.EqualTo(2));
        Assert.That((object?)_selected_index_changed, Is.EqualTo(3));

        // remove remaining tabs
        tab.Controls.RemoveAt(1);
        tab.Controls.RemoveAt(0);
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(-1));
        Assert.That((object?)tab.TabPages.Count, Is.EqualTo(0));
        Assert.That((object?)_selected_index_changed, Is.EqualTo(4));
    }

    [Test]
    [Category("NotWorking")]
    public void Controls_Remove_HandleNotCreated()
    {
        var tab = new TabControl();
        tab.SelectedIndexChanged += SelectedIndexChanged;
        tab.Controls.Add(new TabPage());
        tab.Controls.Add(new TabPage());
        tab.Controls.Add(new TabPage());
        tab.Controls.Add(new TabPage());
        tab.Controls.Add(new TabPage());
        tab.Controls.Add(new TabPage());

        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(-1));
        Assert.That((object?)tab.TabPages.Count, Is.EqualTo(6));
        Assert.That((object?)_selected_index_changed, Is.EqualTo(0));

        // remove selected tab
        tab.SelectedIndex = 2;
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(2));
        Assert.That((object?)_selected_index_changed, Is.EqualTo(0));
        tab.Controls.RemoveAt(2);
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(0));
        Assert.That((object?)tab.TabPages.Count, Is.EqualTo(5));
        Assert.That((object?)_selected_index_changed, Is.EqualTo(0));

        // remove not-selected tab
        tab.Controls.RemoveAt(3);
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(0));
        Assert.That((object?)tab.TabPages.Count, Is.EqualTo(4));
        Assert.That((object?)_selected_index_changed, Is.EqualTo(0));

        // remove last tab
        tab.Controls.RemoveAt(3);
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(0));
        Assert.That((object?)tab.TabPages.Count, Is.EqualTo(3));
        Assert.That((object?)_selected_index_changed, Is.EqualTo(0));

        // remove first tab
        tab.Controls.RemoveAt(0);
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(0));
        Assert.That((object?)tab.TabPages.Count, Is.EqualTo(2));
        Assert.That((object?)_selected_index_changed, Is.EqualTo(0));

        // remove remaining tabs
        tab.Controls.RemoveAt(1);
        tab.Controls.RemoveAt(0);
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(0));
        Assert.That((object?)tab.TabPages.Count, Is.EqualTo(0));
        Assert.That((object?)_selected_index_changed, Is.EqualTo(0));

        var form = new Form();
        form.ShowInTaskbar = false;
        form.Controls.Add(tab);
        form.Show();
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(-1));
        Assert.That((object?)tab.TabPages.Count, Is.EqualTo(0));
        Assert.That((object?)_selected_index_changed, Is.EqualTo(0));
    }

    [Test]
    public void SelectedIndex()
    {
        var tab = new TabControl();
        tab.SelectedIndexChanged += SelectedIndexChanged;
        tab.Controls.Add(new TabPage());
        tab.Controls.Add(new TabPage());

        tab.SelectedIndex = 0;
        Assert.That((object?)_selected_index_changed, Is.EqualTo(0));
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(0));

        tab.SelectedIndex = -1;
        Assert.That((object?)_selected_index_changed, Is.EqualTo(0));
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(-1));

        tab.SelectedIndex = 1;
        Assert.That((object?)_selected_index_changed, Is.EqualTo(0));
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(1));

        tab.SelectedIndex = 1;
        Assert.That((object?)_selected_index_changed, Is.EqualTo(0));
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(1));


        tab.SelectedIndex = 6;
        Assert.That((object?)_selected_index_changed, Is.EqualTo(0));
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(6));

        tab.SelectedIndex = 6;
        Assert.That((object?)_selected_index_changed, Is.EqualTo(0));
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(6));

        var form = new Form();
        form.ShowInTaskbar = false;
        form.Controls.Add(tab);

        form.Show();

        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(0));

        tab.SelectedIndex = 0;
        Assert.That((object?)_selected_index_changed, Is.EqualTo(0));
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(0));

        tab.SelectedIndex = -1;
        Assert.That((object?)_selected_index_changed, Is.EqualTo(1));
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(-1));

        tab.SelectedIndex = 1;
        Assert.That((object?)_selected_index_changed, Is.EqualTo(2));
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(1));

        tab.SelectedIndex = 1;
        Assert.That((object?)_selected_index_changed, Is.EqualTo(2));
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(1));

        form.Dispose();
    }

    [Test] // bug #78395
    public void SelectedIndex_Ignore()
    {
        var c = new TabControl();
        c.SelectedIndexChanged += SelectedIndexChanged;
        c.SelectedIndex = 0;
        Assert.That((object?)_selected_index_changed, Is.EqualTo(0));

        c.TabPages.Add(new TabPage());
        c.TabPages.Add(new TabPage());
        Assert.That((object?)c.SelectedIndex, Is.EqualTo(0));
        var f = new Form();
        f.ShowInTaskbar = false;
        f.Controls.Add(c);
        f.Show();
        Assert.That((object?)_selected_index_changed, Is.EqualTo(0));
        c.SelectedIndex = 2; // beyond the pages - ignored
        Assert.That((object?)_selected_index_changed, Is.EqualTo(1));
        Assert.That((object?)c.SelectedIndex, Is.EqualTo(0));
        f.Dispose();
    }

    [Test]
    public void SelectedIndex_Negative()
    {
        var form = new Form();
        form.ShowInTaskbar = false;
        var tab = new TabControl();
        tab.SelectedIndexChanged += SelectedIndexChanged;
        form.Controls.Add(tab);

        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(-1));
        tab.SelectedIndex = -1;
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(-1));
        Assert.That((object?)_selected_index_changed, Is.EqualTo(0));

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            try
            {
                tab.SelectedIndex = -2;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                object expected = typeof(ArgumentOutOfRangeException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNotNull(ex.Message);
                Assert.IsTrue(ex.Message.IndexOf("'-2'", StringComparison.Ordinal) != -1);
                Assert.IsTrue(ex.Message.IndexOf("'SelectedIndex'", StringComparison.Ordinal) != -1);
                Assert.IsTrue(ex.Message.IndexOf("-1", StringComparison.Ordinal) != -1);
                Assert.IsNotNull(ex.ParamName);
                Assert.That((object?)ex.ParamName, Is.EqualTo("SelectedIndex"));
                throw;
            }
        });

        Assert.That((object?)_selected_index_changed, Is.EqualTo(0));
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(-1));
        form.Show();
        Assert.That((object?)_selected_index_changed, Is.EqualTo(0));
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(-1));

        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            try
            {
                tab.SelectedIndex = -5;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                object expected = typeof(ArgumentOutOfRangeException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNotNull(ex.Message);
                Assert.IsTrue(ex.Message.IndexOf("'-5'") != -1);
                Assert.IsTrue(ex.Message.IndexOf("'SelectedIndex'") != -1);
                Assert.IsTrue(ex.Message.IndexOf("-1") != -1);
                Assert.IsNotNull(ex.ParamName);
                Assert.That((object?)ex.ParamName, Is.EqualTo("SelectedIndex"));
                throw;
            }
        });

        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(-1));
        tab.SelectedIndex = -1;
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(-1));
        Assert.That((object?)_selected_index_changed, Is.EqualTo(0));

        form.Dispose();
    }

    [Test] // bug #79847
    public void NoTabPages()
    {
        var form = new Form();
        var tab = new TabControl();
        tab.SelectedIndex = 0;
        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(0));

        form.Controls.Add(tab);

        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(0));

        form.ShowInTaskbar = false;
        form.Show();

        Assert.That((object?)tab.SelectedIndex, Is.EqualTo(-1));

        form.Dispose();
    }

    [Test] // bug #81802. should not throw an exception
    public void NoTabPages2()
    {
        var form = new Form();
        form.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
        var tab = new TabControl();
        form.Controls.Add(tab);
        tab.Dispose();
        form.Dispose();
    }

    private void SelectedIndexChanged(object? sender, EventArgs e)
    {
        _selected_index_changed++;
    }

    [Test] // bug #499887
    public void SelectedIndexChangeFiresFocus()
    {
        var f = new Form();
        var tc = new TabControl();
        var p1 = new TabPage();
        var p2 = new TabPage();
        var events = string.Empty;

        tc.TabPages.Add(p1);
        tc.TabPages.Add(p2);
        tc.SelectedIndex = 0;
        var b1 = new Button();
        var b2 = new Button();

        f.Controls.Add(b1);
        f.Controls.Add(b2);
        f.Controls.Add(tc);

        f.Show();
        b1.Focus();
        b2.GotFocus += delegate
        {
            tc.SelectedIndex = 1;
        };

        tc.GotFocus += delegate { events += ("tc_OnGotFocus" + tc.SelectedIndex + ";"); };
        tc.SelectedIndexChanged += delegate { events += ("tc_OnSelectedIndexChanged" + tc.SelectedIndex + ";"); };
        p2.Enter += delegate { events += ("p2_OnEnter" + tc.SelectedIndex + ";"); };
        p2.Leave += delegate { events += ("p2_OnLeave;"); };

        b2.Focus();
        Assert.That((object?)events, Is.EqualTo("tc_OnGotFocus0;p2_OnEnter1;tc_OnSelectedIndexChanged1;"));
        Assert.IsTrue(tc.Focused);

        f.Close();
    }

    // Make sure that setting focus for TabControl is *not* resetting SelectedIndex when
    // the value is -1
    [Test]
    public void GotFocusSelectedIndex()
    {
        var tc = new TabControl();
        tc.TabPages.Add("A");
        tc.TabPages.Add("B");

        var b = new Button(); // Dummy button to receive focus by default
        var f = new Form();
        f.Controls.AddRange(b, tc);
        f.Show();

        tc.SelectedIndex = -1;

        // Make sure focus goes back to button
        b.Focus();

        // Finally give focus back to TabControl
        tc.Focus();

        Assert.That((object?)tc.SelectedIndex, Is.EqualTo(-1));

        f.Close();
    }
}

[TestFixture]
public class TabPageCollectionTest : TestHelper
{
    [Test]
    public void Indexer()
    {
        var tab = new TabControl();
        var tabPages = new TabControl.TabPageCollection(tab);
        var tabPageA = new TabPage();
        var tabPageB = new TabPage();
        var tabPageC = new TabPage();
        var tabPageD = new TabPage();
        tabPages.Add(tabPageA);
        Assert.That((object?)tabPages[0], Is.SameAs(tabPageA));
        tabPages[0] = tabPageB;
        Assert.That((object?)tabPages[0], Is.SameAs(tabPageB));
        tabPages.Add(tabPageC);
        Assert.That((object?)tabPages[0], Is.SameAs(tabPageB));
        Assert.That((object?)tabPages[1], Is.SameAs(tabPageC));
        tabPages.Remove(tabPageB);
        Assert.That((object?)tabPages[0], Is.SameAs(tabPageC));
        tabPages[0] = tabPageD;
        Assert.That((object?)tabPages[0], Is.SameAs(tabPageD));

        var form = new Form();
        form.ShowInTaskbar = false;
        form.Controls.Add(tab);
        form.Show();
        form.Dispose();
    }
}