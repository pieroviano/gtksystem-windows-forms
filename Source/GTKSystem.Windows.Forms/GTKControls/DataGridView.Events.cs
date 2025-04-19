namespace System.Windows.Forms;

public partial class DataGridView
{
    public event EventHandler? SelectionChanged;
    
    public event DataGridViewCellEventHandler? CellClick;

    public event DataGridViewCellEventHandler? CellValueChanged;

#pragma warning disable CS0067 // Event is never used
    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellValueEventHandler? CellValuePushed;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewColumnEventHandler? ColumnHeaderCellChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellValueEventHandler? CellValueNeeded;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewEditingControlShowingEventHandler? EditingControlShowing;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewRowEventHandler? RowContextMenuStripChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewRowEventHandler? RowUnshared;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewRowStateChangedEventHandler? RowStateChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewRowsRemovedEventHandler? RowsRemoved;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewRowsAddedEventHandler? RowsAdded;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewRowPrePaintEventHandler? RowPrePaint;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewRowPostPaintEventHandler? RowPostPaint;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewRowEventHandler? RowMinimumHeightChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellEventHandler? RowLeave;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewRowHeightInfoPushedEventHandler? RowHeightInfoPushed;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewRowHeightInfoNeededEventHandler? RowHeightInfoNeeded;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewRowEventHandler? NewRowNeeded;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewRowEventHandler? RowHeightChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellMouseEventHandler? RowHeaderMouseDoubleClick;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellMouseEventHandler? RowHeaderMouseClick;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewRowErrorTextNeededEventHandler? RowErrorTextNeeded;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewRowEventHandler? RowErrorTextChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellEventHandler? RowEnter;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewRowEventHandler? RowDividerHeightChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewRowDividerDoubleClickEventHandler? RowDividerDoubleClick;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event QuestionEventHandler? RowDirtyStateNeeded;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewRowEventHandler? RowDefaultCellStyleChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewRowContextMenuStripNeededEventHandler? RowContextMenuStripNeeded;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewRowEventHandler? RowHeaderCellChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellValidatingEventHandler? CellValidating;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellEventHandler? CellValidated;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event QuestionEventHandler? CancelRowEdit;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewAutoSizeColumnModeEventHandler? AutoSizeColumnModeChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event EventHandler? RowsDefaultCellStyleChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewAutoSizeModeEventHandler? RowHeadersWidthSizeModeChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event EventHandler? RowHeadersWidthChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event EventHandler? RowHeadersDefaultCellStyleChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event EventHandler? RowHeadersBorderStyleChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event EventHandler? ReadOnlyChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellCancelEventHandler? CellBeginEdit;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event EventHandler? MultiSelectChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event EventHandler? EditModeChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event EventHandler? DefaultCellStyleChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event EventHandler? DataSourceChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event EventHandler? DataMemberChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewAutoSizeModeEventHandler? ColumnHeadersHeightSizeModeChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event EventHandler? ColumnHeadersHeightChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event EventHandler? ColumnHeadersDefaultCellStyleChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event EventHandler? ColumnHeadersBorderStyleChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event EventHandler? GridColorChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellEventHandler? CellContentClick;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellEventHandler? CellContentDoubleClick;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellToolTipTextNeededEventHandler? CellToolTipTextNeeded;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellEventHandler? CellToolTipTextChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellStyleContentChangedEventHandler? CellStyleContentChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellEventHandler? CellStyleChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellStateChangedEventHandler? CellStateChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellParsingEventHandler? CellParsing;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellPaintingEventHandler? CellPainting;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellMouseEventHandler? CellMouseUp;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellMouseEventHandler? CellMouseMove;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellEventHandler? CellMouseLeave;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellEventHandler? CellMouseEnter;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellMouseEventHandler? CellMouseDown;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellMouseEventHandler? CellMouseDoubleClick;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellMouseEventHandler? CellMouseClick;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellEventHandler? CellLeave;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellFormattingEventHandler? CellFormatting;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellErrorTextNeededEventHandler? CellErrorTextNeeded;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellEventHandler? CellErrorTextChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellEventHandler? CellEnter;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellEventHandler? CellEndEdit;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellEventHandler? CellDoubleClick;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellContextMenuStripNeededEventHandler? CellContextMenuStripNeeded;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellEventHandler? CellContextMenuStripChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event EventHandler? BorderStyleChanged;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellEventHandler? RowValidated;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewCellCancelEventHandler? RowValidating;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewRowCancelEventHandler? UserDeletingRow;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewRowEventHandler? UserDeletedRow;

    [Obsolete("This event is not implemented and is developed by ourselves.")]
    public event DataGridViewRowEventHandler? UserAddedRow;

    public event EventHandler<DataGridViewColumnEventArgs>? ColumnNameChanged;
}