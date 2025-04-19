using System.Collections;
using System.Drawing;
using Cairo;
using Gdk;
using GLib;
using Gtk;
using Pango;
using Context = Cairo.Context;
using Device = Gdk.Device;
using Layout = Pango.Layout;

namespace System.Windows.Forms;

public class ControlWidget : IWidget, IControlGtk
{
    private string? name;

    public ControlWidget()
    {
        Override = new GtkControlOverride(this);
    }

    public void Dispose()
    {
    }

    public event EventHandler? Destroyed;

    public string Name
    {
        get => name??string.Empty;
        set => name= value ?? string.Empty;
    }

    public Widget Parent { get; set; } = null!;
    public int WidthRequest { get; set; }
    public int HeightRequest { get; set; }
    public bool Visible { get; set; }
    public bool Sensitive { get; set; }
    public bool AppPaintable { get; set; }
    public bool CanFocus { get; set; }
    public bool HasFocus { get; set; }
    public bool IsFocus { get; set; }
    public bool FocusOnClick { get; set; }
    public bool CanDefault { get; set; }
    public bool HasDefault { get; set; }
    public bool ReceivesDefault { get; set; }
    public bool CompositeChild { get; set; }
    public EventMask Events { get; set; }
    public bool NoShowAll { get; set; }
    public bool HasTooltip { get; set; }
    public string TooltipText { get; set; } = string.Empty;
    public string TooltipMarkup { get; set; } = string.Empty;
    public Gdk.Window Window { get; set; } = null!;
    public bool DoubleBuffered { get; set; }
    public Align Halign { get; set; }
    public Align Valign { get; set; }
    public int MarginLeft { get; set; }
    public int MarginRight { get; set; }
    public int MarginStart { get; set; }
    public int MarginEnd { get; set; }
    public int MarginTop { get; set; }
    public int MarginBottom { get; set; }
    public int Margin { get; set; }
    public bool Hexpand { get; set; }
    public bool HexpandSet { get; set; }
    public bool Vexpand { get; set; }
    public bool VexpandSet { get; set; }
    public bool Expand { get; set; }
    public double Opacity { get; set; }
    public int ScaleFactor { get; set; }
    public bool InteriorFocus { get; set; }
    public int FocusLineWidth { get; set; }
    public string FocusLinePattern { get; set; } = string.Empty;
    public int FocusPadding { get; set; }
    public Gdk.Color? CursorColor { get; set; }
    public Gdk.Color? SecondaryCursorColor { get; set; }
    public float CursorAspectRatio { get; set; }
    public bool WindowDragging { get; set; }
    public Gdk.Color? LinkColor { get; set; }
    public Gdk.Color? VisitedLinkColor { get; set; }
    public bool WideSeparators { get; set; }
    public int SeparatorWidth { get; set; }
    public int SeparatorHeight { get; set; }
    public int ScrollArrowHlength { get; set; }
    public int ScrollArrowVlength { get; set; }
    public int TextHandleWidth { get; set; }
    public int TextHandleHeight { get; set; }
    public Atk.Object Accessible { get; set; } = null!;
    public int AllocatedBaseline { get; set; }
    public int AllocatedHeight { get; set; }
    public int AllocatedWidth { get; set; }
    public Gdk.Rectangle Allocation { get; set; }
    public Requisition ChildRequisition { get; set; }
    public bool ChildVisible { get; set; }
    public Gdk.Rectangle Clip { get; set; }
    public string CompositeName { get; set; } = string.Empty;
    public TextDirection Direction { get; set; }
    public Display Display { get; set; } = null!;
    public FontMap FontMap { get; set; } = null!;
    public FontOptions FontOptions { get; set; } = null!;
    public FrameClock FrameClock { get; set; } = null!;
    public bool HasWindow { get; set; }
    public bool IsMapped { get; set; }
    public RcStyle ModifierStyle { get; set; } = null!;
    public Pango.Context PangoContext { get; set; } = null!;
    public Gdk.Window ParentWindow { get; set; } = null!;
    public WidgetPath WidgetPath { get; set; } = null!;
    public bool IsRealized { get; set; }
    public SizeRequestMode RequestMode { get; set; }
    public Gdk.Window RootWindow { get; set; } = null!;
    public Screen Screen { get; set; } = null!;
    public StateFlags StateFlags { get; set; }
    public StyleContext StyleContext { get; set; } = new();
    public bool SupportMultidevice { get; set; }
    public Widget Toplevel { get; set; } = null!;
    public Align ValignWithBaseline { get; set; }
    public Visual Visual { get; set; } = null!;
    public bool HasGrab { get; set; }
    public bool HasRcStyle { get; set; }
    public bool HasScreen { get; set; }
    public bool HasVisibleFocus { get; set; }
    public bool IsComposited { get; set; }
    public bool IsDrawable { get; set; }
    public bool IsSensitive { get; set; }
    public bool IsToplevel { get; set; }
    public bool IsVisible { get; set; }
    public bool RedrawOnAllocate { get; set; }
    public Gdk.Window GdkWindow { get; set; } = null!;
    public IntPtr Handle { get; set; }
    public Hashtable Data { get; set; } = new();
    public GType NativeType { get; set; }
    public IntPtr OwnedHandle { get; set; }
    public bool IsFloating { get; set; }
    public event UnmapEventHandler? UnmapEvent;
    public event ConfigureEventHandler? ConfigureEvent;
    public event DragBeginHandler? DragBegin;
    public event DragDataGetHandler? DragDataGet;
    public event EventHandler? CompositedChanged;
    public event FocusOutEventHandler? FocusOutEvent;
    public event DragMotionHandler? DragMotion;
    public event DamageEventHandler? DamageEvent;
    public event DrawnHandler? Drawn;
    public event StyleSetHandler? StyleSet;
    public event ScreenChangedHandler? ScreenChanged;
    public event QueryTooltipHandler? QueryTooltip;
    public event EventHandler? Shown;
    public event SelectionNotifyEventHandler? SelectionNotifyEvent;
    public event StateFlagsChangedHandler? StateFlagsChanged;
    public event HelpShownHandler? HelpShown;
    public event SelectionClearEventHandler? SelectionClearEvent;
    public event EventHandler? FocusGrabbed;
    public event EventHandler? Realized;
    public event DragDataReceivedHandler? DragDataReceived;
    public event DragDropHandler? DragDrop;
    public event GrabNotifyHandler? GrabNotify;
    public event TouchEventHandler? TouchEvent;
    public event ChildNotifiedHandler? ChildNotified;
    public event DragEndHandler? DragEnd;
    public event PropertyNotifyEventHandler? PropertyNotifyEvent;
    public event EventHandler? Unrealized;
    public event FocusInEventHandler? FocusInEvent;
    public event ButtonPressEventHandler? ButtonPressEvent;
    public event DirectionChangedHandler? DirectionChanged;
    public event EventHandler? Unmapped;
    public event AccelCanActivateHandler? AccelCanActivate;
    public event GrabBrokenEventHandler? GrabBrokenEvent;
    public event WidgetEventAfterHandler? WidgetEventAfter;
    public event ProximityOutEventHandler? ProximityOutEvent;
    public event DragFailedHandler? DragFailed;
    public event MotionNotifyEventHandler? MotionNotifyEvent;
    public event EventHandler? Hidden;
    public event EventHandler? AccelClosuresChanged;
    public event EventHandler? StyleUpdated;
    public event ParentSetHandler? ParentSet;
    public event DragLeaveHandler? DragLeave;
    public event SizeAllocatedHandler? SizeAllocated;
    public event WidgetEventHandler? WidgetEvent;
    public event LeaveNotifyEventHandler? LeaveNotifyEvent;
    public event FocusedHandler? Focused;
    public event MoveFocusHandler? MoveFocus;
    public event SelectionRequestEventHandler? SelectionRequestEvent;
    public event DragDataDeleteHandler? DragDataDelete;
    public event EnterNotifyEventHandler? EnterNotifyEvent;
    public event MnemonicActivatedHandler? MnemonicActivated;
    public event SelectionReceivedHandler? SelectionReceived;
    public event KeyReleaseEventHandler? KeyReleaseEvent;
    public event VisibilityNotifyEventHandler? VisibilityNotifyEvent;
    public event DestroyEventHandler? DestroyEvent;
    public event PopupMenuHandler? PopupMenu;
    public event DeleteEventHandler? DeleteEvent;
    public event EventHandler? Mapped;
    public event WindowStateEventHandler? WindowStateEvent;
    public event MapEventHandler? MapEvent;
    public event HierarchyChangedHandler? HierarchyChanged;
    public event ButtonReleaseEventHandler? ButtonReleaseEvent;
    public event Gtk.ScrollEventHandler? ScrollEvent;
    public event ProximityInEventHandler? ProximityInEvent;
    public event Gtk.KeyPressEventHandler? KeyPressEvent;
    public event SelectionGetHandler? SelectionGet;
    public bool Activate()
    {
        return default!;
    }

