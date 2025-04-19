namespace System.Windows.Forms;

internal sealed partial class PropertyGridView
{
    public event PropertyValueChangedEventHandler? PropertyValueChanged;
    public event SelectedGridItemChangedEventHandler? SelectedGridItemChanged;
}