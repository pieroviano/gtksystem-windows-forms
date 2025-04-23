namespace System.Windows.Forms;

public partial class DateTimePicker
{
    protected virtual void OnValueChanged(EventArgs e)
    {
        EventInvoke(() => ValueChanged?.Invoke(this, e));
    }
}