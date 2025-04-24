namespace System.Windows.Forms;

public abstract partial class ListControl
{
    protected virtual void OnDisplayMemberChanged(EventArgs e)
    {
        DisplayMemberChanged?.Invoke(this, e);
    }

    protected virtual void OnFormat(ListControlConvertEventArgs e)
    {
        Format?.Invoke(this, e);
    }

    protected virtual void OnFormatInfoChanged(EventArgs e)
    {
        FormatInfoChanged?.Invoke(this, e);
    }

    protected virtual void OnFormatStringChanged(EventArgs e)
    {
        FormatStringChanged?.Invoke(this, e);
    }

    protected virtual void OnFormattingEnabledChanged(EventArgs e)
    {
        FormattingEnabledChanged?.Invoke(this, e);
    }

    protected virtual void OnValueMemberChanged(EventArgs e)
    {
        ValueMemberChanged?.Invoke(this, e);
    }
}