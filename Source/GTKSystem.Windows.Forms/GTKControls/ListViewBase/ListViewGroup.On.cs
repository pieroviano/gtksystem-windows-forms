using System.Runtime.Serialization;
using GtkApplication = System.Windows.Forms.Application;

namespace System.Windows.Forms;

public partial class ListViewGroup
{
    protected virtual void OnListViewSet(ListViewSetEventArgs e)
    {
        GtkApplication.EventInvoke(() =>
        {
            ListViewSet?.Invoke(this, e);
        }, false);
    }

}