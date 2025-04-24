namespace System.Windows.Forms;

public partial class MonthCalendar
{
    protected virtual void OnDateSelected(DateRangeEventArgs e)
    {
        DateSelected?.Invoke(this, e);
    }

    protected virtual void OnDateChanged(DateRangeEventArgs e)
    {
        DateChanged?.Invoke(this, e);
    }

}