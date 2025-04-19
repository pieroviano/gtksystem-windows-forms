using System.ComponentModel;

namespace System.Windows.Forms;

public partial class DataGridViewRowCollection
{
    protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
    {
        if (CollectionChanged != null)
            CollectionChanged(dataGridView, e);
    }
}