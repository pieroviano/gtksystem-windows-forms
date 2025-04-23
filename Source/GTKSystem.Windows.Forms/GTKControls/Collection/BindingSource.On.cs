using System.ComponentModel;
using GtkApplication = System.Windows.Forms.Application;

namespace System.Windows.Forms;

public partial class BindingSource
{
    protected virtual void OnAddingNew(AddingNewEventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            var item = (AddingNewEventHandler?)Events[EventAddingNew];
            if (item != null)
            {
                item(this, e);
            }
        }, GtkApplication.UseAsyncLoad);
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.BindingComplete" /> event. </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" />  that contains the event data. </param>
    protected virtual void OnBindingComplete(BindingCompleteEventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            var item = (BindingCompleteEventHandler?)Events[EventBindingComplete];
            if (item != null)
            {
                item(this, e);
            }
        }, GtkApplication.UseAsyncLoad);
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.CurrentChanged" /> event.</summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected virtual void OnCurrentChanged(EventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            UnhookItemChangedEventsForOldCurrent();
            HookItemChangedEventsForNewCurrent();
            var item = (EventHandler?)Events[EventCurrentChanged];
            if (item != null)
            {
                item(this, e);
            }
        }, GtkApplication.UseAsyncLoad);
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.CurrentItemChanged" /> event.</summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected virtual void OnCurrentItemChanged(EventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            var item = (EventHandler?)Events[EventCurrentItemChanged];
            if (item != null)
            {
                item(this, e);
            }
        }, GtkApplication.UseAsyncLoad);
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.DataError" /> event.</summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.BindingManagerDataErrorEventArgs" /> that contains the event data. </param>
    protected virtual void OnDataError(BindingManagerDataErrorEventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            if (Events[EventDataError] is BindingManagerDataErrorEventHandler item)
            {
                item(this, e);
            }
        }, GtkApplication.UseAsyncLoad);
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.DataMemberChanged" /> event.</summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected virtual void OnDataMemberChanged(EventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            if (Events[EventDataMemberChanged] is EventHandler item)
            {
                item(this, e);
            }
        }, GtkApplication.UseAsyncLoad);
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.DataSourceChanged" /> event.</summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected virtual void OnDataSourceChanged(EventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            if (Events[EventDataSourceChanged] is EventHandler item)
            {
                item(this, e);
            }
        }, GtkApplication.UseAsyncLoad);
    }

    private void OnInitialized()
    {
        GtkApplication.EventInvoke(() =>
        {
            var item = (EventHandler?)Events[EventInitialized];
            if (item != null)
            {
                item(this, EventArgs.Empty);
            }
        }, GtkApplication.UseAsyncLoad);
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.ListChanged" /> event.</summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected virtual void OnListChanged(ListChangedEventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            if (!_raiseListChangedEvents || _initializing)
            {
                return;
            }
            var item = (ListChangedEventHandler?)Events[EventListChanged];
            if (item != null)
            {
                item(this, e);
            }
        }, GtkApplication.UseAsyncLoad);
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.BindingSource.PositionChanged" /> event.</summary>
    /// <param name="e">A <see cref="T:System.ComponentModel.ListChangedEventArgs" /> that contains the event data.</param>
    protected virtual void OnPositionChanged(EventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            var item = (EventHandler?)Events[EventPositionChanged];
            if (item != null)
            {
                item(this, e);
            }
        }, GtkApplication.UseAsyncLoad);
    }

    private void OnSimpleListChanged(ListChangedType listChangedType, int newIndex)
    {
        GtkApplication.EventInvoke(() =>
        {
            if (!_isBindingList)
            {
                OnListChanged(new ListChangedEventArgs(listChangedType, newIndex));
            }
        }, GtkApplication.UseAsyncLoad);
    }

}