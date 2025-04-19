using System.ComponentModel;
using Gtk;

namespace System.Windows.Forms;

public partial class Control
{
    private IEventInvoker? _eventInvoker;
    private bool _onLoadFired;

    internal IEventInvoker EventInvoker
    {
        get
        {
            if (_eventInvoker == null)
            {
                _eventInvoker = new EventInvoker();
            }
            return _eventInvoker;
        }
    }

    public bool UseAsyncInvoke
    {
        get => EventInvoker.UseAsyncInvoke;
        set
        {
            EventInvoker.UseAsyncInvoke = value;
        }
    }

    public void EventInvoke(Action eventToInvoke, bool useAsyncInvoke)
    {
        if (useAsyncInvoke)
        {
            Task.Run(eventToInvoke);
        }
        else
        {
            eventToInvoke();
        }
    }

    protected virtual void OnSetUseAsyncInvoke(UseAsyncInvokeArgs e)
    {
        EventInvoke(() => SetUseAsyncInvoke?.Invoke(this, e), UseAsyncInvoke);
    }

    protected virtual void OnImeModeChanged(EventArgs e)
    {
        EventInvoke(() => ImeModeChanged?.Invoke(this, e), false);
    }

    protected virtual void OnBackColorChanged(EventArgs e)
    {
        EventInvoke(() => BackColorChanged?.Invoke(this, e), false);
    }