    public void AddAccelerator(string accelSignal, AccelGroup accelGroup, uint accelKey, ModifierType accelMods,
        AccelFlags accelFlags)
    {
    }

    public void AddAccelerator(string accelSignal, AccelGroup accelGroup, AccelKey accelKey)
    {
    }

    public void AddDeviceEvents(Device device, EventMask events)
    {
    }

    public void AddEvents(int events)
    {
    }

    public uint AddTickCallback(TickCallback cb)
    {
        return 0;
    }

    public bool CanActivateAccel(uint signalId)
    {
        return default!;
    }

    public bool ChildFocus(DirectionType direction)
    {
        return default!;
    }

    public void ChildNotify(string childProperty)
    {
    }

    public void ClassPath(out uint pathLength, out string path, out string pathReversed)
    {
        pathLength = 0;
        path = string.Empty;
        pathReversed = string.Empty;
    }

    public bool ComputeExpand(Gtk.Orientation orientation)
    {
        return default!;
    }

    public Pango.Context CreatePangoContext()
    {
        return default!;
    }

    public Layout CreatePangoLayout(string text)
    {
        return default!;
    }

    public bool DeviceIsShadowed(Device device)
    {
        return default!;
    }

    public void Draw(Context cr)
    {
    }

    public void EnsureStyle()
    {
    }

