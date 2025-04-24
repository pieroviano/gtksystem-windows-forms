namespace System.Windows.Forms;

public partial class ComboBox
{
    protected virtual void OnDropDown(EventArgs e)
    {
        ((Action)(() =>
        {
            DropDown?.Invoke(this, e);
        }))();
    }
}
