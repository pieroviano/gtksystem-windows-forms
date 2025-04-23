using System.ComponentModel;
using Gtk;

namespace System.Windows.Forms;

public partial class Control
{
    private IEventInvoker? _eventInvoker;
    protected bool _onLoadFired;

    static Control()
    {
        CreateEventInvoker = () => new EventInvoker();
    }

    public static Func<IEventInvoker> CreateEventInvoker
    {
        get;
        set;
    }

    internal IEventInvoker EventInvoker
    {
        get
        {
            if (_eventInvoker == null)
            {
                _eventInvoker = CreateEventInvoker();
            }
            return _eventInvoker;
        }
    }

    public bool UseAsyncLoad
    {
        get => EventInvoker.UseAsyncLoad;
        set => EventInvoker.UseAsyncLoad = value;
    }

    public void EventInvoke(Action eventToInvoke, bool useAsyncLoad = false)
    {
        EventInvoker.EventInvoke(eventToInvoke, useAsyncLoad);
    }

    protected virtual void OnSetUseAsyncLoad(UseAsyncLoadArgs e)
    {
        EventInvoke(() => SetUseAsyncLoad?.Invoke(this, e), UseAsyncLoad);
    }

    protected virtual void OnImeModeChanged(EventArgs e)
    {
        EventInvoke(() => ImeModeChanged?.Invoke(this, e));
    }

    protected virtual void OnBackColorChanged(EventArgs e)
    {
        EventInvoke(() => BackColorChanged?.Invoke(this, e));
    }

    protected virtual void OnBindingContextChanged(EventArgs e)
    {
        EventInvoke(() => BindingContextChanged?.Invoke(this, e));
    }

    protected internal virtual void OnLoad(EventArgs e)
    {
        if (_onLoadFired)
        {
            return;
        }

        _onLoadFired = true;
        EventInvoke(() =>
        {
            Load?.Invoke(this, e);
            OnLoadComplete(e);
        }, UseAsyncLoad);
    }

    protected internal virtual void OnLoadComplete(EventArgs e)
    {
        Invoke(() =>
        {
            LoadComplete?.Invoke(this, e);
        });
    }

    protected virtual void OnAnchorChanged(EventArgs e)
    {
        EventInvoke(() => AnchorChanged?.Invoke(this, e));
    }

    protected virtual void OnAutoSizeChanged(EventArgs e)
    {
        EventInvoke(() => AutoSizeChanged?.Invoke(this, e));
    }

    protected virtual void OnBackgroundImageChanged(EventArgs e)
    {
        EventInvoke(() => BackgroundImageChanged?.Invoke(this, e));
    }

    protected virtual void OnBackgroundImageLayoutChanged(EventArgs e)
    {
        EventInvoke(() => BackgroundImageLayoutChanged?.Invoke(this, e));
    }

    protected virtual void OnBeforeInit(EventArgs e)
    {
        EventInvoke(() => BeforeInit?.Invoke(this, e));
    }

    protected virtual void OnCausesValidationChanged(EventArgs e)
    {
        EventInvoke(() => CausesValidationChanged?.Invoke(this, e));
    }

