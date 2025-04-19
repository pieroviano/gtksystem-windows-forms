using System.ComponentModel;

namespace System.Windows.Forms;

public partial class DataGridViewColumn
{
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event EventHandler? Disposed;
}