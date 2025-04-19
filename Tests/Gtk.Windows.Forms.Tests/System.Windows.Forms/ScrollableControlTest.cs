//
// ScrollableControlTest.cs: Test cases for ScrollableControl.
//
// Author:
//   Gert Driesen (drieseng@users.sourceforge.net)
//
// (C) 2006 Gert Driesen
//

using GtkTests.Helpers;
using System.Drawing;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class ScrollableControlTest : TestHelper
{
    [Test]
    public void AutoScrollPositionTest ()
    {
        var sc = new ScrollableControl ();
        sc.AutoScroll = true;
			
        sc.AutoScrollPosition = new Point (-25, -50);
        Assert.That((object?)sc.AutoScrollPosition, Is.EqualTo(Point.Empty));

        sc.AutoScrollPosition = new Point (2500, 5000);
        Assert.That((object?)sc.AutoScrollPosition, Is.EqualTo(Point.Empty));
			
        sc.AutoScrollPosition = new Point (25, 50);
        Assert.That((object?)sc.AutoScrollPosition, Is.EqualTo(Point.Empty));
			
        object o = sc.Handle;

        sc.AutoScrollPosition = new Point (-25, -50);
        Assert.That((object?)sc.AutoScrollPosition, Is.EqualTo(Point.Empty));

        sc.AutoScrollPosition = new Point (2500, 5000);
        Assert.That((object?)sc.AutoScrollPosition, Is.EqualTo(Point.Empty));

        sc.AutoScrollPosition = new Point (25, 50);
        Assert.That((object?)sc.AutoScrollPosition, Is.EqualTo(Point.Empty));
			
        sc.Size = new Size (200, 400);
        sc.Location = new Point (20, 40);

        sc.AutoScrollPosition = new Point (-25, -50);
        Assert.That((object?)sc.AutoScrollPosition, Is.EqualTo(Point.Empty));

        sc.AutoScrollPosition = new Point (2500, 5000);
        Assert.That((object?)sc.AutoScrollPosition, Is.EqualTo(Point.Empty));

        sc.AutoScrollPosition = new Point (25, 50);
        Assert.That((object?)sc.AutoScrollPosition, Is.EqualTo(Point.Empty));


        var c1 = new Control ();
        c1.Location = new Point (-100, -200);
        c1.Size = new Size (10, 10);
        sc.Controls.Add (c1);

        var c2 = new Control ();
        c2.Location = new Point (400, 800);
        c2.Size = new Size (10, 10);
        sc.Controls.Add (c2);

        object expected = new Rectangle (0, 0, 410, 810);
        Assert.That((object?)sc.DisplayRectangle, Is.EqualTo(expected));
			
        sc.AutoScrollPosition = new Point (-25, -50);
        Assert.That((object?)sc.AutoScrollPosition, Is.EqualTo(Point.Empty));
        object expected1 = new Rectangle (0, 0, 410, 810);
        Assert.That((object?)sc.DisplayRectangle, Is.EqualTo(expected1));
        object expected2 = new Point (-100, -200);
        Assert.That((object?)c1.Location, Is.EqualTo(expected2));
        object expected3 = new Point (400, 800);
        Assert.That((object?)c2.Location, Is.EqualTo(expected3));

        sc.AutoScrollPosition = new Point (2500, 5000);
        object expected4 = new Point (-226, -426);
        Assert.That((object?)sc.AutoScrollPosition, Is.EqualTo(expected4));
        object expected5 = new Rectangle (-226, -426, 410, 810);
        Assert.That((object?)sc.DisplayRectangle, Is.EqualTo(expected5));
        object expected6 = new Point (-326, -626);
        Assert.That((object?)c1.Location, Is.EqualTo(expected6));
        object expected7 = new Point (174, 374);
        Assert.That((object?)c2.Location, Is.EqualTo(expected7));

        sc.AutoScrollPosition = new Point (25, 50);
        object expected8 = new Point (-25, -50);
        Assert.That((object?)sc.AutoScrollPosition, Is.EqualTo(expected8));
        object expected9 = new Rectangle (-25, -50, 410, 810);
        Assert.That((object?)sc.DisplayRectangle, Is.EqualTo(expected9));
        object expected10 = new Point (-125, -250);
        Assert.That((object?)c1.Location, Is.EqualTo(expected10));
        object expected11 = new Point (375, 750);
        Assert.That((object?)c2.Location, Is.EqualTo(expected11));
			
        sc.AutoScrollPosition = new Point (2500, 5000);
        object expected12 = new Point (-226, -426);
        Assert.That((object?)sc.AutoScrollPosition, Is.EqualTo(expected12));
        object expected13 = new Rectangle (-226, -426, 410, 810);
        Assert.That((object?)sc.DisplayRectangle, Is.EqualTo(expected13));
        object expected14 = new Point (-326, -626);
        Assert.That((object?)c1.Location, Is.EqualTo(expected14));
        object expected15 = new Point (174, 374);
        Assert.That((object?)c2.Location, Is.EqualTo(expected15));

        sc.AutoScrollPosition = new Point (25, 50);
        object expected16 = new Point (-25, -50);
        Assert.That((object?)sc.AutoScrollPosition, Is.EqualTo(expected16));
        object expected17 = new Rectangle (-25, -50, 410, 810);
        Assert.That((object?)sc.DisplayRectangle, Is.EqualTo(expected17));
        object expected18 = new Point (-125, -250);
        Assert.That((object?)c1.Location, Is.EqualTo(expected18));
        object expected19 = new Point (375, 750);
        Assert.That((object?)c2.Location, Is.EqualTo(expected19));
			
        sc.AutoScrollPosition = new Point (-25, -50);
        Assert.That((object?)sc.AutoScrollPosition, Is.EqualTo(Point.Empty));
        object expected20 = new Rectangle (0, 0, 410, 810);
        Assert.That((object?)sc.DisplayRectangle, Is.EqualTo(expected20));
        object expected21 = new Point (-100, -200);
        Assert.That((object?)c1.Location, Is.EqualTo(expected21));
        object expected22 = new Point (400, 800);
        Assert.That((object?)c2.Location, Is.EqualTo(expected22));

        sc.AutoScrollPosition = new Point (2500, 5000);
        object expected23 = new Point (-226, -426);
        Assert.That((object?)sc.AutoScrollPosition, Is.EqualTo(expected23));
        object expected24 = new Rectangle (-226, -426, 410, 810);
        Assert.That((object?)sc.DisplayRectangle, Is.EqualTo(expected24));
        object expected25 = new Point (-326, -626);
        Assert.That((object?)c1.Location, Is.EqualTo(expected25));
        object expected26 = new Point (174, 374);
        Assert.That((object?)c2.Location, Is.EqualTo(expected26));

        sc.AutoScrollPosition = new Point (25, 50);
        object expected27 = new Point (-25, -50);
        Assert.That((object?)sc.AutoScrollPosition, Is.EqualTo(expected27));
        object expected28 = new Rectangle (-25, -50, 410, 810);
        Assert.That((object?)sc.DisplayRectangle, Is.EqualTo(expected28));
        object expected29 = new Point (-125, -250);
        Assert.That((object?)c1.Location, Is.EqualTo(expected29));
        object expected30 = new Point (375, 750);
        Assert.That((object?)c2.Location, Is.EqualTo(expected30));
			
			
    }
		
    [Test]
    public void ResizeAnchoredTest ()
    {
        var sc = new ScrollableControl ();
        object h = sc.Handle;
        sc.Size = new Size (23, 45);
        var lbl = new Label ();
        lbl.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lbl.Size = sc.ClientSize;
        sc.Controls.Add (lbl);
        sc.Height *= 2;
        sc.Height *= 2;
        Assert.That((object?)Point.Empty, Is.EqualTo(lbl.Location));
        Assert.That((object?)sc.ClientSize, Is.EqualTo(lbl.Size));
			
        RemoveWarning (h);
    }
    [Test]
    public void AutoScroll ()
    {
        var sc = new ScrollableControl ();
        Assert.IsFalse (sc.AutoScroll);
        Assert.That((object?)sc.Controls.Count, Is.EqualTo(0));

        sc.AutoScroll = true;
        Assert.IsTrue(sc.AutoScroll);
        Assert.That((object?)sc.Controls.Count, Is.EqualTo(0));

        sc.AutoScroll = false;
        Assert.IsFalse (sc.AutoScroll);
        Assert.That((object?)sc.Controls.Count, Is.EqualTo(0));
    }

    [Test]
    public void AutoScrollMinSize ()
    {
        var sc = new ScrollableControl ();
        Assert.That((object?)sc.AutoScrollMinSize, Is.EqualTo(Size.Empty));
        Assert.IsFalse (sc.AutoScroll);

        sc.AutoScrollMinSize = Size.Empty;
        Assert.That((object?)sc.AutoScrollMinSize, Is.EqualTo(Size.Empty));
        Assert.IsFalse (sc.AutoScroll);

        sc.AutoScrollMinSize = new Size (10, 20);
        object expected = new Size (10, 20);
        Assert.That((object?)sc.AutoScrollMinSize, Is.EqualTo(expected));
        Assert.IsTrue (sc.AutoScroll);

        sc.AutoScroll = false;
        object expected1 = new Size (10, 20);
        Assert.That((object?)sc.AutoScrollMinSize, Is.EqualTo(expected1));
        Assert.IsFalse (sc.AutoScroll);

        sc.AutoScrollMinSize = new Size (10, 20);
        object expected2 = new Size (10, 20);
        Assert.That((object?)sc.AutoScrollMinSize, Is.EqualTo(expected2));
        Assert.IsFalse (sc.AutoScroll);

        sc.AutoScrollMinSize = new Size (20, 20);
        object expected3 = new Size (20, 20);
        Assert.That((object?)sc.AutoScrollMinSize, Is.EqualTo(expected3));
        Assert.IsTrue (sc.AutoScroll);

        sc.AutoScroll = false;
        object expected4 = new Size (20, 20);
        Assert.That((object?)sc.AutoScrollMinSize, Is.EqualTo(expected4));
        Assert.IsFalse (sc.AutoScroll);

        sc.AutoScrollMinSize = Size.Empty;
        Assert.That((object?)sc.AutoScrollMinSize, Is.EqualTo(Size.Empty));
        Assert.IsTrue (sc.AutoScroll);

        sc.AutoScrollMinSize = new Size (10, 20);
        object expected5 = new Size (10, 20);
        Assert.That((object?)sc.AutoScrollMinSize, Is.EqualTo(expected5));
        Assert.IsTrue (sc.AutoScroll);

        sc.AutoScrollMinSize = Size.Empty;
        Assert.That((object?)sc.AutoScrollMinSize, Is.EqualTo(Size.Empty));
        Assert.IsTrue (sc.AutoScroll);
    }

    [Test]
    public void Padding ()
    {
        var c = new ScrollableControl ();
        c.Dock = DockStyle.Fill;
        c.Padding = new Padding (40);

        Assert.That((object?)c.Padding.All, Is.EqualTo(40));
        c.Padding = new Padding (40, 40, 40, 40);

        Assert.That((object?)c.Padding.Right, Is.EqualTo(40));
        var f = new Form ();
        f.Controls.Add (c);
			
        var b = new Button ();
        c.Controls.Add (b);
			
        f.Show ();
			
        // Padding does not affect laying out the controls
        object expected = new Point (0, 0);
        Assert.That((object?)b.Location, Is.EqualTo(expected));
			
        f.Close ();
        f.Dispose ();
    }

    // Even if padding is not directly affecting the layout, it can
    // cause the ScrollableControl instance to show the scrollbars
    // *after* the first access to DockPadding
    // Tests Xamarin-2562
    [Test]
    public void DisplayRectangle_SamePadding ()
    {
        using var c = new ScrollableControl ();
        c.Size = new Size (100, 100);
        c.Padding = new Padding (4);
        object expected = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)c.ClientRectangle, Is.EqualTo(expected));
        object expected1 = new Rectangle (4, 4, 92, 92);
        Assert.That((object?)c.DisplayRectangle, Is.EqualTo(expected1));
    }

    [Test]
    public void DisplayRectangle_DifferentPadding ()
    {
        using var c = new ScrollableControl ();
        c.Size = new Size (100, 100);
        c.Padding = new Padding (1, 2, 3, 4);
        object expected = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)c.ClientRectangle, Is.EqualTo(expected));
        object expected1 = new Rectangle (1, 2, 96, 94);
        Assert.That((object?)c.DisplayRectangle, Is.EqualTo(expected1));
    }

    [Test]
    public void DisplayRectangeTest ()
    {
        using var sc = new ScrollableControl ();
        sc.Size = new Size (100, 100);
        sc.AutoScroll = true;

        var c = new Control ();
        c.Location = new Point (0, 0);
        c.Size = new Size (200, 200);
        sc.Controls.Add (c);
        object expected = new Rectangle (0, 0, 200, 200);
        Assert.That((object?)sc.DisplayRectangle, Is.EqualTo(expected));

        c.Visible = false;
        object expected1 = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)sc.DisplayRectangle, Is.EqualTo(expected1));
    }
}