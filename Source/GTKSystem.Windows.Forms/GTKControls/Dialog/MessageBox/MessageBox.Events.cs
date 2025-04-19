namespace System.Windows.Forms;

internal partial class NonStaticMessageBox
{
    public event EventHandler<DialogEventArgs>? DialogAvailable;
}