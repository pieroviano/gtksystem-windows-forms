using System.Windows.Forms;
using System.Drawing;
using GtkTests.Helpers;

namespace GtkTests.Controls;

[TestFixture]
public class EventClass : TestHelper
{
    private static bool eventhandled;
    public static void Event_Handler1(object? sender, EventArgs e)
    {
        eventhandled = true;
    }

    [Test]
    public void BackColorChangedTest()
    {
        var c = new Control();
        // Test BackColorChanged Event
        c.BackColorChanged += Event_Handler1;
        c.BackColor = Color.Black;
        Assert.That((object?)eventhandled, Is.EqualTo(true));

    }

    [Test]
    public void BgrndImageChangedTest()
    {
        var c = new Control();
        // Test BackgroundImageChanged Event
        c.BackgroundImageChanged += Event_Handler1;
        var abc = new MemoryStream(Properties.Resources.M);
        eventhandled = false;
        c.BackgroundImage = Image.FromStream(abc);
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }

    [Test]
    public void BindingContextChangedTest()
    {
        var c = new Control();
        // Test BindingContextChanged Event
        c.BindingContextChanged += Event_Handler1;
        var bcG1 = new BindingContext();
        eventhandled = false;
        c.BindingContext = bcG1;
        Assert.That((object?)eventhandled, Is.EqualTo(true));

    }

    [Test]
    public void CausesValidationChangedTest()
    {
        var c = new Control();
        // Test CausesValidationChanged Event
        c.CausesValidationChanged += Event_Handler1;
        eventhandled = false;
        c.CausesValidation = true;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }

    [Test]
    public void CursorChangedTest()
    {
        var c = new Control();
        // Test CursorChanged Event
        c.CursorChanged += Event_Handler1;
        eventhandled = false;
        c.Cursor = Cursors.Hand;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }

    [Test]
    public void DisposedTest()
    {
        var c = new Control();
        // Test Disposed Event
        c.Disposed += Event_Handler1;
        eventhandled = false;
        c.Dispose();
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }

    [Test]
    public void DockChangedTest()
    {
        var c = new Control();
        // Test DockChanged Event
        c.DockChanged += Event_Handler1;
        eventhandled = false;
        c.Dock = DockStyle.Bottom;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }

    [Test]
    public void EnabledChangedTest()
    {
        var c = new Control();
        // Test EnabledChanged Event
        c.EnabledChanged += Event_Handler1;
        eventhandled = false;
        c.Enabled = false;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }

    [Test]
    public void FontChangedTest()
    {
        var c = new Control();
        // Test FontChanged Event
        c.FontChanged += Event_Handler1;
        eventhandled = false;
        c.Font = new Font(c.Font, FontStyle.Bold);
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }

    [Test]
    public void ForeColorChangedTest()
    {
        var c = new Control();
        // Test ForeColorChanged Event
        c.ForeColorChanged += Event_Handler1;
        eventhandled = false;
        c.ForeColor = Color.Red;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }

    [Test]
    public void HandleCreatedTest()
    {
        var c = new Control();
        // Test HandleCreated Event
        c.HandleCreated += Event_Handler1;
        eventhandled = false;
        _ = c.Handle.GetType();
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }

    [Test]
    public void ImeModeChangedTest()
    {
        var c = new Control();
        // Test ImeModeChanged Event
        c.ImeModeChanged += Event_Handler1;
        eventhandled = false;
        c.ImeMode = ImeMode.Off;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }

    [Test]
    public void LocationChangedTest()
    {
        var c = new Control();
        // Test LocationChanged Event
        c.LocationChanged += Event_Handler1;
        eventhandled = false;
        c.Left = 20;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }

    [Test]
    public void ResizeTest()
    {
        var c = new Control();
        // Test Resize Event
        c.Resize += Event_Handler1;
        eventhandled = false;
        c.Height = 20;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }

    [Test]
    public void RightToLeftChangedTest()
    {
        var c = new Control();
        // Test RightToLeftChanged Event
        c.RightToLeftChanged += Event_Handler1;
        eventhandled = false;
        c.RightToLeft = RightToLeft.Yes;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }

    [Test]
    public void SizeChangedTest()
    {
        var c = new Control();
        // Test SizeChanged Event
        c.SizeChanged += Event_Handler1;
        eventhandled = false;
        c.Height = 80;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }

