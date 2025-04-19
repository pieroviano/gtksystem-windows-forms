#if GTKSystemWindowsForms

namespace System.Drawing.Printing;

public partial class PrintDocument
{
    public event PrintEventHandler? BeginPrint
    {
        add => _beginPrint += value;
        remove => _beginPrint -= value;
    }

    public event PrintEventHandler? EndPrint
    {
        add => _endPrint += value;
        remove => _endPrint -= value;
    }

    public event PrintPageEventHandler? PrintPage
    {
        add => _printPage += value;
        remove => _printPage -= value;
    }

    public event QueryPageSettingsEventHandler? QueryPageSettings
    {
        add => _queryPageSettings += value;
        remove => _queryPageSettings -= value;
    }
}
#endif