//
// Copyright (c) 2005 Novell, Inc.
//
// Authors:
//      Hisham Mardam Bey (hisham.mardambey@gmail.com)
//
//

using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using GtkTests.Helpers;
using GtkTests.Utility;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class LabelTest : TestHelper
{
    [Test]
    public void LabelAccessibility()
    {
        var l = new Label();
        Assert.IsNotNull(l.AccessibilityObject, "1");
    }

    [Test]
    public void SizesTest()
    {
        var myform = new Form();
        var l1 = new Label(); l1.Text = "Test";
        var l2 = new Label(); l2.Text = "Test";
        var l3 = new Label(); l3.Text = "Test three";
        var l4 = new Label(); l4.Text = $"Test four{Environment.NewLine}with line breaks";
        myform.Controls.Add(l1);
        myform.Controls.Add(l2);
        myform.Controls.Add(l3);
        myform.Controls.Add(l4);
        myform.Show();

        l2.Font = new Font(l1.Font!.FontFamily, l1.Font.Size + 5, l1.Font.Style);

        // Height: autosize = false
        Assert.That((object?)l2.Height, Is.EqualTo(l1.Height));
        Assert.That((object?)l3.Height, Is.EqualTo(l1.Height));
        Assert.That((object?)l4.Height, Is.EqualTo(l1.Height));

        // Width: autosize = false			
        Assert.That((object?)l2.Width, Is.EqualTo(l1.Width));
        Assert.That((object?)l3.Width, Is.EqualTo(l1.Width));
        Assert.That((object?)l4.Width, Is.EqualTo(l1.Width));

        l1.AutoSize = true;
        l2.AutoSize = true;
        l3.AutoSize = true;
        l4.AutoSize = true;

        // Height: autosize = false
        Assert.IsFalse(l1.Height.Equals(l2.Height));
        Assert.IsTrue(l1.Height.Equals(l3.Height));
        Assert.IsTrue((l4.Height > l1.Height));

        // Width: autosize = false
        Assert.IsFalse(l1.Width.Equals(l2.Width));
        Assert.IsFalse(l1.Width.Equals(l3.Width));

        myform.Dispose();
    }

    [Test]
    public void StyleTest()
    {
        var l = new Label();

        Assert.IsFalse(IsStyleSet(l, WindowStyles.WS_BORDER));
        Assert.IsFalse(IsExStyleSet(l, WindowExStyles.WS_EX_CLIENTEDGE));
        Assert.IsFalse(IsExStyleSet(l, WindowExStyles.WS_EX_STATICEDGE));

        l.BorderStyle = BorderStyle.None;

        Assert.IsFalse(IsStyleSet(l, WindowStyles.WS_BORDER));
        Assert.IsFalse(IsExStyleSet(l, WindowExStyles.WS_EX_CLIENTEDGE));
        Assert.IsFalse(IsExStyleSet(l, WindowExStyles.WS_EX_STATICEDGE));

        l.BorderStyle = BorderStyle.FixedSingle;

        Assert.IsTrue(IsStyleSet(l, WindowStyles.WS_BORDER));
        Assert.IsFalse(IsExStyleSet(l, WindowExStyles.WS_EX_CLIENTEDGE));
        Assert.IsFalse(IsExStyleSet(l, WindowExStyles.WS_EX_STATICEDGE));

        l.BorderStyle = BorderStyle.Fixed3D;

        Assert.IsFalse(IsStyleSet(l, WindowStyles.WS_BORDER));
        Assert.IsFalse(IsExStyleSet(l, WindowExStyles.WS_EX_CLIENTEDGE));
        Assert.IsTrue(IsExStyleSet(l, WindowExStyles.WS_EX_STATICEDGE));
    }

    [Test]
    public void BoundsTest()
    {
        var l = new Label();

        object expected = new Rectangle(0, 0, 100, 23);
        Assert.That((object?)l.Bounds, Is.EqualTo(expected), "1");
        object expected1 = new Rectangle(0, 0, 100, 23);
        Assert.That((object?)l.ClientRectangle, Is.EqualTo(expected1), "2");
        object expected2 = new Size(100, 23);
        Assert.That((object?)l.ClientSize, Is.EqualTo(expected2), "3");
    }

    [Test]
    public void PubPropTest()
    {
        var l = new Label();

        Assert.IsFalse(l.AutoSize);

        Assert.That((object?)l.BackColor.Name, Is.EqualTo("Control"));
        Assert.IsNull(l.BackgroundImage);
        Assert.That((object?)l.BorderStyle, Is.EqualTo(BorderStyle.None));

        Assert.IsNull(l.Container);
        Assert.IsFalse(l.ContainsFocus);
        Assert.IsFalse(l.Created);
        Assert.That((object?)l.Cursor, Is.EqualTo(Cursors.Default));

        Assert.IsNotNull(l.DataBindings);
        Assert.That((object?)l.Dock, Is.EqualTo(DockStyle.None));

        Assert.IsTrue(l.Enabled);

        Assert.IsFalse(l.Focused);
        Assert.That((object?)l.ForeColor, Is.EqualTo(SystemColors.ControlText));

        Assert.IsFalse(l.HasChildren);

        Assert.IsNull(l.Image);
        Assert.That((object?)l.ImageAlign, Is.EqualTo(ContentAlignment.MiddleCenter));
        Assert.IsFalse(l.InvokeRequired);
        Assert.IsFalse(l.IsAccessible);
        Assert.IsFalse(l.IsDisposed);

        Assert.IsNull(l.Parent);

        Assert.IsFalse(l.RecreatingHandle);
        Assert.IsNull(l.Region);
        Assert.That((object?)l.RightToLeft, Is.EqualTo(RightToLeft.No));

        Assert.IsNull(l.Site);

        Assert.That((object?)l.TabIndex, Is.EqualTo(0));
        Assert.IsNull(l.Tag);
        Assert.That((object?)l.Text, Is.EqualTo(string.Empty));
        Assert.That((object?)l.TextAlign, Is.EqualTo(ContentAlignment.TopLeft));
        Assert.IsNull(l.TopLevelControl);

        Assert.IsTrue(l.Visible);
    }

    [Test]
    public void LabelEqualsTest()
    {
        var s1 = new Label();
        var s2 = new Label();
        s1.Text = "abc";
        s2.Text = "abc";
        Assert.IsFalse(s1.Equals(s2));
        Assert.IsTrue(s1.Equals(s1));
    }

    [Test]
    public void LabelScaleTest()
    {
        var r1 = new Label();
        r1.Width = 40;
        r1.Height = 20;
        r1.Scale(2);
        Assert.That((object?)r1.Width, Is.EqualTo(80));
        Assert.That((object?)r1.Height, Is.EqualTo(40));

    }

    [Test]
    public void ToStringTest()
    {
        var l = new Label();

        l.Text = "My Label";

        Assert.That((object?)l.ToString(), Is.EqualTo("System.Windows.Forms.Label, Text: My Label"));
    }

    [Test]
    public void AutoSizeExplicitSize()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        var l = new Label();
        l.Size = new Size(5, 5);
        l.AutoSize = true;
        l.Text = "My Label";

        f.Controls.Add(l);

        var s = l.Size;

        l.Width = 10;
        Assert.That((object?)l.Size, Is.EqualTo(s));

        l.Height = 10;
        Assert.That((object?)l.Size, Is.EqualTo(s));
    }

    [Test]
    public void LabelMargin()
    {
        object expected = new Padding(3, 0, 3, 0);
        Assert.That((object?)new Label().Margin, Is.EqualTo(expected));
    }

    [Test]
    public void SelfSizingTest()
    {
        var p1 = new TableLayoutPanel();
        var l1 = new Label();
        l1.AutoSize = true;
        p1.Controls.Add(l1);
        p1.SuspendLayout();
        var l1_saved_size = l1.Size;
        l1.Text = "Text";
        Assert.That((object?)l1.Size, Is.EqualTo(l1_saved_size));
        p1.ResumeLayout();
        Assert.That((object?)l1.Size, Is.Not.EqualTo(l1_saved_size), "#1a");

        var p2 = new FlowLayoutPanel();
        var l2 = new Label();
        l2.AutoSize = true;
        p2.Controls.Add(l2);
        p2.SuspendLayout();
        var l2_saved_size = l2.Size;
        l2.Text = "Text";
        Assert.That((object?)l2.Size, Is.EqualTo(l2_saved_size));
        p2.ResumeLayout();
        Assert.That((object?)l2.Size, Is.Not.EqualTo(l2_saved_size), "#2a");

        var p3 = new Panel();
        var l3 = new Label();
        l3.AutoSize = true;
        p3.Controls.Add(l3);
        p3.SuspendLayout();
        var l3_saved_size = l3.Size;
        l3.Text = "Text";
        Assert.That((object?)l3.Size, Is.Not.EqualTo(l3_saved_size));
        p3.ResumeLayout();
    }
}

