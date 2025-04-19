//
// MenuStripTest.cs
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
// Copyright (c) 2006 Jonathan Pobst
//
// Authors:
//	Jonathan Pobst (monkey@jpobst.com)
//

using GtkTests.Helpers;
using System.Drawing;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class MenuStripTest : TestHelper
{
    [Test]
    public void Constructor ()
    {
        var ms = new MenuStrip ();

        Assert.That((object?)ms.CanSelect, Is.EqualTo(false));
        Assert.That((object?)ms.LayoutStyle, Is.EqualTo(ToolStripLayoutStyle.HorizontalStackWithOverflow));
			
        Assert.That((object?)ms.AccessibilityObject?.GetType ().ToString (), Is.EqualTo("System.Windows.Forms.MenuStrip+MenuStripAccessibleObject"));
    }

    [Test]
    public void ProtectedProperties ()
    {
        var epp = new ExposeProtectedProperties ();

        object expected = new Padding (6, 2, 0, 2);
        Assert.That((object?)epp.DefaultPadding, Is.EqualTo(expected));
        object expected1 = new Size (200, 24);
        Assert.That((object?)epp.DefaultSize, Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyShowItemToolTips ()
    {
        var ts = new StatusStrip ();

        ts.ShowItemToolTips = true;
        Assert.That((object?)ts.ShowItemToolTips, Is.EqualTo(true));
    }
		
    [Test]
    public void PropertyStretch ()
    {
        var ts = new StatusStrip ();

        ts.Stretch = false;
        Assert.That((object?)ts.Stretch, Is.EqualTo(false));
    }

    private class ExposeProtectedProperties : MenuStrip
    {
        public new Padding DefaultPadding => base.DefaultPadding;
        public new Size DefaultSize => base.DefaultSize;
    }
}