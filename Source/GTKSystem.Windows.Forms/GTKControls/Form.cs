/*
 * A cross-platform interface component developed based on GTK components and compatible with the native C# control winform interface.
 * Use this component GTKSystem.Windows.Forms instead of Microsoft.WindowsDesktop.App.WindowsForms, compile once, run across platforms windows, linux, macos
 * Technical support 438865652@qq.com, https://www.gtkapp.com, https://gitee.com/easywebfactory, https://github.com/easywebfactory
 * author: chenhongjin
 */

using System.Collections;
using Gtk;
using System.ComponentModel;
using System.Diagnostics;
using Icon = System.Drawing.Icon;
using SdSystemColors = System.Drawing.
#if NET462_OR_GREATER
                                      SystemColors;
#else
                                      GtkSystemColors;
#endif

using SdSize = System.Drawing.Size;
using SdSizeF = System.Drawing.SizeF;
using GtkApplication = System.Windows.Forms.Application;
using System.Threading.Tasks;

namespace System.Windows.Forms;
using SdColor = Drawing.Color;

[DesignerCategory("Form")]
[DefaultEvent(nameof(Load)), InitializationEvent(nameof(Load))]
public partial class Form : ContainerControl, IWin32Window
{
    public FormBase self;

    internal FormBorderStyle formBorderStyle = FormBorderStyle.Sizable;
    private bool isControlShown;
    private FormWindowState windowState = FormWindowState.Normal;
    private ObjectCollection objectCollection;

    public override object GtkControl => self;

    private readonly Overlay contanter;
    [ThreadStatic]
    private static bool threadInitialized;

    public static bool ThreadInitialized
    {
        get => threadInitialized;
        set => threadInitialized = value;
    }

    public Form()
    {
        if (!ThreadInitialized)
        {
            ThreadInitialized = true;
            GtkApplication.Init();
        }
        self = new FormBase();
        contanter = new Overlay();
        objectCollection = new ObjectCollection(this, contanter);
        Init();
    }

    public Form(string title) : this()
    {
        self.Title = title;
    }

    private void Init()
    {
        var systemColors = SdSystemColors.Window;
        BackColor = SdColor.FromArgb(systemColors.ToArgb());
        SetScrolledWindow(self);
        contanter.Valign = Align.Fill;
        contanter.Halign = Align.Fill;
        contanter.Hexpand = true;
        contanter.Vexpand = true;
        contanter.MarginBottom = 0;
        contanter.MarginEnd = 0;
        contanter.Add(new Fixed() { Halign = Align.Fill, Valign = Align.Fill });
        self.ScrollView.Child = contanter;
        objectCollection = new ObjectCollection(this, contanter);
        self.ResizeChecked += Self_ResizeChecked;
        self.Shown += Control_Shown;
        self.CloseWindowEvent += Self_CloseWindowEvent;
    }

    public FormWindowState WindowState
    {
        get => windowState;
        set
        {
            if (windowState != value)
            {
                var windowStateArg = new WindowStateArgs(value);
                EventHandler<WindowStateArgs>? eventHandler = WindowStateChanging;
                if (eventHandler != null)
                {
                    eventHandler(this, windowStateArg);
                }

                if (windowStateArg.Cancel)
                {
                    return;
                }
            }

            windowState = value;
            if (self.IsMapped)
            {
                if (value == FormWindowState.Maximized)
                {
                    self.Maximize();
                    return;
                }

                if (value == FormWindowState.Minimized)
                {
                    self.Iconify();
                }
            }
        }
    }

    private void Self_ResizeChecked(object? sender, EventArgs e)
    {
        OnSizeChanged(e);
    }

    private void Self_CloseWindowEvent(object? sender, CloseWindowArgs e)
    {
        var closing = new FormClosingEventArgs(CloseReason.UserClosing, false);
        OnFormClosing(closing);

        if (closing.Cancel == false)
        {
            OnFormClosed(new FormClosedEventArgs(CloseReason.UserClosing));
        }

        e.ReturnValue = closing.Cancel == false;
    }

