/*
 * 基于GTK组件开发，兼容原生C#控件winform界面的跨平台界面组件。
 * 使用本组件GTKSystem.Windows.Forms代替Microsoft.WindowsDesktop.App.WindowsForms，一次编译，跨平台windows、linux、macos运行
 * 技术支持438865652@qq.com，https://www.gtkapp.com, https://gitee.com/easywebfactory, https://github.com/easywebfactory
 * author:chenhongjin
 */

using Gtk;
using GTKSystem.Windows.Forms.GTKControls.ControlBase;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms.Layout;
using GTKSystem.Windows.Forms.Interfaces;
using Color = System.Drawing.Color;
using Font = System.Drawing.Font;
using Graphics = System.Drawing.Graphics;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;
using Size = System.Drawing.Size;

namespace System.Windows.Forms
{
    [DefaultEvent("Click")]
    [DefaultProperty("Text")]
    [Designer(typeof(ControlDesigner))]
    [ToolboxItemFilter("System.Windows.Forms")]
    public partial class Control : Component, IControl, ISynchronizeInvoke, ISupportInitialize, IArrangedElement, IBindableComponent
    {
        private Gtk.Application app = Application.Init();
        public string unique_key { get; protected set; }

        public virtual IWidget Widget => (IWidget)GtkControl;
        public virtual IGtkControl Self => (IGtkControl)GtkControl;

        public virtual object? GtkControl { get; set; } = null!;

        public Control()
        {
            Init();
        }

        private void Init()
        {
            Disposed += Control_Disposed;
            Controls = new ControlCollection(this);
            DataBindings = new ControlBindingsCollection(this);
            Enabled = true;
            this.unique_key = Guid.NewGuid().ToString().ToLower();
            if (this.Widget != null)
            {
                if (this.Widget is Gtk.Window win)
                {
                    this.Widget.Halign = Align.Fill;
                    this.Widget.Valign = Align.Fill;
                }
                else
                {
                    this.Widget.Halign = Align.Start;
                    this.Widget.Valign = Align.Start;
                    this.Widget.Expand = false;
                }
                this.Widget.Data["Control"] = this;
                this.Widget.StyleContext.AddClass("DefaultThemeStyle");
                this.Widget.ButtonPressEvent += Widget_ButtonPressEvent;
                this.Widget.ButtonReleaseEvent += Widget_ButtonReleaseEvent;
                this.Widget.EnterNotifyEvent += Widget_EnterNotifyEvent;
                this.Widget.MotionNotifyEvent += Widget_MotionNotifyEvent;
                this.Widget.LeaveNotifyEvent += Widget_LeaveNotifyEvent;
                this.Widget.ScrollEvent += Widget_ScrollEvent;
                this.Widget.FocusInEvent += Widget_FocusInEvent;
                this.Widget.FocusOutEvent += Widget_FocusOutEvent;
                this.Widget.KeyPressEvent += Widget_KeyPressEvent;
                this.Widget.KeyReleaseEvent += Widget_KeyReleaseEvent;
                this.Widget.Realized += Widget_Realized;
                this.Widget.ConfigureEvent += Widget_ConfigureEvent;
                Self.Override.PaintGraphics += Override_PaintGraphics;
                this.Widget.SizeAllocated += Widget_SizeAllocated;
            }
        }

        private void Control_Disposed(object sender, EventArgs e)
        {
            if (!IsDisposed)
            {
                Disposed?.Invoke(this, e);
            }
        }

