using GtkTests.Helpers;
using System.Drawing;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class DefaultLayoutTest : TestHelper
{
    private int event_count;
    private LayoutEventArgs most_recent_args;

    private void p_Layout(object? sender, LayoutEventArgs e)
    {
        event_count++;
        most_recent_args = e;
    }

    [Test]
    public void AnchorLayoutEvents()
    {
        var p = new Panel();

        var b = new Button();
        p.Controls.Add(b);

        p.Layout += p_Layout;

        /* set the button's anchor to something different */
        b.Anchor = AnchorStyles.Bottom;
        Assert.That((object?)event_count, Is.EqualTo(1), "1");
        Assert.That((object?)most_recent_args.AffectedProperty, Is.EqualTo("Anchor"), "2");

        /* reset it to something new with the panel's layout suspended */
        event_count = 0;
        p.SuspendLayout();
        b.Anchor = AnchorStyles.Top;
        Assert.That((object?)event_count, Is.EqualTo(0), "3");
        p.ResumeLayout();
        Assert.That((object?)event_count, Is.EqualTo(1), "4");
        Assert.That((object?)most_recent_args.AffectedProperty, Is.EqualTo("Anchor"), "5");

        /* with the anchor style set to something, resize the parent */
        event_count = 0;
        p.Size = new Size(500, 500);
        Assert.That((object?)event_count, Is.EqualTo(1), "6");
        Assert.That((object?)most_recent_args.AffectedProperty, Is.EqualTo("Bounds"), "7");

        /* now try it with layout suspended */
        event_count = 0;
        p.SuspendLayout();
        p.Size = new Size(400, 400);
        Assert.That((object?)event_count, Is.EqualTo(0), "8");
        p.ResumeLayout();
        Assert.That((object?)event_count, Is.EqualTo(1), "9");
        Assert.That((object?)most_recent_args.AffectedProperty, Is.EqualTo("Bounds"), "10");

        /* with the anchor style set to something, resize the child */
        event_count = 0;
        b.Size = new Size(100, 100);
        // On .NET Framework PerformLayout is called twice; on Mono only once
        //Assert1.AreEqual(2, event_count, "11");
        Assert.That((object?)most_recent_args.AffectedProperty, Is.EqualTo("Bounds"), "12");

        /* and again with layout suspended */
        event_count = 0;
        p.SuspendLayout();
        b.Size = new Size(200, 200);
        Assert.That((object?)event_count, Is.EqualTo(0), "13");
        p.ResumeLayout();
        Assert.That((object?)event_count, Is.EqualTo(1), "14");
        Assert.That((object?)most_recent_args.AffectedProperty, Is.EqualTo("Bounds"), "15");

        /* change two properties when suspended */
        event_count = 0;
        p.SuspendLayout();
        b.Anchor = AnchorStyles.Left;
        b.Size = new Size(150, 150);
        Assert.That((object?)event_count, Is.EqualTo(0), "15");
        p.ResumeLayout();
        Assert.That((object?)event_count, Is.EqualTo(1), "16");
        Assert.That((object?)most_recent_args.AffectedProperty, Is.EqualTo("Bounds"), "17");

        /* and now in the opposite order */
        event_count = 0;
        p.SuspendLayout();
        b.Size = new Size(100, 100);
        b.Anchor = AnchorStyles.Top;
        Assert.That((object?)event_count, Is.EqualTo(0), "18");
        p.ResumeLayout();
        Assert.That((object?)event_count, Is.EqualTo(1), "19");
        Assert.That((object?)most_recent_args.AffectedProperty, Is.EqualTo("Bounds"), "20");
    }

    [Test]
    public void AnchorTopLeftTest()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        f.Size = new Size(200, 200);

        var b = new Button();
        b.Size = new Size(100, 100);
        b.Anchor = AnchorStyles.Top | AnchorStyles.Left;

        f.Controls.Add(b);

        Assert.That((object?)b.Left, Is.EqualTo(0), "1");
        Assert.That((object?)b.Top, Is.EqualTo(0), "2");
        f.Size = new Size(300, 300);

        Assert.That((object?)b.Left, Is.EqualTo(0), "3");
        Assert.That((object?)b.Top, Is.EqualTo(0), "4");

        f.Dispose();
    }

    [Test]
    public void AnchorTopRightTest()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        f.Size = new Size(200, 200);

        var b = new Button();
        b.Size = new Size(100, 100);
        b.Anchor = AnchorStyles.Top | AnchorStyles.Right;

        f.Controls.Add(b);

        Assert.That((object?)b.Left, Is.EqualTo(0), "1");
        Assert.That((object?)b.Top, Is.EqualTo(0), "2");

        f.Size = new Size(300, 300);

        Assert.That((object?)b.Left, Is.EqualTo(100), "3");
        Assert.That((object?)b.Top, Is.EqualTo(0), "4");

        f.Dispose();
    }

    [Test]
    public void AnchorLeftRightTest()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        f.Size = new Size(200, 200);

        var b = new Button();
        b.Size = new Size(100, 100);
        b.Anchor = AnchorStyles.Left | AnchorStyles.Right;

        f.Controls.Add(b);

        Assert.That((object?)b.Left, Is.EqualTo(0), "1");
        Assert.That((object?)b.Right, Is.EqualTo(100), "2");

        f.Size = new Size(300, 300);

        Assert.That((object?)b.Left, Is.EqualTo(0), "3");
        Assert.That((object?)b.Right, Is.EqualTo(200), "4");

        f.Dispose();
    }

    [Test]
    public void AnchorBottomLeftTest()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        f.Size = new Size(200, 200);

        var b = new Button();
        b.Size = new Size(100, 100);
        b.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;

        f.Controls.Add(b);

        Assert.That((object?)b.Left, Is.EqualTo(0), "1");
        Assert.That((object?)b.Top, Is.EqualTo(0), "2");

        f.Size = new Size(300, 300);

        Assert.That((object?)b.Left, Is.EqualTo(0), "3");
        Assert.That((object?)b.Top, Is.EqualTo(100), "4");

        f.Dispose();
    }

    [Test]
    public void AnchorBottomRightTest()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        f.Size = new Size(200, 200);

        var b = new Button();
        b.Size = new Size(100, 100);
        b.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;

        f.Controls.Add(b);

        Assert.That((object?)b.Left, Is.EqualTo(0), "1");
        Assert.That((object?)b.Top, Is.EqualTo(0), "2");

        f.Size = new Size(300, 300);

        Assert.That((object?)b.Left, Is.EqualTo(100), "3");
        Assert.That((object?)b.Top, Is.EqualTo(100), "4");

        f.Dispose();
    }

    [Test]
    public void AnchorTopBottomTest()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        f.Size = new Size(200, 200);

        var b = new Button();
        b.Size = new Size(100, 100);
        b.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;

        f.Controls.Add(b);

        Assert.That((object?)b.Top, Is.EqualTo(0), "1");
        Assert.That((object?)b.Bottom, Is.EqualTo(100), "2");

        f.Size = new Size(300, 300);

        Assert.That((object?)b.Top, Is.EqualTo(0), "3");
        Assert.That((object?)b.Bottom, Is.EqualTo(200), "4");

        f.Dispose();
    }

    // Unit test version of the test case in bug #80336
    [Test]
    public void AnchorSuspendLayoutTest()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        f.SuspendLayout();

        var b = new Button();
        b.Size = new Size(100, 100);

        f.Controls.Add(b);

        f.Size = new Size(200, 200);

        b.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

        Assert.That((object?)b.Top, Is.EqualTo(0), "1");
        Assert.That((object?)b.Left, Is.EqualTo(0), "2");

        f.Size = new Size(300, 300);

        Assert.That((object?)b.Top, Is.EqualTo(0), "3");
        Assert.That((object?)b.Left, Is.EqualTo(0), "4");

        f.ResumeLayout();

        Assert.That((object?)b.Top, Is.EqualTo(100), "5");
        Assert.That((object?)b.Left, Is.EqualTo(100), "6");

        f.Dispose();
    }

    // another variant of AnchorSuspendLayoutTest1, with
    // the SuspendLayout moved after the Anchor
    // assignment.
    [Test]
    public void AnchorSuspendLayoutTest2()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        var b = new Button();
        b.Size = new Size(100, 100);

        f.Controls.Add(b);

        f.Size = new Size(200, 200);

        b.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

        Assert.That((object?)b.Top, Is.EqualTo(0), "1");
        Assert.That((object?)b.Left, Is.EqualTo(0), "2");

        f.SuspendLayout();

        f.Size = new Size(300, 300);

        Assert.That((object?)b.Top, Is.EqualTo(0), "3");
        Assert.That((object?)b.Left, Is.EqualTo(0), "4");

        f.ResumeLayout();

        Assert.That((object?)b.Top, Is.EqualTo(100), "5");
        Assert.That((object?)b.Left, Is.EqualTo(100), "6");

        f.Dispose();
    }

    // yet another variant, this time with no Suspend/Resume.
    [Test]
    public void AnchorSuspendLayoutTest3()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        var b = new Button();
        b.Size = new Size(100, 100);

        f.Controls.Add(b);

        f.Size = new Size(200, 200);

        b.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

        Assert.That((object?)b.Top, Is.EqualTo(0), "1");
        Assert.That((object?)b.Left, Is.EqualTo(0), "2");

        f.Size = new Size(300, 300);

        Assert.That((object?)b.Top, Is.EqualTo(100), "5");
        Assert.That((object?)b.Left, Is.EqualTo(100), "6");

        f.Dispose();
    }

    private string event_raised = string.Empty;

    [Test]
    public void TestAnchorDockInteraction()
    {
        var p = new Panel();
        p.DockChanged += DockChanged_Handler;

        Assert.That((object?)p.Anchor, Is.EqualTo(AnchorStyles.Top | AnchorStyles.Left));
        Assert.That((object?)p.Dock, Is.EqualTo(DockStyle.None));

        p.Dock = DockStyle.Right;
        Assert.That((object?)p.Anchor, Is.EqualTo(AnchorStyles.Top | AnchorStyles.Left));
        Assert.That((object?)p.Dock, Is.EqualTo(DockStyle.Right));
        Assert.That((object?)event_raised, Is.EqualTo("DockStyleChanged"));
        event_raised = string.Empty;

        p.Anchor = AnchorStyles.Bottom;
        Assert.That((object?)p.Anchor, Is.EqualTo(AnchorStyles.Bottom));
        Assert.That((object?)p.Dock, Is.EqualTo(DockStyle.None));
        object expected = string.Empty;
        Assert.That((object?)event_raised, Is.EqualTo(expected));

        p.Dock = DockStyle.Fill;
        Assert.That((object?)p.Anchor, Is.EqualTo(AnchorStyles.Top | AnchorStyles.Left));
        Assert.That((object?)p.Dock, Is.EqualTo(DockStyle.Fill));
        Assert.That((object?)event_raised, Is.EqualTo("DockStyleChanged"));
        event_raised = string.Empty;

        p.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        Assert.That((object?)p.Anchor, Is.EqualTo(AnchorStyles.Top | AnchorStyles.Left));
        Assert.That((object?)p.Dock, Is.EqualTo(DockStyle.Fill));
        object expected1 = string.Empty;
        Assert.That((object?)event_raised, Is.EqualTo(expected1));

        p.Dock = DockStyle.None;
        Assert.That((object?)p.Anchor, Is.EqualTo(AnchorStyles.Top | AnchorStyles.Left));
        Assert.That((object?)p.Dock, Is.EqualTo(DockStyle.None));
        Assert.That((object?)event_raised, Is.EqualTo("DockStyleChanged"));
        event_raised = string.Empty;

        p.Anchor = AnchorStyles.Bottom;
        p.Dock = DockStyle.None;
        Assert.That((object?)p.Anchor, Is.EqualTo(AnchorStyles.Bottom));
        Assert.That((object?)p.Dock, Is.EqualTo(DockStyle.None));
        object expected2 = string.Empty;
        Assert.That((object?)event_raised, Is.EqualTo(expected2));
    }

    public void DockChanged_Handler(object? sender, EventArgs e)
    {
        event_raised += "DockStyleChanged";
    }

    [Test]	// bug #80917
    public void BehaviorOverriddenDisplayRectangle()
    {
        var c = new Control();
        c.Anchor |= AnchorStyles.Bottom;
        c.Size = new Size(100, 100);

        Form f = new DisplayRectangleForm();
        f.Controls.Add(c);
        f.ShowInTaskbar = false;
        f.Show();

        object expected = new Size(100, 100);
        Assert.That((object?)c.Size, Is.EqualTo(expected));

        f.Dispose();
    }

    private class DisplayRectangleForm : Form
    {
        public override Rectangle DisplayRectangle => Rectangle.Empty;
    }

    [Test]  // bug 80912
    public void AnchoredControlWithZeroWidthAndHeight()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        var c = new Control();
        c.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        f.Controls.Add(c);

        object expected = new Rectangle(0, 0, 0, 0);
        Assert.That((object?)c.Bounds, Is.EqualTo(expected));
    }

    [Test] // bug 81694
    public void TestNestedControls()
    {
        var f = new MainForm();
        f.ShowInTaskbar = false;

        f.Show();
        object expected = new Rectangle(210, 212, 75, 23);
        Assert.That((object?)f._userControl._button2.Bounds, Is.EqualTo(expected));

        f.Dispose();
    }

    [Test] // bug 81695
    public void TestNestedControls2()
    {
        var f = new MainForm();
        f.ShowInTaskbar = false;

        f.Show();

        var s = f.Size;
        f.Size = new Size(10, 10);
        f.Size = s;

        object expected = new Rectangle(210, 212, 75, 23);
        Assert.That((object?)f._userControl._button2.Bounds, Is.EqualTo(expected));

        f.Dispose();
    }

    private class MainForm : Form
    {
        public readonly UserControl1 _userControl;

        public MainForm()
        {
            SuspendLayout();
            // 
            // _userControl
            // 
            _userControl = new UserControl1();
            _userControl.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right);
            _userControl.BackColor = Color.White;
            _userControl.Location = new Point(8, 8);
            _userControl.Size = new Size(288, 238);
            _userControl.TabIndex = 0;
            Controls.Add(_userControl);
            // 
            // MainForm
            // 
            ClientSize = new Size(304, 280);
            Location = new Point(250, 100);
            StartPosition = FormStartPosition.Manual;
            Text = "bug #81694";
            ResumeLayout(false);
        }
    }

    private class UserControl1 : UserControl
    {
        private readonly Button _button1;
        public readonly Button _button2;

        public UserControl1()
        {
            SuspendLayout();
            // 
            // _button1
            // 
            _button1 = new Button();
            _button1.Location = new Point(4, 4);
            _button1.Size = new Size(75, 23);
            _button1.TabIndex = 0;
            _button1.Text = "Button1";
            Controls.Add(_button1);
            // 
            // _button2
            // 
            _button2 = new Button();
            _button2.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            _button2.Location = new Point(210, 212);
            _button2.Size = new Size(75, 23);
            _button2.TabIndex = 1;
            _button2.Text = "Button2";
            Controls.Add(_button2);
            // 
            // UserControl1
            // 
            BackColor = Color.White;
            ClientSize = new Size(288, 238);
            ResumeLayout(false);
        }
    }

    [Test]
    public void TestDockFillWithPadding()
    {
        var f = new Form();
        f.ShowInTaskbar = false;
        f.Padding = new Padding(15, 15, 15, 15);

        var c = new Control();
        c.Dock = DockStyle.Fill;
        f.Controls.Add(c);

        f.Show();
        object expected = new Size(f.ClientSize.Width - 30, f.ClientSize.Height - 30);
        Assert.That((object?)c.Size, Is.EqualTo(expected));

        f.Dispose();
    }

    [Test]
    public void Bug82762()
    {
        var f = new Form();
        f.ShowInTaskbar = false;
        f.ClientSize = new Size(284, 264);

        var b = new Button();
        b.Size = new Size(100, 100);
        b.Anchor = AnchorStyles.None;
        f.Controls.Add(b);

        f.Show();

        object expected = new Rectangle(0, 0, 100, 100);
        Assert.That((object?)b.Bounds, Is.EqualTo(expected));

        f.ClientSize = new Size(600, 600);

        object expected1 = new Rectangle(158, 168, 100, 100);
        Assert.That((object?)b.Bounds, Is.EqualTo(expected1));

        f.Close();
        f.Dispose();
    }

    [Test]
    public void Bug82805()
    {
        var c1 = new Control();
        c1.Size = new Size(100, 100);
        var c2 = new Control();
        c2.Size = new Size(100, 100);

        c2.SuspendLayout();
        c1.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        c2.Controls.Add(c1);
        c2.Size = new Size(200, 200);
        c2.ResumeLayout();

        Assert.That((object?)c1.Width, Is.EqualTo(200));
    }

    [Test]
    public void DockedAutoSizeControls()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        var b = new Button();
        b.Text = "Yo";
        b.AutoSize = true;
        b.Width = 200;
        b.Dock = DockStyle.Left;
        f.Controls.Add(b);

        f.Show();

        if (b.Width >= 200)
            Assert.Fail("button should be less than 200 width: actual {0}", b.Width);

        f.Close();
        f.Dispose();
    }

    [Test]  // bug #81199
    public void NestedControls()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        var c = new MyUserControl();
        c.Dock = DockStyle.Fill;
        c.Size = new Size(500, 500);

        f.SuspendLayout();
        f.Controls.Add(c);
        f.ClientSize = new Size(500, 500);
        f.ResumeLayout(false);

        f.Show();

        object expected = new Size(600, 600);
        Assert.That((object?)c.lv.Size, Is.EqualTo(expected));
        f.Close();
    }

    private class MyUserControl : UserControl
    {
        public readonly ListView lv;

        public MyUserControl()
        {
            lv = new ListView();
            SuspendLayout();
            lv.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            lv.Size = new Size(300, 300);

            Controls.Add(lv);
            Size = new Size(200, 200);
            ResumeLayout(false);
        }
    }
}