[TestFixture]
public class LabelEventTest : TestHelper
{
    private static bool eventhandled;
    public void Label_EventHandler(object? sender, EventArgs e)
    {
        eventhandled = true;
    }

    public void Label_KeyDownEventHandler(object? sender, KeyEventArgs e)
    {
        eventhandled = true;
    }

    [Ignore("AutoSize moved to Control in 2.0, Label.AutoSize needs to be reworked a bit.")]
    [Test]
    public void AutoSizeChangedChangedTest()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var l = new Label();
        l.Visible = true;
        myform.Controls.Add(l);
        l.AutoSizeChanged += Label_EventHandler;
        l.AutoSize = true;
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
        var l = new Label();
        l.Visible = true;
        myform.Controls.Add(l);
        l.BackgroundImageChanged += Label_EventHandler;
        l.BackgroundImage = Image.FromFile(TestResourceHelper.GetFullPathOfResource("Test/System.Windows.Forms/bitmaps/a.png"));
        Assert.That((object?)eventhandled, Is.EqualTo(true));
        eventhandled = false;
        myform.Dispose();
    }

    [Test]
    public void ImeModeChangedTest()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var l = new Label();
        l.Visible = true;
        myform.Controls.Add(l);
        l.ImeModeChanged += Label_EventHandler;
        l.ImeMode = ImeMode.Katakana;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
        eventhandled = false;
        myform.Dispose();
    }

    [Test]
    public void KeyDownTest()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var l = new MyLabel();
        l.Visible = true;
        myform.Controls.Add(l);
        l.KeyDown += Label_KeyDownEventHandler;
        l.KeyPressA();

        Assert.That((object?)eventhandled, Is.EqualTo(true));
        eventhandled = false;
        myform.Dispose();
    }

    [Test]
    public void TabStopChangedTest()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var l = new Label();
        l.Visible = true;
        myform.Controls.Add(l);
        l.TabStopChanged += Label_EventHandler;
        l.TabStop = true;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
        eventhandled = false;
        myform.Dispose();
    }

}

