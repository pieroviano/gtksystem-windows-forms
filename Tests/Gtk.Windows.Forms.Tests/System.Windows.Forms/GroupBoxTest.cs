//
// GroupBoxTest.cs: Test cases for GroupBox.
//
// Author:
//   Ritvik Mayank (mritvik@novell.com)
//
// (C) 2005 Novell, Inc. (http://www.novell.com)
//

using System.Windows.Forms;
using System.Drawing;
using GtkTests.Helpers;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class GroupBoxTest : TestHelper
{
    [Test]
    public void Constructor ()
    {
        var gb = new GroupBox ();

        Assert.That((object?)gb.AllowDrop, Is.EqualTo(false));
        // Top/Height are dependent on font height
        // Assert1.AreEqual(new Rectangle (3, 16, 194, 81), gb.DisplayRectangle);
        Assert.That((object?)gb.TabStop, Is.EqualTo(false));
        object expected = string.Empty;
        Assert.That((object?)gb.Text, Is.EqualTo(expected));
			
        Assert.That((object?)gb.AutoSize, Is.EqualTo(false));
        Assert.That((object?)gb.AccessibilityObject!.GetType ().ToString (), Is.EqualTo("System.Windows.Forms.GroupBox+GroupBoxAccessibleObject"));
    }
		
    [Test]
    public void AutoSize ()
    {
        var f = new Form ();
        f.ShowInTaskbar = false;

        var p = new GroupBox ();
        p.AutoSize = true;
        f.Controls.Add (p);

        var b = new Button ();
        b.Size = new Size (200, 200);
        b.Location = new Point (200, 200);
        p.Controls.Add (b);

        f.Show ();

        object expected = new Size (406, 419);
        Assert.That((object?)p.ClientSize, Is.EqualTo(expected));

        p.Controls.Remove (b);
        object expected1 = new Size (200, 100);
        Assert.That((object?)p.ClientSize, Is.EqualTo(expected1));

        f.Dispose ();
    }

    [Test]
    public void PropertyDisplayRectangle ()
    {
        var gb = new GroupBox ();
        gb.Size = new Size (200, 200);

        object expected = new Padding (3);
        Assert.That((object?)gb.Padding, Is.EqualTo(expected));
        gb.Padding = new Padding (25, 25, 25, 25);

        object expected1 = new Rectangle (0, 0, 200, 200);
        Assert.That((object?)gb.ClientRectangle, Is.EqualTo(expected1));

        // Basically, we are testing that the DisplayRectangle includes
        // Padding.  Top/Height are affected by font height, so we aren't
        // using exact numbers.
        Assert.That((object?)gb.DisplayRectangle.Left, Is.EqualTo(25));
        Assert.That((object?)gb.DisplayRectangle.Width, Is.EqualTo(150));
        Assert.IsTrue (gb.DisplayRectangle.Top > gb.Padding.Top);
        Assert.IsTrue (gb.DisplayRectangle.Height < (gb.Height - gb.Padding.Vertical));
    }
}