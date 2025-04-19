using System.ComponentModel;

namespace System.Windows.Forms;

public partial class DataGridViewColumnCollection : List<DataGridViewColumn>
{
    protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
    {
        if (CollectionChanged != null)
            CollectionChanged(owner, e);
    }
}