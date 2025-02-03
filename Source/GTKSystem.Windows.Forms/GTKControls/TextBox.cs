/*
 * A cross-platform interface component developed based on GTK components and compatible with the native C# control winform interface.
 * Use this component GTKSystem.Windows.Forms instead of Microsoft.WindowsDesktop.App.WindowsForms, compile once, run across platforms windows, linux, macos
 * Technical support 438865652@qq.com, https://www.gtkapp.com, https://gitee.com/easywebfactory, https://github.com/easywebfactory
 * author:chenhongjin
 */

using GLib;
using Gtk;
using GTKSystem.Windows.Forms.GTKControls.ControlBase;
using System.ComponentModel;
using System.Linq;

namespace System.Windows.Forms
{
    [DesignerCategory("Component")]
    public partial class TextBox : Control
    {
        public readonly TextBoxBase self = new TextBoxBase();
        private bool _shortcutsEnabled = true;
        public override object GtkControl => self;

        public TextBox() : base()
        {
            self.MaxWidthChars = 1;
            self.WidthChars = 0;

            self.Valign = Gtk.Align.Start;
            self.Halign = Gtk.Align.Start;
            self.Changed += Self_Changed;
            self.TextInserted += Self_TextInserted;
            self.KeyPressEvent += Self_KeyPressEvent;
            self.ClipboardPasted += (o, args) =>
            {
                OnPasted(args);
            };
        }

        public bool ShortcutEnabled
        {
            get
            {
                return _shortcutsEnabled;
            }
            set
            {
                _shortcutsEnabled = value;
                if (!_shortcutsEnabled)
                {
                    self.KeyPressEvent += OnSelfOnKeyPressEvent;
                }
                else
                {
                    self.KeyPressEvent -= OnSelfOnKeyPressEvent;
                }
            }
        }

        protected virtual void OnSelfOnKeyPressEvent(object s, Gtk.KeyPressEventArgs args)
        {
            // Detect Ctrl + C, Ctrl + V, Ctrl + X, Ctrl + Ins, Shift + Ins, Shift + Delete
            bool isCtrl = (args.Event.State & Gdk.ModifierType.ControlMask) != 0;
            bool isShift = (args.Event.State & Gdk.ModifierType.ShiftMask) != 0;
            if ((isCtrl && (args.Event.Key == Gdk.Key.c || args.Event.Key == Gdk.Key.C /* Copy */ || 
                            args.Event.Key == Gdk.Key.v || args.Event.Key == Gdk.Key.V /* Paste */ || 
                            args.Event.Key == Gdk.Key.x || args.Event.Key == Gdk.Key.X /* Cut */)) || 
                (isCtrl && args.Event.Key == Gdk.Key.Insert) || // Ctrl + Ins (Copy)
                (isShift && args.Event.Key == Gdk.Key.Delete) || // Shift + Del (Cut)
                (isShift && args.Event.Key == Gdk.Key.Insert)) // Shift + Ins (Paste)
            {
                args.RetVal = true; // Block the event
            }
        }

        protected virtual void OnPasted(EventArgs e)
        {

        }

        public int SelectionStart
        {
            get
            {
                return self.SelectionStart;
            }
            set
            {
                self.SelectionStart = value;
            }
        }

        public int SelectionLength
        {
            get
            {
                return self.SelectionLength;
            }
            set
            {
                self.SelectionLength = value;
            }
        }


        private void Self_KeyPressEvent(object o, Gtk.KeyPressEventArgs args)
        {
            if (KeyDown != null)
            {
                if (args.Event is Gdk.EventKey eventkey)
                {
                    Keys keys = (Keys)eventkey.HardwareKeycode;
                    KeyDown(this, new KeyEventArgs(keys));
                }
            }
        }

        public override event KeyEventHandler KeyDown;

        private void Self_TextInserted(object o, TextInsertedArgs args)
        {
            if (KeyDown != null && this.GetType().Name == "TextBox")
            {
                string keytext = args.NewText.ToUpper();
                if (char.IsNumber(args.NewText[0]))
                    keytext = "D" + keytext;
                var keyv = ((Keys[])Enum.GetValues(typeof(Keys))).Where(k => { return Enum.GetName(typeof(Keys), k) == keytext; });
                foreach (var key in keyv)
                    KeyDown(this, new KeyEventArgs(key));
            }
        }

        private void Self_Changed(object sender, EventArgs e)
        {
            if (TextChanged != null && self.IsVisible)
            {
                TextChanged(this, EventArgs.Empty);
            }
        }

        public string PlaceholderText
        {
            get { return self.PlaceholderText; }
            set { self.PlaceholderText = value ?? ""; }
        }

        public override string Text
        {
            get { return self.Text; }
            set { self.Text = value ?? ""; }
        }

        public virtual char PasswordChar
        {
            get => self.InvisibleChar;
            set
            {
                self.InvisibleChar = value;
                self.Visibility = false;
            }
        }

        public virtual bool ReadOnly
        {
            get { return self.IsEditable == false; }
            set { self.IsEditable = value == false; }
        }

        public override event EventHandler TextChanged;
        public bool Multiline { get; set; }
    }
}