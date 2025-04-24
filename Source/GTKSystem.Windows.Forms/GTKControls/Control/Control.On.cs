using System.ComponentModel;
using Gtk;

namespace System.Windows.Forms;

public partial class Control
{
    protected bool _onLoadFired;

    protected virtual void OnImeModeChanged(EventArgs e)
    {
        ImeModeChanged?.Invoke(this, e);
    }

    protected virtual void OnBackColorChanged(EventArgs e)
    {
        BackColorChanged?.Invoke(this, e);
    }

    protected virtual void OnBindingContextChanged(EventArgs e)
    {
        BindingContextChanged?.Invoke(this, e);
    }

    protected internal virtual void OnLoad(EventArgs e)
    {
        if (_onLoadFired)
        {
            return;
        }

        _onLoadFired = true;
        Load?.Invoke(this, e);
    }

    protected virtual void OnAnchorChanged(EventArgs e)
    {
        AnchorChanged?.Invoke(this, e);
    }

    protected virtual void OnAutoSizeChanged(EventArgs e)
    {
        AutoSizeChanged?.Invoke(this, e);
    }

    protected virtual void OnBackgroundImageChanged(EventArgs e)
    {
        BackgroundImageChanged?.Invoke(this, e);
    }

    protected virtual void OnBackgroundImageLayoutChanged(EventArgs e)
    {
        BackgroundImageLayoutChanged?.Invoke(this, e);
    }

    protected virtual void OnBeforeInit(EventArgs e)
    {
        BeforeInit?.Invoke(this, e);
    }

    protected virtual void OnCausesValidationChanged(EventArgs e)
    {
        CausesValidationChanged?.Invoke(this, e);
    }

