using System.Windows.Forms;
using GTKSystem.Windows.Forms.Interfaces;

namespace GTKSystem.Windows.Forms.GTKControls.ControlBase
{
    public sealed class ToolStripBase : Gtk.MenuBar, IGtkControl
    {
        public IGtkControlOverride Override { get; set; }
        public ToolStripBase() : base()
        {
            this.Override = new GtkFormsControlOverride(this);
            this.Override.AddClass("ToolStrip");
            this.Hexpand = true;
            this.Vexpand = false;
            this.Valign = Gtk.Align.Start;
            this.Halign = Gtk.Align.Fill;
            this.HeightRequest = 20;
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
    }
}
