using System.Windows.Forms;
using System.Drawing;
using GtkTests.Helpers;

namespace GtkTests.System.Windows.Forms;

[TestFixture]	
public class FlowPanelTests : TestHelper
{
    [Test]
    public void TestConstruction()
    {
        var p = new FlowLayoutPanel();
			
        Assert.That((object?)p.FlowDirection, Is.EqualTo(FlowDirection.LeftToRight));
        Assert.That((object?)p.WrapContents, Is.EqualTo(true));
        Assert.That((object?)p.LayoutEngine?.ToString(), Is.EqualTo("System.Windows.Forms.Layout.FlowLayout"));
			
        p.FlowDirection = FlowDirection.BottomUp;
        p.WrapContents = false;

        Assert.That((object?)p.FlowDirection, Is.EqualTo(FlowDirection.BottomUp));
        Assert.That((object?)p.WrapContents, Is.EqualTo(false));
    }
		
    [Test]
    public void TestExtenderProvider()
    {
        var p = new FlowLayoutPanel ();
        var b = new Button();
			
        Assert.That((object?)p.GetFlowBreak(b), Is.EqualTo(false));
			
        p.SetFlowBreak(b, true);

        Assert.That((object?)p.GetFlowBreak (b), Is.EqualTo(true));
    }

    #region LeftToRight Tests
    [Test]
    public void LeftToRightLayoutTest1 ()
    {
        // 2 Normal Buttons
        var p = new FlowLayoutPanel ();

        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle (100, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1));
    }

    [Test]
    public void LeftToRightLayoutTest2 ()
    {
        // Dock Fill and Normal
        var p = new FlowLayoutPanel ();

        p.Controls.Add (CreateButton (100, 50, false, DockStyle.Fill, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle (100, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1));
    }

    [Test]
    public void LeftToRightLayoutTest3 ()
    {
        // Anchored: Top/Bottom and Normal
        var p = new FlowLayoutPanel ();

        p.Controls.Add (CreateButton (100, 50, false, DockStyle.None, new Padding (), AnchorStyles.Top | AnchorStyles.Bottom));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle (100, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1));
    }

    [Test]
    public void LeftToRightLayoutTest4 ()
    {
        // Anchored: Top/Bottom and Dock Fill
        var p = new FlowLayoutPanel ();

        p.Controls.Add (CreateButton (100, 50, false, DockStyle.None, new Padding (), AnchorStyles.Top | AnchorStyles.Bottom));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.Fill, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 0, 100, 0);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle (100, 0, 100, 0);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1));
    }

    [Test]
    public void LeftToRightLayoutTest5 ()
    {
        // 2 Anchored: Top/Bottom
        var p = new FlowLayoutPanel ();

        p.Controls.Add (CreateButton (100, 50, false, DockStyle.None, new Padding (), AnchorStyles.Top | AnchorStyles.Bottom));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Top | AnchorStyles.Bottom));

        object expected = new Rectangle (0, 0, 100, 0);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle (100, 0, 100, 0);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1));
    }

    [Test]
    public void LeftToRightLayoutTest6 ()
    {
        // 2 Dock Fill
        var p = new FlowLayoutPanel ();

        p.Controls.Add (CreateButton (100, 50, false, DockStyle.Fill, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.Fill, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 0, 100, 0);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle (100, 0, 100, 0);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1));
    }

    [Test]
    public void LeftToRightLayoutTest7 ()
    {
        // Dock Top
        var p = new FlowLayoutPanel ();

        p.Controls.Add (CreateButton (100, 50, false, DockStyle.Top, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 0, 100, 50);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle (100, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1));
    }

    [Test]
    public void LeftToRightLayoutTest8 ()
    {
        // Dock Bottom
        var p = new FlowLayoutPanel ();

        p.Controls.Add (CreateButton (100, 50, false, DockStyle.Bottom, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 50, 100, 50);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle (100, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1));
    }

    [Test]
    public void LeftToRightLayoutTest9 ()
    {
        // Anchor Bottom
        var p = new FlowLayoutPanel ();

        p.Controls.Add (CreateButton (100, 50, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Bottom));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 50, 100, 50);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle (100, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1));
    }

    [Test]
    public void LeftToRightLayoutTest10 ()
    {
        // No Dock or Anchor
        var p = new FlowLayoutPanel ();

        p.Controls.Add (CreateButton (100, 50, false, DockStyle.None, new Padding (), AnchorStyles.None));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 25, 100, 50);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle (100, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1));
    }

    [Test]
    public void LeftToRightLayoutTest11 ()
    {
        // WrapContents = true
        var p = new FlowLayoutPanel ();

        p.Controls.Add (CreateButton (100, 50, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 0, 100, 50);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle (100, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1));
        object expected2 = new Rectangle (0, 100, 100, 100);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2));
        object expected3 = new Rectangle (100, 100, 100, 100);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3));
    }

    [Test]
    public void LeftToRightLayoutTest12 ()
    {
        // WrapContents = false
        var p = new FlowLayoutPanel ();
        p.WrapContents = false;
			
        p.Controls.Add (CreateButton (100, 50, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 0, 100, 50);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle (100, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1));
        object expected2 = new Rectangle (200, 0, 100, 100);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2));
        object expected3 = new Rectangle (300, 0, 100, 100);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3));
    }

    [Test]
    public void LeftToRightLayoutTest13 ()
    {
        // SetFlowBreak 1, 3
        var p = new FlowLayoutPanel ();

        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Size (400, 100);
        Assert.That((object?)p.PreferredSize, Is.EqualTo(expected));

        p.SetFlowBreak (p.Controls[0], true);
        p.SetFlowBreak (p.Controls[2], true);

        object expected1 = new Size (200, 300);
        Assert.That((object?)p.PreferredSize, Is.EqualTo(expected1));
        object expected2 = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected2));
        object expected3 = new Rectangle (0, 100, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected3));
        object expected4 = new Rectangle (100, 100, 100, 100);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected4));
        object expected5 = new Rectangle (0, 200, 100, 100);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected5));
    }

    [Test]
    public void LeftToRightLayoutTest14 ()
    {
        // Margins
        var p = new FlowLayoutPanel ();

        p.Controls.Add (CreateButton (50, 50, false, DockStyle.None, new Padding (1,3,5,2), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (50, 50, false, DockStyle.None, new Padding (7,3,12,5), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (50, 50, false, DockStyle.None, new Padding (14,7,1,3), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (50, 50, false, DockStyle.None, new Padding (4), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Size (248, 60);
        Assert.That((object?)p.PreferredSize, Is.EqualTo(expected));
        object expected1 = new Rectangle (1, 3, 50, 50);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected1));
        object expected2 = new Rectangle (63, 3, 50, 50);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected2));
        object expected3 = new Rectangle (139, 7, 50, 50);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected3));
        object expected4 = new Rectangle (4, 64, 50, 50);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected4));
    }

    [Test]
    public void LeftToRightLayoutTest15 ()
    {
        // Margins and Different Sizes
        var p = new FlowLayoutPanel ();

        p.Controls.Add (CreateButton (25, 45, false, DockStyle.None, new Padding (6), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (60, 20, false, DockStyle.None, new Padding (9), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (15, 85, false, DockStyle.None, new Padding (2), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (50, 20, false, DockStyle.None, new Padding (4), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Size (192, 89);
        Assert.That((object?)p.PreferredSize, Is.EqualTo(expected));
        object expected1 = new Rectangle (6, 6, 25, 45);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected1));
        object expected2 = new Rectangle (46, 9, 60, 20);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected2));
        object expected3 = new Rectangle (117, 2, 15, 85);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected3));
        object expected4 = new Rectangle (138, 4, 50, 20);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected4));
    }

    [Test]
    public void LeftToRightLayoutTest16 ()
    {
        // Random Complex Layout 1
        var p = new FlowLayoutPanel ();

        p.Controls.Add (CreateButton (25, 45, false, DockStyle.None, new Padding (6), AnchorStyles.Right | AnchorStyles.Top));
        p.Controls.Add (CreateButton (60, 20, false, DockStyle.Fill, new Padding (9), AnchorStyles.Bottom | AnchorStyles.Top));
        p.Controls.Add (CreateButton (15, 85, false, DockStyle.None, new Padding (2), AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom));
        p.Controls.Add (CreateButton (50, 20, false, DockStyle.None, new Padding (4), AnchorStyles.Left | AnchorStyles.Bottom));
        p.Controls.Add (CreateButton (13, 22, false, DockStyle.None, new Padding (12), AnchorStyles.Left | AnchorStyles.Right));
        p.Controls.Add (CreateButton (73, 28, false, DockStyle.Top, new Padding (6), AnchorStyles.None));

        object expected = new Size (314, 57);
        Assert.That((object?)p.PreferredSize, Is.EqualTo(expected));
        object expected1 = new Rectangle (6, 6, 25, 45);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected1));
        object expected2 = new Rectangle (46, 9, 60, 39);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected2));
        object expected3 = new Rectangle (117, 2, 15, 53);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected3));
        object expected4 = new Rectangle (138, 33, 50, 20);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected4));
        object expected5 = new Rectangle (12, 69, 13, 22);
        Assert.That((object?)p.Controls[4].Bounds, Is.EqualTo(expected5));
        object expected6 = new Rectangle (43, 63, 73, 28);
        Assert.That((object?)p.Controls[5].Bounds, Is.EqualTo(expected6));
    }

    [Test]
    public void LeftToRightLayoutTest17 ()
    {
        // Random Complex Layout 2
        var p = new FlowLayoutPanel ();

        p.Controls.Add (CreateButton (12, 345, false, DockStyle.Bottom, new Padding (1, 2, 3, 4), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (9, 44, false, DockStyle.Top, new Padding (6, 3, 2, 7), AnchorStyles.Right | AnchorStyles.Top));
        p.Controls.Add (CreateButton (78, 14, false, DockStyle.None, new Padding (5, 1, 2, 4), AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right));
        p.Controls.Add (CreateButton (21, 64, false, DockStyle.Top, new Padding (3, 3, 3, 1), AnchorStyles.None));
        p.Controls.Add (CreateButton (14, 14, false, DockStyle.Fill, new Padding (11, 4, 6, 3), AnchorStyles.Top | AnchorStyles.Bottom));
        p.Controls.Add (CreateButton (132, 6, false, DockStyle.Fill, new Padding (5, 5, 4, 5), AnchorStyles.Top | AnchorStyles.Bottom));

        p.SetFlowBreak (p.Controls[0], true);
        p.SetFlowBreak (p.Controls[2], true);

        object expected = new Rectangle (1, 2, 12, 345);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle (6, 354, 9, 44);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1));
        object expected2 = new Rectangle (22, 352, 78, 49);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2));
        object expected3 = new Rectangle (3, 408, 21, 64);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3));
        object expected4 = new Rectangle (38, 409, 14, 61);
        Assert.That((object?)p.Controls[4].Bounds, Is.EqualTo(expected4));
        object expected5 = new Rectangle (63, 410, 132, 58);
        Assert.That((object?)p.Controls[5].Bounds, Is.EqualTo(expected5));
    }
		
    [Test]
    public void LeftToRightLayoutTest18 ()
    {
        // SetFlowBreak has no effect when WrapContents = false
        var p = new FlowLayoutPanel ();
        p.WrapContents = false;

        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        p.SetFlowBreak(p.Controls[0], true);

        object expected = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle (100, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1));
    }
    #endregion

    #region RightToLeft Tests
    [Test]
    public void RightToLeftLayoutTest1 ()
    {
        // 2 Normal Buttons
        var p = new FlowLayoutPanel ();
        p.FlowDirection = FlowDirection.RightToLeft;
			
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (100, 0, 100, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "AC1");
        object expected1 = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "AC2");
    }

    [Test]
    public void RightToLeftLayoutTest2 ()
    {
        // Dock Fill and Normal
        var p = new FlowLayoutPanel ();
        p.FlowDirection = FlowDirection.RightToLeft;

        p.Controls.Add (CreateButton (100, 50, false, DockStyle.Fill, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (100, 0, 100, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "AD1");
        object expected1 = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "AD2");
    }

    [Test]
    public void RightToLeftLayoutTest3 ()
    {
        // Anchored: Top/Bottom and Normal
        var p = new FlowLayoutPanel ();
        p.FlowDirection = FlowDirection.RightToLeft;

        p.Controls.Add (CreateButton (100, 50, false, DockStyle.None, new Padding (), AnchorStyles.Top | AnchorStyles.Bottom));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (100, 0, 100, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "AE1");
        object expected1 = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "AE2");
    }

    [Test]
    public void RightToLeftLayoutTest4 ()
    {
        // Anchored: Top/Bottom and Dock Fill
        var p = new FlowLayoutPanel ();
        p.FlowDirection = FlowDirection.RightToLeft;

        p.Controls.Add (CreateButton (100, 50, false, DockStyle.None, new Padding (), AnchorStyles.Top | AnchorStyles.Bottom));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.Fill, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (100, 0, 100, 0);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "AF1");
        object expected1 = new Rectangle (0, 0, 100, 0);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "AF2");
    }

    [Test]
    public void RightToLeftLayoutTest5 ()
    {
        // 2 Anchored: Top/Bottom
        var p = new FlowLayoutPanel ();
        p.FlowDirection = FlowDirection.RightToLeft;

        p.Controls.Add (CreateButton (100, 50, false, DockStyle.None, new Padding (), AnchorStyles.Top | AnchorStyles.Bottom));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Top | AnchorStyles.Bottom));

        object expected = new Rectangle (100, 0, 100, 0);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "AG1");
        object expected1 = new Rectangle (0, 0, 100, 0);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "AG2");
    }

    [Test]
    public void RightToLeftLayoutTest6 ()
    {
        // 2 Dock Fill
        var p = new FlowLayoutPanel ();
        p.FlowDirection = FlowDirection.RightToLeft;

        p.Controls.Add (CreateButton (100, 50, false, DockStyle.Fill, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.Fill, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (100, 0, 100, 0);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "AH1");
        object expected1 = new Rectangle (0, 0, 100, 0);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "AH2");
    }

    [Test]
    public void RightToLeftLayoutTest7 ()
    {
        // Dock Top
        var p = new FlowLayoutPanel ();
        p.FlowDirection = FlowDirection.RightToLeft;

        p.Controls.Add (CreateButton (100, 50, false, DockStyle.Top, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (100, 0, 100, 50);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "AI1");
        object expected1 = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "AI2");
    }

    [Test]
    public void RightToLeftLayoutTest8 ()
    {
        // Dock Bottom
        var p = new FlowLayoutPanel ();
        p.FlowDirection = FlowDirection.RightToLeft;

        p.Controls.Add (CreateButton (100, 50, false, DockStyle.Bottom, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (100, 50, 100, 50);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "AJ1");
        object expected1 = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "AJ2");
    }

    [Test]
    public void RightToLeftLayoutTest9 ()
    {
        // Anchor Bottom
        var p = new FlowLayoutPanel ();
        p.FlowDirection = FlowDirection.RightToLeft;

        p.Controls.Add (CreateButton (100, 50, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Bottom));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (100, 50, 100, 50);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "AK1");
        object expected1 = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "AK2");
    }

    [Test]
    public void RightToLeftLayoutTest10 ()
    {
        // No Dock or Anchor
        var p = new FlowLayoutPanel ();
        p.FlowDirection = FlowDirection.RightToLeft;

        p.Controls.Add (CreateButton (100, 50, false, DockStyle.None, new Padding (), AnchorStyles.None));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (100, 25, 100, 50);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "AL1");
        object expected1 = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "AL2");
    }

    [Test]
    public void RightToLeftLayoutTest11 ()
    {
        // WrapContents = true
        var p = new FlowLayoutPanel ();
        p.FlowDirection = FlowDirection.RightToLeft;

        p.Controls.Add (CreateButton (100, 50, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (100, 0, 100, 50);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "AM1");
        object expected1 = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "AM2");
        object expected2 = new Rectangle (100, 100, 100, 100);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2), "AM3");
        object expected3 = new Rectangle (0, 100, 100, 100);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3), "AM4");
    }

    [Test]
    public void RightToLeftLayoutTest12 ()
    {
        // WrapContents = false
        var p = new FlowLayoutPanel ();
        p.WrapContents = false;
        p.FlowDirection = FlowDirection.RightToLeft;

        p.Controls.Add (CreateButton (100, 50, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (100, 0, 100, 50);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "AN1");
        object expected1 = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "AN2");
        object expected2 = new Rectangle (-100, 0, 100, 100);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2), "AN3");
        object expected3 = new Rectangle (-200, 0, 100, 100);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3), "AN4");
    }

    [Test]
    public void RightToLeftLayoutTest13 ()
    {
        // SetFlowBreak 1, 3
        var p = new FlowLayoutPanel ();
        p.FlowDirection = FlowDirection.RightToLeft;

        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        p.SetFlowBreak (p.Controls[0], true);
        p.SetFlowBreak (p.Controls[2], true);

        object expected = new Rectangle (100, 0, 100, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "AO1");
        object expected1 = new Rectangle (100, 100, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "AO2");
        object expected2 = new Rectangle (0, 100, 100, 100);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2), "AO3");
        object expected3 = new Rectangle (100, 200, 100, 100);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3), "AO4");
    }

    [Test]
    public void RightToLeftLayoutTest14 ()
    {
        // Margins
        var p = new FlowLayoutPanel ();
        p.FlowDirection = FlowDirection.RightToLeft;

        p.Controls.Add (CreateButton (50, 50, false, DockStyle.None, new Padding (1, 3, 5, 2), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (50, 50, false, DockStyle.None, new Padding (7, 3, 12, 5), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (50, 50, false, DockStyle.None, new Padding (14, 7, 1, 3), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (50, 50, false, DockStyle.None, new Padding (4), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (145, 3, 50, 50);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "AP1");
        object expected1 = new Rectangle (82, 3, 50, 50);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "AP2");
        object expected2 = new Rectangle (24, 7, 50, 50);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2), "AP3");
        object expected3 = new Rectangle (146, 64, 50, 50);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3), "AP4");
    }

    [Test]
    public void RightToLeftLayoutTest15 ()
    {
        // Margins and Different Sizes
        var p = new FlowLayoutPanel ();
        p.FlowDirection = FlowDirection.RightToLeft;

        p.Controls.Add (CreateButton (25, 45, false, DockStyle.None, new Padding (6), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (60, 20, false, DockStyle.None, new Padding (9), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (15, 85, false, DockStyle.None, new Padding (2), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (50, 20, false, DockStyle.None, new Padding (4), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (169, 6, 25, 45);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "AQ1");
        object expected1 = new Rectangle (94, 9, 60, 20);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "AQ2");
        object expected2 = new Rectangle (68, 2, 15, 85);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2), "AQ3");
        object expected3 = new Rectangle (12, 4, 50, 20);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3), "AQ4");
    }

    [Test]
    public void RightToLeftLayoutTest16 ()
    {
        // Random Complex Layout 1
        var p = new FlowLayoutPanel ();
        p.FlowDirection = FlowDirection.RightToLeft;

        p.Controls.Add (CreateButton (25, 45, false, DockStyle.None, new Padding (6), AnchorStyles.Right | AnchorStyles.Top));
        p.Controls.Add (CreateButton (60, 20, false, DockStyle.Fill, new Padding (9), AnchorStyles.Bottom | AnchorStyles.Top));
        p.Controls.Add (CreateButton (15, 85, false, DockStyle.None, new Padding (2), AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom));
        p.Controls.Add (CreateButton (50, 20, false, DockStyle.None, new Padding (4), AnchorStyles.Left | AnchorStyles.Bottom));
        p.Controls.Add (CreateButton (13, 22, false, DockStyle.None, new Padding (12), AnchorStyles.Left | AnchorStyles.Right));
        p.Controls.Add (CreateButton (73, 28, false, DockStyle.Top, new Padding (6), AnchorStyles.None));

        object expected = new Rectangle (169, 6, 25, 45);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "AR1");
        object expected1 = new Rectangle (94, 9, 60, 39);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "AR2");
        object expected2 = new Rectangle (68, 2, 15, 53);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2), "AR3");
        object expected3 = new Rectangle (12, 33, 50, 20);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3), "AR4");
        object expected4 = new Rectangle (175, 69, 13, 22);
        Assert.That((object?)p.Controls[4].Bounds, Is.EqualTo(expected4), "AR5");
        object expected5 = new Rectangle (84, 63, 73, 28);
        Assert.That((object?)p.Controls[5].Bounds, Is.EqualTo(expected5), "AR6");
    }

    [Test]
    public void RightToLeftLayoutTest17 ()
    {
        // Random Complex Layout 2
        var p = new FlowLayoutPanel ();
        p.FlowDirection = FlowDirection.RightToLeft;

        p.Controls.Add (CreateButton (12, 345, false, DockStyle.Bottom, new Padding (1, 2, 3, 4), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (9, 44, false, DockStyle.Top, new Padding (6, 3, 2, 7), AnchorStyles.Right | AnchorStyles.Top));
        p.Controls.Add (CreateButton (78, 14, false, DockStyle.None, new Padding (5, 1, 2, 4), AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right));
        p.Controls.Add (CreateButton (21, 64, false, DockStyle.Top, new Padding (3, 3, 3, 1), AnchorStyles.None));
        p.Controls.Add (CreateButton (14, 14, false, DockStyle.Fill, new Padding (11, 4, 6, 3), AnchorStyles.Top | AnchorStyles.Bottom));
        p.Controls.Add (CreateButton (132, 6, false, DockStyle.Fill, new Padding (5, 5, 4, 5), AnchorStyles.Top | AnchorStyles.Bottom));

        p.SetFlowBreak (p.Controls[0], true);
        p.SetFlowBreak (p.Controls[2], true);

        object expected = new Rectangle (185, 2, 12, 345);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "AS1");
        object expected1 = new Rectangle (189, 354, 9, 44);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "AS2");
        object expected2 = new Rectangle (103, 352, 78, 49);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2), "AS3");
        object expected3 = new Rectangle (176, 408, 21, 64);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3), "AS4");
        object expected4 = new Rectangle (153, 409, 14, 61);
        Assert.That((object?)p.Controls[4].Bounds, Is.EqualTo(expected4), "AS5");
        object expected5 = new Rectangle (6, 410, 132, 58);
        Assert.That((object?)p.Controls[5].Bounds, Is.EqualTo(expected5), "AS6");
    }

    [Test]
    public void RightToLeftLayoutTest18 ()
    {
        // SetFlowBreak has no effect when WrapContents = false
        var p = new FlowLayoutPanel ();
        p.WrapContents = false;
        p.FlowDirection = FlowDirection.RightToLeft;

        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        p.SetFlowBreak (p.Controls[0], true);

        object expected = new Rectangle (100, 0, 100, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "AT1");
        object expected1 = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "AT2");
    }
    #endregion

    #region TopDown Tests
    [Test]
    public void TopDownLayoutTest1 ()
    {
        // 2 Normal Buttons
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.TopDown;

        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "BC1");
        object expected1 = new Rectangle (0, 100, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "BC2");
    }

    [Test]
    public void TopDownLayoutTest2 ()
    {
        // Dock Fill and Normal
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.TopDown;

        p.Controls.Add (CreateButton (50, 100, false, DockStyle.Fill, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "BD1");
        object expected1 = new Rectangle (0, 100, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "BD2");
    }

    [Test]
    public void TopDownLayoutTest3 ()
    {
        // Anchored: Left/Right and Normal
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.TopDown;

        p.Controls.Add (CreateButton (50, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Right));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "BE1");
        object expected1 = new Rectangle (0, 100, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "BE2");
    }

    [Test]
    public void TopDownLayoutTest4 ()
    {
        // Anchored: Left/Right and Dock Fill
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.TopDown;

        p.Controls.Add (CreateButton (50, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Right));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.Fill, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 0, 0, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "BF1");
        object expected1 = new Rectangle (0, 100, 0, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "BF2");
    }

    [Test]
    public void TopDownLayoutTest5 ()
    {
        // 2 Anchored: Left/Right
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.TopDown;

        p.Controls.Add (CreateButton (50, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Right));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Right));

        object expected = new Rectangle (0, 0, 0, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "BG1");
        object expected1 = new Rectangle (0, 100, 0, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "BG2");
    }

    [Test]
    public void TopDownLayoutTest6 ()
    {
        // 2 Dock Fill
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.TopDown;

        p.Controls.Add (CreateButton (50, 100, false, DockStyle.Fill, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.Fill, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 0, 0, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "BH1");
        object expected1 = new Rectangle (0, 100, 0, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "BH2");
    }

    [Test]
    public void TopDownLayoutTest7 ()
    {
        // Dock Left
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.TopDown;

        p.Controls.Add (CreateButton (50, 100, false, DockStyle.Left, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 0, 50, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "BI1");
        object expected1 = new Rectangle (0, 100, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "BI2");
    }

    [Test]
    public void TopDownLayoutTest8 ()
    {
        // Dock Right
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.TopDown;

        p.Controls.Add (CreateButton (50, 100, false, DockStyle.Right, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (50, 0, 50, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "BJ1");
        object expected1 = new Rectangle (0, 100, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "BJ2");
    }

    [Test]
    public void TopDownLayoutTest9 ()
    {
        // Anchor Right
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.TopDown;

        p.Controls.Add (CreateButton (50, 100, false, DockStyle.None, new Padding (), AnchorStyles.Top | AnchorStyles.Right));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Top | AnchorStyles.Left));

        object expected = new Rectangle (50, 0, 50, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "BK1");
        object expected1 = new Rectangle (0, 100, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "BK2");
    }

    [Test]
    public void TopDownLayoutTest10 ()
    {
        // No Dock or Anchor
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.TopDown;

        p.Controls.Add (CreateButton (50, 100, false, DockStyle.None, new Padding (), AnchorStyles.None));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (25, 0, 50, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "BL1");
        object expected1 = new Rectangle (0, 100, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "BL2");
    }

    [Test]
    public void TopDownLayoutTest11 ()
    {
        // WrapContents = true
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.TopDown;

        p.Controls.Add (CreateButton (50, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 0, 50, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "BM1");
        object expected1 = new Rectangle (0, 100, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "BM2");
        object expected2 = new Rectangle (100, 0, 100, 100);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2), "BM3");
        object expected3 = new Rectangle (100, 100, 100, 100);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3), "BM4");
    }

    [Test]
    public void TopDownLayoutTest12 ()
    {
        // WrapContents = false
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.WrapContents = false;
        p.FlowDirection = FlowDirection.TopDown;

        p.Controls.Add (CreateButton (50, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 0, 50, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "BN1");
        object expected1 = new Rectangle (0, 100, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "BN2");
        object expected2 = new Rectangle (0, 200, 100, 100);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2), "BN3");
        object expected3 = new Rectangle (0, 300, 100, 100);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3), "BN4");
    }

    [Test]
    public void TopDownLayoutTest13 ()
    {
        // SetFlowBreak 1, 3
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.TopDown;

        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        p.SetFlowBreak (p.Controls[0], true);
        p.SetFlowBreak (p.Controls[2], true);

        object expected = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "BO1");
        object expected1 = new Rectangle (100, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "BO2");
        object expected2 = new Rectangle (100, 100, 100, 100);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2), "BO3");
        object expected3 = new Rectangle (200, 0, 100, 100);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3), "BO4");
    }

    [Test]
    public void TopDownLayoutTest14 ()
    {
        // Margins
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.TopDown;

        p.Controls.Add (CreateButton (50, 50, false, DockStyle.None, new Padding (1, 3, 5, 2), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (50, 50, false, DockStyle.None, new Padding (7, 3, 12, 5), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (50, 50, false, DockStyle.None, new Padding (14, 7, 1, 3), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (50, 50, false, DockStyle.None, new Padding (4), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (1, 3, 50, 50);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "BP1");
        object expected1 = new Rectangle (7, 58, 50, 50);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "BP2");
        object expected2 = new Rectangle (14, 120, 50, 50);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2), "BP3");
        object expected3 = new Rectangle (73, 4, 50, 50);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3), "BP4");
    }

    [Test]
    public void TopDownLayoutTest15 ()
    {
        // Margins and Different Sizes
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.TopDown;

        p.Controls.Add (CreateButton (25, 45, false, DockStyle.None, new Padding (6), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (60, 20, false, DockStyle.None, new Padding (9), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (15, 85, false, DockStyle.None, new Padding (2), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (50, 20, false, DockStyle.None, new Padding (4), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (6, 6, 25, 45);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "BQ1");
        object expected1 = new Rectangle (9, 66, 60, 20);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "BQ2");
        object expected2 = new Rectangle (2, 97, 15, 85);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2), "BQ3");
        object expected3 = new Rectangle (82, 4, 50, 20);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3), "BQ4");
    }

    [Test]
    public void TopDownLayoutTest16 ()
    {
        // Random Complex Layout 1
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.TopDown;

        p.Controls.Add (CreateButton (25, 45, false, DockStyle.None, new Padding (6), AnchorStyles.Right | AnchorStyles.Top));
        p.Controls.Add (CreateButton (60, 20, false, DockStyle.Fill, new Padding (9), AnchorStyles.Bottom | AnchorStyles.Top));
        p.Controls.Add (CreateButton (15, 85, false, DockStyle.None, new Padding (2), AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom));
        p.Controls.Add (CreateButton (50, 20, false, DockStyle.None, new Padding (4), AnchorStyles.Left | AnchorStyles.Bottom));
        p.Controls.Add (CreateButton (13, 22, false, DockStyle.None, new Padding (12), AnchorStyles.Left | AnchorStyles.Right));
        p.Controls.Add (CreateButton (73, 28, false, DockStyle.Left, new Padding (6), AnchorStyles.None));

        object expected = new Rectangle (6, 6, 25, 45);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "BR1");
        object expected1 = new Rectangle (9, 66, 19, 20);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "BR2");
        object expected2 = new Rectangle (2, 97, 15, 85);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2), "BR3");
        object expected3 = new Rectangle (41, 4, 50, 20);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3), "BR4");
        object expected4 = new Rectangle (49, 40, 61, 22);
        Assert.That((object?)p.Controls[4].Bounds, Is.EqualTo(expected4), "BR5");
        object expected5 = new Rectangle (43, 80, 73, 28);
        Assert.That((object?)p.Controls[5].Bounds, Is.EqualTo(expected5), "BR6");
    }

    [Test]
    public void TopDownLayoutTest17 ()
    {
        // Random Complex Layout 2
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.TopDown;

        p.Controls.Add (CreateButton (12, 345, false, DockStyle.Right, new Padding (1, 2, 3, 4), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (9, 44, false, DockStyle.Left, new Padding (6, 3, 2, 7), AnchorStyles.Right | AnchorStyles.Top));
        p.Controls.Add (CreateButton (78, 14, false, DockStyle.None, new Padding (5, 1, 2, 4), AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right));
        p.Controls.Add (CreateButton (21, 64, false, DockStyle.Left, new Padding (3, 3, 3, 1), AnchorStyles.None));
        p.Controls.Add (CreateButton (14, 14, false, DockStyle.Fill, new Padding (11, 4, 6, 3), AnchorStyles.Left | AnchorStyles.Right));
        p.Controls.Add (CreateButton (132, 6, false, DockStyle.Fill, new Padding (5, 5, 4, 5), AnchorStyles.Left | AnchorStyles.Right));

        p.SetFlowBreak (p.Controls[0], true);
        p.SetFlowBreak (p.Controls[2], true);

        object expected = new Rectangle (1, 2, 12, 345);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "BS1");
        object expected1 = new Rectangle (22, 3, 9, 44);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "BS2");
        object expected2 = new Rectangle (21, 55, 10, 14);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2), "BS3");
        object expected3 = new Rectangle (36, 3, 21, 64);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3), "BS4");
        object expected4 = new Rectangle (44, 72, 10, 14);
        Assert.That((object?)p.Controls[4].Bounds, Is.EqualTo(expected4), "BS5");
        object expected5 = new Rectangle (38, 94, 18, 6);
        Assert.That((object?)p.Controls[5].Bounds, Is.EqualTo(expected5), "BS6");
    }

    [Test]
    public void TopDownLayoutTest18 ()
    {
        // SetFlowBreak has no effect when WrapContents = false
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.WrapContents = false;
        p.FlowDirection = FlowDirection.TopDown;

        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        p.SetFlowBreak (p.Controls[0], true);

        object expected = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "BT1");
        object expected1 = new Rectangle (0, 100, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "BT2");
    }
    #endregion

    #region BottomUp Tests
    [Test]
    public void BottomUpLayoutTest1 ()
    {
        // 2 Normal Buttons
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.BottomUp;

        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 100, 100, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "CC1");
        object expected1 = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "CC2");
    }

    [Test]
    public void BottomUpLayoutTest2 ()
    {
        // Dock Fill and Normal
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.BottomUp;

        p.Controls.Add (CreateButton (50, 100, false, DockStyle.Fill, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 100, 100, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "CD1");
        object expected1 = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "CD2");
    }

    [Test]
    public void BottomUpLayoutTest3 ()
    {
        // Anchored: Left/Right and Normal
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.BottomUp;

        p.Controls.Add (CreateButton (50, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Right));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 100, 100, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "CE1");
        object expected1 = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "CE2");
    }

    [Test]
    public void BottomUpLayoutTest4 ()
    {
        // Anchored: Left/Right and Dock Fill
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.BottomUp;

        p.Controls.Add (CreateButton (50, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Right));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.Fill, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 100, 0, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "CF1");
        object expected1 = new Rectangle (0, 0, 0, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "CF2");
    }

    [Test]
    public void BottomUpLayoutTest5 ()
    {
        // 2 Anchored: Left/Right
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.BottomUp;

        p.Controls.Add (CreateButton (50, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Right));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Right));

        object expected = new Rectangle (0, 100, 0, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "CG1");
        object expected1 = new Rectangle (0, 0, 0, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "CG2");
    }

    [Test]
    public void BottomUpLayoutTest6 ()
    {
        // 2 Dock Fill
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.BottomUp;

        p.Controls.Add (CreateButton (50, 100, false, DockStyle.Fill, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.Fill, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 100, 0, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "CH1");
        object expected1 = new Rectangle (0, 0, 0, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "CH2");
    }

    [Test]
    public void BottomUpLayoutTest7 ()
    {
        // Dock Left
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.BottomUp;

        p.Controls.Add (CreateButton (50, 100, false, DockStyle.Left, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 100, 50, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "CI1");
        object expected1 = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "CI2");
    }

    [Test]
    public void BottomUpLayoutTest8 ()
    {
        // Dock Right
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.BottomUp;

        p.Controls.Add (CreateButton (50, 100, false, DockStyle.Right, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (50, 100, 50, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "CJ1");
        object expected1 = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "CJ2");
    }

    [Test]
    public void BottomUpLayoutTest9 ()
    {
        // Anchor Right
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.BottomUp;

        p.Controls.Add (CreateButton (50, 100, false, DockStyle.None, new Padding (), AnchorStyles.Top | AnchorStyles.Right));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Top | AnchorStyles.Left));

        object expected = new Rectangle (50, 100, 50, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "CK1");
        object expected1 = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "CK2");
    }

    [Test]
    public void BottomUpLayoutTest10 ()
    {
        // No Dock or Anchor
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.BottomUp;

        p.Controls.Add (CreateButton (50, 100, false, DockStyle.None, new Padding (), AnchorStyles.None));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (25, 100, 50, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "CL1");
        object expected1 = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "CL2");
    }

    [Test]
    public void BottomUpLayoutTest11 ()
    {
        // WrapContents = true
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.BottomUp;

        p.Controls.Add (CreateButton (50, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 100, 50, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "CM1");
        object expected1 = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "CM2");
        object expected2 = new Rectangle (100, 100, 100, 100);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2), "CM3");
        object expected3 = new Rectangle (100, 0, 100, 100);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3), "CM4");
    }

    [Test]
    public void BottomUpLayoutTest12 ()
    {
        // WrapContents = false
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.WrapContents = false;
        p.FlowDirection = FlowDirection.BottomUp;

        p.Controls.Add (CreateButton (50, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (0, 100, 50, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "CN1");
        object expected1 = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "CN2");
        object expected2 = new Rectangle (0, -100, 100, 100);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2), "CN3");
        object expected3 = new Rectangle (0, -200, 100, 100);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3), "CN4");
    }

    [Test]
    public void BottomUpLayoutTest13 ()
    {
        // SetFlowBreak 1, 3
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.BottomUp;

        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        p.SetFlowBreak (p.Controls[0], true);
        p.SetFlowBreak (p.Controls[2], true);

        object expected = new Rectangle (0, 100, 100, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "CO1");
        object expected1 = new Rectangle (100, 100, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "CO2");
        object expected2 = new Rectangle (100, 0, 100, 100);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2), "CO3");
        object expected3 = new Rectangle (200, 100, 100, 100);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3), "CO4");
    }

    [Test]
    public void BottomUpLayoutTest14 ()
    {
        // Margins
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.BottomUp;

        p.Controls.Add (CreateButton (50, 50, false, DockStyle.None, new Padding (1, 3, 5, 2), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (50, 50, false, DockStyle.None, new Padding (7, 3, 12, 5), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (50, 50, false, DockStyle.None, new Padding (14, 7, 1, 3), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (50, 50, false, DockStyle.None, new Padding (4), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (1, 148, 50, 50);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "CP1");
        object expected1 = new Rectangle (7, 90, 50, 50);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "CP2");
        object expected2 = new Rectangle (14, 34, 50, 50);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2), "CP3");
        object expected3 = new Rectangle (73, 146, 50, 50);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3), "CP4");
    }

    [Test]
    public void BottomUpLayoutTest15 ()
    {
        // Margins and Different Sizes
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.BottomUp;

        p.Controls.Add (CreateButton (25, 45, false, DockStyle.None, new Padding (6), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (60, 20, false, DockStyle.None, new Padding (9), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (15, 85, false, DockStyle.None, new Padding (2), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (50, 20, false, DockStyle.None, new Padding (4), AnchorStyles.Left | AnchorStyles.Top));

        object expected = new Rectangle (6, 149, 25, 45);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "CQ1");
        object expected1 = new Rectangle (9, 114, 60, 20);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "CQ2");
        object expected2 = new Rectangle (2, 18, 15, 85);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2), "CQ3");
        object expected3 = new Rectangle (82, 176, 50, 20);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3), "CQ4");
    }

    [Test]
    public void BottomUpLayoutTest16 ()
    {
        // Random Complex Layout 1
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.BottomUp;

        p.Controls.Add (CreateButton (25, 45, false, DockStyle.None, new Padding (6), AnchorStyles.Right | AnchorStyles.Top));
        p.Controls.Add (CreateButton (60, 20, false, DockStyle.Fill, new Padding (9), AnchorStyles.Bottom | AnchorStyles.Top));
        p.Controls.Add (CreateButton (15, 85, false, DockStyle.None, new Padding (2), AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom));
        p.Controls.Add (CreateButton (50, 20, false, DockStyle.None, new Padding (4), AnchorStyles.Left | AnchorStyles.Bottom));
        p.Controls.Add (CreateButton (13, 22, false, DockStyle.None, new Padding (12), AnchorStyles.Left | AnchorStyles.Right));
        p.Controls.Add (CreateButton (73, 28, false, DockStyle.Left, new Padding (6), AnchorStyles.None));

        object expected = new Rectangle (6, 149, 25, 45);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "CR1");
        object expected1 = new Rectangle (9, 114, 19, 20);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "CR2");
        object expected2 = new Rectangle (2, 18, 15, 85);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2), "CR3");
        object expected3 = new Rectangle (41, 176, 50, 20);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3), "CR4");
        object expected4 = new Rectangle (49, 138, 61, 22);
        Assert.That((object?)p.Controls[4].Bounds, Is.EqualTo(expected4), "CR5");
        object expected5 = new Rectangle (43, 92, 73, 28);
        Assert.That((object?)p.Controls[5].Bounds, Is.EqualTo(expected5), "CR6");
    }

    [Test]
    public void BottomUpLayoutTest17 ()
    {
        // Random Complex Layout 2
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.FlowDirection = FlowDirection.BottomUp;

        p.Controls.Add (CreateButton (12, 345, false, DockStyle.Right, new Padding (1, 2, 3, 4), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (9, 44, false, DockStyle.Left, new Padding (6, 3, 2, 7), AnchorStyles.Right | AnchorStyles.Top));
        p.Controls.Add (CreateButton (78, 14, false, DockStyle.None, new Padding (5, 1, 2, 4), AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right));
        p.Controls.Add (CreateButton (21, 64, false, DockStyle.Left, new Padding (3, 3, 3, 1), AnchorStyles.None));
        p.Controls.Add (CreateButton (14, 14, false, DockStyle.Fill, new Padding (11, 4, 6, 3), AnchorStyles.Left | AnchorStyles.Right));
        p.Controls.Add (CreateButton (132, 6, false, DockStyle.Fill, new Padding (5, 5, 4, 5), AnchorStyles.Left | AnchorStyles.Right));

        p.SetFlowBreak (p.Controls[0], true);
        p.SetFlowBreak (p.Controls[2], true);

        object expected = new Rectangle (1, -149, 12, 345);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "CS1");
        object expected1 = new Rectangle (22, 149, 9, 44);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "CS2");
        object expected2 = new Rectangle (21, 128, 10, 14);
        Assert.That((object?)p.Controls[2].Bounds, Is.EqualTo(expected2), "CS3");
        object expected3 = new Rectangle (36, 135, 21, 64);
        Assert.That((object?)p.Controls[3].Bounds, Is.EqualTo(expected3), "CS4");
        object expected4 = new Rectangle (44, 115, 10, 14);
        Assert.That((object?)p.Controls[4].Bounds, Is.EqualTo(expected4), "CS5");
        object expected5 = new Rectangle (38, 100, 18, 6);
        Assert.That((object?)p.Controls[5].Bounds, Is.EqualTo(expected5), "CS6");
    }

    [Test]
    public void BottomUpLayoutTest18 ()
    {
        // SetFlowBreak has no effect when WrapContents = false
        var p = new FlowLayoutPanel ();
        p.Size = new Size (100, 200);
        p.WrapContents = false;
        p.FlowDirection = FlowDirection.BottomUp;

        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));
        p.Controls.Add (CreateButton (100, 100, false, DockStyle.None, new Padding (), AnchorStyles.Left | AnchorStyles.Top));

        p.SetFlowBreak (p.Controls[0], true);

        object expected = new Rectangle (0, 100, 100, 100);
        Assert.That((object?)p.Controls[0].Bounds, Is.EqualTo(expected), "CT1");
        object expected1 = new Rectangle (0, 0, 100, 100);
        Assert.That((object?)p.Controls[1].Bounds, Is.EqualTo(expected1), "CT2");
    }
    #endregion

    private Button CreateButton (int width, int height, bool autosize, DockStyle dock, Padding margin, AnchorStyles anchor)
    {
        var b = new Button ();
        b.Size = new Size(width, height);
        b.AutoSize = autosize;
        b.Anchor = anchor;
        b.Dock = dock;
        b.Margin = margin;
			
        return b;
    }

    #region PreferredSize
    [Test]
    public void PreferredSize ()
    {
        var panel = new FlowLayoutPanel ();
        panel.Controls.AddRange (new PreferredSizeControl (), new PreferredSizeControl ());
        object expected = new Size (212, 106);
        Assert.That((object?)panel.PreferredSize, Is.EqualTo(expected), "1");
        object expected1 = new Size (106, 212);
        Assert.That((object?)panel.GetPreferredSize (new Size (150, 150)), Is.EqualTo(expected1), "2");
        object expected2 = new Size (212, 106);
        Assert.That((object?)panel.GetPreferredSize (new Size (1000, 1000)), Is.EqualTo(expected2), "3");
        object expected3 = new Size (212, 106);
        Assert.That((object?)panel.GetPreferredSize (new Size (0, 0)), Is.EqualTo(expected3), "4");
        object expected4 = new Size (106, 212);
        Assert.That((object?)panel.GetPreferredSize (new Size (1, 1)), Is.EqualTo(expected4), "5");
        object expected5 = new Size (212, 106);
        Assert.That((object?)panel.GetPreferredSize (new Size (0, 150)), Is.EqualTo(expected5), "6");
        object expected6 = new Size (106, 212);
        Assert.That((object?)panel.GetPreferredSize (new Size (150, 0)), Is.EqualTo(expected6), "7");
        panel.WrapContents = false;
        object expected7 = new Size (212, 106);
        Assert.That((object?)panel.PreferredSize, Is.EqualTo(expected7), "1, WrapContents");
        object expected8 = new Size (212, 106);
        Assert.That((object?)panel.GetPreferredSize (new Size (150, 150)), Is.EqualTo(expected8), "2, WrapContents");
        object expected9 = new Size (212, 106);
        Assert.That((object?)panel.GetPreferredSize (new Size (1000, 1000)), Is.EqualTo(expected9), "3, WrapContents");
        object expected10 = new Size (212, 106);
        Assert.That((object?)panel.GetPreferredSize (new Size (0, 0)), Is.EqualTo(expected10), "4, WrapContents");
        object expected11 = new Size (212, 106);
        Assert.That((object?)panel.GetPreferredSize (new Size (1, 1)), Is.EqualTo(expected11), "5, WrapContents");
        object expected12 = new Size (212, 106);
        Assert.That((object?)panel.GetPreferredSize (new Size (0, 150)), Is.EqualTo(expected12), "6, WrapContents");
        object expected13 = new Size (212, 106);
        Assert.That((object?)panel.GetPreferredSize (new Size (150, 0)), Is.EqualTo(expected13), "7, WrapContents");
    }

    private class PreferredSizeControl : Control
    {
        protected override Size DefaultSize => new(100, 100);
    }
    #endregion
		
    [Test]
    public void Padding ()
    {
        var f = new Form ();
			
        var flp = new FlowLayoutPanel ();
        flp.Padding = new Padding (20);
        flp.Size = new Size (100, 100);

        var b = new Button ();
        b.Size = new Size (50, 50);

        var b2 = new Button ();
        b2.Size = new Size (50, 50);

        flp.Controls.Add (b);
        flp.Controls.Add (b2);

        f.Controls.Add (flp);

        object expected = new Rectangle (23, 23, 50, 50);
        Assert.That((object?)b.Bounds, Is.EqualTo(expected));
        object expected1 = new Rectangle (23, 79, 50, 50);
        Assert.That((object?)b2.Bounds, Is.EqualTo(expected1));
    }
}

