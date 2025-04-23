using System.ComponentModel;
using Gtk;

namespace System.Windows.Forms;

public partial class Control
{
    public event PaintEventHandler? Paint
    {
        add => Self.Override.Paint += value;
        remove => Self.Override.Paint -= value;
    }

    public event EventHandler<UseAsyncInvokeArgs>? SetUseAsyncInvoke;

    public event EventHandler? AutoSizeChanged;

    public event EventHandler? BackColorChanged;

    public event EventHandler? BackgroundImageChanged;

    public event EventHandler? BackgroundImageLayoutChanged;

    public event EventHandler? BindingContextChanged;

    public event EventHandler? CausesValidationChanged;

    public event UiCuesEventHandler? ChangeUICues;

    public event EventHandler? Click;

    public event EventHandler? ClientSizeChanged;

    public event EventHandler? ContextMenuStripChanged;

    public event ControlEventHandler? ControlAdded;

    public event ControlEventHandler? ControlRemoved;

    public event EventHandler? CursorChanged;

    public event EventHandler? DockChanged;

    public event EventHandler? AnchorChanged;

    public event EventHandler? DoubleClick;

    public event EventHandler? DpiChangedAfterParent;

    public event EventHandler? DpiChangedBeforeParent;

    public event DragEventHandler? DragDrop;

    public event DragEventHandler? DragEnter;

    public event EventHandler? DragLeave;

    public event DragEventHandler? DragOver;

    public event EventHandler? EnabledChanged;

    public event EventHandler? Enter;

    public event EventHandler? FontChanged;

    public event EventHandler? ForeColorChanged;

    public event GiveFeedbackEventHandler? GiveFeedback;

    public event EventHandler? GotFocus;

    public event EventHandler? HandleCreated;

    public event EventHandler? HandleDestroyed;

    public event HelpEventHandler? HelpRequested;

    public event EventHandler? ImeModeChanged;

    public event InvalidateEventHandler? Invalidated;

    public event KeyEventHandler? KeyDown;

    public event KeyPressEventHandler? KeyPress;

    public event KeyEventHandler? KeyUp;

    public event LayoutEventHandler? Layout;

    public event EventHandler? Leave;

    public event EventHandler? LocationChanged;

    public event EventHandler? LostFocus;

    public event EventHandler? MarginChanged;

    public event EventHandler? MouseCaptureChanged;

    public event MouseEventHandler? MouseClick;

    public event MouseEventHandler? MouseDoubleClick;

    public event MouseEventHandler? MouseDown;

    public event EventHandler? MouseEnter;

    public event EventHandler? MouseHover;

    public event EventHandler? MouseLeave;

    public event MouseEventHandler? MouseMove;

    public event MouseEventHandler? MouseUp;

    public event MouseEventHandler? MouseWheel;

    public event EventHandler? Move;

    public event EventHandler? PaddingChanged;

    public event EventHandler? ParentChanged;

    public event PreviewKeyDownEventHandler? PreviewKeyDown;

    public event QueryAccessibilityHelpEventHandler? QueryAccessibilityHelp;

    public event QueryContinueDragEventHandler? QueryContinueDrag;

    public event EventHandler? RegionChanged;

    public event EventHandler? Resize;

    public event EventHandler? RightToLeftChanged;

    public event EventHandler? SizeChanged;

    public event EventHandler? StyleChanged;

    public event EventHandler? SystemColorsChanged;

    public event EventHandler? TabIndexChanged;

    public event EventHandler? TabStopChanged;

    public event EventHandler? TextChanged;

    public event EventHandler? PropertyChanged;

    public event EventHandler? Validated;

    public event CancelEventHandler? Validating;

    public event EventHandler? VisibleChanged;

    public event EventHandler? PreLoad;

    public event EventHandler? Load;

    public event EventHandler? LoadComplete;

    public new virtual event EventHandler? Disposed;

    public event CancelEventHandler? Disposing;
}