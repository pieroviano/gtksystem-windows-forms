// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace System.Windows.Forms;

public abstract partial class BindingManagerBase
{
    protected EventHandler? OnCurrentChangedHandler; // Don't rename (breaking change)

    protected EventHandler? OnCurrentItemChangedHandler; // Don't rename (breaking change)

    protected EventHandler? OnPositionChangedHandler; // Don't rename (breaking change)

    // Hook BindingComplete events on all owned Binding objects, and propagate those events through our own BindingComplete event
    private BindingCompleteEventHandler? _bindingCompleteEventHandler;

    // Event handler for the DataError event
    private BindingManagerDataErrorEventHandler? _dataError;

    // same deal about the new currentItemChanged event
    private protected EventHandler? OnCurrentItemChangedValueHandler;

    public event BindingCompleteEventHandler? BindingComplete
    {
        add => _bindingCompleteEventHandler += value;
        remove => _bindingCompleteEventHandler -= value;
    }

    public event EventHandler? CurrentChanged
    {
        add => OnCurrentChangedHandler += value;
        remove => OnCurrentChangedHandler -= value;
    }

    public event EventHandler? CurrentItemChanged
    {
        add => OnCurrentItemChangedValueHandler += value;
        remove => OnCurrentItemChangedValueHandler -= value;
    }

    public event BindingManagerDataErrorEventHandler? DataError
    {
        add => _dataError += value;
        remove => _dataError -= value;
    }

    public event EventHandler? PositionChanged
    {
        add => OnPositionChangedHandler += value;
        remove => OnPositionChangedHandler -= value;
    }
}