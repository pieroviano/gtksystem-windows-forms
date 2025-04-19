//
// Copyright (c) 2005 Novell, Inc.
//
// Authors:
//      Hisham Mardam Bey (hisham.mardambey@gmail.com)
//      Ritvik Mayank (mritvik@novell.com)
//
//

using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using GtkTests.Helpers;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class ScrollBarTest : TestHelper
{
    [Test]
    public void PubPropTest()
    {
        var myscrlbar = new MyScrollBar();

        // B
        myscrlbar.BackColor = Color.Red;
        Assert.That((object?)myscrlbar.BackColor.R, Is.EqualTo(255));
        myscrlbar.BackgroundImage = Image.FromFile(TestResourceHelper.GetFullPathOfResource("Test/System.Windows.Forms/bitmaps/a.png"));
        Assert.That((object?)myscrlbar.BackgroundImage.Height, Is.EqualTo(16));

        // F
        Assert.That((object?)myscrlbar.ForeColor.Name, Is.EqualTo("ControlText"));

        // I
        //Assert1.AreEqual(ImeMode.Disable, myscrlbar.ImeMode);

        // L
        Assert.That((object?)myscrlbar.LargeChange, Is.EqualTo(10));

        // M
        Assert.That((object?)myscrlbar.Maximum, Is.EqualTo(100));
        Assert.That((object?)myscrlbar.Minimum, Is.EqualTo(0));
        myscrlbar.Maximum = 300;
        myscrlbar.Minimum = 100;
        Assert.That((object?)myscrlbar.Maximum, Is.EqualTo(300));
        Assert.That((object?)myscrlbar.Minimum, Is.EqualTo(100));

        // S
        Assert.That((object?)myscrlbar.Site, Is.EqualTo(null));
        Assert.That((object?)myscrlbar.SmallChange, Is.EqualTo(1));
        myscrlbar.SmallChange = 10;
        Assert.That((object?)myscrlbar.SmallChange, Is.EqualTo(10));

        // T
        Assert.That((object?)myscrlbar.TabStop, Is.EqualTo(false));
        myscrlbar.TabStop = true;
        Assert.That((object?)myscrlbar.TabStop, Is.EqualTo(true));
        Assert.That((object?)myscrlbar.Text, Is.EqualTo(string.Empty));
        myscrlbar.Text = "MONO SCROLLBAR";
        Assert.That((object?)myscrlbar.Text, Is.EqualTo("MONO SCROLLBAR"));

        // V
        Assert.That((object?)myscrlbar.Value, Is.EqualTo(100));
        myscrlbar.Value = 150;
        Assert.That((object?)myscrlbar.Value, Is.EqualTo(150));
    }

    [Test]
    public void ExceptionValueTest()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var myscrlbar = new MyScrollBar();
            myscrlbar.Minimum = 10;
            myscrlbar.Maximum = 20;
            myscrlbar.Value = 9;
            myscrlbar.Value = 21;
        });
    }

    [Test]
    public void ExceptionSmallChangeTest()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var myscrlbar = new MyScrollBar();
            myscrlbar.SmallChange = -1;
        });
    }

    [Test]
    public void ExceptionLargeChangeTest()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var myscrlbar = new MyScrollBar();
            myscrlbar.LargeChange = -1;
        });
    }

    [Test]
    public void PubMethodTest()
    {
        var myscrlbar = new MyScrollBar();
        myscrlbar.Text = "New HScrollBar";
        Assert.That((object?)myscrlbar.ToString(), Is.EqualTo("MonoTests.System.Windows.Forms.MyScrollBar, Minimum: 0, Maximum: 100, Value: 0"));
    }

    [Test]
    public void DefaultMarginTest()
    {
        var s = new MyScrollBar();
        object expected = new Padding(0);
        Assert.That((object?)s.PublicDefaultMargin, Is.EqualTo(expected));
    }

    [Test]
    public void MaximumValueTest()
    {
        ScrollBar s = new VScrollBar();

        s.LargeChange = 0;
        s.Maximum = 100;
        s.Value = 20;
        s.Maximum = 0;

        Assert.That((object?)s.LargeChange, Is.EqualTo(0));
        Assert.That((object?)s.Maximum, Is.EqualTo(0));
        Assert.That((object?)s.Value, Is.EqualTo(0));
    }

    [Test]
    public void LargeSmallerThanSmallChange()
    {
        ScrollBar s = new VScrollBar();

        s.LargeChange = 0;

        Assert.That((object?)s.LargeChange, Is.EqualTo(0));
        Assert.That((object?)s.SmallChange, Is.EqualTo(0));

        s.SmallChange = 10;

        Assert.That((object?)s.LargeChange, Is.EqualTo(0));
        Assert.That((object?)s.SmallChange, Is.EqualTo(0));

        s.LargeChange = 15;

        Assert.That((object?)s.LargeChange, Is.EqualTo(15));
        Assert.That((object?)s.SmallChange, Is.EqualTo(10));

        s.LargeChange = 5;

        Assert.That((object?)s.LargeChange, Is.EqualTo(5));
        Assert.That((object?)s.SmallChange, Is.EqualTo(5));
    }

    [Test]
    public void CalculateLargeChange()
    {
        ScrollBar s = new HScrollBar();

        s.Minimum = -50;
        s.Maximum = 50;
        s.LargeChange = 1000;

        Assert.That((object?)s.LargeChange, Is.EqualTo(101));

        s.Maximum = 200;
        s.Minimum = 199;
        s.LargeChange = 1000;

        Assert.That((object?)s.LargeChange, Is.EqualTo(2));

        s.Minimum = 200;
        s.LargeChange = 1000;

        Assert.That((object?)s.LargeChange, Is.EqualTo(1));
    }
}

