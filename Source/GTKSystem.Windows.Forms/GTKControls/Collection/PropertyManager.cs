using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Maintains a <see cref="T:System.Windows.Forms.Binding" /> between an object's property and a data-bound control property.</summary>
/// <filterpriority>2</filterpriority>
public class PropertyManager : BindingManagerBase
{
    private object dataSource;

    private string propName;

    private PropertyDescriptor propInfo;

    private bool bound;

    internal override Type BindType
    {
        get
        {
            return this.dataSource.GetType();
        }
    }

    /// <returns>The number of rows managed by the <see cref="T:System.Windows.Forms.BindingManagerBase" />.</returns>
    /// <filterpriority>1</filterpriority>
    public override int Count
    {
        get
        {
            return 1;
        }
    }

    /// <summary>Gets the object to which the data-bound property belongs.</summary>
    /// <returns>An <see cref="T:System.Object" /> that represents the object to which the property belongs.</returns>
    /// <filterpriority>1</filterpriority>
    public override object Current
    {
        get
        {
            return this.dataSource;
        }
    }

    internal override object DataSource
    {
        get
        {
            return this.dataSource;
        }
    }

    internal override bool IsBinding
    {
        get
        {
            return this.dataSource != null;
        }
    }

    /// <returns>A zero-based index that specifies a position in the underlying list.</returns>
    /// <filterpriority>1</filterpriority>
    public override int Position
    {
        get
        {
            return 0;
        }
        set
        {
        }
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.PropertyManager" /> class.</summary>
    public PropertyManager()
    {
    }

    internal PropertyManager(object dataSource) : base(dataSource)
    {
    }

    internal PropertyManager(object dataSource, string propName)
    {
        this.propName = propName;
        this.SetDataSource(dataSource);
    }

    /// <filterpriority>1</filterpriority>
    public override void AddNew()
    {
        throw new NotSupportedException("DataBindingAddNewNotSupportedOnPropertyManager");
    }

    /// <filterpriority>1</filterpriority>
    public override void CancelCurrentEdit()
    {
        IEditableObject current = this.Current as IEditableObject;
        if (current != null)
        {
            current.CancelEdit();
        }
        base.PushData();
    }

    /// <filterpriority>1</filterpriority>
    public override void EndCurrentEdit()
    {
        bool flag;
        base.PullData(out flag);
        if (flag)
        {
            IEditableObject current = this.Current as IEditableObject;
            if (current != null)
            {
                current.EndEdit();
            }
        }
    }

    internal override PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
    {
        return ListBindingHelper.GetListItemProperties(this.dataSource, listAccessors);
    }

    internal override string GetListName()
    {
        return string.Concat(TypeDescriptor.GetClassName(this.dataSource), ".", this.propName);
    }

    /// <returns>The name of the list supplying the data for the binding.</returns>
    /// <param name="listAccessors">An <see cref="T:System.Collections.ArrayList" /> containing the table's bound properties. </param>
    protected internal override string GetListName(ArrayList listAccessors)
    {
        return "";
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.CurrentChanged" /> event.</summary>
    /// <param name="ea">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
    protected internal override void OnCurrentChanged(EventArgs ea)
    {
        base.PushData();
        if (this.onCurrentChangedHandler != null)
        {
            this.onCurrentChangedHandler(this, ea);
        }
        if (this.onCurrentItemChangedHandler != null)
        {
            this.onCurrentItemChangedHandler(this, ea);
        }
    }

    /// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.CurrentItemChanged" /> event.</summary>
    /// <param name="ea">An <see cref="T:System.EventArgs" /> containing the event data.</param>
    protected internal override void OnCurrentItemChanged(EventArgs ea)
    {
        base.PushData();
        if (this.onCurrentItemChangedHandler != null)
        {
            this.onCurrentItemChangedHandler(this, ea);
        }
    }

    private void PropertyChanged(object sender, EventArgs ea)
    {
        this.EndCurrentEdit();
        this.OnCurrentChanged(EventArgs.Empty);
    }

    /// <param name="index">The index of the row to delete. </param>
    /// <filterpriority>1</filterpriority>
    public override void RemoveAt(int index)
    {
        throw new NotSupportedException("DataBindingRemoveAtNotSupportedOnPropertyManager");
    }

    /// <filterpriority>1</filterpriority>
    public override void ResumeBinding()
    {
        this.OnCurrentChanged(new EventArgs());
        if (!this.bound)
        {
            try
            {
                this.bound = true;
                this.UpdateIsBinding();
            }
            catch
            {
                this.bound = false;
                this.UpdateIsBinding();
                throw;
            }
        }
    }

    private protected override void SetDataSource(object dataSource)
    {
        if (this.dataSource != null && !string.IsNullOrEmpty(this.propName))
        {
            this.propInfo.RemoveValueChanged(this.dataSource, new EventHandler(this.PropertyChanged));
            this.propInfo = null;
        }
        this.dataSource = dataSource;
        if (this.dataSource != null && !string.IsNullOrEmpty(this.propName))
        {
            this.propInfo = TypeDescriptor.GetProperties(dataSource).Find(this.propName, true);
            if (this.propInfo == null)
            {
                throw new ArgumentException("PropertyManagerPropDoesNotExist");
            }
            this.propInfo.AddValueChanged(dataSource, new EventHandler(this.PropertyChanged));
        }
    }

    /// <summary>Suspends the data binding between a data source and a data-bound property.</summary>
    /// <filterpriority>1</filterpriority>
    public override void SuspendBinding()
    {
        this.EndCurrentEdit();
        if (this.bound)
        {
            try
            {
                this.bound = false;
                this.UpdateIsBinding();
            }
            catch
            {
                this.bound = true;
                this.UpdateIsBinding();
                throw;
            }
        }
    }

    /// <summary>Updates the current <see cref="T:System.Windows.Forms.Binding" /> between a data binding and a data-bound property.</summary>
    protected override void UpdateIsBinding()
    {
        for (int i = 0; i < base.Bindings.Count; i++)
        {
            base.Bindings[i].UpdateIsBinding();
        }
    }
}