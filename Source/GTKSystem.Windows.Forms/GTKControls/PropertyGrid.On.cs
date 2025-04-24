using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Windows.Forms;

public partial class PropertyGrid
{
    protected virtual void OnComComponentNameChanged(ComponentRenameEventArgs e)
    {
        ComComponentNameChanged?.Invoke(this, e);
    }
}