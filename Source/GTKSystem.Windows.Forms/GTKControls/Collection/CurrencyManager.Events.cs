using System.ComponentModel;

namespace System.Windows.Forms;

public partial class CurrencyManager
{
    private ItemChangedEventHandler? _itemChanged;

    private ListChangedEventHandler? _listChanged;

    private readonly ItemChangedEventArgs _resetEvent = new(-1);

    private EventHandler? _metaDataChanged;

    /// <summary>Occurs when the current item has been altered.</summary>
    /// <filterpriority>1</filterpriority>
    public event ItemChangedEventHandler? ItemChanged
    {
        add => _itemChanged += value;
        remove => _itemChanged -= value;
    }

    /// <summary>Occurs when the list changes or an item in the list changes.</summary>
    /// <filterpriority>1</filterpriority>
    public event ListChangedEventHandler? ListChanged
    {
        add => _listChanged += value;
        remove => _listChanged -= value;
    }

    /// <summary>Occurs when the metadata of the <see cref="P:System.Windows.Forms.CurrencyManager.List" /> has changed.</summary>
    /// <filterpriority>1</filterpriority>
    public event EventHandler? MetaDataChanged
    {
        add => _metaDataChanged += value;
        remove => _metaDataChanged -= value;
    }
}
