namespace System.Windows.Forms;

public class CommonDialogEventArgs(CommonDialog dialog) : EventArgs()
{
    public CommonDialog Dialog => dialog;
}