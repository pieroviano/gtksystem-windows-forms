using System.Windows.Forms;
using System.Drawing;
using GtkTests.Helpers;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class SplitContainerTests : TestHelper
{
    [Test]
    public void TestSplitContainerConstruction ()
    {
        var sc = new SplitContainer ();

        object expected = new Size (150, 100);
        Assert.That((object?)sc.Size, Is.EqualTo(expected));
        Assert.That((object?)sc.FixedPanel, Is.EqualTo(FixedPanel.None));
        Assert.That((object?)sc.Orientation, Is.EqualTo(Orientation.Vertical));
        Assert.That((object?)sc.SplitterDistance, Is.EqualTo(50));
        Assert.That((object?)sc.SplitterIncrement, Is.EqualTo(1));
        Assert.That((object?)sc.SplitterWidth, Is.EqualTo(4));
        Assert.That((object?)sc.BorderStyle, Is.EqualTo(BorderStyle.None));
        Assert.That((object?)sc.Dock, Is.EqualTo(DockStyle.None));
    }
		
    [Test]
    public void TestProperties ()
    {
        var sc = new SplitContainer ();
			
        sc.BorderStyle = BorderStyle.FixedSingle;
        Assert.That((object?)sc.BorderStyle, Is.EqualTo(BorderStyle.FixedSingle));

        sc.Dock =  DockStyle.Fill;
        Assert.That((object?)sc.Dock, Is.EqualTo(DockStyle.Fill));

        sc.FixedPanel = FixedPanel.Panel1;
        Assert.That((object?)sc.FixedPanel, Is.EqualTo(FixedPanel.Panel1));

        sc.Orientation = Orientation.Horizontal;
        Assert.That((object?)sc.Orientation, Is.EqualTo(Orientation.Horizontal));

        sc.SplitterDistance = 77;
        Assert.That((object?)sc.SplitterDistance, Is.EqualTo(77));
			
        sc.SplitterIncrement = 5;
        Assert.That((object?)sc.SplitterIncrement, Is.EqualTo(5));
			
        sc.SplitterWidth = 10;
        Assert.That((object?)sc.SplitterWidth, Is.EqualTo(10));
    }
		
    [Test]
    public void TestPanelProperties ()
    {
        var sc = new SplitContainer ();
        var p = sc.Panel1;

        Assert.That((object?)p.Anchor, Is.EqualTo(AnchorStyles.Top | AnchorStyles.Left));
        p.Anchor = AnchorStyles.None;
        Assert.That((object?)p.Anchor, Is.EqualTo(AnchorStyles.None), "D1-2");

        Assert.That((object?)p.AutoSize, Is.EqualTo(false));
        p.AutoSize = true;
        Assert.That((object?)p.AutoSize, Is.EqualTo(true), "D2-2");

        Assert.That((object?)p.BorderStyle, Is.EqualTo(BorderStyle.None));
        p.BorderStyle = BorderStyle.FixedSingle;
        Assert.That((object?)p.BorderStyle, Is.EqualTo(BorderStyle.FixedSingle), "D4-2");

        Assert.That((object?)p.Dock, Is.EqualTo(DockStyle.None));
        p.Dock = DockStyle.Left;
        Assert.That((object?)p.Dock, Is.EqualTo(DockStyle.Left), "D5-2");

        object expected = new Point (0, 0);
        Assert.That((object?)p.Location, Is.EqualTo(expected));
        p.Location = new Point (10, 10);
        object expected1 = new Point (0, 0);
        Assert.That((object?)p.Location, Is.EqualTo(expected1), "D7-2");

        object expected2 = new Size (0, 0);
        Assert.That((object?)p.MaximumSize, Is.EqualTo(expected2));
        p.MaximumSize = new Size (10, 10);
        object expected3 = new Size (10, 10);
        Assert.That((object?)p.MaximumSize, Is.EqualTo(expected3), "D8-2");

        object expected4 = new Size (0, 0);
        Assert.That((object?)p.MinimumSize, Is.EqualTo(expected4));
        p.MinimumSize = new Size (10, 10);
        object expected5 = new Size (10, 10);
        Assert.That((object?)p.MinimumSize, Is.EqualTo(expected5), "D9-2");

        object expected6 = string.Empty;
        Assert.That((object?)p.Name, Is.EqualTo(expected6));
        p.Name = "MyPanel";
        Assert.That((object?)p.Name, Is.EqualTo("MyPanel"), "D10-2");

        // We set a new max/min size above, so let's start over with new controls
        sc = new SplitContainer();
        p = sc.Panel1;

        object expected7 = new Size (50, 100);
        Assert.That((object?)p.Size, Is.EqualTo(expected7));
        p.Size = new Size (10, 10);
        object expected8 = new Size (50, 100);
        Assert.That((object?)p.Size, Is.EqualTo(expected8), "D12-2");

        //Assert1.AreEqual(0, p.TabIndex);
        p.TabIndex = 4;
        Assert.That((object?)p.TabIndex, Is.EqualTo(4), "D13-2");

        Assert.That((object?)p.TabStop, Is.EqualTo(false));
        p.TabStop = true;
        Assert.That((object?)p.TabStop, Is.EqualTo(true), "D14-2");

        Assert.That((object?)p.Visible, Is.EqualTo(true));
        p.Visible = false;
        Assert.That((object?)p.Visible, Is.EqualTo(false), "D15-2");
    }
		
    [Test]
    public void TestPanelHeightProperty ()
    {
        Assert.Throws<NotSupportedException>(() =>
        {
            var sc = new SplitContainer();
            var p = sc.Panel1;

            Assert.That((object?)p.Height, Is.EqualTo(100));

            p.Height = 200;
        });
    }

    [Test]
    public void TestPanelWidthProperty ()
    {
        Assert.Throws<NotSupportedException>(() =>
        {
            var sc = new SplitContainer();
            var p = sc.Panel1;

            Assert.That((object?)p.Width, Is.EqualTo(50));

            p.Width = 200;
        });
    }

    [Test]
    public void TestPanelParentProperty ()
    {
        Assert.Throws<NotSupportedException>(() =>
        {
            var sc = new SplitContainer();
            var sc2 = new SplitContainer();
            var p = sc.Panel1;

            Assert.That((object?)p.Parent, Is.EqualTo(sc));

            p.Parent = sc2;
        });
    }

    [Test]
    public void TestFixedPanelNone ()
    {
        var sc = new SplitContainer ();

        Assert.That((object?)sc.SplitterDistance, Is.EqualTo(50));

        sc.Width = 300;
        Assert.That((object?)sc.SplitterDistance, Is.EqualTo(100));
    }
		
    [Test]
    public void TestFixedPanel1 ()
    {
        var sc = new SplitContainer ();
        sc.FixedPanel = FixedPanel.Panel1;
			
        Assert.That((object?)sc.SplitterDistance, Is.EqualTo(50));

        sc.Width = 300;
        Assert.That((object?)sc.SplitterDistance, Is.EqualTo(50));
    }
		
    [Test]
    public void TestFixedPanel2 ()
    {
        var sc = new SplitContainer ();
        sc.FixedPanel = FixedPanel.Panel2;

        Assert.That((object?)sc.SplitterDistance, Is.EqualTo(50));

        sc.Width = 300;
        Assert.That((object?)sc.SplitterDistance, Is.EqualTo(200));
    }
}