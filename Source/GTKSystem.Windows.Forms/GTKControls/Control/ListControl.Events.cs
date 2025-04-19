using System.ComponentModel;

namespace System.Windows.Forms;

public abstract partial class ListControl
{
    public event EventHandler? DataSourceChanged
    {
        add => events.AddHandler("DataSourceChanged", value);
        remove => events.RemoveHandler("DataSourceChanged", value);
    }

    public event EventHandler? DisplayMemberChanged;

    public event ListControlConvertEventHandler? Format;

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public event EventHandler? FormatInfoChanged;

    public event EventHandler? FormatStringChanged;

    public event EventHandler? FormattingEnabledChanged;

    public event EventHandler? ValueMemberChanged;

    public event EventHandler? SelectedItemChanged
    {
        add => events.AddHandler("SelectedItemChanged", value);
        remove => events.RemoveHandler("SelectedItemChanged", value);
    }

    public event EventHandler? SelectedValueChanged
    {
        add => events.AddHandler("SelectedValueChanged", value);
        remove => events.RemoveHandler("SelectedValueChanged", value);
    }

    public event EventHandler? SelectedIndexChanged
    {
        add => events.AddHandler("SelectedIndexChanged", value);
        remove => events.RemoveHandler("SelectedIndexChanged", value);
    }
}