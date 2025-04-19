// This test is designed to find exactly what conditions cause the control's
// Handle to be created.

using System.Drawing;
using System.Windows.Forms;
using GtkTests.Helpers;

namespace GtkTests.Controls;

[TestFixture]
public class ControlHandleTest : TestHelper
{
    [Test]
    public void TestPublicProperties()
    {
        // This long, carpal-tunnel syndrome inducing test shows us that
        // the following properties cause the Handle to be created:
        // - AccessibilityObject	[get]
        // - Capture			[set]
        // - Handle			[get]
        var c = new Control();
        // A
        object? o = c.AccessibilityObject;
        Assert.IsTrue(c.IsHandleCreated);
        c = new Control();

        o = c.AccessibleDefaultActionDescription;
        c.AccessibleDefaultActionDescription = "playdoh";
        Assert.IsFalse(c.IsHandleCreated);
        o = c.AccessibleDescription;
        c.AccessibleDescription = "more playdoh!";
        Assert.IsFalse(c.IsHandleCreated);
        o = c.AccessibleName;
        c.AccessibleName = "playdoh fun factory";
        Assert.IsFalse(c.IsHandleCreated);
        o = c.AllowDrop;
        c.AllowDrop = true;
        Assert.IsFalse(c.IsHandleCreated);
        // If we don't reset the control, handle creation will fail
        // because AllowDrop requires STAThread, which Nunit doesn't do
        c = new Control();
        o = c.Anchor;
        c.Anchor = AnchorStyles.Right;
        Assert.IsFalse(c.IsHandleCreated);
#if !MONO
        o = c.AutoScrollOffset;
        c.AutoScrollOffset = new Point(40, 40);
        Assert.IsFalse(c.IsHandleCreated);
#endif
        o = c.AutoSize;
        c.AutoSize = true;
        Assert.IsFalse(c.IsHandleCreated);

        // B
        o = c.BackColor;
        c.BackColor = Color.Green;
        Assert.IsTrue(c.IsHandleCreated);
        c = new Control();
        o = c.BackgroundImage;
        c.BackgroundImage = new Bitmap(1, 1);
        Assert.IsTrue(c.IsHandleCreated);
        c = new Control();
        o = c.BackgroundImageLayout;
        c.BackgroundImageLayout = ImageLayout.Stretch;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.BindingContext;
        c.BindingContext = new BindingContext();
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Bottom;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Bounds;
        c.Bounds = new Rectangle(0, 0, 12, 12);
        Assert.IsFalse(c.IsHandleCreated);

        // C
        o = c.CanFocus;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.CanSelect;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Capture;
        Assert.IsFalse(c.IsHandleCreated, "A17a");
        c.Capture = true;
        Assert.IsTrue(c.IsHandleCreated, "A17b");
        c = new Control();
        o = c.CausesValidation;
        c.CausesValidation = false;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.ClientRectangle;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.ClientSize;
        c.ClientSize = new Size(30, 30);
        Assert.IsFalse(c.IsHandleCreated);
        o = c.CompanyName;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Container;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.ContainsFocus;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.ContextMenuStrip;
        c.ContextMenuStrip = new ContextMenuStrip();
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Controls;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Created;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Cursor;
        c.Cursor = Cursors.Arrow;
        Assert.IsFalse(c.IsHandleCreated);

        // D
        o = c.DataBindings;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.DisplayRectangle;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.IsDisposing;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Dock;
        c.Dock = DockStyle.Fill;
        Assert.IsFalse(c.IsHandleCreated);

        // E-H
        o = c.Enabled;
        c.Enabled = false;
        Assert.IsFalse(c.IsHandleCreated);
        c = new Control();  //Reset just in case enable = false affects things
        o = c.Focused;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Font;
        c.Font = new Font(c.Font, FontStyle.Bold);
        Assert.IsFalse(c.IsHandleCreated);
        o = c.ForeColor;
        c.ForeColor = Color.Green;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Handle;
        Assert.IsTrue(c.IsHandleCreated);
        c = new Control();
        o = c.HasChildren;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Height;
        c.Height = 12;
        Assert.IsFalse(c.IsHandleCreated);

        // I - L
        o = c.ImeMode;
        c.ImeMode = ImeMode.On;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.InvokeRequired;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.IsAccessible;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.IsDisposed;
        Assert.IsFalse(c.IsHandleCreated);
#if !MONO
        o = c.IsMirrored;
        Assert.IsFalse(c.IsHandleCreated);
#endif
        o = c.LayoutEngine;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Left;
        c.Left = 15;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Location;
        c.Location = new Point(20, 20);
        Assert.IsFalse(c.IsHandleCreated);

        // M - N
        o = c.Margin;
        c.Margin = new Padding(6);
        Assert.IsFalse(c.IsHandleCreated);
        o = c.MaximumSize;
        c.MaximumSize = new Size(500, 500);
        Assert.IsFalse(c.IsHandleCreated);
        o = c.MinimumSize;
        c.MinimumSize = new Size(100, 100);
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Name;
        c.Name = "web";
        Assert.IsFalse(c.IsHandleCreated);

        // P - R
        o = c.Padding;
        c.Padding = new Padding(4);
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Parent;
        c.Parent = new Control();
        Assert.IsFalse(c.IsHandleCreated);
        o = c.PreferredSize;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.ProductName;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.ProductVersion;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.RecreatingHandle;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Region;
        c.Region = new Region(new Rectangle(0, 0, 177, 177));
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Right;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.RightToLeft;
        c.RightToLeft = RightToLeft.Yes;
        Assert.IsFalse(c.IsHandleCreated);

        // S - W
        o = c.Site;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Size;
        c.Size = new Size(188, 188);
        Assert.IsFalse(c.IsHandleCreated);
        o = c.TabIndex;
        c.TabIndex = 5;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Tag;
        c.Tag = "moooooooo";
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Text;
        c.Text = "meoooowww";
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Top;
        c.Top = 16;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.TopLevelControl;
        Assert.IsFalse(c.IsHandleCreated);
#if !MONO
        o = c.UseWaitCursor;
        c.UseWaitCursor = true;
        Assert.IsFalse(c.IsHandleCreated);
#endif
        o = c.Visible;
        c.Visible = true;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Width;
        c.Width = 190;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.WindowTarget!;
        Assert.IsFalse(c.IsHandleCreated);

        RemoveWarning(o!);
    }

