//
// RadioRadioButtonTest.cs: Test cases for RadioRadioButton.
//
// Author:
//   Ritvik Mayank (mritvik@novell.com)
//
// (C) 2005 Novell, Inc. (http://www.novell.com)
//

using GtkTests.Helpers;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class RadioButtonTest : TestHelper
{
    [Test]
    public void RadioButtonPropertyTest ()
    {
        var rButton1 = new RadioButton ();
			
        // S
        Assert.That((object?)rButton1.Site, Is.EqualTo(null));	

        // T
        rButton1.Text = "New RadioButton";
        Assert.That((object?)rButton1.Text, Is.EqualTo("New RadioButton"));
        Assert.IsFalse (rButton1.TabStop);
    }

    [Test]
    public void CheckedTest ()
    {
        var rb = new RadioButton ();

        Assert.That((object?)rb.TabStop, Is.EqualTo(false));
        Assert.That((object?)rb.Checked, Is.EqualTo(false));

        rb.Checked = true;

        Assert.That((object?)rb.TabStop, Is.EqualTo(true));
        Assert.That((object?)rb.Checked, Is.EqualTo(true));

        rb.Checked = false;

        Assert.That((object?)rb.TabStop, Is.EqualTo(false));
        Assert.That((object?)rb.Checked, Is.EqualTo(false));

        // RadioButton is NOT checked, but since it is the only
        // RadioButton instance in Form, when it gets selected (Form.Show)
        // it should acquire the focus
        var f = new Form ();
        f.Controls.Add (rb);
        rb.CheckedChanged += rb_checked_changed;
        event_received = false;

        f.ActiveControl = rb;

        Assert.That((object?)event_received, Is.EqualTo(true));
        Assert.That((object?)rb.Checked, Is.EqualTo(true));
        Assert.That((object?)rb.TabStop, Is.EqualTo(true));

        f.Dispose ();
    }

    private bool event_received;

    private void rb_tabstop_changed (object? sender, EventArgs e)
    {
        event_received = true;
    }

    private void rb_checked_changed (object? sender, EventArgs e)
    {
        event_received = true;
    }

    [Test]
    public void TabStopEventTest ()
    {
        var rb = new RadioButton ();

        rb.TabStopChanged += rb_tabstop_changed;
        event_received = false;

        rb.TabStop = true;

        Assert.IsTrue (event_received);
    }

    [Test]
    public void ToStringTest ()
    {
        var rButton1 = new RadioButton ();
        Assert.That((object?)rButton1.ToString (), Is.EqualTo("System.Windows.Forms.RadioButton, Checked: False"));
    }

    [Test]
    public void AutoSizeText ()
    {
        var f = new Form ();
        f.ShowInTaskbar = false;
			
        var rb = new RadioButton ();
        rb.AutoSize = true;
        rb.Width = 14;
        f.Controls.Add (rb);
			
        var width = rb.Width;
			
        rb.Text = "Some text that is surely longer than 100 pixels.";

        Assert.False(rb.Width == width);
    }
}
	
[TestFixture]
public class RadioButtonEventTestClass : TestHelper
{
    private static bool eventhandled;
    public static void RadioButton_EventHandler (object? sender, EventArgs e)
    {
        eventhandled = true;
    }

    [Test]
    public void ApperanceChangedTest ()
    {
        var myForm = new Form ();
        myForm.ShowInTaskbar = false;
        var rButton1 = new RadioButton ();
        rButton1.Select ();
        rButton1.Visible = true;
        myForm.Controls.Add (rButton1);
        eventhandled = false;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
        myForm.Dispose ();
    }
	
    [Test]
    public void CheckedChangedTest ()
    {
        var myForm = new Form ();
        myForm.ShowInTaskbar = false;
        var rButton1 = new RadioButton ();
        rButton1.Select ();
        rButton1.Visible = true;
        myForm.Controls.Add (rButton1);
        rButton1.Checked = false;
        eventhandled = false;
        rButton1.CheckedChanged += RadioButton_EventHandler;
        rButton1.Checked = true;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
        myForm.Dispose ();
    }
}