[TestFixture]
public class ScrollBarEventTest : TestHelper
{
    private static bool eventhandled;
    public void ScrollBar_EventHandler(object? sender, EventArgs e)
    {
        eventhandled = true;
    }

    public void ScrollBarMouse_EventHandler(object? sender, MouseEventArgs e)
    {
        eventhandled = true;
    }

    public void ScrollBarScroll_EventHandler(object? sender, ScrollEventArgs e)
    {
        eventhandled = true;
    }

    public void ScrollBarPaint_EventHandler(object? sender, PaintEventArgs e)
    {
        eventhandled = true;
    }

    [Test]
    public void BackColorChangedTest()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        ScrollBar myHscrlbar = new HScrollBar();
        myform.Controls.Add(myHscrlbar);
        myHscrlbar.BackColorChanged += ScrollBar_EventHandler;
        myHscrlbar.BackColor = Color.Red;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
        eventhandled = false;
        myform.Dispose();
    }

    [Test]
    public void BackgroundImageChangedTest()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        ScrollBar myHscrlbar = new HScrollBar();
        myform.Controls.Add(myHscrlbar);
        myHscrlbar.BackgroundImageChanged += ScrollBar_EventHandler;
        myHscrlbar.BackgroundImage = Image.FromFile(TestResourceHelper.GetFullPathOfResource("Test/System.Windows.Forms/bitmaps/a.png"));
        Assert.That((object?)eventhandled, Is.EqualTo(true));
        eventhandled = false;
        myform.Dispose();
    }

    [Test]
    public void FontChangedTest()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        ScrollBar myHscrlbar = new HScrollBar();
        myform.Controls.Add(myHscrlbar);
        myHscrlbar.Font = new Font(FontFamily.GenericMonospace, 10);
        myHscrlbar.FontChanged += ScrollBar_EventHandler;
        var myFontDialog = new FontDialog();
        myHscrlbar.Font = myFontDialog.Font;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
        eventhandled = false;
        myform.Dispose();
    }

    [Test]
    public void ForeColorChangedTest()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        ScrollBar myHscrlbar = new HScrollBar();
        myform.Controls.Add(myHscrlbar);
        myHscrlbar.ForeColorChanged += ScrollBar_EventHandler;
        myHscrlbar.ForeColor = Color.Azure;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
        eventhandled = false;
        myform.Dispose();
    }

    [Test]
    [Category("NotWorking")]
    public void ScrollTest()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var myHscrlbar = new MyScrollBar();
        myform.Controls.Add(myHscrlbar);
        myHscrlbar.Scroll += ScrollBarScroll_EventHandler;
        myHscrlbar.ScrollNow();

        Assert.That((object?)eventhandled, Is.EqualTo(true));
        eventhandled = false;
        myform.Dispose();
    }

    [Test]
    public void TextChangedTest()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var myHscrlbar = new MyScrollBar();
        myform.Controls.Add(myHscrlbar);
        myHscrlbar.TextChanged += ScrollBar_EventHandler;
        myHscrlbar.Text = "foo";

        Assert.That((object?)eventhandled, Is.EqualTo(true));
        eventhandled = false;
        myform.Dispose();
    }

    [Test]
    public void ValueChangeTest()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var myHscrlbar = new MyScrollBar();
        myform.Controls.Add(myHscrlbar);
        myHscrlbar.Value = 40;
        myHscrlbar.ValueChanged += ScrollBar_EventHandler;
        myHscrlbar.Value = 50;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
        eventhandled = false;
        myform.Dispose();
    }
}

public class MyHScrollBar : HScrollBar
{
    public Size MyDefaultSize => DefaultSize;

    public CreateParams MyCreateParams => CreateParams;
}

