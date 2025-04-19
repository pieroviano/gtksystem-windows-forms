//
// Copyright (c) 2007 Novell, Inc.
//
// Authors:
//      Rolf Bjarne Kvinge  (RKvinge@novell.com)
//

using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;
using Thread = System.Threading.Thread;
using System.Reflection;
using GtkTests.Helpers;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class MaskedTextBoxTest : TestHelper
{
    [SetUp]
    protected override void SetUp()
    {
        Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
        base.SetUp();
    }

    [Test]
    public void InitialProperties()
    {
        var mtb = new MaskedTextBox();
        Assert.That((object?)mtb.Lines.Length, Is.EqualTo(0));
        Assert.That((object?)mtb.Mask, Is.EqualTo(string.Empty));
        Assert.That((object?)mtb.Multiline, Is.EqualTo(false));
        Assert.That((object?)mtb.PasswordChar, Is.EqualTo('\0'));
        Assert.That((object?)mtb.ReadOnly, Is.EqualTo(false));
        Assert.That((object?)mtb.TextMaskFormat, Is.EqualTo(MaskFormat.IncludeLiterals));
        Assert.IsNull(mtb.ValidatingType);

        mtb.Dispose();
    }

    [Test]
    public void ValidatingTypeTest()
    {
        var mtb = new MaskedTextBox();
        Assert.IsNull(mtb.ValidatingType);
        mtb.ValidatingType = typeof(int);
        Assert.IsNotNull(mtb.ValidatingType);
        object expected = typeof(int);
        Assert.That((object?)mtb.ValidatingType, Is.SameAs(expected));
        mtb.Dispose();
    }

    [Test]
    public void TextMaskFormatTest()
    {
        var mtb = new MaskedTextBox();
        Assert.That((object?)mtb.TextMaskFormat, Is.EqualTo(MaskFormat.IncludeLiterals));
        mtb.TextMaskFormat = MaskFormat.ExcludePromptAndLiterals;
        Assert.That((object?)mtb.TextMaskFormat, Is.EqualTo(MaskFormat.ExcludePromptAndLiterals));
        mtb.TextMaskFormat = MaskFormat.IncludePromptAndLiterals;
        Assert.That((object?)mtb.TextMaskFormat, Is.EqualTo(MaskFormat.IncludePromptAndLiterals));
        mtb.TextMaskFormat = MaskFormat.IncludePrompt;
        Assert.That((object?)mtb.TextMaskFormat, Is.EqualTo(MaskFormat.IncludePrompt));
        mtb.TextMaskFormat = MaskFormat.IncludeLiterals;
        Assert.That((object?)mtb.TextMaskFormat, Is.EqualTo(MaskFormat.IncludeLiterals));
        mtb.Dispose();
    }

    [Test]
    public void TextMaskFormatExceptionTestException()
    {
        Assert.Throws<InvalidEnumArgumentException>(() =>
        {
            var mtb = new MaskedTextBox();
            mtb.TextMaskFormat = (MaskFormat)123;
            mtb.Dispose();
        });
    }

    [Test]
    public void TextTest()
    {
        var mtb = new MaskedTextBox();
        Assert.That((object?)mtb.Text, Is.EqualTo(string.Empty));
        mtb.Text = "abc";
        Assert.That((object?)mtb.Text, Is.EqualTo("abc"));
        mtb.Text = "ABC";
        Assert.That((object?)mtb.Text, Is.EqualTo("ABC"));
        mtb.Mask = "abc";
        mtb.Text = "abc";
        Assert.That((object?)mtb.Text, Is.EqualTo("abc"));
        mtb.Text = "ABC";
        Assert.That((object?)mtb.Text, Is.EqualTo("Abc"));
        mtb.Text = "123";
        Assert.That((object?)mtb.Text, Is.EqualTo("1bc"));
        mtb.Dispose();
    }

    [Test]
    public void TextTest2()
    {
        var mtb = new MaskedTextBox();
        mtb.Mask = "99 99";

        mtb.Text = "23 34";
        Assert.That((object?)mtb.Text, Is.EqualTo("23 34"));

        mtb.Dispose();
    }

    [Test]
    public void TextTest3()
    {
        var mtb = new MaskedTextBox();
        mtb.Mask = "00-00";
        mtb.Text = "12 3";
        Assert.That((object?)mtb.Text, Is.EqualTo("12- 3"));

        mtb.Text = "b31i4";
        Assert.That((object?)mtb.Text, Is.EqualTo("31-4"));

        mtb.Text = "1234";
        Assert.That((object?)mtb.Text, Is.EqualTo("12-34"));

        mtb.Dispose();
    }

    [Test]
    public void ReadOnlyTest()
    {
        var mtb = new MaskedTextBox();
        Assert.That((object?)mtb.ReadOnly, Is.EqualTo(false));
        mtb.ReadOnly = true;
        Assert.That((object?)mtb.ReadOnly, Is.EqualTo(true));
        mtb.Dispose();
    }

    [Test]
    public void PasswordCharTest()
    {
        var mtb = new MaskedTextBox();
        Assert.That((object?)mtb.PasswordChar, Is.EqualTo('\0'));
        mtb.PasswordChar = '*';
        Assert.That((object?)mtb.PasswordChar, Is.EqualTo('*'));
        mtb.Dispose();
    }

    [Test]
    public void MultilineTest()
    {
        var mtb = new MaskedTextBox();
        Assert.That((object?)mtb.Multiline, Is.EqualTo(false));
        mtb.Multiline = true;
        Assert.That((object?)mtb.Multiline, Is.EqualTo(false));
        mtb.Dispose();
    }

    [Test]
    public void MaskTest()
    {
        var mtb = new MaskedTextBox();
        Assert.That((object?)mtb.Mask, Is.EqualTo(string.Empty));
        mtb.Mask = "abc";
        Assert.That((object?)mtb.Mask, Is.EqualTo("abc"));
        mtb.Mask = string.Empty;
        Assert.That((object?)mtb.Mask, Is.EqualTo(string.Empty));
        mtb.Mask = null!;
        Assert.That((object?)mtb.Mask, Is.EqualTo(string.Empty));
        mtb.Mask = string.Empty;
        Assert.That((object?)mtb.Mask, Is.EqualTo(string.Empty));
        mtb.Dispose();
    }

    [Test]
    public void LinesTest()
    {
        var mtb = new MaskedTextBox();
        Assert.That((object?)mtb.Lines.Length, Is.EqualTo(0));
        mtb.Text = "abc";
        Assert.That((object?)mtb.Lines.Length, Is.EqualTo(1));
        Assert.That((object?)mtb.Lines[0], Is.EqualTo("abc"), "#L2a");
        mtb.Text = "abc\nabc";
        Assert.That((object?)mtb.Lines.Length, Is.EqualTo(2));
        Assert.That((object?)mtb.Lines[0], Is.EqualTo("abc"), "#L3a");
        Assert.That((object?)mtb.Lines[1], Is.EqualTo("abc"), "#L3b");
        mtb.Dispose();
    }

    [Test]
    public void CreateHandleTest()
    {
        using var mtb = new MaskedTextBox();
        Assert.That((object?)mtb.IsHandleCreated, Is.EqualTo(false));
        typeof(MaskedTextBox).GetMethod("CreateHandle", BindingFlags.Instance | BindingFlags.NonPublic)!.Invoke(mtb,
            []);
        Assert.That((object?)mtb.IsHandleCreated, Is.EqualTo(true));
    }

    [Test]
    public void IsInputKeyTest()
    {
        using var f = new Form();
        using var mtb = new MaskedTextBox();
        f.Controls.Add(mtb);
        f.Show();
        var IsInputKey = typeof(MaskedTextBox).GetMethod("IsInputKey", BindingFlags.NonPublic | BindingFlags.Instance);

        for (var i = 0; i <= 0xFF; i++)
        {
            var key = (Keys)i;
            var key_ALT = key | Keys.Alt;
            var key_SHIFT = key | Keys.Shift;
            var key_CTRL = key | Keys.Control;
            var key_ALT_SHIFT = key | Keys.Alt | Keys.Shift;
            var key_ALT_CTRL = key | Keys.Alt | Keys.Control;
            var key_SHIFT_CTLR = key | Keys.Shift | Keys.Control;
            var key_ALT_SHIFT_CTLR = key | Keys.Alt | Keys.Shift | Keys.Control;

            var is_input = false;

            switch (key)
            {
                case Keys.PageDown:
                case Keys.PageUp:
                case Keys.End:
                case Keys.Home:
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                case Keys.Back:
                    is_input = true;
                    break;
            }

            Assert.That((object?)(bool)IsInputKey!.Invoke(mtb, [key])!, Is.EqualTo(is_input));
            Assert.That((object?)(bool)IsInputKey.Invoke(mtb, [key_ALT])!, Is.EqualTo(false));
            Assert.That((object?)(bool)IsInputKey.Invoke(mtb, [key_SHIFT])!, Is.EqualTo(is_input));
            Assert.That((object?)(bool)IsInputKey.Invoke(mtb, [key_CTRL])!, Is.EqualTo(is_input));
            Assert.That((object?)(bool)IsInputKey.Invoke(mtb, [key_ALT_SHIFT])!, Is.EqualTo(false));
            Assert.That((object?)(bool)IsInputKey.Invoke(mtb, [key_ALT_CTRL])!, Is.EqualTo(false));
            Assert.That((object?)(bool)IsInputKey.Invoke(mtb, [key_SHIFT_CTLR])!, Is.EqualTo(is_input));
            Assert.That((object?)(bool)IsInputKey.Invoke(mtb, [key_ALT_SHIFT_CTLR])!, Is.EqualTo(false));
        }
    }

    [Test]
    public void ValidateTextTest()
    {
        Assert.Ignore("Pending implementation");
    }

    [Test]
    public void ToStringTest()
    {
        Assert.Ignore("Pending implementation");
    }
}