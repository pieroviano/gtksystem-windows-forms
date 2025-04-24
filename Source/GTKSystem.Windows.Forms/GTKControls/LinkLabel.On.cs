namespace System.Windows.Forms;

public partial class LinkLabel
{
    protected internal virtual void OnLinkClicked(LinkLabelLinkClickedEventArgs e)
    {
        LinkClicked?.Invoke(this, e);
    }
}