// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel;
using GtkApplication = System.Windows.Forms.Application;

namespace System.Windows.Forms;

public abstract partial class BindingManagerBase
{
    protected internal virtual void OnBindingComplete(BindingCompleteEventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            _bindingCompleteEventHandler?.Invoke(this, e);
        }, GtkApplication.UseAsyncLoad);
    }

    protected internal abstract void OnCurrentChanged(EventArgs e);

    protected internal abstract void OnCurrentItemChanged(EventArgs e);

    protected internal virtual void OnDataError(Exception e)
    {
        GtkApplication.EventInvoke(() =>
        {
            _dataError?.Invoke(this, new BindingManagerDataErrorEventArgs(e));
        }, GtkApplication.UseAsyncLoad);
    }

    /// <summary>
    ///  BindingComplete events on individual Bindings are propagated up through the BindingComplete event on
    ///  the owning BindingManagerBase. To do this, we have to track changes to the bindings collection, adding
    ///  or removing handlers on items in the collection as appropriate.
    ///
    ///  For the Add and Remove cases, we hook the collection 'changed' event, and add or remove handler for
    ///  specific binding.
    ///
    ///  For the Refresh case, we hook both the 'changing' and 'changed' events, removing handlers for all
    ///  items that were in the collection before the change, then adding handlers for whatever items are
    ///  in the collection after the change.
    /// </summary>
    protected virtual void OnBindingsCollectionChanged(object? sender, CollectionChangeEventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            if (e.Element is not Binding binding)
            {
                return;
            }

            switch (e.Action)
            {
                case CollectionChangeAction.Add:
                    binding.BindingComplete += Binding_BindingComplete;
                    break;
                case CollectionChangeAction.Remove:
                    binding.BindingComplete -= Binding_BindingComplete;
                    break;
                case CollectionChangeAction.Refresh:
                    foreach (Binding bi in Bindings)
                    {
                        bi.BindingComplete += Binding_BindingComplete;
                    }

                    break;
            }
        }, GtkApplication.UseAsyncLoad);
    }

    protected virtual void OnBindingsCollectionChanging(object? sender, CollectionChangeEventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            if (e.Action != CollectionChangeAction.Refresh)
            {
                return;
            }

            foreach (Binding bi in Bindings)
            {
                bi.BindingComplete -= Binding_BindingComplete;
            }
        }, GtkApplication.UseAsyncLoad);
    }
}

