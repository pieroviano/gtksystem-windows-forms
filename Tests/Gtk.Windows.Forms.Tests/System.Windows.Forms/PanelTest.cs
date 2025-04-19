//
// PanelTest.cs: Test cases for PanelTest.
//
// Author:
//   Jonathan Pobst (monkey@jpobst.com)
//
// (C) 2007 Novell, Inc.
//

using GtkTests.Helpers;
using System.Drawing;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class PanelTest : TestHelper
{
    [Test]
    public void Constructor ()
    {
        var p = new Panel ();

        Assert.That((object?)p.AutoSize, Is.EqualTo(false));
        Assert.That((object?)p.BorderStyle, Is.EqualTo(BorderStyle.None));
        Assert.That((object?)p.TabStop, Is.EqualTo(false));
        object expected = string.Empty;
        Assert.That((object?)p.Text, Is.EqualTo(expected));
    }

    [Test]
    public void AutoSize ()
    {
        var f = new Form ();
        f.ShowInTaskbar = false;

        var p = new Panel ();
        p.AutoSize = true;
        f.Controls.Add (p);
			
        var b = new Button ();
        b.Size = new Size (200, 200);
        b.Location = new Point (200, 200);
        p.Controls.Add (b);

        f.Show ();

        object expected = new Size (403, 403);
        Assert.That((object?)p.ClientSize, Is.EqualTo(expected));
			
        p.Controls.Remove (b);
        object expected1 = new Size (200, 100);
        Assert.That((object?)p.ClientSize, Is.EqualTo(expected1));

        object expected2 = new Size (0, 0);
        Assert.That((object?)p.ClientSize, Is.EqualTo(expected2));
        f.Dispose ();
    }
}