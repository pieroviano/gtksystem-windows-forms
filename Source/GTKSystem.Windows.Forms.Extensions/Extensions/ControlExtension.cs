namespace System.Windows.Forms;

public static class ControlExtension
{
    public static void PerformClick(this Control control)
    {
        control.OnClick(EventArgs.Empty);
    }
}