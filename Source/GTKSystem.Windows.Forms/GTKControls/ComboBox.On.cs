namespace System.Windows.Forms;

public partial class ComboBox
{
    protected virtual void OnDropDown(EventArgs e)
    {
        EventInvoke(() =>
        {
            DropDown?.Invoke(this, e);
        }, false);
    }
}
