using Gtk;

namespace System.Windows.Forms;

public partial class CheckedListBox
{
    protected internal virtual void OnItemCheck(object? sender, ItemCheckEventArgs e)
    {
        EventInvoke(()=>ItemCheck?.Invoke(sender, e), false);
    }

    protected virtual void OnSelectedItemChanged(ChildActivatedArgs args)
    {
        EventInvoke(() => SelectedItemChanged?.Invoke(this, args), false);
    }

    protected virtual void OnSelectedValueChanged(ChildActivatedArgs args)
    {
        EventInvoke(() => SelectedValueChanged?.Invoke(this, args), false);
    }

    protected virtual void OnSelectedIndexChanged(ChildActivatedArgs args)
    {
        EventInvoke(() => SelectedIndexChanged?.Invoke(this, args), false);
    }

}

