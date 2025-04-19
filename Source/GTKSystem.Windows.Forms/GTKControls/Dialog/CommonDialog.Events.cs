using GtkApplication = System.Windows.Forms.Application;

namespace System.Windows.Forms;

public partial class CommonDialog
{
    public event EventHandler? HelpRequest
    {
        add => Events.AddHandler(helpRequestEvent, value);
        remove => Events.RemoveHandler(helpRequestEvent, value);
    }

    public event EventHandler<CommonDialogEventArgs>? DialogOnShown;

    public event EventHandler? OKClicked;
    public event EventHandler? CancelClicked;

}