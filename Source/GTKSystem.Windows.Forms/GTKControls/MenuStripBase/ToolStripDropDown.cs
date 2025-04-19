using System.Drawing;
using Gtk;

namespace System.Windows.Forms;

using Size = Size;

public class ToolStripDropDown : ToolStripItem
{
    //public readonly ToolStripDropDownBase self = new ToolStripDropDownBase();
    public readonly Menu self;

    public ToolStripDropDown()
    {
        self = new Menu();
    }

    public override Widget Widget => self;

    public Size ImageScalingSize { get; set; }
}