    private void Control_Shown(object? sender, EventArgs e)
    {
        if (isControlShown == false)
        {
            isControlShown = true;
            if (self.Titlebar is HeaderBar titlebar)
            {
                titlebar.DecorationLayout = "menu:close";
                if (formBorderStyle == FormBorderStyle.FixedToolWindow ||
                    formBorderStyle == FormBorderStyle.SizableToolWindow)
                {
                }
                else
                {
                    if (MaximizeBox)
                    {
                        var maximize = new Gtk.Button("window-maximize-symbolic", IconSize.SmallToolbar)
                        {
                            Name = "maximize",
                            Visible = true,
                            Relief = ReliefStyle.None,
                            Valign = Align.Center,
                            Halign = Align.Center
                        };
                        maximize.StyleContext.AddClass("maximize");
                        maximize.StyleContext.AddClass("titlebutton");
                        maximize.Clicked += Maximize_Clicked;
                        titlebar.PackEnd(maximize);
                    }

                    if (MinimizeBox)
                    {
                        var minimize = new Gtk.Button("window-minimize-symbolic", IconSize.SmallToolbar)
                        {
                            Name = "minimize",
                            Visible = true,
                            Relief = ReliefStyle.None,
                            Valign = Align.Center,
                            Halign = Align.Center
                        };
                        minimize.StyleContext.AddClass("minimize");
                        minimize.StyleContext.AddClass("titlebutton");
                        minimize.Clicked += Minimize_Clicked;
                        titlebar.PackEnd(minimize);
                    }
                }
            }

            OnLoad(EventArgs.Empty);
        }

        OnShown(EventArgs.Empty);
    }

    private void Maximize_Clicked(object? sender, EventArgs e)
    {
        var maximize = sender as Gtk.Button;
        if (maximize?.Name == "restore")
        {
            self.Unmaximize();
            maximize.Image = Gtk.Image.NewFromIconName("window-maximize-symbolic", IconSize.SmallToolbar);
            maximize.Name = "maximize";
        }
        else
        {
            self.Maximize();
            if (maximize != null)
            {
                maximize.Image = Gtk.Image.NewFromIconName("window-restore-symbolic", IconSize.SmallToolbar);
                maximize.Name = "restore";
            }
        }
    }

    private void Minimize_Clicked(object? sender, EventArgs e)
    {
        self.Iconify();
    }

    protected override void RemoveScrollHandler(ScrollEventHandler? value)
    {
        if (value != null)
        {
            self.Scroll += value;
        }
    }

    protected override void AddScrollHandler(ScrollEventHandler? value)
    {
        if (value != null)
        {
            self.Scroll += value;
        }
    }

    public override void Show()
    {
        PrepareShowAsync(null, out var taskCompletionSource);
        _ = AwaitGlibIdleAsync(false, taskCompletionSource);
    }

    public void Show(IWin32Window? owner)
    {
        PrepareShowAsync(owner, out var taskCompletionSource);
        _ = AwaitGlibIdleAsync(false, taskCompletionSource);
    }

    internal async Task AwaitGlibIdleAsync(bool isShownFromApplication, TaskCompletionSource<string> taskCompletionSource)
    {
        await taskCompletionSource.Task;
        OnLoadComplete(EventArgs.Empty);
        if (IsClosed)
        {
            if (isShownFromApplication)
            {
                Gtk.Application.Invoke(delegate
                {
                    Gtk.Application.Quit();
                });
            }
        }
    }

    internal void PrepareShowAsync(IWin32Window? owner, out TaskCompletionSource<string> taskCompletionSource)
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

        if (owner is Form parent)
        {
            Parent = parent;
            self.SetPosition(WindowPosition.CenterOnParent);
            self.DestroyWithParent = true;
            self.Activate();
        }

        if (self.IsVisible == false)
        {
            FormBorderStyle = FormBorderStyle;
            if (MaximizeBox == false && MinimizeBox == false)
            {
                self.TypeHint = Gdk.WindowTypeHint.Dialog;
            }

            self.Resize(self.DefaultWidth, self.DefaultHeight);

            if (WindowState == FormWindowState.Maximized)
            {
                self.Maximize();
            }
            else if (WindowState == FormWindowState.Minimized)
            {
                self.Iconify();
            }

            HandleIsMapped();
        }

