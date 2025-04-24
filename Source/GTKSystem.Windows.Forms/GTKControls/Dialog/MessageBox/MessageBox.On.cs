namespace System.Windows.Forms;

internal partial class NonStaticMessageBox
{
    protected virtual void OnDialogAvailable(DialogEventArgs e)
    {
        DialogAvailable?.Invoke(this, e);
    }
}

