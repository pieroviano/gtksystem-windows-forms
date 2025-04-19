//
// FormTest.cs: Test cases for Form.
//
// Author:
//   Ritvik Mayank (mritvik@novell.com)
//
// (C) 2005 Novell, Inc. (http://www.novell.com)
//

using GtkTests.Helpers;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class FormHandleTest : TestHelper
{
    [Test]
    public void TestConstructors()
    {
        var c = new Form();
        Assert.IsFalse(c.IsHandleCreated);
    }

    [Test]
    public void TestPublicProperties()
    {
        // This long, carpal-tunnel syndrome inducing test shows us that
        // the following properties cause the Handle to be created:
        // - AccessibilityObject	[get]
        // - Capture			[set]
        // - Handle			[get]
        // - Visible			[set]

        var c = new Form();
        // A
        object? o = c.AccessibilityObject;
        Assert.IsTrue(c.IsHandleCreated);
        c.Dispose();

        c = new Form();
        o = c.AccessibleDefaultActionDescription;
        c.AccessibleDefaultActionDescription = "playdoh";
        Assert.IsFalse(c.IsHandleCreated);
        o = c.AccessibleDescription;
        c.AccessibleDescription = "more playdoh!";
        Assert.IsFalse(c.IsHandleCreated);
        o = c.AccessibleName;
        c.AccessibleName = "playdoh fun factory";
        Assert.IsFalse(c.IsHandleCreated);
        o = c.AccessibleRole;
        o = c.AllowDrop;
        c.AllowDrop = true;
        Assert.IsFalse(c.IsHandleCreated);
        // If we don't reset the control, handle creation will fail
        // because AllowDrop requires STAThread, which Nunit doesn't do
        c.Dispose();

        c = new Form();
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

        // A - Form			
        o = c.ActiveControl;
        c.ActiveControl = null;
        Assert.IsFalse(c.IsHandleCreated, "FA2");

        o = c.AutoScaleDimensions;
        c.AutoScaleDimensions = SizeF.Empty;
        Assert.IsFalse(c.IsHandleCreated, "FA5");

        o = c.AutoScaleMode;
        c.AutoScaleMode = AutoScaleMode.Dpi;
        Assert.IsFalse(c.IsHandleCreated, "FA6");
        o = c.AutoScroll;
        c.AutoScroll = !c.AutoScroll;
        Assert.IsFalse(c.IsHandleCreated, "FA7");

        o = c.AutoScrollMargin;
        c.AutoScrollMargin = new Size(c.AutoScrollMargin.Width + 1, c.AutoScrollMargin.Height + 1);
        Assert.IsFalse(c.IsHandleCreated, "FA8");

        o = c.AutoScrollMinSize;
        c.AutoScrollMinSize = new Size(c.AutoScrollMinSize.Width + 1, c.AutoScrollMinSize.Height + 1);
        Assert.IsFalse(c.IsHandleCreated, "FA9");

#if !MONO
        o = c.AutoScrollOffset;
        c.AutoScrollOffset = new Point(c.AutoScrollOffset.X + 1, c.AutoScrollOffset.Y + 1);
        Assert.IsFalse(c.IsHandleCreated, "FA10");
#endif

        o = c.AutoScrollPosition;
        c.AutoScrollPosition = new Point(c.AutoScrollPosition.X + 1, c.AutoScrollPosition.Y + 1);
        Assert.IsFalse(c.IsHandleCreated, "FA11");

        o = c.AutoSize;
        c.AutoSize = !c.AutoSize;
        Assert.IsFalse(c.IsHandleCreated, "FA12");
        // B
        o = c.BackColor;
        c.BackColor = Color.Green;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.BackgroundImage;
        c.BackgroundImage = new Bitmap(1, 1);
        Assert.IsFalse(c.IsHandleCreated);
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


        // B - Form
        o = c.BindingContext;
        c.BindingContext = null;
        Assert.IsFalse(c.IsHandleCreated, "FB1");

        // C
        o = c.CanFocus;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.CanSelect;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Capture;
        Assert.IsFalse(c.IsHandleCreated, "A17a");
        c.Capture = true;
        Assert.IsTrue(c.IsHandleCreated, "A17b");
        c.Dispose();

        c = new Form();
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

        o = c.ClientSize;
        c.ClientSize = new Size(c.ClientSize.Width + 1, c.ClientSize.Height + 1);
        Assert.IsFalse(c.IsHandleCreated, "FC2");

        o = c.Container;
        Assert.IsFalse(c.IsHandleCreated, "FC3");

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

        // D - Form
        o = c.DataBindings;
        Assert.IsFalse(c.IsHandleCreated, "FD6");

        o = c.DialogResult;
        c.DialogResult = DialogResult.Abort;
        Assert.IsFalse(c.IsHandleCreated, "FD9");

        o = c.DisplayRectangle;
        Assert.IsFalse(c.IsHandleCreated, "FD10");

        o = c.IsDisposing;
        Assert.IsFalse(c.IsHandleCreated, "FD11");

        o = c.Dock;
        c.Dock = DockStyle.Right;
        Assert.IsFalse(c.IsHandleCreated, "FD12");

        // E-H
        o = c.Enabled;
        c.Enabled = false;
        Assert.IsFalse(c.IsHandleCreated);
        c.Dispose();

        c = new Form();  //Reset just in case enable = false affects things
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
        c.Dispose();

        c = new Form();
        o = c.HasChildren;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Height;
        c.Height = 12;
        Assert.IsFalse(c.IsHandleCreated);

        // E-H - Form
        o = c.FormBorderStyle;
        c.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Assert.IsFalse(c.IsHandleCreated, "FF1");

        // I - L
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

        // I - L - Form

        o = c.Icon;
        c.Icon = null;
        Assert.IsFalse(c.IsHandleCreated, "FI1");

        o = c.Location;
        c.Location = c.Location + new Size(1, 1);
        Assert.IsFalse(c.IsHandleCreated, "FL1");


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

        // M - O - Form
        o = c.MainMenuStrip;
        c.MainMenuStrip = null;
        Assert.IsFalse(c.IsHandleCreated, "FM1");

        o = c.MaximizeBox;
        c.MaximizeBox = !c.MaximizeBox;
        Assert.IsFalse(c.IsHandleCreated, "FM2");

        o = c.MaximumSize;
        c.MaximumSize = c.MaximumSize + new Size(1, 1);
        Assert.IsFalse(c.IsHandleCreated, "FM3");

        o = c.MinimizeBox;
        c.MinimizeBox = !c.MinimizeBox;
        Assert.IsFalse(c.IsHandleCreated, "FM8");

        o = c.MinimumSize;
        c.MinimumSize = c.MinimumSize + new Size(1, 1);
        Assert.IsFalse(c.IsHandleCreated, "FM9");

        o = c.Opacity;
        c.Opacity = 0.9;
        Assert.IsFalse(c.IsHandleCreated, "FO1");

        // P - R
        o = c.Padding;
        c.Padding = new Padding(4);
        Assert.IsFalse(c.IsHandleCreated);
        o = c.Parent;
        c.Parent = new Form();
        Assert.IsFalse(c.IsHandleCreated);
        c.Close();

        c = new Form();
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
        Assert.IsFalse(c.IsHandleCreated);

        c.Visible = true;
        Assert.IsTrue(c.IsHandleCreated, "A69-b");
        c.Dispose();
        c = new Form();

        o = c.Width;
        c.Width = 190;
        Assert.IsFalse(c.IsHandleCreated);
        o = c.WindowTarget;
        Assert.IsFalse(c.IsHandleCreated);

        // S - W - Form

        o = c.ShowIcon;
        c.ShowIcon = !c.ShowIcon;
        Assert.IsFalse(c.IsHandleCreated, "FS1");

        o = c.ShowInTaskbar;
        c.ShowInTaskbar = !c.ShowInTaskbar;
        Assert.IsFalse(c.IsHandleCreated, "FS2");

        o = c.Size;
        c.Size = c.Size + new Size(1, 1);
        Assert.IsFalse(c.IsHandleCreated, "FS3");

        o = c.StartPosition;
        c.StartPosition = FormStartPosition.Manual;
        Assert.IsFalse(c.IsHandleCreated, "FS5");

        o = c.Text;
        c.Text = "hooray!";
        Assert.IsFalse(c.IsHandleCreated, "FT1");

        o = c.WindowState;
        c.WindowState = FormWindowState.Maximized;
        Assert.IsFalse(c.IsHandleCreated, "FW1");

        c.Dispose();
        RemoveWarning(o);
    }

    [Test]
    public void TestProtectedProperties()
    {
        // Not a surprise, but none of these cause handle creation.
        // Included just to absolutely certain.
        var c = new ProtectedPropertyForm();

        object o = c.PublicCreateParams;
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

        o = c.PublicDesignMode;
        Assert.IsFalse(c.IsHandleCreated);

        o = c.PublicEvents;
        Assert.IsFalse(c.IsHandleCreated);

        o = c.PublicHScroll;
        c.PublicHScroll = !c.PublicHScroll;
        Assert.IsFalse(c.IsHandleCreated);

        o = c.PublicVScroll;
        c.PublicVScroll = !c.PublicVScroll;
        Assert.IsFalse(c.IsHandleCreated);


        RemoveWarning(o);
    }

    private readonly Form invokeform = new();

    [OneTimeTearDown]
    protected override void TearDown()
    {
        invokeform.Dispose();
    }

    [Test]
    public void TestPublicMethods()
    {
        // Public Methods that force Handle creation:
        // - CreateGraphics ()
        // - GetChildAtPoint ()
        // - Invoke, BeginInvoke throws InvalidOperationException if Handle has not been created
        // - PointToClient ()
        // - PointToScreen ()
        // - RectangleToClient ()
        // - RectangleToScreen ()
        // - Select ()
        // - Show (IWin32Window)
        // Notes:
        // - CreateControl does NOT force Handle creation!

        var c = new Form();

        c.BringToFront();
        Assert.IsFalse(c.IsHandleCreated);

        c.Contains(new Form());
        Assert.IsFalse(c.IsHandleCreated);

        c.CreateControl();
        Assert.IsFalse(c.IsHandleCreated);

        c = new Form();
        var g = c.CreateGraphics();
        g.Dispose();
        Assert.IsTrue(c.IsHandleCreated);
        c.Dispose();
        c = new Form();

        c.Dispose();
        Assert.IsFalse(c.IsHandleCreated);
        c = new Form();

        // This is weird, it causes a form to appear that won't go away until you move the mouse over it, 
        // but it doesn't create a handle??
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
        c.Dispose();
        c = new Form();

        c.GetContainerControl();
        Assert.IsFalse(c.IsHandleCreated);
        c.Dispose();

        c = new Form();
        c.GetNextControl(new Control(), true);
        Assert.IsFalse(c.IsHandleCreated);
        c.GetPreferredSize(Size.Empty);
        Assert.IsFalse(c.IsHandleCreated);
        c.Hide();
        Assert.IsFalse(c.IsHandleCreated);

        c.Invalidate();
        Assert.IsFalse(c.IsHandleCreated);

        //c.Invoke (new InvokeDelegate (InvokeMethod));
        //Assert.IsFalse (c.IsHandleCreated);
        c.PerformLayout();
        Assert.IsFalse(c.IsHandleCreated);

        c.PointToClient(new Point(100, 100));
        Assert.IsTrue(c.IsHandleCreated);
        c.Dispose();
        c = new Form();

        c.PointToScreen(new Point(100, 100));
        Assert.IsTrue(c.IsHandleCreated);
        c.Dispose();

        c = new Form();

        //c.PreProcessControlMessage   ???
        //c.PreProcessMessage          ???
        c.RectangleToClient(new Rectangle(0, 0, 100, 100));
        Assert.IsTrue(c.IsHandleCreated);
        c.Dispose();
        c = new Form();
        c.RectangleToScreen(new Rectangle(0, 0, 100, 100));
        Assert.IsTrue(c.IsHandleCreated);
        c.Dispose();
        c = new Form();
        c.Refresh();
        Assert.IsFalse(c.IsHandleCreated);
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
        Assert.IsTrue(c.IsHandleCreated);
        c.Dispose();

        c = new Form();

        c.SelectNextControl(new Control(), true, true, true, true);
        Assert.IsFalse(c.IsHandleCreated);
        c.SetBounds(0, 0, 100, 100);
        Assert.IsFalse(c.IsHandleCreated);
        c.Update();
        Assert.IsFalse(c.IsHandleCreated);

        // Form

        c.Activate();
        Assert.IsFalse(c.IsHandleCreated);

        c.Close();
        Assert.IsFalse(c.IsHandleCreated);

        c.Hide();
        Assert.IsFalse(c.IsHandleCreated);

        c.PerformLayout();
        Assert.IsFalse(c.IsHandleCreated);

        c = new Form();
        c.Show(null);
        Assert.IsTrue(c.IsHandleCreated);
        c.Close();
        c = new Form();

        //c.ShowDialog ()

        c.ToString();
        Assert.IsFalse(c.IsHandleCreated);

        c.Close();
    }

    [Test]
    public void Show()
    {
        var c = new Form();
        Assert.IsFalse(c.IsHandleCreated);
        c.HandleCreated += HandleCreated_WriteStackTrace;
        c.Show();
        Assert.IsTrue(c.IsHandleCreated);
        c.Dispose();
    }

    private void HandleCreated_WriteStackTrace(object? sender, EventArgs e)
    {
        //Console.WriteLine (Environment.StackTrace);
    }

    public delegate void InvokeDelegate();
    public void InvokeMethod() { invokeform.Text = "methodinvoked"; }

    [Test]
    public void InvokeIOE()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            var c = new Form();
            c.Invoke(new InvokeDelegate(InvokeMethod));
        });
    }

    public class ProtectedPropertyForm : Form
    {
        public CreateParams PublicCreateParams => base.CreateParams;
        public Cursor PublicDefaultCursor => base.DefaultCursor!;
        public ImeMode PublicDefaultImeMode => base.DefaultImeMode;
        public Padding PublicDefaultMargin => base.DefaultMargin;
        public Size PublicDefaultMaximumSize => base.DefaultMaximumSize;
        public Size PublicDefaultMinimumSize => base.DefaultMinimumSize;
        public Padding PublicDefaultPadding => base.DefaultPadding;
        public Size PublicDefaultSize => base.DefaultSize;
        public bool PublicDesignMode => DesignMode;
        public bool PublicDoubleBuffered
        {
            get => base.DoubleBuffered;
            set => base.DoubleBuffered = value;
        }
        public EventHandlerList PublicEvents => Events;
        public int PublicFontHeight
        {
            get => FontHeight;
            set => FontHeight = value;
        }
        public bool PublicHScroll
        {
            get => HScroll;
            set => HScroll = value;
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
        public bool PublicVScroll
        {
            get => VScroll;
            set => VScroll = value;
        }
    }

}