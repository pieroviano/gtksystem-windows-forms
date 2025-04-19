//
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
// (C) 2006 Novell, Inc.
//

using GtkTests.Helpers;
using System.Windows.Forms;

namespace GtkTests.Controls;

[TestFixture]
public class ColumnStyleTest : TestHelper {

    [Test]
    public void CtorTest1 ()
    {
        var cs = new ColumnStyle ();
        Assert.That((object?)cs.Width, Is.EqualTo(0.0f), "1");
        Assert.That((object?)cs.SizeType, Is.EqualTo(SizeType.AutoSize), "2");
    }

    [Test]
    public void CtorTest2 ()
    {
        var cs = new ColumnStyle (SizeType.Absolute);
			
        Assert.That((object?)cs.Width, Is.EqualTo(0.0f), "1");
        Assert.That((object?)cs.SizeType, Is.EqualTo(SizeType.Absolute), "2");
    }

    [Test]
    public void CtorTest3 ()
    {
        var cs = new ColumnStyle (SizeType.Absolute, 5.0f);
			
        Assert.That((object?)cs.Width, Is.EqualTo(5.0), "1");
        Assert.That((object?)cs.SizeType, Is.EqualTo(SizeType.Absolute), "2");
    }

    [Test]
    public void CtorTest4 ()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var cs = new ColumnStyle(SizeType.Absolute, -1.0f);

            RemoveWarning(cs);
        });
    }

    [Test]
    public void WidthTest1 ()
    {
        var cs = new ColumnStyle
        {
            Width = 1.0f
        };
        Assert.That((object?)cs.Width, Is.EqualTo(1.0f), "1");
    }

    [Test]
    public void WidthTest2 ()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var cs = new ColumnStyle
            {
                Width = -1.0f
            };
        });
    }
}