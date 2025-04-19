namespace System.Windows.Forms;

public abstract partial class ScrollBar
{
    public event ScrollEventHandler? Scroll;
    public event EventHandler? ValueChanged;
}