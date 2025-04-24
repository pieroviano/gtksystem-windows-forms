/*
 * A cross-platform interface component developed based on GTK components and compatible with the native C# control windows form interface.
 * Use this component GTKSystem.Windows.Forms instead of Microsoft.WindowsDesktop.App.WindowsForms, compile once, run across platforms windows, linux, macos
 * Technical support 438865652@qq.com, https://www.gtkapp.com, https://gitee.com/easywebfactory, https://github.com/easywebfactory
 * author: chenhongjin
 */

using Gtk;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms.Design;
using Cairo;
using Context = Cairo.Context;
using Font = System.Drawing.Font;
using FontFamily = System.Drawing.FontFamily;
using Graphics = System.Drawing.Graphics;
using Image = System.Drawing.Image;
using Menu = Gtk.Menu;
using Region = System.Drawing.Region;
using Size = System.Drawing.Size;
using Task = System.Threading.Tasks.Task;
using Window = Gtk.Window;
#if NET462_OR_GREATER
using GtkSystemColors = System.Drawing.SystemColors;
#endif

namespace System.Windows.Forms;

using Color = Drawing.Color;
using Size = Size;
using SizeF = SizeF;
using Rectangle = Drawing.Rectangle;
using Point = Drawing.Point;

[DefaultEvent("Click")]
[DefaultProperty("Text")]
[Designer(typeof(ControlDesigner))]
[ToolboxItemFilter("System.Windows.Forms")]
public partial class Control : Component, IControl, ISynchronizeInvoke, ISupportInitialize, IArrangedElement, IBindableComponent
{
    public Gtk.Application Application { get; }

    public string? UniqueKey { get; protected set; }

    public virtual IWidget Widget => (IWidget)GtkControl!;

    public virtual IControlGtk Self => (IControlGtk)GtkControl!;

    public virtual object? GtkControl
    {
        get => gtkControl ??= new ControlWidget();
        set => gtkControl = value;
    }

    public static event EventHandler? BeforeInit;

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
    private ArrangedElementCollection? arrangedElementCollection;
#pragma warning restore CS0649 // Field is never assigned to, and will always have its default value
    private Cursor? cursor;
    private ImeMode imeMode;
    private ContextMenuStrip? _contextMenuStrip;
    private Padding margin;
    private AnchorStyles _anchor;
    private bool _autoSize;
    private BorderStyle _borderStyle;
    private bool _capture;
    private bool causesValidation;
    private AccessibleObject? accessibilityObject;
    private BindingContext? bindingContext;
    private int sizeWidth;
    private int sizeHeight;
    private int locationX;
    private int locationY;
    private bool widgetRealized;
    private ImageSurface? image;
    private Surface? surface;
    private Context? context;

    public Control()
    {
        if (!Form.ThreadInitialized)
        {
            Form.ThreadInitialized = true;
        }

        ForeColor = GtkSystemColors.WindowText;
        Application = Forms.Application.Init();

        Init();
    }

    private ControlStyles controlStyle;

    protected internal bool GetStyle(ControlStyles flag)
    {
        return (controlStyle & flag) == flag;
    }