public class MyLabelInvalidate : MyLabel
{
    //protected ArrayList results = new ArrayList ();
    public MyLabelInvalidate() : base()
    {
        Invalidated += OnInvalidated;
    }

    protected new void OnInvalidated(object? sender, InvalidateEventArgs e)
    {
        var res = (string?)results[results.Count - 1];
        results[results.Count - 1] = string.Concat(res, "," + e.InvalidRect.ToString());
        //results.Add ("OnInvalidate," + e.InvalidRect.ToString ());
    }

    //public ArrayList Results {
    //	get {	return results; }
    //}

}

public class MyLabel : Label
{
    protected ArrayList results = new();

    public MyLabel() : base()
    {
        AutoSizeChanged += OnAutoSizeChanged;
        BackgroundImageChanged += OnBackgroundImageChanged;
        ImeModeChanged += OnImeModeChanged;
        KeyDown += OnKeyDown;
        KeyPress += OnKeyPress;
        KeyUp += OnKeyUp;
        HandleCreated += OnHandleCreated;
        BindingContextChanged += OnBindingContextChanged;
        Invalidated += OnInvalidated;
        Resize += OnResize;
        SizeChanged += OnSizeChanged;
        Layout += OnLayout;
        VisibleChanged += OnVisibleChanged;
        Paint += OnPaint;
    }

    protected void OnAutoSizeChanged(object? sender, EventArgs e)
    {
        results.Add("OnAutoSizeChanged");
    }

    protected void OnBackgroundImageChanged(object? sender, EventArgs e)
    {
        results.Add("OnBackgroundImageChanged");
    }

    protected void OnImeModeChanged(object? sender, EventArgs e)
    {
        results.Add("OnImeModeChanged");
    }

    protected void OnKeyDown(object? sender, KeyEventArgs e)
    {
        results.Add("OnKeyDown," + (char)e.KeyValue);
    }

    protected void OnKeyPress(object? sender, KeyPressEventArgs e)
    {
        results.Add("OnKeyPress," + e.KeyChar.ToString());
    }

