//
// ImageImageListTest.cs: Test cases for ImageImageList.
//
// Author:
//   Ritvik Mayank (mritvik@novell.com)
//
// (C) 2005 Novell, Inc. (http://www.novell.com)
//

using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using GtkTests.Helpers;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class ImageListTest : TestHelper
{
    [Test]
    public void ImageListPropertyTest()
    {
        var myimagelist = new ImageList();

        // C
        Assert.That((object?)myimagelist.ColorDepth, Is.EqualTo(ColorDepth.Depth8Bit));
        myimagelist.ColorDepth = ColorDepth.Depth32Bit;
        Assert.That((object?)myimagelist.ColorDepth, Is.EqualTo(ColorDepth.Depth32Bit));
        Assert.That((object?)myimagelist.Images.Count, Is.EqualTo(0));
        // H
        Assert.That((object?)myimagelist.HandleCreated, Is.EqualTo(false));
        myimagelist.Handle.ToInt32();
        Assert.That((object?)myimagelist.HandleCreated, Is.EqualTo(true));
        Assert.That((object?)myimagelist.Handle.GetType().FullName, Is.EqualTo("System.IntPtr"));

        // I
        var myImage = Image.FromFile(TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif"));
        myimagelist.Images.Add(myImage);
        Assert.That((object?)myimagelist.Images.Count, Is.EqualTo(1));
        Assert.That((object?)myimagelist.ImageSize.Height, Is.EqualTo(16));
        Assert.That((object?)myimagelist.ImageSize.Width, Is.EqualTo(16));
        // [MonoTODO ("Add test for ImageStream")]
        // [MonoTODO ("Test for Draw Method (visual test)")]

        // T
        Assert.That((object?)myimagelist.TransparentColor, Is.EqualTo(Color.Transparent));
    }

    [Test]
    public void ImageListComponentModelTest()
    {
        var colordepth_prop = TypeDescriptor.GetProperties(typeof(ImageList))["ColorDepth"];
        var imagesize_prop = TypeDescriptor.GetProperties(typeof(ImageList))["ImageSize"];
        var transparentcolor_prop = TypeDescriptor.GetProperties(typeof(ImageList))["TransparentColor"];

        // create a blank ImageList
        var il = new ImageList();

        // test its defaults
        Assert.IsTrue(colordepth_prop!.ShouldSerializeValue(il), "1");
        Assert.IsTrue(colordepth_prop.CanResetValue(il), "2");
        Assert.IsTrue(imagesize_prop!.ShouldSerializeValue(il), "3");
        Assert.IsTrue(imagesize_prop.CanResetValue(il), "4");
        Assert.IsTrue(transparentcolor_prop!.ShouldSerializeValue(il), "5");
        Assert.IsTrue(transparentcolor_prop.CanResetValue(il), "6");

        // test what happens when we set the transparent color to LightGray
        il.TransparentColor = Color.LightGray;
        Assert.IsFalse(transparentcolor_prop.ShouldSerializeValue(il), "7");
        Assert.IsFalse(transparentcolor_prop.CanResetValue(il), "8");

        // test what happens when we set the depth to something other than the default
        il.ColorDepth = ColorDepth.Depth16Bit;
        Assert.IsTrue(colordepth_prop.ShouldSerializeValue(il), "9");
        Assert.IsTrue(colordepth_prop.CanResetValue(il), "10");
        // same test for ImageSize
        il.ImageSize = new Size(32, 32);
        Assert.IsTrue(imagesize_prop.ShouldSerializeValue(il), "11");
        Assert.IsTrue(imagesize_prop.CanResetValue(il), "12");

        // create an ImageList containing an image
        il = new ImageList();
        il.Images.Add(Image.FromFile(TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif")));

        Assert.IsFalse(colordepth_prop.ShouldSerializeValue(il), "13");
        Assert.IsFalse(colordepth_prop.CanResetValue(il), "14");
        Assert.IsFalse(imagesize_prop.ShouldSerializeValue(il), "15");
        Assert.IsFalse(imagesize_prop.CanResetValue(il), "16");
        Assert.IsTrue(transparentcolor_prop.ShouldSerializeValue(il), "17");
        Assert.IsTrue(transparentcolor_prop.CanResetValue(il), "18");

        // test what happens when we set the transparent color to LightGray
        il.TransparentColor = Color.LightGray;
        Assert.IsFalse(transparentcolor_prop.ShouldSerializeValue(il), "19");
        Assert.IsFalse(transparentcolor_prop.CanResetValue(il), "20");

        // test what happens when we set the depth to something other than the default
        il.ColorDepth = ColorDepth.Depth16Bit;
        Assert.IsFalse(colordepth_prop.ShouldSerializeValue(il), "21");
        Assert.IsFalse(colordepth_prop.CanResetValue(il), "22");

        // same test for ImageSize
        il.ImageSize = new Size(32, 32);
        Assert.IsFalse(imagesize_prop.ShouldSerializeValue(il), "23");
        Assert.IsFalse(imagesize_prop.CanResetValue(il), "24");
    }

    [Test]
    public void ToStringMethodTest()
    {
        var myimagelist = new ImageList();
        Assert.That((object?)myimagelist.ToString(), Is.EqualTo("System.Windows.Forms.ImageList Images.Count: 0, ImageSize: {Width=16, Height=16}"));
    }

    [Test] // bug #409169
    public void ICollection_CopyTo()
    {
        var imgList = new ImageList();
        var coll = imgList.Images;

        var gif = Image.FromFile(TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif"));
        coll.Add(gif);
        var bmp = new Bitmap(10, 10);
        coll.Add(bmp);

        const int dstOffset = 5;
        object[] dst = new object[dstOffset + coll.Count + 1];
        ((ICollection)coll).CopyTo(dst, dstOffset);

        Assert.IsNull(dst[0]);
        Assert.IsNull(dst[1]);
        Assert.IsNull(dst[2]);
        Assert.IsNull(dst[3]);
        Assert.IsNull(dst[4]);
        Assert.IsNotNull(dst[5], "#6a");
        Assert.IsFalse(ReferenceEquals(gif, dst[5]), "#6b");
        object expected = typeof(Bitmap);
        Assert.That((object?)dst[5].GetType(), Is.EqualTo(expected), "#6c");
        Assert.IsNotNull(dst[6], "#7a");
        Assert.IsFalse(ReferenceEquals(bmp, dst[6]), "#7b");
        object expected1 = typeof(Bitmap);
        Assert.That((object?)dst[6].GetType(), Is.EqualTo(expected1), "#7c");
        Assert.IsNull(dst[7]);

        ((Image)dst[5]).Dispose();
        ((Image)dst[6]).Dispose();

        coll[0]!.RotateFlip(RotateFlipType.Rotate90FlipY);
        coll[1]!.RotateFlip(RotateFlipType.Rotate90FlipY);
    }

    [TestFixture]
    public class ImageListRecreateHandleEventClass : TestHelper
    {
        private static bool eventhandled;
        public static void RecreateHandle_EventHandler(object? sender, EventArgs e)
        {
            eventhandled = true;
        }

        [Test]
        public void RecreateHandleEvenTest()
        {
            var myform = new Form();
            myform.ShowInTaskbar = false;
            var myimagelist = new ImageList();
            var myImage = Image.FromFile(TestResourceHelper.GetFullPathOfResource("Test/resources/M.gif"));
            myimagelist.Images.Add(myImage);
            myimagelist.ColorDepth = ColorDepth.Depth8Bit;
            myimagelist.ImageSize = new Size(50, 50);
            myimagelist.RecreateHandle += RecreateHandle_EventHandler;
            var mygraphics = Graphics.FromHwnd(myform.Handle);
            myimagelist.Draw(mygraphics, new Point(5, 5), 0);
            myimagelist.ImageSize = new Size(100, 100);
            Assert.That((object?)eventhandled, Is.EqualTo(true));
            eventhandled = false;
            myimagelist.Images.Add(myImage);
            myimagelist.ColorDepth = ColorDepth.Depth32Bit;
            Assert.That((object?)eventhandled, Is.EqualTo(true));
            myform.Dispose();
        }
    }
}