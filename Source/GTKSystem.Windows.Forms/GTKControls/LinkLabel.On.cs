namespace System.Windows.Forms;

public partial class LinkLabel
{
    protected internal virtual void OnLinkClicked(LinkLabelLinkClickedEventArgs e)
    {
        EventInvoke(() => LinkClicked?.Invoke(this, e), false);
    }
}