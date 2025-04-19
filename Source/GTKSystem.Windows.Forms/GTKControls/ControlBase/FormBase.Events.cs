namespace System.Windows.Forms;

public partial class FormBase
{
    public event CloseWindowHandler? CloseWindowEvent;

    public event ScrollEventHandler? Scroll;
}