[TestFixture]
public class FlowPanelTests_AutoSize: TestHelper
{
    private Form f;
    protected override void SetUp ()
    {
        base.SetUp ();
        f = new Form ();
        f.AutoSize = false;
        f.ClientSize = new Size (100, 300);
        f.ShowInTaskbar = false;
        f.Show ();
    }

    protected override void TearDown ()
    {
        f.Dispose ();
        base.TearDown ();
    }

    [Test]
    public void AutoSizeGrowOnly_ResizeIfLarger ()
    {
        var panel = new FlowLayoutPanel ();
        panel.SuspendLayout ();
        panel.AutoSize = true;
        panel.WrapContents = true;
        panel.Bounds = new Rectangle (5, 5, 10, 10);
        panel.Dock = DockStyle.None;

        var c = new Label ();
        c.Size = new Size (90, 25);
        panel.Controls.Add (c);
        c = new Label ();
        c.Size = new Size (90, 25);
        panel.Controls.Add (c);
        f.Controls.Add (panel);
        panel.ResumeLayout (true);

        Assert.That((object?)panel.Width, Is.EqualTo(192), "1"); // 2 * 90 + 4 * 3 margin
        Assert.That((object?)panel.Height, Is.EqualTo(25), "2");
    }

    [Test]
    public void AutoSizeGrowOnly_ResizeIfLarger_DockBottom ()
    {
        var panel = new FlowLayoutPanel ();
        panel.SuspendLayout ();
        panel.AutoSize = true;
        panel.WrapContents = true;
        panel.Bounds = new Rectangle (5, 5, 10, 10);
        panel.Dock = DockStyle.Bottom;

        var c = new Label ();
        c.Size = new Size (90, 25);
        panel.Controls.Add (c);
        c = new Label ();
        c.Size = new Size (90, 25);
        panel.Controls.Add (c);
        f.Controls.Add (panel);
        panel.ResumeLayout (true);

        Assert.That((object?)panel.Top, Is.EqualTo(250), "1");
        Assert.That((object?)panel.Width, Is.EqualTo(f.ClientRectangle.Width), "2");
        Assert.That((object?)panel.Height, Is.EqualTo(50), "3");
    }