    protected void OnKeyUp(object? sender, KeyEventArgs e)
    {
        results.Add("OnKeyUp," + (char)e.KeyValue);
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

    protected void OnPaint(object? sender, PaintEventArgs e)
    {
        results.Add("OnPaint");
    }

    public void KeyPressA()
    {
        var m = new Message
        {
            Msg = (int)WndMsg.WM_KEYDOWN,
            HWnd = Handle,
            WParam = (IntPtr)0x41,
            LParam = (IntPtr)0x1e0001
        };

        WndProc(ref m);

        m.Msg = (int)WndMsg.WM_CHAR;
        m.HWnd = Handle;
        m.WParam = (IntPtr)0x61;
        m.LParam = (IntPtr)0x1e0001;
        WndProc(ref m);

        m.Msg = (int)WndMsg.WM_KEYUP;
        m.HWnd = Handle;
        m.WParam = (IntPtr)0x41;
        m.LParam = (IntPtr)unchecked((int)0xC01e0001);
        WndProc(ref m);
    }

    public void KeyDownA()
    {
        var m = new Message
        {
            Msg = (int)WndMsg.WM_KEYDOWN,
            HWnd = Handle,
            WParam = (IntPtr)0x41,
            LParam = (IntPtr)0x1e0001
        };

        WndProc(ref m);

        m.Msg = (int)WndMsg.WM_CHAR;
        m.HWnd = Handle;
        m.WParam = (IntPtr)0x61;
        m.LParam = (IntPtr)0x1e0001;
        WndProc(ref m);
    }

    public void KeyUpA()
    {
        var m = new Message
        {
            Msg = (int)WndMsg.WM_KEYUP,
            HWnd = Handle,
            WParam = (IntPtr)0x41,
            LParam = (IntPtr)unchecked((int)0xC01e0001)
        };

        WndProc(ref m);
    }

    public ArrayList Results => results;
}

[TestFixture]
[Ignore("Comparisons too strict")]
public class LabelTestEventsOrder : TestHelper
{
    public string[] ArrayListToString(ArrayList arrlist)
    {
        string[] retval = new string[arrlist.Count];
        for (var i = 0; i < arrlist.Count; i++)
            retval[i] = (string?)arrlist[i]!;
        return retval;
    }

    //private void OrderedAssert(string[] wanted, ArrayList found) {
    //        int	last_target;
    //        bool	seen;
    //
    //        last_target = 0;
    //
    //        for (int i = 0; i < wanted.Length; i++) {
    //                seen = false;
    //                for (int j = last_target; j < found.Count; j++) {
    //                        if (wanted[i] == (string)found[j]) {
    //                                seen = true;
    //                                last_target = j + 1;
    //                                break;
    //                        }
    //                }
    //
    //                if (!seen) {
    //                        Console.WriteLine("Needed {0}", wanted[i]);
    //                }
    //        }
    //}

