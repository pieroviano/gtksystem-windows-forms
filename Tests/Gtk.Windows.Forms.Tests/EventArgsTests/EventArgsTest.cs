using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using GtkTests.Helpers;

namespace GtkTests.EventArgsTests;

[TestFixture]
public class EventArgsTest : TestHelper
{
    [Test]
    public void TestBindingCompleteEventArgs()
    {
        var b = new Binding("TestBind", null, "TestMember");
        var c = new BindingCompleteContext();
        var errorText = "This is an error!";
        Exception ex = new ArgumentNullException();

        var e = new BindingCompleteEventArgs(b, BindingCompleteState.Success, c);

        Assert.That((object?)e.Binding, Is.EqualTo(b));
        Assert.That((object?)e.BindingCompleteState, Is.EqualTo(BindingCompleteState.Success));
        Assert.That((object?)e.BindingCompleteContext, Is.EqualTo(c));
        Assert.That((object?)e.Cancel, Is.EqualTo(false));
        object expected = string.Empty;
        Assert.That((object?)e.ErrorText, Is.EqualTo(expected));
        Assert.That((object?)e.Exception, Is.EqualTo(null));

        var e2 = new BindingCompleteEventArgs(b, BindingCompleteState.Success, c, errorText);

        Assert.That((object?)e2.Binding, Is.EqualTo(b));
        Assert.That((object?)e2.BindingCompleteState, Is.EqualTo(BindingCompleteState.Success));
        Assert.That((object?)e2.BindingCompleteContext, Is.EqualTo(c));
        Assert.That((object?)e2.Cancel, Is.EqualTo(true));
        Assert.That((object?)e2.ErrorText, Is.EqualTo(errorText));
        Assert.That((object?)e2.Exception, Is.EqualTo(null));

        var e3 = new BindingCompleteEventArgs(b, BindingCompleteState.Success, c, errorText, ex);

        Assert.That((object?)e3.Binding, Is.EqualTo(b));
        Assert.That((object?)e3.BindingCompleteState, Is.EqualTo(BindingCompleteState.Success));
        Assert.That((object?)e3.BindingCompleteContext, Is.EqualTo(c));
        Assert.That((object?)e3.Cancel, Is.EqualTo(true));
        Assert.That((object?)e3.ErrorText, Is.EqualTo(errorText));
        Assert.That((object?)e3.Exception, Is.EqualTo(ex));

        var e4 = new BindingCompleteEventArgs(b, BindingCompleteState.Success, c, errorText, ex, true);

        Assert.That((object?)e4.Binding, Is.EqualTo(b));
        Assert.That((object?)e4.BindingCompleteState, Is.EqualTo(BindingCompleteState.Success));
        Assert.That((object?)e4.BindingCompleteContext, Is.EqualTo(c));
        Assert.That((object?)e4.Cancel, Is.EqualTo(true));
        Assert.That((object?)e4.ErrorText, Is.EqualTo(errorText));
        Assert.That((object?)e4.Exception, Is.EqualTo(ex));

    }

    [Test]
    public void TestBindingManagerDataErrorEventArgs()
    {
        Exception ex = new ArgumentNullException();

        var e = new BindingManagerDataErrorEventArgs(ex);

        Assert.That((object?)e.Exception, Is.EqualTo(ex));
    }

    [Test]
    public void TestCacheVirtualItemsEventArgs()
    {
        var start = 7;
        var end = 26;

        var e = new CacheVirtualItemsEventArgs(start, end);

        Assert.That((object?)e.StartIndex, Is.EqualTo(start));
        Assert.That((object?)e.EndIndex, Is.EqualTo(end));
    }