[TestFixture]
public class DockingTests : TestHelper
{
    private Form form;
    private Panel panel;

    [TearDown]
    public void TestTearDown()
    {
        panel.Dispose();
    }

    private int event_count;

    [SetUp]
    protected override void SetUp()
    {
        form = new Form();
        form.ShowInTaskbar = false;
        form.Size = new Size(400, 400);
        panel = new Panel();
        form.Controls.Add(panel);
        event_count = 0;
        base.SetUp();
    }

    [TearDown]
    protected override void TearDown()
    {
        form.Dispose();
        base.TearDown();
    }

    private void IncrementEventCount(object? o, EventArgs args)
    {
        event_count++;
    }

    [Test]
    public void TestDockSizeChangedEvent()
    {
        panel.SizeChanged += IncrementEventCount;
        panel.Dock = DockStyle.Bottom;
        Assert.That((object?)event_count, Is.EqualTo(1));
    }

    [Test]
    public void TestDockLocationChangedEvent()
    {
        panel.LocationChanged += IncrementEventCount;
        panel.Dock = DockStyle.Bottom;
        Assert.That((object?)event_count, Is.EqualTo(1));
    }

    [Test]
    public void TestDockFillFirst()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        var b1 = new Panel();
        var b2 = new Panel();

        b1.Dock = DockStyle.Fill;
        b2.Dock = DockStyle.Left;

