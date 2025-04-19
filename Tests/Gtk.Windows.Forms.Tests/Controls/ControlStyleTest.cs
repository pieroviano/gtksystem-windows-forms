//
// ControlStyleTest.cs
//
// Author: 
//   Peter Dennis Bartok (pbartok@novell.com)
//
// (C) 2005 Novell, Inc. (http://www.novell.com)
//

using GtkTests.Helpers;
using System.Windows.Forms;

namespace GtkTests.Controls;

[TestFixture]
public class TestControlStyle : TestHelper
{
    private static readonly Array values = Enum.GetValues(typeof(ControlStyles));
    private static readonly string[] names = Enum.GetNames(typeof(ControlStyles));

    public static string[] GetStyles<TControl>(TControl control) where TControl : Control
    {
        string[] result = new string[names.Length];

        for (var i = 0; i < values.Length; i++)
        {
            result[i] = names[i] + "=" + control.GetStyle((ControlStyles)values.GetValue(i)!);
        }

        return result;
    }

    [Test]
    public void ControlStyleTest()
    {
        PrepareAssert(new Control(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }


    [Test]
    public void ButtonStyleTest()
    {
        PrepareAssert(new Button(), ["ContainerControl=False", "UserPaint=False", "Opaque=False", "ResizeRedraw=False", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=False", "UserMouse=False", "SupportsTransparentBackColor=False", "StandardDoubleClick=False", "AllPaintingInWmPaint=False", "CacheText=False", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=False", "UseTextForAccessibility=False"]);
    }

    [Test]
    public void CheckBoxStyleTest()
    {
        PrepareAssert(new CheckBox(), ["ContainerControl=False", "UserPaint=False", "Opaque=False", "ResizeRedraw=False", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=False", "UserMouse=False", "SupportsTransparentBackColor=False", "StandardDoubleClick=False", "AllPaintingInWmPaint=False", "CacheText=False", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=False", "UseTextForAccessibility=False"]);
    }

    [Test]
    public void RadioButtonStyleTest()
    {
        PrepareAssert(new RadioButton(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    [Test]
    public void DateTimePickerStyleTest()
    {
        PrepareAssert(new DateTimePicker(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    [Test]
    public void GroupBoxStyleTest()
    {
        PrepareAssert(new GroupBox(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    [Test]
    public void LabelStyleTest()
    {
        PrepareAssert(new Label(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    [Test]
    public void LinkLabelStyleTest()
    {
        PrepareAssert(new LinkLabel(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    [Test]
    public void ComboBoxStyleTest()
    {
        PrepareAssert(new ComboBox(), ["ContainerControl=False", "UserPaint=False", "Opaque=False", "ResizeRedraw=False", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=False", "UserMouse=False", "SupportsTransparentBackColor=False", "StandardDoubleClick=False", "AllPaintingInWmPaint=False", "CacheText=False", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=False", "UseTextForAccessibility=False"]);
    }

    [Test]
    public void ListBoxStyleTest()
    {
        PrepareAssert(new ListBox(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    [Test]
    public void CheckedListBoxStyleTest()
    {
        PrepareAssert(new CheckedListBox(), ["ContainerControl=False", "UserPaint=False", "Opaque=False", "ResizeRedraw=False", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=False", "UserMouse=False", "SupportsTransparentBackColor=False", "StandardDoubleClick=False", "AllPaintingInWmPaint=False", "CacheText=False", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=False", "UseTextForAccessibility=False"]);
    }

    [Test]
    public void ListViewStyleTest()
    {
        PrepareAssert(new ListView(), ["ContainerControl=False", "UserPaint=False", "Opaque=False", "ResizeRedraw=False", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=False", "UserMouse=False", "SupportsTransparentBackColor=False", "StandardDoubleClick=False", "AllPaintingInWmPaint=False", "CacheText=False", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=False", "UseTextForAccessibility=False"]);
    }

    [Test]
    public void MonthCalendarStyleTest()
    {
        PrepareAssert(new MonthCalendar(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    [Test]
    public void PictureBoxStyleTest()
    {
        PrepareAssert(new PictureBox(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    [Test]
    public void ProgressBarStyleTest()
    {
        PrepareAssert(new ProgressBar(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    [Test]
    public void ScrollableControlStyleTest()
    {
        PrepareAssert(new ScrollableControl(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    [Test]
    public void ContainerControlStyleTest()
    {
        PrepareAssert(new ContainerControl(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    [Test]
    public void FormStyleTest()
    {
        var f = new Form();
        f.ShowInTaskbar = false;
        PrepareAssert(f, ["ContainerControl=False", "UserPaint=False", "Opaque=False", "ResizeRedraw=False", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=False", "UserMouse=False", "SupportsTransparentBackColor=False", "StandardDoubleClick=False", "AllPaintingInWmPaint=False", "CacheText=False", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=False", "UseTextForAccessibility=False"]);
        f.Dispose();
    }

    [Test]
    public void PropertyGridStyleTest()
    {
        PrepareAssert(new PropertyGrid(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    [Test]
    public void NumericUpDownStyleTest()
    {
        PrepareAssert(new NumericUpDown(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    [Test]
    public void UserControlStyleTest()
    {
        PrepareAssert(new UserControl(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    [Test]
    public void PanelStyleTest()
    {
        PrepareAssert(new Panel(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    [Test]
    public void TabPageStyleTest()
    {
        PrepareAssert(new TabPage(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    [Test]
    public void HScrollBarStyleTest()
    {
        PrepareAssert(new HScrollBar(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    [Test]
    public void VScrollBarStyleTest()
    {
        PrepareAssert(new VScrollBar(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    [Test]
    public void TabControlStyleTest()
    {
        PrepareAssert(new TabControl(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    [Test]
    public void RichTextBoxStyleTest()
    {
        PrepareAssert(new RichTextBox(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    [Test]
    public void TextBoxStyleTest()
    {
        PrepareAssert(new TextBox(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    [Test]
    public void TrackBarStyleTest()
    {
        PrepareAssert(new TrackBar(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    [Test]
    public void TreeViewStyleTest()
    {
        PrepareAssert(new TreeView(), ["ContainerControl=False", "UserPaint=True", "Opaque=True", "ResizeRedraw=True", "FixedWidth=False", "FixedHeight=False", "StandardClick=False", "Selectable=True", "UserMouse=True", "SupportsTransparentBackColor=True", "StandardDoubleClick=False", "AllPaintingInWmPaint=True", "CacheText=True", "EnableNotifyMessage=False", "DoubleBuffer=False", "OptimizedDoubleBuffer=True", "UseTextForAccessibility=True"]);
    }

    private static void PrepareAssert<TControl>(TControl control, params string[] want) where TControl : Control
    {
        var styles = GetStyles(control);
        Assert.That((object?)styles, Is.EqualTo(want), $"{typeof(TControl).Name}Styles");
    }

}