    public void ErrorBell()
    {
    }

    public bool ProcessEvent(Event evnt)
    {
        return default!;
    }

    public void FreezeChildNotify()
    {
    }

    public IActionGroup GetActionGroup(string prefix)
    {
        return default!;
    }

    public void GetAllocatedSize(out Gdk.Rectangle allocation, out int baseline)
    {
        allocation = default;
        baseline = default;
    }

    public Gtk.Clipboard GetClipboard(Atom selection)
    {
        return default!;
    }

    public bool GetDeviceEnabled(Device device)
    {
        return default!;
    }

    public EventMask GetDeviceEvents(Device device)
    {
        return default!;
    }

    public ModifierType GetModifierMask(ModifierIntent intent)
    {
        return default!;
    }

    public void GetPointer(out int x, out int y)
    {
        x = 0;
        y = 0;
    }

    public void GetPreferredHeight(out int minimumHeight, out int naturalHeight)
    {
        minimumHeight = 0;
        naturalHeight = 0;
    }

    public void GetPreferredHeightAndBaselineForWidth(int width, out int minimumHeight, out int naturalHeight,
        out int minimumBaseline, out int naturalBaseline)
    {
        minimumHeight = 0;
        naturalHeight = 0;
        minimumBaseline = 0;
        naturalBaseline = 0;
    }

    public void GetPreferredHeightForWidth(int width, out int minimumHeight, out int naturalHeight)
    {
        minimumHeight = 0;
        naturalHeight = 0;
    }

    public void GetPreferredSize(out Requisition minimumSize, out Requisition naturalSize)
    {
        minimumSize = default;
        naturalSize = default;
    }

    public void GetPreferredWidth(out int minimumWidth, out int naturalWidth)
    {
        minimumWidth = 0;
        naturalWidth = 0;
    }

    public void GetPreferredWidthForHeight(int height, out int minimumWidth, out int naturalWidth)
    {
        minimumWidth = 0;
        naturalWidth = 0;
    }

    public void GetSizeRequest(out int width, out int height)
    {
        width = 0;
        height = 0;
    }

    public GLib.Object GetTemplateChild(GType widgetType, string name)
    {
        return default!;
    }

    public void GrabDefault()
    {

    }

    public void GrabFocus()
    {

    }

    public void Hide()
    {

    }

    public bool HideOnDelete()
    {
        return default;
    }

    public bool InDestruction()
    {
        return default;
    }

    public void InputShapeCombineRegion(Cairo.Region region)
    {

    }

    public void InsertActionGroup(string name, IActionGroup group)
    {

    }

    public bool Intersect(Gdk.Rectangle area, out Gdk.Rectangle intersection)
    {
        intersection = default;
        return default;
    }

    public bool KeynavFailed(DirectionType direction)
    {
        return default;
    }

    public string[] ListActionPrefixes()
    {
        return [];
    }

    public void Map()
    {

    }

    public bool MnemonicActivate(bool groupCycling)
    {
        return default;
    }

    public void ModifyFont(FontDescription fontDesc)
    {

    }

    public void ModifyStyle(RcStyle style)
    {

    }

    public void OverrideBackgroundColor(StateFlags state, RGBA color)
    {

    }

