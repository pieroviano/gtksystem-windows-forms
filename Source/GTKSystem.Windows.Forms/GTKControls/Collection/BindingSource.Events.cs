using System.ComponentModel;

namespace System.Windows.Forms;

public partial class BindingSource
{
    /// <summary>Occurs before an item is added to the underlying list.</summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="P:System.ComponentModel.AddingNewEventArgs.NewObject" /> is not the same type as the type contained in the list.</exception>
    [Category("CatData")]
    [Description("BindingSourceAddingNewEventHandlerDescr")]
    public event AddingNewEventHandler AddingNew
    {
        add => Events.AddHandler(EventAddingNew, value);
        remove => Events.RemoveHandler(EventAddingNew, value);
    }

    /// <summary>Occurs when all the clients have been bound to this <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
    [Category("CatData")]
    [Description("BindingSourceBindingCompleteEventHandlerDescr")]
    public event BindingCompleteEventHandler BindingComplete
    {
        add => Events.AddHandler(EventBindingComplete, value);
        remove => Events.RemoveHandler(EventBindingComplete, value);
    }

    /// <summary>Occurs when the currently bound item changes.</summary>
    [Category("CatData")]
    [Description("BindingSourceCurrentChangedEventHandlerDescr")]
    public event EventHandler CurrentChanged
    {
        add => Events.AddHandler(EventCurrentChanged, value);
        remove => Events.RemoveHandler(EventCurrentChanged, value);
    }

    /// <summary>Occurs when a property value of the <see cref="P:System.Windows.Forms.BindingSource.Current" /> property has changed.</summary>
    [Category("CatData")]
    [Description("BindingSourceCurrentItemChangedEventHandlerDescr")]
    public event EventHandler CurrentItemChanged
    {
        add => Events.AddHandler(EventCurrentItemChanged, value);
        remove => Events.RemoveHandler(EventCurrentItemChanged, value);
    }

    /// <summary>Occurs when a currency-related exception is silently handled by the <see cref="T:System.Windows.Forms.BindingSource" />.</summary>
    [Category("CatData")]
    [Description("BindingSourceDataErrorEventHandlerDescr")]
    public event BindingManagerDataErrorEventHandler DataError
    {
        add => Events.AddHandler(EventDataError, value);
        remove => Events.RemoveHandler(EventDataError, value);
    }

    /// <summary>Occurs when the <see cref="P:System.Windows.Forms.BindingSource.DataMember" /> property value has changed.</summary>
    [Category("CatData")]
    [Description("BindingSourceDataMemberChangedEventHandlerDescr")]
    public event EventHandler DataMemberChanged
    {
        add => Events.AddHandler(EventDataMemberChanged, value);
        remove => Events.RemoveHandler(EventDataMemberChanged, value);
    }

    /// <summary>Occurs when the <see cref="P:System.Windows.Forms.BindingSource.DataSource" /> property value has changed.</summary>
    [Category("CatData")]
    [Description("BindingSourceDataSourceChangedEventHandlerDescr")]
    public event EventHandler DataSourceChanged
    {
        add => Events.AddHandler(EventDataSourceChanged, value);
        remove => Events.RemoveHandler(EventDataSourceChanged, value);
    }

    /// <summary>Occurs when the underlying list changes or an item in the list changes.</summary>
    [Category("CatData")]
    [Description("BindingSourceListChangedEventHandlerDescr")]
    public event ListChangedEventHandler ListChanged
    {
        add => Events.AddHandler(EventListChanged, value);
        remove => Events.RemoveHandler(EventListChanged, value);
    }

    /// <summary>Occurs after the value of the <see cref="P:System.Windows.Forms.BindingSource.Position" /> property has changed.</summary>
    [Category("CatData")]
    [Description("BindingSourcePositionChangedEventHandlerDescr")]
    public event EventHandler PositionChanged
    {
        add => Events.AddHandler(EventPositionChanged, value);
        remove => Events.RemoveHandler(EventPositionChanged, value);
    }

    /// <summary>Occurs when the <see cref="T:System.Windows.Forms.BindingSource" /> is initialized.</summary>
    event EventHandler ISupportInitializeNotification.Initialized
    {
        add => Events.AddHandler(EventInitialized, value);
        remove => Events.RemoveHandler(EventInitialized, value);
    }
}