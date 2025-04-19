// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// Copyright (c) 2005 Novell, Inc. (http://www.novell.com)
//
// Author:
//	Pedro Martínez Juliá <pedromj@gmail.com>
//

using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Globalization;
using GtkTests.Helpers;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class DataGridViewCellStyleTest  : TestHelper {
    private DataGridViewCellStyle style;
		
    [SetUp]
    protected override void SetUp () {
        style = new DataGridViewCellStyle();
        base.SetUp ();
    }

    [Test]
    public void TestDefaultValues () {
        Assert.That((object?)style.Alignment, Is.EqualTo(DataGridViewContentAlignment.NotSet));
        Assert.That((object?)style.BackColor, Is.EqualTo(Color.Empty));
        Assert.That((object?)style.Font, Is.EqualTo(null));
        Assert.That((object?)style.ForeColor, Is.EqualTo(Color.Empty));
        object expected = string.Empty;
        Assert.That((object?)style.Format, Is.EqualTo(expected));
        Assert.That((object?)style.IsNullValueDefault, Is.EqualTo(true));
        object expected1 = string.Empty;
        Assert.That(style.NullValue, Is.EqualTo(expected1));
        Assert.That((object?)style.SelectionBackColor, Is.EqualTo(Color.Empty));
        Assert.That((object?)style.SelectionForeColor, Is.EqualTo(Color.Empty));
        Assert.That(style.Tag, Is.EqualTo(null));
        Assert.That((object?)style.WrapMode, Is.EqualTo(DataGridViewTriState.NotSet));
    }

    [Test]
    public void TestApplyStyle () {
        var style_aux = new DataGridViewCellStyle();
        style.ApplyStyle(style_aux);
        Assert.That((object?)style, Is.EqualTo(style_aux));
    }

    [Test]
    public void TestClone () {
        var style_aux = (DataGridViewCellStyle) style.Clone();
        Assert.That((object?)style, Is.EqualTo(style_aux));
    }

    [Test]
    public void TestEquals () {
        var style_aux = (DataGridViewCellStyle) style.Clone();
        Assert.That((object?)(style_aux.Equals(style)), Is.EqualTo(true));
    }

    [Test]
    public void TestAlignmentInvalidEnumArgumentException ()
    {
        Assert.Throws<InvalidEnumArgumentException>(() =>
        {
            style.Alignment = DataGridViewContentAlignment.BottomCenter | DataGridViewContentAlignment.BottomRight;
        });
    }

    [Test]
    public void TestWrapModeInvalidEnumArgumentException ()
    {
        Assert.Throws<InvalidEnumArgumentException>(() =>
        {
            style.WrapMode = (DataGridViewTriState)3;
        });
    }

    [Test]
    public void FormatProvider ()
    {
        var orignalCulture = CultureInfo.CurrentCulture;
        var orignalUICulture = CultureInfo.CurrentUICulture;

        try {
            Thread.CurrentThread.CurrentCulture = new CultureInfo ("nl-BE");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo ("ja-JP");
            Assert.That((object?)style.FormatProvider, Is.SameAs(CultureInfo.CurrentCulture));
            Thread.CurrentThread.CurrentCulture = new CultureInfo ("fr-FR");
            Assert.That((object?)style.FormatProvider, Is.SameAs(CultureInfo.CurrentCulture));
            style.FormatProvider = CultureInfo.CurrentCulture;
            Assert.That((object?)style.FormatProvider, Is.SameAs(CultureInfo.CurrentCulture));
            Thread.CurrentThread.CurrentCulture = new CultureInfo ("en-US");
            object expected = new CultureInfo ("fr-FR");
            Assert.That((object?)style.FormatProvider, Is.EqualTo(expected));
            style.FormatProvider = null;
            Assert.That((object?)style.FormatProvider, Is.SameAs(CultureInfo.CurrentCulture));
        } finally {
            Thread.CurrentThread.CurrentCulture = orignalCulture;
            Thread.CurrentThread.CurrentUICulture = orignalUICulture;
        }
    }

    [Test]
    public void IsFormatProviderDefault ()
    {
        var orignalCulture = CultureInfo.CurrentCulture;

        try {
            Assert.IsTrue (style.IsFormatProviderDefault);
            Thread.CurrentThread.CurrentCulture = new CultureInfo ("nl-BE");
            Assert.IsTrue (style.IsFormatProviderDefault);
            Thread.CurrentThread.CurrentCulture = new CultureInfo ("fr-FR");
            Assert.IsTrue (style.IsFormatProviderDefault);
            style.FormatProvider = CultureInfo.CurrentCulture;
            Assert.IsFalse (style.IsFormatProviderDefault);
            style.FormatProvider = new CultureInfo ("en-US");
            Assert.IsFalse (style.IsFormatProviderDefault);
            style.FormatProvider = null;
            Assert.IsTrue (style.IsFormatProviderDefault);
        } finally {
            Thread.CurrentThread.CurrentCulture = orignalCulture;
        }
    }
}