        f.Controls.Add(b1);
        f.Controls.Add(b2);

        f.Show();
        object expected = new Rectangle(b2.Width, 0, f.ClientRectangle.Width - b2.Width, f.ClientRectangle.Height);
        Assert.That((object?)b1.Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle(0, 0, 200, f.ClientRectangle.Height);
        Assert.That((object?)b2.Bounds, Is.EqualTo(expected1));
        f.Dispose();
    }

    [Test]
    public void TestDockFillLast()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        var b1 = new Panel();
        var b2 = new Panel();

        b1.Dock = DockStyle.Fill;
        b2.Dock = DockStyle.Left;

        f.Controls.Add(b2);
        f.Controls.Add(b1);

        f.Show();
        object expected = new Rectangle(0, 0, f.ClientRectangle.Width, f.ClientRectangle.Height);
        Assert.That((object?)b1.Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle(0, 0, 200, f.ClientRectangle.Height);
        Assert.That((object?)b2.Bounds, Is.EqualTo(expected1));
        f.Dispose();
    }

    [Test]  // bug #81397
    public void TestDockingWithCustomDisplayRectangle()
    {
        var mc = new MyControl();
        mc.Size = new Size(200, 200);

        var c = new Control();
        c.Dock = DockStyle.Fill;

        mc.Controls.Add(c);

        var f = new Form();
        f.ShowInTaskbar = false;

        f.Controls.Add(mc);
        f.Show();

        object expected = new Point(20, 20);
        Assert.That((object?)c.Location, Is.EqualTo(expected));
        object expected1 = new Size(160, 160);
        Assert.That((object?)c.Size, Is.EqualTo(expected1));

        f.Dispose();
    }

