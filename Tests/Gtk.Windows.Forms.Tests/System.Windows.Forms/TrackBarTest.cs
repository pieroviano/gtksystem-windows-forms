//
// TrackBarTest.cs: Test cases for TrackBar.
//
// Author:
//   Ritvik Mayank (mritvik@novell.com)
//
// (C) 2005 Novell, Inc. (http://www.novell.com)
//

using GtkTests.Helpers;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class TrackBarBaseTest : TestHelper
{
    [Test]
    public void TrackBarPropertyTest()
    {
        var myTrackBar = new TrackBar();

        // A
        Assert.That((object?)myTrackBar.AutoSize, Is.EqualTo(true));

        // L
        Assert.That((object?)myTrackBar.LargeChange, Is.EqualTo(5));

        // M
        Assert.That((object?)myTrackBar.Maximum, Is.EqualTo(10));
        Assert.That((object?)myTrackBar.Minimum, Is.EqualTo(0));

        // O
        Assert.That((object?)myTrackBar.Orientation, Is.EqualTo(Orientation.Horizontal));

        // T
        Assert.That((object?)myTrackBar.TickFrequency, Is.EqualTo(1));
        Assert.That((object?)myTrackBar.TickStyle, Is.EqualTo(TickStyle.BottomRight));
        Assert.That((object?)myTrackBar.Text, Is.EqualTo(string.Empty));
        myTrackBar.Text = "New TrackBar";
        Assert.That((object?)myTrackBar.Text, Is.EqualTo("New TrackBar"));

        // V
        Assert.That((object?)myTrackBar.Value, Is.EqualTo(0));
    }

    [Test]
    public void LargeChangeTest()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var myTrackBar = new TrackBar();
            myTrackBar.LargeChange = -1;
        });
    }

    [Test]
    public void ToStringMethodTest()
    {
        var myTrackBar = new TrackBar();
        myTrackBar.Text = "New TrackBar";
        Assert.That((object?)myTrackBar.ToString(), Is.EqualTo("System.Windows.Forms.TrackBar, Minimum: 0, Maximum: 10, Value: 0"));
    }

    [Test]
    public void OrientationSizeTest()
    {
        IntPtr handle;
        int width;
        int height;
        var default_height = 45;
        var default_height2 = 42;

        using (var myTrackBar = new TrackBar())
        {
            width = myTrackBar.Width;
            height = myTrackBar.Height;
            myTrackBar.Orientation = Orientation.Vertical;
            Assert.That((object?)myTrackBar.Width, Is.EqualTo(width), "#OS1");
            Assert.That((object?)myTrackBar.Height, Is.EqualTo(height), "#OS2");
        }

        using (var myForm = new Form())
        {
            using (var myTrackBar = new TrackBar())
            {
                width = myTrackBar.Width;
                height = myTrackBar.Height;
                myForm.Controls.Add(myTrackBar);
                handle = myTrackBar.Handle; // causes the handle to be created.
                myTrackBar.Orientation = Orientation.Vertical;
                AreEqual(default_height, default_height2, myTrackBar.Width, "#OS3");
                Assert.That((object?)myTrackBar.Height, Is.EqualTo(width), "#OS4");
            }
        }

        using (var myForm = new Form())
        {
            using (var myTrackBar = new TrackBar())
            {
                myForm.Controls.Add(myTrackBar);
                handle = myTrackBar.Handle; // causes the handle to be created.
                myTrackBar.Width = 200;
                myTrackBar.Orientation = Orientation.Vertical;
                Assert.That((object?)myTrackBar.Height, Is.EqualTo(200), "#OS5");
            }
        }
        Assert.That((object?)handle, Is.EqualTo(handle), "Removes warning");
    }

    private void AreEqual(int expected1, int expected2, int real, string name)
    {
        // This is needed since the default size vary between XP theme and W2K theme.
        Assert.False(real != expected1 && real != expected2, "{3}: Expected <{0}> or <{1}>, but was <{2}>", expected1, expected2, real, name);
    }

    [Test]
    [Category("NotWorking")]
    public void SizeTestSettingOrientation()
    {
        IntPtr handle;
        var default_height = 45;
        var default_height2 = 42;

        using (var myTrackBar = new TrackBar())
        {
            myTrackBar.Width = 200;
            myTrackBar.Height = 250;
            myTrackBar.Orientation = Orientation.Vertical;
            Assert.That((object?)myTrackBar.Width, Is.EqualTo(200), "#SIZE03");
            Assert.That((object?)myTrackBar.Height, Is.EqualTo(250), "#SIZE04");
        }

        using (var myTrackBar = new TrackBar())
        {
            myTrackBar.AutoSize = false;
            myTrackBar.Width = 200;
            myTrackBar.Height = 250;
            myTrackBar.Orientation = Orientation.Vertical;
            Assert.That((object?)myTrackBar.Width, Is.EqualTo(200), "#SIZE07");
            Assert.That((object?)myTrackBar.Height, Is.EqualTo(250), "#SIZE08");
        }

        using (var myTrackBar = new TrackBar())
        {
            myTrackBar.Width = 200;
            myTrackBar.Height = 250;
            myTrackBar.AutoSize = false;
            myTrackBar.Orientation = Orientation.Vertical;
            Assert.That((object?)myTrackBar.Width, Is.EqualTo(200), "#SIZE11");
            Assert.That((object?)myTrackBar.Height, Is.EqualTo(250), "#SIZE12");
        }

        using (var myTrackBar = new TrackBar())
        {
            using (var myForm = new Form())
            {
                myForm.Controls.Add(myTrackBar);
                myTrackBar.Width = 200;
                myTrackBar.Height = 250;
                myTrackBar.Orientation = Orientation.Vertical;
                handle = myTrackBar.Handle;

                AreEqual(default_height, default_height2, myTrackBar.Width, "#SIZE17");
                Assert.That((object?)myTrackBar.Height, Is.EqualTo(250), "#SIZE18");
            }
        }

        using (var myTrackBar = new TrackBar())
        {
            using (var myForm = new Form())
            {
                myForm.Controls.Add(myTrackBar);
                myTrackBar.Width = 200;
                myTrackBar.Height = 250;
                myTrackBar.Orientation = Orientation.Vertical;
                handle = myTrackBar.Handle;

                AreEqual(default_height, default_height2, myTrackBar.Width, "#SIZE19");
                Assert.That((object?)myTrackBar.Height, Is.EqualTo(250), "#SIZE20");
            }
        }

        using (var myTrackBar = new TrackBar())
        {
            using (var myForm = new Form())
            {
                myForm.Controls.Add(myTrackBar);
                myTrackBar.Width = 200;
                myTrackBar.Height = 250;
                myTrackBar.Orientation = Orientation.Vertical;
                handle = myTrackBar.Handle;

                myTrackBar.Orientation = Orientation.Horizontal;

                Assert.That((object?)myTrackBar.Width, Is.EqualTo(250), "#SIZE23");
                AreEqual(default_height, default_height2, myTrackBar.Height, "#SIZE24");
            }
        }

        using (var myTrackBar = new TrackBar())
        {
            myTrackBar.AutoSize = false;
            myTrackBar.Height = 50;
            myTrackBar.Width = 80;
            myTrackBar.Orientation = Orientation.Vertical;
            myTrackBar.Width = 100;

            Assert.That((object?)myTrackBar.Height, Is.EqualTo(50), "#SIZE2_1");
            Assert.That((object?)myTrackBar.Width, Is.EqualTo(100), "#SIZE2_2");

            using (var myForm = new Form())
            {
                myForm.Controls.Add(myTrackBar);
                myForm.Show();

                Assert.That((object?)myTrackBar.Height, Is.EqualTo(50), "#SIZE2_3");
                Assert.That((object?)myTrackBar.Width, Is.EqualTo(100), "#SIZE2_4");
            }
        }

        Assert.That((object?)handle, Is.EqualTo(handle), "Removes warning");
    }

    [Test]
    public void SizeTest()
    {
        IntPtr handle;
        var default_height = 45;
        var default_height2 = 42;

        using (var myTrackBar = new TrackBar())
        {
            myTrackBar.Width = 200;
            myTrackBar.Height = 250;
            Assert.That((object?)myTrackBar.Width, Is.EqualTo(200), "#SIZE01");
            AreEqual(default_height, default_height2, myTrackBar.Height, "#SIZE02");
        }

        using (var myTrackBar = new TrackBar())
        {
            myTrackBar.AutoSize = false;
            myTrackBar.Width = 200;
            myTrackBar.Height = 250;
            Assert.That((object?)myTrackBar.Width, Is.EqualTo(200), "#SIZE05");
            Assert.That((object?)myTrackBar.Height, Is.EqualTo(250), "#SIZE06");
        }

        using (var myTrackBar = new TrackBar())
        {
            myTrackBar.Width = 200;
            myTrackBar.Height = 250;
            myTrackBar.AutoSize = false;
            Assert.That((object?)myTrackBar.Width, Is.EqualTo(200), "#SIZE09");
            AreEqual(default_height, default_height2, myTrackBar.Height, "#SIZE10");
        }

        using (var myTrackBar = new TrackBar())
        {
            using (var myForm = new Form())
            {
                myForm.Controls.Add(myTrackBar);
                myTrackBar.Width = 200;
                myTrackBar.Height = 250;
                myTrackBar.Orientation = Orientation.Vertical;
                myTrackBar.Orientation = Orientation.Horizontal;
                handle = myTrackBar.Handle;

                Assert.That((object?)myTrackBar.Width, Is.EqualTo(200), "#SIZE21");
                AreEqual(default_height, default_height2, myTrackBar.Height, "#SIZE22");
            }
        }

        Assert.That((object?)handle, Is.EqualTo(handle), "Removes warning");
    }
}