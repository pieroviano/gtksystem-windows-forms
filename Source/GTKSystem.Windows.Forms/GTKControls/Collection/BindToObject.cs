using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
    internal class BindToObject
    {
        private PropertyDescriptor fieldInfo;

        private BindingMemberInfo dataMember;

        private object dataSource;

        private BindingManagerBase bindingManager;

        private Binding owner;

        private string errorText = string.Empty;

        private bool dataSourceInitialized;

        private bool waitingOnDataSource;

        internal BindingManagerBase BindingManagerBase
        {
            get
            {
                return this.bindingManager;
            }
        }

        internal BindingMemberInfo BindingMemberInfo
        {
            get
            {
                return this.dataMember;
            }
        }

        internal Type BindToType
        {
            get
            {
                if (this.dataMember.BindingField.Length != 0)
                {
                    if (this.fieldInfo == null)
                    {
                        return null;
                    }
                    return this.fieldInfo.PropertyType;
                }
                Type bindType = this.bindingManager.BindType;
                if (typeof(Array).IsAssignableFrom(bindType))
                {
                    bindType = bindType.GetElementType();
                }
                return bindType;
            }
        }

        internal string DataErrorText
        {
            get
            {
                return this.errorText;
            }
        }

        internal object DataSource
        {
            get
            {
                return this.dataSource;
            }
        }

        internal PropertyDescriptor FieldInfo
        {
            get
            {
                return this.fieldInfo;
            }
        }

        private bool IsDataSourceInitialized
        {
            get
            {
                if (this.dataSourceInitialized)
                {
                    return true;
                }
                ISupportInitializeNotification supportInitializeNotification = this.dataSource as ISupportInitializeNotification;
                if (supportInitializeNotification == null || supportInitializeNotification.IsInitialized)
                {
                    this.dataSourceInitialized = true;
                    return true;
                }
                if (this.waitingOnDataSource)
                {
                    return false;
                }
                supportInitializeNotification.Initialized += new EventHandler(this.DataSource_Initialized);
                this.waitingOnDataSource = true;
                return false;
            }
        }

        internal BindToObject(Binding owner, object dataSource, string dataMember)
        {
            this.owner = owner;
            this.dataSource = dataSource;
            this.dataMember = new BindingMemberInfo(dataMember);
            this.CheckBinding();
        }

        internal void CheckBinding()
        {
            if (this.owner != null && this.owner.BindableComponent != null && this.owner.ControlAtDesignTime())
            {
                return;
            }
            if (this.owner.BindingManagerBase != null && this.fieldInfo != null && this.owner.BindingManagerBase.IsBinding && !(this.owner.BindingManagerBase is CurrencyManager))
            {
                this.fieldInfo.RemoveValueChanged(this.owner.BindingManagerBase.Current, new EventHandler(this.PropValueChanged));
            }
            if (this.owner == null || this.owner.BindingManagerBase == null || this.owner.BindableComponent == null || !this.owner.ComponentCreated || !this.IsDataSourceInitialized)
            {
                this.fieldInfo = null;
            }
            else
            {
                string bindingField = this.dataMember.BindingField;
                this.fieldInfo = this.owner.BindingManagerBase.GetItemProperties().Find(bindingField, true);
                if (this.owner.BindingManagerBase.DataSource != null && this.fieldInfo == null && bindingField.Length > 0)
                {
                    throw new ArgumentException("ListBindingBindField", "dataMember");
                }
                if (this.fieldInfo != null && this.owner.BindingManagerBase.IsBinding && !(this.owner.BindingManagerBase is CurrencyManager))
                {
                    this.fieldInfo.AddValueChanged(this.owner.BindingManagerBase.Current, new EventHandler(this.PropValueChanged));
                    return;
                }
            }
        }

        private void DataSource_Initialized(object sender, EventArgs e)
        {
            ISupportInitializeNotification supportInitializeNotification = this.dataSource as ISupportInitializeNotification;
            if (supportInitializeNotification != null)
            {
                supportInitializeNotification.Initialized -= new EventHandler(this.DataSource_Initialized);
            }
            this.waitingOnDataSource = false;
            this.dataSourceInitialized = true;
            this.CheckBinding();
        }

        private string GetErrorText(object value)
        {
            IDataErrorInfo dataErrorInfo = value as IDataErrorInfo;
            string empty = string.Empty;
            if (dataErrorInfo != null)
            {
                empty = (this.fieldInfo != null ? dataErrorInfo[this.fieldInfo.Name] : dataErrorInfo.Error);
            }
            return empty ?? string.Empty;
        }

        internal object GetValue()
        {
            object current = this.bindingManager.Current;
            this.errorText = this.GetErrorText(current);
            if (this.fieldInfo != null)
            {
                current = this.fieldInfo.GetValue(current);
            }
            return current;
        }

        private void PropValueChanged(object sender, EventArgs e)
        {
            if (this.bindingManager != null)
            {
                this.bindingManager.OnCurrentChanged(EventArgs.Empty);
            }
        }

        internal void SetBindingManagerBase(BindingManagerBase lManager)
        {
            if (this.bindingManager == lManager)
            {
                return;
            }
            if (this.bindingManager != null && this.fieldInfo != null && this.bindingManager.IsBinding && !(this.bindingManager is CurrencyManager))
            {
                this.fieldInfo.RemoveValueChanged(this.bindingManager.Current, new EventHandler(this.PropValueChanged));
                this.fieldInfo = null;
            }
            this.bindingManager = lManager;
            this.CheckBinding();
        }

        internal void SetValue(object value)
        {
            object current = null;
            if (this.fieldInfo == null)
            {
                CurrencyManager currencyManager = this.bindingManager as CurrencyManager;
                if (currencyManager != null)
                {
                    currencyManager[currencyManager.Position] = value;
                    current = value;
                }
            }
            else
            {
                current = this.bindingManager.Current;
                if (current is IEditableObject)
                {
                    ((IEditableObject)current).BeginEdit();
                }
                if (!this.fieldInfo.IsReadOnly)
                {
                    this.fieldInfo.SetValue(current, value);
                }
            }
            this.errorText = this.GetErrorText(current);
        }
    }
}