using GLib;
using GTKSystem.Windows.Forms.Interfaces;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Orientation = Gtk.Orientation;


namespace GTKSystem.Windows.Forms.GTKControls.ControlBase
{
    public sealed class ScrollbarBase<T>: Gtk.Scrollbar, IGtkControl
    {
        public new static GType GType
        {
            get
            {
                return (typeof(T) == typeof(Gtk.HScrollbar)) ? Gtk.HScrollbar.GType : Gtk.VScrollbar.GType;
            }
        }
        
        public IGtkControlOverride Override { get; set; }
        internal ScrollbarBase(Orientation orientation): base(orientation, new Gtk.Adjustment(0, 0, 100, 1, 10, 0))
        {
            this.Override = new GtkFormsControlOverride(this);
        }
    }
}