    private class MyControl : Control
    {
        public override Rectangle DisplayRectangle => new(20, 20, Width - 40, Height - 40);
    }

    [Test]
    public void DockingPreferredSize()
    {
        var f = new Form();
        f.ShowInTaskbar = false;
        f.ClientSize = new Size(300, 300);

        var c1 = new C1();
        c1.Size = new Size(100, 100);
        c1.Dock = DockStyle.Left;

        f.Controls.Add(c1);
        f.Show();

        object expected = new Size(100, 300);
        Assert.That((object?)c1.Size, Is.EqualTo(expected));

        f.Controls.Clear();
        var c2 = new C2();
        c2.Size = new Size(100, 100);
        c2.Dock = DockStyle.Left;

        f.Controls.Add(c2);
        object expected1 = new Size(100, 300);
        Assert.That((object?)c1.Size, Is.EqualTo(expected1));

        f.Dispose();
    }

    private class C1 : Panel
    {
        public override Size GetPreferredSize(Size proposedSize)
        {
            Console.WriteLine("HOYO!");
            return new Size(200, 200);
        }
    }

    private class C2 : Panel
    {
        public override Size GetPreferredSize(Size proposedSize)
        {
            return Size.Empty;
        }
    }

    [Test]
    public void ResettingDockToNone()
    {
        var f = new Form();
        f.ShowInTaskbar = false;
        f.ClientSize = new Size(300, 300);

        var c = new Control();
        c.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

        f.Controls.Add(c);

        f.Show();

        f.ClientSize = new Size(350, 350);

        Assert.IsTrue(c.Left > 0, $"A1: c.Left ({c.Left}) must be greater than 0");
        Assert.IsTrue(c.Top > 0, $"A2: c.Top ({c.Top}) must be greater than 0");

        c.Dock = DockStyle.None;
        Assert.IsTrue(c.Left > 0, $"A3: c.Left ({c.Left}) must be greater than 0");
        Assert.IsTrue(c.Top > 0, $"A4: c.Top ({c.Top}) must be greater than 0");

        f.ClientSize = new Size(400, 400);
        Assert.IsTrue(c.Left > 70, $"A5: c.Left ({c.Left}) must be greater than 70");
        Assert.IsTrue(c.Top > 70, $"A6: c.Top ({c.Top}) must be greater than 70");

        f.Dispose();
    }
}