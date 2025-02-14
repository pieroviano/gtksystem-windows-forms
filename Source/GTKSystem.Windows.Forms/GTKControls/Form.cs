/*
 * 基于GTK组件开发，兼容原生C#控件winform界面的跨平台界面组件。
 * 使用本组件GTKSystem.Windows.Forms代替Microsoft.WindowsDesktop.App.WindowsForms，一次编译，跨平台windows、linux、macos运行
 * 技术支持438865652@qq.com，https://www.gtkapp.com, https://gitee.com/easywebfactory, https://github.com/easywebfactory
 */

using Gtk;
using GTKSystem.Windows.Forms.GTKControls.ControlBase;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms
{
    [DesignerCategory("Form")]
    [DefaultEvent(nameof(Load)),
    InitializationEvent(nameof(Load))]
    public partial class Form : ContainerControl, IWin32Window
    {
        private Gtk.Application app = Application.Init();
        public FormBase self = new FormBase();
        public override object GtkControl { get => self; }
        private Gtk.Overlay contanter = new Gtk.Overlay();
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
        private void Init()
        {
            this.SetScrolledWindow(self);
            contanter.Valign = Gtk.Align.Fill;
            contanter.Halign = Gtk.Align.Fill;
            contanter.Hexpand = true;
            contanter.Vexpand = true;
            contanter.MarginBottom = 0;
            contanter.MarginEnd = 0;
            contanter.Add(new Gtk.Fixed() { Halign = Align.Fill, Valign = Align.Fill });
            self.ScrollView.Child = contanter;
            _ObjectCollection = new ObjectCollection(this, contanter);
            self.ResizeChecked += Self_ResizeChecked;
            self.Shown += Control_Shown;
            self.CloseWindowEvent += Self_CloseWindowEvent;
        }

        private void Self_ResizeChecked(object sender, EventArgs e)
        {
            if (SizeChanged != null)
                SizeChanged(this, EventArgs.Empty);
        }

        private bool Self_CloseWindowEvent(object sender, EventArgs e)
        {
            FormClosingEventArgs closing = new FormClosingEventArgs(CloseReason.UserClosing, false);
            if (FormClosing != null)
                FormClosing(this, closing);

            if (closing.Cancel == false)
            {
                if (FormClosed != null)
                    FormClosed(this, new FormClosedEventArgs(CloseReason.UserClosing));
            }
            return closing.Cancel == false;
        }
        private bool Is_Control_Shown = false;
        private void Control_Shown(object sender, EventArgs e)
        {
            if (Is_Control_Shown == false)
            {
                Is_Control_Shown = true;
                if (self.Titlebar is Gtk.HeaderBar titlebar)
                {
                    titlebar.DecorationLayout = "menu:close";
                    if (formBorderStyle == FormBorderStyle.FixedToolWindow || formBorderStyle == FormBorderStyle.SizableToolWindow)
                    {
                    }
                    else
                    {
                        if (MaximizeBox == true)
                        {
                            Gtk.Button maximize = new Gtk.Button("window-maximize-symbolic", IconSize.SmallToolbar) { Name = "maximize", Visible = true, Relief = ReliefStyle.None, Valign = Align.Center, Halign = Align.Center };
                            maximize.StyleContext.AddClass("maximize");
                            maximize.StyleContext.AddClass("titlebutton");
                            maximize.Clicked += Maximize_Clicked;
                            titlebar.PackEnd(maximize);
                        }
                        if (MinimizeBox == true)
                        {
                            Gtk.Button minimize = new Gtk.Button("window-minimize-symbolic", IconSize.SmallToolbar) { Name = "minimize", Visible = true, Relief = ReliefStyle.None, Valign = Align.Center, Halign = Align.Center };
                            minimize.StyleContext.AddClass("minimize");
                            minimize.StyleContext.AddClass("titlebutton");
                            minimize.Clicked += Minimize_Clicked;
                            titlebar.PackEnd(minimize);
                        }
                    }
                }
                OnLoadHandler();
            }
            OnShownHandler();
        }

        private void Close_Clicked(object sender, EventArgs e)
        {
            self.CloseWindow();
        }

        private void Maximize_Clicked(object sender, EventArgs e)
        {
            Gtk.Button maximize = (Gtk.Button)sender;
            if (maximize.Name == "restore")
            {
                self.Unmaximize();
                maximize.Image = Gtk.Image.NewFromIconName("window-maximize-symbolic", IconSize.SmallToolbar);
                maximize.Name = "maximize";
            }
            else
            {
                self.Maximize();
                maximize.Image = Gtk.Image.NewFromIconName("window-restore-symbolic", IconSize.SmallToolbar);
                maximize.Name = "restore";
            }
        }

        private void Minimize_Clicked(object sender, EventArgs e)
        {
            self.Iconify();
        }

        public override event ScrollEventHandler Scroll
        {
            add { self.Scroll += value; }
            remove { self.Scroll += value; }
        }

        private void OnLoadHandler()
        {
            var e = EventArgs.Empty;
            OnLoad(e);
        }

        private void OnShownHandler()
        {
            var e = EventArgs.Empty;
            OnShown(e);
        }

        protected internal virtual void OnShown(EventArgs e)
        {
            Shown?.Invoke(this, e);
            if (!BindingContextSet)
            {
                OnBindingContextChanged(e);
            }
            foreach (Control control in Controls)
            {
                control.OnLoad(e);
            }
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
                self.SetPosition(WindowPosition.CenterOnParent);
                self.Activate();
            }

            if (self.IsVisible == false)
            {
                if (AutoScroll == true)
                {
                    self.ScrollView.HscrollbarPolicy = PolicyType.Automatic;
                    self.ScrollView.VscrollbarPolicy = PolicyType.Automatic;
                }
                else
                {
                    self.ScrollView.HscrollbarPolicy = PolicyType.Never;
                    self.ScrollView.VscrollbarPolicy = PolicyType.Never;
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
                self.Resize(self.DefaultWidth, self.DefaultHeight);

                if (this.WindowState == FormWindowState.Maximized)
                {
                    self.Maximize();
                }
                else if (this.WindowState == FormWindowState.Minimized)
                {
                    self.Iconify();
                }
                if (self.IsMapped == false)
                {
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
                            Gtk.HeaderBar titlebar = (Gtk.HeaderBar)self.Titlebar;
                            Gtk.Image flag = new Gtk.Image(self.Icon);
                            flag.Visible = true;
                            titlebar.PackStart(flag);
                        }
                        else
                        {
                            self.Icon = new Gdk.Pixbuf(this.GetType().Assembly, "GTKSystem.Windows.Forms.Resources.System.view-more.png");
                        }

                    }
                    catch
                    {

                    }
                }
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
                return new Size(self.AllocatedWidth, self.AllocatedHeight);
            }
            set
            {
                self.WidthRequest = -1;
                self.HeightRequest = -1;
                self.SetDefaultSize(value.Width, value.Height);
            }
        }
        public SizeF AutoScaleDimensions { get; set; }
        public AutoScaleMode AutoScaleMode { get; set; }
        public FormBorderStyle formBorderStyle = FormBorderStyle.Sizable;
        public FormBorderStyle FormBorderStyle
        {
            get { return formBorderStyle; }
            set
            {
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
        public FormStartPosition StartPosition { get; set; }
        private FormWindowState _WindowState = FormWindowState.Normal;
        public FormWindowState WindowState
        {
            get
            {
                return _WindowState;
            }
            set
            {
                _WindowState = value;
                if (self.IsMapped)
                {
                    if (value == FormWindowState.Maximized)
                    {
                        self.Maximize();
                    }
                    else if (value == FormWindowState.Minimized)
                    {
                        self.Iconify();
                    }
                }
            }
        }
        public DialogResult DialogResult { get; set; }
        public void Close()
        {
            if (self != null)
            {
                self.CloseWindow();
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
        public override Padding Padding
        {
            get => base.Padding;
            set
            {
                base.Padding = value;
                contanter.MarginStart = value.Left;
                contanter.MarginTop = value.Top;
                contanter.MarginEnd = value.Right;
                contanter.MarginBottom = value.Bottom;
            }
        }
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
        public bool Activate()
        {
            return self.Activate();
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
        internal class HashKey
        {
            private WeakReference wRef;

            private int dataSourceHashCode;

            private string dataMember;

            internal HashKey(object dataSource, string dataMember)
            {
                if (dataSource == null)
                {
                    throw new ArgumentNullException("dataSource");
                }
                if (dataMember == null)
                {
                    dataMember = "";
                }
                this.wRef = new WeakReference(dataSource, false);
                this.dataSourceHashCode = dataSource.GetHashCode();
                this.dataMember = dataMember.ToLower(CultureInfo.InvariantCulture);
            }

            public override bool Equals(object target)
            {
                if (!(target is BindingContext.HashKey))
                {
                    return false;
                }
                BindingContext.HashKey hashKey = (BindingContext.HashKey)target;
                if (this.wRef.Target != hashKey.wRef.Target)
                {
                    return false;
                }
                return this.dataMember == hashKey.dataMember;
            }

            public override int GetHashCode()
            {
                return this.dataSourceHashCode * this.dataMember.GetHashCode();
            }
        }


        public static void UpdateBinding(BindingContext newBindingContext, Binding binding)
        {
            BindingManagerBase bindingManagerBase = binding.BindingManagerBase;
            if (bindingManagerBase != null)
            {
                bindingManagerBase.Bindings.Remove(binding);
            }
            if (newBindingContext != null)
            {
                if (binding.BindToObject.BindingManagerBase is PropertyManager)
                {
                    BindingContext.CheckPropertyBindingCycles(newBindingContext, binding);
                }
                BindToObject bindToObject = binding.BindToObject;
                BindingManagerBase bindingManagerBase1 = newBindingContext.EnsureListManager(bindToObject.DataSource, bindToObject.BindingMemberInfo.BindingPath);
                bindingManagerBase1.Bindings.Add(binding);
            }
        }

        internal BindingManagerBase EnsureListManager(object dataSource, string dataMember)
        {
            BindingManagerBase relatedCurrencyManager = null;
            if (dataMember == null)
            {
                dataMember = "";
            }
            if (dataSource is ICurrencyManagerProvider)
            {
                relatedCurrencyManager = (dataSource as ICurrencyManagerProvider).GetRelatedCurrencyManager(dataMember);
                if (relatedCurrencyManager != null)
                {
                    return relatedCurrencyManager;
                }
            }
            BindingContext.HashKey key = this.GetKey(dataSource, dataMember);
            WeakReference item = this.listManagers[key] as WeakReference;
            if (item != null)
            {
                relatedCurrencyManager = (BindingManagerBase)item.Target;
            }
            if (relatedCurrencyManager != null)
            {
                return relatedCurrencyManager;
            }
            if (dataMember.Length != 0)
            {
                int num = dataMember.LastIndexOf(".");
                string str = (num == -1 ? "" : dataMember.Substring(0, num));
                string str1 = dataMember.Substring(num + 1);
                BindingManagerBase bindingManagerBase = this.EnsureListManager(dataSource, str);
                PropertyDescriptor propertyDescriptor = bindingManagerBase.GetItemProperties().Find(str1, true);
                if (propertyDescriptor == null)
                {
                    throw new ArgumentException("RelatedListManagerChild");
                }
                if (!typeof(IList).IsAssignableFrom(propertyDescriptor.PropertyType))
                {
                    relatedCurrencyManager = new RelatedPropertyManager(bindingManagerBase, str1);
                }
                else
                {
                    relatedCurrencyManager = new RelatedCurrencyManager(bindingManagerBase, str1);
                }
            }
            else if (dataSource is IList || dataSource is IListSource)
            {
                relatedCurrencyManager = new CurrencyManager(dataSource);
            }
            else
            {
                relatedCurrencyManager = new PropertyManager(dataSource);
            }
            if (item != null)
            {
                item.Target = relatedCurrencyManager;
            }
            else
            {
                this.listManagers.Add(key, new WeakReference(relatedCurrencyManager, false));
            }
            this.ScrubWeakRefs();
            return relatedCurrencyManager;
        }

        private void ScrubWeakRefs()
        {
            ArrayList arrayLists = null;
            foreach (DictionaryEntry listManager in this.listManagers)
            {
                if (((WeakReference)listManager.Value).Target != null)
                {
                    continue;
                }
                if (arrayLists == null)
                {
                    arrayLists = new ArrayList();
                }
                arrayLists.Add(listManager.Key);
            }
            if (arrayLists != null)
            {
                foreach (object arrayList in arrayLists)
                {
                    this.listManagers.Remove(arrayList);
                }
            }
        }

        private Hashtable listManagers= new Hashtable();

        public bool Contains(object dataSource, string dataMember)
        {
            return this.listManagers.ContainsKey(this.GetKey(dataSource, dataMember));
        }

        internal BindingContext.HashKey GetKey(object dataSource, string dataMember)
        {
            return new BindingContext.HashKey(dataSource, dataMember);
        }


        private static void CheckPropertyBindingCycles(BindingContext newBindingContext, Binding propBinding)
        {
            if (newBindingContext == null || propBinding == null)
            {
                return;
            }
            if (newBindingContext.Contains(propBinding.BindableComponent, ""))
            {
                BindingManagerBase bindingManagerBase = newBindingContext.EnsureListManager(propBinding.BindableComponent, "");
                for (int i = 0; i < bindingManagerBase.Bindings.Count; i++)
                {
                    Binding item = bindingManagerBase.Bindings[i];
                    if (item.DataSource == propBinding.BindableComponent)
                    {
                        if (propBinding.BindToObject.BindingMemberInfo.BindingMember.Equals(item.PropertyName))
                        {
                            throw new ArgumentException("DataBindingCycle", "propBinding");
                        }
                    }
                    else if (propBinding.BindToObject.BindingManagerBase is PropertyManager)
                    {
                        BindingContext.CheckPropertyBindingCycles(newBindingContext, item);
                    }
                }
            }
        }

    }
}