    [Test]
    public void TestColumnReorderedEventArgs()
    {
        var oldindex = 7;
        var newindex = 26;
        var ch = new ColumnHeader();
        ch.Text = "TestHeader";

        var e = new ColumnReorderedEventArgs(oldindex, newindex, ch);

        Assert.That((object?)e.OldDisplayIndex, Is.EqualTo(oldindex));
        Assert.That((object?)e.NewDisplayIndex, Is.EqualTo(newindex));
        Assert.That((object?)e.Header, Is.EqualTo(ch));
        Assert.That((object?)e.Cancel, Is.EqualTo(false));
    }

    [Test]
    public void TestColumnWidthChangedEventArgs()
    {
        var col = 42;

        var e = new ColumnWidthChangedEventArgs(col);

        Assert.That((object?)e.ColumnIndex, Is.EqualTo(col));
    }

    [Test]
    public void TestColumnWidthChangingEventArgs()
    {
        var col = 27;
        var width = 543;

        var e = new ColumnWidthChangingEventArgs(col, width);

        Assert.That((object?)e.ColumnIndex, Is.EqualTo(col));
        Assert.That((object?)e.NewWidth, Is.EqualTo(width));
        Assert.That((object?)e.Cancel, Is.EqualTo(false));

        var e2 = new ColumnWidthChangingEventArgs(col, width, true);

        Assert.That((object?)e2.ColumnIndex, Is.EqualTo(col));
        Assert.That((object?)e2.NewWidth, Is.EqualTo(width));
        Assert.That((object?)e2.Cancel, Is.EqualTo(true));
    }

    [Test]
    public void TestFormClosedEventArgs()
    {
        var cr = CloseReason.WindowsShutDown;

        var e = new FormClosedEventArgs(cr);

        Assert.That((object?)e.CloseReason, Is.EqualTo(cr));
    }

    [Test]
    public void TestFormClosingEventArgs()
    {
        var cr = CloseReason.WindowsShutDown;

        var e = new FormClosingEventArgs(cr, true);

        Assert.That((object?)e.CloseReason, Is.EqualTo(cr));
        Assert.That((object?)e.Cancel, Is.EqualTo(true));
    }

    [Test]
    public void TestItemCheckedEventArgs()
    {
        var item = new ListViewItem("TestItem");

        var e = new ItemCheckedEventArgs(item);

        Assert.That((object?)e.Item, Is.EqualTo(item));
    }

    [Test]
    public void TestListControlConvertEventArgs()
    {
        var item = new ListViewItem("TestItem");
        var value = (object)"TestObject";
        var t = typeof(string);

        var e = new ListControlConvertEventArgs(value, t, item);

        Assert.That(e.ListItem, Is.EqualTo(item));
        Assert.That(e.Value, Is.EqualTo(value));
        Assert.That((object?)e.DesiredType, Is.EqualTo(t));
    }

    [Test]
    public void TestListViewItemMouseHoverEventArgs()
    {
        var item = new ListViewItem("TestItem");

        var e = new ListViewItemMouseHoverEventArgs(item);

        Assert.That((object?)e.Item, Is.EqualTo(item));
    }

    [Test]
    public void TestListViewItemSelectionChangedEventArgs()
    {
        var item = new ListViewItem("TestItem");
        var selected = false;
        var index = 35;

        var e = new ListViewItemSelectionChangedEventArgs(item, index, selected);

        Assert.That((object?)e.Item, Is.EqualTo(item));
        Assert.That((object?)e.IsSelected, Is.EqualTo(selected));
        Assert.That((object?)e.ItemIndex, Is.EqualTo(index));
    }

    [Test]
    public void TestListViewVirtualItemsSelectionRangeChangedEventArgs()
    {
        var selected = false;
        var start = 3;
        var end = 76;

        var e = new ListViewVirtualItemsSelectionRangeChangedEventArgs(start, end, selected);

        Assert.That((object?)e.IsSelected, Is.EqualTo(selected));
        Assert.That((object?)e.StartIndex, Is.EqualTo(start));
        Assert.That((object?)e.EndIndex, Is.EqualTo(end));
    }

