//
// StatusStripTests.cs
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
// Copyright (c) 2006 Novell, Inc.
//
// Authors:
//	Jonathan Pobst (monkey@jpobst.com)
//

using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using GtkTests.Helpers;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class StatusStripTests : TestHelper
{
    [Test]
    public void Constructor()
    {
        var ts = new StatusStrip();

        object expected = new Rectangle(1, 0, 185, 22);
        Assert.That((object?)ts.DisplayRectangle, Is.EqualTo(expected));
        Assert.That((object?)ts.Dock, Is.EqualTo(DockStyle.Bottom));
        Assert.That((object?)ts.LayoutStyle, Is.EqualTo(ToolStripLayoutStyle.Table));
        object expected1 = new Padding(1, 0, 14, 0);
        Assert.That((object?)ts.Padding, Is.EqualTo(expected1));
        Assert.That((object?)ts.ShowItemToolTips, Is.EqualTo(false));
        Assert.That((object?)ts.SizingGrip, Is.EqualTo(true));
        Assert.That((object?)ts.Stretch, Is.EqualTo(true));

        Assert.That((object?)ts.AccessibilityObject?.GetType().ToString(), Is.EqualTo("System.Windows.Forms.StatusStrip+StatusStripAccessibleObject"));
        Assert.That((object?)ts.LayoutEngine?.ToString(), Is.EqualTo("System.Windows.Forms.Layout.TableLayout"));
    }

    [Test]
    public void PropertyDock()
    {
        var ts = new StatusStrip();

        ts.Dock = DockStyle.Top;
        Assert.That((object?)ts.Dock, Is.EqualTo(DockStyle.Top));
    }

    [Test]
    public void PropertyDockIEAE()
    {
        Assert.Throws<InvalidEnumArgumentException>(() =>
        {
            var ts = new StatusStrip();
            ts.Dock = (DockStyle)42;
        });
    }

    [Test]
    public void PropertyLayoutStyle()
    {
        var ts = new StatusStrip();

        ts.LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
        Assert.That((object?)ts.LayoutStyle, Is.EqualTo(ToolStripLayoutStyle.VerticalStackWithOverflow));
    }

    [Test]
    public void PropertyLayoutStyleIEAE()
    {
        Assert.Throws<InvalidEnumArgumentException>(() =>
        {
            var ts = new StatusStrip();

            ts.LayoutStyle = (ToolStripLayoutStyle)42;
        });
    }

    [Test]
    public void PropertyPadding()
    {
        var ts = new StatusStrip();

        ts.Padding = new Padding(7);
        object expected = new Padding(7);
        Assert.That((object?)ts.Padding, Is.EqualTo(expected));
    }

    [Test]
    public void PropertyShowItemToolTips()
    {
        var ts = new StatusStrip();

        ts.ShowItemToolTips = true;
        Assert.That((object?)ts.ShowItemToolTips, Is.EqualTo(true));
    }

    [Test]
    public void PropertySizingGrip()
    {
        var ts = new StatusStrip();

        ts.SizingGrip = false;
        Assert.That((object?)ts.SizingGrip, Is.EqualTo(false));
    }

    [Test]
    public void PropertyStretch()
    {
        var ts = new StatusStrip();

        ts.Stretch = false;
        Assert.That((object?)ts.Stretch, Is.EqualTo(false));
    }

    [Test]
    public void Layout()
    {
        var ss = new StatusStrip();
        ToolStripStatusLabel label;

        ss.SuspendLayout();
        ss.Items.Add(string.Empty);
        ss.Items.Add(label = new ToolStripStatusLabel(string.Empty));
        ss.Items.Add(string.Empty);
        ss.ResumeLayout();

        object expected = new Rectangle(0, 0, 200, 22);
        Assert.That((object?)ss.Bounds, Is.EqualTo(expected));
        object expected1 = new Size(0, 17);
        Assert.That((object?)ss.Items[0].Size, Is.EqualTo(expected1));
        object expected2 = new Size(0, 17);
        Assert.That((object?)label.Size, Is.EqualTo(expected2));

        object expected3 = new Rectangle(1, 3, 0, 17);
        Assert.That((object?)ss.Items[0].Bounds, Is.EqualTo(expected3));
        object expected4 = new Rectangle(1, 3, 0, 17);
        Assert.That((object?)ss.Items[1].Bounds, Is.EqualTo(expected4));
        object expected5 = new Rectangle(1, 3, 0, 17);
        Assert.That((object?)ss.Items[2].Bounds, Is.EqualTo(expected5));

        label.Spring = true;

        object expected6 = new Rectangle(1, 3, 0, 17);
        Assert.That((object?)ss.Items[0].Bounds, Is.EqualTo(expected6));
        object expected7 = new Rectangle(1, 3, 185, 17);
        Assert.That((object?)ss.Items[1].Bounds, Is.EqualTo(expected7));
        object expected8 = new Rectangle(186, 3, 0, 17);
        Assert.That((object?)ss.Items[2].Bounds, Is.EqualTo(expected8));
    }
}