    [Test]
    public void TestProtectedProperties()
    {
        // Not a surprise, but none of these cause handle creation.
        // Included just to absolutely certain.
        var c = new ProtectedPropertyControl();

#if !MONO
        object o = c.PublicCanRaiseEvents;
        Assert.IsFalse(c.IsHandleCreated);
#endif
        o = c.PublicCreateParams;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.PublicDefaultCursor;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.PublicDefaultImeMode;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.PublicDefaultMargin;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.PublicDefaultMaximumSize;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.PublicDefaultMinimumSize;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.PublicDefaultPadding;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.PublicDefaultSize;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.PublicDoubleBuffered;
        c.PublicDoubleBuffered = !c.PublicDoubleBuffered;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.PublicFontHeight;
        c.PublicFontHeight = c.PublicFontHeight + 1;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.PublicResizeRedraw;
        c.PublicResizeRedraw = !c.PublicResizeRedraw;
        Assert.IsFalse(c.IsHandleCreated);
#if !MONO
        o = c.PublicScaleChildren;
        Assert.IsFalse(c.IsHandleCreated);
#endif
        o = c.PublicShowFocusCues;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.PublicShowKeyboardCues;
        Assert.IsFalse(c.IsHandleCreated);

        RemoveWarning(o);
    }

    private readonly Control invokecontrol = new();

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        invokecontrol.Dispose();
    }

    [Test]
    public void TestPublicMethods()
    {
        // Public Methods that force Handle creation:
        // - CreateControl ()
        // - CreateGraphics ()
        // - GetChildAtPoint ()
        // - Invoke, BeginInvoke throws InvalidOperationException if Handle has not been created
        // - PointToClient ()
        // - PointToScreen ()
        // - RectangleToClient ()
        // - RectangleToScreen ()
        var c = new Control();

        c.BringToFront();
        Assert.IsFalse(c.IsHandleCreated);
        c.Contains(new Control());
        Assert.IsFalse(c.IsHandleCreated);
        c.CreateControl();
        Assert.IsTrue(c.IsHandleCreated);
        c = new Control();
        var g = c.CreateGraphics();
        g.Dispose();
        Assert.IsTrue(c.IsHandleCreated);
        c = new Control();
        c.Dispose();
        Assert.IsFalse(c.IsHandleCreated);
        c = new Control();
        //DragDropEffects d = c.DoDragDrop ("yo", DragDropEffects.None);
        //Assert.IsFalse (c.IsHandleCreated);
        //Assert1.AreEqual(DragDropEffects.None, d, "A6b");
        //Bitmap b = new Bitmap (100, 100);
        //c.DrawToBitmap (b, new Rectangle (0, 0, 100, 100));
        //Assert.IsFalse (c.IsHandleCreated);
        //b.Dispose ();
        c.FindForm();
        Assert.IsFalse(c.IsHandleCreated);
        c.Focus();
        Assert.IsFalse(c.IsHandleCreated);

        c.GetChildAtPoint(new Point(10, 10));
        Assert.IsTrue(c.IsHandleCreated);
        c.GetContainerControl();
        c = new Control();
        Assert.IsFalse(c.IsHandleCreated);
        c.GetNextControl(new Control(), true);
        Assert.IsFalse(c.IsHandleCreated);
        c.GetPreferredSize(Size.Empty);
        Assert.IsFalse(c.IsHandleCreated);
        c.Hide();
        Assert.IsFalse(c.IsHandleCreated);
        c = new Control();
        c.Invalidate();
        Assert.IsTrue(c.IsHandleCreated);
        c = new Control();
        c.PerformLayout();
        Assert.IsFalse(c.IsHandleCreated);
        c.PointToClient(new Point(100, 100));
        Assert.IsTrue(c.IsHandleCreated);
        c = new Control();
        c.PointToScreen(new Point(100, 100));
        Assert.IsTrue(c.IsHandleCreated);
        c = new Control();
        //c.PreProcessControlMessage   ???
        //c.PreProcessMessage          ???
        c.RectangleToClient(new Rectangle(0, 0, 100, 100));
        Assert.IsTrue(c.IsHandleCreated);
        c = new Control();
        c.RectangleToScreen(new Rectangle(0, 0, 100, 100));
        Assert.IsTrue(c.IsHandleCreated);
        c = new Control();
        c.Refresh();
        Assert.IsTrue(c.IsHandleCreated);
        c = new Control();
        c.ResetBackColor();
        Assert.IsFalse(c.IsHandleCreated);
        c.ResetBindings();
        Assert.IsFalse(c.IsHandleCreated);
        c.ResetCursor();
        Assert.IsFalse(c.IsHandleCreated);
        c.ResetFont();
        Assert.IsFalse(c.IsHandleCreated);
        c.ResetForeColor();
        Assert.IsFalse(c.IsHandleCreated);
        c.ResetImeMode();
        Assert.IsFalse(c.IsHandleCreated);
        c.ResetRightToLeft();
        Assert.IsFalse(c.IsHandleCreated);
        c.ResetText();
        Assert.IsFalse(c.IsHandleCreated);
        c.SuspendLayout();
        Assert.IsFalse(c.IsHandleCreated);
        c.ResumeLayout();
        Assert.IsFalse(c.IsHandleCreated);
        c.Scale(new SizeF(1.5f, 1.5f));
        Assert.IsFalse(c.IsHandleCreated);
        c.Select();
        Assert.IsFalse(c.IsHandleCreated);
        c.SelectNextControl(new Control(), true, true, true, true);
        Assert.IsFalse(c.IsHandleCreated);
        c.SetBounds(0, 0, 100, 100);
        Assert.IsFalse(c.IsHandleCreated);
        c.Update();
        Assert.IsFalse(c.IsHandleCreated);
    }

    [Test]
    public void Show()
    {
        var c = new Control();
        Assert.IsFalse(c.IsHandleCreated);
        c.HandleCreated += HandleCreated_WriteStackTrace;
        c.Show();
        Assert.IsFalse(c.IsHandleCreated);
    }

    private void HandleCreated_WriteStackTrace(object? sender, EventArgs e)
    {
        Console.WriteLine(Environment.StackTrace);
    }

    public delegate void InvokeDelegate();
    public void InvokeMethod() { invokecontrol.Text = "methodinvoked"; }

    [Test]
    public void InvokeIOE()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            var c = new Control();
            c.Invoke(new InvokeDelegate(InvokeMethod));
        });
    }

    private class ProtectedPropertyControl : Control
    {
#if !MONO
        public bool PublicCanRaiseEvents => base.CanRaiseEvents;
#endif
        public CreateParams PublicCreateParams => base.CreateParams;
        public Cursor PublicDefaultCursor => base.DefaultCursor!;
        public ImeMode PublicDefaultImeMode => base.DefaultImeMode;
        public Padding PublicDefaultMargin => base.DefaultMargin;
        public Size PublicDefaultMaximumSize => base.DefaultMaximumSize;
        public Size PublicDefaultMinimumSize => base.DefaultMinimumSize;
        public Padding PublicDefaultPadding => base.DefaultPadding;
        public Size PublicDefaultSize => base.DefaultSize;
        public bool PublicDoubleBuffered
        {
            get => base.DoubleBuffered;
            set => base.DoubleBuffered = value;
        }
        public int PublicFontHeight
        {
            get => FontHeight;
            set => FontHeight = value;
        }
        public bool PublicResizeRedraw
        {
            get => ResizeRedraw;
            set => ResizeRedraw = value;
        }
#if !MONO
        public bool PublicScaleChildren => base.ScaleChildren;
#endif
        public bool PublicShowFocusCues => base.ShowFocusCues;
        public bool PublicShowKeyboardCues => base.ShowKeyboardCues;
    }

}