    protected virtual void OnChangeUiCues(UiCuesEventArgs e)
    {
        ChangeUICues?.Invoke(this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected internal virtual void OnClick(EventArgs e)
    {
        Click?.Invoke(this, e);
    }

    protected virtual void OnClientSizeChanged(EventArgs e)
    {
        ClientSizeChanged?.Invoke(this, e);
    }

    protected virtual void OnContextMenuStripChanged(EventArgs e)
    {
        ContextMenuStripChanged?.Invoke(this, e);
    }

    protected virtual void OnControlAdded(ControlEventArgs e)
    {
        ControlAdded?.Invoke(this, e);
    }

    protected virtual void OnControlRemoved(ControlEventArgs e)
    {
        ControlRemoved?.Invoke(this, e);
    }

    protected virtual void OnCursorChanged(EventArgs e)
    {
        CursorChanged?.Invoke(this, e);
    }

    protected virtual void OnDisposed(EventArgs e)
    {
        Disposed?.Invoke(this, e);
    }

    protected virtual void OnDisposing(CancelEventArgs e)
    {
        Disposing?.Invoke(this, e);
    }

    protected virtual void OnDockChanged(EventArgs e)
    {
        DockChanged?.Invoke(this, e);
    }

    protected virtual void OnDoubleClick(EventArgs e)
    {
        DoubleClick?.Invoke(this, e);
    }

    protected virtual void OnDpiChangedAfterParent(EventArgs e)
    {
        DpiChangedAfterParent?.Invoke(this, e);
    }

    protected virtual void OnDpiChangedBeforeParent(EventArgs e)
    {
        DpiChangedBeforeParent?.Invoke(this, e);
    }

    protected virtual void OnDragDrop(DragEventArgs e)
    {
        DragDrop?.Invoke(this, e);
    }

    protected virtual void OnDragEnter(DragEventArgs e)
    {
        DragEnter?.Invoke(this, e);
    }

    protected virtual void OnDragLeave(EventArgs e)
    {
        DragLeave?.Invoke(this, e);
    }

    protected virtual void OnDragOver(DragEventArgs e)
    {
        DragOver?.Invoke(this, e);
    }

    protected virtual void OnEnabledChanged(EventArgs e)
    {
        EnabledChanged?.Invoke(this, e);
    }

    protected virtual void OnEnter(EnterNotifyEventArgs e)
    {
        Enter?.Invoke(this, e);
    }

    protected virtual void OnFontChanged(EventArgs e)
    {
        FontChanged?.Invoke(this, e);
    }

    protected virtual void OnForeColorChanged(EventArgs e)
    {
        ForeColorChanged?.Invoke(this, e);
    }

    protected virtual void OnGiveFeedback(GiveFeedbackEventArgs e)
    {
        GiveFeedback?.Invoke(this, e);
    }

    protected virtual void OnGotFocus(EventArgs e)
    {
        GotFocus?.Invoke(this, e);
    }

    protected virtual void OnGotFocus(FocusInEventArgs e)
    {
        GotFocus?.Invoke(this, e);
    }

    protected virtual void OnHandleCreated(EventArgs e)
    {
        if (!IsHandleCreated)
        {
            IsHandleCreated = true;
            HandleCreated?.Invoke(this, e);
        }
    }

    protected virtual void OnHandleDestroyed(EventArgs e)
    {
        HandleDestroyed?.Invoke(this, e);
    }

    protected virtual void OnHelpRequested(HelpEventArgs e)
    {
        HelpRequested?.Invoke(this, e);
    }

    protected virtual void OnInvalidated(InvalidateEventArgs e)
    {
        Invalidated?.Invoke(this, e);
    }

    protected virtual void OnKeyDown(KeyEventArgs e)
    {
        KeyDown?.Invoke(this, e);
    }

    protected virtual void OnKeyPress(KeyPressEventArgs e)
    {
        KeyPress?.Invoke(this, e);
    }

    protected virtual void OnKeyUp(KeyEventArgs e)
    {
        KeyUp?.Invoke(this, e);
    }

    protected virtual void OnLayout(LayoutEventArgs e)
    {
        Layout?.Invoke(this, e);
    }

    protected virtual void OnLeave(LeaveNotifyEventArgs e)
    {
        Leave?.Invoke(this, e);
    }

    protected virtual void OnLocationChanged(EventArgs e)
    {
        LocationChanged?.Invoke(this, e);
    }

    protected virtual void OnLostFocus(FocusOutEventArgs e)
    {
        LostFocus?.Invoke(this, e);
    }

    protected virtual void OnMarginChanged(EventArgs e)
    {
        MarginChanged?.Invoke(this, e);
    }

    protected virtual void OnMouseCaptureChanged(EventArgs e)
    {
        MouseCaptureChanged?.Invoke(this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnMouseClick(MouseEventArgs e)
    {
        MouseClick?.Invoke(this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnMouseDoubleClick(MouseEventArgs e)
    {
        MouseDoubleClick?.Invoke(this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnMouseDown(MouseEventArgs e)
    {
        MouseDown?.Invoke(this, e);
    }

    protected virtual void OnMouseEnter(EnterNotifyEventArgs e)
    {
        MouseEnter?.Invoke(this, e);
    }

    protected virtual void OnMouseHover(EnterNotifyEventArgs e)
    {
        MouseHover?.Invoke(this, e);
    }

    protected virtual void OnMouseLeave(LeaveNotifyEventArgs e)
    {
        MouseLeave?.Invoke(this, e);
    }

    protected virtual void OnMouseMove(MouseEventArgs e)
    {
        MouseMove?.Invoke(this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnMouseUp(MouseEventArgs e)
    {
        MouseUp?.Invoke(this, e);
    }

    protected virtual void OnMouseWheel(MouseEventArgs e)
    {
        MouseWheel?.Invoke(this, e);
    }

    protected virtual void OnMove(EventArgs e)
    {
        Move?.Invoke(this, e);
    }

    protected virtual void OnMove(ConfigureEventArgs e)
    {
        Move?.Invoke(this, e);
    }

    protected virtual void OnPaddingChanged(EventArgs e)
    {
        PaddingChanged?.Invoke(this, e);
    }

    protected virtual void OnPaint(PaintEventArgs e)
    {
        Self.Override.OnPaint(e);
    }

    protected virtual void OnParentChanged(EventArgs e)
    {
        ParentChanged?.Invoke(this, e);
    }

    protected virtual void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
    {
        PreviewKeyDown?.Invoke(this, e);
    }

    protected virtual void OnPropertyChanged(EventArgs e)
    {
        PropertyChanged?.Invoke(this, e);
    }

    protected virtual void OnQueryAccessibilityHelp(QueryAccessibilityHelpEventArgs e)
    {
        QueryAccessibilityHelp?.Invoke(this, e);
    }

    protected virtual void OnQueryContinueDrag(QueryContinueDragEventArgs e)
    {
        QueryContinueDrag?.Invoke(this, e);
    }

    protected virtual void OnRegionChanged(EventArgs e)
    {
        RegionChanged?.Invoke(this, e);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnResize(EventArgs e)
    {
        Widget?.QueueResize();
        Resize?.Invoke(this, e);
    }

    protected virtual void OnRightToLeftChanged(EventArgs e)
    {
        RightToLeftChanged?.Invoke(this, e);
    }

    protected virtual void OnSizeChanged(EventArgs e)
    {
        SizeChanged?.Invoke(this, e);
    }

    protected virtual void OnStyleChanged(EventArgs e)
    {
        StyleChanged?.Invoke(this, e);
    }

    protected virtual void OnSystemColorsChanged(EventArgs e)
    {
        SystemColorsChanged?.Invoke(this, e);
    }

    protected virtual void OnTabIndexChanged(EventArgs e)
    {
        TabIndexChanged?.Invoke(this, e);
    }

    protected virtual void OnTabStopChanged(EventArgs e)
    {
        TabStopChanged?.Invoke(this, e);
    }

    protected virtual void OnTextChanged(EventArgs e)
    {
        TextChanged?.Invoke(this, e);
    }

    protected virtual void OnValidated(CancelEventArgs e)
    {
        Validated?.Invoke(this, e);
    }

    protected virtual void OnValidating(CancelEventArgs e)
    {
        Validating?.Invoke(this, e);
    }

    protected virtual void OnVisibleChanged(EventArgs e)
    {
        VisibleChanged?.Invoke(this, e);
    }

    private void Widget_FocusInEvent(object? o, FocusInEventArgs args)
    {
        OnGotFocus(args);
    }
}