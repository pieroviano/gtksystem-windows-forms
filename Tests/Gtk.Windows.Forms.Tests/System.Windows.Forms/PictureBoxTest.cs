//
// PictureBoxTest.cs: Test cases for PictureBox.
//
// Author:
//   Ritvik Mayank (mritvik@novell.com)
//
// (C) 2005 Novell, Inc. (http://www.novell.com)
//

using System.Drawing;
using System.Windows.Forms;
using GtkTests.Helpers;

using BorderStyle = System.Windows.Forms.BorderStyle;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class PictureBoxTest : TestHelper
{
    [Test]
    public void PictureBoxPropertyTest()
    {
        var myForm = new Form();
        myForm.ShowInTaskbar = false;
        var myPicBox = new PictureBox();
        myForm.Controls.Add(myPicBox);

        // B 
        Assert.That((object?)myPicBox.BorderStyle, Is.EqualTo(BorderStyle.None));
        myPicBox.BorderStyle = BorderStyle.Fixed3D;
        Assert.That((object?)myPicBox.BorderStyle, Is.EqualTo(BorderStyle.Fixed3D));

        // P 
        Assert.That((object?)myPicBox.SizeMode, Is.EqualTo(PictureBoxSizeMode.Normal));
        myPicBox.SizeMode = PictureBoxSizeMode.AutoSize;
        Assert.That((object?)myPicBox.SizeMode, Is.EqualTo(PictureBoxSizeMode.AutoSize));

        myForm.Dispose();
    }

    [Test]
    [Category("NotWorking")]
    public void ImageLocation_Async()
    {
        var f = new Form();
        var pb = new PictureBox();
        f.Controls.Add(pb);
        f.Show();

        Assert.IsNull(pb.ImageLocation, "#A");

        pb.ImageLocation = TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif");
        Application.DoEvents();

        object expected = TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif");
        Assert.That((object?)pb.ImageLocation, Is.EqualTo(expected));
        Assert.That((object?)pb.Image, Is.SameAs(pb.InitialImage));

        using (var s = TestResourceHelper.GetStreamOfResource("Test/resources/32x32.ico"))
        {
            pb.Image = Image.FromStream(s);
        }
        Application.DoEvents();

        object expected1 = TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif");
        Assert.That((object?)pb.ImageLocation, Is.EqualTo(expected1));
        Assert.IsNotNull(pb.Image);
        Assert.That((object?)pb.Image.Height, Is.EqualTo(60));
        Assert.That((object?)pb.Image.Width, Is.EqualTo(150));

        pb.ImageLocation = null;
        Application.DoEvents();

        Assert.IsNull(pb.ImageLocation);
        Assert.IsNull(pb.Image);

        pb.ImageLocation = TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif");
        Application.DoEvents();

        object expected2 = TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif");
        Assert.That((object?)pb.ImageLocation, Is.EqualTo(expected2));
        Assert.IsNull(pb.Image);

        pb.Load();
        Application.DoEvents();

        object expected3 = TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif");
        Assert.That((object?)pb.ImageLocation, Is.EqualTo(expected3));
        Assert.IsNotNull(pb.Image);
        Assert.That((object?)pb.Image.Height, Is.EqualTo(60));
        Assert.That((object?)pb.Image.Width, Is.EqualTo(150));

        pb.ImageLocation = null;
        Application.DoEvents();

        Assert.IsNull(pb.ImageLocation);
        Assert.IsNull(pb.Image);

        pb.ImageLocation = TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif");
        pb.Load();
        pb.ImageLocation = "XYZ.gif";
        Application.DoEvents();

        Assert.That((object?)pb.ImageLocation, Is.EqualTo("XYZ.gif"));
        Assert.IsNotNull(pb.Image);
        Assert.That((object?)pb.Image.Height, Is.EqualTo(60));
        Assert.That((object?)pb.Image.Width, Is.EqualTo(150));

        pb.ImageLocation = string.Empty;
        Application.DoEvents();

        object expected4 = string.Empty;
        Assert.That((object?)pb.ImageLocation, Is.EqualTo(expected4));
        Assert.IsNull(pb.Image);

        using (var s = TestResourceHelper.GetStreamOfResource("Test/resources/32x32.ico"))
        {
            pb.Image = Image.FromStream(s);
        }
        Application.DoEvents();

        object expected5 = string.Empty;
        Assert.That((object?)pb.ImageLocation, Is.EqualTo(expected5));
        Assert.IsNotNull(pb.Image);
        Assert.That((object?)pb.Image.Height, Is.EqualTo(96));
        Assert.That((object?)pb.Image.Width, Is.EqualTo(96));

        pb.Load(TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif"));
        Application.DoEvents();

        object expected6 = TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif");
        Assert.That((object?)pb.ImageLocation, Is.EqualTo(expected6));
        Assert.IsNotNull(pb.Image);
        Assert.That((object?)pb.Image.Height, Is.EqualTo(60));
        Assert.That((object?)pb.Image.Width, Is.EqualTo(150));

        pb.ImageLocation = null;
        Application.DoEvents();

        Assert.IsNull(pb.ImageLocation);
        Assert.IsNull(pb.Image);

        f.Dispose();
    }

    [Test]
    public void ImageLocation_Sync()
    {
        var f = new Form();
        var pb = new PictureBox();
        f.Controls.Add(pb);
        f.Show();

        Assert.IsNull(pb.ImageLocation, "#A");

        pb.ImageLocation = TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif");

        object expected = TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif");
        Assert.That((object?)pb.ImageLocation, Is.EqualTo(expected));
        Assert.IsNotNull(pb.Image);
        Assert.That((object?)pb.Image.Height, Is.EqualTo(60));
        Assert.That((object?)pb.Image.Width, Is.EqualTo(150));

        using (var s = TestResourceHelper.GetStreamOfResource("Test/resources/32x32.ico"))
        {
            pb.Image = Image.FromStream(s);
        }

        object expected1 = TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif");
        Assert.That((object?)pb.ImageLocation, Is.EqualTo(expected1));
        Assert.IsNotNull(pb.Image);
        Assert.That((object?)pb.Image.Height, Is.EqualTo(96));
        Assert.That((object?)pb.Image.Width, Is.EqualTo(96));

        pb.ImageLocation = null;

        Assert.IsNull(pb.ImageLocation);
        Assert.IsNotNull(pb.Image);
        Assert.That((object?)pb.Image.Height, Is.EqualTo(96));
        Assert.That((object?)pb.Image.Width, Is.EqualTo(96));

        pb.ImageLocation = TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif");

        object expected2 = TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif");
        Assert.That((object?)pb.ImageLocation, Is.EqualTo(expected2));
        Assert.IsNotNull(pb.Image);
        Assert.That((object?)pb.Image.Height, Is.EqualTo(60));
        Assert.That((object?)pb.Image.Width, Is.EqualTo(150));

        pb.Load();

        object expected3 = TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif");
        Assert.That((object?)pb.ImageLocation, Is.EqualTo(expected3));
        Assert.IsNotNull(pb.Image);
        Assert.That((object?)pb.Image.Height, Is.EqualTo(60));
        Assert.That((object?)pb.Image.Width, Is.EqualTo(150));

        pb.ImageLocation = null;

        Assert.IsNull(pb.ImageLocation);
        Assert.IsNull(pb.Image);

        using (var s = TestResourceHelper.GetStreamOfResource("Test/resources/32x32.ico"))
        {
            pb.Image = Image.FromStream(s);
        }

        Assert.IsNull(pb.ImageLocation);
        Assert.IsNotNull(pb.Image);
        Assert.That((object?)pb.Image.Height, Is.EqualTo(96));
        Assert.That((object?)pb.Image.Width, Is.EqualTo(96));

        pb.Load(TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif"));

        object expected4 = TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif");
        Assert.That((object?)pb.ImageLocation, Is.EqualTo(expected4));
        Assert.IsNotNull(pb.Image);
        Assert.That((object?)pb.Image.Height, Is.EqualTo(60));
        Assert.That((object?)pb.Image.Width, Is.EqualTo(150));

        pb.ImageLocation = string.Empty;

        object expected5 = string.Empty;
        Assert.That((object?)pb.ImageLocation, Is.EqualTo(expected5));
        Assert.IsNull(pb.Image);

        pb.ImageLocation = TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif");

        object expected6 = TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif");
        Assert.That((object?)pb.ImageLocation, Is.EqualTo(expected6));
        Assert.IsNotNull(pb.Image);
        Assert.That((object?)pb.Image.Height, Is.EqualTo(60));
        Assert.That((object?)pb.Image.Width, Is.EqualTo(150));

        Assert.Throws<FileNotFoundException>(() =>
        {
            try
            {
                pb.ImageLocation = "XYZ.gif";
            }
            catch (FileNotFoundException ex)
            {
                object expected7 = typeof(FileNotFoundException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected7));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                throw;
            }
        });

        Assert.That((object?)pb.ImageLocation, Is.EqualTo("XYZ.gif"));
        Assert.IsNotNull(pb.Image);
        Assert.That((object?)pb.Image.Height, Is.EqualTo(60));
        Assert.That((object?)pb.Image.Width, Is.EqualTo(150));

        f.Dispose();
    }

    [Test]
    public void ImagePropertyTest()
    {
        var myPicBox = new PictureBox();
        // I 
        Assert.IsNull(myPicBox.Image);
        var myImage = Image.FromFile(TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif"));
        myPicBox.Image = myImage;
        Assert.That((object?)myPicBox.Image, Is.SameAs(myImage));
        Assert.That((object?)myPicBox.Image.Height, Is.EqualTo(60));
        Assert.That((object?)myPicBox.Image.Width, Is.EqualTo(150));
        myPicBox.Image = null;
        Assert.IsNull(myPicBox.Image);
        myPicBox.Image = null;
        Assert.IsNull(myPicBox.Image);
    }

    [Test] // Load ()
    public void Load_ImageLocation_Empty()
    {
        var pb = new PictureBox();
        pb.ImageLocation = string.Empty;

        Assert.Throws<InvalidOperationException>(() =>
        {
            try
            {
                pb.Load();
            }
            catch (InvalidOperationException ex)
            {
                // ImageLocation must be set
                object expected = typeof(InvalidOperationException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                throw;
            }
        });
    }

    [Test] // Load ()
    public void Load_ImageLocation_Null()
    {
        var pb = new PictureBox();
        pb.ImageLocation = null;

        Assert.Throws<InvalidOperationException>(() =>
        {
            try
            {
                pb.Load();
            }
            catch (InvalidOperationException ex)
            {
                // ImageLocation must be set
                object expected = typeof(InvalidOperationException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                throw;
            }
        });
    }

    [Test] // Load (String)
    public void Load2_Url_Empty()
    {
        var pb = new PictureBox();

        Assert.Throws<InvalidOperationException>(() =>
        {
            try
            {
                pb.Load(string.Empty);
            }
            catch (InvalidOperationException ex)
            {
                // ImageLocation must be set
                object expected = typeof(InvalidOperationException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                throw;
            }
        });
    }

    [Test] // Load (String)
    public void Load2_Url_Null()
    {
        var pb = new PictureBox();

        Assert.Throws<InvalidOperationException>(() =>
        {
            try
            {
                pb.Load(null);
            }
            catch (InvalidOperationException ex)
            {
                // ImageLocation must be set
                object expected = typeof(InvalidOperationException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                throw;
            }
        });
    }

    [Test] // LoadAsync ()
    public async Task LoadAsync1_ImageLocation_Empty()
    {
        var pb = new PictureBox();
        pb.ImageLocation = string.Empty;

        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            try
            {
                await pb.LoadAsync();
            }
            catch (InvalidOperationException ex)
            {
                // ImageLocation must be set
                object expected = typeof(InvalidOperationException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                throw;
            }
        });
    }

    [Test] // LoadAsync ()
    public async Task LoadAsync1_ImageLocation_Null()
    {
        var pb = new PictureBox();
        pb.ImageLocation = null;

        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            try
            {
                await pb.LoadAsync();
            }
            catch (InvalidOperationException ex)
            {
                // ImageLocation must be set
                object expected = typeof(InvalidOperationException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                throw;
            }
        });
    }

    [Test] // LoadAsync (String)
    public async Task LoadASync2_Url_Empty()
    {
        var pb = new PictureBox();

        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            try
            {
                await pb.LoadAsync(string.Empty);
            }
            catch (InvalidOperationException ex)
            {
                // ImageLocation must be set
                object expected = typeof(InvalidOperationException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                throw;
            }
        });
    }

    [Test] // LoadAsync (String)
    public async Task LoadAsync2_Url_Null()
    {
        var pb = new PictureBox();

        Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            try
            {
                await pb.LoadAsync(null);
            }
            catch (InvalidOperationException ex)
            {
                // ImageLocation must be set
                object expected = typeof(InvalidOperationException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                throw;
            }
        });
    }

    [Test]
    public void ToStringMethodTest()
    {
        var myPicBox = new PictureBox();
        Assert.That((object?)myPicBox.ToString(), Is.EqualTo("System.Windows.Forms.PictureBox, SizeMode: Normal"));
    }

    [Test]
    public void Defaults()
    {
        var pb = new PictureBox();

        Assert.IsNotNull(pb.ErrorImage);

        Assert.That((object?)pb.AutoSize, Is.EqualTo(false));
        pb.SizeMode = PictureBoxSizeMode.AutoSize;
        Assert.That((object?)pb.AutoSize, Is.EqualTo(true));

    }
}