namespace System.Windows.Forms;

public partial class NumericUpDown
{
    protected virtual void OnValueChanged(EventArgs e)
    {
        EventInvoke(() => ValueChanged?.Invoke(this, e), false);
    }
}