    [Test]
    public void TestMaskInputRejectedEventArgs()
    {
        var pos = 2;
        var hint = MaskedTextResultHint.InvalidInput;

        var e = new MaskInputRejectedEventArgs(pos, hint);

        Assert.That((object?)e.Position, Is.EqualTo(pos));
        Assert.That((object?)e.RejectionHint, Is.EqualTo(hint));
    }

    [Test]
    public void TestPopupEventArgs()
    {
        Control c = new ListBox();
        IWin32Window? w = null;
        var balloon = true;
        var s = new Size(123, 54);

        var e = new PopupEventArgs(w, c, balloon, s);

        Assert.That((object?)e.AssociatedControl, Is.EqualTo(c));
        Assert.That((object?)e.AssociatedWindow, Is.EqualTo(w));
        Assert.That((object?)e.IsBalloon, Is.EqualTo(balloon));
        Assert.That((object?)e.ToolTipSize, Is.EqualTo(s));
    }

    [Test]
    public void TestPreviewKeyDownEventArgs()
    {
        var k = (Keys)196674;  // Control-Shift-B

        var e = new PreviewKeyDownEventArgs(k);

        Assert.That((object?)e.Alt, Is.EqualTo(false));
        Assert.That((object?)e.Control, Is.EqualTo(true));
        Assert.That((object?)e.IsInputKey, Is.EqualTo(false));
        Assert.That((object?)e.KeyCode, Is.EqualTo((Keys)66));  // B
        Assert.That((object?)e.KeyData, Is.EqualTo(k));
        Assert.That((object?)e.KeyValue, Is.EqualTo(66));
        Assert.That((object?)e.Modifiers, Is.EqualTo((Keys)196608));  // Control + Shift
        Assert.That((object?)e.Shift, Is.EqualTo(true));

        e.IsInputKey = true;

        Assert.That((object?)e.IsInputKey, Is.EqualTo(true));
    }

    [Test]
    public void TestRetrieveVirtualItemEventArgs()
    {
        var item = new ListViewItem("TestItem");
        var index = 75;

        var e = new RetrieveVirtualItemEventArgs(index);

        Assert.That((object?)e.ItemIndex, Is.EqualTo(index));
        Assert.That((object?)e.Item, Is.EqualTo(null));

        e.Item = item;

        Assert.That((object?)e.Item, Is.EqualTo(item));
    }

    [Test]
    public void TestSplitterCancelEventArgs()
    {
        var mx = 23;
        var my = 33;
        var sx = 43;
        var sy = 53;

        var e = new SplitterCancelEventArgs(mx, my, sx, sy);

        Assert.That((object?)e.MouseCursorX, Is.EqualTo(mx));
        Assert.That((object?)e.MouseCursorY, Is.EqualTo(my));
        Assert.That((object?)e.SplitX, Is.EqualTo(sx));
        Assert.That((object?)e.SplitY, Is.EqualTo(sy));

        e.SplitX = 11;
        e.SplitY = 12;

        Assert.That((object?)e.SplitX, Is.EqualTo(11));
        Assert.That((object?)e.SplitY, Is.EqualTo(12));
    }

    [Test]
    public void TestTabControlCancelEventArgs()
    {
        var tca = TabControlAction.Deselecting;
        var tp = new TabPage("HI!");
        var index = 477;

        var e = new TabControlCancelEventArgs(tp, index, true, tca);

        Assert.That((object?)e.Action, Is.EqualTo(tca));
        Assert.That((object?)e.TabPage, Is.EqualTo(tp));
        Assert.That((object?)e.TabPageIndex, Is.EqualTo(index));
        Assert.That((object?)e.Cancel, Is.EqualTo(true));
    }

    [Test]
    public void TestTabControlEventArgs()
    {
        var tca = TabControlAction.Selected;
        var tp = new TabPage("HI!");
        var index = 477;

        var e = new TabControlEventArgs(tp, index, tca);

        Assert.That((object?)e.Action, Is.EqualTo(tca));
        Assert.That((object?)e.TabPage, Is.EqualTo(tp));
        Assert.That((object?)e.TabPageIndex, Is.EqualTo(index));
    }