    [Test]
    public void AutoSizeGrowOnly_DontResizeIfSmaller ()
    {
        var panel = new FlowLayoutPanel ();
        panel.SuspendLayout ();
        panel.AutoSize = true;
        panel.WrapContents = true;
        panel.Bounds = new Rectangle(5, 5, 100, 100);
        panel.Dock = DockStyle.None;

        var c = new Label ();
        c.Size = new Size (90, 25);
        panel.Controls.Add (c);
        f.Controls.Add (panel);
        panel.ResumeLayout (true);

        Assert.That((object?)panel.Width, Is.EqualTo(100), "1");
        Assert.That((object?)panel.Height, Is.EqualTo(100), "2");
    }

    [Test]
    public void AutoSizeGrowOnly_ResizeIfSmaller_DockTop ()
    {
        var panel = new FlowLayoutPanel ();
        panel.SuspendLayout ();
        panel.AutoSize = true;
        panel.WrapContents = true;
        panel.Bounds = new Rectangle(5, 5, 100, 100);
        panel.Dock = DockStyle.Top;

        var c = new Label ();
        c.Size = new Size (90, 25);
        panel.Controls.Add (c);
        f.Controls.Add(panel);
        panel.ResumeLayout (true);

        Assert.That((object?)panel.Top, Is.EqualTo(0), "1");
        Assert.That((object?)panel.Width, Is.EqualTo(f.ClientRectangle.Width), "2");
        Assert.That((object?)panel.Height, Is.EqualTo(25), "3");
    }

