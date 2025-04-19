//
// ToolStripProgressBarTests.cs
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
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class ToolStripProgressBarTests : TestHelper
{
    [Test]
    public void Constructor ()
    {
        var tsi = new ToolStripProgressBar ();

        Assert.That((object?)tsi.MarqueeAnimationSpeed, Is.EqualTo(100));
        Assert.That((object?)tsi.Maximum, Is.EqualTo(100));
        Assert.That((object?)tsi.Minimum, Is.EqualTo(0));
        Assert.That((object?)tsi.ProgressBar.GetType ().ToString (), Is.EqualTo("System.Windows.Forms.ProgressBar"));
        Assert.That((object?)tsi.Step, Is.EqualTo(10));
        Assert.That((object?)tsi.Style, Is.EqualTo(ProgressBarStyle.Blocks));
        object expected = string.Empty;
        Assert.That((object?)tsi.Text, Is.EqualTo(expected));
        Assert.That((object?)tsi.Value, Is.EqualTo(0));

        tsi = new ToolStripProgressBar ("Bob");
        Assert.That((object?)tsi.Name, Is.EqualTo("Bob"));
    }
	
    [Test]
    public void PropertyMarqueeAnimationSpeed ()
    {
        var tsi = new ToolStripProgressBar ();
        var ew = new EventWatcher (tsi);

        tsi.MarqueeAnimationSpeed = 200;
        Assert.That((object?)tsi.MarqueeAnimationSpeed, Is.EqualTo(200));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));

        ew.Clear ();
        tsi.MarqueeAnimationSpeed = 200;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyMaximum ()
    {
        var tsi = new ToolStripProgressBar ();
        var ew = new EventWatcher (tsi);

        tsi.Maximum = 200;
        Assert.That((object?)tsi.Maximum, Is.EqualTo(200));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));

        ew.Clear ();
        tsi.Maximum = 200;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyMinimum ()
    {
        var tsi = new ToolStripProgressBar ();
        var ew = new EventWatcher (tsi);

        tsi.Minimum = 200;
        Assert.That((object?)tsi.Minimum, Is.EqualTo(200));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));

        ew.Clear ();
        tsi.Minimum = 200;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyStep ()
    {
        var tsi = new ToolStripProgressBar ();
        var ew = new EventWatcher (tsi);

        tsi.Step = 200;
        Assert.That((object?)tsi.Step, Is.EqualTo(200));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));

        ew.Clear ();
        tsi.Step = 200;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyStyle ()
    {
        var tsi = new ToolStripProgressBar ();
        var ew = new EventWatcher (tsi);

        tsi.Style = ProgressBarStyle.Continuous;
        Assert.That((object?)tsi.Style, Is.EqualTo(ProgressBarStyle.Continuous));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));

        ew.Clear ();
        tsi.Style = ProgressBarStyle.Continuous;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyText ()
    {
        var tsi = new ToolStripProgressBar ();
        var ew = new EventWatcher (tsi);

        tsi.Text = "Hi";
        Assert.That((object?)tsi.Text, Is.EqualTo("Hi"));
        Assert.That((object?)tsi.ProgressBar.Text, Is.EqualTo("Hi"));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));

        ew.Clear ();
        tsi.Text = "Hi";
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyValue ()
    {
        var tsi = new ToolStripProgressBar ();
        var ew = new EventWatcher (tsi);

        tsi.Value = 30;
        Assert.That((object?)tsi.Value, Is.EqualTo(30));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected));

        ew.Clear ();
        tsi.Value = 30;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString (), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyValueAOORE ()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var tsi = new ToolStripProgressBar();

            tsi.Value = 200;
        });
    }

    [Test]
    public void BehaviorIncrement ()
    {
        var tsi = new ToolStripProgressBar ();
			
        tsi.Increment (14);
        Assert.That((object?)tsi.Value, Is.EqualTo(14));

        tsi.Increment (104);
        Assert.That((object?)tsi.Value, Is.EqualTo(100));

        tsi.Increment (-245);
        Assert.That((object?)tsi.Value, Is.EqualTo(0));
    }

    [Test]
    public void BehaviorPerformStep ()
    {
        var tsi = new ToolStripProgressBar ();

        tsi.PerformStep ();
        Assert.That((object?)tsi.Value, Is.EqualTo(10));
    }

    private class EventWatcher
    {
        private string events = string.Empty;
			
        public EventWatcher (ToolStripProgressBar tsi)
        {
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
}