using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
    [DefaultEvent("CollectionChanged")]
    internal class ListManagerBindingsCollection : BindingsCollection
    {
        private BindingManagerBase bindingManagerBase;

        internal ListManagerBindingsCollection(BindingManagerBase bindingManagerBase)
        {
            this.bindingManagerBase = bindingManagerBase;
        }

        protected override void AddCore(Binding dataBinding)
        {
            if (dataBinding == null)
            {
                throw new ArgumentNullException("dataBinding");
            }
            if (dataBinding.BindingManagerBase == this.bindingManagerBase)
            {
                throw new ArgumentException(nameof(dataBinding));
            }
            if (dataBinding.BindingManagerBase != null)
            {
                throw new ArgumentException(nameof(dataBinding));
            }
            dataBinding.SetListManager(this.bindingManagerBase);
            base.AddCore(dataBinding);
        }

        protected override void ClearCore()
        {
            int count = this.Count;
            for (int i = 0; i < count; i++)
            {
                base[i].SetListManager(null);
            }
            base.ClearCore();
        }

        protected override void RemoveCore(Binding dataBinding)
        {
            if (dataBinding.BindingManagerBase != this.bindingManagerBase)
            {
                throw new ArgumentException(nameof(dataBinding));
            }
            dataBinding.SetListManager(null);
            base.RemoveCore(dataBinding);
        }
    }
}