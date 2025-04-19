// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace System.Windows.Forms;

public abstract partial class BindingManagerBase
{
    private BindingsCollection? _bindings;
    private bool _pullingData;

    public BindingsCollection Bindings
    {
        get
        {
            if (_bindings is null)
            {
                _bindings = new ListManagerBindingsCollection(this);

                // Hook collection change events on collection, so we can hook or unhook the BindingComplete events on individual bindings
                _bindings.CollectionChanging += OnBindingsCollectionChanging;
                _bindings.CollectionChanged += OnBindingsCollectionChanged;
            }

            return _bindings;
        }
    }

    public abstract object? Current { get; }

    private protected abstract void SetDataSource(object? source);

    public BindingManagerBase() { }

    internal BindingManagerBase(object? dataSource) => Init(dataSource);

    private void Init(object? dataSource)
    {
        SetDataSource(dataSource);
    }

    internal abstract Type? BindType { get; }

    internal abstract PropertyDescriptorCollection? GetItemProperties(PropertyDescriptor?[]? listAccessors);

    public virtual PropertyDescriptorCollection? GetItemProperties() => GetItemProperties(listAccessors: null);

    protected internal virtual PropertyDescriptorCollection? GetItemProperties(ArrayList dataSources, ArrayList listAccessors)
    {
        IList? list = null;
        if (this is CurrencyManager currencyManager)
        {
            list = currencyManager.List;
        }

        if (list is ITypedList typedList)
        {
            var properties = new PropertyDescriptor[listAccessors.Count];
            listAccessors.CopyTo(properties, 0);
            return typedList.GetItemProperties(properties);
        }

        return GetItemProperties(BindType, 0, dataSources, listAccessors);
    }

    protected virtual PropertyDescriptorCollection? GetItemProperties(
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type? listType,
        int offset,
        ArrayList dataSources,
        ArrayList listAccessors)
    {
        if (listAccessors.Count < offset)
        {
            return null;
        }

        if (listAccessors.Count == offset)
        {
            if (!typeof(IList).IsAssignableFrom(listType))
            {
                return TypeDescriptor.GetProperties(listType);
            }

            if (listType != null)
            {
                foreach (var property in listType.GetProperties())
                {
                    if (property.Name == "Item" && property.PropertyType != typeof(object))
                    {
                        return TypeDescriptor.GetProperties(property.PropertyType, [new BrowsableAttribute(true)]);
                    }
                }
            }

            // return the properties on the type of the first element in the list
            if (dataSources[offset - 1] is IList { Count: > 0 } list)
            {
                return TypeDescriptor.GetProperties(list[0]!);
            }

            return null;
        }

        if (typeof(IList).IsAssignableFrom(listType))
        {
            PropertyDescriptorCollection? itemProps = null;
            if (listType != null)
            {
                foreach (var property in listType.GetProperties())
                {
                    if (property.Name == "Item" && property.PropertyType != typeof(object))
                    {
                        // get all the properties that are not marked as Browsable(false)
                        itemProps = TypeDescriptor.GetProperties(property.PropertyType, [new BrowsableAttribute(true)]);
                    }
                }
            }

            if (itemProps is null)
            {
                // Use the properties on the type of the first element in the list
                // if offset == 0, then this means that the first dataSource did not have a strongly typed Item property.
                // the dataSources are added only for relatedCurrencyManagers, so in this particular case
                // we need to use the dataSource in the currencyManager.
                IList? list;
                if (offset == 0)
                {
                    list = DataSource as IList;
                }
                else
                {
                    list = dataSources[offset - 1] as IList;
                }

                if (list is not null && list.Count > 0)
                {
                    itemProps = TypeDescriptor.GetProperties(list[0]!);
                }
            }

            if (itemProps is not null)
            {
                for (var j = 0; j < itemProps.Count; j++)
                {
                    if (itemProps[j].Equals(listAccessors[offset]))
                    {
                        return GetItemProperties(itemProps[j].PropertyType, offset + 1, dataSources, listAccessors);
                    }
                }
            }
        }
        else
        {
            if (listType != null)
            {
                foreach (var property in listType.GetProperties())
                {
                    if (property.Name.Equals(((PropertyDescriptor)listAccessors[offset]!).Name))
                    {
                        return GetItemProperties(property.PropertyType, offset + 1, dataSources, listAccessors);
                    }
                }
            }
        }

        return null;
    }

    internal abstract string? GetListName();
    public abstract void CancelCurrentEdit();
    public abstract void EndCurrentEdit();

    public abstract void AddNew();
    public abstract void RemoveAt(int index);

    public abstract int Position { get; set; }

    protected abstract void UpdateIsBinding();

    protected internal abstract string? GetListName(ArrayList? listAccessors);

    public abstract void SuspendBinding();

    public abstract void ResumeBinding();

    protected void PullData() => PullData(out _);

    internal void PullData(out bool success)
    {
        success = true;
        _pullingData = true;

        try
        {
            UpdateIsBinding();

            var numLinks = Bindings.Count;
            for (var i = 0; i < numLinks; i++)
            {
                if (Bindings[i].PullData())
                {
                    success = false;
                }
            }
        }
        finally
        {
            _pullingData = false;
        }
    }

    protected void PushData()
    {
        if (_pullingData)
        {
            return;
        }

        UpdateIsBinding();

        var numLinks = Bindings.Count;
        for (var i = 0; i < numLinks; i++)
        {
            Bindings[i].PushData();
        }
    }

    internal abstract object? DataSource { get; }

    internal abstract bool IsBinding { get; }

    public bool IsBindingSuspended => !IsBinding;

    public abstract int Count { get; }

    private void Binding_BindingComplete(object? sender, BindingCompleteEventArgs args)
    {
        OnBindingComplete(args);
    }
}