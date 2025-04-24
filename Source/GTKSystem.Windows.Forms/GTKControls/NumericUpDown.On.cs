namespace System.Windows.Forms;

public partial class NumericUpDown
{
    protected virtual void OnValueChanged(EventArgs e)
    {
        ValueChanged?.Invoke(this, e);
    }
}