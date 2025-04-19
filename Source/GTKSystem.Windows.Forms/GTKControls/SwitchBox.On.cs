namespace System.Windows.Forms;

public partial class SwitchBox : Control
{
    protected virtual void OnCheckedChanged()
    {
        CheckedChanged?.Invoke(this, EventArgs.Empty);
    }
}

