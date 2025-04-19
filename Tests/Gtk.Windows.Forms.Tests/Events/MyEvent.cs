//
// ListViewEventTest.cs: Test cases for ListView events.
//
// Author:
//   Ritvik Mayank (mritvik@novell.com)
//
// (C) 2005 Novell, Inc. (http://www.novell.com)
//

using System.Windows.Forms;
using GtkTests.Helpers;

namespace GtkTests.Events;

[TestFixture]
public class MyEvent : TestHelper
{
    private static bool eventhandled;
    public void New_EventHandler(object? sender, EventArgs e)
    {
        eventhandled = true;
    }

    [Test]
    public void ItemActivateTest()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        var mylistview = new ListView();
        mylistview.Activation = ItemActivation.OneClick;
        mylistview.LabelEdit = true;
        mylistview.ItemActivate += New_EventHandler;
        mylistview.View = View.Details;
        mylistview.SetBounds(10, 10, 200, 200, BoundsSpecified.All);
        mylistview.Columns.Add("A", 10, HorizontalAlignment.Center);
        mylistview.Columns.Add("B", 10, HorizontalAlignment.Center);
        var item1 = new ListViewItem("A", -1);
        mylistview.Items.Add(item1);
        myform.Controls.Add(mylistview);
        async Task OnLoad()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            mylistview.PerformItemActivate();
            await Task.Delay(TimeSpan.FromSeconds(1));
            myform.Dispose();
        }
        myform.Load += async (_, _) =>
        {
            await OnLoad();
        };
        myform.ShowDialog();
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }

    [Test]
    public void SelectedIndexChangedTest()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        var mylistview = new ListView();
        mylistview.LabelEdit = true;
        mylistview.SelectedIndexChanged += New_EventHandler;
        eventhandled = false;
        mylistview.View = View.Details;
        mylistview.SetBounds(10, 10, 200, 200, BoundsSpecified.All);
        mylistview.Columns.Add("A", -2, HorizontalAlignment.Center);
        mylistview.Columns.Add("B", -2, HorizontalAlignment.Center);
        var item1 = new ListViewItem("A", -1);
        mylistview.Items.Add(item1);
        myform.Controls.Add(mylistview);
        async Task OnLoad()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            mylistview.PerformSelectedIndexChanged();
            await Task.Delay(TimeSpan.FromSeconds(1));
            myform.Dispose();
        }
        myform.Load += async (_, _) =>
        {
            await OnLoad();
        };
        myform.ShowDialog();
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }
}