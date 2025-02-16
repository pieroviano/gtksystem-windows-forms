using Gtk;
using GTKSystem.Windows.Forms.Interfaces;
using System;
using System.Linq;
using System.Windows.Forms;

namespace GTKSystem.Windows.Forms.GTKControls.ControlBase
{
    public sealed class TabControlBase : Gtk.Notebook, IGtkControl
    {
        public IGtkControlOverride Override { get; set; }
        public TabControlBase() : base()
        {
            this.Scrollable = true;
            this.EnablePopup = false;
            this.Override = new GtkFormsControlOverride(this);
            this.Override.AddClass("TabControl");
            base.Halign = Gtk.Align.Start;
            base.Valign = Gtk.Align.Start;
        }
        public void AddClass(string cssClass)
        {
            this.Override.AddClass(cssClass);
        }
        protected override void OnShown()
        {
            Override.OnAddClass();
            base.OnShown();
        }
        protected override bool OnDrawn(Cairo.Context cr)
        {
            Gdk.Rectangle rec = new Gdk.Rectangle(0, 0, this.AllocatedWidth, this.AllocatedHeight);
            Override.OnPaint(cr, rec);
            return base.OnDrawn(cr);
        }
    }
}
