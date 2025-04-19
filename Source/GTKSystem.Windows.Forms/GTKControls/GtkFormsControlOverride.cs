using System.Drawing;
using Gtk;

namespace System.Windows.Forms;

internal class GtkFormsControlOverride: GtkControlOverride
{
    public GtkFormsControlOverride(IWidget? container) : base(container)
    {
    }
}