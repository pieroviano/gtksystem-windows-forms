namespace System.Windows.Forms;

public partial class DateTimePicker
{
    protected virtual void OnValueChanged(EventArgs e)
    {
        ValueChanged?.Invoke(this, e);
    }
}