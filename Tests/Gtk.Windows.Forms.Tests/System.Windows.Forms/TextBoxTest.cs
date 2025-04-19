//
// Copyright (c) 2005 Novell, Inc.
//
// Authors:
//      Ritvik Mayank (mritvik@novell.com)
//

using System.Drawing;
using System.Windows.Forms;
using GtkTests.Helpers;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class TextBoxTest : TestHelper
{
    private TextBox textBox;
    private int _changed;
    private int _invalidated;
    private int _paint;

    [TearDown]
    protected override void TearDown()
    {
        textBox.Dispose();
    }

    [SetUp]
    protected override void SetUp () {
        textBox = new TextBox();
        textBox.Invalidated += TextBox_Invalidated;
        textBox.Paint += TextBox_Paint;
        textBox.TextChanged += TextBox_TextChanged;
        Reset ();
        base.SetUp ();
    }

    [Test]
    public void TextBoxBasePropertyTest ()
    {
        textBox.Multiline = true;
        Assert.That((object?)textBox.AutoSize, Is.EqualTo(true));
        Assert.That((object?)textBox.BackgroundImage, Is.EqualTo(null), "#4a");
        var gif = TestResourceHelper.GetFullPathOfResource ("Test/resources/M.gif");
        textBox.BackgroundImage = Image.FromFile (gif);
        // comparing image objects fails on MS .Net so using Size property
        object expected = Image.FromFile(gif, true).Size;
        Assert.That((object?)textBox.BackgroundImage.Size, Is.EqualTo(expected), "#4b");
			
        Assert.That((object?)textBox.BorderStyle, Is.EqualTo(BorderStyle.Fixed3D));
        Assert.That((object?)textBox.Lines.Length, Is.EqualTo(0));
        Assert.That((object?)textBox.Multiline, Is.EqualTo(true), "#12a");
        Assert.That((object?)textBox.Multiline, Is.EqualTo(true), "#12b");
        Assert.That((object?)textBox.Multiline, Is.EqualTo(true), "#12c");
        Assert.That((object?)textBox.ReadOnly, Is.EqualTo(false));
        textBox.Text = "sample TextBox";
        Assert.That((object?)textBox.Text, Is.EqualTo("sample TextBox"));
    }

    [Test]
    public void TextBoxPropertyTest ()
    {
        Assert.That((object?)textBox.PasswordChar, Is.EqualTo('\0'));
        textBox.PasswordChar = '*';
        Assert.That((object?)textBox.PasswordChar, Is.EqualTo('*'), "#23b");
    }

    [Test]
    public void AppendTextTest ()
    {
        var f = new Form (); 
        f.ShowInTaskbar = false;
        f.Visible = true;
        textBox.Visible = true;
        textBox.Text = "TextBox1";
        var textBox2 = new TextBox ();
        textBox2.Visible = true;
        f.Controls.Add (textBox);
        f.Controls.Add (textBox2);
        textBox2.AppendText (textBox.Text);
        Assert.That((object?)textBox2.Text, Is.EqualTo("TextBox1"));
        f.Dispose ();
    }

    [Test]
    public void AppendTextTest2 ()
    {
        var textBox2 = new TextBox ();
        textBox2.AppendText ("hi");
        textBox2.AppendText ("ho");
        Assert.That((object?)textBox2.Text, Is.EqualTo("hiho"));
        Assert.IsNotNull (textBox2.Lines);
        Assert.That((object?)textBox2.Lines.Length, Is.EqualTo(1));
        Assert.That((object?)textBox2.Lines [0], Is.EqualTo("hiho"));
    }

    [Test]
    public void AppendText_Multiline_CRLF ()
    {
        var textBox = new TextBox ();
        textBox.Text = "ha";
        textBox.AppendText ("hi\r\n\r\n");
        textBox.AppendText ("ho\r\n");
        Assert.That((object?)textBox.Text, Is.EqualTo("hahi\r\n\r\nho\r\n"));
        Assert.IsNotNull (textBox.Lines);
        Assert.That((object?)textBox.Lines.Length, Is.EqualTo(4));
        Assert.That((object?)textBox.Lines [0], Is.EqualTo("hahi"));
        object expected = string.Empty;
        Assert.That((object?)textBox.Lines [1], Is.EqualTo(expected));
        Assert.That((object?)textBox.Lines [2], Is.EqualTo("ho"));
        object expected1 = string.Empty;
        Assert.That((object?)textBox.Lines [3], Is.EqualTo(expected1));

        textBox.Multiline = true;

        textBox.Text = "ha";
        textBox.AppendText ("hi\r\n\r\n");
        textBox.AppendText ("ho\r\n");
        Assert.That((object?)textBox.Text, Is.EqualTo("hahi\r\n\r\nho\r\n"));
        Assert.IsNotNull (textBox.Lines);
        Assert.That((object?)textBox.Lines.Length, Is.EqualTo(4));
        Assert.That((object?)textBox.Lines [0], Is.EqualTo("hahi"));
        object expected2 = string.Empty;
        Assert.That((object?)textBox.Lines [1], Is.EqualTo(expected2));
        Assert.That((object?)textBox.Lines [2], Is.EqualTo("ho"));
        object expected3 = string.Empty;
        Assert.That((object?)textBox.Lines [3], Is.EqualTo(expected3));
    }

    [Test]
    public void AppendText_Multiline_LF ()
    {
        var textBox = new TextBox ();

        textBox.Text = "ha";
        textBox.AppendText ("hi\n\n");
        textBox.AppendText ("ho\n");
        Assert.That((object?)textBox.Text, Is.EqualTo("hahi\n\nho\n"));
        Assert.IsNotNull (textBox.Lines);
        Assert.That((object?)textBox.Lines.Length, Is.EqualTo(4));
        Assert.That((object?)textBox.Lines [0], Is.EqualTo("hahi"));
        object expected = string.Empty;
        Assert.That((object?)textBox.Lines [1], Is.EqualTo(expected));
        Assert.That((object?)textBox.Lines [2], Is.EqualTo("ho"));
        object expected1 = string.Empty;
        Assert.That((object?)textBox.Lines [3], Is.EqualTo(expected1));

        textBox.Multiline = true;

        textBox.Text = "ha";
        textBox.AppendText ("hi\n\n");
        textBox.AppendText ("ho\n");
        Assert.That((object?)textBox.Text, Is.EqualTo("hahi\n\nho\n"));
        Assert.IsNotNull (textBox.Lines);
        Assert.That((object?)textBox.Lines.Length, Is.EqualTo(4));
        Assert.That((object?)textBox.Lines [0], Is.EqualTo("hahi"));
        object expected2 = string.Empty;
        Assert.That((object?)textBox.Lines [1], Is.EqualTo(expected2));
        Assert.That((object?)textBox.Lines [2], Is.EqualTo("ho"));
        object expected3 = string.Empty;
        Assert.That((object?)textBox.Lines [3], Is.EqualTo(expected3));
    }

    [Test]
    public void BackColorTest ()
    {
        Assert.That((object?)textBox.BackColor, Is.EqualTo(SystemColors.Window));
        textBox.BackColor = Color.Red;
        Assert.That((object?)textBox.BackColor, Is.EqualTo(Color.Red));
        textBox.BackColor = Color.White;
        Assert.That((object?)textBox.BackColor, Is.EqualTo(Color.White));
        Assert.That((object?)_invalidated, Is.EqualTo(0));
        Assert.That((object?)_paint, Is.EqualTo(0));

        var form = new Form ();
        form.ShowInTaskbar = false;
        form.Controls.Add (textBox);
        form.Show ();

        _invalidated = 0;
        _paint = 0;
			
        Assert.That((object?)textBox.BackColor, Is.EqualTo(Color.White));
        Assert.That((object?)_invalidated, Is.EqualTo(0));
        Assert.That((object?)_paint, Is.EqualTo(0));
        textBox.BackColor = Color.Red;
        Assert.That((object?)textBox.BackColor, Is.EqualTo(Color.Red));
        Assert.That((object?)_invalidated, Is.EqualTo(1));
        Assert.That((object?)_paint, Is.EqualTo(0));
        textBox.BackColor = Color.Red;
        Assert.That((object?)textBox.BackColor, Is.EqualTo(Color.Red));
        Assert.That((object?)_invalidated, Is.EqualTo(1));
        Assert.That((object?)_paint, Is.EqualTo(0));
        textBox.BackColor = Color.Blue;
        Assert.That((object?)textBox.BackColor, Is.EqualTo(Color.Blue));
        Assert.That((object?)_invalidated, Is.EqualTo(2));
        Assert.That((object?)_paint, Is.EqualTo(0));
        textBox.BackColor = Color.Empty;
        Assert.That((object?)textBox.BackColor, Is.EqualTo(SystemColors.Window));
        Assert.That((object?)_invalidated, Is.EqualTo(3));
        Assert.That((object?)_paint, Is.EqualTo(0));
			
        form.Close ();
    }

    [Test] // bug #80626
    [Ignore ("Depends on default font height")]
    public void BorderStyle_None ()
    {
        textBox.BorderStyle = BorderStyle.None;
        Assert.That((object?)textBox.Height, Is.EqualTo(20));
        textBox.CreateControl ();
        Assert.That((object?)textBox.Height, Is.EqualTo(13));
    }

    [Test]
    public void ClearTest ()
    {
        textBox.Text = "TextBox1";
        Assert.That((object?)textBox.Text, Is.EqualTo("TextBox1"), "#28a");
        textBox.Clear ();
        Assert.That((object?)textBox.Text, Is.EqualTo(string.Empty), "#28b");
    }

    [Test] // bug #80620
    [Ignore ("Depends on default font height")]
    public void ClientRectangle_Borders ()
    {
        textBox.CreateControl ();
        Assert.That((object?)new TextBox ().ClientRectangle, Is.EqualTo(textBox.ClientRectangle));
    }

    [Test]		
    public void ForeColorTest ()
    {
        Assert.That((object?)textBox.ForeColor, Is.EqualTo(SystemColors.WindowText));
        textBox.ForeColor = Color.Red;
        Assert.That((object?)textBox.ForeColor, Is.EqualTo(Color.Red));
        textBox.ForeColor = Color.White;
        Assert.That((object?)textBox.ForeColor, Is.EqualTo(Color.White));
        Assert.That((object?)_invalidated, Is.EqualTo(0));
        Assert.That((object?)_paint, Is.EqualTo(0));

        var form = new Form ();
        form.ShowInTaskbar = false;
        form.Controls.Add (textBox);
        form.Show ();

        Assert.That((object?)textBox.ForeColor, Is.EqualTo(Color.White));
        Assert.That((object?)_invalidated, Is.EqualTo(0));
        Assert.That((object?)_paint, Is.EqualTo(0));
        textBox.ForeColor = Color.Red;
        Assert.That((object?)textBox.ForeColor, Is.EqualTo(Color.Red));
        Assert.That((object?)_invalidated, Is.EqualTo(1));
        Assert.That((object?)_paint, Is.EqualTo(0));
        textBox.ForeColor = Color.Red;
        Assert.That((object?)textBox.ForeColor, Is.EqualTo(Color.Red));
        Assert.That((object?)_invalidated, Is.EqualTo(1));
        Assert.That((object?)_paint, Is.EqualTo(0));
        textBox.ForeColor = Color.Blue;
        Assert.That((object?)textBox.ForeColor, Is.EqualTo(Color.Blue));
        Assert.That((object?)_invalidated, Is.EqualTo(2));
        Assert.That((object?)_paint, Is.EqualTo(0));

        form.Close ();
    }

    [Test]
    public void ReadOnly_BackColor_NotSet ()
    {
        textBox.ReadOnly = true;
        Assert.IsTrue (textBox.ReadOnly);
        Assert.That((object?)textBox.BackColor, Is.EqualTo(SystemColors.Control));

        var form = new Form ();
        form.ShowInTaskbar = false;
        form.Controls.Add (textBox);
        form.Show ();

        Assert.IsTrue (textBox.ReadOnly);
        Assert.That((object?)textBox.BackColor, Is.EqualTo(SystemColors.Control));

        textBox.ResetBackColor ();
        Assert.IsTrue (textBox.ReadOnly);
        Assert.That((object?)textBox.BackColor, Is.EqualTo(SystemColors.Control));

        textBox.ReadOnly = false;
        Assert.IsFalse (textBox.ReadOnly);
        Assert.That((object?)textBox.BackColor, Is.EqualTo(SystemColors.Window));

        textBox.ReadOnly = true;
        Assert.IsTrue (textBox.ReadOnly);
        Assert.That((object?)textBox.BackColor, Is.EqualTo(SystemColors.Control));

        textBox.BackColor = Color.Red;
        Assert.IsTrue (textBox.ReadOnly);
        Assert.That((object?)textBox.BackColor, Is.EqualTo(Color.Red));

        textBox.ReadOnly = false;
        Assert.IsFalse (textBox.ReadOnly);
        Assert.That((object?)textBox.BackColor, Is.EqualTo(Color.Red));

        textBox.ReadOnly = true;
        Assert.IsTrue (textBox.ReadOnly);
        Assert.That((object?)textBox.BackColor, Is.EqualTo(Color.Red));

        textBox.ResetBackColor ();
        Assert.IsTrue (textBox.ReadOnly);
        Assert.That((object?)textBox.BackColor, Is.EqualTo(SystemColors.Control));

        form.Close ();
    }

    [Test]
    public void ReadOnly_BackColor_Set ()
    {
        textBox.BackColor = Color.Blue;
        textBox.ReadOnly = true;
        Assert.IsTrue (textBox.ReadOnly);
        Assert.That((object?)textBox.BackColor, Is.EqualTo(Color.Blue));

        var form = new Form ();
        form.ShowInTaskbar = false;
        form.Controls.Add (textBox);
        form.Show ();

        Assert.IsTrue (textBox.ReadOnly);
        Assert.That((object?)textBox.BackColor, Is.EqualTo(Color.Blue));

        textBox.ReadOnly = false;
        Assert.IsFalse (textBox.ReadOnly);
        Assert.That((object?)textBox.BackColor, Is.EqualTo(Color.Blue));

        textBox.ReadOnly = true;
        Assert.IsTrue (textBox.ReadOnly);
        Assert.That((object?)textBox.BackColor, Is.EqualTo(Color.Blue));

        textBox.BackColor = Color.Red;
        Assert.IsTrue (textBox.ReadOnly);
        Assert.That((object?)textBox.BackColor, Is.EqualTo(Color.Red));

        textBox.ReadOnly = false;
        Assert.IsFalse (textBox.ReadOnly);
        Assert.That((object?)textBox.BackColor, Is.EqualTo(Color.Red));

        textBox.ReadOnly = true;
        textBox.ResetBackColor ();
        Assert.IsTrue (textBox.ReadOnly);
        Assert.That((object?)textBox.BackColor, Is.EqualTo(SystemColors.Control));

        form.Dispose ();

        textBox = new TextBox ();
        textBox.ReadOnly = true;
        textBox.BackColor = Color.Blue;
        Assert.IsTrue (textBox.ReadOnly);
        Assert.That((object?)textBox.BackColor, Is.EqualTo(Color.Blue));

        form = new Form ();
        form.ShowInTaskbar = false;
        form.Controls.Add (textBox);
        form.Show ();

        Assert.IsTrue (textBox.ReadOnly);
        Assert.That((object?)textBox.BackColor, Is.EqualTo(Color.Blue));

        textBox.ReadOnly = false;
        Assert.IsFalse (textBox.ReadOnly);
        Assert.That((object?)textBox.BackColor, Is.EqualTo(Color.Blue));

        textBox.ResetBackColor ();
        Assert.IsFalse (textBox.ReadOnly);
        Assert.That((object?)textBox.BackColor, Is.EqualTo(SystemColors.Window));
			
        form.Close ();
    }

    [Test]
    public void ToStringTest ()
    {
        Assert.That((object?)textBox.ToString(), Is.EqualTo("System.Windows.Forms.TextBox, Text: "));
    }

    [Test] // bug #79851
    public void WrappedText ()
    {
        var text = "blabla blablabalbalbalbalbalbal blabla blablabl bal " +
                   "bal bla bal balajkdhfk dskfk ersd dsfjksdhf sdkfjshd f";

        textBox.Multiline = true;
        textBox.Size = new Size (30, 168);
        textBox.Text = text;

        var form = new Form ();
        form.Controls.Add (textBox);
        form.ShowInTaskbar = false;
        form.Show ();

        Assert.That((object?)textBox.Text, Is.EqualTo(text));
			
        form.Close ();
    }

    [Test] // bug #79909
    public void MultilineText ()
    {
        var text = "line1\n\nline2\nline3\r\nline4";

        textBox.Size = new Size (300, 168);
        textBox.Text = text;

        var form = new Form ();
        form.Controls.Add (textBox);
        form.ShowInTaskbar = false;
        form.Show ();

        Assert.That((object?)textBox.Text, Is.EqualTo(text));

        text = "line1\n\nline2\nline3\r\nline4\rline5\r\n\nline6\n\n\nline7";

        textBox.Text = text;

        form.Visible = false;
        form.Show ();

        Assert.That((object?)textBox.Text, Is.EqualTo(text));
			
        form.Close ();
    }

    [Test]
    public void Bug82749 ()
    {
        var f = new Form ();
        f.ShowInTaskbar = false;

        var _textBox = new TextBox ();
        _textBox.Dock = DockStyle.Top;
        _textBox.Height = 100;
        _textBox.Multiline = true;
        f.Controls.Add (_textBox);
			
        f.Show ();
        Assert.That((object?)_textBox.Height, Is.EqualTo(100));
			
        // Font dependent, but should be less than 30.
        _textBox.Multiline = false;
        Assert.IsTrue (_textBox.Height < 30);

        _textBox.Multiline = true;
        Assert.That((object?)_textBox.Height, Is.EqualTo(100));
			
        f.Close ();
        f.Dispose ();
    }
		
    [Test]
    public void Bug6357 ()
    {
        var f = new Form (); 
        f.ShowInTaskbar = false;
        f.Visible = true;
        f.ClientSize = new Size (300, 130);
        textBox.Visible = true;
        textBox.AppendText(
            "Achtung! Passwort für URL angepasst! Anführungszeichen im Passwort funktionieren in URL nur mit Escape.\r\n" +
            "\r\n" +
            "{S:fileFilepath} -> {S:##volumeDriveLetter}:\\\r\n" +
            "\r\n" +
            "Verschlüsselter Kontainer (VeraCrypt).\r\n" +
            "\r\n" +
            "URL-Anmerkungen:\r\n" +
            "- nur für Windows\r\n" +
            "- volumeDriveLetter muss frei sein\r\n" +
            "\r\n" +
            "veracrypt --mount /media/NAS_container_flo/test.vc -p '1 1' --fs-options=X-mount.mkdir=0700 /media/vera\r\n" +
            "\r\n" +
            "cmd://veracrypt --mount {S:fFilepath} -p '{PASSWORD}' --pim='{S:#pim}' --fs-options=X-mount.mkdir=0700 {S:mPoint}" +
            "\r\n" +
            "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\r\n" +
            "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
        textBox.Multiline = true;
        f.Dispose ();
    }

    private bool modified_changed_fired;

    private void TextBox_ModifiedChanged (object? sender, EventArgs e)
    {
        modified_changed_fired = true;
    }

    private void TextBox_TextChanged (object? sender, EventArgs e)
    {
        _changed++;
    }

    private void TextBox_Invalidated (object? sender, InvalidateEventArgs e)
    {
        _invalidated++;
    }

    private void TextBox_Paint (object? sender, PaintEventArgs e)
    {
        _paint++;
    }

    private void Reset ()
    {
        _changed = 0;
        _invalidated = 0;
        _paint = 0;
    }

}
