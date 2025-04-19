using System.ComponentModel;
using GtkApplication = System.Windows.Forms.Application;

namespace System.Windows.Forms;

public partial class BindingsCollection
{
    protected virtual void OnCollectionChanging(CollectionChangeEventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            _collectionChanging?.Invoke(this, e);
        }, GtkApplication.UseAsyncInvoke);
    }

    protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            _collectionChanged?.Invoke(this, e);
        }, GtkApplication.UseAsyncInvoke);
    }

}