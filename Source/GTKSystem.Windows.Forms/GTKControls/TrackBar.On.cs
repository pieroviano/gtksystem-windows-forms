namespace System.Windows.Forms;

public partial class TrackBar
{
    protected virtual void OnScroll(EventArgs e)
    {
        EventInvoke(() => Scroll?.Invoke(this, e),false);
    }

}