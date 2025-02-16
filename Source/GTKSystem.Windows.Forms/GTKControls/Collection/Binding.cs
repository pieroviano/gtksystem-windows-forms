using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;

namespace System.Windows.Forms
{
    /// <summary>Represents the simple binding between the property value of an object and the property value of a control.</summary>
    /// <filterpriority>1</filterpriority>
    public class Binding
    {
        private IBindableComponent control;

        private BindingManagerBase bindingManagerBase;

        private BindToObject bindToObject;

        private string propertyName = "";

        private PropertyDescriptor propInfo;

        private PropertyDescriptor propIsNullInfo;

        private EventDescriptor validateInfo;

        private TypeConverter propInfoConverter;

        private bool formattingEnabled = true;

        private bool bound;

        private bool modified;

        private bool inSetPropValue;

        private bool inPushOrPull;

        private bool inOnBindingComplete;

        private string formatString = string.Empty;

        private IFormatProvider formatInfo;

        private object nullValue;

        private object dsNullValue = Formatter.GetDefaultDataSourceNullValue(null);

        private bool dsNullValueSet;

        private ConvertEventHandler onParse;

        private ConvertEventHandler onFormat;

        private ControlUpdateMode controlUpdateMode;

        private DataSourceUpdateMode dataSourceUpdateMode;

        private BindingCompleteEventHandler onComplete;