    [Test]
    public void AutoSizeGrowOnly_ResizeIfSmaller_DockBottom ()
    {
        var panel = new FlowLayoutPanel ();
        panel.SuspendLayout ();
        panel.AutoSize = true;
        panel.WrapContents = true;
        panel.Bounds = new Rectangle(5, 5, 100, 100);
        panel.Dock = DockStyle.Bottom;

        var c = new Label ();
        c.Size = new Size (90, 25);
        panel.Controls.Add (c);
        f.Controls.Add(panel);
        panel.ResumeLayout (true);

        Assert.That((object?)panel.Top, Is.EqualTo(275), "1");
        Assert.That((object?)panel.Width, Is.EqualTo(f.ClientRectangle.Width), "2");
        Assert.That((object?)panel.Height, Is.EqualTo(25), "3");
    }

    [Test]
    public void AutoSizeGrowOnly_ResizeIfSmaller_DockLeft ()
    {
        f.ClientSize = new Size (300, 100);

        var panel = new FlowLayoutPanel ();
        panel.SuspendLayout ();
        panel.AutoSize = true;
        panel.WrapContents = true;
        panel.Bounds = new Rectangle(5, 5, 100, 100);
        panel.Dock = DockStyle.Left;

        var c = new Label ();
        c.Size = new Size (25, 90);
        panel.Controls.Add (c);
        f.Controls.Add(panel);
        panel.ResumeLayout (true);

        Assert.That((object?)panel.Left, Is.EqualTo(0), "1");
        Assert.That((object?)panel.Height, Is.EqualTo(f.ClientRectangle.Height), "2");
        Assert.That((object?)panel.Width, Is.EqualTo(31), "3"); // 25 + 2*3 margin
    }