[TestFixture]
public class MyHScrollBarTest : TestHelper
{
    [Test]
    public void ProtectedTest()
    {
        var msbar = new MyHScrollBar();

        Assert.That((object?)msbar.MyDefaultSize.Width, Is.EqualTo(80));
        // this is environment dependent.
        //Assert1.AreEqual(21, msbar.MyDefaultSize.Height);
    }
}

public class MyVScrollBar : VScrollBar
{
    public Size MyDefaultSize => DefaultSize;

    public CreateParams MyCreateParams => CreateParams;
}

[TestFixture]
public class MyVScrollBarTest : TestHelper
{
    [Test]
    public void PubMethodTest()
    {
        var msbar = new MyVScrollBar();

        Assert.That((object?)msbar.RightToLeft, Is.EqualTo(RightToLeft.No));

    }

    [Test]
    public void ProtMethodTest()
    {
        var msbar = new MyVScrollBar();

        // This is environment dependent.
        //Assert1.AreEqual(21, msbar.MyDefaultSize.Width);
        Assert.That((object?)msbar.MyDefaultSize.Height, Is.EqualTo(80));
    }
}

[TestFixture]
[Ignore("Tests too strict")]
public class HScrollBarTestEventsOrder : TestHelper
{
    public string[] ArrayListToString(ArrayList arrlist)
    {
        string[] retval = new string[arrlist.Count];
        for (var i = 0; i < arrlist.Count; i++)
            retval[i] = (string)arrlist[i]!;
        return retval;
    }

