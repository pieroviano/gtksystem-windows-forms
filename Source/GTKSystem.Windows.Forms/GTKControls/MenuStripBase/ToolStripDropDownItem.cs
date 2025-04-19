using System.Drawing;
using System.ComponentModel;

namespace System.Windows.Forms;

using Point = Point;

public abstract partial class ToolStripDropDownItem : WidgetToolStrip<Gtk.MenuItem>
{
    protected ToolStripDropDownItem() : this("", null, null, "")
    {

    }

    protected ToolStripDropDownItem(string? text, Image? image, EventHandler? onClick) : this(text, image, onClick, "")
    {

    }

    protected ToolStripDropDownItem(string? text, Image? image, params ToolStripItem[] dropDownItems) : this(text, image, null, "")
    {
        DropDownItems.AddRange(dropDownItems);
    }

    protected ToolStripDropDownItem(string? text, Image? image, EventHandler? onClick, string? name) : base("ToolStripDropDownItem", text, image, onClick, name)
    {
        DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
    }

    [Browsable(false)]
    public bool HasDropDown { get; set; }

    [Browsable(false)]
    public virtual bool HasDropDownItems { get; set; }

    //public ToolStripItemCollection DropDownItems { get; }

    [Browsable(false)]
    public ToolStripDropDownDirection DropDownDirection { get; set; }

    [TypeConverter(typeof(ReferenceConverter))]
    public ToolStripDropDown? DropDown { get; set; }

    protected internal virtual Point DropDownLocation { get; set; }

    public void HideDropDown() { }

    public void ShowDropDown() { }

}