    [Test]
    public void AutoSizeGrowOnly_ResizeIfSmaller_DockRight ()
    {
        f.ClientSize = new Size (300, 100);

        var panel = new FlowLayoutPanel ();
        panel.SuspendLayout ();
        panel.AutoSize = true;
        panel.WrapContents = true;
        panel.Bounds = new Rectangle(5, 5, 100, 100);
        panel.Dock = DockStyle.Right;

        var c = new Label ();
        c.Size = new Size (25, 90);
        panel.Controls.Add (c);
        f.Controls.Add(panel);
        panel.ResumeLayout (true);

        Assert.That((object?)panel.Left, Is.EqualTo(269), "1");
        Assert.That((object?)panel.Height, Is.EqualTo(f.ClientRectangle.Height), "2");
        Assert.That((object?)panel.Width, Is.EqualTo(31), "3"); // 25 + 2*3 margin
    }

    [Test]
    public void AutoSizeGrowAndShrink_ResizeIfSmaller ()
    {
        var panel = new FlowLayoutPanel ();
        panel.SuspendLayout ();
        panel.AutoSize = true;
        panel.WrapContents = true;
        panel.Bounds = new Rectangle (5, 5, 100, 100);
        panel.Dock = DockStyle.None;

        var c = new Label ();
        c.Size = new Size (90, 25);
        panel.Controls.Add (c);
        f.Controls.Add(panel);
        panel.ResumeLayout (true);

        Assert.That((object?)panel.Width, Is.EqualTo(96), "1"); // 90 + 2*3 margin
        Assert.That((object?)panel.Height, Is.EqualTo(25), "2");
    }

