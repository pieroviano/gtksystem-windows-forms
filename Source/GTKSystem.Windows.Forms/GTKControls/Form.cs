﻿/*
 * 基于GTK组件开发，兼容原生C#控件winform界面的跨平台界面组件。
 * 使用本组件GTKSystem.Windows.Forms代替Microsoft.WindowsDesktop.App.WindowsForms，一次编译，跨平台windows、linux、macos运行
 * 技术支持438865652@qq.com，https://gitee.com/easywebfactory, https://github.com/easywebfactory, https://www.cnblogs.com/easywebfactory
 * author:chenhongjin
 */

using Gtk;
using GTKSystem.Windows.Forms.GTKControls.ControlBase;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace System.Windows.Forms
{
    [DesignerCategory("Form")]
    [DefaultEvent(nameof(Load)),
    InitializationEvent(nameof(Load))]
    public partial class Form: ScrollableControl, IWin32Window
    {
        private Gtk.Application app = Application.Init();
        public FormBase self = new FormBase();
        public override object GtkControl { get => self; }
        private Gtk.Fixed _body = new Gtk.Fixed();
        private ObjectCollection _ObjectCollection;
        public override event EventHandler SizeChanged;

        public Form() : base()
        {
            Init();
        }
        public Form(string title) : this()
        {
            self.Title = title;
        }

        public Form(string title, Window parent) : base()
        {
            self.Title = title;
            Init();
        }
        private void Init()
        {
            _body.Valign = Gtk.Align.Fill;
            _body.Halign = Gtk.Align.Fill;
            _body.Expand = true;
            _body.Hexpand = true;
            _body.Vexpand = true;
            self.ScrollView.Child = _body;
            _ObjectCollection = new ObjectCollection(this, _body);

            self.ButtonReleaseEvent += Body_ButtonReleaseEvent;
            self.ResizeChecked += Form_ResizeChecked;
            self.Shown += Control_Shown;
            self.DeleteEvent += Self_DeleteEvent;
        }

        private void Self_DeleteEvent(object o, DeleteEventArgs args)
        {
            FormClosingEventArgs closing = new FormClosingEventArgs(CloseReason.UserClosing, false);
            if (FormClosing != null)
                FormClosing(this, closing);

            args.RetVal = closing.Cancel == true;
            if (closing.Cancel == false)
            {
                if (FormClosed != null)
                    FormClosed(this, new FormClosedEventArgs(CloseReason.UserClosing));
            }
        }

        private void Control_Shown(object sender, EventArgs e)
        {
            if (Shown != null)
                Shown(this, e);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void MenuPositionFuncNative(IntPtr menu, out int x, out int y, out bool push_in, IntPtr user_data);
        static MenuPositionFuncNative StatusIconPositionMenuFunc = null;
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void d_gtk_menu_popup(IntPtr menu, IntPtr parent_menu_shell, IntPtr parent_menu_item, MenuPositionFuncNative func, IntPtr data, uint button, uint activate_time);
        private static d_gtk_menu_popup gtk_menu_popup = FuncLoader.LoadFunction<d_gtk_menu_popup>(FuncLoader.GetProcAddress(GLibrary.Load(Library.Gtk), "gtk_menu_popup"));
        public void PresentMenu(Gtk.Menu menu, uint button, uint activate_time)
        {
            gtk_menu_popup(menu == null ? IntPtr.Zero : menu.Handle, IntPtr.Zero, IntPtr.Zero, StatusIconPositionMenuFunc, self.Handle, button, activate_time);
        }
        private void Body_ButtonReleaseEvent(object o, ButtonReleaseEventArgs args)
        {
            if (base.ContextMenuStrip != null)
            {
                base.ContextMenuStrip.Widget.ShowAll();
                if (args.Event.Button == 3)
                    PresentMenu((Gtk.Menu)base.ContextMenuStrip.Widget, args.Event.Button, args.Event.Time);
            }
        }
        int resizeWidth= 0;
        int resizeHeight= 0;
        private void Form_ResizeChecked(object sender, EventArgs e)
        {
            if (self.Resizable == true && _body.IsMapped && self.IsMapped)
            {
                if (resizeWidth < 1)
                {
                    resizeWidth = self.ContentArea.AllocatedWidth;
                    resizeHeight = self.ContentArea.AllocatedHeight;
                }
                else if (resizeWidth != self.ContentArea.AllocatedWidth || resizeHeight != self.ContentArea.AllocatedHeight)
                {
                    try
                    {
                        int widthIncrement = self.ContentArea.AllocatedWidth - resizeWidth;
                        int heightIncrement = self.ContentArea.AllocatedHeight - resizeHeight;
                        if (widthIncrement != 0 || heightIncrement != 0)
                        {
                            resizeWidth = self.ContentArea.AllocatedWidth;
                            resizeHeight = self.ContentArea.AllocatedHeight;
                            _body.WidthRequest = resizeWidth - (AutoScroll ? self.ScrollArrowVlength : 0); //留出滚动条位置
                            _body.HeightRequest = resizeHeight - self.StatusBarView.AllocatedHeight - (AutoScroll ? self.ScrollArrowHlength : 0);
                            ResizeControls(widthIncrement, heightIncrement, _body);
                        }
                    }
                    catch
                    {

                    }
                    if (SizeChanged != null)
                        SizeChanged(this, e);
                }

            }

        }

        private void ResizeControls(int widthIncrement, int heightIncrement, Gtk.Container parent)
        {
            foreach (Gtk.Widget control in parent.Children)
            {
                if (control != null)
                {
                    bool checksizing = false;
                    if (control.Data["Control"] != null)
                    {
                        Control con = (Control)control.Data["Control"];
                        DockStyle dock = con.Dock;
                        if(dock == DockStyle.None)
                        {
                            AnchorStyles anchor = con.Anchor;
                            if (anchor != AnchorStyles.None)
                            {
                                checksizing = true;
                                if ((anchor & AnchorStyles.Right) != 0)
                                {
                                    if ((anchor & AnchorStyles.Left) != 0)
                                    {
                                        control.WidthRequest = control.WidthRequest + widthIncrement;
                                    }
                                    else
                                    {
                                        if (parent[control] is Gtk.Layout.LayoutChild lc)
                                        {
                                            lc.X = lc.X + widthIncrement;
                                        }
                                        else if (parent[control] is Gtk.Fixed.FixedChild fc)
                                        {
                                            fc.X = fc.X + widthIncrement;
                                        }
                                    }
                                }
                                if ((anchor & AnchorStyles.Bottom) != 0)
                                {
                                    if ((anchor & AnchorStyles.Top) != 0)
                                    {
                                        control.HeightRequest = control.HeightRequest + heightIncrement;
                                    }
                                    else
                                    {
                                        if (parent[control] is Gtk.Layout.LayoutChild lc)
                                        {
                                            lc.Y = lc.Y + heightIncrement;
                                        }
                                        else if (parent[control] is Gtk.Fixed.FixedChild fc)
                                        {
                                            fc.Y = fc.Y + heightIncrement;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            checksizing = true;
                            string dockStyle = dock.ToString();
                            if (dockStyle == DockStyle.Top.ToString())
                            {
                                control.Valign = Gtk.Align.Start;
                                control.Hexpand = true;
                                if (control.WidthRequest > -1)
                                    control.WidthRequest = control.WidthRequest + widthIncrement;
                            }
                            else if (dockStyle == DockStyle.Bottom.ToString())
                            {
                                control.Valign = Gtk.Align.End;
                                control.Halign = Gtk.Align.Fill;
                                control.Expand = true;
                                if (parent[control] is Gtk.Layout.LayoutChild lc)
                                {
                                    lc.Y = parent.HeightRequest - control.HeightRequest;
                                }
                                else if (parent[control] is Gtk.Fixed.FixedChild fc)
                                {
                                    fc.Y = parent.HeightRequest - control.HeightRequest;
                                }
                                if (control.WidthRequest > -1)
                                    control.WidthRequest = control.WidthRequest + widthIncrement;
                            }
                            else if (dockStyle == DockStyle.Left.ToString())
                            {
                                control.Halign = Gtk.Align.Start;
                                control.Vexpand = true;
                                if (control.HeightRequest > -1)
                                    control.HeightRequest = control.HeightRequest + heightIncrement;
                            }
                            else if (dockStyle == DockStyle.Right.ToString())
                            {
                                control.Halign = Gtk.Align.End;
                                if (parent[control] is Gtk.Layout.LayoutChild lc)
                                {
                                    lc.X = parent.WidthRequest - control.WidthRequest - 6;
                                }
                                else if (parent[control] is Gtk.Fixed.FixedChild fc)
                                {
                                    fc.X = parent.WidthRequest - control.WidthRequest - 6;
                                }
                                if (control.HeightRequest > -1)
                                    control.HeightRequest = control.HeightRequest + heightIncrement;
                            }
                            else if (dockStyle == DockStyle.Fill.ToString())
                            {
                                control.Hexpand = true;
                                control.Vexpand = true;
                                if (control.HeightRequest > -1)
                                    control.HeightRequest = control.HeightRequest + heightIncrement;
                                if (control.WidthRequest > -1)
                                    control.WidthRequest = control.WidthRequest + widthIncrement;
                            }
                        }
                    }
                    if (control is Gtk.MenuBar menuba)
                    {
                        //菜单不用处理
                    }
                    else if (control is Gtk.TreeView)
                    {
                        //树目录不用处理
                    }
                    else if(checksizing || control is Gtk.Viewport || control is Gtk.ScrolledWindow || control is Gtk.Fixed || control is Gtk.Layout || control is Gtk.Box)
                    {
                        ResizeControls(widthIncrement, heightIncrement, (Gtk.Container)control);
                    }
                }
            }
        }

        private void OnLoad()
        {
            if (Load != null)
                Load(this, new EventArgs());
        }

        public override void Show()
        {
            this.Show(null);
        }
        public void Show(IWin32Window owner)
        {
            if (owner == this)
            {
                throw new InvalidOperationException("OwnsSelfOrOwner");
            }

            if (base.Visible)
            {
                throw new InvalidOperationException("ShowDialogOnVisible");
            }

            if (!base.Enabled)
            {
                throw new InvalidOperationException("ShowDialogOnDisabled");
            }

            if (owner != null && owner is Form parent)
            {
                this.Parent = parent;
            }
            if (self.IsVisible == false)
            {
                if (AutoScroll == true)
                {
                    self.ScrollView.HscrollbarPolicy = PolicyType.Always;
                    self.ScrollView.VscrollbarPolicy = PolicyType.Always;
                }
                else
                {
                    self.ScrollView.HscrollbarPolicy = PolicyType.External;
                    self.ScrollView.VscrollbarPolicy = PolicyType.External;
                }

                this.FormBorderStyle = this.FormBorderStyle;
                if (this.MaximizeBox == false && this.MinimizeBox == false)
                {
                    self.TypeHint = Gdk.WindowTypeHint.Dialog;
                }
                else if (this.MaximizeBox == false && this.MinimizeBox == true)
                {
                    self.Resizable = false;
                }

                if (self.Resizable == false)
                {
                    self.WidthRequest = self.DefaultSize.Width;
                    self.HeightRequest = self.DefaultSize.Height;
                }

                if (this.WindowState == FormWindowState.Maximized)
                {
                    self.Maximize();
                }
                else if (this.WindowState == FormWindowState.Minimized)
                {
                    self.Iconify();
                }

                try
                {
                    if (this.ShowIcon)
                    {
                        if (this.Icon != null)
                        {
                            if (this.Icon.Pixbuf != null)
                                self.Icon = this.Icon.Pixbuf;
                            else if (this.Icon.PixbufData != null)
                                self.Icon = new Gdk.Pixbuf(this.Icon.PixbufData);
                            else if (this.Icon.FileName != null && System.IO.File.Exists(this.Icon.FileName))
                                self.SetIconFromFile(this.Icon.FileName);
                            else if (this.Icon.FileName != null && System.IO.File.Exists("Resources\\" + this.Icon.FileName))
                                self.SetIconFromFile("Resources\\" + this.Icon.FileName);
                        }
                    }
                    else
                    {
                        System.IO.Stream sm = typeof(System.Windows.Forms.Form).Assembly.GetManifestResourceStream("GTKSystem.Windows.Forms.Resources.System.view-more.png");
                        self.Icon = new Gdk.Pixbuf(sm);
                    }
                }
                catch
                {

                }

                OnLoad();
            }

            self.ShowAll();
        }

        public DialogResult ShowDialog()
        {
            return ShowDialog(null);
        }
        public DialogResult ShowDialog(IWin32Window owner)
        {
            if (owner == this)
            {
                throw new ArgumentException("OwnsSelfOrOwner", "showDialog");
            }

            if (base.Visible)
            {
                throw new InvalidOperationException("ShowDialogOnVisible");
            }

            if (!base.Enabled)
            {
                throw new InvalidOperationException("ShowDialogOnDisabled");
            }
            Show(owner);
            int irun = self.Run();

            return this.DialogResult;
        }

        public event EventHandler Shown;
        public event FormClosingEventHandler FormClosing;
        public event FormClosedEventHandler FormClosed;
        public override event EventHandler Load;
        public override string Text { get { return self.Title; } set { self.Title = value; } }
        public override Size ClientSize
        {
            get
            {
                return new Size(self.WidthRequest, self.HeightRequest);
            }
            set
            {
                self.WidthRequest = 100;
                self.HeightRequest = 60;
                self.SetDefaultSize(value.Width, value.Height);
            }
        }
        public SizeF AutoScaleDimensions { get; set; }
        public AutoScaleMode AutoScaleMode { get; set; }
        public FormBorderStyle formBorderStyle = FormBorderStyle.Sizable;
        public FormBorderStyle FormBorderStyle
        {
            get { return formBorderStyle; }
            set {
                formBorderStyle = value;
                self.Resizable = value == FormBorderStyle.Sizable || value == FormBorderStyle.SizableToolWindow;
                if (value == FormBorderStyle.None)
                {
                    self.Decorated = false; //删除工具栏
                }
                else if (value == FormBorderStyle.FixedToolWindow)
                {
                    self.Decorated = true;
                    self.TypeHint = Gdk.WindowTypeHint.Dialog;
                }
                else if (value == FormBorderStyle.SizableToolWindow)
                {
                    self.Decorated = true;
                    self.TypeHint = Gdk.WindowTypeHint.Dialog;
                }
                else
                {
                    self.Decorated = true;
                    self.TypeHint = Gdk.WindowTypeHint.Normal;
                }
            }
        }
        public FormWindowState WindowState { get; set; } = FormWindowState.Normal;
        public DialogResult DialogResult { get; set; }
        public void Close() {
            if (self != null)
            {
                self.CloseWindow();
                self.Dispose();
            }
        }
        public override void Hide()
        {
            if (self != null)
            {
                self.Hide();
            }
        }

        public new ObjectCollection Controls { get { return _ObjectCollection; } }

        public bool MaximizeBox { get; set; } = true;
        public bool MinimizeBox { get; set; } = true;
        public double Opacity { get { return self.Opacity; } set { self.Opacity = value; } }
        public bool ShowIcon { get; set; } = true;
        public bool ShowInTaskbar { get { return self.SkipTaskbarHint == false; } set { self.SkipTaskbarHint = value == false; } }
        public System.Drawing.Icon Icon { get; set; }
        public override void SuspendLayout()
        {
            _Created = false;
        }
        public override void ResumeLayout(bool resume)
        {
            _Created = resume == false;
        }

        public override void PerformLayout()
        {
            _Created = true;
        }

        public bool ActivateControl(object active)
        {
            if (active is Gtk.Widget wg)
            {
                wg.SetStateFlags(StateFlags.Active, false);
                return true;
            }
            return false;
        }

        public MenuStrip MainMenuStrip { get; set; }

        public override IntPtr Handle => self.Handle;

        public class ObjectCollection : ControlCollection
        {
            Gtk.Container __owner;
            public ObjectCollection(Control control, Gtk.Container owner) : base(control, owner)
            {
                __owner = owner;
            }

        }

        public class MdiLayout
        {
        }
    }

    public class BindingContext : ContextBoundObject
    {
    }
}

