namespace System.Windows.Forms;

public partial class TrackBar
{
    protected virtual void OnScroll(EventArgs e)
    {
        Scroll?.Invoke(this, e);
    }

}