        /// <summary>Gets the control the <see cref="T:System.Windows.Forms.Binding" /> is associated with.</summary>
        /// <returns>The <see cref="T:System.Windows.Forms.IBindableComponent" /> the <see cref="T:System.Windows.Forms.Binding" /> is associated with.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
        /// </PermissionSet>
        [DefaultValue(null)]
        public IBindableComponent BindableComponent
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                return this.control;
            }
            internal set => this.control = value;
        }

        /// <summary>Gets the <see cref="T:System.Windows.Forms.BindingManagerBase" /> for this <see cref="T:System.Windows.Forms.Binding" />.</summary>
        /// <returns>The <see cref="T:System.Windows.Forms.BindingManagerBase" /> that manages this <see cref="T:System.Windows.Forms.Binding" />.</returns>
        /// <filterpriority>1</filterpriority>
        public BindingManagerBase BindingManagerBase
        {
            get
            {
                return this.bindingManagerBase;
            }
        }

        /// <summary>Gets an object that contains information about this binding based on the <paramref name="dataMember" /> parameter in the <see cref="Overload:System.Windows.Forms.Binding.#ctor" /> constructor.</summary>
        /// <returns>A <see cref="T:System.Windows.Forms.BindingMemberInfo" /> that contains information about this <see cref="T:System.Windows.Forms.Binding" />.</returns>
        /// <filterpriority>1</filterpriority>
        public BindingMemberInfo BindingMemberInfo
        {
            get
            {
                return this.bindToObject.BindingMemberInfo;
            }
        }

        internal BindToObject BindToObject
        {
            get
            {
                return this.bindToObject;
            }
        }

        internal bool ComponentCreated
        {
            get
            {
                return Binding.IsComponentCreated(this.control);
            }
        }

        /// <summary>Gets the control that the binding belongs to.</summary>
        /// <returns>The <see cref="T:System.Windows.Forms.Control" /> that the binding belongs to.</returns>
        /// <filterpriority>1</filterpriority>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
        /// </PermissionSet>
        [DefaultValue(null)]
        public Control Control
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                return this.control as Control;
            }
            internal set => this.control=value;
        }

        /// <summary>Gets or sets when changes to the data source are propagated to the bound control property.</summary>
        /// <returns>One of the <see cref="T:System.Windows.Forms.ControlUpdateMode" /> values. The default is <see cref="F:System.Windows.Forms.ControlUpdateMode.OnPropertyChanged" />.</returns>
        [DefaultValue(ControlUpdateMode.OnPropertyChanged)]
        public ControlUpdateMode ControlUpdateMode
        {
            get
            {
                return this.controlUpdateMode;
            }
            set
            {
                if (this.controlUpdateMode != value)
                {
                    this.controlUpdateMode = value;
                    if (this.IsBinding)
                    {
                        this.PushData();
                    }
                }
            }
        }

        /// <summary>Gets the data source for this binding.</summary>
        /// <returns>An <see cref="T:System.Object" /> that represents the data source.</returns>
        /// <filterpriority>1</filterpriority>
        public object DataSource
        {
            get
            {
                return this.bindToObject.DataSource;
            }
        }

        /// <summary>Gets or sets the value to be stored in the data source if the control value is null or empty.</summary>
        /// <returns>The <see cref="T:System.Object" /> to be stored in the data source when the control property is empty or null. The default is <see cref="T:System.DBNull" /> for value types and null for non-value types.</returns>
        public object DataSourceNullValue
        {
            get
            {
                return this.dsNullValue;
            }
            set
            {
                if (!object.Equals(this.dsNullValue, value))
                {
                    object obj = this.dsNullValue;
                    this.dsNullValue = value;
                    this.dsNullValueSet = true;
                    if (this.IsBinding)
                    {
                        object obj1 = this.bindToObject.GetValue();
                        if (Formatter.IsNullData(obj1, obj))
                        {
                            this.WriteValue();
                        }
                        if (Formatter.IsNullData(obj1, value))
                        {
                            this.ReadValue();
                        }
                    }
                }
            }
        }

        /// <summary>Gets or sets a value that indicates when changes to the bound control property are propagated to the data source.</summary>
        /// <returns>A value that indicates when changes are propagated. The default is <see cref="F:System.Windows.Forms.DataSourceUpdateMode.OnValidation" />.</returns>
        [DefaultValue(DataSourceUpdateMode.OnValidation)]
        public DataSourceUpdateMode DataSourceUpdateMode
        {
            get
            {
                return this.dataSourceUpdateMode;
            }
            set
            {
                if (this.dataSourceUpdateMode != value)
                {
                    this.dataSourceUpdateMode = value;
                }
            }
        }

        /// <summary>Gets or sets the <see cref="T:System.IFormatProvider" /> that provides custom formatting behavior.</summary>
        /// <returns>The <see cref="T:System.IFormatProvider" /> implementation that provides custom formatting behavior.</returns>
        /// <filterpriority>1</filterpriority>
        [DefaultValue(null)]
        public IFormatProvider FormatInfo
        {
            get
            {
                return this.formatInfo;
            }
            set
            {
                if (this.formatInfo != value)
                {
                    this.formatInfo = value;
                    if (this.IsBinding)
                    {
                        this.PushData();
                    }
                }
            }
        }

        /// <summary>Gets or sets the format specifier characters that indicate how a value is to be displayed.</summary>
        /// <returns>The string of format specifier characters that indicate how a value is to be displayed.</returns>
        /// <filterpriority>1</filterpriority>
        public string FormatString
        {
            get
            {
                return this.formatString;
            }
            set
            {
                if (value == null)
                {
                    value = string.Empty;
                }
                if (!value.Equals(this.formatString))
                {
                    this.formatString = value;
                    if (this.IsBinding)
                    {
                        this.PushData();
                    }
                }
            }
        }

        /// <summary>Gets or sets a value indicating whether type conversion and formatting is applied to the control property data.</summary>
        /// <returns>true if type conversion and formatting of control property data is enabled; otherwise, false. The default is false.</returns>
        /// <filterpriority>1</filterpriority>
        public bool FormattingEnabled
        {
            get
            {
                return this.formattingEnabled;
            }
            set
            {
                if (this.formattingEnabled != value)
                {
                    this.formattingEnabled = value;
                    if (this.IsBinding)
                    {
                        this.PushData();
                    }
                }
            }
        } 

        internal bool IsBindable
        {
            get
            {
                if (this.control == null || this.propertyName.Length <= 0 || this.bindToObject.DataSource == null)
                {
                    return false;
                }
                return this.bindingManagerBase != null;
            }
        }

        /// <summary>Gets a value indicating whether the binding is active.</summary>
        /// <returns>true if the binding is active; otherwise, false.</returns>
        /// <filterpriority>1</filterpriority>
        public bool IsBinding
        {
            get
            {
                return this.bound;
            }
        }

        /// <summary>Gets or sets the <see cref="T:System.Object" /> to be set as the control property when the data source contains a <see cref="T:System.DBNull" /> value. </summary>
        /// <returns>The <see cref="T:System.Object" /> to be set as the control property when the data source contains a <see cref="T:System.DBNull" /> value. The default is null.</returns>
        /// <filterpriority>1</filterpriority>
        public object NullValue
        {
            get
            {
                return this.nullValue;
            }
            set
            {
                if (!object.Equals(this.nullValue, value))
                {
                    this.nullValue = value;
                    if (this.IsBinding && Formatter.IsNullData(this.bindToObject.GetValue(), this.dsNullValue))
                    {
                        this.PushData();
                    }
                }
            }
        }

        /// <summary>Gets or sets the name of the control's data-bound property.</summary>
        /// <returns>The name of a control property to bind to.</returns>
        /// <filterpriority>1</filterpriority>
        [DefaultValue("")]
        public string PropertyName
        {
            get
            {
                return this.propertyName;
            }
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class that simple-binds the indicated control property to the specified data member of the data source.</summary>
        /// <param name="propertyName">The name of the control property to bind. </param>
        /// <param name="dataSource">An <see cref="T:System.Object" /> that represents the data source. </param>
        /// <param name="dataMember">The property or list to bind to. </param>
        /// <exception cref="T:System.Exception">
        ///   <paramref name="propertyName" /> is neither a valid property of a control nor an empty string (""). </exception>
        /// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.</exception>
        public Binding(string propertyName, object dataSource, string dataMember) : this(propertyName, dataSource, dataMember, false, DataSourceUpdateMode.OnValidation, null, string.Empty, null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class that binds the indicated control property to the specified data member of the data source, and optionally enables formatting to be applied.</summary>
        /// <param name="propertyName">The name of the control property to bind. </param>
        /// <param name="dataSource">An <see cref="T:System.Object" /> that represents the data source. </param>
        /// <param name="dataMember">The property or list to bind to. </param>
        /// <param name="formattingEnabled">true to format the displayed data; otherwise, false. </param>
        /// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.-or-The property given is a read-only property.</exception>
        /// <exception cref="T:System.Exception">Formatting is disabled and <paramref name="propertyName" /> is neither a valid property of a control nor an empty string (""). </exception>
        public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled) : this(propertyName, dataSource, dataMember, formattingEnabled, DataSourceUpdateMode.OnValidation, null, string.Empty, null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class that binds the specified control property to the specified data member of the specified data source. Optionally enables formatting and propagates values to the data source based on the specified update setting.</summary>
        /// <param name="propertyName">The name of the control property to bind. </param>
        /// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source. </param>
        /// <param name="dataMember">The property or list to bind to.</param>
        /// <param name="formattingEnabled">true to format the displayed data; otherwise, false.</param>
        /// <param name="dataSourceUpdateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
        /// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.-or-The data source or data member or control property specified are associated with another binding in the collection.</exception>
        public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode) : this(propertyName, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, null, string.Empty, null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class that binds the indicated control property to the specified data member of the specified data source. Optionally enables formatting, propagates values to the data source based on the specified update setting, and sets the property to the specified value when a <see cref="T:System.DBNull" /> is returned from the data source.</summary>
        /// <param name="propertyName">The name of the control property to bind. </param>
        /// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source. </param>
        /// <param name="dataMember">The property or list to bind to.</param>
        /// <param name="formattingEnabled">true to format the displayed data; otherwise, false.</param>
        /// <param name="dataSourceUpdateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
        /// <param name="nullValue">The <see cref="T:System.Object" /> to be applied to the bound control property if the data source value is <see cref="T:System.DBNull" />.</param>
        /// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.-or-The data source or data member or control property specified are associated with another binding in the collection.</exception>
        public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue) : this(propertyName, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, nullValue, string.Empty, null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class that binds the specified control property to the specified data member of the specified data source. Optionally enables formatting with the specified format string; propagates values to the data source based on the specified update setting; and sets the property to the specified value when a <see cref="T:System.DBNull" /> is returned from the data source.</summary>
        /// <param name="propertyName">The name of the control property to bind. </param>
        /// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source. </param>
        /// <param name="dataMember">The property or list to bind to.</param>
        /// <param name="formattingEnabled">true to format the displayed data; otherwise, false.</param>
        /// <param name="dataSourceUpdateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
        /// <param name="nullValue">The <see cref="T:System.Object" /> to be applied to the bound control property if the data source value is <see cref="T:System.DBNull" />.</param>
        /// <param name="formatString">One or more format specifier characters that indicate how a value is to be displayed.</param>
        /// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.-or-The data source or data member or control property specified are associated with another binding in the collection.</exception>
        public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue, string formatString) : this(propertyName, dataSource, dataMember, formattingEnabled, dataSourceUpdateMode, nullValue, formatString, null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Binding" /> class with the specified control property to the specified data member of the specified data source. Optionally enables formatting with the specified format string; propagates values to the data source based on the specified update setting; enables formatting with the specified format string; sets the property to the specified value when a <see cref="T:System.DBNull" /> is returned from the data source; and sets the specified format provider.</summary>
        /// <param name="propertyName">The name of the control property to bind. </param>
        /// <param name="dataSource">An <see cref="T:System.Object" /> representing the data source. </param>
        /// <param name="dataMember">The property or list to bind to.</param>
        /// <param name="formattingEnabled">true to format the displayed data; otherwise, false.</param>
        /// <param name="dataSourceUpdateMode">One of the <see cref="T:System.Windows.Forms.DataSourceUpdateMode" /> values.</param>
        /// <param name="nullValue">The <see cref="T:System.Object" /> to be applied to the bound control property if the data source value is <see cref="T:System.DBNull" />.</param>
        /// <param name="formatString">One or more format specifier characters that indicate how a value is to be displayed.</param>
        /// <param name="formatInfo">An implementation of <see cref="T:System.IFormatProvider" /> to override default formatting behavior.</param>
        /// <exception cref="T:System.ArgumentException">The property given by <paramref name="propertyName" /> does not exist on the control.-or-The data source or data member or control property specified are associated with another binding in the collection.</exception>
        public Binding(string propertyName, object dataSource, string dataMember, bool formattingEnabled, DataSourceUpdateMode dataSourceUpdateMode, object nullValue, string formatString, IFormatProvider formatInfo)
        {
            this.bindToObject = new BindToObject(this, dataSource, dataMember);
            this.propertyName = propertyName;
            this.formattingEnabled = formattingEnabled;
            this.formatString = formatString;
            this.nullValue = nullValue;
            this.formatInfo = formatInfo;
            this.formattingEnabled = formattingEnabled;
            this.dataSourceUpdateMode = dataSourceUpdateMode;
            this.CheckBinding();
        }

        private Binding()
        {
        }

        private void binding_MetaDataChanged(object sender, EventArgs e)
        {
            this.CheckBinding();
        }

        private void BindTarget(bool bind)
        {
            if (!bind)
            {
                if (this.propInfo != null && this.control != null)
                {
                    EventHandler eventHandler = new EventHandler(this.Target_PropertyChanged);
                    this.propInfo.RemoveValueChanged(this.control, eventHandler);
                }
                if (this.validateInfo != null)
                {
                    CancelEventHandler cancelEventHandler = new CancelEventHandler(this.Target_Validate);
                    this.validateInfo.RemoveEventHandler(this.control, cancelEventHandler);
                }
            }
            else if (this.IsBinding)
            {
                if (this.propInfo != null && this.control != null)
                {
                    EventHandler eventHandler1 = new EventHandler(this.Target_PropertyChanged);
                    this.propInfo.AddValueChanged(this.control, eventHandler1);
                }
                if (this.validateInfo != null)
                {
                    CancelEventHandler cancelEventHandler1 = new CancelEventHandler(this.Target_Validate);
                    this.validateInfo.AddEventHandler(this.control, cancelEventHandler1);
                    return;
                }
            }
        }

        private void CheckBinding()
        {
            PropertyDescriptorCollection propertyDescriptorCollections;
            this.bindToObject.CheckBinding();
            if (this.control == null || this.propertyName.Length <= 0)
            {
                this.propInfo = null;
                this.validateInfo = null;
            }
            else
            {
                this.control.DataBindings.CheckDuplicates(this);
                Type type = this.control.GetType();
                string str = string.Concat(this.propertyName, "IsNull");
                Type propertyType = null;
                PropertyDescriptor item = null;
                PropertyDescriptor propertyDescriptor = null;
                InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)TypeDescriptor.GetAttributes(this.control)[typeof(InheritanceAttribute)];
                propertyDescriptorCollections = (inheritanceAttribute == null || inheritanceAttribute.InheritanceLevel == InheritanceLevel.NotInherited ? TypeDescriptor.GetProperties(this.control) : TypeDescriptor.GetProperties(type));
                for (int i = 0; i < propertyDescriptorCollections.Count; i++)
                {
                    if (item == null && string.Equals(propertyDescriptorCollections[i].Name, this.propertyName, StringComparison.OrdinalIgnoreCase))
                    {
                        item = propertyDescriptorCollections[i];
                        if (propertyDescriptor != null)
                        {
                            break;
                        }
                    }
                    if (propertyDescriptor == null && string.Equals(propertyDescriptorCollections[i].Name, str, StringComparison.OrdinalIgnoreCase))
                    {
                        propertyDescriptor = propertyDescriptorCollections[i];
                        if (item != null)
                        {
                            break;
                        }
                    }
                }
                if (item == null)
                {
                    throw new ArgumentException("ListBindingBindProperty", "PropertyName");
                }
                if (item.IsReadOnly && this.controlUpdateMode != ControlUpdateMode.Never)
                {
                    throw new ArgumentException("ListBindingBindPropertyReadOnly", "PropertyName");
                }
                this.propInfo = item;
                propertyType = this.propInfo.PropertyType;
                this.propInfoConverter = this.propInfo.Converter;
                if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof(bool) && !propertyDescriptor.IsReadOnly)
                {
                    this.propIsNullInfo = propertyDescriptor;
                }
                EventDescriptor eventDescriptor = null;
                string str1 = "Validating";
                EventDescriptorCollection events = TypeDescriptor.GetEvents(this.control);
                int num = 0;
                while (num < events.Count)
                {
                    if (eventDescriptor != null || !string.Equals(events[num].Name, str1, StringComparison.OrdinalIgnoreCase))
                    {
                        num++;
                    }
                    else
                    {
                        eventDescriptor = events[num];
                        break;
                    }
                }
                this.validateInfo = eventDescriptor;
            }
            this.UpdateIsBinding();
        }

        internal bool ControlAtDesignTime()
        {
            IComponent component = this.control;
            ISite site = component?.Site;
            if (site == null)
            {
                return false;
            }
            return site.DesignMode;
        }

        private BindingCompleteEventArgs CreateBindingCompleteEventArgs(BindingCompleteContext context, Exception ex)
        {
            bool flag = false;
            string empty = string.Empty;
            BindingCompleteState bindingCompleteState = BindingCompleteState.Success;
            if (ex == null)
            {
                empty = this.BindToObject.DataErrorText;
                if (!string.IsNullOrEmpty(empty))
                {
                    bindingCompleteState = BindingCompleteState.DataError;
                }
            }
            else
            {
                empty = ex.Message;
                bindingCompleteState = BindingCompleteState.Exception;
                flag = true;
            }
            return new BindingCompleteEventArgs(this, bindingCompleteState, context, empty, ex, flag);
        }

        private object FormatObject(object value)
        {
            if (this.ControlAtDesignTime())
            {
                return value;
            }
            Type propertyType = this.propInfo.PropertyType;
            if (this.formattingEnabled)
            {
                ConvertEventArgs convertEventArg = new ConvertEventArgs(value, propertyType);
                this.OnFormat(convertEventArg);
                if (convertEventArg.Value != value)
                {
                    return convertEventArg.Value;
                }
                TypeConverter converter = null;
                if (this.bindToObject.FieldInfo != null)
                {
                    converter = this.bindToObject.FieldInfo.Converter;
                }
                return Formatter.FormatObject(value, propertyType, converter, this.propInfoConverter, this.formatString, this.formatInfo, this.nullValue, this.dsNullValue);
            }
            ConvertEventArgs convertEventArg1 = new ConvertEventArgs(value, propertyType);
            this.OnFormat(convertEventArg1);
            object obj = convertEventArg1.Value;
            if (propertyType == typeof(object))
            {
                return value;
            }
            if (obj != null && (obj.GetType().IsSubclassOf(propertyType) || obj.GetType() == propertyType))
            {
                return obj;
            }
            TypeConverter typeConverter = TypeDescriptor.GetConverter((value != null ? value.GetType() : typeof(object)));
            if (typeConverter != null && typeConverter.CanConvertTo(propertyType))
            {
                obj = typeConverter.ConvertTo(value, propertyType);
                return obj;
            }
            if (value is IConvertible)
            {
                obj = Convert.ChangeType(value, propertyType, CultureInfo.CurrentCulture);
                if (obj != null && (obj.GetType().IsSubclassOf(propertyType) || obj.GetType() == propertyType))
                {
                    return obj;
                }
            }
            throw new FormatException("ListBindingFormatFailed");
        }

        private void FormLoaded(object sender, EventArgs e)
        {
            this.CheckBinding();
        }

        private object GetDataSourceNullValue(Type type)
        {
            if (!this.dsNullValueSet)
            {
                return Formatter.GetDefaultDataSourceNullValue(type);
            }
            return this.dsNullValue;
        }

        private object GetPropValue()
        {
            object obj;
            bool value = false;
            if (this.propIsNullInfo != null)
            {
                value = (bool)this.propIsNullInfo.GetValue(this.control);
            }
            obj = (!value ? this.propInfo.GetValue(this.control) ?? this.DataSourceNullValue : this.DataSourceNullValue);
            return obj;
        }

        internal static bool IsComponentCreated(IBindableComponent component)
        {
            Control control = component as Control;
            if (control == null)
            {
                return true;
            }
            return control.Created;
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Binding.BindingComplete" /> event. </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" />  that contains the event data. </param>
        protected virtual void OnBindingComplete(BindingCompleteEventArgs e)
        {
            if (!this.inOnBindingComplete)
            {
                try
                {
                    try
                    {
                        this.inOnBindingComplete = true;
                        this.onComplete?.Invoke(this, e);
                    }
                    catch (Exception exception)
                    {
                        if (ClientUtils.IsSecurityOrCriticalException(exception))
                        {
                            throw;
                        }
                        e.Cancel = true;
                    }
                }
                finally
                {
                    this.inOnBindingComplete = false;
                }
            }
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Binding.Format" /> event.</summary>
        /// <param name="cevent">A <see cref="T:System.Windows.Forms.ConvertEventArgs" /> that contains the event data. </param>
        protected virtual void OnFormat(ConvertEventArgs cevent)
        {
            this.onFormat?.Invoke(this, cevent);
            if (!this.formattingEnabled && !(cevent.Value is DBNull) && cevent.DesiredType != null && !cevent.DesiredType.IsInstanceOfType(cevent.Value) && cevent.Value is IConvertible)
            {
                cevent.Value = Convert.ChangeType(cevent.Value, cevent.DesiredType, CultureInfo.CurrentCulture);
            }
        }

        /// <summary>Raises the <see cref="E:System.Windows.Forms.Binding.Parse" /> event.</summary>
        /// <param name="cevent">A <see cref="T:System.Windows.Forms.ConvertEventArgs" /> that contains the event data. </param>
        protected virtual void OnParse(ConvertEventArgs cevent)
        {
            this.onParse?.Invoke(this, cevent);
            if (!this.formattingEnabled && !(cevent.Value is DBNull) && cevent.Value != null && cevent.DesiredType != null && !cevent.DesiredType.IsInstanceOfType(cevent.Value) && cevent.Value is IConvertible)
            {
                cevent.Value = Convert.ChangeType(cevent.Value, cevent.DesiredType, CultureInfo.CurrentCulture);
            }
        }

        private object ParseObject(object value)
        {
            Type bindToType = this.bindToObject.BindToType;
            if (this.formattingEnabled)
            {
                ConvertEventArgs convertEventArg = new ConvertEventArgs(value, bindToType);
                this.OnParse(convertEventArg);
                object obj = convertEventArg.Value;
                if (!object.Equals(value, obj))
                {
                    return obj;
                }
                TypeConverter converter = null;
                if (this.bindToObject.FieldInfo != null)
                {
                    converter = this.bindToObject.FieldInfo.Converter;
                }
                return Formatter.ParseObject(value, bindToType, (value == null ? this.propInfo.PropertyType : value.GetType()), converter, this.propInfoConverter, this.formatInfo, this.nullValue, this.GetDataSourceNullValue(bindToType));
            }
            ConvertEventArgs convertEventArg1 = new ConvertEventArgs(value, bindToType);
            this.OnParse(convertEventArg1);
            if (convertEventArg1.Value != null && (convertEventArg1.Value.GetType().IsSubclassOf(bindToType) || convertEventArg1.Value.GetType() == bindToType || convertEventArg1.Value is DBNull))
            {
                return convertEventArg1.Value;
            }
            TypeConverter typeConverter = TypeDescriptor.GetConverter((value != null ? value.GetType() : typeof(object)));
            if (typeConverter != null && typeConverter.CanConvertTo(bindToType))
            {
                return typeConverter.ConvertTo(value, bindToType);
            }
            if (value is IConvertible)
            {
                object obj1 = Convert.ChangeType(value, bindToType, CultureInfo.CurrentCulture);
                if (obj1 != null && (obj1.GetType().IsSubclassOf(bindToType) || obj1.GetType() == bindToType))
                {
                    return obj1;
                }
            }
            return null;
        }

        internal bool PullData()
        {
            return this.PullData(true, false);
        }

        internal bool PullData(bool reformat)
        {
            return this.PullData(reformat, false);
        }

        internal bool PullData(bool reformat, bool force)
        {
            if (this.ControlUpdateMode == ControlUpdateMode.Never)
            {
                reformat = false;
            }
            bool flag = false;
            object value = null;
            Exception exception = null;
            if (!this.IsBinding)
            {
                return false;
            }
            if (!force)
            {
                if (this.propInfo.SupportsChangeEvents && !this.modified)
                {
                    return false;
                }
                if (this.DataSourceUpdateMode == DataSourceUpdateMode.Never)
                {
                    return false;
                }
            }
            if (this.inPushOrPull && this.formattingEnabled)
            {
                return false;
            }
            this.inPushOrPull = true;
            object propValue = this.GetPropValue();
            try
            {
                value = this.ParseObject(propValue);
            }
            catch (Exception exception1)
            {
                exception = exception1;
            }
            try
            {
                try
                {
                    if (exception != null || !this.FormattingEnabled && value == null)
                    {
                        flag = true;
                        value = this.bindToObject.GetValue();
                    }
                    if (reformat && !this.FormattingEnabled | !flag)
                    {
                        object obj = this.FormatObject(value);
                        if (force || !this.FormattingEnabled || !object.Equals(obj, propValue))
                        {
                            this.SetPropValue(obj);
                        }
                    }
                    if (!flag)
                    {
                        this.bindToObject.SetValue(value);
                    }
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    if (!this.FormattingEnabled)
                    {
                        throw;
                    }
                }
            }
            finally
            {
                this.inPushOrPull = false;
            }
            if (!this.FormattingEnabled)
            {
                this.modified = false;
                return false;
            }
            BindingCompleteEventArgs bindingCompleteEventArg = this.CreateBindingCompleteEventArgs(BindingCompleteContext.DataSourceUpdate, exception);
            this.OnBindingComplete(bindingCompleteEventArg);
            if (bindingCompleteEventArg.BindingCompleteState == BindingCompleteState.Success && !bindingCompleteEventArg.Cancel)
            {
                this.modified = false;
            }
            return bindingCompleteEventArg.Cancel;
        }

        internal bool PushData()
        {
            return this.PushData(false);
        }

        internal bool PushData(bool force)
        {
            Exception exception = null;
            if (!force && this.ControlUpdateMode == ControlUpdateMode.Never)
            {
                return false;
            }
            if (this.inPushOrPull && this.formattingEnabled)
            {
                return false;
            }
            this.inPushOrPull = true;
            try
            {
                try
                {
                    if (!this.IsBinding)
                    {
                        this.SetPropValue(null);
                    }
                    else
                    {
                        this.SetPropValue(this.FormatObject(this.bindToObject.GetValue()));
                        this.modified = false;
                    }
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    if (!this.FormattingEnabled)
                    {
                        throw;
                    }
                }
            }
            finally
            {
                this.inPushOrPull = false;
            }
            if (!this.FormattingEnabled)
            {
                return false;
            }
            BindingCompleteEventArgs bindingCompleteEventArg = this.CreateBindingCompleteEventArgs(BindingCompleteContext.ControlUpdate, exception);
            this.OnBindingComplete(bindingCompleteEventArg);
            return bindingCompleteEventArg.Cancel;
        }

        /// <summary>Sets the control property to the value read from the data source.</summary>
        public void ReadValue()
        {
            this.PushData(true);
        }

        internal void SetBindableComponent(IBindableComponent value)
        {
            BindingContext bindingContext;
            if (this.control != value)
            {
                IBindableComponent bindableComponent = this.control;
                this.BindTarget(false);
                this.control = value;
                this.BindTarget(true);
                try
                {
                    this.CheckBinding();
                }
                catch
                {
                    this.BindTarget(false);
                    this.control = bindableComponent;
                    this.BindTarget(true);
                    throw;
                }
                if (this.control == null || !Binding.IsComponentCreated(this.control))
                {
                    bindingContext = null;
                }
                else
                {
                    bindingContext = this.control.BindingContext;
                }
                BindingContext.UpdateBinding(bindingContext, this);
                Form form = value as Form;
                if (form != null)
                {
                    form.Load += new EventHandler(this.FormLoaded);
                }
            }
        }

        internal void SetListManager(BindingManagerBase bindingManagerBase)
        {
            if (this.bindingManagerBase is CurrencyManager)
            {
                ((CurrencyManager)this.bindingManagerBase).MetaDataChanged -= new EventHandler(this.binding_MetaDataChanged);
            }
            this.bindingManagerBase = bindingManagerBase;
            if (this.bindingManagerBase is CurrencyManager)
            {
                ((CurrencyManager)this.bindingManagerBase).MetaDataChanged += new EventHandler(this.binding_MetaDataChanged);
            }
            this.BindToObject.SetBindingManagerBase(bindingManagerBase);
            this.CheckBinding();
        }

        private void SetPropValue(object value)
        {
            if (this.ControlAtDesignTime())
            {
                return;
            }
            this.inSetPropValue = true;
            try
            {
                if ((value == null ? false : !Formatter.IsNullData(value, this.DataSourceNullValue)))
                {
                    this.propInfo.SetValue(this.control, value);
                }
                else if (this.propIsNullInfo != null)
                {
                    this.propIsNullInfo.SetValue(this.control, true);
                }
                else if (this.propInfo.PropertyType != typeof(object))
                {
                    this.propInfo.SetValue(this.control, null);
                }
                else
                {
                    this.propInfo.SetValue(this.control, this.DataSourceNullValue);
                }
            }
            finally
            {
                this.inSetPropValue = false;
            }
        }

        private bool ShouldSerializeDataSourceNullValue()
        {
            if (!this.dsNullValueSet)
            {
                return false;
            }
            return this.dsNullValue != Formatter.GetDefaultDataSourceNullValue(null);
        }

        private bool ShouldSerializeFormatString()
        {
            if (this.formatString == null)
            {
                return false;
            }
            return this.formatString.Length > 0;
        }

        private bool ShouldSerializeNullValue()
        {
            return this.nullValue != null;
        }

        private void Target_PropertyChanged(object sender, EventArgs e)
        {
            if (this.inSetPropValue)
            {
                return;
            }
            if (this.IsBinding)
            {
                this.modified = true;
                if (this.DataSourceUpdateMode == DataSourceUpdateMode.OnPropertyChanged)
                {
                    this.PullData(false);
                    this.modified = true;
                }
            }
        }

        private void Target_Validate(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.PullData(true))
                {
                    e.Cancel = true;
                }
            }
            catch
            {
                e.Cancel = true;
            }
        }

        internal void UpdateIsBinding()
        {
            bool flag = (!this.IsBindable || !this.ComponentCreated ? false : this.bindingManagerBase.IsBinding);
            if (this.bound != flag)
            {
                this.bound = flag;
                this.BindTarget(flag);
                if (this.bound)
                {
                    if (this.controlUpdateMode == ControlUpdateMode.Never)
                    {
                        this.PullData(false, true);
                        return;
                    }
                    this.PushData();
                }
            }
        }

        /// <summary>Reads the current value from the control property and writes it to the data source.</summary>
        public void WriteValue()
        {
            this.PullData(true, true);
        }

        /// <summary>Occurs when the <see cref="P:System.Windows.Forms.Binding.FormattingEnabled" /> property is set to true and a binding operation is complete, such as when data is pushed from the control to the data source or vice versa</summary>
        public event BindingCompleteEventHandler? BindingComplete
        {
            add
            {
                this.onComplete += value;
            }
            remove
            {
                this.onComplete -= value;
            }
        }

        /// <summary>Occurs when the property of a control is bound to a data value.</summary>
        /// <filterpriority>1</filterpriority>
        public event ConvertEventHandler? Format
        {
            add
            {
                this.onFormat += value;
            }
            remove
            {
                this.onFormat -= value;
            }
        }

        /// <summary>Occurs when the value of a data-bound control changes.</summary>
        /// <filterpriority>1</filterpriority>
        public event ConvertEventHandler? Parse
        {
            add
            {
                this.onParse += value;
            }
            remove
            {
                this.onParse -= value;
            }
        }
    }
}