    public void OverrideColor(StateFlags state, RGBA color)
    {

    }

    public void OverrideCursor(RGBA cursor, RGBA secondaryCursor)
    {

    }

    public void OverrideFont(FontDescription fontDesc)
    {

    }

    public void OverrideSymbolicColor(string name, RGBA color)
    {

    }

    public void Path(out uint pathLength, out string path, out string pathReversed)
    {
        pathLength = 0;
        path = string.Empty;
        pathReversed = string.Empty;
    }

    public void Path(out string path, out string pathReversed)
    {
        path = string.Empty;
        pathReversed = string.Empty;
    }

    public void QueueAllocate()
    {

    }

    public void QueueComputeExpand()
    {

    }

    public void QueueDraw()
    {

    }

    public void QueueDrawArea(int x, int y, int width, int height)
    {

    }

    public void QueueDrawRegion(Cairo.Region region)
    {

    }

    public void QueueResize()
    {

    }

    public void QueueResizeNoRedraw()
    {

    }

    public void Realize()
    {

    }

    public Cairo.Region RegionIntersect(Cairo.Region region)
    {
        return default!;
    }

    public void RegisterWindow(Gdk.Window window)
    {

    }

    public bool RemoveAccelerator(AccelGroup accelGroup, uint accelKey, ModifierType accelMods)
    {
        return default;
    }

    public void RemoveMnemonicLabel(Widget label)
    {

    }

    public void RemoveTickCallback(uint id)
    {

    }

    public Pixbuf RenderIcon(string stockId, IconSize size, string detail)
    {
        return default!;
    }

    public Pixbuf RenderIconPixbuf(string stockId, IconSize size)
    {
        return default!;
    }

    public void Reparent(Widget newParent)
    {

    }

    public void ResetRcStyles()
    {

    }

    public void ResetStyle()
    {

    }

    public int SendExpose(Event evnt)
    {
        return default;
    }

    public bool SendFocusChange(Event evnt)
    {
        return default;
    }

    public void SetAccelPath(string accelPath, AccelGroup accelGroup)
    {

    }

    public void SetAllocation(Gdk.Rectangle allocation)
    {

    }

    public void SetClip(Gdk.Rectangle clip)
    {

    }

    public void SetDeviceEnabled(Device device, bool enabled)
    {

    }

    public void SetDeviceEvents(Device device, EventMask events)
    {

    }

    public void SetSizeRequest(int width, int height)
    {

    }

    public void SetStateFlags(StateFlags flags, bool clear)
    {

    }

    public void ShapeCombineRegion(Cairo.Region region)
    {

    }

    public void Show()
    {

    }

    public void ShowAll()
    {

    }

    public void ShowNow()
    {

    }

    public void SizeAllocate(Gdk.Rectangle allocation)
    {

    }

    public void SizeAllocateWithBaseline(Gdk.Rectangle allocation, int baseline)
    {

    }

    public Requisition SizeRequest()
    {
        return default;
    }

    public void StyleAttach()
    {

    }

    public void ThawChildNotify()
    {

    }

    public bool TranslateCoordinates(Widget destWidget, int srcX, int srcY, out int destX, out int destY)
    {
        destX = 0;
        destY = 0;
        return default;
    }

    public void TriggerTooltipQuery()
    {

    }

    public void Unmap()
    {

    }

    public void Unparent()
    {

    }

    public void Unrealize()
    {

    }

    public void UnregisterWindow(Gdk.Window window)
    {

    }

    public void UnsetStateFlags(StateFlags flags)
    {

    }

    public Atk.Object RefAccessible()
    {
        return default!;
    }

    public object StyleGetProperty(string propertyName)
    {
        return default!;
    }

    public void Destroy()
    {

    }

    public void AddNotification(string property, NotifyHandler handler)
    {

    }

    public void AddNotification(NotifyHandler handler)
    {

    }

    public void RemoveNotification(string property, NotifyHandler handler)
    {

    }

    public void RemoveNotification(NotifyHandler handler)
    {

    }

    public Value GetProperty(string name)
    {
        return default!;
    }

    public void SetProperty(string name, Value val)
    {

    }

    public void AddSignalHandler(string name, Delegate handler)
    {

    }

    public void AddSignalHandler(string name, Delegate handler, Delegate marshaler)
    {

    }

    public void AddSignalHandler(string name, Delegate handler, Type argsType)
    {

    }

    public void RemoveSignalHandler(string name, Delegate handler)
    {

    }

    public void QueueSignalFree()
    {

    }

    public IGtkControlOverride Override { get; set; }
}