    [Test]
    public void AutoSizeGrowAndShrink_ResizeIfSmaller_DockBottom ()
    {
        var panel = new FlowLayoutPanel ();
        panel.SuspendLayout ();
        panel.AutoSize = true;
        panel.WrapContents = true;
        panel.Bounds = new Rectangle (5, 5, 100, 100);
        panel.Dock = DockStyle.Bottom;

        var c = new Label ();
        c.Size = new Size (90, 25);
        panel.Controls.Add (c);
        f.Controls.Add (panel);
        panel.ResumeLayout (true);

        Assert.That((object?)panel.Top, Is.EqualTo(275), "1");
        Assert.That((object?)panel.Width, Is.EqualTo(f.ClientRectangle.Width), "2");
        Assert.That((object?)panel.Height, Is.EqualTo(25), "3");
    }

    [Test]
    public void NoAutoSize_DontResizeIfLarger ()
    {
        var panel = new FlowLayoutPanel ();
        panel.SuspendLayout ();
        panel.AutoSize = false;
        panel.WrapContents = true;
        panel.Bounds = new Rectangle (5, 5, 10, 10);
        panel.Dock = DockStyle.None;

        var c = new Label ();
        c.Size = new Size (90, 25);
        panel.Controls.Add (c);
        c = new Label ();
        c.Size = new Size (90, 25);
        panel.Controls.Add (c);
        f.Controls.Add (panel);
        panel.ResumeLayout (true);

        Assert.That((object?)panel.Width, Is.EqualTo(10), "1");
        Assert.That((object?)panel.Height, Is.EqualTo(10), "2");
    }

