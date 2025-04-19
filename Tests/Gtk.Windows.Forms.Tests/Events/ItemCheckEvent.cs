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
public class ItemCheckEvent : TestHelper
{
    private static bool eventhandled;
    public void ItemCheckEventHandler(object? sender, ItemCheckEventArgs e)
    {
        eventhandled = true;
    }

    [Test]
    public void ItemCheckTest()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        var mylistview = new ListView();
        mylistview.CheckBoxes = true;
        mylistview.LabelEdit = true;
        mylistview.ItemCheck += ItemCheckEventHandler;
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
            mylistview.Columns[0].PerformItemCheck(0, CheckState.Unchecked, CheckState.Checked);
            await Task.Delay(TimeSpan.FromSeconds(1));
            myform.Dispose();
        }
        myform.Load += async (_, _) =>
        {
            await OnLoad();
        };
        myform.ShowDialog();
        mylistview.Visible = true;
        Assert.That((object?)eventhandled, Is.EqualTo(true));
        myform.Dispose();
    }
}