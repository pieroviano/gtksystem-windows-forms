using System.ComponentModel;

namespace System.Windows.Forms;

public partial class BindingsCollection
{
    private CollectionChangeEventHandler? _collectionChanging;
    private CollectionChangeEventHandler? _collectionChanged;

    public event CollectionChangeEventHandler? CollectionChanging
    {
        add => _collectionChanging += value;
        remove => _collectionChanging -= value;
    }

    public event CollectionChangeEventHandler? CollectionChanged
    {
        add => _collectionChanged += value;
        remove => _collectionChanged -= value;
    }

}