using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Windows.Forms
{
    internal class RelatedCurrencyManager : CurrencyManager
    {
        private BindingManagerBase parentManager;

        private string dataField;

        private PropertyDescriptor fieldInfo;

        private static List<BindingManagerBase> IgnoreItemChangedTable;

        static RelatedCurrencyManager()
        {
            RelatedCurrencyManager.IgnoreItemChangedTable = new List<BindingManagerBase>();
        }

        internal RelatedCurrencyManager(BindingManagerBase parentManager, string dataField) : base(null)
        {
            this.Bind(parentManager, dataField);
        }

        internal void Bind(BindingManagerBase parentManager, string dataField)
        {
            this.UnwireParentManager(this.parentManager);
            this.parentManager = parentManager;
            this.dataField = dataField;
            this.fieldInfo = parentManager.GetItemProperties().Find(dataField, true);
            if (this.fieldInfo == null || !typeof(IList).IsAssignableFrom(this.fieldInfo.PropertyType))
            {
                throw new ArgumentException("RelatedListManagerChild");
            }
            this.finalType = this.fieldInfo.PropertyType;
            this.WireParentManager(this.parentManager);
            this.ParentManager_CurrentItemChanged(parentManager, EventArgs.Empty);
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

        public override PropertyDescriptorCollection GetItemProperties()
        {
            return this.GetItemProperties(null);
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
            if (RelatedCurrencyManager.IgnoreItemChangedTable.Contains(this.parentManager))
            {
                return;
            }
            int num = this.listposition;
            try
            {
                base.PullData();
            }
            catch (Exception exception)
            {
                base.OnDataError(exception);
            }
            if (!(this.parentManager is CurrencyManager))
            {
                this.SetDataSource(this.fieldInfo.GetValue(this.parentManager.Current));
                this.listposition = (this.Count > 0 ? 0 : -1);
            }
            else
            {
                CurrencyManager currencyManager = (CurrencyManager)this.parentManager;
                if (currencyManager.Count <= 0)
                {
                    currencyManager.AddNew();
                    try
                    {
                        RelatedCurrencyManager.IgnoreItemChangedTable.Add(currencyManager);
                        currencyManager.CancelCurrentEdit();
                    }
                    finally
                    {
                        if (RelatedCurrencyManager.IgnoreItemChangedTable.Contains(currencyManager))
                        {
                            RelatedCurrencyManager.IgnoreItemChangedTable.Remove(currencyManager);
                        }
                    }
                }
                else
                {
                    this.SetDataSource(this.fieldInfo.GetValue(currencyManager.Current));
                    this.listposition = (this.Count > 0 ? 0 : -1);
                }
            }
            if (num != this.listposition)
            {
                this.OnPositionChanged(EventArgs.Empty);
            }
            this.OnCurrentChanged(EventArgs.Empty);
            this.OnCurrentItemChanged(EventArgs.Empty);
        }

        private void ParentManager_MetaDataChanged(object sender, EventArgs e)
        {
            base.OnMetaDataChanged(e);
        }

        private void UnwireParentManager(BindingManagerBase bmb)
        {
            if (bmb != null)
            {
                bmb.CurrentItemChanged -= new EventHandler(this.ParentManager_CurrentItemChanged);
                if (bmb is CurrencyManager)
                {
                    (bmb as CurrencyManager).MetaDataChanged -= new EventHandler(this.ParentManager_MetaDataChanged);
                }
            }
        }

        private void WireParentManager(BindingManagerBase bmb)
        {
            if (bmb != null)
            {
                bmb.CurrentItemChanged += new EventHandler(this.ParentManager_CurrentItemChanged);
                if (bmb is CurrencyManager)
                {
                    (bmb as CurrencyManager).MetaDataChanged += new EventHandler(this.ParentManager_MetaDataChanged);
                }
            }
        }
    }
}