using System.ComponentModel;

namespace System.Windows.Forms;

using GtkApplication=System.Windows.Forms.Application;

public partial class AutoCompleteStringCollection
{
    protected void OnCollectionChanged(CollectionChangeEventArgs e)
    {
        GtkApplication.EventInvoke(() => CollectionChanged?.Invoke(this, e), GtkApplication.UseAsyncLoad);
    }
}
