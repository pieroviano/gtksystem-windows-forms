//
// Copyright (c) 2005 Novell, Inc.
//
// Authors:
//      Ritvik Mayank (mritvik@novell.com)
//

using GtkTests.Helpers;
using System.Windows.Forms;

namespace GtkTests.Controls;

[TestFixture]
public class CheckBoxEventTest : TestHelper
{
    private bool eventHandled;

    private bool EventHandled
    {
        get
        {
            var handled = eventHandled;
            eventHandled = false;
            return handled;
        }
        set => eventHandled = value;
    }

    public void CheckBox_EventHandler(object? sender, EventArgs e)
    {
        EventHandled = true;
    }

    [Test]
    public void CheckedChangedEventTest()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        EventHandled = false;
        myform.Visible = true;
        var chkbox = new CheckBox();
        myform.Controls.Add(chkbox);
        chkbox.CheckedChanged += CheckBox_EventHandler;
        chkbox.Visible = true;
        chkbox.CheckState = CheckState.Checked;
        Assert.That((object?)EventHandled, Is.EqualTo(true));
        myform.Dispose();
    }

    [Test]
    public void CheckStateChangedEventTest()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        EventHandled = false;
        myform.Visible = true;
        var chkbox = new CheckBox();
        chkbox.Visible = true;
        myform.Controls.Add(chkbox);
        chkbox.CheckStateChanged += CheckBox_EventHandler;
        chkbox.CheckState = CheckState.Checked;
        Assert.That((object?)EventHandled, Is.EqualTo(true));
        myform.Dispose();
    }
}