//
// UserControlTest.cs
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
// Copyright (c) 2006 Daniel Nauck
//
// Authors:
//   	Daniel Nauck    (dna(at)mono-project(dot)de)

using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using GtkTests.Helpers;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class UserControlTest : TestHelper
{
    private UserControl? uc;

    [TearDown]
    public void TestTearDown()
    {
        uc?.Dispose();
    }

    [SetUp]
    protected override void SetUp () {
        uc = new UserControl();
        base.SetUp ();
    }

    [Test]
    public void PropertyTest()
    {
        object expected = string.Empty;
        Assert.That((object?)uc!.Text, Is.EqualTo(expected));

        Assert.That((object?)uc.BorderStyle, Is.EqualTo(BorderStyle.None));
        uc.BorderStyle = BorderStyle.Fixed3D;
        Assert.That((object?)uc.BorderStyle, Is.EqualTo(BorderStyle.Fixed3D));
        uc.BorderStyle = BorderStyle.FixedSingle;
        Assert.That((object?)uc.BorderStyle, Is.EqualTo(BorderStyle.FixedSingle));
        uc.BorderStyle = BorderStyle.None;
        Assert.That((object?)uc.BorderStyle, Is.EqualTo(BorderStyle.None));
    }

    [Test]
    public void BorderStyleInvalidEnumArgumentException()
    {
        Assert.Throws<InvalidEnumArgumentException>(() =>
        {
            uc!.BorderStyle = (BorderStyle)9999;
        });
    }
		
    [Test]
    public void MethodCreateParams ()
    {
        var uc = new ExposeProtectedProperties ();

        Assert.That((object?)(WindowStyles)uc.CreateParams.Style, Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_MAXIMIZEBOX | WindowStyles.WS_CLIPCHILDREN | WindowStyles.WS_CLIPSIBLINGS | WindowStyles.WS_VISIBLE | WindowStyles.WS_CHILD));
        Assert.That((object?)(WindowExStyles)uc.CreateParams.ExStyle, Is.EqualTo(WindowExStyles.WS_EX_CONTROLPARENT));
    }

    private class ExposeProtectedProperties : UserControl
    {
        public new CreateParams CreateParams => base.CreateParams;
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

        f.Close ();
    }

    [Test]
    public void PreferredSize ()
    {
        var f = new Form ();
        f.ShowInTaskbar = false;

        var p = new UserControl ();
        f.Controls.Add (p);

        var b1 = new Button ();
        b1.Size = new Size (200, 200);
        b1.Dock = DockStyle.Fill;
        p.Controls.Add (b1);

        var b = new Button ();
        b.Size = new Size (100, 100);
        b.Dock = DockStyle.Top;
        p.Controls.Add (b);
			
        f.Show ();

        object expected = new Size (0, 100);
        Assert.That((object?)p.PreferredSize, Is.EqualTo(expected));
			
        b1.Dock = DockStyle.Left;
        object expected1 = new Size (200, 100);
        Assert.That((object?)p.PreferredSize, Is.EqualTo(expected1));

        b1.Dock = DockStyle.None;
        object expected2 = new Size (203, 203);
        Assert.That((object?)p.PreferredSize, Is.EqualTo(expected2));

        b1.Dock = DockStyle.Fill;
        b.Dock = DockStyle.Fill;
        object expected3 = new Size (0, 0);
        Assert.That((object?)p.PreferredSize, Is.EqualTo(expected3));
			
        b1.Dock = DockStyle.Top;
        b.Dock = DockStyle.Left;

        object expected4 = new Size (100, 200);
        Assert.That((object?)p.PreferredSize, Is.EqualTo(expected4));
		
        var b2 = new Button ();
        b2.Size = new Size (50, 50);
        p.Controls.Add (b2);

        object expected5 = new Size (100, 200);
        Assert.That((object?)p.PreferredSize, Is.EqualTo(expected5));
			
        b2.Left = 300;
        object expected6 = new Size (353, 200);
        Assert.That((object?)p.PreferredSize, Is.EqualTo(expected6));

        b2.Top = 300;
        object expected7 = new Size (353, 353);
        Assert.That((object?)p.PreferredSize, Is.EqualTo(expected7));

        b2.Anchor = AnchorStyles.Bottom;
        object expected8 = new Size (100, 200);
        Assert.That((object?)p.PreferredSize, Is.EqualTo(expected8));

        b2.Anchor = AnchorStyles.Left;
        object expected9 = new Size (353, 353);
        Assert.That((object?)p.PreferredSize, Is.EqualTo(expected9));
			
        f.Dispose ();
    }
	
}