using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Windows.Forms;

public partial class PropertyGrid
{

    public event ComponentRenameEventHandler? ComComponentNameChanged;

    public event EventHandler SelectedObjectsChanged
    {
        add => Events.AddHandler(s_selectedObjectsChangedEvent, value);
        remove => Events.RemoveHandler(s_selectedObjectsChangedEvent, value);
    }

    public event SelectedGridItemChangedEventHandler SelectedGridItemChanged
    {
        add => Events.AddHandler(s_selectedGridItemChangedEvent, value);
        remove => Events.RemoveHandler(s_selectedGridItemChangedEvent, value);
    }

    public event EventHandler PropertySortChanged
    {
        add => Events.AddHandler(s_propertySortChangedEvent, value);
        remove => Events.RemoveHandler(s_propertySortChangedEvent, value);
    }

    public event PropertyValueChangedEventHandler PropertyValueChanged
    {
        add => Events.AddHandler(s_propertyValueChangedEvent, value);
        remove => Events.RemoveHandler(s_propertyValueChangedEvent, value);
    }

    public event PropertyTabChangedEventHandler PropertyTabChanged
    {
        add => Events.AddHandler(s_propertyTabChangedEvent, value);
        remove => Events.RemoveHandler(s_propertyTabChangedEvent, value);
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public new event EventHandler MouseLeave
    {
        add => base.MouseLeave += value;
        remove => base.MouseLeave -= value;
    }

    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public new event EventHandler MouseEnter
    {
        add => base.MouseEnter += value;
        remove => base.MouseEnter -= value;
    }
}