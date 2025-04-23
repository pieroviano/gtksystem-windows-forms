namespace System.Windows.Forms;

public abstract partial class ListControl
{
    protected virtual void OnDisplayMemberChanged(EventArgs e)
    {
        EventInvoke(() => DisplayMemberChanged?.Invoke(this, e));
    }

    protected virtual void OnFormat(ListControlConvertEventArgs e)
    {
        EventInvoke(() => Format?.Invoke(this, e));
    }

    protected virtual void OnFormatInfoChanged(EventArgs e)
    {
        EventInvoke(() => FormatInfoChanged?.Invoke(this, e));
    }

    protected virtual void OnFormatStringChanged(EventArgs e)
    {
        EventInvoke(() => FormatStringChanged?.Invoke(this, e));
    }

    protected virtual void OnFormattingEnabledChanged(EventArgs e)
    {
        EventInvoke(() => FormattingEnabledChanged?.Invoke(this, e));
    }

    protected virtual void OnValueMemberChanged(EventArgs e)
    {
        EventInvoke(() => ValueMemberChanged?.Invoke(this, e));
    }
}