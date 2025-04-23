namespace System.Windows.Forms;

public partial class MonthCalendar
{
    protected virtual void OnDateSelected(DateRangeEventArgs e)
    {
        EventInvoke(() => DateSelected?.Invoke(this, e));
    }

    protected virtual void OnDateChanged(DateRangeEventArgs e)
    {
        EventInvoke(() => DateChanged?.Invoke(this, e));
    }

}