//
// ToolStripButtonTests.cs
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

using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using GtkTests.Helpers;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class ToolStripButtonTests : TestHelper
{
    [Test]
    public void Constructor ()
    {
        var tsi = new ToolStripButton ();

        Assert.That((object?)tsi.AutoToolTip, Is.EqualTo(true));
        Assert.That((object?)tsi.Checked, Is.EqualTo(false));
        Assert.That((object?)tsi.CheckOnClick, Is.EqualTo(false));
        Assert.That((object?)tsi.CheckState, Is.EqualTo(CheckState.Unchecked));

        var count = 0;
        var oc = new EventHandler (delegate { count++; });
        Image i = new Bitmap (1,1);
			
    }

    [Test]
    public void ProtectedProperties ()
    {
        var epp = new ExposeProtectedProperties ();

        Assert.That((object?)epp.DefaultAutoToolTip, Is.EqualTo(true));
    }

    [Test]
    public void PropertyAutoToolTip ()
    {
        var tsi = new ToolStripButton ();
        var ew = new EventWatcher (tsi);

        tsi.AutoToolTip = true;
        Assert.That((object?)tsi.AutoToolTip, Is.EqualTo(true));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));

        ew.Clear ();
        tsi.AutoToolTip = true;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyChecked ()
    {
        var tsi = new ToolStripButton ();
        var ew = new EventWatcher (tsi);

        tsi.Checked = true;
        Assert.That((object?)tsi.Checked, Is.EqualTo(true));
        Assert.That((object?)ew.ToString (), Is.EqualTo("CheckedChanged;CheckStateChanged"));

        ew.Clear ();
        tsi.Checked = true;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyCheckOnClick ()
    {
        var tsi = new ToolStripButton ();
        var ew = new EventWatcher (tsi);

        tsi.CheckOnClick = true;
        Assert.That((object?)tsi.CheckOnClick, Is.EqualTo(true));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));

        ew.Clear ();
        tsi.CheckOnClick = true;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyCheckState ()
    {
        var tsi = new ToolStripButton ();
        var ew = new EventWatcher (tsi);

        tsi.CheckState = CheckState.Checked;
        Assert.That((object?)tsi.CheckState, Is.EqualTo(CheckState.Checked));
        Assert.That((object?)ew.ToString (), Is.EqualTo("CheckedChanged;CheckStateChanged"));

        ew.Clear ();
        tsi.CheckState = CheckState.Checked;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyCheckStateIEAE ()
    {
        Assert.Throws<InvalidEnumArgumentException>(() =>
        {
            var tsi = new ToolStripButton();
            tsi.CheckState = (CheckState)42;
        });
    }

    private class EventWatcher
    {
        private string events = string.Empty;
			
        public EventWatcher (ToolStripButton tsi)
        {
            tsi.CheckedChanged += delegate { events += ("CheckedChanged;"); };
            tsi.CheckStateChanged += delegate { events += ("CheckStateChanged;"); };
        }

        public override string ToString ()
        {
            return events.TrimEnd (';');
        }
			
        public void Clear ()
        {
            events = string.Empty;
        }
    }
		
    private class ExposeProtectedProperties : ToolStripButton
    {
        public new bool DefaultAutoToolTip => base.DefaultAutoToolTip;
    }
}