        private int size_width = 0;
        private int size_height = 0;
        private int location_x = 0;
        private int location_y = 0;
        private void Widget_SizeAllocated(object o, SizeAllocatedArgs args)
        {
            if (args.Allocation.Width != size_width || args.Allocation.Height != size_height)
            {
                size_width = args.Allocation.Width;
                size_height = args.Allocation.Height;
                SizeChanged?.Invoke(this, EventArgs.Empty);
            }
            if (args.Allocation.X != location_x || args.Allocation.Y != location_y)
            {
                location_x = args.Allocation.X;
                location_y = args.Allocation.Y;
                LocationChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        private void Widget_ConfigureEvent(object o, ConfigureEventArgs args)
        {
            Move?.Invoke(this, args);
        }
        #region events
        private bool WidgetRealized = false;
        private void Widget_Realized(object sender, EventArgs e)
        {
            if (WidgetRealized == false)
            {
                WidgetRealized = true;
                InitStyle((Gtk.Widget)sender);
                OnLoad(e);
                foreach (Control control in Controls)
                {
                    control.OnLoad(e);
                }
            }
        }

        protected internal void OnBindingContextChanged(EventArgs e)
        {
            BindingContextChanged?.Invoke(this, e);
        }

        private bool _loaded;

        protected internal virtual void OnLoad(EventArgs e)
        {
            if (_loaded)
            {
                return;
            }
            _loaded = true;
            Load?.Invoke(this, e);
            if (!BindingContextSet)
            {
                OnBindingContextChanged(e);
            }
        }

        private void Widget_ButtonPressEvent(object o, ButtonPressEventArgs args)
        {
            //Console.WriteLine($"Widget_ButtonPressEvent1:{args.Event.XRoot},{args.Event.YRoot};{args.Event.X},{args.Event.Y}");
            MouseButtons result = MouseButtons.None;
            if (args.Event.Button == 1)
                result = MouseButtons.Left;
            else if (args.Event.Button == 2)
                result = MouseButtons.Middle;
            else if (args.Event.Button == 3)
                result = MouseButtons.Right;

            Gtk.Widget owidget = o as Gtk.Widget;
            if (owidget != null)
            {
                owidget.Window.GetOrigin(out int x, out int y); //避免事件穿透错误
                MouseDown?.Invoke(this, new MouseEventArgs(result, 1, (int)args.Event.XRoot - x, (int)args.Event.YRoot - y, 0));
                if (args.Event.Type == Gdk.EventType.TwoButtonPress || args.Event.Type == Gdk.EventType.DoubleButtonPress)
                {
                    MouseDoubleClick?.Invoke(this, new MouseEventArgs(result, 2, (int)args.Event.XRoot - x, (int)args.Event.YRoot - y, 0));
                    DoubleClick?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    Click?.Invoke(this, EventArgs.Empty);
                    MouseClick?.Invoke(this, new MouseEventArgs(result, 1, (int)args.Event.XRoot - x, (int)args.Event.YRoot - y, 0));
                }
            }

        }
        private void Widget_ButtonReleaseEvent(object o, ButtonReleaseEventArgs args)
        {
            if (MouseUp != null)
            {
                MouseButtons result = MouseButtons.None;
                if (args.Event.Button == 1)
                    result = MouseButtons.Left;
                else if (args.Event.Button == 2)
                    result = MouseButtons.Middle;
                else if (args.Event.Button == 3)
                    result = MouseButtons.Right;
                Gtk.Widget owidget = (Gtk.Widget)o;
                owidget.Window.GetOrigin(out int x, out int y);
                MouseUp?.Invoke(this, new MouseEventArgs(result, 1, (int)args.Event.XRoot - x, (int)args.Event.YRoot - y, 0));
            }

            if (ContextMenuStrip != null)
            {
                if (args.Event.Button == 3)
                {
                    ContextMenuStrip.Widget.ShowAll();
                    ((Gtk.Menu)ContextMenuStrip.Widget).PopupAtPointer(args.Event);
                }
            }
        }

        private void Widget_EnterNotifyEvent(object o, EnterNotifyEventArgs args)
        {
            if (Cursor != null)
            {
                if (Cursor.CursorType == Gdk.CursorType.CursorIsPixmap)
                {
                    this.Widget.Window.Cursor = new Gdk.Cursor(((Gtk.Widget)o).Display, Cursor.CursorPixbuf, Cursor.CursorsXY.X, Cursor.CursorsXY.Y);
                }
                else
                {
                    this.Widget.Window.Cursor = new Gdk.Cursor(((Gtk.Widget)o).Display, Cursor.CursorType);
                }
            }

            Enter?.Invoke(this, args);
            MouseEnter?.Invoke(this, args);

            MouseHover?.Invoke(this, args);
        }
        private void Widget_MotionNotifyEvent(object o, MotionNotifyEventArgs args)
        {
            MouseMove?.Invoke(this, new MouseEventArgs(MouseButtons.None, 1, (int)args.Event.X, (int)args.Event.Y, 0));
        }
        private void Widget_LeaveNotifyEvent(object o, LeaveNotifyEventArgs args)
        {
            if (Cursor != null)
            {
                this.Widget.Window.Cursor = null;
            }

            Leave?.Invoke(this, args);
            MouseLeave?.Invoke(this, args);


        }
        private void Widget_ScrollEvent(object o, Gtk.ScrollEventArgs args)
        {
            MouseWheel?.Invoke(this, new MouseEventArgs(MouseButtons.None, 0, (int)args.Event.X, (int)args.Event.Y, (int)args.Event.DeltaY));
        }
        private void Widget_FocusInEvent(object o, FocusInEventArgs args)
        {
            GotFocus?.Invoke(this, args);
        }
        private void Widget_FocusOutEvent(object o, FocusOutEventArgs args)
        {
            LostFocus?.Invoke(this, args);

            Validating?.Invoke(this, cancelEventArgs);
            if (Validated != null && cancelEventArgs.Cancel == false)
                Validated?.Invoke(this, cancelEventArgs);
        }
        private void Widget_KeyPressEvent(object o, Gtk.KeyPressEventArgs args)
        {
            if (KeyDown != null)
            {
                if (args.Event is Gdk.EventKey eventkey)
                {
                    Keys keys = (Keys)eventkey.HardwareKeycode;
                    if (eventkey.State.HasFlag(Gdk.ModifierType.Mod1Mask))
                        keys |= Keys.Alt;
                    if (eventkey.State.HasFlag(Gdk.ModifierType.ControlMask))
                        keys |= Keys.Control;
                    if (eventkey.State.HasFlag(Gdk.ModifierType.ShiftMask))
                        keys |= Keys.Shift;
                    if (eventkey.State.HasFlag(Gdk.ModifierType.LockMask))
                        keys |= Keys.CapsLock;

                    KeyDown?.Invoke(this, new KeyEventArgs(keys));
                }
            }
        }
        private void Widget_KeyReleaseEvent(object o, KeyReleaseEventArgs args)
        {
            if (KeyUp != null)
            {
                Keys keys = (Keys)args.Event.HardwareKeycode;
                KeyUp?.Invoke(this, new KeyEventArgs(keys));
            }
            if (KeyPress != null)
            {
                Keys keys = (Keys)args.Event.HardwareKeycode;
                KeyPress?.Invoke(this, new KeyPressEventArgs(Convert.ToChar(keys)));
            }
        }

        #endregion

        //===================
        protected virtual void InitStyle(Gtk.Widget widget)
        {
            SetStyle(widget);
        }
        protected virtual void UpdateStyle()
        {
            if (this.Widget != null && this.Widget.IsMapped)
            {
                var widget = this.Widget as Gtk.Widget;
                if (widget != null) SetStyle(widget);
            }
        }
        protected virtual void UpdateBackgroundStyle()
        {
            if (this.Widget != null && this.Widget.IsMapped)
                Self.Override.OnAddClass();
        }
        protected virtual void SetStyle(Gtk.Widget widget)
        {
            StringBuilder style = new StringBuilder();
            if (widget is Gtk.Image) { }
            else
            {
                if (this.Image != null && this.Image.PixbufData != null)
                {
                    string imguri = $"Resources/{widget.WidgetPath.IterGetName(0)}${widget.Name}_img.png";
                    if (!File.Exists(imguri))
                    {
                        Gdk.Pixbuf imagepixbuf = new Gdk.Pixbuf(this.Image.PixbufData);
                        imagepixbuf.Save(imguri, "png");
                    }
                    style.AppendFormat("background:url(\"{0}\")", imguri);
                    style.Append(" no-repeat");
                    if (this.ImageAlign == ContentAlignment.TopLeft)
                    {
                        style.Append(" top left");
                    }
                    else if (this.ImageAlign == ContentAlignment.TopCenter)
                    {
                        style.Append(" top center");
                    }
                    else if (this.ImageAlign == ContentAlignment.TopRight)
                    {
                        style.Append(" top right");
                    }
                    else if (this.ImageAlign == ContentAlignment.MiddleLeft)
                    {
                        style.Append(" center left");
                    }
                    else if (this.ImageAlign == ContentAlignment.MiddleCenter)
                    {
                        style.Append(" center center");
                    }
                    else if (this.ImageAlign == ContentAlignment.MiddleRight)
                    {
                        style.Append(" center right");
                    }
                    else if (this.ImageAlign == ContentAlignment.BottomLeft)
                    {
                        style.Append(" bottom left");
                    }
                    else if (this.ImageAlign == ContentAlignment.BottomCenter)
                    {
                        style.Append(" bottom center");
                    }
                    else if (this.ImageAlign == ContentAlignment.BottomRight)
                    {
                        style.Append(" bottom right");
                    }
                    else
                    {
                        style.Append(" center center");
                    }

                    if (this.BackgroundImage != null && this.BackgroundImage.PixbufData != null)
                    {
                        string bgimguri = $"Resources/{widget.WidgetPath.IterGetName(0)}${widget.Name}_bg.png";
                        if (!File.Exists(bgimguri))
                        {
                            Gdk.Pixbuf bgpixbuf = new Gdk.Pixbuf(this.BackgroundImage.PixbufData);
                            bgpixbuf.Save(bgimguri, "png");
                        }

                        style.AppendFormat(",url(\"Resources/{0}_bg.png\") repeat", widget.Name);
                    }
                    style.Append(";");
                    style.Append("background-origin: padding-box;");
                    style.Append("background-clip: padding-box;");
                }
                else if (this.BackgroundImage != null && this.BackgroundImage.PixbufData != null)
                {
                    Gdk.Pixbuf bgpixbuf = new Gdk.Pixbuf(this.BackgroundImage.PixbufData);
                    string bgimguri = $"Resources/{widget.WidgetPath.IterGetName(0)}${widget.Name}_bg.png";
                    if (!File.Exists(bgimguri))
                    {
                        bgpixbuf.Save(bgimguri, "png");
                    }
                    style.AppendFormat("background-image:url(\"{0}\");", bgimguri);
                    if (this.BackgroundImageLayout == ImageLayout.Tile)
                    {
                        style.Append("background-repeat:repeat;");
                    }
                    else if (this.BackgroundImageLayout == ImageLayout.Zoom)
                    {
                        style.Append("background-repeat:no-repeat;");
                        style.Append("background-size: contain;");
                        style.Append("background-position:center;");
                    }
                    else if (this.BackgroundImageLayout == ImageLayout.Stretch)
                    {
                        style.Append("background-repeat:no-repeat;");
                        style.Append("background-size: cover;");
                        style.Append("background-position:center;");
                    }
                    else if (this.BackgroundImageLayout == ImageLayout.Center)
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
                    if (this.BackColor.Name != "0")
                    {
                        Color backColor = this.BackColor;
                        string color = $"rgba({backColor.R},{backColor.G},{backColor.B},{backColor.A})";
                        style.AppendFormat("background-color:{0};", color);
                    }
                }
                else if (this.BackColor.Name != "0")
                {
                    Color backColor = this.BackColor;
                    string color = $"rgba({backColor.R},{backColor.G},{backColor.B},{backColor.A})";
                    style.AppendFormat("background-color:{0};background:{0};", color);
                }

                if (this.ForeColor.Name != "0")
                {
                    Color foreColor = this.ForeColor;
                    string color = $"rgba({foreColor.R},{foreColor.G},{foreColor.B},{foreColor.A})";
                    style.AppendFormat("color:{0};", color);
                }
                if (this.Font != null)
                {
                    Font font = this.Font;
                    if (font.Unit == GraphicsUnit.Pixel)
                        style.AppendFormat("font-size:{0}px;", font.Size);
                    else if (font.Unit == GraphicsUnit.Inch)
                        style.AppendFormat("font-size:{0}in;", font.Size);
                    else if (font.Unit == GraphicsUnit.Point)
                        style.AppendFormat("font-size:{0}pt;", font.Size);
                    else if (font.Unit == GraphicsUnit.Millimeter)
                        style.AppendFormat("font-size:{0}mm;", font.Size);
                    else if (font.Unit == GraphicsUnit.Document)
                        style.AppendFormat("font-size:{0}cm;", font.Size);
                    else if (font.Unit == GraphicsUnit.Display)
                        style.AppendFormat("font-size:{0}pc;", font.Size);
                    else
                        style.AppendFormat("font-size:{0}pt;", font.Size);

                    if (string.IsNullOrWhiteSpace(font.FontFamily?.Name) == false)
                    {
                        style.AppendFormat("font-family:\"{0}\";", font.FontFamily.Name);
                    }
                    if ((font.Style & FontStyle.Bold) != 0)
                    {
                        style.Append("font-weight:bold;");
                    }
                    if ((font.Style & FontStyle.Italic) != 0)
                    {
                        style.Append("font-style:italic;");
                    }
                    if ((font.Style & FontStyle.Underline) != 0)
                    {
                        style.Append("text-decoration:underline;");
                    }
                    if ((font.Style & FontStyle.Strikeout) != 0)
                    {
                        style.Append("text-decoration:line-through;");
                    }
                }
                if (style.Length > 10)
                {
                    string styleClassName = $"s{unique_key}";
                    StringBuilder css = new StringBuilder();
                    css.AppendLine($".{styleClassName}{{{style.ToString()}}}");
                    if (widget is Gtk.TextView)
                    {
                        css.AppendLine($".{styleClassName} text{{{style.ToString()}}}");
                        css.AppendLine($".{styleClassName} .view{{{style.ToString()}}}");
                    }
                    CssProvider provider = new CssProvider();
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
        }

        #region 背景
        public virtual System.Drawing.Image Image { get; set; }
        public virtual System.Drawing.ContentAlignment ImageAlign { get; set; }

        public virtual bool UseVisualStyleBackColor { get; set; } = true;
        public virtual Color VisualStyleBackColor { get; }

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
                        BackgroundImageLayoutChanged?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        public virtual Drawing.Image BackgroundImage
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
                        BackgroundImageChanged?.Invoke(this, EventArgs.Empty);
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
                else
                    return Color.FromName("0");
            }
            set
            {
                var overrideBackColor = Self.Override.BackColor;
                Self.Override.BackColor = value;
                if (overrideBackColor != value)
                {
                    BackColorChanged?.Invoke(this, EventArgs.Empty);
                }
                Self.Override.OnAddClass();
                UpdateStyle();
                Refresh();
            }
        }
        public virtual event PaintEventHandler Paint
        {
            add { Self.Override.Paint += value; }
            remove { Self.Override.Paint -= value; }
        }
        #endregion

        public virtual AccessibleObject AccessibilityObject
        {
            get
            {
                _handleCreated = true;
                return accessibilityObject;
            }
            set => accessibilityObject = value;
        }

        public virtual string AccessibleDefaultActionDescription { get; set; }
        public virtual string AccessibleDescription { get; set; }
        public virtual string AccessibleName { get; set; }
        public virtual AccessibleRole AccessibleRole { get; set; }
        public virtual bool AllowDrop { get; set; }
        private AnchorStyles _anchor;
        public virtual AnchorStyles Anchor
        {
            get => _anchor;
            set
            {
                _anchor = value;
                if (Widget is Widget widget) SetAnchorStyles(widget, _anchor);

                AnchorChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        private void SetAnchorStyles(Gtk.Widget widget, AnchorStyles anchorStyles)
        {
            if (anchorStyles.HasFlag(AnchorStyles.Left) && anchorStyles.HasFlag(AnchorStyles.Right))
            {
                widget.Halign = Gtk.Align.Fill;
            }
            else if (anchorStyles.HasFlag(AnchorStyles.Left))
            {
                widget.Halign = Gtk.Align.Start;
            }
            else if (anchorStyles.HasFlag(AnchorStyles.Right))
            {
                widget.Halign = Gtk.Align.End;
            }
            else
            {
                widget.Halign = Gtk.Align.Start;
            }

            if (anchorStyles.HasFlag(AnchorStyles.Top) && anchorStyles.HasFlag(AnchorStyles.Bottom))
            {
                widget.Valign = Gtk.Align.Fill;
            }
            else if (anchorStyles.HasFlag(AnchorStyles.Top))
            {
                widget.Valign = Gtk.Align.Start;
            }
            else if (anchorStyles.HasFlag(AnchorStyles.Bottom))
            {
                widget.Valign = Gtk.Align.End;
            }
            else
            {
                widget.Valign = Gtk.Align.Start;
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
                    AutoSizeChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        internal bool BindingContextSet;

        public virtual BindingContext BindingContext
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
                BindingContextSet = true;
            }
        }

        public virtual Rectangle Bounds
        {
            get => new Rectangle(Widget.Clip.X, this.Widget.Clip.Y, this.Widget.Clip.Width, this.Widget.Clip.Height);
            set
            {
                var r = new Rectangle(Widget.Clip.X, this.Widget.Clip.Y, this.Widget.Clip.Width,
                    this.Widget.Clip.Height);
                SetBounds(value.X, value.Y, value.Width, value.Height);
                if (r != value)
                {
                    Layout?.Invoke(this, new LayoutEventArgs(this, nameof(Bounds)));
                    Resize?.Invoke(this, EventArgs.Empty);
                    SizeChanged?.Invoke(this, EventArgs.Empty);
                    ClientSizeChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public virtual bool CanFocus { get { return this.Widget.CanFocus; } }

        public virtual bool CanSelect { get; }

        public virtual bool Capture
        {
            get => _capture;
            set
            {
                _capture = value;
                _handleCreated = true;
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
                    CausesValidationChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public virtual string CompanyName { get; }

        public virtual bool ContainsFocus { get; }

        public virtual ContextMenuStrip ContextMenuStrip
        {
            get => _contextMenuStrip;
            set
            {
                var menuStrip = _contextMenuStrip;
                _contextMenuStrip = value;
                if (menuStrip != value)
                {
                    ContextMenuStripChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public virtual ControlCollection Controls { get; set; }

        public virtual bool Created => _Created;
        internal bool _Created;

        public virtual Cursor Cursor
        {
            get => cursor;
            set
            {
                var c = cursor;
                cursor = value;
                if (c != value)
                {
                    CursorChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public virtual ControlBindingsCollection DataBindings { get; set; }

        public virtual int DeviceDpi { get; }

        public virtual Rectangle DisplayRectangle { get; }

        public virtual bool Disposing { get; }
        private DockStyle _dock;
        public virtual DockStyle Dock
        {
            get
            {
                return _dock;
            }
            set
            {
                var dockStyle = _dock;
                _dock = value;
                var widget = this.Widget;
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
                    DockChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual bool Enabled
        {
            get => this.Widget.Sensitive;
            set
            {
                bool sensitive = false;
                if (this.Widget != null)
                {
                    sensitive = this.Widget.Sensitive;
                    this.Widget.Sensitive = value;
                }

                if (sensitive != value)
                {
                    EnabledChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public virtual bool Focused { get { return this.Widget.IsFocus; } }
        private Font _Font;
        public virtual Font Font
        {
            get
            {
                if (_Font == null)
                {
                    var fontdes = this.Widget.PangoContext.FontDescription;
                    int size = Convert.ToInt32(fontdes.Size / Pango.Scale.PangoScale);
                    return new Drawing.Font(new Drawing.FontFamily(fontdes.Family), size);
                }
                else
                    return _Font;
            }
            set
            {
                var font = _Font;
                _Font = value; UpdateStyle();
                if (font != value)
                {
                    FontChanged?.Invoke(this, EventArgs.Empty);
                    Layout?.Invoke(this, new LayoutEventArgs(this, nameof(Font)));
                }
            }
        }
        private Color _ForeColor;
        public virtual Color ForeColor
        {
            get { return _ForeColor; }
            set
            {
                var foreColor = _ForeColor;
                _ForeColor = value; UpdateStyle();
                if (foreColor != value)
                {
                    ForeColorChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public virtual bool HasChildren { get; }

        public virtual ImeMode ImeMode
        {
            get => imeMode;
            set
            {
                var mode = imeMode;
                imeMode = value;
                if (mode != value)
                {
                    ImeModeChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public virtual bool InvokeRequired { get; }

        public virtual bool IsAccessible { get; set; }

        public virtual bool IsDisposed { get; internal set; }

        public virtual bool IsHandleCreated
        {
            get => _handleCreated;
            set => _handleCreated = value;
        }

        public virtual bool IsMirrored { get; }

        public virtual LayoutEngine LayoutEngine { get; }
        public virtual int Top
        {
            get => this.Widget.MarginTop;
            set
            {
                var widget = this.Widget;
                var marginTop = widget?.MarginTop ?? 0;
                if (widget != null)
                {
                    widget.MarginTop = value;
                }
                if (marginTop != value)
                {
                    LocationChanged?.Invoke(this, EventArgs.Empty);
                }

                DockChanged?.Invoke(this, EventArgs.Empty);
                AnchorChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public virtual int Left
        {
            get => this.Widget.MarginStart;
            set
            {
                var widget = this.Widget;
                var marginStart = widget?.MarginStart ?? 0;
                if (widget != null)
                {
                    widget.MarginStart = value;
                }

                if (marginStart != value)
                {
                    Move?.Invoke(this, EventArgs.Empty);
                    LocationChanged?.Invoke(this, EventArgs.Empty);
                }

                DockChanged?.Invoke(this, EventArgs.Empty);
                AnchorChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public virtual int Right
        {
            get => this.Widget.MarginEnd;
        }
        public virtual int Bottom
        {
            get => this.Widget.MarginBottom;
        }
        internal bool LockLocation = false;//由于代码有顺序执行，特殊锁定
        public virtual Point Location
        {
            get
            {
                return new Point(Left, Top);
            }
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
            get { return this.Widget.Name ?? _name; }
            set
            {
                var widget = this.Widget;
                if (widget != null)
                {
                    widget.Name = value;
                }
                _name = value;
            }
        }
        public virtual Padding Padding { get; set; }
        public virtual Control Parent { get; set; }
        public virtual Size PreferredSize { get; }
        public virtual string ProductName { get; }
        public virtual string ProductVersion { get; }
        public virtual bool RecreatingHandle { get; }
        public virtual Drawing.Region Region { get; set; }

        public virtual RightToLeft RightToLeft
        {
            get => rightToLeft;
            set
            {
                var toLeft = rightToLeft;
                rightToLeft = value;
                if (toLeft != value)
                {
                    RightToLeftChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public virtual Size Size
        {
            get
            {
                return new Size(Width, Height);
            }
            set
            {
                if (this.Widget is Gtk.Button)
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
        }
        public virtual int Height
        {
            get
            {
                if (this.Widget.IsMapped == false && this.Widget is Gtk.Window wnd)
                {
                    return wnd.HeightRequest == -1 ? wnd.DefaultHeight : wnd.HeightRequest;
                }
                return this.Widget.HeightRequest == -1 ? this.Widget.AllocatedHeight : this.Widget.HeightRequest;
            }
            set
            {
                var widget = this.Widget;
                var heightRequest = widget?.HeightRequest ?? 0;
                if (widget != null)
                {
                    widget.HeightRequest = Math.Max(-1, value);
                }

                if (heightRequest != value)
                {
                    Layout?.Invoke(this, new LayoutEventArgs(this, nameof(Height)));
                    Resize?.Invoke(this, EventArgs.Empty);
                    SizeChanged?.Invoke(this, EventArgs.Empty);
                    ClientSizeChanged?.Invoke(this, EventArgs.Empty);
                }

                if (Dock != DockStyle.None)
                {
                    Dock = DockStyle.None;
                }
                AnchorChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        protected int _width;
        public virtual int Width
        {
            get
            {
                var widget = this.Widget;
                if (widget != null && widget.IsMapped == false && widget is Gtk.Window wnd)
                {
                    return wnd.WidthRequest == -1 ? wnd.DefaultWidth : wnd.WidthRequest;
                }
                return widget?.WidthRequest == -1 ? widget.AllocatedWidth : widget?.WidthRequest ?? _width;
            }
            set
            {
                var widget = this.Widget;
                var widthRequest = widget?.WidthRequest ?? 0;
                if (widget != null)
                {
                    widget.WidthRequest = Math.Max(-1, value);
                }
                else
                {
                    _width = value;
                }

                if (widthRequest != value)
                {
                    SizeChanged?.Invoke(this, EventArgs.Empty);
                    Resize?.Invoke(this, EventArgs.Empty);
                }

                DockChanged?.Invoke(this, EventArgs.Empty);
                AnchorChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual int TabIndex
        {
            get => tabIndex;
            set
            {
                var index = tabIndex;
                tabIndex = value;
                if (index != value)
                {
                    TabIndexChanged?.Invoke(this, EventArgs.Empty);
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
                    TabStopChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler? TagChanged;

        public virtual object Tag
        {
            get => tag;
            set
            {
                var o = tag;
                tag = value;
                if (o != value)
                {
                    TagChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public virtual string Text
        {
            get => text;
            set
            {
                var oltText = text;
                text = value;
                if (oltText != value)
                {
                    TextChanged?.Invoke(this, EventArgs.Empty);
                    PropertyChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public virtual Control TopLevelControl { get; }
        public virtual bool UseWaitCursor { get; set; }

        public virtual bool Visible
        {
            get
            {
                return this.Widget.Visible;
            }
            set
            {
                var widget = this.Widget;
                var visible = widget?.Visible ?? true;
                if (widget != null)
                {
                    widget.Visible = value;
                    widget.NoShowAll = value == false;
                }

                if (visible != value)
                {
                    VisibleChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        protected virtual Size DefaultSize { get; }
        protected virtual Padding DefaultPadding { get; }
        protected virtual Size DefaultMinimumSize { get; }
        protected virtual Padding DefaultMargin { get; }
        protected virtual Cursor DefaultCursor { get; }
        protected override bool CanRaiseEvents { get; }
        protected virtual bool DoubleBuffered { get; set; }
        protected int FontHeight { get; set; }
        protected virtual Size DefaultMaximumSize { get; }
        protected virtual ImeMode ImeModeBase { get; set; }
        protected virtual ImeMode DefaultImeMode { get; }
        protected virtual bool CanEnableIme { get; }
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual bool ScaleChildren { get; }
        protected bool ResizeRedraw { get; set; }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected internal virtual bool ShowFocusCues { get; }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected internal virtual bool ShowKeyboardCues { get; }


        public virtual IWindowTarget WindowTarget { get; set; }
        public virtual event EventHandler? AutoSizeChanged;
        public virtual event EventHandler? BackColorChanged;
        public virtual event EventHandler? BackgroundImageChanged;
        public virtual event EventHandler? BackgroundImageLayoutChanged;
        public virtual event EventHandler? BindingContextChanged;
        public virtual event EventHandler? CausesValidationChanged;
        public virtual event UICuesEventHandler? ChangeUICues;
        public virtual event EventHandler? Click;
        public virtual event EventHandler? ClientSizeChanged;
        public virtual event EventHandler? ContextMenuStripChanged;
        public virtual event ControlEventHandler? ControlAdded;
        public virtual event ControlEventHandler? ControlRemoved;
        public virtual event EventHandler? CursorChanged;
        public virtual event EventHandler? DockChanged;
        public virtual event EventHandler? AnchorChanged;
        public virtual event EventHandler? DoubleClick;
        public virtual event EventHandler? DpiChangedAfterParent;
        public virtual event EventHandler? DpiChangedBeforeParent;
        public virtual event DragEventHandler? DragDrop;
        public virtual event DragEventHandler? DragEnter;
        public virtual event EventHandler? DragLeave;
        public virtual event DragEventHandler? DragOver;
        public virtual event EventHandler? EnabledChanged;
        public virtual event EventHandler? Enter;
        public virtual event EventHandler? FontChanged;
        public virtual event EventHandler? ForeColorChanged;
        public virtual event GiveFeedbackEventHandler? GiveFeedback;
        public virtual event EventHandler? GotFocus;
        public virtual event EventHandler? HandleCreated;
        public virtual event EventHandler? HandleDestroyed;
        public virtual event HelpEventHandler? HelpRequested;
        public virtual event EventHandler? ImeModeChanged;
        public virtual event InvalidateEventHandler? Invalidated;
        public virtual event KeyEventHandler? KeyDown;
        public virtual event KeyPressEventHandler? KeyPress;
        public virtual event KeyEventHandler? KeyUp;
        public virtual event LayoutEventHandler? Layout;
        public virtual event EventHandler? Leave;
        public virtual event EventHandler? LocationChanged;
        public virtual event EventHandler? LostFocus;
        public virtual event EventHandler? MarginChanged;
        public virtual event EventHandler? MouseCaptureChanged;
        public virtual event MouseEventHandler? MouseClick;
        public virtual event MouseEventHandler? MouseDoubleClick;
        public virtual event MouseEventHandler? MouseDown;
        public virtual event EventHandler? MouseEnter;
        public virtual event EventHandler? MouseHover;
        public virtual event EventHandler? MouseLeave;
        public virtual event MouseEventHandler? MouseMove;
        public virtual event MouseEventHandler? MouseUp;
        public virtual event MouseEventHandler? MouseWheel;
        public virtual event EventHandler? Move;
        public virtual event EventHandler? PaddingChanged;
        //public virtual event PaintEventHandler Paint;
        public virtual event EventHandler? ParentChanged;
        public virtual event PreviewKeyDownEventHandler? PreviewKeyDown;
        public virtual event QueryAccessibilityHelpEventHandler? QueryAccessibilityHelp;
        public virtual event QueryContinueDragEventHandler? QueryContinueDrag;
        public virtual event EventHandler? RegionChanged;
        public virtual event EventHandler? Resize;
        public virtual event EventHandler? RightToLeftChanged;
        public virtual event EventHandler? SizeChanged;
        public virtual event EventHandler? StyleChanged;
        public virtual event EventHandler? SystemColorsChanged;
        public virtual event EventHandler? TabIndexChanged;
        public virtual event EventHandler? TabStopChanged;
        public virtual event EventHandler? TextChanged;
        public virtual event EventHandler? PropertyChanged;

        CancelEventArgs cancelEventArgs = new CancelEventArgs(false);
        public virtual event EventHandler? Validated;
        public virtual event CancelEventHandler? Validating;
        public virtual event EventHandler? VisibleChanged;
        //public event EventHandler? Disposed;
        public virtual event EventHandler? Load;
        public virtual IAsyncResult BeginInvoke(Delegate method, params object[] args)
        {
            System.Threading.Tasks.Task task = System.Threading.Tasks.Task.Factory.StartNew(state =>
            {
                method.DynamicInvoke((object[])state);
            }, args);

            return task;
        }
        public virtual IAsyncResult BeginInvoke(Delegate method)
        {
            return BeginInvoke(method, null);
        }
        public virtual IAsyncResult BeginInvoke(Action method)
        {
            System.Threading.Tasks.Task task = System.Threading.Tasks.Task.Factory.StartNew(method);
            return task;
        }
        public virtual object EndInvoke(IAsyncResult asyncResult)
        {
            if (asyncResult is System.Threading.Tasks.Task task)
            {
                if (task.IsCompleted == false && task.IsCanceled == false && task.IsFaulted == false)
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
            _handleCreated = true;
            _Created = true;
        }

        Cairo.ImageSurface image;
        Cairo.Surface surface;
        Cairo.Context context;
        public virtual Graphics? CreateGraphics()
        {
            try
            {
                if (image == null)
                    image = new Cairo.ImageSurface(Cairo.Format.Argb32, this.Widget.AllocatedWidth, this.Widget.AllocatedHeight);

                surface?.Dispose();
                surface = image.CreateSimilar(Cairo.Content.ColorAlpha, this.Widget.AllocatedWidth, this.Widget.AllocatedHeight);
                context?.Dispose();
                context = new Cairo.Context(surface);

                var widget = this.Widget as IWidget;
                if (widget != null) return new Drawing.Graphics(widget, context, this.Widget.Allocation);
                return null;
            }
            finally
            {
                _handleCreated = true;
            }
        }

        private void Override_PaintGraphics(Cairo.Context cr, Rectangle rec)
        {
            if (surface != null)
            {
                cr.Save();
                cr.SetSourceSurface(surface, 0, 0);
                cr.Paint();
                cr.Restore();
                this.Widget.QueueDraw();
            }
        }

        public virtual DragDropEffects DoDragDrop(object data, DragDropEffects allowedEffects)
        {
            return DragDropEffects.None;
        }

        public virtual void DrawToBitmap(Bitmap bitmap, Rectangle targetBounds)
        {
        }

        public virtual Form FindForm()
        {
            if (this.Widget.Toplevel?.Data.ContainsKey("Control")??false)
            {
                return this.Widget.Toplevel.Data["Control"] as Form;
            }
            else
            {
                Control control = this.Parent;
                while (control != null)
                {
                    if (control is Form)
                        break;
                    else
                        control = this.Parent;
                }
                return control as Form;
            }
        }

        public virtual bool Focus()
        {
            if (this.Widget != null)
            {
                this.Widget.IsFocus = true;
                return this.Widget.IsFocus;
            }
            else
            {
                return false;
            }
        }

        public virtual Control GetChildAtPoint(Point pt)
        {
            _handleCreated = true;
            return null;
        }

        public virtual Control GetChildAtPoint(Point pt, GetChildAtPointSkip skipValue)
        {
            _handleCreated = true;
            return null;
        }

        public virtual IContainerControl GetContainerControl()
        {
            return this as IContainerControl;
        }

        public virtual Control GetNextControl(Control ctl, bool forward)
        {
            Control prev = null;
            Control next = null;
            bool finded = false;

            foreach (var obj in this.Controls)
            {
                if (obj is Control control)
                {
                    if (finded == true)
                        next = control;

                    if (control.Widget.Handle == ctl.Widget.Handle)
                    {
                        finded = true;
                    }
                    if (finded == true)
                    {
                        if (forward == false && prev != null)
                        {
                            return prev;
                        }
                        else if (forward == true && next != null)
                        {
                            return next;
                        }
                    }
                    prev = control;
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
            if (this.Widget != null && this.Widget.IsVisible)
            {
                this.Widget.Window.InvalidateRect(Widget.Allocation, invalidateChildren);
            }
        }

        public virtual void Invalidate(Rectangle rc)
        {
            Invalidate(rc, true);
        }

        public virtual void Invalidate(Rectangle rc, bool invalidateChildren)
        {
            if (this.Widget != null)
            {
                Self?.Override.OnAddClass();
                this.Widget.Window.InvalidateRect(new Gdk.Rectangle(rc.X, rc.Y, rc.Width, rc.Height), invalidateChildren);
            }
        }

        public virtual void Invalidate(Drawing.Region region)
        {
            Invalidate(region, true);
        }

        public virtual void Invalidate(Drawing.Region region, bool invalidateChildren)
        {
            if (this.Widget != null)
            {
                Self?.Override.OnAddClass();
                this.Widget.Window.InvalidateRect(Widget.Allocation, invalidateChildren);
            }
        }

        public virtual object Invoke(Delegate method)
        {
            return Invoke(method, null);
        }

        public virtual object Invoke(Delegate method, params object[] args)
        {
            object result = null;
            if (!_handleCreated)
            {
                throw new InvalidOperationException();
            }
            GLib.Idle.Add(() =>
            {
                result = method.DynamicInvoke(args);
                return false;
            });
            return result;
        }
        public virtual void Invoke(Action method)
        {
            GLib.Idle.Add(() =>
            {
                method.Invoke();
                return false;
            });
        }
        public virtual ENTRY Invoke<ENTRY>(Func<ENTRY> method)
        {
            ENTRY result = default(ENTRY);
            GLib.Idle.Add(() =>
            {
                result = method.Invoke();
                return false;
            });
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
            int x=0;
            int y=0;
            _handleCreated = true;
            if (Widget != null)
            {
                this.Widget.Window?.GetOrigin(out x, out y);
                if (p.X > x && p.Y > y)
                    return new Point(p.X - x, p.Y - y);
            }
            return new Point(p.X, p.Y);
        }

        public virtual Point PointToScreen(Point p)
        {
            int x = 0;
            int y = 0;
            _handleCreated = true;
            if (Widget != null)
            {
                this.Widget.Window?.GetOrigin(out x, out y);
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
            int x = 0;
            int y = 0;
            _handleCreated = true;
            if (Widget != null)
            {
                this.Widget.Window?.GetPosition(out x, out y);
                if (r.X > x && r.Y > y)
                    return new Rectangle(r.X - x, r.Y - y, r.Width, r.Height);
            }
            return new Rectangle(r.X, r.Y, r.Width, r.Height);
        }

        public virtual Rectangle RectangleToScreen(Rectangle r)
        {
            int x = 0;
            int y = 0;
            _handleCreated = true;
            if (Widget != null)
            {
                this.Widget.Window?.GetPosition(out x, out y);
                if (r.X < x && r.Y < y)
                    return new Rectangle(r.X + x, r.Y + y, r.Width, r.Height);
            }
            return new Rectangle(r.X, r.Y, r.Width, r.Height);
        }

        public virtual void Refresh()
        {
            if (this.Widget != null && this.Widget.IsVisible)
            {
                Self?.Override.ClearNativeBackground();
                this.Widget.QueueDraw();
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
            _Created = true;
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
            this.Widget?.SetStateFlags(StateFlags.Selected, true);
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
            if (this.Widget != null)
            {
                Gdk.Rectangle rect = this.Widget.Clip;
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
                this.Widget.SetClip(rect);
            }
        }
        public virtual Rectangle ClientRectangle { get { this.Widget.GetAllocatedSize(out Gdk.Rectangle allocation, out int baseline); return new Rectangle(allocation.X, allocation.Y, allocation.Width, allocation.Height); } }

        public virtual Size ClientSize
        {
            get
            {
                return new Size(Widget.AllocatedWidth, this.Widget.AllocatedHeight);
            }
            set
            {
                var size = new Size(Widget.AllocatedWidth, this.Widget.AllocatedHeight);
                this.Widget.SetSizeRequest(value.Width, value.Height);
                if (size != value)
                {
                    Layout?.Invoke(this, new LayoutEventArgs(this, nameof(ClientSize)));
                    Resize?.Invoke(this, EventArgs.Empty);
                    SizeChanged?.Invoke(this, EventArgs.Empty);
                }
                ClientSizeChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public virtual IntPtr Handle
        {
            get
            {
                OnHandleCreated(EventArgs.Empty);
                return this.Widget.Handle;
            }
            set => OnHandleCreated(EventArgs.Empty);
        }


        protected virtual void OnHandleCreated(EventArgs eventArgs)
        {
            if (!_handleCreated)
            {
                _handleCreated = true;
                HandleCreated?.Invoke(this, eventArgs);
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
                    MarginChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public virtual Size MaximumSize { get; set; }
        public virtual Size MinimumSize { get; set; }
        private BorderStyle _BorderStyle;
        private BindingContext bindingContext;
        private string text = string.Empty;
        private string _name;
        private bool causesValidation = true;
        private Cursor cursor;
        private bool tabStop = true;
        private object tag;
        private int tabIndex;
        private RightToLeft rightToLeft;
        private ImeMode imeMode;
        private bool _autoSize;
        private bool _capture;
        private ContextMenuStrip _contextMenuStrip;
        private AccessibleObject accessibilityObject;
        private bool _handleCreated;
        private Padding margin;

        public virtual BorderStyle BorderStyle
        {
            get { return _BorderStyle; }
            set
            {
                _BorderStyle = value;
                if (value == BorderStyle.FixedSingle)
                {
                    this.Widget.StyleContext.AddClass("BorderFixedSingle");
                }
                else if (value == BorderStyle.Fixed3D)
                {
                    this.Widget.StyleContext.AddClass("BorderFixed3D");
                }
                else
                {
                    this.Widget.StyleContext.AddClass("BorderNone");
                }
            }
        }

        public virtual void Hide()
        {
            if (this.GtkControl is Misc con)
            {
                con.Hide();
            }
        }

        public virtual void Show()
        {
            this.Widget?.ShowAll();
        }
        protected virtual void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
        }
        protected virtual void OnParentChanged(EventArgs e)
        {
        }

        public virtual void SuspendLayout()
        {
            _Created = false;
        }

        public virtual void PerformLayout()
        {
            _Created = true;
        }

        public virtual void PerformLayout(Control affectedControl, string affectedProperty)
        {
            _Created = true;
        }

        public virtual void Update()
        {
            if (this.Widget != null)
            {
                this.Widget.Window?.ProcessUpdates(true);
                this.Widget.QueueDraw();
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

        public new virtual event EventHandler? Disposed;

        public new virtual void Dispose()
        {
            Dispose(true);
            base.Dispose();
            _handleCreated = false;
            Disposed?.Invoke(this, EventArgs.Empty);
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

                if (this.Widget != null)
                {
                    this.Widget.Destroy();
                    this.GtkControl = null;
                }
            }
            catch (Exception ex) { Trace.WriteLine(ex); }
            base.Dispose(disposing);
            HandleDestroyed?.Invoke(this, EventArgs.Empty);
        }

        protected virtual CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = new CreateParams();
                createParams.ExStyle |= 32;
                return createParams;
            }
        }

        public bool ParticipatesInLayout => throw new NotImplementedException();

        PropertyStore IArrangedElement.Properties => throw new NotImplementedException();

        IArrangedElement IArrangedElement.Container => throw new NotImplementedException();

        public ArrangedElementCollection Children => throw new NotImplementedException();

        protected virtual void OnKeyDown(KeyEventArgs e)
        {

        }
        protected virtual void OnKeyUp(KeyEventArgs e)
        {

        }
        protected virtual void OnVisibleChanged(EventArgs e)
        {

        }
        protected virtual void OnSizeChanged(EventArgs e)
        {

        }
        protected virtual void Select(bool directed, bool forward)
        {

        }
        protected virtual void OnGotFocus(EventArgs e)
        {

        }
        protected virtual void WndProc(ref Message m)
        {
            //Console.WriteLine($"HWnd:{m.HWnd},WParam:{m.WParam},LParam:{m.LParam},Msg:{m.Msg}");
        }

        public void SetBounds(Rectangle bounds, BoundsSpecified specified)
        {
        }

        void IArrangedElement.PerformLayout(IArrangedElement affectedElement, string propertyName)
        {
        }
    }
}
