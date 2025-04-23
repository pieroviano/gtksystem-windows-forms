using Gtk;

namespace System.Windows.Forms;

public partial class CheckedListBox
{
    protected internal virtual void OnItemCheck(object? sender, ItemCheckEventArgs e)
    {
        EventInvoke(()=>ItemCheck?.Invoke(sender, e));
    }

    protected virtual void OnSelectedItemChanged(ChildActivatedArgs args)
    {
        EventInvoke(() => SelectedItemChanged?.Invoke(this, args));
    }

    protected virtual void OnSelectedValueChanged(ChildActivatedArgs args)
    {
        EventInvoke(() => SelectedValueChanged?.Invoke(this, args));
    }

    protected virtual void OnSelectedIndexChanged(ChildActivatedArgs args)
    {
        EventInvoke(() => SelectedIndexChanged?.Invoke(this, args));
    }

}

