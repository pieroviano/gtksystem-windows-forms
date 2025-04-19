namespace System.Windows.Forms;

public partial class CheckedListBox
{
    public event ItemCheckEventHandler? ItemCheck;

    public event EventHandler? SelectedIndexChanged;

    public event EventHandler? SelectedValueChanged;

    public event EventHandler? SelectedItemChanged;
}

