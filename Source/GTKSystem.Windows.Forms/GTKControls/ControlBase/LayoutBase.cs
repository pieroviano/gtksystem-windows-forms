using System.Windows.Forms;
using Gtk;
using GTKSystem.Windows.Forms.Interfaces;


namespace GTKSystem.Windows.Forms.GTKControls.ControlBase
{
    public sealed class LayoutBase: Gtk.Layout, IGtkControl
    {
        public IGtkControlOverride Override { get; set; }
        public LayoutBase(Adjustment hadjustment, Adjustment vadjustment) : base(hadjustment, vadjustment)
        {
            this.Override = new GtkFormsControlOverride(this);
            base.Valign = Gtk.Align.Start;
            base.Halign = Gtk.Align.Start;
        }
        protected override bool OnDrawn(Cairo.Context cr)
        {
            Gdk.Rectangle rec = new Gdk.Rectangle(0, 0, this.AllocatedWidth, this.AllocatedHeight);
            Override.DrawnBackColor(cr, rec);
            return base.OnDrawn(cr);
        }
    }
}
