using System.Windows.Forms;
using GTKSystem.Windows.Forms.Interfaces;

namespace GTKSystem.Windows.Forms.GTKControls.ControlBase
{
    public sealed class ListViewBase : Gtk.Viewport, IGtkControl
    {
        public Gtk.Box box = new Gtk.Box(Gtk.Orientation.Vertical, 0);
        public IGtkControlOverride Override { get; set; }
        public ListViewBase() : base()
        {
            this.Override = new GtkFormsControlOverride(this);
            this.StyleContext.AddClass("view");
            this.Override.AddClass("ListView");
            this.BorderWidth = 1;
            this.Add(box);
        }
    }
}