    [Test]
    public void TestTableLayoutCellPaintEventArgs()
    {
        var bounds = new Rectangle(0, 0, 100, 200);
        var clip = new Rectangle(50, 50, 50, 50);
        var col = 54;
        var row = 77;
        var b = new Bitmap(100, 100);
        var g = Graphics.FromImage(b);

        var e = new TableLayoutCellPaintEventArgs(g, clip, bounds, col, row);

        Assert.That((object?)e.CellBounds, Is.EqualTo(bounds));
        Assert.That((object?)e.Column, Is.EqualTo(col));
        Assert.That((object?)e.Row, Is.EqualTo(row));
        Assert.That((object?)e.Graphics, Is.EqualTo(g));
        Assert.That((object?)e.ClipRectangle, Is.EqualTo(clip));
    }

    [Test]
    public void TestTreeNodeMouseClickEventArgs()
    {
        var tn = new TreeNode("HI");
        var clicks = 4;
        var x = 75;
        var y = 34;
        var mb = MouseButtons.Right;

        var e = new TreeNodeMouseClickEventArgs(tn, mb, clicks, x, y);

        Assert.That((object?)e.Node, Is.EqualTo(tn));
        Assert.That((object?)e.Clicks, Is.EqualTo(clicks));
        Assert.That((object?)e.X, Is.EqualTo(x));
        Assert.That((object?)e.Y, Is.EqualTo(y));
        Assert.That((object?)e.Button, Is.EqualTo(mb));
    }

    [Test]
    public void TestTreeNodeMouseHoverEventArgs()
    {
        var tn = new TreeNode("HI");

        var e = new TreeNodeMouseHoverEventArgs(tn);

        Assert.That((object?)e.Node, Is.EqualTo(tn));
    }

    [Test]
    public void TestTypeValidationEventArgs()
    {
        var valid = true;
        var message = "This is a test.";
        var rv = (object)"MyObject";
        var vt = typeof(int);

        var e = new TypeValidationEventArgs(vt, valid, rv, message);

        Assert.That((object?)e.IsValidInput, Is.EqualTo(valid));
        Assert.That((object?)e.Message, Is.EqualTo(message));
        Assert.That(e.ReturnValue, Is.EqualTo(rv));
        Assert.That((object?)e.ValidatingType, Is.EqualTo(vt));
        Assert.That((object?)e.Cancel, Is.EqualTo(false));

        e.Cancel = true;

        Assert.That((object?)e.Cancel, Is.EqualTo(true));
    }

    [Test]
    public void TestWebBrowserDocumentCompletedEventArgs()
    {
        var url = new Uri("http://www.example.com/");

        var e = new WebBrowserDocumentCompletedEventArgs(url);

        Assert.That((object?)e.Url, Is.EqualTo(url));
    }

    [Test]
    public void TestWebBrowserNavigatedEventArgs()
    {
        var url = new Uri("http://www.example.com/");

        var e = new WebBrowserNavigatedEventArgs(url);

        Assert.That((object?)e.Url, Is.EqualTo(url));
    }

    [Test]
    public void TestWebBrowserNavigatingEventArgs()
    {
        var url = new Uri("http://www.example.com/");
        var frame = "TOP";

        var e = new WebBrowserNavigatingEventArgs(url, frame);

        Assert.That((object?)e.Url, Is.EqualTo(url));
        Assert.That((object?)e.TargetFrameName, Is.EqualTo(frame));
    }

    [Test]
    public void TestWebBrowserProgressChangedEventArgs()
    {
        long current = 3000;
        long max = 5000;

        var e = new WebBrowserProgressChangedEventArgs(current, max);

        Assert.That((object?)e.CurrentProgress, Is.EqualTo(current));
        Assert.That((object?)e.MaximumProgress, Is.EqualTo(max));
    }

}