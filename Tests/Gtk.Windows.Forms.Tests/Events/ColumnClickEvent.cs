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
public class ColumnClickEvent : TestHelper
{
    private static bool eventhandled;
    public void ColumnClickEventHandler(object? sender, ColumnClickEventArgs e)
    {
        eventhandled = true;
    }

    [Test]
    public void ColumnClickTest()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        var mylistview = new ListView();

        mylistview.LabelEdit = true;
        mylistview.ColumnClick += ColumnClickEventHandler;
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
            mylistview.Columns[0].PerformClick();
            await Task.Delay(TimeSpan.FromSeconds(1));
            myform.Dispose();
        }
        myform.Load += async (_, _) =>
        {
            await OnLoad();
        };
        myform.ShowDialog();
        mylistview.Sort();
        Assert.That((object?)eventhandled, Is.EqualTo(true));
    }
}