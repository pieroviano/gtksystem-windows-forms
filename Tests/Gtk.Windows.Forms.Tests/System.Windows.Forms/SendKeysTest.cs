/**
 * SendKeysTest.cs: Test cases for SendKeys
 * 
 * These tests can only run in ms.net one at a time.
 * Since ms.net apparently hooks the keyboard to 
 * implement this, running two tests in a row
 * makes the second test run before the hook
 * of the first test is released, effectively
 * hanging the keyboard. CTRL-ALT-DEL releases
 * the keyboard, but the test still hangs.
 * Running each test separately works.
 * 
 * Author:
 *		Andreia Gaita (avidigal@novell.com)
 * 
 * (C) 2005 Novell, Inc. (http://www.novell.com)
 * 
*/

using System.Windows.Forms;
using System.Collections;
using Timer = System.Windows.Forms.Timer;
using GtkTests.Helpers;
#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value

namespace GtkTests.System.Windows.Forms;

[TestFixture]
[Category("NotDotNet")]
[Category("NotWithXvfb")]
[Category("Interactive")]
public class SendKeysTest : TestHelper
{
    private static readonly Queue keys = new();

    internal struct Keys
    {
        public string key;
        public bool up;
        public bool shift;
        public bool ctrl;
        public bool alt;

        public Keys(string key, bool up, bool shift, bool ctrl, bool alt)
        {
            this.key = key;
            this.up = up;
            this.shift = shift;
            this.ctrl = ctrl;
            this.alt = alt;
        }
    }

    internal class Custom : TextBox
    {

        protected override void OnKeyDown(KeyEventArgs e)
        {
            keys.Enqueue(new Keys(e.KeyData.ToString(), false, e.Shift, e.Control, e.Alt));
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            keys.Enqueue(new Keys(e.KeyData.ToString(), true, e.Shift, e.Control, e.Alt));
            base.OnKeyUp(e);
        }
    }

    private Form f;
    private Timer t;
    private Custom c;

    [TearDown]
    protected override void TearDown()
    {
        f?.Dispose();
        t.Dispose();
        c.Dispose();
    }

    private void SendKeysTest1_tick(object? sender, EventArgs e)
    {
        if (f.InvokeRequired)
        {
            f.Invoke(new EventHandler(SendKeysTest1_tick), sender!, e);
            return;
        }
        t.Stop();
        Assert.That((object?)keys.Count, Is.EqualTo(2));
        var k = (Keys)keys.Dequeue()!;
        Assert.IsFalse(k.up);
        Assert.IsFalse(k.shift);
        Assert.IsFalse(k.ctrl);
        Assert.IsFalse(k.alt);
        Assert.That((object?)k.key, Is.EqualTo("A"));

        k = (Keys)keys.Dequeue()!;
        Assert.IsTrue(k.up);
        Assert.IsFalse(k.shift);
        Assert.IsFalse(k.ctrl);
        Assert.IsFalse(k.alt);
        Assert.That((object?)k.key, Is.EqualTo("A"));

        t.Dispose();
        f.Close();
    }

    [SetUp]
    protected override void SetUp()
    {
        keys.Clear();
        base.SetUp();
    }

    private void SendKeysTest2_tick(object? sender, EventArgs e)
    {
        t.Stop();
        if (f.InvokeRequired)
        {
            f.Invoke(new EventHandler(SendKeysTest2_tick), [sender!, e]!);
            return;
        }
        Assert.That((object?)keys.Count, Is.EqualTo(12));

        var k = (Keys)keys.Dequeue()!;
        Assert.IsFalse(k.up);
        Assert.IsTrue(k.shift);
        Assert.IsFalse(k.ctrl);
        Assert.IsFalse(k.alt);

        k = (Keys)keys.Dequeue()!;
        Assert.IsFalse(k.up);
        Assert.IsTrue(k.shift);
        Assert.IsFalse(k.ctrl);
        Assert.IsFalse(k.alt);
        Assert.That((object?)k.key, Is.EqualTo("A, Shift"));

        k = (Keys)keys.Dequeue()!;
        Assert.IsTrue(k.up);
        Assert.IsTrue(k.shift);
        Assert.IsFalse(k.ctrl);
        Assert.IsFalse(k.alt);
        Assert.That((object?)k.key, Is.EqualTo("A, Shift"));

        k = (Keys)keys.Dequeue()!;
        Assert.IsFalse(k.up);
        Assert.IsTrue(k.shift);
        Assert.IsFalse(k.ctrl);
        Assert.IsFalse(k.alt);
        Assert.That((object?)k.key, Is.EqualTo("B, Shift"));

        k = (Keys)keys.Dequeue()!;
        Assert.IsTrue(k.up);
        Assert.IsTrue(k.shift);
        Assert.IsFalse(k.ctrl);
        Assert.IsFalse(k.alt);
        Assert.That((object?)k.key, Is.EqualTo("B, Shift"));

        k = (Keys)keys.Dequeue()!;
        Assert.IsFalse(k.up);
        Assert.IsTrue(k.shift);
        Assert.IsFalse(k.ctrl);
        Assert.IsFalse(k.alt);
        Assert.That((object?)k.key, Is.EqualTo("C, Shift"));

        k = (Keys)keys.Dequeue()!;
        Assert.IsTrue(k.up);
        Assert.IsTrue(k.shift);
        Assert.IsFalse(k.ctrl);
        Assert.IsFalse(k.alt);
        Assert.That((object?)k.key, Is.EqualTo("C, Shift"));

        k = (Keys)keys.Dequeue()!;
        Assert.IsTrue(k.up);
        Assert.IsFalse(k.shift);
        Assert.IsFalse(k.ctrl);
        Assert.IsFalse(k.alt);
        Assert.That((object?)k.key, Is.EqualTo("ShiftKey"));

        k = (Keys)keys.Dequeue()!;
        Assert.IsFalse(k.up, "#b1");
        Assert.IsFalse(k.shift, "#b2");
        Assert.IsFalse(k.ctrl, "#b3");
        Assert.IsFalse(k.alt, "#b4");
        Assert.That((object?)k.key, Is.EqualTo("Back"), "#b5");

        k = (Keys)keys.Dequeue()!;
        Assert.IsTrue(k.up, "#b6");
        Assert.IsFalse(k.shift, "#b7");
        Assert.IsFalse(k.ctrl, "#b8");
        Assert.IsFalse(k.alt, "#b9");
        Assert.That((object?)k.key, Is.EqualTo("Back"), "#b10");

        k = (Keys)keys.Dequeue()!;
        Assert.IsFalse(k.up, "#c1");
        Assert.IsFalse(k.shift, "#c2");
        Assert.IsFalse(k.ctrl, "#c3");
        Assert.IsFalse(k.alt, "#c4");
        Assert.That((object?)k.key, Is.EqualTo("Back"), "#c5");

        k = (Keys)keys.Dequeue()!;
        Assert.IsTrue(k.up, "#c6");
        Assert.IsFalse(k.shift, "#c7");
        Assert.IsFalse(k.ctrl, "#c8");
        Assert.IsFalse(k.alt, "#c9");
        Assert.That((object?)k.key, Is.EqualTo("Back"), "#c10");

        Assert.That((object?)keys.Count, Is.EqualTo(0), "#d1");

        Assert.That((object?)c.Text, Is.EqualTo("A"), "#e1");

        t.Dispose();
        f.Close();
    }

}