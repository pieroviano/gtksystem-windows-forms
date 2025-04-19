namespace System.Windows.Forms;

public partial class CheckBox
{
    public event ItemCheckEventHandler? ItemCheck;

    public event EventHandler? CheckedChanged;

    public event EventHandler? CheckStateChanged;
}