    public void PrintList(string name, ArrayList list)
    {
        Console.WriteLine("{0}", name);
        for (var i = 0; i < list.Count; i++)
        {
            Console.WriteLine("   {0}", list[i]);
        }
        Console.WriteLine(string.Empty);
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
        var l = new MyLabel();
        myform.Controls.Add(l);

        Assert.That((object?)ArrayListToString(l.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void SizeChangedEventsOrder()
    {
        string[] EventsWanted =
        [
            "OnHandleCreated",
            "OnBindingContextChanged",
            "OnBindingContextChanged",
            "OnSizeChanged",
            "OnResize",
            "OnInvalidated",
            "OnLayout"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var l = new MyLabel();
        myform.Controls.Add(l);
        l.Size = new Size(150, 20);

        Assert.That((object?)ArrayListToString(l.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void AutoSizeChangedEventsOrder()
    {
        string[] EventsWanted =
        [
            "OnHandleCreated",
            "OnBindingContextChanged",
            "OnBindingContextChanged",
            "OnSizeChanged",
            "OnResize",
            "OnInvalidated",
            "OnLayout",
            "OnAutoSizeChanged"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var l = new MyLabel();
        myform.Controls.Add(l);
        l.AutoSize = true;

        Assert.That((object?)ArrayListToString(l.Results), Is.EqualTo(EventsWanted));
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
        var l = new MyLabel();
        myform.Controls.Add(l);
        l.BackgroundImage = Image.FromFile(TestResourceHelper.GetFullPathOfResource("Test/System.Windows.Forms/bitmaps/a.png"));

        Assert.That((object?)ArrayListToString(l.Results), Is.EqualTo(EventsWanted));
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
        var l = new MyLabel();
        myform.Controls.Add(l);
        l.ImeMode = ImeMode.Katakana;

        Assert.That((object?)ArrayListToString(l.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void KeyPressEventsOrder()
    {
        string[] EventsWanted =
        [
            "OnHandleCreated",
            "OnBindingContextChanged",
            "OnBindingContextChanged",
            "OnKeyDown,A",
            "OnKeyPress,a",
            "OnKeyUp,A"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var l = new MyLabel();
        myform.Controls.Add(l);
        l.KeyPressA();

        Assert.That((object?)ArrayListToString(l.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void TabStopChangedEventsOrder()
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
        var l = new MyLabel();
        myform.Controls.Add(l);
        l.TabStop = true;
        PrintList("TabStopChanged", l.Results);
        Assert.That((object?)ArrayListToString(l.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void TextAlignChangedEventsOrder()
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
        var l = new MyLabel();
        myform.Controls.Add(l);
        l.TextAlign = ContentAlignment.TopRight;

        Assert.That((object?)ArrayListToString(l.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void InvalidateEventsOrder()
    {
        var rect = new Rectangle(new Point(0, 0), new Size(2, 2));

        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var l = new MyLabelInvalidate();
        myform.Controls.Add(l);
        l.TextAlign = ContentAlignment.TopRight;

        string[] EventsWanted =
        [
            "OnHandleCreated",
            "OnBindingContextChanged",
            "OnBindingContextChanged",
            "OnInvalidated,{X=0,Y=0,Width="+l.Size.Width+",Height="+l.Size.Height+"}",
            "OnInvalidated," + rect.ToString ()
        ];

        l.Invalidate(rect);

        Assert.That((object?)ArrayListToString(l.Results), Is.EqualTo(EventsWanted));
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
            "OnInvalidated",
            "OnInvalidated",
            "OnPaint"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var l = new MyLabel();
        myform.Controls.Add(l);
        l.TextAlign = ContentAlignment.TopRight;
        l.Refresh();
        Assert.That((object?)ArrayListToString(l.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

}

public class MyLabel2 : Label
{
    protected ArrayList results = new();
    public MyLabel2() : base()
    {
        AutoSizeChanged += AutoSizeChanged_Handler;
        HandleCreated += HandleCreated_Handler;
        BindingContextChanged += BindingContextChanged_Handler;
        BackgroundImageChanged += BackgroundImageChanged_Handler;
        ImeModeChanged += ImeModeChanged_Handler;
        KeyDown += KeyDown_Handler;
        KeyPress += KeyPress_Handler;
        KeyUp += KeyUp_Handler;
        Invalidated += Invalidated_Handler;
        Resize += Resize_Handler;
        SizeChanged += SizeChanged_Handler;
        Layout += Layout_Handler;
        VisibleChanged += VisibleChanged_Handler;
        Paint += Paint_Handler;
    }

    protected void AutoSizeChanged_Handler(object? sender, EventArgs e)
    {
        results.Add("AutoSizeChanged");
    }

    protected void BackgroundImageChanged_Handler(object? sender, EventArgs e)
    {
        results.Add("BackgroundImageChanged");
    }

    protected void ImeModeChanged_Handler(object? sender, EventArgs e)
    {
        results.Add("ImeModeChanged");
    }

    protected void KeyDown_Handler(object? sender, KeyEventArgs e)
    {
        results.Add("KeyDown," + (char)e.KeyValue);
    }

    protected void KeyPress_Handler(object? sender, KeyPressEventArgs e)
    {
        results.Add("KeyPress," + e.KeyChar.ToString());
    }

    protected void KeyUp_Handler(object? sender, KeyEventArgs e)
    {
        results.Add("KeyUp," + (char)e.KeyValue);
    }

    protected void HandleCreated_Handler(object? sender, EventArgs e)
    {
        results.Add("HandleCreated");
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

    public void KeyPressA()
    {
        var m = new Message
        {
            Msg = (int)WndMsg.WM_KEYDOWN,
            HWnd = Handle,
            WParam = (IntPtr)0x41,
            LParam = (IntPtr)0x1e0001
        };

        WndProc(ref m);

        m.Msg = (int)WndMsg.WM_CHAR;
        m.HWnd = Handle;
        m.WParam = (IntPtr)0x61;
        m.LParam = (IntPtr)0x1e0001;
        WndProc(ref m);

        m.Msg = (int)WndMsg.WM_KEYUP;
        m.HWnd = Handle;
        m.WParam = (IntPtr)0x41;
        m.LParam = (IntPtr)unchecked((int)0xC01e0001);
        WndProc(ref m);
    }

    public void KeyDownA()
    {
        var m = new Message
        {
            Msg = (int)WndMsg.WM_KEYDOWN,
            HWnd = Handle,
            WParam = (IntPtr)0x41,
            LParam = (IntPtr)0x1e0001
        };

        WndProc(ref m);

        m.Msg = (int)WndMsg.WM_CHAR;
        m.HWnd = Handle;
        m.WParam = (IntPtr)0x61;
        m.LParam = (IntPtr)0x1e0001;
        WndProc(ref m);
    }

    public void KeyUpA()
    {
        var m = new Message
        {
            Msg = (int)WndMsg.WM_KEYUP,
            HWnd = Handle,
            WParam = (IntPtr)0x41,
            LParam = (IntPtr)unchecked((int)0xC01e0001)
        };

        WndProc(ref m);
    }

    public ArrayList Results => results;
}

[TestFixture]
[Ignore("Comparisons too strict")]
public class LabelTestEventsOrder2 : TestHelper
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
        var l = new MyLabel2();
        myform.Controls.Add(l);

        Assert.That((object?)ArrayListToString(l.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void SizeChangedEventsOrder()
    {
        string[] EventsWanted =
        [
            "HandleCreated",
            "BindingContextChanged",
            "BindingContextChanged",
            "Invalidated",
            "Layout",
            "Resize",
            "SizeChanged"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var l = new MyLabel2();
        myform.Controls.Add(l);
        l.Size = new Size(150, 20);

        Assert.That((object?)ArrayListToString(l.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void AutoSizeChangedEventsOrder()
    {
        string[] EventsWanted =
        [
            "HandleCreated",
            "BindingContextChanged",
            "BindingContextChanged",
            "Invalidated",
            "Layout",
            "Resize",
            "SizeChanged",
            "AutoSizeChanged"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var l = new MyLabel2();
        myform.Controls.Add(l);
        l.AutoSize = true;

        Assert.That((object?)ArrayListToString(l.Results), Is.EqualTo(EventsWanted));
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
        var l = new MyLabel2();
        myform.Controls.Add(l);
        l.BackgroundImage = Image.FromFile(TestResourceHelper.GetFullPathOfResource("Test/System.Windows.Forms/bitmaps/a.png"));

        Assert.That((object?)ArrayListToString(l.Results), Is.EqualTo(EventsWanted));
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
        var l = new MyLabel2();
        myform.Controls.Add(l);
        l.ImeMode = ImeMode.Katakana;

        Assert.That((object?)ArrayListToString(l.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void KeyPressEventsOrder()
    {
        string[] EventsWanted =
        [
            "HandleCreated",
            "BindingContextChanged",
            "BindingContextChanged",
            "KeyDown,A",
            "KeyPress,a",
            "KeyUp,A"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var l = new MyLabel2();
        myform.Controls.Add(l);
        l.KeyPressA();

        Assert.That((object?)ArrayListToString(l.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void TabStopChangedEventsOrder()
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
        var l = new MyLabel2();
        myform.Controls.Add(l);
        l.TabStop = true;

        Assert.That((object?)ArrayListToString(l.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

    [Test]
    public void TextAlignChangedEventsOrder()
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
        var l = new MyLabel2();
        myform.Controls.Add(l);
        l.TextAlign = ContentAlignment.TopRight;

        Assert.That((object?)ArrayListToString(l.Results), Is.EqualTo(EventsWanted));
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
            "Invalidated",
            "Invalidated",
            "Paint"
        ];
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        var l = new MyLabel2();
        myform.Controls.Add(l);
        l.TextAlign = ContentAlignment.TopRight;
        l.Refresh();
        Assert.That((object?)ArrayListToString(l.Results), Is.EqualTo(EventsWanted));
        myform.Dispose();
    }

}