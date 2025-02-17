using System.Windows.Forms;
using System.Drawing;
using GtkTests.Helpers;
using System.Resources;
using System.Collections;
using System.Drawing.Imaging;

namespace GtkTests.System.Windows.Forms.Events;

[TestFixture]
public class EventClass : TestHelper
{
    static bool eventhandled = false;
    public static void Event_Handler1(object sender, EventArgs e)
    {
        eventhandled = true;
    }

    [Test]
    public void BackColorChangedTest()
    {
        var c = new MockControl();
        // Test BackColorChanged Event
        c.BackColorChanged += Event_Handler1;
        c.BackColor = Color.Black;
        Assert.AreEqual(true, eventhandled, "#A1");

    }

    [Test]
    public void BgrndImageChangedTest()
    {
        var c = new MockControl();
        // Test BackgroundImageChanged Event
        c.BackgroundImageChanged += Event_Handler1;
        eventhandled = false;
        c.BackgroundImage = Image.FromStream(new MemoryStream(Properties.Resources.a));

        Assert.AreEqual(true, eventhandled, "#A2");
    }

    [Test]
    public void BindingContextChangedTest()
    {
        var c = new MockControl();
        // Test BindingContextChanged Event
        c.BindingContextChanged += Event_Handler1;
        var bcG1 = new BindingContext();
        eventhandled = false;
        c.BindingContext = bcG1;
        Assert.AreEqual(true, eventhandled, "#A3");

    }

    [Test]
    public void CausesValidationChangedTest()
    {
        var c = new MockControl();
        // Test CausesValidationChanged Event
        c.CausesValidationChanged += Event_Handler1;
        eventhandled = false;
        c.CausesValidation = false;
        Assert.AreEqual(true, eventhandled, "#A4");

    }

    [Test]
    public void CursorChangedTest()
    {
        var c = new MockControl();
        // Test CursorChanged Event
        c.CursorChanged += Event_Handler1;
        eventhandled = false;
        c.Cursor = Cursors.Hand;
        Assert.AreEqual(true, eventhandled, "#A6");
    }

    [Test]
    public void DisposedTest()
    {
        var c = new MockControl();
        // Test Disposed Event
        c.Disposed += Event_Handler1;
        eventhandled = false;
        c.Dispose();
        Assert.AreEqual(true, eventhandled, "#A7");
    }

    [Test]
    public void DockChangedTest()
    {
        var c = new MockControl();
        // Test DockChanged Event
        c.DockChanged += Event_Handler1;
        eventhandled = false;
        c.Dock = DockStyle.Bottom;
        Assert.AreEqual(true, eventhandled, "#A8");
    }

    [Test]
    public void EnabledChangedTest()
    {
        var c = new MockControl();
        // Test EnabledChanged Event
        c.EnabledChanged += Event_Handler1;
        eventhandled = false;
        c.Enabled = false;
        Assert.AreEqual(true, eventhandled, "#A9");
    }

    [Test]
    public void FontChangedTest()
    {
        var c = new MockControl();
        // Test FontChanged Event
        c.FontChanged += Event_Handler1;
        eventhandled = false;
        c.Font = new Font(FontFamily.GenericSerif, 10.0f);
        c.Font = new Font(c.Font, FontStyle.Bold);
        Assert.AreEqual(true, eventhandled, "#A11");
    }

    [Test]
    public void ForeColorChangedTest()
    {
        var c = new MockControl();
        // Test ForeColorChanged Event
        c.ForeColorChanged += Event_Handler1;
        eventhandled = false;
        c.ForeColor = Color.Red;
        Assert.AreEqual(true, eventhandled, "#A12");
    }

    [Test]
    public void HandleCreatedTest()
    {
        var c = new MockControl();
        // Test HandleCreated Event
        c.HandleCreated += Event_Handler1;
        eventhandled = false;
        c.Handle.GetType();
        Assert.AreEqual(true, eventhandled, "#A15");
    }

    [Test]
    public void ImeModeChangedTest()
    {
        var c = new MockControl();
        // Test ImeModeChanged Event
        c.ImeModeChanged += Event_Handler1;
        eventhandled = false;
        c.ImeMode = ImeMode.Off;
        Assert.AreEqual(true, eventhandled, "#A19");
    }

    [Test]
    public void LocationChangedTest()
    {
        var c = new MockControl();
        // Test LocationChanged Event
        c.LocationChanged += Event_Handler1;
        eventhandled = false;
        c.Left = 20;
        Assert.AreEqual(true, eventhandled, "#A20");
    }

    [Test]
    public void ResizeTest()
    {
        var c = new MockControl();
        // Test Resize Event
        c.Resize += Event_Handler1;
        eventhandled = false;
        c.Height = 20;
        Assert.AreEqual(true, eventhandled, "#A22");
    }

    [Test]
    public void RightToLeftChangedTest()
    {
        var c = new MockControl();
        // Test RightToLeftChanged Event
        c.RightToLeftChanged += Event_Handler1;
        eventhandled = false;
        c.RightToLeft = RightToLeft.Yes;
        Assert.AreEqual(true, eventhandled, "#A23");
    }

