using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms;

[LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "SelectedValue")]
public abstract partial class ListControl : ScrollableControl
{
    protected EventHandlerList events = new();
    [DefaultValue(null)]
    [RefreshProperties(RefreshProperties.Repaint)]
    [AttributeProvider(typeof(IListSource))]
    public virtual object? DataSource
    {
        get;set;
    }

    [DefaultValue("")]
    public virtual string DisplayMember
    {
        get;
        set;
    } = string.Empty;

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DefaultValue(null)]
    public virtual IFormatProvider FormatInfo
    {
        get;
        set;
    } = CultureInfo.CurrentCulture;

    [DefaultValue("")]
    [MergableProperty(false)]
    public virtual string FormatString
    {
        get;
        set;
    } = string.Empty;

    [DefaultValue(false)]
    public virtual bool FormattingEnabled
    {
        get; set;
    }

    [DefaultValue("")]
    public virtual string ValueMember
    {
        get; set;
    } = string.Empty;

    public abstract int SelectedIndex { get; set; }

    [DefaultValue(null)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Bindable(true)]
    public virtual object? SelectedValue
    {
        get; set;
    }

    public virtual string? GetItemText(object? item)
    {
        return item?.ToString();
    }
}