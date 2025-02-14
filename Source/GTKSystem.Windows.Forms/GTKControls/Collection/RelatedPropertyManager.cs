using System;
using System.Collections;
using System.ComponentModel;

namespace System.Windows.Forms
{
    internal class RelatedPropertyManager : PropertyManager
    {
        private BindingManagerBase parentManager;

        private string dataField;

        private PropertyDescriptor fieldInfo;

        internal override Type BindType
        {
            get
            {
                return this.fieldInfo.PropertyType;
            }
        }

        public override object Current
        {
            get
            {
                if (this.DataSource == null)
                {
                    return null;
                }
                return this.fieldInfo.GetValue(this.DataSource);
            }
        }

        internal RelatedPropertyManager(BindingManagerBase parentManager, string dataField) : base(RelatedPropertyManager.GetCurrentOrNull(parentManager), dataField)
        {
            this.Bind(parentManager, dataField);
        }

        private void Bind(BindingManagerBase parentManager, string dataField)
        {
            this.parentManager = parentManager;
            this.dataField = dataField;
            this.fieldInfo = parentManager.GetItemProperties().Find(dataField, true);
            if (this.fieldInfo == null)
            {
                throw new ArgumentException("RelatedListManagerChild");
            }
            parentManager.CurrentItemChanged += new EventHandler(this.ParentManager_CurrentItemChanged);
            this.Refresh();
        }

        private static object GetCurrentOrNull(BindingManagerBase parentManager)
        {
            if ((parentManager.Position < 0 ? true : parentManager.Position >= parentManager.Count))
            {
                return null;
            }
            return parentManager.Current;
        }

        internal override PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            PropertyDescriptor[] propertyDescriptorArray;
            if (listAccessors == null || listAccessors.Length == 0)
            {
                propertyDescriptorArray = new PropertyDescriptor[1];
            }
            else
            {
                propertyDescriptorArray = new PropertyDescriptor[(int)listAccessors.Length + 1];
                listAccessors.CopyTo(propertyDescriptorArray, 1);
            }
            propertyDescriptorArray[0] = this.fieldInfo;
            return this.parentManager.GetItemProperties(propertyDescriptorArray);
        }

        internal override string GetListName()
        {
            string listName = this.GetListName(new ArrayList());
            if (listName.Length > 0)
            {
                return listName;
            }
            return base.GetListName();
        }

        protected internal override string GetListName(ArrayList listAccessors)
        {
            listAccessors.Insert(0, this.fieldInfo);
            return this.parentManager.GetListName(listAccessors);
        }

        private void ParentManager_CurrentItemChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void Refresh()
        {
            this.EndCurrentEdit();
            this.SetDataSource(RelatedPropertyManager.GetCurrentOrNull(this.parentManager));
            this.OnCurrentChanged(EventArgs.Empty);
        }
    }
}