        FakeHandle = FakeHandleValue;
        SetFakeHandle(Controls);
        OnLoad(EventArgs.Empty);
        OnBindingContextChanged(EventArgs.Empty);
        taskCompletionSource = new TaskCompletionSource<string>();
        new GlibHelper(self, taskCompletionSource).AddIdle();
    }

    private class GlibHelper(FormBase self, TaskCompletionSource<string> taskCompletionSource)
    {
        public void AddIdle()
        {
            GLib.Idle.Add(() =>
            {
                self.ShowAll();
                taskCompletionSource.SetResult(string.Empty);
                return false;
            });

        }
    }

    private void HandleIsMapped()
    {
        if (self.IsMapped == false)
        {
            try
            {
                if (ShowIcon)
                {
                    if (Icon != null)
                    {
                        if (Icon.Pixbuf != null)
                            self.Icon = Icon.Pixbuf;
                        else if (Icon.PixbufData != null)
                            self.Icon = new Gdk.Pixbuf(Icon.PixbufData);
                        else if (Icon.FileName != null && File.Exists(Icon.FileName))
                            self.SetIconFromFile(Icon.FileName);
                        else if (Icon.FileName != null && File.Exists("Resources\\" + Icon.FileName))
                            self.SetIconFromFile("Resources\\" + Icon.FileName);
                    }

                    var titlebar = (HeaderBar)self.Titlebar;
                    var flag = new Image(self.Icon);
                    flag.Visible = true;
                    titlebar.PackStart(flag);
                }
                else
                {
                    self.Icon = new Gdk.Pixbuf(GetType().Assembly,
                        "System.Windows.Forms.Resources.System.view-more.png");
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex);
            }
        }
    }

    private void SetFakeHandle(IEnumerable collection)
    {
        foreach (var item in collection)
        {
            if (item is Control control)
            {
                control.FakeHandle = FakeHandleValue;
                SetFakeHandle(control.Controls);
            }
        }
    }

    public DialogResult ShowDialog()
    {
        return ShowDialog(null);
    }

    public DialogResult ShowDialog(IWin32Window? owner)
    {
        if (owner == this)
        {
            throw new ArgumentException(@"OwnsSelfOrOwner", nameof(ShowDialog));
        }

        if (base.Visible)
        {
            throw new InvalidOperationException("ShowDialogOnVisible");
        }

        if (!base.Enabled)
        {
            throw new InvalidOperationException("ShowDialogOnDisabled");
        }

        PrepareShowAsync(owner, out var taskCompletionSource);
        _ = AwaitGlibIdleAsync(false, taskCompletionSource);
        self.Run();

        return DialogResult;
    }

    public override string Text
    {
        get => self.Title;
        set
        {
            self.Title = value;
            base.Text = value ?? string.Empty;
        }
    }

    public override SdSize ClientSize
    {
        get => new(self.AllocatedWidth, self.AllocatedHeight);
        set
        {
            self.WidthRequest = -1;
            self.HeightRequest = -1;
            self.SetDefaultSize(value.Width, value.Height);
        }
    }

    public SdSizeF AutoScaleDimensions { get; set; }

    public AutoScaleMode AutoScaleMode { get; set; }

    public FormBorderStyle FormBorderStyle
    {
        get => formBorderStyle;
        set
        {
            formBorderStyle = value;
            self.Resizable = value == FormBorderStyle.Sizable || value == FormBorderStyle.SizableToolWindow;
            if (value == FormBorderStyle.None)
            {
                self.Decorated = false; // Delete toolbar
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

    public DialogResult DialogResult { get; set; }

    public void Close()
    {
        self?.CloseWindow();
        OnFormClosed(new FormClosedEventArgs(CloseReason.None));
    }

    public override void Hide()
    {
        self?.Hide();
    }

    public new ObjectCollection Controls => objectCollection;

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

    public double Opacity
    {
        get => self.Opacity;
        set => self.Opacity = value;
    }

    public bool ShowIcon { get; set; } = true;

    public bool ShowInTaskbar
    {
        get => self.SkipTaskbarHint == false;
        set => self.SkipTaskbarHint = value == false;
    }

    public Icon? Icon { get; set; }

    public override void SuspendLayout()
    {
        created = false;
    }

    public override void ResumeLayout(bool resume)
    {
        created = resume == false;
    }

    public override void PerformLayout()
    {
        created = true;
    }

    public bool Activate()
    {
        return self.Activate();
    }

    public MenuStrip? MainMenuStrip { get; set; }

    public override IntPtr Handle
    {
        get
        {
            if (FakeHandle == IntPtr.Zero)
            {
                FakeHandle = FakeHandleValue;
            }
            if (self.Handle != IntPtr.Zero || FakeHandle != IntPtr.Zero)
            {
                OnHandleCreated(EventArgs.Empty);
            }
            return self.Handle;
        }
        set
        {
            FakeHandle = value;
            OnHandleCreated(EventArgs.Empty);
        }
    }

    public bool IsClosed { get; set; }

    public class ObjectCollection : ControlCollection
    {
        private Gtk.Container? owner;

        public ObjectCollection(Control? control, Gtk.Container? owner) : base(control, owner)
        {
            this.owner = owner;
        }
    }
}