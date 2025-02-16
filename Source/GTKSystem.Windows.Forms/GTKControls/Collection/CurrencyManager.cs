using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
    /// <summary>Manages a list of <see cref="T:System.Windows.Forms.Binding" /> objects.</summary>
    /// <filterpriority>2</filterpriority>
    public class CurrencyManager : BindingManagerBase
    {
        private object dataSource;

        private IList list;

        private bool bound;

        private bool shouldBind = true;

        /// <summary>Specifies the current position of the <see cref="T:System.Windows.Forms.CurrencyManager" /> in the list.</summary>
        protected int listposition = -1;

        private int lastGoodKnownRow = -1;

        private bool pullingData;

        private bool inChangeRecordState;

        private bool suspendPushDataInCurrentChanged;

        private ItemChangedEventHandler onItemChanged;

        private ListChangedEventHandler onListChanged;

        private ItemChangedEventArgs resetEvent = new ItemChangedEventArgs(-1);

        private EventHandler onMetaDataChangedHandler;

        /// <summary>Specifies the data type of the list.</summary>
        protected Type finalType;

        internal bool AllowAdd
        {
            get
            {
                if (this.list is IBindingList)
                {
                    return ((IBindingList)this.list).AllowNew;
                }
                if (this.list == null)
                {
                    return false;
                }
                if (this.list.IsReadOnly)
                {
                    return false;
                }
                return !this.list.IsFixedSize;
            }
        }

        internal bool AllowEdit
        {
            get
            {
                if (this.list is IBindingList)
                {
                    return ((IBindingList)this.list).AllowEdit;
                }
                if (this.list == null)
                {
                    return false;
                }
                return !this.list.IsReadOnly;
            }
        }

        internal bool AllowRemove
        {
            get
            {
                if (this.list is IBindingList)
                {
                    return ((IBindingList)this.list).AllowRemove;
                }
                if (this.list == null)
                {
                    return false;
                }
                if (this.list.IsReadOnly)
                {
                    return false;
                }
                return !this.list.IsFixedSize;
            }
        }

        internal override Type BindType
        {
            get
            {
                return ListBindingHelper.GetListItemType(this.List);
            }
        }

        /// <summary>Gets the number of items in the list.</summary>
        /// <returns>The number of items in the list.</returns>
        /// <filterpriority>1</filterpriority>
        public override int Count
        {
            get
            {
                if (this.list == null)
                {
                    return 0;
                }
                return this.list.Count;
            }
        }

        /// <summary>Gets the current item in the list.</summary>
        /// <returns>A list item of type <see cref="T:System.Object" />.</returns>
        /// <filterpriority>1</filterpriority>
        public override object Current
        {
            get
            {
                return this[this.Position];
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
                return this.bound;
            }
        }

        internal object this[int index]
        {
            get
            {
                if (index < 0 || index >= this.list.Count)
                {
                    throw new IndexOutOfRangeException("ListManagerNoValue");
                }
                return this.list[index];
            }
            set
            {
                if (index < 0 || index >= this.list.Count)
                {
                    throw new IndexOutOfRangeException("ListManagerNoValue");
                }
                this.list[index] = value;
            }
        }

        /// <summary>Gets the list for this <see cref="T:System.Windows.Forms.CurrencyManager" />.</summary>
        /// <returns>An <see cref="T:System.Collections.IList" /> that contains the list.</returns>
        /// <filterpriority>1</filterpriority>
        public IList List
        {
            get
            {
                return this.list;
            }
        }

        /// <summary>Gets or sets the position you are at within the list.</summary>
        /// <returns>A number between 0 and <see cref="P:System.Windows.Forms.CurrencyManager.Count" /> minus 1.</returns>
        /// <filterpriority>1</filterpriority>
        public override int Position
        {
            get
            {
                return this.listposition;
            }
            set
            {
                if (this.listposition == -1)
                {
                    return;
                }
                if (value < 0)
                {
                    value = 0;
                }
                int count = this.list.Count;
                if (value >= count)
                {
                    value = count - 1;
                }
                this.ChangeRecordState(value, this.listposition != value, true, true, false);
            }
        }

        internal bool ShouldBind
        {
            get
            {
                return this.shouldBind;
            }
        }

        internal CurrencyManager(object dataSource)
        {
            this.SetDataSource(dataSource);
        }

        /// <summary>Adds a new item to the underlying list.</summary>
        /// <exception cref="T:System.NotSupportedException">The underlying data source does not implement <see cref="T:System.ComponentModel.IBindingList" />, or the data source has thrown an exception because the user has attempted to add a row to a read-only or fixed-size <see cref="T:System.Data.DataView" />. </exception>
        /// <filterpriority>1</filterpriority>
        public override void AddNew()
        {
            IBindingList bindingLists = this.list as IBindingList;
            if (bindingLists == null)
            {
                throw new NotSupportedException("CurrencyManagerCantAddNew");
            }
            bindingLists.AddNew();
            this.ChangeRecordState(this.list.Count - 1, this.Position != this.list.Count - 1, this.Position != this.list.Count - 1, true, true);
        }

        /// <summary>Cancels the current edit operation.</summary>
        /// <filterpriority>1</filterpriority>
        public override void CancelCurrentEdit()
        {
            if (this.Count > 0)
            {
                IEditableObject editableObject = (this.Position < 0 || this.Position >= this.list.Count ? null : this.list[this.Position]) as IEditableObject;
                editableObject?.CancelEdit();
                ICancelAddNew cancelAddNew = this.list as ICancelAddNew;
                cancelAddNew?.CancelNew(this.Position);
                this.OnItemChanged(new ItemChangedEventArgs(this.Position));
                if (this.Position != -1)
                {
                    this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, this.Position));
                }
            }
        }

        private void ChangeRecordState(int newPosition, bool validating, bool endCurrentEdit, bool firePositionChange, bool pullData)
        {
            if (newPosition == -1 && this.list.Count == 0)
            {
                if (this.listposition != -1)
                {
                    this.listposition = -1;
                    this.OnPositionChanged(EventArgs.Empty);
                }
                return;
            }
            if ((newPosition < 0 || newPosition >= this.Count) && this.IsBinding)
            {
                throw new IndexOutOfRangeException("ListManagerBadPosition");
            }
            int num = this.listposition;
            if (endCurrentEdit)
            {
                this.inChangeRecordState = true;
                try
                {
                    this.EndCurrentEdit();
                }
                finally
                {
                    this.inChangeRecordState = false;
                }
            }
            if (validating & pullData)
            {
                this.CurrencyManager_PullData();
            }
            this.listposition = Math.Min(newPosition, this.Count - 1);
            if (validating)
            {
                this.OnCurrentChanged(EventArgs.Empty);
            }
            if (num != this.listposition & firePositionChange)
            {
                this.OnPositionChanged(EventArgs.Empty);
            }
        }

        /// <summary>Throws an exception if there is no list, or the list is empty.</summary>
        /// <exception cref="T:System.Exception">There is no list, or the list is empty. </exception>
        protected void CheckEmpty()
        {
            if (this.dataSource == null || this.list == null || this.list.Count == 0)
            {
                throw new InvalidOperationException("ListManagerEmptyList");
            }
        }

        private bool CurrencyManager_PullData()
        {
            bool flag = true;
            this.pullingData = true;
            try
            {
                base.PullData(out flag);
            }
            finally
            {
                this.pullingData = false;
            }
            return flag;
        }

        private bool CurrencyManager_PushData()
        {
            if (this.pullingData)
            {
                return false;
            }
            int num = this.listposition;
            if (this.lastGoodKnownRow != -1)
            {
                try
                {
                    base.PushData();
                }
                catch (Exception exception)
                {
                    base.OnDataError(exception);
                    this.listposition = this.lastGoodKnownRow;
                    base.PushData();
                }
                this.lastGoodKnownRow = this.listposition;
            }
            else
            {
                try
                {
                    base.PushData();
                }
                catch (Exception exception1)
                {
                    base.OnDataError(exception1);
                    this.FindGoodRow();
                }
                this.lastGoodKnownRow = this.listposition;
            }
            return num != this.listposition;
        }

        /// <summary>Ends the current edit operation.</summary>
        /// <filterpriority>1</filterpriority>
        public override void EndCurrentEdit()
        {
            if (this.Count > 0 && this.CurrencyManager_PullData())
            {
                IEditableObject editableObject = (this.Position < 0 || this.Position >= this.list.Count ? null : this.list[this.Position]) as IEditableObject;
                editableObject?.EndEdit();
                ICancelAddNew cancelAddNew = this.list as ICancelAddNew;
                cancelAddNew?.EndNew(this.Position);
            }
        }

        internal int Find(PropertyDescriptor property, object key, bool keepIndex)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }
            if (property != null && this.list is IBindingList && ((IBindingList)this.list).SupportsSearching)
            {
                return ((IBindingList)this.list).Find(property, key);
            }
            if (property != null)
            {
                for (int i = 0; i < this.list.Count; i++)
                {
                    if (key.Equals(property.GetValue(this.list[i])))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private void FindGoodRow()
        {
            int count = this.list.Count;
            for (int i = 0; i < count; i++)
            {
                this.listposition = i;
                try
                {
                    base.PushData();
                    this.listposition = i;
                    return;
                }
                catch (Exception exception)
                {
                    base.OnDataError(exception);
                }
            }
            this.SuspendBinding();
            throw new InvalidOperationException("DataBindingPushDataException");
        }

        internal override PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            return ListBindingHelper.GetListItemProperties(this.list, listAccessors);
        }

        /// <summary>Gets the property descriptor collection for the underlying list.</summary>
        /// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> for the list.</returns>
        /// <filterpriority>1</filterpriority>
        public override PropertyDescriptorCollection GetItemProperties()
        {
            return this.GetItemProperties(null);
        }

        internal override string GetListName()
        {
            if (!(this.list is ITypedList))
            {
                return this.finalType.Name;
            }
            return ((ITypedList)this.list).GetListName(null);
        }

        /// <summary>Gets the name of the list supplying the data for the binding using the specified set of bound properties.</summary>
        /// <returns>If successful, a <see cref="T:System.String" /> containing name of the list supplying the data for the binding; otherwise, an <see cref="F:System.String.Empty" /> string.</returns>
        /// <param name="listAccessors">An <see cref="T:System.Collections.ArrayList" /> of properties to be found in the data source.</param>
        protected internal override string GetListName(ArrayList listAccessors)
        {
            if (!(this.list is ITypedList))
            {
                return "";
            }
            PropertyDescriptor[] propertyDescriptorArray = new PropertyDescriptor[listAccessors.Count];
            listAccessors.CopyTo(propertyDescriptorArray, 0);
            return ((ITypedList)this.list).GetListName(propertyDescriptorArray);
        }

        internal ListSortDirection GetSortDirection()
        {
            if (!(this.list is IBindingList) || !((IBindingList)this.list).SupportsSorting)
            {
                return ListSortDirection.Ascending;
            }
            return ((IBindingList)this.list).SortDirection;
        }

        internal PropertyDescriptor GetSortProperty()
        {
            if (!(this.list is IBindingList) || !((IBindingList)this.list).SupportsSorting)
            {
                return null;
            }
            return ((IBindingList)this.list).SortProperty;
        }

        private void List_ListChanged(object sender, ListChangedEventArgs e)
        {
            ListChangedEventArgs listChangedEventArg;
            if (e.ListChangedType != ListChangedType.ItemMoved || e.OldIndex >= 0)
            {
                listChangedEventArg = (e.ListChangedType != ListChangedType.ItemMoved || e.NewIndex >= 0 ? e : new ListChangedEventArgs(ListChangedType.ItemDeleted, e.OldIndex, e.NewIndex));
            }
            else
            {
                listChangedEventArg = new ListChangedEventArgs(ListChangedType.ItemAdded, e.NewIndex, e.OldIndex);
            }
            int num = this.listposition;
            this.UpdateLastGoodKnownRow(listChangedEventArg);
            this.UpdateIsBinding();
            if (this.list.Count == 0)
            {
                this.listposition = -1;
                if (num != -1)
                {
                    this.OnPositionChanged(EventArgs.Empty);
                    this.OnCurrentChanged(EventArgs.Empty);
                }
                if (listChangedEventArg.ListChangedType == ListChangedType.Reset && e.NewIndex == -1)
                {
                    this.OnItemChanged(this.resetEvent);
                }
                if (listChangedEventArg.ListChangedType == ListChangedType.ItemDeleted)
                {
                    this.OnItemChanged(this.resetEvent);
                }
                if (e.ListChangedType == ListChangedType.PropertyDescriptorAdded || e.ListChangedType == ListChangedType.PropertyDescriptorDeleted || e.ListChangedType == ListChangedType.PropertyDescriptorChanged)
                {
                    this.OnMetaDataChanged(EventArgs.Empty);
                }
                this.OnListChanged(listChangedEventArg);
                return;
            }
            this.suspendPushDataInCurrentChanged = true;
            try
            {
                switch (listChangedEventArg.ListChangedType)
                {
                    case ListChangedType.Reset:
                        {
                            if (this.listposition != -1 || this.list.Count <= 0)
                            {
                                this.ChangeRecordState(Math.Min(this.listposition, this.list.Count - 1), true, false, true, false);
                            }
                            else
                            {
                                this.ChangeRecordState(0, true, false, true, false);
                            }
                            this.UpdateIsBinding(false);
                            this.OnItemChanged(this.resetEvent);
                            break;
                        }
                    case ListChangedType.ItemAdded:
                        {
                            if (listChangedEventArg.NewIndex > this.listposition || this.listposition >= this.list.Count - 1)
                            {
                                if (listChangedEventArg.NewIndex == this.listposition && this.listposition == this.list.Count - 1 && this.listposition != -1)
                                {
                                    this.OnCurrentItemChanged(EventArgs.Empty);
                                }
                                if (this.listposition == -1)
                                {
                                    this.ChangeRecordState(0, false, false, true, false);
                                }
                                this.UpdateIsBinding();
                                this.OnItemChanged(this.resetEvent);
                                break;
                            }
                            else
                            {
                                this.ChangeRecordState(this.listposition + 1, true, true, this.listposition != this.list.Count - 2, false);
                                this.UpdateIsBinding();
                                this.OnItemChanged(this.resetEvent);
                                if (this.listposition != this.list.Count - 1)
                                {
                                    break;
                                }
                                this.OnPositionChanged(EventArgs.Empty);
                                break;
                            }
                        }
                    case ListChangedType.ItemDeleted:
                        {
                            if (listChangedEventArg.NewIndex == this.listposition)
                            {
                                this.ChangeRecordState(Math.Min(this.listposition, this.Count - 1), true, false, true, false);
                                this.OnItemChanged(this.resetEvent);
                                break;
                            }
                            else if (listChangedEventArg.NewIndex >= this.listposition)
                            {
                                this.OnItemChanged(this.resetEvent);
                                break;
                            }
                            else
                            {
                                this.ChangeRecordState(this.listposition - 1, true, false, true, false);
                                this.OnItemChanged(this.resetEvent);
                                break;
                            }
                        }
                    case ListChangedType.ItemMoved:
                        {
                            if (listChangedEventArg.OldIndex == this.listposition)
                            {
                                this.ChangeRecordState(listChangedEventArg.NewIndex, true, (this.Position <= -1 ? false : this.Position < this.list.Count), true, false);
                            }
                            else if (listChangedEventArg.NewIndex == this.listposition)
                            {
                                this.ChangeRecordState(listChangedEventArg.OldIndex, true, (this.Position <= -1 ? false : this.Position < this.list.Count), true, false);
                            }
                            this.OnItemChanged(this.resetEvent);
                            break;
                        }
                    case ListChangedType.ItemChanged:
                        {
                            if (listChangedEventArg.NewIndex == this.listposition)
                            {
                                this.OnCurrentItemChanged(EventArgs.Empty);
                            }
                            this.OnItemChanged(new ItemChangedEventArgs(listChangedEventArg.NewIndex));
                            break;
                        }
                    case ListChangedType.PropertyDescriptorAdded:
                    case ListChangedType.PropertyDescriptorDeleted:
                    case ListChangedType.PropertyDescriptorChanged:
                        {
                            this.lastGoodKnownRow = -1;
                            if (this.listposition == -1 && this.list.Count > 0)
                            {
                                this.ChangeRecordState(0, true, false, true, false);
                            }
                            else if (this.listposition > this.list.Count - 1)
                            {
                                this.ChangeRecordState(this.list.Count - 1, true, false, true, false);
                            }
                            this.OnMetaDataChanged(EventArgs.Empty);
                            break;
                        }
                }
                this.OnListChanged(listChangedEventArg);
            }
            finally
            {
                this.suspendPushDataInCurrentChanged = false;
            }
        }

        /// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected internal override void OnCurrentChanged(EventArgs e)
        {
            if (!this.inChangeRecordState)
            {
                int num = this.lastGoodKnownRow;
                bool flag = false;
                if (!this.suspendPushDataInCurrentChanged)
                {
                    flag = this.CurrencyManager_PushData();
                }
                if (this.Count > 0)
                {
                    object item = this.list[this.Position];
                    (item as IEditableObject)?.BeginEdit();
                }
                try
                {
                    if (!flag || flag && num != -1)
                    {
                        this.onCurrentChangedHandler?.Invoke(this, e);
                        this.onCurrentItemChangedHandler?.Invoke(this, e);
                    }
                }
                catch (Exception exception)
                {
                    base.OnDataError(exception);
                }
            }
        }

        /// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected internal override void OnCurrentItemChanged(EventArgs e)
        {
            this.onCurrentItemChangedHandler?.Invoke(this, e);
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.CurrencyManager.ItemChanged" /> event.</summary>
        /// <param name="e">An <see cref="T:System.Windows.Forms.ItemChangedEventArgs" /> that contains the event data. </param>
        protected virtual void OnItemChanged(ItemChangedEventArgs e)
        {
            bool flag = false;
            if ((e.Index == this.listposition || e.Index == -1 && this.Position < this.Count) && !this.inChangeRecordState)
            {
                flag = this.CurrencyManager_PushData();
            }
            try
            {
                this.onItemChanged?.Invoke(this, e);
            }
            catch (Exception exception)
            {
                base.OnDataError(exception);
            }
            if (flag)
            {
                this.OnPositionChanged(EventArgs.Empty);
            }
        }

        private void OnListChanged(ListChangedEventArgs e)
        {
            this.onListChanged?.Invoke(this, e);
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.CurrencyManager.MetaDataChanged" /> event.</summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
        protected internal void OnMetaDataChanged(EventArgs e)
        {
            this.onMetaDataChangedHandler?.Invoke(this, e);
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.BindingManagerBase.PositionChanged" /> event.</summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
        protected virtual void OnPositionChanged(EventArgs e)
        {
            try
            {
                this.onPositionChangedHandler?.Invoke(this, e);
            }
            catch (Exception exception)
            {
                base.OnDataError(exception);
            }
        }

        /// <summary>Forces a repopulation of the data-bound list.</summary>
        /// <filterpriority>1</filterpriority>
        public void Refresh()
        {
            if (this.list.Count <= 0)
            {
                this.listposition = -1;
            }
            else if (this.listposition >= this.list.Count)
            {
                this.lastGoodKnownRow = -1;
                this.listposition = 0;
            }
            this.List_ListChanged(this.list, new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        internal void Release()
        {
            this.UnwireEvents(this.list);
        }

        /// <summary>Removes the item at the specified index.</summary>
        /// <param name="index">The index of the item to remove from the list. </param>
        /// <exception cref="T:System.IndexOutOfRangeException">There is no row at the specified <paramref name="index" />. </exception>
        /// <filterpriority>1</filterpriority>
        public override void RemoveAt(int index)
        {
            this.list.RemoveAt(index);
        }

        /// <summary>Resumes data binding.</summary>
        /// <filterpriority>1</filterpriority>
        public override void ResumeBinding()
        {
            this.lastGoodKnownRow = -1;
            try
            {
                if (!this.shouldBind)
                {
                    this.shouldBind = true;
                    this.listposition = (this.list == null || this.list.Count == 0 ? -1 : 0);
                    this.UpdateIsBinding();
                }
            }
            catch
            {
                this.shouldBind = false;
                this.UpdateIsBinding();
                throw;
            }
        }

        private protected override void SetDataSource(object dataSource)
        {
            if (this.dataSource == dataSource)
            {
                return;
            }
            this.Release();
            this.dataSource = dataSource;
            this.list = null;
            this.finalType = null;
            object list = dataSource;
            if (list is Array)
            {
                this.finalType = list.GetType();
                list = (Array)list;
            }
            if (list is IListSource)
            {
                list = ((IListSource)list).GetList();
            }
            if (!(list is IList))
            {
                if (list != null)
                {
                    throw new ArgumentException("ListManagerSetDataSource");
                }
                throw new ArgumentNullException("dataSource");
            }
            if (this.finalType == null)
            {
                this.finalType = list.GetType();
            }
            this.list = (IList)list;
            this.WireEvents(this.list);
            if (this.list.Count <= 0)
            {
                this.listposition = -1;
            }
            else
            {
                this.listposition = 0;
            }
            this.OnItemChanged(this.resetEvent);
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1, -1));
            this.UpdateIsBinding();
        }

        internal void SetSort(PropertyDescriptor property, ListSortDirection sortDirection)
        {
            if (this.list is IBindingList && ((IBindingList)this.list).SupportsSorting)
            {
                ((IBindingList)this.list).ApplySort(property, sortDirection);
            }
        }

        /// <summary>Suspends data binding to prevents changes from updating the bound data source.</summary>
        /// <filterpriority>1</filterpriority>
        public override void SuspendBinding()
        {
            this.lastGoodKnownRow = -1;
            if (this.shouldBind)
            {
                this.shouldBind = false;
                this.UpdateIsBinding();
            }
        }

        internal void UnwireEvents(IList list)
        {
            if (list is IBindingList && ((IBindingList)list).SupportsChangeNotification)
            {
                ((IBindingList)list).ListChanged -= new ListChangedEventHandler(this.List_ListChanged);
            }
        }

        /// <summary>Updates the status of the binding.</summary>
        protected override void UpdateIsBinding()
        {
            this.UpdateIsBinding(true);
        }

        private void UpdateIsBinding(bool raiseItemChangedEvent)
        {
            bool flag = (this.list == null || this.list.Count <= 0 || !this.shouldBind ? false : this.listposition != -1);
            if (this.list != null && this.bound != flag)
            {
                this.bound = flag;
                int num = (flag ? 0 : -1);
                this.ChangeRecordState(num, this.bound, this.Position != num, true, false);
                int count = base.Bindings.Count;
                for (int i = 0; i < count; i++)
                {
                    base.Bindings[i].UpdateIsBinding();
                }
                if (raiseItemChangedEvent)
                {
                    this.OnItemChanged(this.resetEvent);
                }
            }
        }

        private void UpdateLastGoodKnownRow(ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.Reset:
                    {
                        this.lastGoodKnownRow = -1;
                        return;
                    }
                case ListChangedType.ItemAdded:
                    {
                        if (e.NewIndex > this.lastGoodKnownRow || this.lastGoodKnownRow >= this.List.Count - 1)
                        {
                            break;
                        }
                        this.lastGoodKnownRow++;
                        return;
                    }
                case ListChangedType.ItemDeleted:
                    {
                        if (e.NewIndex != this.lastGoodKnownRow)
                        {
                            break;
                        }
                        this.lastGoodKnownRow = -1;
                        return;
                    }
                case ListChangedType.ItemMoved:
                    {
                        if (e.OldIndex != this.lastGoodKnownRow)
                        {
                            break;
                        }
                        this.lastGoodKnownRow = e.NewIndex;
                        return;
                    }
                case ListChangedType.ItemChanged:
                    {
                        if (e.NewIndex != this.lastGoodKnownRow)
                        {
                            break;
                        }
                        this.lastGoodKnownRow = -1;
                        break;
                    }
                default:
                    {
                        return;
                    }
            }
        }

        internal void WireEvents(IList list)
        {
            if (list is IBindingList && ((IBindingList)list).SupportsChangeNotification)
            {
                ((IBindingList)list).ListChanged += new ListChangedEventHandler(this.List_ListChanged);
            }
        }

        /// <summary>Occurs when the current item has been altered.</summary>
        /// <filterpriority>1</filterpriority>
        public event ItemChangedEventHandler? ItemChanged
        {
            add
            {
                this.onItemChanged += value;
            }
            remove
            {
                this.onItemChanged -= value;
            }
        }

        /// <summary>Occurs when the list changes or an item in the list changes.</summary>
        /// <filterpriority>1</filterpriority>
        public event ListChangedEventHandler? ListChanged
        {
            add
            {
                this.onListChanged += value;
            }
            remove
            {
                this.onListChanged -= value;
            }
        }

        /// <summary>Occurs when the metadata of the <see cref="P:System.Windows.Forms.CurrencyManager.List" /> has changed.</summary>
        /// <filterpriority>1</filterpriority>
        public event EventHandler? MetaDataChanged
        {
            add
            {
                this.onMetaDataChangedHandler += value;
            }
            remove
            {
                this.onMetaDataChangedHandler -= value;
            }
        }
    }
}