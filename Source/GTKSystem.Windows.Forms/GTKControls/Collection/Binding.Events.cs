namespace System.Windows.Forms;

public partial class Binding
{
    private event BindingCompleteEventHandler? _bindingComplete;

    private event ConvertEventHandler? _parse;

    private event ConvertEventHandler? _format;

    /// <summary>Occurs when the <see cref="P:System.Windows.Forms.Binding.FormattingEnabled" /> property is set to true and a binding operation is complete, such as when data is pushed from the control to the data source or vice versa</summary>
    public event BindingCompleteEventHandler? BindingComplete
    {
        add => _bindingComplete += value;
        remove => _bindingComplete -= value;
    }

    /// <summary>Occurs when the property of a control is bound to a data value.</summary>
    /// <filterpriority>1</filterpriority>
    public event ConvertEventHandler? Format
    {
        add => _format += value;
        remove => _format -= value;
    }

    /// <summary>Occurs when the value of a data-bound control changes.</summary>
    /// <filterpriority>1</filterpriority>
    public event ConvertEventHandler? Parse
    {
        add => _parse += value;
        remove => _parse -= value;
    }
}