    protected virtual void OnBindingContextChanged(EventArgs e)
    {
        EventInvoke(() => BindingContextChanged?.Invoke(this, e), false);
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
        }, UseAsyncInvoke);
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
        EventInvoke(() => AnchorChanged?.Invoke(this, e), false);
    }

    protected virtual void OnAutoSizeChanged(EventArgs e)
    {
        EventInvoke(() => AutoSizeChanged?.Invoke(this, e), false);
    }

    protected virtual void OnBackgroundImageChanged(EventArgs e)
    {
        EventInvoke(() => BackgroundImageChanged?.Invoke(this, e), false);
    }

    protected virtual void OnBackgroundImageLayoutChanged(EventArgs e)
    {
        EventInvoke(() => BackgroundImageLayoutChanged?.Invoke(this, e), false);
    }

    protected virtual void OnBeforeInit(EventArgs e)
    {
        EventInvoke(() => BeforeInit?.Invoke(this, e), false);
    }

    protected virtual void OnCausesValidationChanged(EventArgs e)
    {
        EventInvoke(() => CausesValidationChanged?.Invoke(this, e), false);
    }

    protected virtual void OnChangeUiCues(UiCuesEventArgs e)
    {
        EventInvoke(() => ChangeUICues?.Invoke(this, e), false);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected internal virtual void OnClick(EventArgs e)
    {
        EventInvoke(() => Click?.Invoke(this, e), false);
    }

    protected virtual void OnClientSizeChanged(EventArgs e)
    {
        EventInvoke(() => ClientSizeChanged?.Invoke(this, e), false);
    }

    protected virtual void OnContextMenuStripChanged(EventArgs e)
    {
        EventInvoke(() => ContextMenuStripChanged?.Invoke(this, e), false);
    }

    protected virtual void OnControlAdded(ControlEventArgs e)
    {
        EventInvoke(() => ControlAdded?.Invoke(this, e), false);
    }

    protected virtual void OnControlRemoved(ControlEventArgs e)
    {
        EventInvoke(() => ControlRemoved?.Invoke(this, e), false);
    }

    protected virtual void OnCursorChanged(EventArgs e)
    {
        EventInvoke(() => CursorChanged?.Invoke(this, e), false);
    }

    protected virtual void OnDisposed(EventArgs e)
    {
        EventInvoke(() => Disposed?.Invoke(this, e), false);
    }

    protected virtual void OnDisposing(CancelEventArgs e)
    {
        EventInvoke(() => Disposing?.Invoke(this, e), false);
    }

    protected virtual void OnDockChanged(EventArgs e)
    {
        EventInvoke(() => DockChanged?.Invoke(this, e), false);
    }

    protected virtual void OnDoubleClick(EventArgs e)
    {
        EventInvoke(() => DoubleClick?.Invoke(this, e), false);
    }

    protected virtual void OnDpiChangedAfterParent(EventArgs e)
    {
        EventInvoke(() => DpiChangedAfterParent?.Invoke(this, e), false);
    }

    protected virtual void OnDpiChangedBeforeParent(EventArgs e)
    {
        EventInvoke(() => DpiChangedBeforeParent?.Invoke(this, e), false);
    }

    protected virtual void OnDragDrop(DragEventArgs e)
    {
        EventInvoke(() => DragDrop?.Invoke(this, e), false);
    }

    protected virtual void OnDragEnter(DragEventArgs e)
    {
        EventInvoke(() => DragEnter?.Invoke(this, e), false);
    }

    protected virtual void OnDragLeave(EventArgs e)
    {
        EventInvoke(() => DragLeave?.Invoke(this, e), false);
    }

    protected virtual void OnDragOver(DragEventArgs e)
    {
        EventInvoke(() => DragOver?.Invoke(this, e), false);
    }

    protected virtual void OnEnabledChanged(EventArgs e)
    {
        EventInvoke(() => EnabledChanged?.Invoke(this, e), false);
    }

    protected virtual void OnEnter(EnterNotifyEventArgs e)
    {
        EventInvoke(() => Enter?.Invoke(this, e), false);
    }

    protected virtual void OnFontChanged(EventArgs e)
    {
        EventInvoke(() => FontChanged?.Invoke(this, e), false);
    }

    protected virtual void OnForeColorChanged(EventArgs e)
    {
        EventInvoke(() => ForeColorChanged?.Invoke(this, e), false);
    }

    protected virtual void OnGiveFeedback(GiveFeedbackEventArgs e)
    {
        EventInvoke(() => GiveFeedback?.Invoke(this, e), false);
    }

    protected virtual void OnGotFocus(EventArgs e)
    {
        EventInvoke(() => GotFocus?.Invoke(this, e), false);
    }

    protected virtual void OnGotFocus(FocusInEventArgs e)
    {
        EventInvoke(() => GotFocus?.Invoke(this, e), false);
    }

    protected virtual void OnHandleCreated(EventArgs e)
    {
        if (!IsHandleCreated)
        {
            IsHandleCreated = true;
            EventInvoke(() => HandleCreated?.Invoke(this, e), false);
        }
    }

    protected virtual void OnHandleDestroyed(EventArgs e)
    {
        EventInvoke(() => HandleDestroyed?.Invoke(this, e), false);
    }

    protected virtual void OnHelpRequested(HelpEventArgs e)
    {
        EventInvoke(() => HelpRequested?.Invoke(this, e), false);
    }

    protected virtual void OnInvalidated(InvalidateEventArgs e)
    {
        EventInvoke(() => Invalidated?.Invoke(this, e), false);
    }

    protected virtual void OnKeyDown(KeyEventArgs e)
    {
        EventInvoke(() => KeyDown?.Invoke(this, e), false);
    }

    protected virtual void OnKeyPress(KeyPressEventArgs e)
    {
        EventInvoke(() => KeyPress?.Invoke(this, e), false);
    }

    protected virtual void OnKeyUp(KeyEventArgs e)
    {
        EventInvoke(() => KeyUp?.Invoke(this, e), false);
    }

    protected virtual void OnLayout(LayoutEventArgs e)
    {
        EventInvoke(() => Layout?.Invoke(this, e), false);
    }

    protected virtual void OnLeave(LeaveNotifyEventArgs e)
    {
        EventInvoke(() => Leave?.Invoke(this, e), false);
    }

    protected virtual void OnLocationChanged(EventArgs e)
    {
        EventInvoke(() => LocationChanged?.Invoke(this, e), false);
    }

    protected virtual void OnLostFocus(FocusOutEventArgs e)
    {
        EventInvoke(() => LostFocus?.Invoke(this, e), false);
    }

    protected virtual void OnMarginChanged(EventArgs e)
    {
        EventInvoke(() => MarginChanged?.Invoke(this, e), false);
    }

    protected virtual void OnMouseCaptureChanged(EventArgs e)
    {
        EventInvoke(() => MouseCaptureChanged?.Invoke(this, e), false);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnMouseClick(MouseEventArgs e)
    {
        EventInvoke(() => MouseClick?.Invoke(this, e), false);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnMouseDoubleClick(MouseEventArgs e)
    {
        EventInvoke(() => MouseDoubleClick?.Invoke(this, e), false);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnMouseDown(MouseEventArgs e)
    {
        EventInvoke(() => MouseDown?.Invoke(this, e), false);
    }

    protected virtual void OnMouseEnter(EnterNotifyEventArgs e)
    {
        EventInvoke(() => MouseEnter?.Invoke(this, e), false);
    }

    protected virtual void OnMouseHover(EnterNotifyEventArgs e)
    {
        EventInvoke(() => MouseHover?.Invoke(this, e), false);
    }

    protected virtual void OnMouseLeave(LeaveNotifyEventArgs e)
    {
        EventInvoke(() => MouseLeave?.Invoke(this, e), false);
    }

    protected virtual void OnMouseMove(MouseEventArgs e)
    {
        EventInvoke(() => MouseMove?.Invoke(this, e), false);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnMouseUp(MouseEventArgs e)
    {
        EventInvoke(() => MouseUp?.Invoke(this, e), false);
    }

    protected virtual void OnMouseWheel(MouseEventArgs e)
    {
        EventInvoke(() => MouseWheel?.Invoke(this, e), false);
    }

    protected virtual void OnMove(EventArgs e)
    {
        EventInvoke(() => Move?.Invoke(this, e), false);
    }

    protected virtual void OnMove(ConfigureEventArgs e)
    {
        EventInvoke(() => Move?.Invoke(this, e), false);
    }

    protected virtual void OnPaddingChanged(EventArgs e)
    {
        EventInvoke(() => PaddingChanged?.Invoke(this, e), false);
    }

    protected virtual void OnPaint(PaintEventArgs e)
    {
        Self.Override.OnPaint(e);
    }

    protected virtual void OnParentChanged(EventArgs e)
    {
        EventInvoke(() => ParentChanged?.Invoke(this, e), false);
    }

    protected virtual void OnPreLoad(EventArgs e)
    {
        EventInvoke(() => PreLoad?.Invoke(this, e), false);
    }

    protected virtual void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
    {
        EventInvoke(() => PreviewKeyDown?.Invoke(this, e), false);
    }

    protected virtual void OnPropertyChanged(EventArgs e)
    {
        EventInvoke(() => PropertyChanged?.Invoke(this, e), false);
    }

    protected virtual void OnQueryAccessibilityHelp(QueryAccessibilityHelpEventArgs e)
    {
        EventInvoke(() => QueryAccessibilityHelp?.Invoke(this, e), false);
    }

    protected virtual void OnQueryContinueDrag(QueryContinueDragEventArgs e)
    {
        EventInvoke(() => QueryContinueDrag?.Invoke(this, e), false);
    }

    protected virtual void OnRegionChanged(EventArgs e)
    {
        EventInvoke(() => RegionChanged?.Invoke(this, e), false);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected virtual void OnResize(EventArgs e)
    {
        Widget?.QueueResize();
        EventInvoke(() => Resize?.Invoke(this, e), false);
    }

    protected virtual void OnRightToLeftChanged(EventArgs e)
    {
        EventInvoke(() => RightToLeftChanged?.Invoke(this, e), false);
    }

    protected virtual void OnSizeChanged(EventArgs e)
    {
        EventInvoke(() => SizeChanged?.Invoke(this, e), false);
    }

    protected virtual void OnStyleChanged(EventArgs e)
    {
        EventInvoke(() => StyleChanged?.Invoke(this, e), false);
    }

    protected virtual void OnSystemColorsChanged(EventArgs e)
    {
        EventInvoke(() => SystemColorsChanged?.Invoke(this, e), false);
    }

    protected virtual void OnTabIndexChanged(EventArgs e)
    {
        EventInvoke(() => TabIndexChanged?.Invoke(this, e), false);
    }

    protected virtual void OnTabStopChanged(EventArgs e)
    {
        EventInvoke(() => TabStopChanged?.Invoke(this, e), false);
    }

    protected virtual void OnTextChanged(EventArgs e)
    {
        EventInvoke(() => TextChanged?.Invoke(this, e), false);
    }

    protected virtual void OnValidated(CancelEventArgs e)
    {
        EventInvoke(() => Validated?.Invoke(this, e), false);
    }

    protected virtual void OnValidating(CancelEventArgs e)
    {
        EventInvoke(() => Validating?.Invoke(this, e), false);
    }

    protected virtual void OnVisibleChanged(EventArgs e)
    {
        EventInvoke(() => VisibleChanged?.Invoke(this, e), false);
    }

    private void Widget_FocusInEvent(object? o, FocusInEventArgs args)
    {
        OnGotFocus(args);
    }
}