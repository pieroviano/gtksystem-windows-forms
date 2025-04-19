namespace System.Windows.Forms;

public static class LinkLabelExtension
{
    public static void PerformClick(this LinkLabel control)
    {
        var eventArgs = new LinkLabelLinkClickedEventArgs(new LinkLabel.Link
        { Description = control.self.Label, LinkData = control.self.Uri });
        control.OnLinkClicked(eventArgs);
        ((Control)control).PerformClick();
    }
}