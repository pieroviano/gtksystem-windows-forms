#if GTKSystemWindowsForms

namespace System.Drawing.Printing;

public partial class PrintDocument
{
    protected internal virtual void OnBeginPrint(PrintEventArgs e) => _beginPrint?.Invoke(this, e);

    protected internal virtual void OnEndPrint(PrintEventArgs e) => _endPrint?.Invoke(this, e);

    protected internal virtual void OnPrintPage(PrintPageEventArgs e) => _printPage?.Invoke(this, e);

    protected internal virtual void OnQueryPageSettings(QueryPageSettingsEventArgs e) =>
        _queryPageSettings?.Invoke(this, e);
}

#endif