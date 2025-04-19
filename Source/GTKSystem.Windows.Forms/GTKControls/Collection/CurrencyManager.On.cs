using System.ComponentModel;
using GtkApplication = System.Windows.Forms.Application;

namespace System.Windows.Forms;

public partial class CurrencyManager
{
    protected virtual void OnCurrencyChanged(EventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            OnCurrentChangedHandler?.Invoke(this, e);
        }, GtkApplication.UseAsyncInvoke);
    }

    /// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected internal override void OnCurrentItemChanged(EventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            OnCurrentItemChangedHandler?.Invoke(this, e);
        }, GtkApplication.UseAsyncInvoke);
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.CurrencyManager.ItemChanged" /> event.</summary>
    /// <param name="e">An <see cref="T:System.Windows.Forms.ItemChangedEventArgs" /> that contains the event data. </param>
    protected virtual void OnItemChanged(ItemChangedEventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            var flag = false;
            if ((e.Index == ListPosition || e.Index == -1 && Position < Count) && !_inChangeRecordState)
            {
                flag = CurrencyManager_PushData();
            }
            try
            {
                _itemChanged?.Invoke(this, e);
            }
            catch (Exception exception)
            {
                OnDataError(exception);
            }
            if (flag)
            {
                OnPositionChanged(EventArgs.Empty);
            }
        }, GtkApplication.UseAsyncInvoke);
    }

    protected virtual void OnListChanged(ListChangedEventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            _listChanged?.Invoke(this, e);
        }, GtkApplication.UseAsyncInvoke);
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.CurrencyManager.MetaDataChanged" /> event.</summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
    protected internal virtual void OnMetaDataChanged(EventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            _metaDataChanged?.Invoke(this, e);
        }, GtkApplication.UseAsyncInvoke);
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.PositionChanged" /> event.</summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
    protected virtual void OnPositionChanged(EventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            try
            {
                OnPositionChangedHandler?.Invoke(this, e);
            }
            catch (Exception exception)
            {
                OnDataError(exception);
            }
        }, GtkApplication.UseAsyncInvoke);
    }
}