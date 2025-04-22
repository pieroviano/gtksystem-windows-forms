using System.Runtime.Serialization;

namespace System.Windows.Forms;

public partial class ListViewGroup
{
    public event EventHandler<ListViewSetEventArgs>? ListViewSet;
}