    [Test]
    public void NoAutoSize_DontResizeIfLarger_DockBottom ()
    {
        var panel = new FlowLayoutPanel ();
        panel.SuspendLayout ();
        panel.AutoSize = false;
        panel.WrapContents = true;
        panel.Bounds = new Rectangle (5, 5, 10, 10);
        panel.Dock = DockStyle.Bottom;

        var c = new Label ();
        c.Size = new Size (90, 25);
        panel.Controls.Add (c);
        c = new Label ();
        c.Size = new Size (90, 25);
        panel.Controls.Add (c);
        f.Controls.Add (panel);
        panel.ResumeLayout (true);

        Assert.That((object?)panel.Top, Is.EqualTo(290), "1");
        Assert.That((object?)panel.Width, Is.EqualTo(f.ClientRectangle.Width), "2");
        Assert.That((object?)panel.Height, Is.EqualTo(10), "3");
    }

    [Test]
    public void NoAutoSize_DontResizeIfSmaller ()
    {
        var panel = new FlowLayoutPanel ();
        panel.SuspendLayout ();
        panel.AutoSize = false;
        panel.WrapContents = true;
        panel.Bounds = new Rectangle(5, 5, 100, 100);
        panel.Dock = DockStyle.None;

        var c = new Label ();
        c.Size = new Size (90, 25);
        panel.Controls.Add (c);
        f.Controls.Add (panel);
        panel.ResumeLayout (true);

        Assert.That((object?)panel.Width, Is.EqualTo(100), "1");
        Assert.That((object?)panel.Height, Is.EqualTo(100), "2");
    }

    [Test]
    public void NoAutoSize_DontResizeIfSmaller_DockBottom ()
    {
        var panel = new FlowLayoutPanel ();
        panel.SuspendLayout ();
        panel.AutoSize = false;
        panel.WrapContents = true;
        panel.Bounds = new Rectangle(5, 5, 100, 100);
        panel.Dock = DockStyle.Bottom;

        var c = new Label ();
        c.Size = new Size (90, 25);
        panel.Controls.Add (c);
        f.Controls.Add(panel);
        panel.ResumeLayout (true);

        Assert.That((object?)panel.Top, Is.EqualTo(200), "1");
        Assert.That((object?)panel.Width, Is.EqualTo(f.ClientRectangle.Width), "2");
        Assert.That((object?)panel.Height, Is.EqualTo(100), "3");
    }
}