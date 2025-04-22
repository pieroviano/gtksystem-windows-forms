using System.ComponentModel;

namespace System.Windows.Forms;

[ListBindable(false)]
public class ListViewGroupCollection : List<ListViewGroup>
{
    public ListView? ListView { get; internal set; }

    public ListViewGroup this[string key]
    {
        get
        {
            return Find(w => w.Name == key);
        }
        set
        {
            if (FindIndex(w => w.Name == key) > -1)
            {
                base[FindIndex(w => w.Name == key)] = value;
            }
        }
    }

    internal ListViewGroupCollection(ListView? listView)
    {
        ListView = listView;
    }

    public new int Add(ListViewGroup group)
    {
        if (ListView != null)
        {
            group.ListView = ListView;
        }

        foreach (var item in group.Items)
        {
            item.ListView = ListView;
        }

        AddCore(group);
        return Count;
    }
    public ListViewGroup Add(string key, string headerText)
    {
        var group = new ListViewGroup(key, headerText);
        Add(group);
        return group;
    }


    public void AddRange(ListViewGroup?[] groups)
    {
        foreach (var group in groups)
        {
            if (group != null)
            {
                group.ListView = ListView;
                AddCore(group);
            }
        }
    }

    public void AddRange(ListViewGroupCollection groups)
    {
        foreach (var group in groups)
        {
            if (group != null)
            {
                group.ListView = ListView;
                AddCore(group);
            }
        }
    }
    private void AddCore(ListViewGroup? group)
    {
        if (Exists(m => m.Name == group?.Name))
        {
            return;
        }

        if (group != null)
        {
            group.ListView = ListView;
            base.Add(group);

            ListView?.NativeGroupAdd(group, -1);
        }
    }

    public new void Clear()
    {
        foreach (var group in this)
        {
            group.ListView = null;
        }
        ListView?.NativeGroupsClear();
        base.Clear();
    }

    public bool Contains(string name)
    {
        return FindIndex(w => w.Name == name) > -1;
    }
}