    protected virtual void OnChangeUiCues(UiCuesEventArgs e)
    {
        EventInvoke(() => ChangeUICues?.Invoke(this, e));
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected internal virtual void OnClick(EventArgs e)
    {
        EventInvoke(() => Click?.Invoke(this, e));
    }

    protected virtual void OnClientSizeChanged(EventArgs e)
    {
        EventInvoke(() => ClientSizeChanged?.Invoke(this, e));
    }

    protected virtual void OnContextMenuStripChanged(EventArgs e)
    {
        EventInvoke(() => ContextMenuStripChanged?.Invoke(this, e));
    }

    protected virtual void OnControlAdded(ControlEventArgs e)
    {
        EventInvoke(() => ControlAdded?.Invoke(this, e));
    }

    protected virtual void OnControlRemoved(ControlEventArgs e)
    {
        EventInvoke(() => ControlRemoved?.Invoke(this, e));
    }

    protected virtual void OnCursorChanged(EventArgs e)
    {
        EventInvoke(() => CursorChanged?.Invoke(this, e));
    }

    protected virtual void OnDisposed(EventArgs e)
    {
        EventInvoke(() => Disposed?.Invoke(this, e));
    }

    protected virtual void OnDisposing(CancelEventArgs e)
    {
        EventInvoke(() => Disposing?.Invoke(this, e));
    }

    protected virtual void OnDockChanged(EventArgs e)
    {
        EventInvoke(() => DockChanged?.Invoke(this, e));
    }

    protected virtual void OnDoubleClick(EventArgs e)
    {
        EventInvoke(() => DoubleClick?.Invoke(this, e));
    }

    protected virtual void OnDpiChangedAfterParent(EventArgs e)
    {
        EventInvoke(() => DpiChangedAfterParent?.Invoke(this, e));
    }

    protected virtual void OnDpiChangedBeforeParent(EventArgs e)
    {
        EventInvoke(() => DpiChangedBeforeParent?.Invoke(this, e));
    }

    protected virtual void OnDragDrop(DragEventArgs e)
    {
        EventInvoke(() => DragDrop?.Invoke(this, e));
    }

    protected virtual void OnDragEnter(DragEventArgs e)
    {
        EventInvoke(() => DragEnter?.Invoke(this, e));
    }

    protected virtual void OnDragLeave(EventArgs e)
    {
        EventInvoke(() => DragLeave?.Invoke(this, e));
    }

    protected virtual void OnDragOver(DragEventArgs e)
    {
        EventInvoke(() => DragOver?.Invoke(this, e));
    }

    protected virtual void OnEnabledChanged(EventArgs e)
    {
        EventInvoke(() => EnabledChanged?.Invoke(this, e));
    }

    protected virtual void OnEnter(EnterNotifyEventArgs e)
    {
        EventInvoke(() => Enter?.Invoke(this, e));
    }

    protected virtual void OnFontChanged(EventArgs e)
    {
        EventInvoke(() => FontChanged?.Invoke(this, e));
    }

    protected virtual void OnForeColorChanged(EventArgs e)
    {
        EventInvoke(() => ForeColorChanged?.Invoke(this, e));
    }

    protected virtual void OnGiveFeedback(GiveFeedbackEventArgs e)
    {
        EventInvoke(() => GiveFeedback?.Invoke(this, e));
    }

    protected virtual void OnGotFocus(EventArgs e)
    {
        EventInvoke(() => GotFocus?.Invoke(this, e));
    }

    protected virtual void OnGotFocus(FocusInEventArgs e)
    {
        EventInvoke(() => GotFocus?.Invoke(this, e));
    }

    protected virtual void OnHandleCreated(EventArgs e)
    {
        if (!IsHandleCreated)
        {
            IsHandleCreated = true;
            EventInvoke(() => HandleCreated?.Invoke(this, e));
        }
    }

    protected virtual void OnHandleDestroyed(EventArgs e)
    {
        EventInvoke(() => HandleDestroyed?.Invoke(this, e));
    }

    protected virtual void OnHelpRequested(HelpEventArgs e)
    {
        EventInvoke(() => HelpRequested?.Invoke(this, e));
    }

    protected virtual void OnInvalidated(InvalidateEventArgs e)
    {
        EventInvoke(() => Invalidated?.Invoke(this, e));
    }

    protected virtual void OnKeyDown(KeyEventArgs e)
    {
        EventInvoke(() => KeyDown?.Invoke(this, e));
    }

    protected virtual void OnKeyPress(KeyPressEventArgs e)
    {
        EventInvoke(() => KeyPress?.Invoke(this, e));
    }

    protected virtual void OnKeyUp(KeyEventArgs e)
    {
        EventInvoke(() => KeyUp?.Invoke(this, e));
    }

    protected virtual void OnLayout(LayoutEventArgs e)
    {
        EventInvoke(() => Layout?.Invoke(this, e));
    }

    protected virtual void OnLeave(LeaveNotifyEventArgs e)
    {
        EventInvoke(() => Leave?.Invoke(this, e));
    }

    protected virtual void OnLocationChanged(EventArgs e)
    {
        EventInvoke(() => LocationChanged?.Invoke(this, e));
    }

    protected virtual void OnLostFocus(FocusOutEventArgs e)
    {
        EventInvoke(() => LostFocus?.Invoke(this, e));
    }

    protected virtual void OnMarginChanged(EventArgs e)
    {
        EventInvoke(() => MarginChanged?.Invoke(this, e));
    }

    protected virtual void OnMouseCaptureChanged(EventArgs e)
    {
        EventInvoke(() => MouseCaptureChanged?.Invoke(this, e));
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnMouseClick(MouseEventArgs e)
    {
        EventInvoke(() => MouseClick?.Invoke(this, e));
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnMouseDoubleClick(MouseEventArgs e)
    {
        EventInvoke(() => MouseDoubleClick?.Invoke(this, e));
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnMouseDown(MouseEventArgs e)
    {
        EventInvoke(() => MouseDown?.Invoke(this, e));
    }

    protected virtual void OnMouseEnter(EnterNotifyEventArgs e)
    {
        EventInvoke(() => MouseEnter?.Invoke(this, e));
    }

    protected virtual void OnMouseHover(EnterNotifyEventArgs e)
    {
        EventInvoke(() => MouseHover?.Invoke(this, e));
    }

    protected virtual void OnMouseLeave(LeaveNotifyEventArgs e)
    {
        EventInvoke(() => MouseLeave?.Invoke(this, e));
    }

    protected virtual void OnMouseMove(MouseEventArgs e)
    {
        EventInvoke(() => MouseMove?.Invoke(this, e));
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnMouseUp(MouseEventArgs e)
    {
        EventInvoke(() => MouseUp?.Invoke(this, e));
    }

    protected virtual void OnMouseWheel(MouseEventArgs e)
    {
        EventInvoke(() => MouseWheel?.Invoke(this, e));
    }

    protected virtual void OnMove(EventArgs e)
    {
        EventInvoke(() => Move?.Invoke(this, e));
    }

    protected virtual void OnMove(ConfigureEventArgs e)
    {
        EventInvoke(() => Move?.Invoke(this, e));
    }

    protected virtual void OnPaddingChanged(EventArgs e)
    {
        EventInvoke(() => PaddingChanged?.Invoke(this, e));
    }

    protected virtual void OnPaint(PaintEventArgs e)
    {
        Self.Override.OnPaint(e);
    }

    protected virtual void OnParentChanged(EventArgs e)
    {
        EventInvoke(() => ParentChanged?.Invoke(this, e));
    }

    protected virtual void OnPreLoad(EventArgs e)
    {
        EventInvoke(() => PreLoad?.Invoke(this, e));
    }

    protected virtual void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
    {
        EventInvoke(() => PreviewKeyDown?.Invoke(this, e));
    }

    protected virtual void OnPropertyChanged(EventArgs e)
    {
        EventInvoke(() => PropertyChanged?.Invoke(this, e));
    }

    protected virtual void OnQueryAccessibilityHelp(QueryAccessibilityHelpEventArgs e)
    {
        EventInvoke(() => QueryAccessibilityHelp?.Invoke(this, e));
    }

    protected virtual void OnQueryContinueDrag(QueryContinueDragEventArgs e)
    {
        EventInvoke(() => QueryContinueDrag?.Invoke(this, e));
    }

    protected virtual void OnRegionChanged(EventArgs e)
    {
        EventInvoke(() => RegionChanged?.Invoke(this, e));
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnResize(EventArgs e)
    {
        Widget?.QueueResize();
        EventInvoke(() => Resize?.Invoke(this, e));
    }

    protected virtual void OnRightToLeftChanged(EventArgs e)
    {
        EventInvoke(() => RightToLeftChanged?.Invoke(this, e));
    }

    protected virtual void OnSizeChanged(EventArgs e)
    {
        EventInvoke(() => SizeChanged?.Invoke(this, e));
    }

    protected virtual void OnStyleChanged(EventArgs e)
    {
        EventInvoke(() => StyleChanged?.Invoke(this, e));
    }

    protected virtual void OnSystemColorsChanged(EventArgs e)
    {
        EventInvoke(() => SystemColorsChanged?.Invoke(this, e));
    }

    protected virtual void OnTabIndexChanged(EventArgs e)
    {
        EventInvoke(() => TabIndexChanged?.Invoke(this, e));
    }

    protected virtual void OnTabStopChanged(EventArgs e)
    {
        EventInvoke(() => TabStopChanged?.Invoke(this, e));
    }

    protected virtual void OnTextChanged(EventArgs e)
    {
        EventInvoke(() => TextChanged?.Invoke(this, e));
    }

    protected virtual void OnValidated(CancelEventArgs e)
    {
        EventInvoke(() => Validated?.Invoke(this, e));
    }

    protected virtual void OnValidating(CancelEventArgs e)
    {
        EventInvoke(() => Validating?.Invoke(this, e));
    }

    protected virtual void OnVisibleChanged(EventArgs e)
    {
        EventInvoke(() => VisibleChanged?.Invoke(this, e));
    }

    private void Widget_FocusInEvent(object? o, FocusInEventArgs args)
    {
        OnGotFocus(args);
    }
}