    [Test]
    public void SizeChangedTest()
    {
        var c = new MockControl();
        // Test SizeChanged Event
        c.SizeChanged += Event_Handler1;
        eventhandled = false;
        c.Height = 80;
        Assert.AreEqual(true, eventhandled, "#A24");
    }

    [Test]
    public void TabIndexChangedTest()
    {
        var c = new MockControl();
        // Test TabIndexChanged Event
        c.TabIndexChanged += Event_Handler1;
        eventhandled = false;
        c.TabIndex = 1;
        Assert.AreEqual(true, eventhandled, "#A27");
    }

    [Test]
    public void TabStopChangedTest()
    {
        var c = new MockControl();
        // Test TabStopChanged Event
        c.TabStopChanged += Event_Handler1;
        eventhandled = false;
        c.TabStop = false;
        Assert.AreEqual(true, eventhandled, "#A28");
    }

    [Test]
    public void TextChangedTest()
    {
        var c = new MockControl();
        // Test TextChanged Event
        c.TextChanged += Event_Handler1;
        eventhandled = false;
        c.Text = "some Text";
        Assert.AreEqual(true, eventhandled, "#A29");
    }

    [Test]
    public void VisibleChangedTest()
    {
        var c = new MockControl();
        // Test VisibleChanged Event
        c.VisibleChanged += Event_Handler1;
        eventhandled = false;
        c.Visible = false;
        Assert.AreEqual(true, eventhandled, "#A30");
    }
}


[TestFixture]
public class LayoutEventClass
{
    static bool eventhandled = false;
    public static void LayoutEvent(object sender, LayoutEventArgs e)
    {
        eventhandled = true;
    }

    [Test]
    public void LayoutTest()
    {
        var c = new MockControl();
        c.Layout += LayoutEvent;
        eventhandled = false;
        c.Visible = true;
        c.Height = 100;
        Assert.AreEqual(true, eventhandled, "#D1");
    }

    int event_count;
    int resize_event;
    int size_changed_event;
    int layout_event;

    void resize(object sender, EventArgs e)
    {
        resize_event = ++event_count;
    }

    void layout(object sender, LayoutEventArgs le)
    {
        layout_event = ++event_count;
    }

    void size_changed(object sender, EventArgs e)
    {
        size_changed_event = ++event_count;
    }

    [Test]
    public void LayoutResizeTest()
    {
        var c = new MockControl();
        c.Layout += layout;
        c.Resize += resize;
        c.SizeChanged += size_changed;
        c.Size = new Size(100, 100);
        Assert.AreEqual(1, layout_event, "1");
        Assert.AreEqual(2, resize_event, "2");
        Assert.AreEqual(3, size_changed_event, "3");
    }
}

[TestFixture]
public class ControlAddRemoveEventClass
{
    static bool eventhandled = false;
    public static void ControlEvent(object sender, ControlEventArgs e)
    {
        eventhandled = true;
    }

    [Test]
    public void ControlAddedTest()
    {
        var c = new MockControl();
        c.ControlAdded += ControlEvent;
        var TB = new TextBox();
        eventhandled = false;
        c.Controls.Add(TB);
        Assert.AreEqual(true, eventhandled, "#F1");
    }

    [Test]
    public void ControlRemovedTest()
    {
        var c = new MockControl();
        c.ControlRemoved += ControlEvent;
        var TB = new TextBox();
        c.Controls.Add(TB);
        eventhandled = false;
        c.Controls.Remove(TB);
        Assert.AreEqual(true, eventhandled, "#F2");
    }
}

[TestFixture]
public class ControlRefresh : TestHelper
{
    [SetUp]
    protected override void SetUp()
    {
        invalidated = 0;
        base.SetUp();
    }

    [Test]
    public void HandleNotCreated()
    {
        var c = new MockControl();
        c.Invalidated += Control_Invalidated;

        c.Visible = true;
        c.Refresh();
        Assert.AreEqual(0, invalidated, "#1");

        c.Visible = false;
        c.Refresh();
        Assert.AreEqual(0, invalidated, "#2");
    }

    [Test]
    [Category("NotWorking")]
    public void Visible()
    {
        var c = new MockControl();
        c.Invalidated += Control_Invalidated;
        c.Visible = true;

        var form = new Form();
        form.ShowInTaskbar = false;
        form.Controls.Add(c);

        form.Show();
        Assert.AreEqual(0, invalidated, "#1");

        c.Refresh();
        Assert.AreEqual(1, invalidated, "#2");

        form.Refresh();
        Assert.AreEqual(1, invalidated, "#3");
    }

    [Test]
    public void NotVisible()
    {
        var c = new MockControl();
        c.Invalidated += Control_Invalidated;
        c.Visible = false;

        var form = new Form();
        form.ShowInTaskbar = false;
        form.Controls.Add(c);

        form.Show();
        Assert.AreEqual(0, invalidated, "#1");

        c.Refresh();
        Assert.AreEqual(0, invalidated, "#2");

        form.Refresh();
        Assert.AreEqual(0, invalidated, "#3");

        form.Close();
    }

    private void Control_Invalidated(object sender, InvalidateEventArgs e)
    {
        invalidated++;
    }

    private int invalidated;
}