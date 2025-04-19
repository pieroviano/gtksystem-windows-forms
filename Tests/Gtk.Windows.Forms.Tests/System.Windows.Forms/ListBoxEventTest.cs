//
// Copyright  (c) 2005 Novell, Inc.
//
// Authors:
//      Ritvik Mayank (mritvik@novell.com)
//

using GtkTests.Helpers;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
[Ignore ("This test has to be completly reviewed")]
public class ListBoxDrawItemEvent : TestHelper
{
    private static bool eventhandled;
    public void DrawItem_EventHandler (object sender,DrawItemEventArgs e)
    {
        eventhandled = true;
    }

    [Test]
    public void DrawItemTest ()
    {
        var myform = new Form ();
        myform.ShowInTaskbar = false;
        var lb1 = new ListBox ();
        lb1.Items.Add ("A");
        // Test DrawItem Event
        lb1.DrawMode = DrawMode.OwnerDrawFixed;
        myform.Controls.Add (lb1);
        myform.Show ();
        Assert.That((object?)eventhandled, Is.EqualTo(true));
        myform.Dispose();
    }

    [TestFixture]
    [Ignore ("This test has to be completly reviewed")]
    public class ListBoxMeasureItemEvent : TestHelper
    {
        private static bool eventhandled;
        public void MeasureItem_EventHandler (object sender,MeasureItemEventArgs e)
        {
            eventhandled = true;
        }		

        [Test]
        public void MeasureItemTest ()
        {
            var myform = new Form ();
            myform.ShowInTaskbar = false;
            myform.Visible = true;
            var lb1 = new ListBox ();
            lb1.Items.Add ("B");
            lb1.Visible = true;
            myform.Controls.Add (lb1);
            // Test MeasureItem Event
            lb1.DrawMode = DrawMode.OwnerDrawVariable;
            Assert.That((object?)eventhandled, Is.EqualTo(true));
            myform.Dispose();
        }
    }
}