    [Test]
    public void TabIndexChangedTest()
    {
        var c = new Control();
        // Test TabIndexChanged Event
        c.TabIndexChanged += Event_Handler1;
        eventhandled = false;
        c.TabIndex = 1;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }

    [Test]
    public void TabStopChangedTest()
    {
        var c = new Control();
        // Test TabStopChanged Event
        c.TabStopChanged += Event_Handler1;
        eventhandled = false;
        c.TabStop = true;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }

    [Test]
    public void TextChangedTest()
    {
        var c = new Control();
        // Test TextChanged Event
        c.TextChanged += Event_Handler1;
        eventhandled = false;
        c.Text = "some Text";
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }

    [Test]
    public void VisibleChangedTest()
    {
        var c = new Control();
        // Test VisibleChanged Event
        c.VisibleChanged += Event_Handler1;
        eventhandled = false;
        c.Visible = false;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }
}


[TestFixture]
public class LayoutEventClass
{
    private static bool eventhandled;
    public static void LayoutEvent(object? sender, LayoutEventArgs e)
    {
        eventhandled = true;
    }

    [Test]
    public void LayoutTest()
    {
        var c = new Control();
        c.Layout += LayoutEvent;
        eventhandled = false;
        c.Visible = true;
        c.Height = 100;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }

    private int event_count;
    private int resize_event;
    private int size_changed_event;
    private int layout_event;

    private void resize(object? sender, EventArgs e)
    {
        resize_event = ++event_count;
    }

    private void layout(object? sender, LayoutEventArgs le)
    {
        layout_event = ++event_count;
    }

    private void size_changed(object? sender, EventArgs e)
    {
        size_changed_event = ++event_count;
    }

    [Test]
    public void LayoutResizeTest()
    {
        var c = new Control();
        c.Layout += layout;
        c.Resize += resize;
        c.SizeChanged += size_changed;
        c.Size = new Size(100, 100);
        Assert.That((object?)layout_event, Is.GreaterThan(0), "1");
        Assert.That((object?)resize_event, Is.GreaterThan(0), "2");
        Assert.That((object?)size_changed_event, Is.GreaterThan(0), "3");
    }
}

[TestFixture]
public class ControlAddRemoveEventClass
{
    private static bool eventhandled;
    public static void ControlEvent(object? sender, ControlEventArgs e)
    {
        eventhandled = true;
    }

    [Test]
    public void ControlAddedTest()
    {
        var c = new Control();
        c.ControlAdded += ControlEvent;
        var TB = new TextBox();
        eventhandled = false;
        c.Controls.Add(TB);
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }

    [Test]
    public void ControlRemovedTest()
    {
        var c = new Control();
        c.ControlRemoved += ControlEvent;
        var TB = new TextBox();
        c.Controls.Add(TB);
        eventhandled = false;
        c.Controls.Remove(TB);
        Assert.That((object?)eventhandled, Is.EqualTo(true));
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
        var c = new Control();
        c.Invalidated += Control_Invalidated;

        c.Visible = true;
        c.Refresh();
        Assert.That((object?)invalidated, Is.EqualTo(1));

        invalidated = 0;
        c.Visible = false;
        c.Refresh();
        Assert.That((object?)invalidated, Is.EqualTo(0));
    }

    [Test]
    [Category("NotWorking")]
    public void Visible()
    {
        var c = new Control();
        c.Invalidated += Control_Invalidated;
        c.Visible = true;

        var form = new Form();
        form.ShowInTaskbar = false;
        form.Controls.Add(c);

        form.Show();
        Assert.That((object?)invalidated, Is.EqualTo(0));

        c.Refresh();
        Assert.That((object?)invalidated, Is.EqualTo(1));

        form.Refresh();
        Assert.That((object?)invalidated, Is.EqualTo(1));
    }

    [Test]
    public void NotVisible()
    {
        var c = new Control();
        c.Invalidated += Control_Invalidated;
        c.Visible = false;

        var form = new Form();
        form.ShowInTaskbar = false;
        form.Controls.Add(c);

        form.Show();
        Assert.That((object?)invalidated, Is.EqualTo(0));

        c.Refresh();
        Assert.That((object?)invalidated, Is.EqualTo(0));

        form.Refresh();
        Assert.That((object?)invalidated, Is.EqualTo(0));

        form.Close();
    }

    private void Control_Invalidated(object? sender, InvalidateEventArgs e)
    {
        invalidated++;
    }

    private int invalidated;
}