using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using GtkTests.Helpers;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class ControlPropertyEventsTest : TestHelper
{
    [Test]
    public void PropertyAllowDrop ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.AllowDrop = true;
        Assert.That((object?)c.AllowDrop, Is.EqualTo(true));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));

        ew.Clear ();
        c.AllowDrop = true;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyAnchor ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.Anchor = AnchorStyles.Bottom;
        Assert.That((object?)c.Anchor, Is.EqualTo(AnchorStyles.Bottom));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));

        ew.Clear ();
        c.Anchor = AnchorStyles.Bottom;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyAutoSize ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.AutoSize = true;
        Assert.That((object?)c.AutoSize, Is.EqualTo(true));
        Assert.That((object?)ew.ToString (), Is.EqualTo("AutoSizeChanged"));

        ew.Clear ();
        c.AutoSize = true;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyBackColor ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.BackColor = Color.Aquamarine;
        Assert.That((object?)c.BackColor, Is.EqualTo(Color.Aquamarine));
        Assert.That((object?)ew.ToString (), Is.EqualTo("BackColorChanged"));

        ew.Clear ();
        c.BackColor = Color.Aquamarine;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyBackgroundImage ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);
        Image i = new Bitmap (5, 5);

        c.BackgroundImage = i;
        Assert.That((object?)c.BackgroundImage, Is.SameAs(i));
        Assert.That((object?)ew.ToString (), Is.EqualTo("BackgroundImageChanged"));

        ew.Clear ();
        c.BackgroundImage = i;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyBackgroundImageLayout ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.BackgroundImageLayout = ImageLayout.Zoom;
        Assert.That((object?)c.BackgroundImageLayout, Is.EqualTo(ImageLayout.Zoom));
        Assert.That((object?)ew.ToString (), Is.EqualTo("BackgroundImageLayoutChanged"));

        ew.Clear ();
        c.BackgroundImageLayout = ImageLayout.Zoom;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyBindingContext ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);
        var b = new BindingContext ();

        c.BindingContext = b;
        Assert.That((object?)c.BindingContext, Is.SameAs(b));
        Assert.That((object?)ew.ToString (), Is.EqualTo("BindingContextChanged"));
			
        c.BindingContext = b;
        Assert.That((object?)ew.ToString (), Is.EqualTo("BindingContextChanged"));
    }

    [Test]
    public void PropertyBounds ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.Bounds = new Rectangle (0, 0, 5, 5);
        object expected = new Rectangle (0, 0, 5, 5);
        Assert.That((object?)c.Bounds, Is.EqualTo(expected));
        Assert.That((object?)ew.ToString (), Is.EqualTo("Layout;Resize;SizeChanged;ClientSizeChanged"));

        ew.Clear ();
        c.Bounds = new Rectangle (0, 0, 5, 5);
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected1));
    }

    [Test]
    [Ignore ("Setting Capture to true does not hold, getter returns false.")]
    public void PropertyCapture ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.Capture = true;
        Assert.That((object?)c.Capture, Is.EqualTo(true));
        Assert.That((object?)ew.ToString (), Is.EqualTo("HandleCreated"));

        ew.Clear ();
        c.Capture = true;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyClientSize ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.ClientSize = new Size (5, 5);
        object expected = new Size (5, 5);
        Assert.That((object?)c.ClientSize, Is.EqualTo(expected));
        Assert.That((object?)ew.ToString (), Is.EqualTo("Layout;Resize;SizeChanged;ClientSizeChanged;ClientSizeChanged"));

        ew.Clear ();
        c.ClientSize = new Size (5, 5);
        Assert.That((object?)ew.ToString (), Is.EqualTo("ClientSizeChanged"));
    }

    [Test]
    public void PropertyContextMenuStrip ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);
        var cm = new ContextMenuStrip ();

        c.ContextMenuStrip = cm;
        Assert.That((object?)c.ContextMenuStrip, Is.EqualTo(cm));
        Assert.That((object?)ew.ToString (), Is.EqualTo("ContextMenuStripChanged"));

        ew.Clear ();
        c.ContextMenuStrip = cm;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyCursor ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.Cursor = Cursors.HSplit;
        Assert.That((object?)c.Cursor, Is.EqualTo(Cursors.HSplit));
        Assert.That((object?)ew.ToString (), Is.EqualTo("CursorChanged"));

        ew.Clear ();
        c.Cursor = Cursors.HSplit;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyDock ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.Dock = DockStyle.Fill;
        Assert.That((object?)c.Dock, Is.EqualTo(DockStyle.Fill));
        Assert.That((object?)ew.ToString (), Is.EqualTo("DockChanged"));

        ew.Clear ();
        c.Dock = DockStyle.Fill;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyEnabled ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.Enabled = false;
        Assert.That((object?)c.Enabled, Is.EqualTo(false));
        Assert.That((object?)ew.ToString (), Is.EqualTo("EnabledChanged"));

        ew.Clear ();
        c.Enabled = false;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyFont ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);
        var f = new Font ("Arial", 14);
			
        c.Font = f;
        Assert.That((object?)c.Font, Is.EqualTo(f));
        Assert.That((object?)ew.ToString (), Is.EqualTo("FontChanged;Layout"));

        ew.Clear ();
        c.Font = f;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyForeColor ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.ForeColor = Color.Peru;
        Assert.That((object?)c.ForeColor, Is.EqualTo(Color.Peru));
        Assert.That((object?)ew.ToString (), Is.EqualTo("ForeColorChanged"));

        ew.Clear ();
        c.ForeColor = Color.Peru;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyHeight ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.Height = 27;
        Assert.That((object?)c.Height, Is.EqualTo(27));
        Assert.That((object?)ew.ToString (), Is.EqualTo("Layout;Resize;SizeChanged;ClientSizeChanged"));

        ew.Clear ();
        c.Height = 27;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyImeMode ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.ImeMode = ImeMode.Hiragana;
        Assert.That((object?)c.ImeMode, Is.EqualTo(ImeMode.Hiragana));
        Assert.That((object?)ew.ToString (), Is.EqualTo("ImeModeChanged"));

        ew.Clear ();
        c.ImeMode = ImeMode.Hiragana;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyLeft ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.Left = 27;
        Assert.That((object?)c.Left, Is.EqualTo(27));
        Assert.That((object?)ew.ToString (), Is.EqualTo("Move;LocationChanged"));

        ew.Clear ();
        c.Left = 27;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyLocation ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.Location = new Point (5, 5);
        object expected = new Point (5, 5);
        Assert.That((object?)c.Location, Is.EqualTo(expected));
        Assert.That((object?)ew.ToString (), Is.EqualTo("Move;LocationChanged"));

        ew.Clear ();
        c.Location = new Point (5, 5);
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyMargin ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.Margin = new Padding (5);
        object expected = new Padding (5);
        Assert.That((object?)c.Margin, Is.EqualTo(expected));
        Assert.That((object?)ew.ToString (), Is.EqualTo("MarginChanged"));

        ew.Clear ();
        c.Margin = new Padding (5);
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyMaximumSize ()
    {
        var c = new Control ();
        c.Size = new Size(10, 10);

        // Chaning MaximumSize below Size forces a size change
        var ew = new EventWatcher (c);
        c.MaximumSize = new Size (5, 5);
        object expected = new Size (5, 5);
        Assert.That((object?)c.MaximumSize, Is.EqualTo(expected));
        Assert.That((object?)ew.ToString (), Is.EqualTo("Layout;Resize;SizeChanged;ClientSizeChanged"));

        // Changing MaximumSize when Size is already smaller or equal doesn't raise any events
        ew.Clear ();
        c.MaximumSize = new Size (5, 5);
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyMinimumSize ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.MinimumSize = new Size (5, 5);
        object expected = new Size (5, 5);
        Assert.That((object?)c.MinimumSize, Is.EqualTo(expected));
        Assert.That((object?)ew.ToString (), Is.EqualTo("Layout;Resize;SizeChanged;ClientSizeChanged"));

        ew.Clear ();
        c.MinimumSize = new Size (5, 5);
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyName ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.Name = "Bob";
        Assert.That((object?)c.Name, Is.EqualTo("Bob"));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));

        ew.Clear ();
        c.Name = "Bob";
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyPadding ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.Padding = new Padding (5);
        object expected = new Padding (5);
        Assert.That((object?)c.Padding, Is.EqualTo(expected));
        Assert.That((object?)ew.ToString (), Is.EqualTo("PaddingChanged;Layout"));

        ew.Clear ();
        c.Padding = new Padding (5);
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyRegion ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);
        var r = new Region ();
			
        c.Region = r;
        Assert.That((object?)c.Region, Is.SameAs(r));
        Assert.That((object?)ew.ToString (), Is.EqualTo("RegionChanged"));

        ew.Clear ();
        c.Region = r;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyRightToLeft ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.RightToLeft = RightToLeft.Yes;
        Assert.That((object?)c.RightToLeft, Is.EqualTo(RightToLeft.Yes));
        Assert.That((object?)ew.ToString (), Is.EqualTo("RightToLeftChanged;Layout"));

        ew.Clear ();
        c.RightToLeft = RightToLeft.Yes;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertySize ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.Size = new Size (5, 5);
        object expected = new Size (5, 5);
        Assert.That((object?)c.Size, Is.EqualTo(expected));
        Assert.That((object?)ew.ToString (), Is.EqualTo("Layout;Resize;SizeChanged;ClientSizeChanged"));

        ew.Clear ();
        c.Size = new Size (5, 5);
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyTabIndex ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.TabIndex = 4;
        Assert.That((object?)c.TabIndex, Is.EqualTo(4));
        Assert.That((object?)ew.ToString (), Is.EqualTo("TabIndexChanged"));

        ew.Clear ();
        c.TabIndex = 4;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyTabStop ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.TabStop = false;
        Assert.That((object?)c.TabStop, Is.EqualTo(false));
        Assert.That((object?)ew.ToString (), Is.EqualTo("TabStopChanged"));

        ew.Clear ();
        c.TabStop = false;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyTag ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);
        object o = "Hello";

        c.Tag = o;
        Assert.That(c.Tag, Is.SameAs(o));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));

        ew.Clear ();
        c.Tag = o;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyText ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.Text = "Enchilada";
        Assert.That((object?)c.Text, Is.EqualTo("Enchilada"));
        Assert.That((object?)ew.ToString (), Is.EqualTo("TextChanged"));

        ew.Clear ();
        c.Text = "Enchilada";
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyTop ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.Top = 27;
        Assert.That((object?)c.Top, Is.EqualTo(27));
        Assert.That((object?)ew.ToString (), Is.EqualTo("Move;LocationChanged"));

        ew.Clear ();
        c.Top = 27;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyVisible ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.Visible = false;
        Assert.That((object?)c.Visible, Is.EqualTo(false));
        Assert.That((object?)ew.ToString (), Is.EqualTo("VisibleChanged"));

        ew.Clear ();
        c.Visible = false;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyWidth ()
    {
        var c = new Control ();
        var ew = new EventWatcher (c);

        c.Width = 27;
        Assert.That((object?)c.Width, Is.EqualTo(27));
        Assert.That((object?)ew.ToString (), Is.EqualTo("Layout;Resize;SizeChanged;ClientSizeChanged"));

        ew.Clear ();
        c.Width = 27;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    private class EventWatcher
    {
        private string events = string.Empty;

        public EventWatcher (Control c)
        {
            c.AutoSizeChanged += delegate { events += ("AutoSizeChanged;"); };
            c.BackColorChanged += delegate { events += ("BackColorChanged;"); };
            c.BackgroundImageChanged += delegate { events += ("BackgroundImageChanged;"); };
            c.BackgroundImageLayoutChanged += delegate { events += ("BackgroundImageLayoutChanged;"); };
            c.BindingContextChanged += delegate { events += ("BindingContextChanged;"); };
            c.CausesValidationChanged += delegate { events += ("CausesValidationChanged;"); };
            c.ChangeUICues += delegate { events += ("ChangeUICues;"); };
            c.Click += delegate { events += ("Click;"); };
            c.ClientSizeChanged += delegate { events += ("ClientSizeChanged;"); };
            c.ContextMenuStripChanged += delegate { events += ("ContextMenuStripChanged;"); };
            c.ControlAdded += delegate { events += ("ControlAdded;"); };
            c.ControlRemoved += delegate { events += ("ControlRemoved;"); };
            c.CursorChanged += delegate { events += ("CursorChanged;"); };
            c.DockChanged += delegate { events += ("DockChanged;"); };
            c.DoubleClick += delegate { events += ("DoubleClick;"); };
            c.DragDrop += delegate { events += ("DragDrop;"); };
            c.DragEnter += delegate { events += ("DragEnter;"); };
            c.DragLeave += delegate { events += ("DragLeave;"); };
            c.DragOver += delegate { events += ("DragOver;"); };
            c.EnabledChanged += delegate { events += ("EnabledChanged;"); };
            c.Enter += delegate { events += ("Enter;"); };
            c.FontChanged += delegate { events += ("FontChanged;"); };
            c.ForeColorChanged += delegate { events += ("ForeColorChanged;"); };
            c.GiveFeedback += delegate { events += ("GiveFeedback;"); };
            c.GotFocus += delegate { events += ("GotFocus;"); };
            c.HandleCreated += delegate { events += ("HandleCreated;"); };
            c.HandleDestroyed += delegate { events += ("HandleDestroyed;"); };
            c.ImeModeChanged += delegate { events += ("ImeModeChanged;"); };
            c.Invalidated += delegate { events += ("Invalidated;"); };
            c.KeyDown += delegate { events += ("KeyDown;"); };
            c.KeyPress += delegate { events += ("KeyPress;"); };
            c.KeyUp += delegate { events += ("KeyUp;"); };
            c.Layout += delegate { events += ("Layout;"); };
            c.Leave += delegate { events += ("Leave;"); };
            c.LocationChanged += delegate { events += ("LocationChanged;"); };
            c.LostFocus += delegate { events += ("LostFocus;"); };
            c.MarginChanged += delegate { events += ("MarginChanged;"); };
            c.MouseCaptureChanged += delegate { events += ("MouseCaptureChanged;"); };
            c.MouseClick += delegate { events += ("MouseClick;"); };
            c.MouseDoubleClick += delegate { events += ("MouseDoubleClick;"); };
            c.MouseDown += delegate { events += ("MouseDown;"); };
            c.MouseEnter += delegate { events += ("MouseEnter;"); };
            c.MouseLeave += delegate { events += ("MouseLeave;"); };
            c.MouseMove += delegate { events += ("MouseMove;"); };
            c.MouseUp += delegate { events += ("MouseUp;"); };
            c.MouseWheel += delegate { events += ("MouseWheel;"); };
            c.Move += delegate { events += ("Move;"); };
            c.PaddingChanged += delegate { events += ("PaddingChanged;"); };
            c.Paint += delegate { events += ("Paint;"); };
            c.ParentChanged += delegate { events += ("ParentChanged;"); };
            c.PreviewKeyDown += delegate { events += ("PreviewKeyDown;"); };
            c.QueryAccessibilityHelp += delegate { events += ("QueryAccessibilityHelp;"); };
            c.QueryContinueDrag += delegate { events += ("QueryContinueDrag;"); };
            c.RegionChanged += delegate { events += ("RegionChanged;"); };
            c.Resize += delegate { events += ("Resize;"); };
            c.RightToLeftChanged += delegate { events += ("RightToLeftChanged;"); };
            c.SizeChanged += delegate { events += ("SizeChanged;"); };
            c.StyleChanged += delegate { events += ("StyleChanged;"); };
            c.SystemColorsChanged += delegate { events += ("SystemColorsChanged;"); };
            c.TabIndexChanged += delegate { events += ("TabIndexChanged;"); };
            c.TabStopChanged += delegate { events += ("TabStopChanged;"); };
            c.TextChanged += delegate { events += ("TextChanged;"); };
            c.Validated += delegate { events += ("Validated;"); };
            c.Validating += delegate { events += ("Validating;"); };
            c.VisibleChanged += delegate { events += ("VisibleChanged;"); };
        }

        public override string ToString ()
        {
            return events.TrimEnd (';');
        }

        public void Clear ()
        {
            events = string.Empty;
        }
    }

    private class ExposeProtectedProperties : Control
    {
        //public new bool CanRaiseEvents { get { return base.CanRaiseEvents; } }
        public new Cursor DefaultCursor => base.DefaultCursor!;
        public new Size DefaultMaximumSize => base.DefaultMaximumSize;
        public new Size DefaultMinimumSize => base.DefaultMinimumSize;
        public new Padding DefaultPadding => base.DefaultPadding;
    }
}