    [Test]
    public void CreateEventsOrder()
    {
        string[] EventsWanted =
        [
            "OnHandleCreated",
            "OnBindingContextChanged",
            "OnBindingContextChanged"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var s = new MyScrollBar();
        myform.Controls.Add(s);

        Assert.That((object?)ArrayListToString(s.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void BackColorChangedEventsOrder()
    {
        string[] EventsWanted =
        [
            "OnHandleCreated",
            "OnBindingContextChanged",
            "OnBindingContextChanged",
            "OnBackColorChanged",
            "OnInvalidated"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var s = new MyScrollBar();
        myform.Controls.Add(s);
        s.BackColor = Color.Aqua;

        Assert.That((object?)ArrayListToString(s.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void BackgroundImageChangedEventsOrder()
    {
        string[] EventsWanted =
        [
            "OnHandleCreated",
            "OnBindingContextChanged",
            "OnBindingContextChanged",
            "OnBackgroundImageChanged",
            "OnInvalidated"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var s = new MyScrollBar();
        myform.Controls.Add(s);
        s.BackgroundImage = Image.FromFile(TestResourceHelper.GetFullPathOfResource("Test/System.Windows.Forms/bitmaps/a.png"));

        Assert.That((object?)ArrayListToString(s.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    //[Test, Ignore ("Need to send proper Click / DoubleClick")]
    //public void ClickEventsOrder ()
    //   {
    //           string[] EventsWanted = {
    //                   "OnHandleCreated",
    //                     "OnBindingContextChanged",
    //                     "OnBindingContextChanged"
    //           };
    //           Form myform = new Form ();
    //           myform.ShowInTaskbar = false;
    //           myform.Visible = true;
    //           MyScrollBar s = new MyScrollBar ();
    //           myform.Controls.Add (s);
    //           s.MouseClick ();

    //           Assert1.AreEqual(EventsWanted, ArrayListToString (s.Results));
    //           myform.Dispose ();
    //   }

    //[Test, Ignore ("Need to send proper Click / DoubleClick")]
    //public void DoubleClickEventsOrder ()
    //   {
    //           string[] EventsWanted = {
    //                   "OnHandleCreated",
    //                     "OnBindingContextChanged",
    //                     "OnBindingContextChanged"
    //           };
    //           Form myform = new Form ();
    //           myform.ShowInTaskbar = false;
    //           myform.Visible = true;
    //           MyScrollBar s = new MyScrollBar ();
    //           myform.Controls.Add (s);
    //           s.MouseDoubleClick ();

    //           Assert1.AreEqual(EventsWanted, ArrayListToString (s.Results));
    //           myform.Dispose ();
    //   }

    [Test]
    public void FontChangedEventsOrder()
    {
        string[] EventsWanted =
        [
            "OnHandleCreated",
            "OnBindingContextChanged",
            "OnBindingContextChanged"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var s = new MyScrollBar();
        myform.Controls.Add(s);
        var myFontDialog = new FontDialog();
        s.Font = myFontDialog.Font;

        Assert.That((object?)ArrayListToString(s.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void ForeColorChangedEventsOrder()
    {
        string[] EventsWanted =
        [
            "OnHandleCreated",
            "OnBindingContextChanged",
            "OnBindingContextChanged",
            "OnForeColorChanged",
            "OnInvalidated"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var s = new MyScrollBar();
        myform.Controls.Add(s);
        s.ForeColor = Color.Aqua;

        Assert.That((object?)ArrayListToString(s.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void ImeModeChangedChangedEventsOrder()
    {
        string[] EventsWanted =
        [
            "OnHandleCreated",
            "OnBindingContextChanged",
            "OnBindingContextChanged",
            "OnImeModeChanged"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var s = new MyScrollBar();
        myform.Controls.Add(s);
        s.ImeMode = ImeMode.Katakana;

        Assert.That((object?)ArrayListToString(s.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void PaintEventsOrder()
    {
        string[] EventsWanted =
        [
            "OnHandleCreated",
            "OnBindingContextChanged",
            "OnBindingContextChanged",
            "OnInvalidated"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var s = new MyScrollBar();
        myform.Controls.Add(s);
        s.Visible = true;
        s.Refresh();
        Assert.That((object?)ArrayListToString(s.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void ScrollEventsOrder()
    {
        string[] EventsWanted =
        [
            "OnHandleCreated",
            "OnBindingContextChanged",
            "OnBindingContextChanged",
            "OnScroll",
            "OnValueChanged"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var s = new MyScrollBar();
        myform.Controls.Add(s);
        s.ScrollNow();

        Assert.That((object?)ArrayListToString(s.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void TextChangedEventsOrder()
    {
        string[] EventsWanted =
        [
            "OnHandleCreated",
            "OnBindingContextChanged",
            "OnBindingContextChanged",
            "OnTextChanged"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var s = new MyScrollBar();
        myform.Controls.Add(s);
        s.Text = "foobar";

        Assert.That((object?)ArrayListToString(s.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void ValueChangedEventsOrder()
    {
        string[] EventsWanted =
        [
            "OnHandleCreated",
            "OnBindingContextChanged",
            "OnBindingContextChanged",
            "OnValueChanged"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var s = new MyScrollBar();
        myform.Controls.Add(s);
        s.Value = 10;

        Assert.That((object?)ArrayListToString(s.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }
}

public class MyScrollBar2 : HScrollBar
{
    protected ArrayList results = new();
    public MyScrollBar2() : base()
    {
        HandleCreated += HandleCreated_Handler;
        BackColorChanged += BackColorChanged_Handler;
        BackgroundImageChanged += BackgroundImageChanged_Handler;
        BindingContextChanged += BindingContextChanged_Handler;
        Click += Click_Handler;
        DoubleClick += DoubleClick_Handler;
        FontChanged += FontChanged_Handler;
        ForeColorChanged += ForeColorChanged_Handler;
        ImeModeChanged += ImeModeChanged_Handler;
        MouseDown += MouseDown_Handler;
        MouseMove += MouseMove_Handler;
        MouseUp += MouseUp_Handler;
        Invalidated += Invalidated_Handler;
        Resize += Resize_Handler;
        SizeChanged += SizeChanged_Handler;
        Layout += Layout_Handler;
        VisibleChanged += VisibleChanged_Handler;
        Paint += Paint_Handler;
        Scroll += Scroll_Handler;
        TextChanged += TextChanged_Handler;
        ValueChanged += ValueChanged_Handler;
    }

    protected void HandleCreated_Handler(object? sender, EventArgs e)
    {
        results.Add("HandleCreated");
    }

    protected void BackColorChanged_Handler(object? sender, EventArgs e)
    {
        results.Add("BackColorChanged");
    }

    protected void BackgroundImageChanged_Handler(object? sender, EventArgs e)
    {
        results.Add("BackgroundImageChanged");
    }

    protected void Click_Handler(object? sender, EventArgs e)
    {
        results.Add("Click");
    }

    protected void DoubleClick_Handler(object? sender, EventArgs e)
    {
        results.Add("DoubleClick");
    }

    protected void FontChanged_Handler(object? sender, EventArgs e)
    {
        results.Add("FontChanged");
    }

    protected void ForeColorChanged_Handler(object? sender, EventArgs e)
    {
        results.Add("ForeColorChanged");
    }

    protected void ImeModeChanged_Handler(object? sender, EventArgs e)
    {
        results.Add("ImeModeChanged");
    }

    protected void MouseDown_Handler(object? sender, MouseEventArgs e)
    {
        results.Add("MouseDown");
    }

    protected void MouseMove_Handler(object? sender, MouseEventArgs e)
    {
        results.Add("MouseMove");
    }

    protected void MouseUp_Handler(object? sender, MouseEventArgs e)
    {
        results.Add("MouseUp");
    }

    protected void BindingContextChanged_Handler(object? sender, EventArgs e)
    {
        results.Add("BindingContextChanged");
    }

    protected void Invalidated_Handler(object? sender, InvalidateEventArgs e)
    {
        results.Add("Invalidated");
    }

    protected void Resize_Handler(object? sender, EventArgs e)
    {
        results.Add("Resize");
    }

    protected void SizeChanged_Handler(object? sender, EventArgs e)
    {
        results.Add("SizeChanged");
    }

    protected void Layout_Handler(object? sender, LayoutEventArgs e)
    {
        results.Add("Layout");
    }

    protected void VisibleChanged_Handler(object? sender, EventArgs e)
    {
        results.Add("VisibleChanged");
    }

    protected void Paint_Handler(object? sender, PaintEventArgs e)
    {
        results.Add("Paint");
    }

    protected void Scroll_Handler(object? sender, ScrollEventArgs e)
    {
        results.Add("Scroll");
    }

    protected void TextChanged_Handler(object? sender, EventArgs e)
    {
        results.Add("TextChanged");
    }

    protected void ValueChanged_Handler(object? sender, EventArgs e)
    {
        results.Add("ValueChanged");
    }

    public ArrayList Results => results;

    //public void MoveMouse ()
    // {
    //         Message m;

    //         m = new Message ();

    //         m.Msg = (int)WndMsg.WM_NCHITTEST;
    //         m.HWnd = this.Handle;
    //         m.WParam = (IntPtr)0x0;
    //         m.LParam = (IntPtr)0x1c604ea;
    //         this.WndProc(ref m);

    //         m.Msg = (int)WndMsg.WM_SETCURSOR;
    //         m.HWnd = this.Handle;
    //         m.WParam = (IntPtr)0x100448;
    //         m.LParam = (IntPtr)0x2000001;
    //         this.WndProc(ref m);

    //         m.Msg = (int)WndMsg.WM_MOUSEFIRST;
    //         m.HWnd = this.Handle;
    //         m.WParam = (IntPtr)0x0;
    //         m.LParam = (IntPtr)0x14000b;
    //         this.WndProc(ref m);

    //         m.Msg = (int)WndMsg.WM_MOUSEHOVER;
    //         m.HWnd = this.Handle;
    //         m.WParam = (IntPtr)0x0;
    //         m.LParam = (IntPtr)0x14000b;
    //         this.WndProc(ref m);
    // }

    //public void MouseRightDown()
    // {
    //         Message m;

    //         m = new Message();

    //         m.Msg = (int)WndMsg.WM_RBUTTONDOWN;
    //         m.HWnd = this.Handle;
    //         m.WParam = (IntPtr)0x01;
    //         m.LParam = (IntPtr)0x9004f;
    //         this.WndProc(ref m);
    // }

    //public new void MouseClick()
    // {
    //         Message m;

    //         m = new Message();

    //         m.Msg = (int)WndMsg.WM_LBUTTONDOWN;
    //         m.HWnd = this.Handle;
    //         m.WParam = (IntPtr)0x01;
    //         m.LParam = (IntPtr)0x9004f;
    //         this.WndProc(ref m);

    //         m = new Message();

    //         m.Msg = (int)WndMsg.WM_LBUTTONUP;
    //         m.HWnd = this.Handle;
    //         m.WParam = (IntPtr)0x01;
    //         m.LParam = (IntPtr)0x9004f;
    //         this.WndProc(ref m);
    // }

    //public new void MouseDoubleClick ()
    // {
    //         MouseClick ();
    //         MouseClick ();
    // }

    //public void MouseRightUp()
    // {
    //         Message m;

    //         m = new Message();

    //         m.Msg = (int)WndMsg.WM_RBUTTONUP;
    //         m.HWnd = this.Handle;
    //         m.WParam = (IntPtr)0x01;
    //         m.LParam = (IntPtr)0x9004f;
    //         this.WndProc(ref m);
    // }

    public void ScrollNow()
    {
        var m = new Message
        {
            Msg = 8468,
            HWnd = Handle,
            WParam = (IntPtr)0x1,
            LParam = (IntPtr)0x1a051a
        };

        WndProc(ref m);

        m.Msg = 233;
        m.HWnd = Handle;
        m.WParam = (IntPtr)0x1;
        m.LParam = (IntPtr)0x12eb34;
        WndProc(ref m);
    }
}

[TestFixture]
[Ignore("Tests too strict")]
public class HScrollBarTestEventsOrder2 : TestHelper
{
    public string[] ArrayListToString(ArrayList arrlist)
    {
        string[] retval = new string[arrlist.Count];
        for (var i = 0; i < arrlist.Count; i++)
            retval[i] = (string)arrlist[i]!;
        return retval;
    }

    [Test]
    public void CreateEventsOrder()
    {
        string[] EventsWanted =
        [
            "HandleCreated",
            "BindingContextChanged",
            "BindingContextChanged"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var s = new MyScrollBar2();
        myform.Controls.Add(s);

        Assert.That((object?)ArrayListToString(s.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void BackColorChangedEventsOrder()
    {
        string[] EventsWanted =
        [
            "HandleCreated",
            "BindingContextChanged",
            "BindingContextChanged",
            "Invalidated",
            "BackColorChanged"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var s = new MyScrollBar2();
        myform.Controls.Add(s);
        s.BackColor = Color.Aqua;

        Assert.That((object?)ArrayListToString(s.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void BackgroundImageChangedEventsOrder()
    {
        string[] EventsWanted =
        [
            "HandleCreated",
            "BindingContextChanged",
            "BindingContextChanged",
            "Invalidated",
            "BackgroundImageChanged"

        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var s = new MyScrollBar2();
        myform.Controls.Add(s);
        s.BackgroundImage = Image.FromFile(TestResourceHelper.GetFullPathOfResource("Test/System.Windows.Forms/bitmaps/a.png"));

        Assert.That((object?)ArrayListToString(s.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    //[Test, Ignore ("Need to send proper Click / DoubleClick")]
    //public void ClickEventsOrder ()
    //   {
    //           string[] EventsWanted = {
    //                   "HandleCreated",
    //                     "BindingContextChanged",
    //                     "BindingContextChanged"
    //           };
    //           Form myform = new Form ();
    //           myform.ShowInTaskbar = false;
    //           myform.Visible = true;
    //           MyScrollBar2 s = new MyScrollBar2 ();
    //           myform.Controls.Add (s);
    //           s.MouseClick ();

    //           Assert1.AreEqual(EventsWanted, ArrayListToString (s.Results));
    //           myform.Dispose ();
    //   }

    //[Test, Ignore ("Need to send proper Click / DoubleClick")]
    //public void DoubleClickEventsOrder ()
    //   {
    //           string[] EventsWanted = {
    //                   "HandleCreated",
    //                     "BindingContextChanged",
    //                     "BindingContextChanged"
    //           };
    //           Form myform = new Form ();
    //           myform.ShowInTaskbar = false;
    //           myform.Visible = true;
    //           MyScrollBar2 s = new MyScrollBar2 ();
    //           myform.Controls.Add (s);
    //           s.MouseDoubleClick ();

    //           Assert1.AreEqual(EventsWanted, ArrayListToString (s.Results));
    //           myform.Dispose ();
    //   }

    [Test]
    public void FontChangedEventsOrder()
    {
        string[] EventsWanted =
        [
            "HandleCreated",
            "BindingContextChanged",
            "BindingContextChanged"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var s = new MyScrollBar2();
        myform.Controls.Add(s);
        var myFontDialog = new FontDialog();
        s.Font = myFontDialog.Font;

        Assert.That((object?)ArrayListToString(s.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void ForeColorChangedEventsOrder()
    {
        string[] EventsWanted =
        [
            "HandleCreated",
            "BindingContextChanged",
            "BindingContextChanged",
            "Invalidated",
            "ForeColorChanged"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var s = new MyScrollBar2();
        myform.Controls.Add(s);
        s.ForeColor = Color.Aqua;

        Assert.That((object?)ArrayListToString(s.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void ImeModeChangedChangedEventsOrder()
    {
        string[] EventsWanted =
        [
            "HandleCreated",
            "BindingContextChanged",
            "BindingContextChanged",
            "ImeModeChanged"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var s = new MyScrollBar2();
        myform.Controls.Add(s);
        s.ImeMode = ImeMode.Katakana;

        Assert.That((object?)ArrayListToString(s.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void PaintEventsOrder()
    {
        string[] EventsWanted =
        [
            "HandleCreated",
            "BindingContextChanged",
            "BindingContextChanged",
            "Invalidated"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var s = new MyScrollBar2();
        myform.Controls.Add(s);
        s.Visible = true;
        s.Refresh();

        Assert.That((object?)ArrayListToString(s.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void ScrollEventsOrder()
    {
        string[] EventsWanted =
        [
            "HandleCreated",
            "BindingContextChanged",
            "BindingContextChanged",
            "Scroll",
            "ValueChanged"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var s = new MyScrollBar2();
        myform.Controls.Add(s);
        s.ScrollNow();

        Assert.That((object?)ArrayListToString(s.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void TextChangedEventsOrder()
    {
        string[] EventsWanted =
        [
            "HandleCreated",
            "BindingContextChanged",
            "BindingContextChanged",
            "TextChanged"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var s = new MyScrollBar2();
        myform.Controls.Add(s);
        s.Text = "foobar";

        Assert.That((object?)ArrayListToString(s.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void ValueChangedEventsOrder()
    {
        string[] EventsWanted =
        [
            "HandleCreated",
            "BindingContextChanged",
            "BindingContextChanged",
            "ValueChanged"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var s = new MyScrollBar2();
        myform.Controls.Add(s);
        s.Value = 10;

        Assert.That((object?)ArrayListToString(s.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }
}

[TestFixture]
public class ScrollEventArgsTest : TestHelper
{
    [Test]
    public void Defaults()
    {
        var e = new ScrollEventArgs(ScrollEventType.EndScroll, 5);

        Assert.That((object?)e.NewValue, Is.EqualTo(5));
        Assert.That((object?)e.OldValue, Is.EqualTo(-1));
        Assert.That((object?)e.ScrollOrientation, Is.EqualTo(ScrollOrientation.HorizontalScroll));
        Assert.That((object?)e.Type, Is.EqualTo(ScrollEventType.EndScroll));

        e = new ScrollEventArgs(ScrollEventType.EndScroll, 5, 10);

        Assert.That((object?)e.NewValue, Is.EqualTo(10));
        Assert.That((object?)e.OldValue, Is.EqualTo(5));
        Assert.That((object?)e.ScrollOrientation, Is.EqualTo(ScrollOrientation.HorizontalScroll));
        Assert.That((object?)e.Type, Is.EqualTo(ScrollEventType.EndScroll));

        e = new ScrollEventArgs(ScrollEventType.EndScroll, 5, ScrollOrientation.VerticalScroll);

        Assert.That((object?)e.NewValue, Is.EqualTo(5));
        Assert.That((object?)e.OldValue, Is.EqualTo(-1));
        Assert.That((object?)e.ScrollOrientation, Is.EqualTo(ScrollOrientation.VerticalScroll));
        Assert.That((object?)e.Type, Is.EqualTo(ScrollEventType.EndScroll));

        e = new ScrollEventArgs(ScrollEventType.EndScroll, 5, 10, ScrollOrientation.VerticalScroll);

        Assert.That((object?)e.NewValue, Is.EqualTo(10));
        Assert.That((object?)e.OldValue, Is.EqualTo(5));
        Assert.That((object?)e.ScrollOrientation, Is.EqualTo(ScrollOrientation.VerticalScroll));
        Assert.That((object?)e.Type, Is.EqualTo(ScrollEventType.EndScroll));
    }
}

public class MyScrollBar : HScrollBar
{
    private readonly ArrayList results = new();

    public MyScrollBar() : base()
    {
        BackColorChanged += OnBackColorChanged;
        BackgroundImageChanged += OnBackgroundImageChanged;
        Click += OnClick;
        DoubleClick += OnDoubleClick;
        FontChanged += OnFontChanged;
        ForeColorChanged += OnForeColorChanged;
        ImeModeChanged += OnImeModeChanged;
        MouseDown += OnMouseDown;
        MouseMove += OnMouseMove;
        MouseEnter += OnMouseEnter;
        MouseLeave += OnMouseLeave;
        MouseHover += OnMouseHover;
        MouseUp += OnMouseUp;
        HandleCreated += OnHandleCreated;
        BindingContextChanged += OnBindingContextChanged;
        Invalidated += OnInvalidated;
        Resize += OnResize;
        SizeChanged += OnSizeChanged;
        Layout += OnLayout;
        VisibleChanged += OnVisibleChanged;
        Scroll += OnScroll;
        TextChanged += OnTextChanged;
        ValueChanged += OnValueChanged;
        Paint += OnPaint;

    }

    public Padding PublicDefaultMargin => base.DefaultMargin;

    protected void OnBackColorChanged(object? sender, EventArgs e)
    {
        results.Add("OnBackColorChanged");
    }

    protected void OnBackgroundImageChanged(object? sender, EventArgs e)
    {
        results.Add("OnBackgroundImageChanged");
    }

    protected void OnClick(object? sender, EventArgs e)
    {
        results.Add("OnClick");
    }

    protected void OnDoubleClick(object? sender, EventArgs e)
    {
        results.Add("OnDoubleClick");
    }

    protected void OnFontChanged(object? sender, EventArgs e)
    {
        results.Add("OnFontChanged");
    }

    protected void OnForeColorChanged(object? sender, EventArgs e)
    {
        results.Add("OnForeColorChanged");
    }

    protected void OnImeModeChanged(object? sender, EventArgs e)
    {
        results.Add("OnImeModeChanged");
    }

    protected void OnMouseDown(object? sender, MouseEventArgs e)
    {
        results.Add("OnMouseDown");
    }

    protected void OnMouseMove(object? sender, MouseEventArgs e)
    {
        results.Add("OnMouseMove");
    }

    protected void OnMouseEnter(object? sender, EventArgs e)
    {
        results.Add("OnMouseEnter");
    }

    protected void OnMouseLeave(object? sender, EventArgs e)
    {
        results.Add("OnMouseLeave");
    }

    protected void OnMouseHover(object? sender, EventArgs e)
    {
        results.Add("OnMouseHover");
    }

    protected void OnMouseUp(object? sender, MouseEventArgs e)
    {
        results.Add("OnMouseUp");
    }

    protected void OnHandleCreated(object? sender, EventArgs e)
    {
        results.Add("OnHandleCreated");
    }

    protected void OnBindingContextChanged(object? sender, EventArgs e)
    {
        results.Add("OnBindingContextChanged");
    }

    protected void OnInvalidated(object? sender, InvalidateEventArgs e)
    {
        results.Add("OnInvalidated");
    }

    protected void OnResize(object? sender, EventArgs e)
    {
        results.Add("OnResize");
    }

    protected void OnSizeChanged(object? sender, EventArgs e)
    {
        results.Add("OnSizeChanged");
    }

    protected void OnLayout(object? sender, LayoutEventArgs e)
    {
        results.Add("OnLayout");
    }

    protected void OnVisibleChanged(object? sender, EventArgs e)
    {
        results.Add("OnVisibleChanged");
    }

    protected void OnScroll(object? sender, ScrollEventArgs e)
    {
        results.Add("OnScroll");
    }

    protected void OnTextChanged(object? sender, EventArgs e)
    {
        results.Add("OnTextChanged");
    }

    protected void OnValueChanged(object? sender, EventArgs e)
    {
        results.Add("OnValueChanged");
    }

    protected void OnPaint(object? sender, PaintEventArgs e)
    {
        results.Add("OnPaint");
    }

    public ArrayList Results => results;

    //public void MoveMouse ()
    // {
    //         Message m;

    //         m = new Message ();

    //         m.Msg = (int)WndMsg.WM_NCHITTEST;
    //         m.HWnd = this.Handle;
    //         m.WParam = (IntPtr)0x0;
    //         m.LParam = (IntPtr)0x1c604ea;
    //         this.WndProc(ref m);

    //         m.Msg = (int)WndMsg.WM_SETCURSOR;
    //         m.HWnd = this.Handle;
    //         m.WParam = (IntPtr)0x100448;
    //         m.LParam = (IntPtr)0x2000001;
    //         this.WndProc(ref m);

    //         m.Msg = (int)WndMsg.WM_MOUSEFIRST;
    //         m.HWnd = this.Handle;
    //         m.WParam = (IntPtr)0x0;
    //         m.LParam = (IntPtr)0x14000b;
    //         this.WndProc(ref m);

    //         m.Msg = (int)WndMsg.WM_MOUSEHOVER;
    //         m.HWnd = this.Handle;
    //         m.WParam = (IntPtr)0x0;
    //         m.LParam = (IntPtr)0x14000b;
    //         this.WndProc(ref m);
    // }

    //public new void MouseClick()
    // {

    //         Message m;

    //         m = new Message();

    //         m.Msg = (int)WndMsg.WM_LBUTTONDOWN;
    //         m.HWnd = this.Handle;
    //         m.WParam = (IntPtr)0x01;
    //         m.LParam = (IntPtr)0x9004f;
    //         this.WndProc(ref m);

    //         m = new Message();

    //         m.Msg = (int)WndMsg.WM_LBUTTONUP;
    //         m.HWnd = this.Handle;
    //         m.WParam = (IntPtr)0x01;
    //         m.LParam = (IntPtr)0x9004f;
    //         this.WndProc(ref m);
    // }

    //public new void MouseDoubleClick ()
    // {
    //         this.MouseClick ();
    //         this.MouseClick ();
    // }
    //public void MouseRightDown()
    // {
    //         Message m;

    //         m = new Message();

    //         m.Msg = (int)WndMsg.WM_RBUTTONDOWN;
    //         m.HWnd = this.Handle;
    //         m.WParam = (IntPtr)0x01;
    //         m.LParam = (IntPtr)0x9004f;
    //         this.WndProc(ref m);
    // }

    //public void MouseRightUp()
    // {
    //         Message m;

    //         m = new Message();

    //         m.Msg = (int)WndMsg.WM_RBUTTONUP;
    //         m.HWnd = this.Handle;
    //         m.WParam = (IntPtr)0x01;
    //         m.LParam = (IntPtr)0x9004f;
    //         this.WndProc(ref m);
    // }

    public void ScrollNow()
    {
        var m = new Message
        {
            Msg = 8468,
            HWnd = Handle,
            WParam = (IntPtr)0x1,
            LParam = (IntPtr)0x1a051a
        };

        WndProc(ref m);

        m.Msg = 233;
        m.HWnd = Handle;
        m.WParam = (IntPtr)0x1;
        m.LParam = (IntPtr)0x12eb34;
        WndProc(ref m);
    }
}