    private void Init()
    {
        Controls = new ControlCollection(this);
        OnBeforeInit(EventArgs.Empty);
        Disposed += Control_Disposed;
        DataBindings = new ControlBindingsCollection(this);
        Enabled = true;
        UniqueKey = Guid.NewGuid().ToString().ToLower();
        if (Widget != null)
        {
            if (Widget is Window)
            {
                Widget.Halign = Align.Fill;
                Widget.Valign = Align.Fill;
            }
            else
            {
                Widget.Halign = Align.Start;
                Widget.Valign = Align.Start;
                Widget.Expand = false;
            }
            Widget.Data["Control"] = this;
            Widget.StyleContext.AddClass("DefaultThemeStyle");
            Widget.ButtonPressEvent += Widget_ButtonPressEvent;
            Widget.ButtonReleaseEvent += Widget_ButtonReleaseEvent;
            Widget.EnterNotifyEvent += Widget_EnterNotifyEvent;
            Widget.MotionNotifyEvent += Widget_MotionNotifyEvent;
            Widget.LeaveNotifyEvent += Widget_LeaveNotifyEvent;
            Widget.ScrollEvent += Widget_ScrollEvent;
            Widget.FocusInEvent += Widget_FocusInEvent;
            Widget.FocusOutEvent += Widget_FocusOutEvent;
            Widget.KeyPressEvent += Widget_KeyPressEvent;
            Widget.KeyReleaseEvent += Widget_KeyReleaseEvent;
            Widget.Realized += Widget_Realized;
            Widget.ConfigureEvent += Widget_ConfigureEvent;
            Self.Override.PaintGraphics += Override_PaintGraphics;
            Widget.SizeAllocated += Widget_SizeAllocated;

            SetStyle(ControlStyles.ContainerControl, false);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.FixedWidth, false);
            SetStyle(ControlStyles.FixedHeight, false);
            SetStyle(ControlStyles.StandardClick, false);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.UserMouse, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.StandardDoubleClick, false);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.CacheText, true);
            SetStyle(ControlStyles.EnableNotifyMessage, false);
            SetStyle(ControlStyles.DoubleBuffer, false);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UseTextForAccessibility, true);
        }

        if (Self != null)
        {
            Self.Shown += (_, _) =>
            {
                FakeHandle = (IntPtr)int.MaxValue;
            };
        }
        Visible = true;
    }

    private void Control_Disposed(object? sender, EventArgs e)
    {
        if (!IsDisposed)
        {
            OnDisposed(e);
        }
    }

    private void Widget_SizeAllocated(object? o, SizeAllocatedArgs args)
    {
        if (args.Allocation.Width != sizeWidth || args.Allocation.Height != sizeHeight)
        {
            sizeWidth = args.Allocation.Width;
            sizeHeight = args.Allocation.Height;
            OnSizeChanged(EventArgs.Empty);
        }
        if (args.Allocation.X != locationX || args.Allocation.Y != locationY)
        {
            locationX = args.Allocation.X;
            locationY = args.Allocation.Y;
            OnLocationChanged(EventArgs.Empty);
        }
    }

    private void Widget_ConfigureEvent(object? o, ConfigureEventArgs args)
    {
        OnMove(args);
    }

    private void Widget_Realized(object? sender, EventArgs e)
    {
        if (widgetRealized == false)
        {
            widgetRealized = true;
            InitStyle((Widget)sender!);
            OnLoad(e);
            if (Controls != null)
            {
                foreach (var item in Controls)
                {
                    if (item is Control control)
                    {
                        control.OnLoad(e);
                    }
                }
            }
        }
    }

    private void Widget_ButtonPressEvent(object o, ButtonPressEventArgs args)
    {
        if (o is Widget { Window: not null } owidget)
        {
            var result = MouseButtons.None;
            if (args.Event.Button == 1)
                result = MouseButtons.Left;
            else if (args.Event.Button == 2)
                result = MouseButtons.Middle;
            else if (args.Event.Button == 3)
                result = MouseButtons.Right;

            owidget.Window.GetOrigin(out var x, out var y);// Avoiding event penetration errors
            if (MouseDown != null)
            {
                MouseDown(this, new MouseEventArgs(result, 1, (int)args.Event.XRoot - x, (int)args.Event.YRoot - y, 0));
            }
        }
    }
    private void Widget_ButtonReleaseEvent(object o, ButtonReleaseEventArgs args)
    {
        if (o is Widget { Window: not null } owidget)
        {
            var result = MouseButtons.None;
            if (args.Event.Button == 1)
                result = MouseButtons.Left;
            else if (args.Event.Button == 2)
                result = MouseButtons.Middle;
            else if (args.Event.Button == 3)
                result = MouseButtons.Right;
            owidget.Window.GetOrigin(out var x, out var y);
            if (MouseUp != null)
            {
                MouseUp(this, new MouseEventArgs(result, 1, (int)args.Event.XRoot - x, (int)args.Event.YRoot - y, 0));
            }
            if (args.Event.Type == Gdk.EventType.TwoButtonPress || args.Event.Type == Gdk.EventType.DoubleButtonPress)
            {
                if (MouseDoubleClick != null)
                    MouseDoubleClick(this, new MouseEventArgs(result, 2, (int)args.Event.XRoot - x, (int)args.Event.YRoot - y, 0));
                if (DoubleClick != null)
                    DoubleClick(this, EventArgs.Empty);
            }
            else
            {
                if (Click != null)
                    Click(this, EventArgs.Empty);
                if (MouseClick != null)
                    MouseClick(this, new MouseEventArgs(result, 1, (int)args.Event.XRoot - x, (int)args.Event.YRoot - y, 0));
            }
            if (ContextMenuStrip != null)
            {
                if (args.Event.Button == 3)
                {
                    ContextMenuStrip.Widget.ShowAll();
                    ((Menu)ContextMenuStrip.Widget).PopupAtPointer(args.Event);
                }
            }
        }
    }

    private void Widget_EnterNotifyEvent(object? o, EnterNotifyEventArgs args)
    {
        if (Cursor != null)
        {
            if (Cursor.CursorType == Gdk.CursorType.CursorIsPixmap)
            {
                if (o != null)
                {
                    Widget.Window.Cursor = new Gdk.Cursor(((Widget)o).Display, Cursor.CursorPixbuf, Cursor.CursorsXy.X,
                        Cursor.CursorsXy.Y);
                }
            }
            else
            {
                if (o != null)
                {
                    Widget.Window.Cursor = new Gdk.Cursor(((Widget)o).Display, Cursor.CursorType);
                }
            }
        }

        OnEnter(args);
        OnMouseEnter(args);

        OnMouseHover(args);
    }

    private void Widget_MotionNotifyEvent(object? o, MotionNotifyEventArgs args)
    {
        var eventArgs = new MouseEventArgs(MouseButtons.None, 1, (int)args.Event.X, (int)args.Event.Y, 0);
        OnMouseMove(eventArgs);
    }

    private void Widget_LeaveNotifyEvent(object? o, LeaveNotifyEventArgs args)
    {
        if (Cursor != null)
        {
            Widget.Window.Cursor = null;
        }

        OnLeave(args);
        OnMouseLeave(args);
    }

    private void Widget_ScrollEvent(object? o, Gtk.ScrollEventArgs args)
    {
        var eventArgs = new MouseEventArgs(MouseButtons.None, 0, (int)args.Event.X, (int)args.Event.Y, (int)args.Event.DeltaY);
        OnMouseWheel(eventArgs);
    }

    private void Widget_FocusOutEvent(object? o, FocusOutEventArgs args)
    {
        OnLostFocus(args);

        var cancelEventArgs = new CancelEventArgs(false);
        OnValidating(cancelEventArgs);
        if (Validated != null && cancelEventArgs.Cancel == false)
            OnValidated(cancelEventArgs);
    }

    private void Widget_KeyPressEvent(object? o, Gtk.KeyPressEventArgs args)
    {
        if (KeyDown != null)
        {
            if (args.Event is { } eventkey)
            {
                var keys = (Keys)eventkey.HardwareKeycode;
                if (eventkey.State.HasFlag(Gdk.ModifierType.Mod1Mask))
                    keys |= Keys.Alt;
                if (eventkey.State.HasFlag(Gdk.ModifierType.ControlMask))
                    keys |= Keys.Control;
                if (eventkey.State.HasFlag(Gdk.ModifierType.ShiftMask))
                    keys |= Keys.Shift;
                if (eventkey.State.HasFlag(Gdk.ModifierType.LockMask))
                    keys |= Keys.CapsLock;

                OnKeyDown(new KeyEventArgs(keys));
            }
        }
    }

    private void Widget_KeyReleaseEvent(object? o, KeyReleaseEventArgs args)
    {
        if (KeyUp != null)
        {
            var keys = (Keys)args.Event.HardwareKeycode;
            OnKeyUp(new KeyEventArgs(keys));
        }
        if (KeyPress != null)
        {
            var keys = (Keys)args.Event.HardwareKeycode;
            var eventArgs = new KeyPressEventArgs(Convert.ToChar(keys));
            OnKeyPress(eventArgs);
        }
    }

    protected virtual void InitStyle(Widget widget)
    {
        SetStyle(widget);
    }

    protected virtual void UpdateStyle()
    {
        if (Widget is { IsMapped: true })
        {
            if (Widget is Widget widget) SetStyle(widget);
        }
    }

    protected virtual void UpdateBackgroundStyle()
    {
        if (Widget is { IsMapped: true })
            Self.Override.OnAddClass();
    }

    protected virtual void SetStyle(Widget widget)
    {
        var style = new StringBuilder();
        if (widget is Gtk.Image) { }
        else
        {
            if (Image is { PixbufData: not null })
            {
                var imgdir = $"Resources/{widget.WidgetPath.IterGetName(0)}";
                var imguri = $"{imgdir}/{widget.Name}_image.png";
                if (!File.Exists(imguri))
                {
                    if (!Directory.Exists(imgdir))
                        Directory.CreateDirectory(imgdir);
                    var imagepixbuf = new Gdk.Pixbuf(Image.PixbufData);
                    imagepixbuf.Save(imguri, "png");
                }
                style.AppendFormat("background:url(\"{0}\")", imguri);
                style.Append(" no-repeat");
                if (ImageAlign == ContentAlignment.TopLeft)
                {
                    style.Append(" top left");
                }
                else if (ImageAlign == ContentAlignment.TopCenter)
                {
                    style.Append(" top center");
                }
                else if (ImageAlign == ContentAlignment.TopRight)
                {
                    style.Append(" top right");
                }
                else if (ImageAlign == ContentAlignment.MiddleLeft)
                {
                    style.Append(" center left");
                }
                else if (ImageAlign == ContentAlignment.MiddleCenter)
                {
                    style.Append(" center center");
                }
                else if (ImageAlign == ContentAlignment.MiddleRight)
                {
                    style.Append(" center right");
                }
                else if (ImageAlign == ContentAlignment.BottomLeft)
                {
                    style.Append(" bottom left");
                }
                else if (ImageAlign == ContentAlignment.BottomCenter)
                {
                    style.Append(" bottom center");
                }
                else if (ImageAlign == ContentAlignment.BottomRight)
                {
                    style.Append(" bottom right");
                }
                else
                {
                    style.Append(" center center");
                }

                if (BackgroundImage is { PixbufData: not null })
                {
                    var bgimguri = $"{imgdir}/{widget.Name}_bgimage.png";
                    if (!File.Exists(bgimguri))
                    {
                        var bgpixbuf = new Gdk.Pixbuf(BackgroundImage.PixbufData);
                        bgpixbuf.Save(bgimguri, "png");
                    }

                    style.AppendFormat(",url(\"{0}\") repeat", bgimguri);
                }
                style.Append(";");
                style.Append("background-origin: padding-box;");
                style.Append("background-clip: padding-box;");
            }
            else if (BackgroundImage is { PixbufData: not null })
            {
                var bgimgdir = $"Resources/{widget.WidgetPath.IterGetName(0)}";
                var bgimguri = $"{bgimgdir}/{widget.Name}_bgimage.png";
                var bgpixbuf = new Gdk.Pixbuf(BackgroundImage.PixbufData);
                if (!File.Exists(bgimguri))
                {
                    if (!Directory.Exists(bgimgdir))
                        Directory.CreateDirectory(bgimgdir);
                    bgpixbuf.Save(bgimguri, "png");
                }
                style.AppendFormat("background-image:url(\"{0}\");", bgimguri);
                if (BackgroundImageLayout == ImageLayout.Tile)
                {
                    style.Append("background-repeat:repeat;");
                }
                else if (BackgroundImageLayout == ImageLayout.Zoom)
                {
                    style.Append("background-repeat:no-repeat;");
                    style.Append("background-size: contain;");
                    style.Append("background-position:center;");
                }
                else if (BackgroundImageLayout == ImageLayout.Stretch)
                {
                    style.Append("background-repeat:no-repeat;");
                    style.Append("background-size: cover;");
                    style.Append("background-position:center;");
                }
                else if (BackgroundImageLayout == ImageLayout.Center)
                {
                    style.Append("background-repeat:no-repeat;");
                    if (widget.HeightRequest < bgpixbuf.Height)
                        style.Append("background-position:top,center;");
                    else
                        style.Append("background-position:center,center;");

                }
                else
                {
                    style.Append("background-repeat:no-repeat;");
                }
                style.Append("background-origin: padding-box;");
                style.Append("background-clip: padding-box;");
                if (BackColor.Name != "0")
                {
                    var backColor = BackColor;
                    var color = $"rgba({backColor.R},{backColor.G},{backColor.B},{backColor.A})";
                    style.AppendFormat("background-color:{0};", color);
                }
            }
            else if (BackColor.Name != "0")
            {
                var backColor = BackColor;
                var color = $"rgba({backColor.R},{backColor.G},{backColor.B},{backColor.A})";
                style.AppendFormat("background-color:{0};background:{0};", color);
            }

            if (ForeColor.Name != "0")
            {
                var foreColorValue = ForeColor;
                var color = $"rgba({foreColorValue.R},{foreColorValue.G},{foreColorValue.B},{foreColorValue.A})";
                style.AppendFormat("color:{0};", color);
            }
            if (Font != null)
            {
                var fontValue = Font;
                if (fontValue.Unit == GraphicsUnit.Pixel)
                    style.AppendFormat("font-size:{0}px;", fontValue.Size);
                else if (fontValue.Unit == GraphicsUnit.Inch)
                    style.AppendFormat("font-size:{0}in;", fontValue.Size);
                else if (fontValue.Unit == GraphicsUnit.Point)
                    style.AppendFormat("font-size:{0}pt;", fontValue.Size);
                else if (fontValue.Unit == GraphicsUnit.Millimeter)
                    style.AppendFormat("font-size:{0}mm;", fontValue.Size);
                else if (fontValue.Unit == GraphicsUnit.Document)
                    style.AppendFormat("font-size:{0}cm;", fontValue.Size);
                else if (fontValue.Unit == GraphicsUnit.Display)
                    style.AppendFormat("font-size:{0}pc;", fontValue.Size);
                else
                    style.AppendFormat("font-size:{0}pt;", fontValue.Size);

                if (string.IsNullOrWhiteSpace(fontValue.FontFamily?.Name) == false)
                {
                    style.AppendFormat("font-family:\"{0}\";", fontValue.FontFamily?.Name);
                }
                if ((fontValue.Style & FontStyle.Bold) != 0)
                {
                    style.Append("font-weight:bold;");
                }
                if ((fontValue.Style & FontStyle.Italic) != 0)
                {
                    style.Append("font-style:italic;");
                }
                if ((fontValue.Style & FontStyle.Underline) != 0)
                {
                    style.Append("text-decoration:underline;");
                }
                if ((fontValue.Style & FontStyle.Strikeout) != 0)
                {
                    style.Append("text-decoration:line-through;");
                }
            }
            if (style.Length > 10)
            {
                var styleClassName = $"s{UniqueKey}";
                var css = new StringBuilder();
                css.AppendLine($".{styleClassName}{{{style}}}");
                if (widget is TextView)
                {
                    css.AppendLine($".{styleClassName} text{{{style}}}");
                    css.AppendLine($".{styleClassName} .view{{{style}}}");
                }
                var provider = new CssProvider();
                if (provider.LoadFromData(css.ToString()))
                {
                    if (widget.StyleContext.HasClass(styleClassName))
                        widget.StyleContext.RemoveProvider(provider);
                    widget.StyleContext.AddProvider(provider, 900);
                    widget.StyleContext.AddClass(styleClassName);
                }
            }
        }
    }

    protected virtual void SetStyle(ControlStyles styles, bool value)
    {
        controlStyle = value ? controlStyle | styles : controlStyle & ~styles;
    }

    public virtual Image? Image { get; set; }

    public virtual ContentAlignment ImageAlign { get; set; }

    public virtual bool UseVisualStyleBackColor { get; set; } = true;

    public virtual Color VisualStyleBackColor { get; set; }

    public virtual ImageLayout BackgroundImageLayout
    {
        get => Self == null ? ImageLayout.None : Self.Override.BackgroundImageLayout;
        set
        {
            if (Self != null)
            {
                var layout = Self.Override.BackgroundImageLayout;
                Self.Override.BackgroundImageLayout = value;
                if (layout != value)
                {
                    OnBackgroundImageLayoutChanged(EventArgs.Empty);
                }
            }
        }
    }

    public virtual Image? BackgroundImage
    {
        get => Self == null ? null : Self.Override.BackgroundImage;
        set
        {
            if (Self != null)
            {
                var overrideBackgroundImage = Self.Override.BackgroundImage;
                Self.Override.BackgroundImage = value;
                Refresh();
                if (overrideBackgroundImage != value)
                {
                    OnBackgroundImageChanged(EventArgs.Empty);
                }
            }
        }
    }

    public virtual Color BackColor
    {
        get
        {
            if (Self.Override.BackColor.HasValue)
                return Self.Override.BackColor.Value;
            //else if (UseVisualStyleBackColor)
            //    return Color.FromName("0");
            //else
            //    return Color.Transparent; 
            return Color.Transparent; //Color.FromName("0");
        }
        set
        {
            var overrideBackColor = Self.Override.BackColor;
            Self.Override.BackColor = value;
            if (overrideBackColor != value)
            {
                OnBackColorChanged(EventArgs.Empty);
            }
            Self.Override.OnAddClass();
            UpdateStyle();
            Refresh();
        }
    }

    public virtual AccessibleObject? AccessibilityObject
    {
        get
        {
            IsHandleCreated = true;
            return accessibilityObject;
        }
        set
        {
            accessibilityObject = value;
            IsHandleCreated = true;
        }
    }

    public virtual string? AccessibleDefaultActionDescription
    {
        get => accessibleDefaultActionDescription;
        set
        {
            accessibleDefaultActionDescription = value;
        }
    }

    public virtual string? AccessibleDescription { get; set; }

    public virtual string? AccessibleName { get; set; }

    public virtual AccessibleRole AccessibleRole { get; set; }

    public virtual bool AllowDrop { get; set; }

    public virtual AnchorStyles Anchor
    {
        get => _anchor;
        set
        {
            _anchor = value;
            if (Widget is Widget widget) SetAnchorStyles(widget, _anchor);

            OnAnchorChanged(EventArgs.Empty);
        }
    }

    private void SetAnchorStyles(Widget widget, AnchorStyles anchorStyles)
    {
        if (anchorStyles.HasFlag(AnchorStyles.Left) && anchorStyles.HasFlag(AnchorStyles.Right))
        {
            widget.Halign = Align.Fill;
        }
        else if (anchorStyles.HasFlag(AnchorStyles.Left))
        {
            widget.Halign = Align.Start;
        }
        else if (anchorStyles.HasFlag(AnchorStyles.Right))
        {
            widget.Halign = Align.End;
        }
        else
        {
            widget.Halign = Align.Start;
        }

        if (anchorStyles.HasFlag(AnchorStyles.Top) && anchorStyles.HasFlag(AnchorStyles.Bottom))
        {
            widget.Valign = Align.Fill;
        }
        else if (anchorStyles.HasFlag(AnchorStyles.Top))
        {
            widget.Valign = Align.Start;
        }
        else if (anchorStyles.HasFlag(AnchorStyles.Bottom))
        {
            widget.Valign = Align.End;
        }
        else
        {
            widget.Valign = Align.Start;
        }
    }
    public virtual Point AutoScrollOffset { get; set; }

    public virtual bool AutoSize
    {
        get => _autoSize;
        set
        {
            var autoSize = _autoSize;
            _autoSize = value;
            if (autoSize != value)
            {
                OnAutoSizeChanged(EventArgs.Empty);
            }
        }
    }

    internal bool bindingContextSet;

    public virtual BindingContext? BindingContext
    {
        get => bindingContext;
        set
        {
            if (bindingContext != value)
            {
                var e = EventArgs.Empty;
                OnBindingContextChanged(e);
            }
            bindingContext = value;
            bindingContextSet = true;
        }
    }

    public virtual Rectangle Bounds
    {
        get => new(Widget.Clip.X, Widget.Clip.Y, Widget.Clip.Width, Widget.Clip.Height);
        set
        {
            var r = new Rectangle(Widget.Clip.X, Widget.Clip.Y, Widget.Clip.Width,
                Widget.Clip.Height);
            SetBounds(value.X, value.Y, value.Width, value.Height);
            if (r != value)
            {
                OnLayout(new LayoutEventArgs(this, nameof(Bounds)));
                OnResize(EventArgs.Empty);
                OnSizeChanged(EventArgs.Empty);
                OnClientSizeChanged(EventArgs.Empty);
            }
        }
    }

    public virtual bool CanFocus => Widget.CanFocus;

    public virtual bool CanSelect { get; set; }

    public virtual bool Capture
    {
        get => _capture;
        set
        {
            _capture = value;
            IsHandleCreated = true;
            Handle = IntPtr.Zero;
        }
    }

    public virtual bool CausesValidation
    {
        get => causesValidation;
        set
        {
            var validation = causesValidation;
            causesValidation = value;
            if (validation != value)
            {
                OnCausesValidationChanged(EventArgs.Empty);
            }
        }
    }

    public virtual string? CompanyName { get; set; }

    public virtual bool ContainsFocus { get; set; }

    public virtual ContextMenuStrip? ContextMenuStrip
    {
        get => _contextMenuStrip;
        set
        {
            var menuStrip = _contextMenuStrip;
            _contextMenuStrip = value;
            if (menuStrip != value)
            {
                OnContextMenuStripChanged(EventArgs.Empty);
            }
        }
    }

    public virtual ControlCollection Controls { get; set; } = null!;

    public virtual bool Created => created;
    internal bool created;

    public virtual Cursor? Cursor
    {
        get => cursor;
        set
        {
            var c = cursor;
            cursor = value;
            if (c != value)
            {
                OnCursorChanged(EventArgs.Empty);
            }
        }
    }

    public virtual ControlBindingsCollection? DataBindings { get; set; }

    public virtual int DeviceDpi { get; set; }

    public virtual Rectangle DisplayRectangle { get; set; }

    public virtual bool IsDisposing { get; set; }
    private DockStyle _dock;
    public virtual DockStyle Dock
    {
        get => _dock;
        set
        {
            var dockStyle = _dock;
            _dock = value;
            var widget = Widget;
            if (value == DockStyle.Fill)
            {
                widget.Halign = Align.Fill;
                widget.Valign = Align.Fill;
            }
            else if (value == DockStyle.Left)
            {
                widget.Halign = Align.Start;
                widget.Valign = Align.Fill;
            }
            else if (value == DockStyle.Top)
            {
                widget.Halign = Align.Fill;
                widget.Valign = Align.Start;
            }
            else if (value == DockStyle.Right)
            {
                widget.Halign = Align.End;
                widget.Valign = Align.Fill;
            }
            else if (value == DockStyle.Bottom)
            {
                widget.Halign = Align.Fill;
                widget.Valign = Align.End;
            }
            else if (value == DockStyle.None)
            {
                widget.Halign = Align.Start;
                widget.Valign = Align.Start;
            }
            if (dockStyle != value)
                OnDockChanged(EventArgs.Empty);
        }
    }

    public virtual bool Enabled
    {
        get => Widget.Sensitive;
        set
        {
            var sensitive = false;
            if (Widget != null)
            {
                sensitive = Widget.Sensitive;
                Widget.Sensitive = value;
            }

            if (sensitive != value)
            {
                OnEnabledChanged(EventArgs.Empty);
            }
        }
    }

    public virtual bool Focused => Widget.IsFocus;
    private Font? font;
    public virtual Font? Font
    {
        get
        {
            if (font == null)
            {
                var fontdes = Widget.PangoContext?.FontDescription;
                var size = Convert.ToInt32((fontdes?.Size ?? 0) / Pango.Scale.PangoScale);
                if (fontdes != null)
                {
                    return new Font(new FontFamily(fontdes.Family), size);
                }
            }

            return font;
        }
        set
        {
            var fontValue = font;
            font = value; UpdateStyle();
            if (!Equals(fontValue, value))
            {
                OnFontChanged(EventArgs.Empty);
                var layoutEventArgs = new LayoutEventArgs(this, nameof(Font));
                OnLayout(layoutEventArgs);
            }
        }
    }

    private Color foreColor;

    public virtual Color ForeColor
    {
        get => foreColor;
        set
        {
            var foreColorValue = foreColor;
            foreColor = value;
            UpdateStyle();
            if (foreColorValue != value)
            {
                OnForeColorChanged(EventArgs.Empty);
            }
        }
    }

    public virtual bool HasChildren { get; set; }

    public virtual ImeMode ImeMode
    {
        get => imeMode;
        set
        {
            var mode = imeMode;
            imeMode = value;
            if (mode != value)
            {
                OnImeModeChanged(EventArgs.Empty);
            }
        }
    }

    public virtual bool InvokeRequired { get; set; }

    public virtual bool IsAccessible { get; set; }

    public virtual bool IsDisposed { get; internal set; }

    public virtual bool IsHandleCreated
    {
        get => Widget?.IsRealized ?? isHandleCreated;
        set
        {
            if (Widget != null)
            {
                Widget.IsRealized = value;
            }
            else
            {
                isHandleCreated = value;
            }
        }
    }

    public virtual bool IsMirrored { get; internal set; }
    public virtual LayoutEngine? LayoutEngine { get; set; }

    public virtual string Text
    {
        get => text;
        set
        {
            var s = text;
            text = value;
            if (s != value)
            {
                OnTextChanged(EventArgs.Empty);
            }
        }
    }

    public virtual int Top
    {
        get => Widget.MarginTop;
        set
        {
            var widget = Widget;
            var marginTop = widget?.MarginTop ?? 0;
            if (widget != null)
            {
                widget.MarginTop = value;
            }
            if (marginTop != value)
            {
                OnLocationChanged(EventArgs.Empty);
            }

            OnDockChanged(EventArgs.Empty);
            OnAnchorChanged(EventArgs.Empty);
        }
    }

    public virtual int Left
    {
        get => Widget.MarginStart;
        set
        {
            var widget = Widget;
            var marginStart = widget?.MarginStart ?? 0;
            if (widget != null)
            {
                widget.MarginStart = value;
            }

            if (marginStart != value)
            {
                OnMove(EventArgs.Empty);
                OnLocationChanged(EventArgs.Empty);
            }

            OnDockChanged(EventArgs.Empty);
            OnAnchorChanged(EventArgs.Empty);
        }
    }

    public virtual int Right => Widget.MarginEnd;

    public virtual int Bottom => Widget.MarginBottom;
    internal bool LockLocation = false;// Because the code is executed sequentially, special locks
    public virtual Point Location
    {
        get => new(Left, Top);
        set
        {
            if (LockLocation == false)
            {
                Left = value.X;
                Top = value.Y;
            }
        }
    }
    public virtual string Name
    {
        get => Widget.Name ?? string.Empty;
        set
        {
            var widget = Widget;
            if (widget != null)
            {
                widget.Name = value ?? string.Empty;
            }
        }
    }
    public virtual Padding Padding { get; set; }
    public virtual Control? Parent { get; set; }
    public virtual Size PreferredSize { get; set; }
    public virtual string? ProductName { get; set; }
    public virtual string? ProductVersion { get; set; }
    public virtual bool RecreatingHandle { get; set; }
    public virtual Region? Region { get; set; }

    public virtual RightToLeft RightToLeft
    {
        get => rightToLeft;
        set
        {
            var toLeft = rightToLeft;
            var b = toLeft != value;
            rightToLeft = value;
            if (b)
            {
                OnRightToLeftChanged(EventArgs.Empty);
            }
        }
    }

    private Size _size;
    private object? gtkControl;
    private RightToLeft rightToLeft;
    private int tabIndex;
    private bool tabStop;
    private string text = string.Empty;

    internal IntPtr FakeHandle
    {
        get => fakeHandle;
        set
        {
            fakeHandle = value;
            OnBindingContextChanged(EventArgs.Empty);
        }
    }

    private object? tag;
    private IntPtr fakeHandle;
    private bool isHandleCreated;
    private string? accessibleDefaultActionDescription;

    public virtual Size Size
    {
        get => _size;
        set
        {
            var b = _size != value;
            _size = value;
            if (AutoSize == false)
            {
                if (Widget is Gtk.Button)
                {
                    Width = value.Width > 6 ? value.Width - 6 : value.Width;
                    Height = value.Height > 6 ? value.Height - 6 : value.Height;
                }
                else
                {
                    Width = value.Width;
                    Height = value.Height;
                }
            }

            if (b)
            {
                OnLayout(new LayoutEventArgs(this, nameof(Size)));
                OnResize(EventArgs.Empty);
                OnSizeChanged(EventArgs.Empty);
            }
        }
    }

    public virtual int TabIndex
    {
        get => tabIndex;
        set
        {
            var index = tabIndex;
            var b = index != value;
            tabIndex = value;
            if (b)
            {
                OnTabIndexChanged(EventArgs.Empty);
            }
        }
    }

    public virtual bool TabStop
    {
        get => tabStop;
        set
        {
            var stop = tabStop;
            tabStop = value;
            if (stop != value)
            {
                OnTabStopChanged(EventArgs.Empty);
            }
        }
    }

    public object? Tag
    {
        get => tag ?? DBNull.Value;
        set => tag = value;
    }

    public virtual int Height
    {
        get
        {
            if (Widget.IsMapped == false && Widget is Window wnd)
            {
                return wnd.HeightRequest == -1 ? wnd.DefaultHeight : wnd.HeightRequest;
            }
            return Widget.HeightRequest == -1 ? Widget.AllocatedHeight : Widget.HeightRequest;
        }
        set
        {
            var heightRequest = Widget.HeightRequest;
            var max = Math.Max(-1, value);
            var b = heightRequest != max;
            Widget.HeightRequest = max;
            if (DockChanged != null)
                OnDockChanged(EventArgs.Empty);
            if (AnchorChanged != null)
                OnAnchorChanged(EventArgs.Empty);
            if (b)
            {
                OnResize(EventArgs.Empty);
                OnSizeChanged(EventArgs.Empty);
                OnLayout(new LayoutEventArgs(this, nameof(Height)));
            }
        }
    }
    public virtual int Width
    {
        get
        {
            if (Widget.IsMapped == false && Widget is Window wnd)
            {
                return wnd.WidthRequest == -1 ? wnd.DefaultWidth : wnd.WidthRequest;
            }
            return Widget.WidthRequest == -1 ? Widget.AllocatedWidth : Widget.WidthRequest;
        }
        set
        {
            var request = Widget.WidthRequest;
            var max = Math.Max(-1, value);
            var b = request != max;
            Widget.WidthRequest = max;
            if (DockChanged != null)
                OnDockChanged(EventArgs.Empty);
            if (AnchorChanged != null)
                OnAnchorChanged(EventArgs.Empty);
            if (b)
            {
                OnResize(EventArgs.Empty);
                OnSizeChanged(EventArgs.Empty);
            }
        }
    }

    public virtual Control? TopLevelControl { get; internal set; }

    public virtual bool UseWaitCursor { get; set; }

    public virtual bool Visible
    {
        get => Widget.Visible;
        set
        {
            var widget = Widget;
            var visible = widget?.Visible ?? true;
            if (widget != null)
            {
                widget.Visible = value;
                widget.NoShowAll = value == false;
            }

            if (visible != value)
            {
                OnVisibleChanged(EventArgs.Empty);
            }
        }
    }

    protected virtual Size DefaultSize { get; set; }

    protected virtual Padding DefaultPadding { get; set; }

    protected virtual Size DefaultMinimumSize { get; set; }

    protected virtual Padding DefaultMargin { get; set; }

    protected virtual Cursor? DefaultCursor { get; set; }

    protected virtual bool DoubleBuffered { get; set; }

    protected int FontHeight { get; set; }

    protected virtual Size DefaultMaximumSize { get; set; }

    protected virtual ImeMode ImeModeBase { get; set; }

    protected virtual ImeMode DefaultImeMode { get; set; }

    protected virtual bool CanEnableIme { get; set; }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual bool ScaleChildren { get; set; }

    protected bool ResizeRedraw { get; set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected internal virtual bool ShowFocusCues { get; set; }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected internal virtual bool ShowKeyboardCues { get; set; }

    public virtual IWindowTarget? WindowTarget { get; set; }

    public virtual IAsyncResult BeginInvoke(Delegate method, params object[] args)
    {
        if (FakeHandle == (IntPtr)int.MaxValue)
        {
            throw new InvalidOperationException("BeginInvoke can be called after the parent form event LoadComplete. Before that event use: 'Task.Factory.StartNew'");
        }
        var taskCompletionSource = new TaskCompletionSource<object>();
        GLib.Idle.Add(() =>
        {
            var r = method.DynamicInvoke(args);
            taskCompletionSource.SetResult(r);
            return false;
        });
        return taskCompletionSource.Task;
    }

    public virtual IAsyncResult BeginInvoke(Delegate method)
    {
        return BeginInvoke(method, []);
    }

    public virtual IAsyncResult BeginInvoke(Action method)
    {
        if (FakeHandle == (IntPtr)int.MaxValue)
        {
            throw new InvalidOperationException("BeginInvoke can be called after the parent form event LoadComplete. Before that event use: 'Task.Factory.StartNew'");
        }
        var taskCompletionSource = new TaskCompletionSource<object>();
        GLib.Idle.Add(() =>
        {
            method.Invoke();
            taskCompletionSource.SetResult(string.Empty);
            return false;
        });
        return taskCompletionSource.Task;
    }

    public virtual object EndInvoke(IAsyncResult asyncResult)
    {
        if (asyncResult is Task task)
        {
            if (task.IsCompleted == false && task is { IsCanceled: false, IsFaulted: false })
                task.GetAwaiter().GetResult();
        }
        return asyncResult.AsyncState;
    }

    public virtual void BringToFront()
    {

    }

    public virtual bool Contains(Control ctl)
    {
        return false;
    }

    public virtual void CreateControl()
    {
        IsHandleCreated = true;
        created = true;
    }

    public virtual Graphics CreateGraphics()
    {
        try
        {
            if (image == null)
                image = new ImageSurface(Format.Argb32, Widget.AllocatedWidth, Widget.AllocatedHeight);

            surface?.Dispose();
            surface = image.CreateSimilar(Content.ColorAlpha, Widget.AllocatedWidth, Widget.AllocatedHeight);
            context?.Dispose();
            context = new Context(surface);
            IsHandleCreated = true;
            return new Graphics(Widget, context, Widget.Allocation);
        }
        catch (Exception ex)
        {
            Trace.WriteLine(@"Failed to create graphics：" + ex.Message);
            throw;
        }
    }

    private void Override_PaintGraphics(object sender, PaintGraphicsEventArgs e)
    {
        var cr = e.Context;
        var rec = e.Rectangle;
        if (surface != null && cr != null)
        {
            cr.Save();
            cr.SetSourceSurface(surface, 0, 0);
            cr.Paint();
            cr.Restore();
        }
    }

    public virtual DragDropEffects DoDragDrop(object? data, DragDropEffects allowedEffects)
    {
        return DragDropEffects.None;
    }

    public virtual void DrawToBitmap(Bitmap bitmap, Rectangle targetBounds)
    {
    }

    public virtual Form? FindForm()
    {
        if (Widget.Toplevel?.Data.ContainsKey("Control") ?? false)
        {
            return Widget.Toplevel.Data["Control"] as Form;
        }

        var control = Parent;
        while (control != null)
        {
            if (control is Form)
                break;
            control = Parent;
        }
        return control as Form;
    }

    public virtual bool Focus()
    {
        if (Widget != null)
        {
            Widget.IsFocus = true;
            return Widget.IsFocus;
        }

        return false;
    }

    public virtual Control? GetChildAtPoint(Point pt)
    {
        IsHandleCreated = true;
        return null;
    }

    public virtual Control? GetChildAtPoint(Point pt, GetChildAtPointSkip skipValue)
    {
        IsHandleCreated = true;
        return null;
    }

    public virtual IContainerControl? GetContainerControl()
    {
        return this as IContainerControl;
    }

    public virtual Control? GetNextControl(Control ctl, bool forward)
    {
        Control? prev = null;
        Control? next = null;
        var finded = false;

        if (Controls != null)
        {
            foreach (var obj in Controls)
            {
                if (obj is Control control)
                {
                    if (finded)
                        next = control;

                    if (control.Widget.Handle == ctl.Widget.Handle)
                    {
                        finded = true;
                    }

                    if (finded)
                    {
                        if (forward == false && prev != null)
                        {
                            return prev;
                        }

                        if (forward && next != null)
                        {
                            return next;
                        }
                    }

                    prev = control;
                }
            }
        }

        return null;
    }

    public virtual Size GetPreferredSize(Size proposedSize)
    {
        return proposedSize;
    }

    public virtual void Invalidate()
    {
        Invalidate(true);
    }

    public virtual void Invalidate(bool invalidateChildren)
    {
        Invalidate(new Rectangle(Widget.Allocation.X, Widget.Allocation.Y, Widget.Allocation.Width, Widget.Allocation.Height), invalidateChildren);
    }

    public virtual void Invalidate(Rectangle rc)
    {
        Invalidate(rc, true);
    }

    public virtual void Invalidate(Rectangle rc, bool invalidateChildren)
    {
        if (Widget != null)
        {
            if (Self != null)
                Self.Override.OnAddClass();
            Widget.Window?.InvalidateRect(new Gdk.Rectangle(rc.X, rc.Y, rc.Width, rc.Height), invalidateChildren);
            if (invalidateChildren && Widget is Gtk.Container container)
            {
                foreach (var child in container.Children)
                    child.Window.InvalidateRect(child.Allocation, invalidateChildren);
            }
            Refresh();
        }
    }

    public virtual void Invalidate(Region region)
    {
        Invalidate(region, true);
    }

    public virtual void Invalidate(Region region, bool invalidateChildren)
    {
        if (Widget != null)
        {
            Self?.Override.OnAddClass();
            Widget.Window.InvalidateRect(Widget.Allocation, invalidateChildren);
        }
    }

    public virtual object? Invoke(Delegate method)
    {
        return Invoke(method, []);
    }

    public virtual object? Invoke(Delegate method, params object[] args)
    {
        object? result = null;
        if (!IsHandleCreated)
        {
            throw new InvalidOperationException();
        }
        using var mre = new ManualResetEvent(false);
        try
        {
            GLib.Idle.Add(() =>
            {
                result = method.DynamicInvoke(args);
                return false;
            });
        }
        finally
        {
            mre.Set(); // signal that we're done
        }
        return result;
    }
    public static void Invoke(Action action)
    {
        using var mre = new ManualResetEvent(false);
        GLib.Idle.Add(() =>
        {
            try
            {
                action();
            }
            finally
            {
                mre.Set(); // signal that we're done
            }

            return false; // run only once
        });

        mre.WaitOne(); // block current thread until signaled
    }
    public virtual TEntry? Invoke<TEntry>(Func<TEntry> method)
    {
        var result = default(TEntry);
        using var mre = new ManualResetEvent(false);
        GLib.Idle.Add(() =>
        {
            try
            {
                result = method.Invoke();
            }
            finally
            {
                mre.Set(); // signal that we're done
            }

            return false; // run only once
        });

        mre.WaitOne(); // block current thread until signaled
        return result;
    }

    public virtual int LogicalToDeviceUnits(int value)
    {
        return value;
    }

    public virtual Size LogicalToDeviceUnits(Size value)
    {
        return value;
    }

    public virtual Point PointToClient(Point p)
    {
        var x = 0;
        var y = 0;
        IsHandleCreated = true;
        if (Widget != null)
        {
            Widget.Window?.GetOrigin(out x, out y);
            if (p.X > x && p.Y > y)
                return new Point(p.X - x, p.Y - y);
        }
        return new Point(p.X, p.Y);
    }

    public virtual Point PointToScreen(Point p)
    {
        var x = 0;
        var y = 0;
        IsHandleCreated = true;
        if (Widget != null)
        {
            Widget.Window?.GetOrigin(out x, out y);
            if (p.X < x && p.Y < y)
                return new Point(p.X + x, p.Y + y);
        }
        return new Point(p.X, p.Y);
    }

    public virtual PreProcessControlState PreProcessControlMessage(ref Message msg)
    {
        return PreProcessControlState.MessageNotNeeded;
    }

    public virtual bool PreProcessMessage(ref Message msg)
    {
        return false;
    }

    public virtual Rectangle RectangleToClient(Rectangle r)
    {
        var x = 0;
        var y = 0;
        IsHandleCreated = true;
        if (Widget != null)
        {
            Widget.Window?.GetPosition(out x, out y);
            if (r.X > x && r.Y > y)
                return new Rectangle(r.X - x, r.Y - y, r.Width, r.Height);
        }
        return new Rectangle(r.X, r.Y, r.Width, r.Height);
    }

    public virtual Rectangle RectangleToScreen(Rectangle r)
    {
        var x = 0;
        var y = 0;
        IsHandleCreated = true;
        if (Widget != null)
        {
            Widget.Window?.GetPosition(out x, out y);
            if (r.X < x && r.Y < y)
                return new Rectangle(r.X + x, r.Y + y, r.Width, r.Height);
        }
        return new Rectangle(r.X, r.Y, r.Width, r.Height);
    }

    public virtual void Refresh()
    {
        if (Widget is { IsVisible: true })
        {
            if (Self != null)
                Self.Override.OnAddClass();
            Widget.QueueDraw();
            if (Widget is Gtk.Container container)
            {
                foreach (var child in container.Children)
                    child.QueueDraw();
            }
        }

        if (Visible && (Handle != IntPtr.Zero || FakeHandle != IntPtr.Zero))
        {
            OnInvalidated(new InvalidateEventArgs(default));
        }
    }

    public virtual void ResetBackColor()
    {

    }

    public virtual void ResetBindings()
    {

    }

    public virtual void ResetCursor()
    {

    }

    public virtual void ResetFont()
    {

    }

    public virtual void ResetForeColor()
    {

    }

    public virtual void ResetImeMode()
    {

    }

    public virtual void ResetRightToLeft()
    {

    }

    public virtual void ResetText()
    {

    }

    public virtual void ResumeLayout()
    {
        ResumeLayout(false);
    }

    public virtual void ResumeLayout(bool performLayout)
    {
        created = true;
    }

    public virtual void Scale(float ratio)
    {

    }

    public virtual void Scale(float dx, float dy)
    {

    }

    public virtual void Scale(SizeF factor)
    {

    }

    public virtual void ScaleBitmapLogicalToDevice(ref Bitmap logicalBitmap)
    {

    }

    public virtual void Select()
    {
        Widget?.SetStateFlags(StateFlags.Selected, true);
    }

    public virtual bool SelectNextControl(Control ctl, bool forward, bool tabStopOnly, bool nested, bool wrap)
    {
        return false;
    }

    public virtual void SendToBack()
    {

    }

    public virtual void SetBounds(int x, int y, int width, int height)
    {
        SetBounds(x, y, width, height, BoundsSpecified.All);
    }

    public virtual void SetBounds(int x, int y, int width, int height, BoundsSpecified specified)
    {
        if (Widget != null)
        {
            var rect = Widget.Clip;
            if (specified == BoundsSpecified.X)
                rect.X = x;
            else if (specified == BoundsSpecified.Y)
                rect.Y = y;
            else if (specified == BoundsSpecified.Width)
                rect.Width = width;
            else if (specified == BoundsSpecified.Height)
                rect.Height = height;
            else if (specified == BoundsSpecified.Size)
            {
                rect.Width = width;
                rect.Height = height;
            }
            else if (specified == BoundsSpecified.Location)
            {
                rect.X = x;
                rect.Y = y;
            }
            else
            {
                rect.X = x;
                rect.Y = y;
                rect.Width = width;
                rect.Height = height;
            }
            Widget.SetClip(rect);
        }
    }

    public virtual Rectangle ClientRectangle { get { Widget.GetAllocatedSize(out var allocation, out _); return new Rectangle(allocation.X, allocation.Y, allocation.Width, allocation.Height); } }

    public virtual Size ClientSize
    {
        get => new(Widget.AllocatedWidth, Widget.AllocatedHeight);
        set
        {
            var size = new Size(Widget.AllocatedWidth, Widget.AllocatedHeight);
            Widget.SetSizeRequest(value.Width, value.Height);
            if (size != value)
            {
                OnLayout(new LayoutEventArgs(this, nameof(ClientSize)));
                OnResize(EventArgs.Empty);
                OnSizeChanged(EventArgs.Empty);
            }
            OnClientSizeChanged(EventArgs.Empty);
        }
    }

    public virtual IntPtr Handle
    {
        get
        {
            if (FakeHandle == IntPtr.Zero)
            {
                FakeHandle = (IntPtr)int.MaxValue;
            }
            if (Widget.Handle != IntPtr.Zero || FakeHandle != IntPtr.Zero)
            {
                OnHandleCreated(EventArgs.Empty);
            }
            return Widget.Handle;
        }
        set
        {
            FakeHandle = value;
            OnHandleCreated(EventArgs.Empty);
        }
    }

    public virtual Padding Margin
    {
        get => margin;
        set
        {
            var padding = margin;
            margin = value;
            if (padding != value)
            {
                OnMarginChanged(EventArgs.Empty);
            }
        }
    }

    public virtual Size MaximumSize { get; set; }

    public virtual Size MinimumSize { get; set; }

    public virtual BorderStyle BorderStyle
    {
        get => _borderStyle;
        set
        {
            _borderStyle = value;
            if (value == BorderStyle.FixedSingle)
            {
                Widget.StyleContext.RemoveClass("BorderFixed3D");
                Widget.StyleContext.RemoveClass("BorderNone");
                Widget.StyleContext.AddClass("BorderFixedSingle");
            }
            else if (value == BorderStyle.Fixed3D)
            {
                Widget.StyleContext.RemoveClass("BorderFixedSingle");
                Widget.StyleContext.RemoveClass("BorderNone");
                Widget.StyleContext.AddClass("BorderFixed3D");
            }
            else
            {
                Widget.StyleContext.RemoveClass("BorderFixedSingle");
                Widget.StyleContext.RemoveClass("BorderFixed3D");
                Widget.StyleContext.AddClass("BorderNone");
            }
        }
    }

    public virtual void Hide()
    {
        if (GtkControl is Misc con)
        {
            con.Hide();
        }
    }

    public virtual void Show()
    {
        Widget?.ShowAll();
    }

    public virtual void SuspendLayout()
    {
        created = false;
    }

    public virtual void PerformLayout()
    {
        created = true;
    }

    public virtual void PerformLayout(Control affectedControl, string affectedProperty)
    {
        created = true;
    }

    public virtual void Update()
    {
        if (Widget != null)
        {
            Widget.Window?.ProcessUpdates(true);
            Widget.QueueDraw();
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public virtual void BeginInit()
    {

    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public virtual void EndInit()
    {

    }

    public new virtual void Dispose()
    {
        if (!IsDisposed)
        {
            var eventArgs = new CancelEventArgs();
            OnDisposing(eventArgs);
            if (eventArgs.Cancel)
            {
                return;
            }
            Dispose(true);
            base.Dispose();
            IsHandleCreated = false;
            OnDisposed(EventArgs.Empty);

        }
    }

    protected override void Dispose(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }
        IsDisposed = true;
        try
        {
            image?.Dispose();
            surface?.Dispose();
            context?.Dispose();

            if (Widget != null)
            {
                Widget.Destroy();
                GtkControl = null;
            }
        }
        catch (Exception ex) { Trace.WriteLine(ex); }
        base.Dispose(disposing);
        OnHandleDestroyed(EventArgs.Empty);
    }

    protected virtual CreateParams CreateParams
    {
        get
        {
            var createParams = new CreateParams();
            createParams.ExStyle |= 32;
            return createParams;
        }
    }

    public bool ParticipatesInLayout => false;

    PropertyStore IArrangedElement.Properties => throw new NotImplementedException();

    IArrangedElement IArrangedElement.Container => throw new NotImplementedException();

    public ArrangedElementCollection? Children => arrangedElementCollection;

    protected virtual void Select(bool directed, bool forward)
    {

    }

    protected virtual void WndProc(ref Message m)
    {
    }

    public void SetBounds(Rectangle bounds, BoundsSpecified specified)
    {
    }

    void IArrangedElement.PerformLayout(IArrangedElement affectedElement, string? propertyName)
    {
    }

}