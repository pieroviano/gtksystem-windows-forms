//
// FormTest.cs: Test cases for Form.
//
// Author:
//   Ritvik Mayank (mritvik@novell.com)
//
// (C) 2005 Novell, Inc. (http://www.novell.com)
//

using GtkTests.Helpers;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CategoryAttribute = NUnit.Framework.CategoryAttribute;
using Timer = System.Windows.Forms.Timer;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class FormTest : TestHelper
{
    [Test]
    public void bug_82358()
    {
        //Console.WriteLine ("Starting bug_82358");

        var sizeable_factor =
            // WinXP, default theme
            2;
        var title_bar = 26;
        var tool_bar = 18;
        var tool_border = 6;
        var d3 = 10;
        var d2 = 6;

        // WinXP, Win32 theme:
        sizeable_factor = 2;
        title_bar = 19;
        tool_bar = 16;
        tool_border = 6;
        d3 = 10;
        d2 = 6;


        var size = new Size(200, 200);

        // Universal theme??
        using (var f = new Form())
        {
            f.FormBorderStyle = FormBorderStyle.FixedSingle;
            f.Visible = true;
            d2 = f.Size.Width - f.ClientSize.Width;
            title_bar = f.Size.Height - f.ClientSize.Height - d2;
        }
        using (var f = new Form())
        {
            f.FormBorderStyle = FormBorderStyle.Sizable;
            f.Visible = true;
            sizeable_factor = f.Size.Width - f.ClientSize.Width - d2;
        }
        using (var f = new Form())
        {
            f.ClientSize = size;
            f.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            //f.Visible = true;
            tool_border = f.Size.Width - f.ClientSize.Width;
            tool_bar = f.Size.Height - f.ClientSize.Height - tool_border;
        }
        using (var f = new Form())
        {
            f.FormBorderStyle = FormBorderStyle.Fixed3D;
            f.Visible = true;
            d3 = f.Size.Width - f.ClientSize.Width;
        }

        FormBorderStyle style;


        //Console.WriteLine ("Universal theme says: d2={0}, d3={1}, title_bar={2}, sizeable_factor={3}, tool_border={4}, tool_bar={5}", d2, d3, title_bar, sizeable_factor, tool_border, tool_bar);

        // Changing client size, then FormBorderStyle.
        using (var f = new Form())
        {
            style = FormBorderStyle.FixedToolWindow;
            //Console.WriteLine ("Created form, size: {0}, clientsize: {1}", f.Size, f.ClientSize);
            f.ClientSize = size;
            //Console.WriteLine ("Changed ClientSize, size: {0}, clientsize: {1}", f.Size, f.ClientSize);
            f.FormBorderStyle = style;
            //Console.WriteLine ("Changed FormBorderStyle, size: {0}, clientsize: {1}", f.Size, f.ClientSize);
            var message = style.ToString() + "-A1";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width + tool_border, size.Height + tool_border + tool_bar).ToString();
            var message1 = style.ToString() + "-A2";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            //Console.WriteLine ("Made visible, size: {0}, clientsize: {1}", f.Size, f.ClientSize);
            var message2 = style.ToString() + "-A3";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width + tool_border, size.Height + tool_border + tool_bar).ToString();
            var message3 = style.ToString() + "-A4";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected1), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.SizableToolWindow;
            f.ClientSize = size;
            f.FormBorderStyle = style;
            var message = style.ToString() + "-A1";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width + tool_border + sizeable_factor, size.Height + tool_border + tool_bar + sizeable_factor).ToString();
            var message1 = style.ToString() + "-A2";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-A3";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width + tool_border + sizeable_factor, size.Height + tool_border + tool_bar + sizeable_factor).ToString();
            var message3 = style.ToString() + "-A4";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected1), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.Fixed3D;
            f.ClientSize = size;
            f.FormBorderStyle = style;
            var message = style.ToString() + "-A1";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width + d3, size.Height + title_bar + d3).ToString();
            var message1 = style.ToString() + "-A2";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-A3";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width + d3, size.Height + title_bar + d3).ToString();
            var message3 = style.ToString() + "-A4";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected1), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.FixedDialog;
            f.ClientSize = size;
            f.FormBorderStyle = style;
            var message = style.ToString() + "-A1";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width + d2, size.Height + title_bar + d2).ToString();
            var message1 = style.ToString() + "-A2";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-A3";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width + d2, size.Height + title_bar + d2).ToString();
            var message3 = style.ToString() + "-A4";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected1), message3);

        }

        using (var f = new Form())
        {
            style = FormBorderStyle.FixedSingle;
            f.ClientSize = size;
            f.FormBorderStyle = style;
            var message = style.ToString() + "-A1";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width + d2, size.Height + title_bar + d2).ToString();
            var message1 = style.ToString() + "-A2";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-A3";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width + d2, size.Height + title_bar + d2).ToString();
            var message3 = style.ToString() + "-A4";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected1), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.None;
            f.ClientSize = size;
            f.FormBorderStyle = style;
            var message = style.ToString() + "-A1";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()));
            var message1 = style.ToString() + "-A2";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-A3";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()), message2);
            var message3 = style.ToString() + "-A4";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.Sizable;
            f.ClientSize = size;
            f.FormBorderStyle = style;
            var message = style.ToString() + "-A1";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width + d2 + sizeable_factor, size.Height + title_bar + d2 + sizeable_factor).ToString();
            var message1 = style.ToString() + "-A2";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-A3";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width + d2 + sizeable_factor, size.Height + title_bar + d2 + sizeable_factor).ToString();
            var message3 = style.ToString() + "-A4";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected1), message3);
        }


        // Changing size, then FormBorderStyle.
        using (var f = new Form())
        {
            style = FormBorderStyle.FixedToolWindow;
            f.Size = size;
            f.FormBorderStyle = style;
            var message = style.ToString() + "-B1";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message1 = style.ToString() + "-B2";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-B3";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width - tool_border, size.Height - tool_border - tool_bar).ToString();
            var message3 = style.ToString() + "-B4";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected1), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.SizableToolWindow;
            f.Size = size;
            f.FormBorderStyle = style;
            var message = style.ToString() + "-B1";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message1 = style.ToString() + "-B2";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-B3";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width - tool_border - sizeable_factor, size.Height - tool_border - tool_bar - sizeable_factor).ToString();
            var message3 = style.ToString() + "-B4";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected1), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.Fixed3D;
            f.Size = size;
            f.FormBorderStyle = style;
            var message = style.ToString() + "-B1";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message1 = style.ToString() + "-B2";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-B3";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width - d3, size.Height - title_bar - d3).ToString();
            var message3 = style.ToString() + "-B4";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected1), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.FixedDialog;
            f.Size = size;
            f.FormBorderStyle = style;
            var message = style.ToString() + "-B1";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message1 = style.ToString() + "-B2";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-B3";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width - d2, size.Height - title_bar - d2).ToString();
            var message3 = style.ToString() + "-B4";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected1), message3);

        }

        using (var f = new Form())
        {
            style = FormBorderStyle.FixedSingle;
            f.Size = size;
            f.FormBorderStyle = style;
            var message = style.ToString() + "-B1";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message1 = style.ToString() + "-B2";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-B3";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width - d2, size.Height - title_bar - d2).ToString();
            var message3 = style.ToString() + "-B4";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected1), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.None;
            f.Size = size;
            f.FormBorderStyle = style;
            var message = style.ToString() + "-B1";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message1 = style.ToString() + "-B2";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-B3";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()), message2);
            var message3 = style.ToString() + "-B4";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.Sizable;
            f.Size = size;
            f.FormBorderStyle = style;
            var message = style.ToString() + "-B1";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message1 = style.ToString() + "-B2";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-B3";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message3 = style.ToString() + "-B4";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected1), message3);
        }



        // Changing FormBorderStyle, then client size
        using (var f = new Form())
        {
            style = FormBorderStyle.FixedToolWindow;
            f.FormBorderStyle = style;
            f.ClientSize = size;
            var message = style.ToString() + "-C1";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width + tool_border, size.Height + tool_border + tool_bar).ToString();
            var message1 = style.ToString() + "-C2";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-C3";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width + tool_border, size.Height + tool_border + tool_bar).ToString();
            var message3 = style.ToString() + "-C4";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected1), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.SizableToolWindow;
            f.FormBorderStyle = style;
            f.ClientSize = size;
            var message = style.ToString() + "-C1";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width + tool_border + sizeable_factor, size.Height + tool_border + tool_bar + sizeable_factor).ToString();
            var message1 = style.ToString() + "-C2";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-C3";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width + tool_border + sizeable_factor, size.Height + tool_border + tool_bar + sizeable_factor).ToString();
            var message3 = style.ToString() + "-C4";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected1), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.Fixed3D;
            f.FormBorderStyle = style;
            f.ClientSize = size;
            var message = style.ToString() + "-C1";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width + d3, size.Height + title_bar + d3).ToString();
            var message1 = style.ToString() + "-C2";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-C3";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width + d3, size.Height + title_bar + d3).ToString();
            var message3 = style.ToString() + "-C4";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected1), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.FixedDialog;
            f.FormBorderStyle = style;
            f.ClientSize = size;
            var message = style.ToString() + "-C1";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width + d2, size.Height + title_bar + d2).ToString();
            var message1 = style.ToString() + "-C2";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-C3";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width + d2, size.Height + title_bar + d2).ToString();
            var message3 = style.ToString() + "-C4";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected1), message3);

        }

        using (var f = new Form())
        {
            style = FormBorderStyle.FixedSingle;
            f.FormBorderStyle = style;
            f.ClientSize = size;
            var message = style.ToString() + "-C1";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width + d2, size.Height + title_bar + d2).ToString();
            var message1 = style.ToString() + "-C2";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-C3";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width + d2, size.Height + title_bar + d2).ToString();
            var message3 = style.ToString() + "-C4";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected1), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.None;
            f.FormBorderStyle = style;
            f.ClientSize = size;
            var message = style.ToString() + "-C1";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()));
            var message1 = style.ToString() + "-C2";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-C3";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()), message2);
            var message3 = style.ToString() + "-C4";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.Sizable;
            f.FormBorderStyle = style;
            f.ClientSize = size;
            var message = style.ToString() + "-C1";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width + d2 + sizeable_factor, size.Height + title_bar + d2 + sizeable_factor).ToString();
            var message1 = style.ToString() + "-C2";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-C3";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width + d2 + sizeable_factor, size.Height + title_bar + d2 + sizeable_factor).ToString();
            var message3 = style.ToString() + "-C4";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected1), message3);
        }


        // Changing FormBorderStyle, then size
        using (var f = new Form())
        {
            style = FormBorderStyle.FixedToolWindow;
            f.FormBorderStyle = style;
            f.Size = size;
            var message = style.ToString() + "-D1";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width - tool_border, size.Height - tool_border - tool_bar).ToString();
            var message1 = style.ToString() + "-D2";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-D3";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width - tool_border, size.Height - tool_border - tool_bar).ToString();
            var message3 = style.ToString() + "-D4";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected1), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.SizableToolWindow;
            f.FormBorderStyle = style;
            f.Size = size;
            var message = style.ToString() + "-D1";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width - tool_border - sizeable_factor, size.Height - tool_border - tool_bar - sizeable_factor).ToString();
            var message1 = style.ToString() + "-D2";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-D3";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width - tool_border - sizeable_factor, size.Height - tool_border - tool_bar - sizeable_factor).ToString();
            var message3 = style.ToString() + "-D4";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected1), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.Fixed3D;
            f.FormBorderStyle = style;
            f.Size = size;
            var message = style.ToString() + "-D1";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width - d3, size.Height - title_bar - d3).ToString();
            var message1 = style.ToString() + "-D2";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-D3";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width - d3, size.Height - title_bar - d3).ToString();
            var message3 = style.ToString() + "-D4";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected1), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.FixedDialog;
            f.FormBorderStyle = style;
            f.Size = size;
            var message = style.ToString() + "-D1";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width - d2, size.Height - title_bar - d2).ToString();
            var message1 = style.ToString() + "-D2";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-D3";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width - d2, size.Height - title_bar - d2).ToString();
            var message3 = style.ToString() + "-D4";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected1), message3);

        }

        using (var f = new Form())
        {
            style = FormBorderStyle.FixedSingle;
            f.FormBorderStyle = style;
            f.Size = size;
            var message = style.ToString() + "-D1";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width - d2, size.Height - title_bar - d2).ToString();
            var message1 = style.ToString() + "-D2";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-D3";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width - d2, size.Height - title_bar - d2).ToString();
            var message3 = style.ToString() + "-D4";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected1), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.None;
            f.FormBorderStyle = style;
            f.Size = size;
            var message = style.ToString() + "-D1";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()));
            var message1 = style.ToString() + "-D2";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-D3";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(size.ToString()), message2);
            var message3 = style.ToString() + "-D4";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.Sizable;
            f.FormBorderStyle = style;
            f.Size = size;
            var message = style.ToString() + "-D1";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()));
            object expected = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message1 = style.ToString() + "-D2";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected), message1);
            f.Visible = true;
            var message2 = style.ToString() + "-D3";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(size.ToString()), message2);
            object expected1 = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message3 = style.ToString() + "-D4";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected1), message3);
        }



        // Set clientsize, then change size, then FormBorderStyle.
        using (var f = new Form())
        {
            style = FormBorderStyle.FixedToolWindow;
            f.ClientSize = f.ClientSize;
            f.Size = size;
            f.FormBorderStyle = style;
            // Here we subtract the Sizable borders (default) then add FixedToolWindow's border.
            // Note how now the sizes doesn't change when creating the handle.
            object expected = new Size(size.Width - d2 - sizeable_factor + tool_border, size.Height - title_bar - d2 - sizeable_factor + tool_border + tool_bar).ToString();
            var message = style.ToString() + "-E1";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected));
            object expected1 = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message1 = style.ToString() + "-E2";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected1), message1);
            f.Visible = true;
            object expected2 = new Size(size.Width - d2 - sizeable_factor + tool_border, size.Height - title_bar - d2 - sizeable_factor + tool_border + tool_bar).ToString();
            var message2 = style.ToString() + "-E3";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected2), message2);
            object expected3 = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message3 = style.ToString() + "-E4";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected3), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.SizableToolWindow;
            f.ClientSize = f.ClientSize;
            f.Size = size;
            f.FormBorderStyle = style;
            object expected = new Size(size.Width - d2 - sizeable_factor + tool_border + sizeable_factor, size.Height - title_bar - d2 - sizeable_factor + tool_border + tool_bar + sizeable_factor).ToString();
            var message = style.ToString() + "-E1";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected));
            object expected1 = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message1 = style.ToString() + "-E2";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected1), message1);
            f.Visible = true;
            object expected2 = new Size(size.Width - d2 - sizeable_factor + tool_border + sizeable_factor, size.Height - title_bar - d2 - sizeable_factor + tool_border + tool_bar + sizeable_factor).ToString();
            var message2 = style.ToString() + "-E3";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected2), message2);
            object expected3 = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message3 = style.ToString() + "-E4";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected3), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.Fixed3D;
            f.ClientSize = f.ClientSize;
            f.Size = size;
            f.FormBorderStyle = style;
            object expected = new Size(size.Width - d2 - sizeable_factor + d3, size.Height - title_bar - d2 - sizeable_factor + title_bar + d3).ToString();
            var message = style.ToString() + "-E1";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected));
            object expected1 = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message1 = style.ToString() + "-E2";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected1), message1);
            f.Visible = true;
            object expected2 = new Size(size.Width - d2 - sizeable_factor + d3, size.Height - title_bar - d2 - sizeable_factor + title_bar + d3).ToString();
            var message2 = style.ToString() + "-E3";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected2), message2);
            object expected3 = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message3 = style.ToString() + "-E4";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected3), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.FixedDialog;
            f.ClientSize = f.ClientSize;
            f.Size = size;
            f.FormBorderStyle = style;
            object expected = new Size(size.Width - d2 - sizeable_factor + d2, size.Height - title_bar - d2 - sizeable_factor + title_bar + d2).ToString();
            var message = style.ToString() + "-E1";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected));
            object expected1 = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message1 = style.ToString() + "-E2";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected1), message1);
            f.Visible = true;
            object expected2 = new Size(size.Width - d2 - sizeable_factor + d2, size.Height - title_bar - d2 - sizeable_factor + title_bar + d2).ToString();
            var message2 = style.ToString() + "-E3";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected2), message2);
            object expected3 = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message3 = style.ToString() + "-E4";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected3), message3);

        }

        using (var f = new Form())
        {
            style = FormBorderStyle.FixedSingle;
            f.ClientSize = f.ClientSize;
            f.Size = size;
            f.FormBorderStyle = style;
            object expected = new Size(size.Width - d2 - sizeable_factor + d2, size.Height - title_bar - d2 - sizeable_factor + title_bar + d2).ToString();
            var message = style.ToString() + "-E1";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected));
            object expected1 = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message1 = style.ToString() + "-E2";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected1), message1);
            f.Visible = true;
            object expected2 = new Size(size.Width - d2 - sizeable_factor + d2, size.Height - title_bar - d2 - sizeable_factor + title_bar + d2).ToString();
            var message2 = style.ToString() + "-E3";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected2), message2);
            object expected3 = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message3 = style.ToString() + "-E4";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected3), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.None;
            f.ClientSize = f.ClientSize;
            f.Size = size;
            f.FormBorderStyle = style;
            object expected = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message = style.ToString() + "-E1";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected));
            object expected1 = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message1 = style.ToString() + "-E2";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected1), message1);
            f.Visible = true;
            object expected2 = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message2 = style.ToString() + "-E3";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected2), message2);
            object expected3 = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message3 = style.ToString() + "-E4";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected3), message3);
        }

        using (var f = new Form())
        {
            style = FormBorderStyle.Sizable;
            f.ClientSize = f.ClientSize;
            f.Size = size;
            f.FormBorderStyle = style;
            object expected = new Size(size.Width - d2 - sizeable_factor + d2 + sizeable_factor, size.Height - title_bar - d2 - sizeable_factor + d2 + sizeable_factor + title_bar).ToString();
            var message = style.ToString() + "-E1";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected));
            object expected1 = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message1 = style.ToString() + "-E2";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected1), message1);
            f.Visible = true;
            object expected2 = new Size(size.Width - d2 - sizeable_factor + d2 + sizeable_factor, size.Height - title_bar - d2 - sizeable_factor + d2 + sizeable_factor + title_bar).ToString();
            var message2 = style.ToString() + "-E3";
            Assert.That((object?)f.Size.ToString(), Is.EqualTo(expected2), message2);
            object expected3 = new Size(size.Width - d2 - sizeable_factor, size.Height - title_bar - d2 - sizeable_factor).ToString();
            var message3 = style.ToString() + "-E4";
            Assert.That((object?)f.ClientSize.ToString(), Is.EqualTo(expected3), message3);
        }




    }

    [Test] // bug 81969
    public void StartPositionClosedForm()
    {
        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.CenterParent;
            form.Load += CenterDisposedForm_Load;
            form.Show();
        }

        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Load += CenterDisposedForm_Load;
            form.Show();
        }


        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.Manual;
            form.Load += CenterDisposedForm_Load;
            form.Show();
        }


        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.WindowsDefaultBounds;
            form.Load += CenterDisposedForm_Load;
            form.Show();
        }

        using (var form = new Form())
        {
            form.StartPosition = FormStartPosition.WindowsDefaultLocation;
            form.Load += CenterDisposedForm_Load;
            form.Show();
        }
    }

    private void CenterDisposedForm_Load(object? sender, EventArgs e)
    {
        ((Form)sender!).Close();
    }

    private void Form_VisibleChanged1(object? sender, EventArgs e)
    {
        var f = (TimeBombedForm)sender!;
        f.Reason = "VisibleChanged";
        f.Visible = false;
    }

    private void Form_VisibleChanged2(object? sender, EventArgs e)
    {
        var f = (TimeBombedForm)sender!;
        f.Reason = "VisibleChanged";
        f.Visible = false;
        f.DialogResult = DialogResult.OK;
        Assert.IsFalse(f.Visible);
    }

    [Test]
    [Category("NotWorking")]
    public void FormStartupPositionChangeTest()
    {
        using var frm = new Form();
        frm.ShowInTaskbar = false;
        frm.StartPosition = FormStartPosition.Manual;
        frm.Location = new Point(0, 0);
        frm.Show();

        // On X there seem to be pending messages in the queue aren't processed
        // before Show returns, so process them. Otherwise the Location returns
        // something like (5,23)
        Application.DoEvents();

        Assert.That((object?)frm.Location.ToString(), Is.EqualTo("{X=0,Y=0}"));

        frm.StartPosition = FormStartPosition.CenterParent;
        Assert.That((object?)frm.Location.ToString(), Is.EqualTo("{X=0,Y=0}"));

        frm.StartPosition = FormStartPosition.CenterScreen;
        Assert.That((object?)frm.Location.ToString(), Is.EqualTo("{X=0,Y=0}"));

        frm.StartPosition = FormStartPosition.Manual;
        Assert.That((object?)frm.Location.ToString(), Is.EqualTo("{X=0,Y=0}"));

        frm.StartPosition = FormStartPosition.WindowsDefaultBounds;
        Assert.That((object?)frm.Location.ToString(), Is.EqualTo("{X=0,Y=0}"));

        frm.StartPosition = FormStartPosition.WindowsDefaultLocation;
        Assert.That((object?)frm.Location.ToString(), Is.EqualTo("{X=0,Y=0}"));
    }

    [Test]
    public void FormStartupPositionTest()
    {
        CreateParams cp;

        using (var frm = new Form())
        {
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultLocation), "$01");
            object expected = new Point(int.MinValue, int.MinValue).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected));

            frm.StartPosition = FormStartPosition.CenterParent;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.CenterParent), "$01");
            object expected1 = new Point(int.MinValue, int.MinValue).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected1));

            frm.StartPosition = FormStartPosition.CenterScreen;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.CenterScreen), "$01");

            frm.StartPosition = FormStartPosition.Manual;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.Manual), "$01");
            object expected2 = new Point(0, 0).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected2));

            frm.StartPosition = FormStartPosition.WindowsDefaultBounds;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultBounds), "$01");
            object expected3 = new Point(int.MinValue, int.MinValue).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected3));

            frm.StartPosition = FormStartPosition.WindowsDefaultLocation;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultLocation), "$01");
            object expected4 = new Point(int.MinValue, int.MinValue).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected4));

        }


        using (var frm = new Form())
        {
            frm.Location = new Point(23, 45);

            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultLocation), "$A1");
            object expected = new Point(int.MinValue, int.MinValue).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected));

            frm.StartPosition = FormStartPosition.CenterParent;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.CenterParent), "$A2");
            object expected1 = new Point(int.MinValue, int.MinValue).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected1));

            frm.StartPosition = FormStartPosition.CenterScreen;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.CenterScreen), "$A3");

            frm.StartPosition = FormStartPosition.Manual;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.Manual), "$A4");
            object expected2 = new Point(23, 45).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected2));

            frm.StartPosition = FormStartPosition.WindowsDefaultBounds;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultBounds), "$A5");
            object expected3 = new Point(int.MinValue, int.MinValue).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected3));

            frm.StartPosition = FormStartPosition.WindowsDefaultLocation;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultLocation), "$A6");
            object expected4 = new Point(int.MinValue, int.MinValue).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected4));
        }
    }

    [Test]
    public void ParentedFormStartupPositionTest()
    {
        CreateParams cp;
        using var Main = new Form();
        Main.ShowInTaskbar = false;
        Main.Show();

        using (var frm = new Form())
        {
            Main.Controls.Add(frm);
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultLocation), "$01");
            object expected = new Point(0, 0).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected));

            frm.StartPosition = FormStartPosition.CenterParent;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.CenterParent), "$02");
            object expected1 = new Point(0, 0).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected1));

            frm.StartPosition = FormStartPosition.CenterScreen;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.CenterScreen), "$03");
            object expected2 = new Point(0, 0).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected2));

            frm.StartPosition = FormStartPosition.Manual;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.Manual), "$04");
            object expected3 = new Point(0, 0).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected3));

            frm.StartPosition = FormStartPosition.WindowsDefaultBounds;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultBounds), "$05");
            object expected4 = new Point(0, 0).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected4));

            frm.StartPosition = FormStartPosition.WindowsDefaultLocation;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultLocation), "$06");
            object expected5 = new Point(0, 0).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected5));
            frm.Show();
        }

        using (var frm = new Form())
        {
            Main.Controls.Add(frm);
            frm.Location = new Point(23, 45);

            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultLocation), "$A1");
            object expected = new Point(23, 45).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected));

            frm.StartPosition = FormStartPosition.CenterParent;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.CenterParent), "$A2");
            object expected1 = new Point(23, 45).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected1));

            frm.StartPosition = FormStartPosition.CenterScreen;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.CenterScreen), "$A3");
            object expected2 = new Point(23, 45).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected2));

            frm.StartPosition = FormStartPosition.Manual;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.Manual), "$A4");
            object expected3 = new Point(23, 45).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected3));

            frm.StartPosition = FormStartPosition.WindowsDefaultBounds;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultBounds), "$A5");
            object expected4 = new Point(23, 45).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected4));

            frm.StartPosition = FormStartPosition.WindowsDefaultLocation;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultLocation), "$A6");
            object expected5 = new Point(23, 45).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected5));

            frm.Show();
        }

        using (var frm = new Form())
        {
            Main.Controls.Add(frm);
            frm.Location = new Point(34, 56);

            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultLocation), "$B1");
            object expected = new Point(34, 56).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected));

            frm.StartPosition = FormStartPosition.CenterParent;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.CenterParent), "$B2");
            object expected1 = new Point(34, 56).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected1));

            frm.StartPosition = FormStartPosition.CenterScreen;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.CenterScreen), "$B3");
            object expected2 = new Point(34, 56).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected2));

            frm.StartPosition = FormStartPosition.Manual;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.Manual), "$B4");
            object expected3 = new Point(34, 56).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected3));

            frm.StartPosition = FormStartPosition.WindowsDefaultBounds;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultBounds), "$B5");
            object expected4 = new Point(34, 56).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected4));

            frm.StartPosition = FormStartPosition.WindowsDefaultLocation;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultLocation), "$B6");
            object expected5 = new Point(34, 56).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected5));

            frm.Show();
        }

        Main.Size = new Size(600, 600);
        using (var frm = new Form())
        {
            Main.Controls.Add(frm);
            frm.Location = new Point(34, 56);

            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultLocation), "$C1");
            object expected = new Point(34, 56).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected));

            frm.StartPosition = FormStartPosition.CenterParent;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.CenterParent), "$C2");
            object expected1 = new Point(34, 56).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected1));

            frm.StartPosition = FormStartPosition.CenterScreen;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.CenterScreen), "$C3");
            object expected2 = new Point(34, 56).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected2));

            frm.StartPosition = FormStartPosition.Manual;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.Manual), "$C4");
            object expected3 = new Point(34, 56).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected3));

            frm.StartPosition = FormStartPosition.WindowsDefaultBounds;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultBounds), "$C5");
            object expected4 = new Point(34, 56).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected4));

            frm.StartPosition = FormStartPosition.WindowsDefaultLocation;
            cp = GetCreateParams(frm);
            Assert.That((object?)frm.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultLocation), "$C6");
            object expected5 = new Point(34, 56).ToString();
            Assert.That((object?)new Point(cp.X, cp.Y).ToString(), Is.EqualTo(expected5));

            frm.Show();
        }
    }

    [Test]
    public void UnparentForm()
    {
        var f1 = new Form();
        f1.Show();

        var f2 = new Form();
        f2.Parent = f1;
        Assert.That((object?)f2.Parent, Is.SameAs(f1));
        f2.Show();
        f2.Parent = null;
        Assert.IsNull(f2.Parent);

        f1.Close();
        f2.Close();
    }

    [Test] // bug #80791
    public void ClientSizeTest()
    {
        var form = new Form();
        Assert.IsFalse(form.ClientSize == form.Size);
    }

    [Test] // bug #80574
    [Category("NotWorking")]
    public void FormBorderStyleTest()
    {
        var form = new Form();
        var boundsBeforeBorderStyleChange = form.Bounds;
        var clientRectangleBeforeBorderStyleChange = form.ClientRectangle;
        form.FormBorderStyle = FormBorderStyle.None;
        Assert.That((object?)boundsBeforeBorderStyleChange, Is.EqualTo(form.Bounds));
        Assert.That((object?)clientRectangleBeforeBorderStyleChange, Is.EqualTo(form.ClientRectangle));

        form.Visible = true;
        form.FormBorderStyle = FormBorderStyle.Sizable;
        boundsBeforeBorderStyleChange = form.Bounds;
        clientRectangleBeforeBorderStyleChange = form.ClientRectangle;
        form.FormBorderStyle = FormBorderStyle.None;
        Assert.IsFalse(form.Bounds == boundsBeforeBorderStyleChange);
        Assert.That((object?)clientRectangleBeforeBorderStyleChange, Is.EqualTo(form.ClientRectangle));

        form.Visible = false;
        form.FormBorderStyle = FormBorderStyle.Sizable;
        boundsBeforeBorderStyleChange = form.Bounds;
        clientRectangleBeforeBorderStyleChange = form.ClientRectangle;
        form.FormBorderStyle = FormBorderStyle.None;
        Assert.IsFalse(form.Bounds == boundsBeforeBorderStyleChange);
        Assert.That((object?)clientRectangleBeforeBorderStyleChange, Is.EqualTo(form.ClientRectangle));
    }

    [Test]
    [Category("NotWorking")]
    public void FormCreateParamsStyleTest()
    {
        Form frm;

        using (frm = new Form())
        {
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TILEDWINDOW | WindowStyles.WS_CLIPCHILDREN), "#01-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#01-ExStyle");
        }

        using (frm = new Form())
        {
            frm.AllowDrop = !frm.AllowDrop;
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TILEDWINDOW | WindowStyles.WS_CLIPCHILDREN), "#02-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#02-ExStyle");
        }

        using (frm = new Form())
        {
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TILEDWINDOW | WindowStyles.WS_CLIPCHILDREN), "#03-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW | WindowExStyles.WS_EX_LAYERED), "#03-ExStyle");
        }

        using (frm = new Form())
        {
            frm.Opacity = 0.50;
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TILEDWINDOW | WindowStyles.WS_CLIPCHILDREN), "#04-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW | WindowExStyles.WS_EX_LAYERED), "#04-ExStyle");
        }

        using (frm = new Form())
        {
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TILEDWINDOW | WindowStyles.WS_CLIPCHILDREN), "#05-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW | WindowExStyles.WS_EX_LAYERED), "#05-ExStyle");
        }

        using (frm = new Form())
        {
            frm.CausesValidation = !frm.CausesValidation;
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TILEDWINDOW | WindowStyles.WS_CLIPCHILDREN), "#06-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#06-ExStyle");
        }

        using (frm = new Form())
        {
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TABSTOP | WindowStyles.WS_GROUP | WindowStyles.WS_THICKFRAME | WindowStyles.WS_BORDER | WindowStyles.WS_CLIPCHILDREN), "#07-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#07-ExStyle");
        }

        using (frm = new Form())
        {
            frm.Enabled = true;
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TILEDWINDOW | WindowStyles.WS_CLIPCHILDREN), "#08-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#08-ExStyle");
        }

        using (frm = new Form())
        {
            frm.FormBorderStyle = FormBorderStyle.Fixed3D;
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TABSTOP | WindowStyles.WS_GROUP | WindowStyles.WS_SYSMENU | WindowStyles.WS_CAPTION | WindowStyles.WS_CLIPCHILDREN), "#10-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CLIENTEDGE | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#10-ExStyle");
        }

        using (frm = new Form())
        {
            frm.FormBorderStyle = FormBorderStyle.FixedDialog;
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TABSTOP | WindowStyles.WS_GROUP | WindowStyles.WS_SYSMENU | WindowStyles.WS_CAPTION | WindowStyles.WS_CLIPCHILDREN), "#11-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_DLGMODALFRAME | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#11-ExStyle");
        }

        using (frm = new Form())
        {
            frm.FormBorderStyle = FormBorderStyle.FixedSingle;
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TABSTOP | WindowStyles.WS_GROUP | WindowStyles.WS_SYSMENU | WindowStyles.WS_CAPTION | WindowStyles.WS_CLIPCHILDREN), "#12-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#12-ExStyle");
        }

        using (frm = new Form())
        {
            frm.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TABSTOP | WindowStyles.WS_GROUP | WindowStyles.WS_SYSMENU | WindowStyles.WS_CAPTION | WindowStyles.WS_CLIPCHILDREN), "#13-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_TOOLWINDOW | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#13-ExStyle");
        }

        using (frm = new Form())
        {
            frm.FormBorderStyle = FormBorderStyle.None;
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TABSTOP | WindowStyles.WS_CLIPCHILDREN), "#14-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#14-ExStyle");
        }

        using (frm = new Form())
        {
            frm.FormBorderStyle = FormBorderStyle.Sizable;
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TILEDWINDOW | WindowStyles.WS_CLIPCHILDREN), "#15-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#15-ExStyle");
        }

        using (frm = new Form())
        {
            frm.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TILEDWINDOW | WindowStyles.WS_CLIPCHILDREN), "#16-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_TOOLWINDOW | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#16-ExStyle");
        }

        using (frm = new Form())
        {
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TILEDWINDOW | WindowStyles.WS_CLIPCHILDREN), "#17-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#17-ExStyle");
        }

        using (frm = new Form())
        {
            frm.Icon = null;
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TILEDWINDOW | WindowStyles.WS_CLIPCHILDREN), "#18-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#18-ExStyle");
        }

        using (frm = new Form())
        {
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TILEDWINDOW | WindowStyles.WS_CLIPCHILDREN), "#19-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#19-ExStyle");
        }

        using (frm = new Form())
        {
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TILEDWINDOW | WindowStyles.WS_CLIPCHILDREN), "#20-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#20-ExStyle");
        }

        using (frm = new Form())
        {
            frm.MaximizeBox = !frm.MaximizeBox;
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_GROUP | WindowStyles.WS_THICKFRAME | WindowStyles.WS_SYSMENU | WindowStyles.WS_CAPTION | WindowStyles.WS_CLIPCHILDREN), "#21-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#21-ExStyle");
        }

        using (frm = new Form())
        {
            frm.MinimizeBox = !frm.MinimizeBox;
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TABSTOP | WindowStyles.WS_THICKFRAME | WindowStyles.WS_SYSMENU | WindowStyles.WS_CAPTION | WindowStyles.WS_CLIPCHILDREN), "#22-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#22-ExStyle");
        }

        using (frm = new Form())
        {
            frm.ShowIcon = !frm.ShowIcon;
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TILEDWINDOW | WindowStyles.WS_CLIPCHILDREN), "#23-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_DLGMODALFRAME | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#23-ExStyle");
        }

        using (frm = new Form())
        {
            frm.ShowInTaskbar = !frm.ShowInTaskbar;
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TILEDWINDOW | WindowStyles.WS_CLIPCHILDREN), "#24-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CONTROLPARENT), "#24-ExStyle");
        }


        using (frm = new Form())
        {
            frm.TabStop = !frm.TabStop;
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TILEDWINDOW | WindowStyles.WS_CLIPCHILDREN), "#25-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#25-ExStyle");
        }

        using (frm = new Form())
        {
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TILEDWINDOW | WindowStyles.WS_CLIPCHILDREN | WindowStyles.WS_CLIPSIBLINGS | WindowStyles.WS_CHILD), "#26-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#26-ExStyle");
        }

        using (frm = new Form())
        {
            frm.Visible = !frm.Visible;
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TILEDWINDOW | WindowStyles.WS_CLIPCHILDREN), "#27-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#27-ExStyle");
        }

        using (frm = new Form())
        {
            frm.Text = string.Empty;
            Assert.That((object?)((WindowStyles)GetCreateParams(frm).Style), Is.EqualTo(WindowStyles.WS_TILED | WindowStyles.WS_TABSTOP | WindowStyles.WS_GROUP | WindowStyles.WS_THICKFRAME | WindowStyles.WS_BORDER | WindowStyles.WS_CLIPCHILDREN), "#28-Style");
            Assert.That((object?)((WindowExStyles)GetCreateParams(frm).ExStyle), Is.EqualTo(WindowExStyles.WS_EX_LEFT | WindowExStyles.WS_EX_RIGHTSCROLLBAR | WindowExStyles.WS_EX_CONTROLPARENT | WindowExStyles.WS_EX_APPWINDOW), "#28-ExStyle");
        }
    }

    [Test]
    public void FormPropertyTest()
    {
        var myform = new Form();
        myform.Visible = true;
        myform.Text = "NewForm";
        myform.Name = "FormTest";
        Assert.That((object?)myform.DialogResult, Is.EqualTo(DialogResult.None));
        Assert.That((object?)myform.FormBorderStyle, Is.EqualTo(FormBorderStyle.Sizable));
        Assert.That((object?)myform.Icon?.GetType().ToString(), Is.EqualTo("System.Drawing.Icon"));
        Assert.IsTrue(myform.MaximizeBox);
        Assert.That((object?)myform.MaximumSize.Height, Is.EqualTo(0), "#20a");
        Assert.That((object?)myform.MaximumSize.Width, Is.EqualTo(0), "#20b");
        Assert.IsTrue(myform.MinimizeBox);
        Assert.That((object?)myform.MinimumSize.Height, Is.EqualTo(0), "#26a");
        Assert.That((object?)myform.MinimumSize.Width, Is.EqualTo(0), "#26b");
        Assert.IsTrue(myform.MinimumSize.IsEmpty, "#26c");
        Assert.That((object?)myform.Opacity, Is.EqualTo(1));
        Assert.IsTrue(myform.ShowInTaskbar);
        Assert.That((object?)myform.Size.Height, Is.EqualTo(300), "#32a");
        Assert.That((object?)myform.Size.Width, Is.EqualTo(300), "#32b");
        Assert.That((object?)myform.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultLocation));
        Assert.That((object?)myform.WindowState, Is.EqualTo(FormWindowState.Normal));
        Assert.That((object?)myform.ImeMode, Is.EqualTo(ImeMode.NoControl));
        myform.Dispose();
    }

    [Test]
    [Category("NotWorking")]
    public void ActivateTest()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;
        myform.Text = "NewForm";
        myform.Name = "FormTest";
        myform.Activate();
        Assert.That((object?)myform.Focus(), Is.EqualTo(true));
        myform.Dispose();
    }

    [Test]
    public void SetDialogResultOutOfRange()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        Assert.Throws<InvalidEnumArgumentException>(() =>
        {
            myform.DialogResult = (DialogResult)(-1);
        });

        Assert.Throws<InvalidEnumArgumentException>(() =>
        {
            myform.DialogResult = (DialogResult)((int)DialogResult.No + 1);
        });
        myform.Dispose();
    }

    private void myform_set_dialogresult(object? sender, EventArgs e)
    {
        var f = (Form)sender!;

        f.DialogResult = DialogResult.OK;
    }

    private void myform_close(object? sender, EventArgs e)
    {
        var f = (Form)sender!;

        f.Close();
    }

    [Test]
    public void SetDialogResult()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;
        myform.Visible = true;

        myform.DialogResult = DialogResult.Cancel;

        Assert.IsTrue(myform.Visible);
        Assert.IsFalse(myform.IsDisposed);

        myform.Close();

        Assert.IsFalse(myform.Visible);
        Assert.IsTrue(myform.IsDisposed);

        myform = new Form();
        myform.ShowInTaskbar = false;
        myform.VisibleChanged += myform_set_dialogresult;
        var result = myform.ShowDialog();

        Assert.That((object?)result, Is.EqualTo(DialogResult.OK));
        Assert.IsFalse(myform.Visible);
        Assert.IsFalse(myform.IsDisposed);
        myform.Dispose();

        myform = new Form();
        myform.ShowInTaskbar = false;
        myform.VisibleChanged += myform_close;
        result = myform.ShowDialog();

        Assert.That((object?)result, Is.EqualTo(DialogResult.Cancel));
        Assert.IsFalse(myform.Visible);
        Assert.IsFalse(myform.IsDisposed);

        myform.Dispose();
    }

    [Test] // bug #80052
    [Category("NotWorking")]
    public void Location()
    {
        // 
        // CenterParent
        // 

        var formA = new Form();
        formA.ShowInTaskbar = false;
        formA.StartPosition = FormStartPosition.CenterParent;
        formA.Location = new Point(151, 251);
        formA.Show();

        Assert.That((object?)formA.StartPosition, Is.EqualTo(FormStartPosition.CenterParent));
        Assert.IsFalse(formA.Location.X == 151);
        Assert.IsFalse(formA.Location.Y == 251);

        formA.Location = new Point(311, 221);

        Assert.That((object?)formA.StartPosition, Is.EqualTo(FormStartPosition.CenterParent));
        Assert.That((object?)formA.Location.X, Is.EqualTo(311));
        Assert.That((object?)formA.Location.Y, Is.EqualTo(221));

        formA.Dispose();

        // 
        // CenterScreen
        // 

        var formB = new Form();
        formB.ShowInTaskbar = false;
        formB.StartPosition = FormStartPosition.CenterScreen;
        formB.Location = new Point(151, 251);
        formB.Show();

        Assert.That((object?)formB.StartPosition, Is.EqualTo(FormStartPosition.CenterScreen));
        Assert.IsFalse(formB.Location.X == 151);
        Assert.IsFalse(formB.Location.Y == 251);

        formB.Location = new Point(311, 221);

        Assert.That((object?)formB.StartPosition, Is.EqualTo(FormStartPosition.CenterScreen));
        Assert.That((object?)formB.Location.X, Is.EqualTo(311));
        Assert.That((object?)formB.Location.Y, Is.EqualTo(221));

        formB.Dispose();

        // 
        // Manual
        // 

        var formC = new Form();
        formC.ShowInTaskbar = false;
        formC.StartPosition = FormStartPosition.Manual;
        formC.Location = new Point(151, 251);
        formC.Show();

        Assert.That((object?)formC.StartPosition, Is.EqualTo(FormStartPosition.Manual));
        Assert.That((object?)formC.Location.X, Is.EqualTo(151));
        Assert.That((object?)formC.Location.Y, Is.EqualTo(251));

        formC.Location = new Point(311, 221);

        Assert.That((object?)formC.StartPosition, Is.EqualTo(FormStartPosition.Manual));
        Assert.That((object?)formC.Location.X, Is.EqualTo(311));
        Assert.That((object?)formC.Location.Y, Is.EqualTo(221));

        formC.Dispose();

        // 
        // WindowsDefaultBounds
        // 

        var formD = new Form();
        formD.ShowInTaskbar = false;
        formD.StartPosition = FormStartPosition.WindowsDefaultBounds;
        formD.Location = new Point(151, 251);
        formD.Show();

        Assert.That((object?)formD.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultBounds));
        Assert.IsFalse(formD.Location.X == 151);
        Assert.IsFalse(formD.Location.Y == 251);

        formD.Location = new Point(311, 221);

        Assert.That((object?)formD.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultBounds));
        Assert.That((object?)formD.Location.X, Is.EqualTo(311));
        Assert.That((object?)formD.Location.Y, Is.EqualTo(221));

        formD.Dispose();

        // 
        // WindowsDefaultLocation
        // 

        var formE = new Form();
        formE.ShowInTaskbar = false;
        formE.Location = new Point(151, 251);
        formE.Show();

        Assert.That((object?)formE.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultLocation));
        Assert.IsFalse(formE.Location.X == 151);
        Assert.IsFalse(formE.Location.Y == 251);

        formE.Location = new Point(311, 221);

        Assert.That((object?)formE.StartPosition, Is.EqualTo(FormStartPosition.WindowsDefaultLocation));
        Assert.That((object?)formE.Location.X, Is.EqualTo(311));
        Assert.That((object?)formE.Location.Y, Is.EqualTo(221));

        formE.Dispose();
    }

    [Test]
    public void Opacity()
    {
        Form frm;
        using (frm = new Form())
        {
            Assert.That((object?)frm.Opacity, Is.EqualTo(1.0f), "#01-opacity");
            frm.Opacity = 0.50;
            Assert.That((object?)frm.Opacity, Is.EqualTo(0.50f), "#02-opacity");
            frm.Opacity = -0.1f;
            Assert.That((object?)frm.Opacity, Is.EqualTo(0), "#03-opacity");
            frm.Opacity = 1.1f;
            Assert.That((object?)frm.Opacity, Is.EqualTo(1), "#04-opacity");
        }
    }

    [Test]
    public void AccessDisposedForm()
    {
        Assert.Throws<ObjectDisposedException>(() =>
        {
            var myform = new Form();
            myform.ShowInTaskbar = false;

            myform.Show();
            myform.Close(); // this should result in the form being disposed
            myform.Show(); // and this line should result in the ODE being thrown
        });
    }

    private int handle_destroyed_count;

    private void handle_destroyed(object? sender, EventArgs e)
    {
        handle_destroyed_count++;
    }

    [Test]
    public void FormClose()
    {
        var myform = new Form();
        myform.ShowInTaskbar = false;

        Assert.IsFalse(myform.Visible);
        Assert.IsFalse(myform.IsDisposed);

        myform.Close();

        Assert.IsTrue(myform.IsDisposed);
    }

    [Test]
    public void FormClose2()
    {
        var f = new WMCloseWatcher();
        f.ShowInTaskbar = false;

        f.close_count = 0;
        Assert.IsFalse(f.Visible);
        f.Close();
        Assert.That((object?)f.close_count, Is.EqualTo(0));
        Assert.IsTrue(f.IsDisposed);
    }

    private class WMCloseWatcher : Form
    {
        public int close_count;

        protected override void WndProc(ref Message msg)
        {
            if (msg.Msg == 0x0010 /* WM_CLOSE */)
            {
                close_count++;
            }

            base.WndProc(ref msg);
        }
    }

    [Test]
    public void ShowWithOwnerIOE()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            using var f = new Form();
            f.Show(f);
        });
    }

    [Test]	// Bug #79959, #80574, #80791
    [Category("NotWorking")]
    public void BehaviorResizeOnBorderStyleChanged()
    {
        // Marked NotWorking because the ClientSize is dependent on the WM.
        // The values below match XP Luna to make sure our behavior is the same.
        var f = new Form();
        f.ShowInTaskbar = false;
        f.Show();

        Assert.That((object?)f.IsHandleCreated, Is.EqualTo(true));

        object expected = new Size(300, 300);
        Assert.That((object?)f.Size, Is.EqualTo(expected));
        object expected1 = new Size(292, 266);
        Assert.That((object?)f.ClientSize, Is.EqualTo(expected1));

        f.FormBorderStyle = FormBorderStyle.Fixed3D;
        object expected2 = new Size(302, 302);
        Assert.That((object?)f.Size, Is.EqualTo(expected2));
        object expected3 = new Size(292, 266);
        Assert.That((object?)f.ClientSize, Is.EqualTo(expected3));

        f.FormBorderStyle = FormBorderStyle.FixedDialog;
        object expected4 = new Size(298, 298);
        Assert.That((object?)f.Size, Is.EqualTo(expected4));
        object expected5 = new Size(292, 266);
        Assert.That((object?)f.ClientSize, Is.EqualTo(expected5));

        f.FormBorderStyle = FormBorderStyle.FixedSingle;
        object expected6 = new Size(298, 298);
        Assert.That((object?)f.Size, Is.EqualTo(expected6));
        object expected7 = new Size(292, 266);
        Assert.That((object?)f.ClientSize, Is.EqualTo(expected7));

        f.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        object expected8 = new Size(298, 290);
        Assert.That((object?)f.Size, Is.EqualTo(expected8));
        object expected9 = new Size(292, 266);
        Assert.That((object?)f.ClientSize, Is.EqualTo(expected9));

        f.FormBorderStyle = FormBorderStyle.None;
        object expected10 = new Size(292, 266);
        Assert.That((object?)f.Size, Is.EqualTo(expected10));
        object expected11 = new Size(292, 266);
        Assert.That((object?)f.ClientSize, Is.EqualTo(expected11));

        f.FormBorderStyle = FormBorderStyle.SizableToolWindow;
        object expected12 = new Size(300, 292);
        Assert.That((object?)f.Size, Is.EqualTo(expected12));
        object expected13 = new Size(292, 266);
        Assert.That((object?)f.ClientSize, Is.EqualTo(expected13));

        f.FormBorderStyle = FormBorderStyle.Sizable;
        object expected14 = new Size(300, 300);
        Assert.That((object?)f.Size, Is.EqualTo(expected14));
        object expected15 = new Size(292, 266);
        Assert.That((object?)f.ClientSize, Is.EqualTo(expected15));

        f.Close();
    }

    [Test]  // Bug #80574, #80791
    [Category("NotWorking")]
    public void BehaviorResizeOnBorderStyleChangedNotVisible()
    {
        // Marked NotWorking because the ClientSize is dependent on the WM.
        // The values below match XP Luna to make sure our behavior is the same.
        var f = new Form();
        f.ShowInTaskbar = false;

        Assert.That((object?)f.IsHandleCreated, Is.EqualTo(false));

        object expected = new Size(300, 300);
        Assert.That((object?)f.Size, Is.EqualTo(expected));
        object expected1 = new Size(292, 266);
        Assert.That((object?)f.ClientSize, Is.EqualTo(expected1));

        f.FormBorderStyle = FormBorderStyle.Fixed3D;
        object expected2 = new Size(300, 300);
        Assert.That((object?)f.Size, Is.EqualTo(expected2));
        object expected3 = new Size(292, 266);
        Assert.That((object?)f.ClientSize, Is.EqualTo(expected3));

        f.FormBorderStyle = FormBorderStyle.FixedDialog;
        object expected4 = new Size(300, 300);
        Assert.That((object?)f.Size, Is.EqualTo(expected4));
        object expected5 = new Size(292, 266);
        Assert.That((object?)f.ClientSize, Is.EqualTo(expected5));

        f.FormBorderStyle = FormBorderStyle.FixedSingle;
        object expected6 = new Size(300, 300);
        Assert.That((object?)f.Size, Is.EqualTo(expected6));
        object expected7 = new Size(292, 266);
        Assert.That((object?)f.ClientSize, Is.EqualTo(expected7));

        f.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        object expected8 = new Size(300, 300);
        Assert.That((object?)f.Size, Is.EqualTo(expected8));
        object expected9 = new Size(292, 266);
        Assert.That((object?)f.ClientSize, Is.EqualTo(expected9));

        f.FormBorderStyle = FormBorderStyle.None;
        object expected10 = new Size(300, 300);
        Assert.That((object?)f.Size, Is.EqualTo(expected10));
        object expected11 = new Size(292, 266);
        Assert.That((object?)f.ClientSize, Is.EqualTo(expected11));

        f.FormBorderStyle = FormBorderStyle.SizableToolWindow;
        object expected12 = new Size(300, 300);
        Assert.That((object?)f.Size, Is.EqualTo(expected12));
        object expected13 = new Size(292, 266);
        Assert.That((object?)f.ClientSize, Is.EqualTo(expected13));

        f.FormBorderStyle = FormBorderStyle.Sizable;
        object expected14 = new Size(300, 300);
        Assert.That((object?)f.Size, Is.EqualTo(expected14));
        object expected15 = new Size(292, 266);
        Assert.That((object?)f.ClientSize, Is.EqualTo(expected15));
    }

    [Test]  // Bug #80574, #80791
    [Category("NotWorking")]
    public void MoreBehaviorResizeOnBorderStyleChangedNotVisible()
    {
        // Marked NotWorking because the ClientSize is dependent on the WM.
        // The values below match XP Luna to make sure our behavior is the same.
        var f = new Form();
        f.ShowInTaskbar = false;

        f.Show();
        f.Hide();

        Assert.That((object?)f.IsHandleCreated, Is.EqualTo(true));

        f.FormBorderStyle = FormBorderStyle.Sizable;
        object expected = new Size(300, 300);
        Assert.That((object?)f.Size, Is.EqualTo(expected));
        object expected1 = new Size(292, 266);
        Assert.That((object?)f.ClientSize, Is.EqualTo(expected1));
        f.FormBorderStyle = FormBorderStyle.None;
        object expected2 = new Size(292, 266);
        Assert.That((object?)f.Size, Is.EqualTo(expected2));
        object expected3 = new Size(292, 266);
        Assert.That((object?)f.ClientSize, Is.EqualTo(expected3));
    }

    [Test]  // bug #438866
    public void MinMaxSize()
    {
        var f = new Form();

        f.MinimumSize = new Size(200, 200);
        f.MaximumSize = new Size(150, 150);

        object expected = new Size(150, 150);
        Assert.That((object?)f.MinimumSize, Is.EqualTo(expected));
        object expected1 = new Size(150, 150);
        Assert.That((object?)f.MaximumSize, Is.EqualTo(expected1));

        f.MinimumSize = new Size(200, 200);

        object expected2 = new Size(200, 200);
        Assert.That((object?)f.MinimumSize, Is.EqualTo(expected2));
        object expected3 = new Size(200, 200);
        Assert.That((object?)f.MaximumSize, Is.EqualTo(expected3));

        f.Dispose();
    }

    [Test]
    public void MinSizeIssue()
    {
        var f = new Form();

        f.MinimumSize = new Size(100, 100);

        f.Show();

        object expected = new Size(300, 300);
        Assert.That((object?)f.Size, Is.EqualTo(expected));

        f.Dispose();
    }

    private void tv_GotFocus(object? sender, EventArgs e)
    {
        //Console.WriteLine (Environment.StackTrace);
    }

    [Test]
    public void Bug82470()
    {
        var f = new Form();
        f.Load += Form_LoadAndHide;
        f.Show();

        Assert.That((object?)f.Visible, Is.EqualTo(true));

        f.Dispose();
    }

    private void Form_LoadAndHide(object? sender, EventArgs e)
    {
        ((Form)sender!).Visible = false;
    }

    [Test]
    public void AutoSizeGrowOnly()
    {
        var f = new Form();
        f.ShowInTaskbar = false;
        f.AutoSize = true;

        var b = new Button();
        b.Size = new Size(200, 200);
        b.Location = new Point(200, 200);
        f.Controls.Add(b);

        f.Show();

        object expected = new Size(403, 403);
        Assert.That((object?)f.ClientSize, Is.EqualTo(expected));

        f.Controls.Remove(b);
        object expected1 = new Size(403, 403);
        Assert.That((object?)f.ClientSize, Is.EqualTo(expected1));

        f.Dispose();
    }

    [Test]
    public void AutoSizeReset()
    {
        var f = new Form();
        f.ShowInTaskbar = false;

        var b = new Button();
        b.Size = new Size(200, 200);
        b.Location = new Point(200, 200);
        f.Controls.Add(b);

        f.Show();

        var start_size = f.ClientSize;

        f.AutoSize = true;
        object expected = new Size(403, 403);
        Assert.That((object?)f.ClientSize, Is.EqualTo(expected));

        f.AutoSize = false;
        Assert.That((object?)f.ClientSize, Is.EqualTo(start_size));
        f.Close();
    }

    [Test]
    public void AutoSizeGrowAndShrink()
    {
        var f = new Form();
        f.ShowInTaskbar = false;
        f.AutoSize = true;

        f.Show();

        // Make sure form shrunk
        Assert.IsTrue(f.ClientSize.Width < 150);
        Assert.IsTrue(f.ClientSize.Height < 150, "A1-2");

        var b = new Button();
        b.Size = new Size(200, 200);
        b.Location = new Point(0, 0);
        f.Controls.Add(b);

        object expected = new Size(203, 203);
        Assert.That((object?)f.ClientSize, Is.EqualTo(expected));
        f.Dispose();
    }


    [Test]
    public void SettingIconToNull()
    {
        var form = new Form();
        Assert.IsNotNull(form.Icon, "1");
        form.Icon = null;
        Assert.IsNotNull(form.Icon, "2");
    }

}

public class TimeBombedForm : Form
{
    public Timer timer;
    public bool CloseOnPaint;
    public string Reason;
    public TimeBombedForm()
    {
        timer = new Timer();
        timer.Interval = 500;
        timer.Tick += timer_Tick;
        timer.Start();
    }

    private void timer_Tick(object? sender, EventArgs e)
    {
        Reason = "Bombed";
        Close();
    }

    protected override void OnPaint(PaintEventArgs pevent)
    {
        base.OnPaint(pevent);
        if (CloseOnPaint)
        {
            Reason = "OnPaint";
            Close();
        }
    }
}