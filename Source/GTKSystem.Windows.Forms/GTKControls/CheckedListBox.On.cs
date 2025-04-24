using Gtk;

namespace System.Windows.Forms;

public partial class CheckedListBox
{
    protected internal virtual void OnItemCheck(object? sender, ItemCheckEventArgs e)
    {
        ItemCheck?.Invoke(sender, e);
    }

    protected virtual void OnSelectedItemChanged(ChildActivatedArgs args)
    {
        SelectedItemChanged?.Invoke(this, args);
    }

    protected virtual void OnSelectedValueChanged(ChildActivatedArgs args)
    {
        SelectedValueChanged?.Invoke(this, args);
    }

    protected virtual void OnSelectedIndexChanged(ChildActivatedArgs args)
    {
        SelectedIndexChanged?.Invoke(this, args);
    }

}

