namespace System.Windows.Forms;

public partial class Form
{
    public event EventHandler<WindowStateArgs>? WindowStateChanging;
    public event EventHandler? Shown;
    public event FormClosingEventHandler? FormClosing;
    public event FormClosedEventHandler? FormClosed;
}