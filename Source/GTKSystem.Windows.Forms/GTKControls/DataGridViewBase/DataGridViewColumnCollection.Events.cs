using System.ComponentModel;

namespace System.Windows.Forms;

public partial class DataGridViewColumnCollection : List<DataGridViewColumn>
{
    public event CollectionChangeEventHandler? CollectionChanged;
}
