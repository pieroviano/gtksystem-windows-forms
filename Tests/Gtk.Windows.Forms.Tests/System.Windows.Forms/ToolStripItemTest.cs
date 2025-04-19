//
// ToolStripItemTests.cs
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
// Copyright (c) 2006 Jonathan Pobst
//
// Authors:
//	Jonathan Pobst (monkey@jpobst.com)
//

using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using Image = System.Drawing.Image;
using GtkTests.Helpers;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class ToolStripItemTests : TestHelper
{
    [Test]
    public void Constructor()
    {
        ToolStripItem tsi = new NullToolStripItem();

        Assert.That((object?)tsi.AutoToolTip, Is.EqualTo(false));
        Assert.That((object?)tsi.BackColor, Is.EqualTo(SystemColors.Control));
        Assert.That((object?)tsi.BackgroundImage, Is.EqualTo(null));
        Assert.That((object?)tsi.BackgroundImageLayout, Is.EqualTo(ImageLayout.Tile));
        object expected = new Rectangle(0, 0, 23, 23);
        Assert.That((object?)tsi.Bounds, Is.EqualTo(expected));
        Assert.That((object?)tsi.DisplayStyle, Is.EqualTo(ToolStripItemDisplayStyle.ImageAndText));
        Assert.That((object?)tsi.Enabled, Is.EqualTo(true));
        //Assert1.AreEqual(new Font ("Tahoma", 8.25f), tsi.Font);
        Assert.That((object?)tsi.ForeColor, Is.EqualTo(SystemColors.ControlText));
        Assert.That((object?)tsi.Height, Is.EqualTo(23));
        Assert.That((object?)tsi.Image, Is.EqualTo(null));
        Assert.That((object?)tsi.ImageAlign, Is.EqualTo(ContentAlignment.MiddleCenter));
        Assert.That((object?)tsi.ImageIndex, Is.EqualTo(-1));
        object expected1 = string.Empty;
        Assert.That((object?)tsi.ImageKey, Is.EqualTo(expected1), "A22-1");
        Assert.That((object?)tsi.ImageScaling, Is.EqualTo(ToolStripItemImageScaling.SizeToFit));
        Assert.That((object?)tsi.ImageTransparentColor, Is.EqualTo(Color.Empty));
        Assert.That((object?)tsi.MergeAction, Is.EqualTo(MergeAction.Append));
        Assert.That((object?)tsi.MergeIndex, Is.EqualTo(-1));
        object expected2 = string.Empty;
        Assert.That((object?)tsi.Name, Is.EqualTo(expected2));
        Assert.That((object?)tsi.Overflow, Is.EqualTo(ToolStripItemOverflow.AsNeeded));
        Assert.That((object?)tsi.Owner, Is.EqualTo(null));
        Assert.That((object?)tsi.OwnerItem, Is.EqualTo(null));
        object expected3 = new Padding(0);
        Assert.That((object?)tsi.Padding, Is.EqualTo(expected3));
        Assert.That((object?)tsi.Placement, Is.EqualTo(ToolStripItemPlacement.None));
        Assert.That((object?)tsi.Pressed, Is.EqualTo(false));
        Assert.That((object?)tsi.RightToLeft, Is.EqualTo(RightToLeft.Inherit));
        Assert.That((object?)tsi.RightToLeftAutoMirrorImage, Is.EqualTo(false));
        object expected4 = new Size(23, 23);
        Assert.That((object?)tsi.Size, Is.EqualTo(expected4));
        Assert.That(tsi.Tag, Is.EqualTo(null));
        object expected5 = string.Empty;
        Assert.That((object?)tsi.Text, Is.EqualTo(expected5));
        Assert.That((object?)tsi.TextAlign, Is.EqualTo(ContentAlignment.MiddleCenter));
        Assert.That((object?)tsi.TextDirection, Is.EqualTo(ToolStripTextDirection.Horizontal));
        Assert.That((object?)tsi.TextImageRelation, Is.EqualTo(TextImageRelation.ImageBeforeText));
        Assert.That((object?)tsi.ToolTipText, Is.EqualTo(null));
        Assert.That((object?)tsi.Width, Is.EqualTo(23));

    }

    [Test]
    public void PropertyAutoToolTip()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.AutoToolTip = true;
        Assert.That((object?)tsi.AutoToolTip, Is.EqualTo(true));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));

        ew.Clear();
        tsi.AutoToolTip = true;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyBackColor()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.BackColor = Color.BurlyWood;
        Assert.That((object?)tsi.BackColor, Is.EqualTo(Color.BurlyWood));
        Assert.That((object?)ew.ToString(), Is.EqualTo("BackColorChanged"));

        ew.Clear();
        tsi.BackColor = Color.BurlyWood;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyBackgroundImage()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        Image i = new Bitmap(1, 1);
        tsi.BackgroundImage = i;
        Assert.That((object?)tsi.BackgroundImage, Is.SameAs(i));
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));

        ew.Clear();
        tsi.BackgroundImage = i;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.SameAs(expected));
    }

    [Test]
    public void PropertyBackgroundImageLayout()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.BackgroundImageLayout = ImageLayout.Zoom;
        Assert.That((object?)tsi.BackgroundImageLayout, Is.EqualTo(ImageLayout.Zoom));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));

        ew.Clear();
        tsi.BackgroundImageLayout = ImageLayout.Zoom;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyDisplayStyle()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.DisplayStyle = ToolStripItemDisplayStyle.Image;
        Assert.That((object?)tsi.DisplayStyle, Is.EqualTo(ToolStripItemDisplayStyle.Image));
        Assert.That((object?)ew.ToString(), Is.EqualTo("DisplayStyleChanged"));

        ew.Clear();
        tsi.DisplayStyle = ToolStripItemDisplayStyle.Image;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyEnabled()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.Enabled = false;
        Assert.That((object?)tsi.Enabled, Is.EqualTo(false));
        Assert.That((object?)ew.ToString(), Is.EqualTo("EnabledChanged"));

        ew.Clear();
        tsi.Enabled = false;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyFont()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        var f = new Font("Arial", 12);

        tsi.Font = f;
        Assert.That((object?)tsi.Font, Is.SameAs(f));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));

        ew.Clear();
        tsi.Font = f;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyForeColor()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.ForeColor = Color.BurlyWood;
        Assert.That((object?)tsi.ForeColor, Is.EqualTo(Color.BurlyWood));
        Assert.That((object?)ew.ToString(), Is.EqualTo("ForeColorChanged"));

        ew.Clear();
        tsi.ForeColor = Color.BurlyWood;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyHeight()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.Height = 42;
        Assert.That((object?)tsi.Height, Is.EqualTo(42));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));

        ew.Clear();
        tsi.Height = 42;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyImage()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        Image i = new Bitmap(1, 1);
        tsi.Image = i;
        Assert.That((object?)tsi.Image, Is.SameAs(i));
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));

        ew.Clear();
        tsi.Image = i;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.SameAs(expected));
    }

    [Test]
    public void PropertyImageAlign()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.ImageAlign = ContentAlignment.TopRight;
        Assert.That((object?)tsi.ImageAlign, Is.EqualTo(ContentAlignment.TopRight));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));

        ew.Clear();
        tsi.ImageAlign = ContentAlignment.TopRight;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyImageAlignIEAE()
    {
        Assert.Throws<InvalidEnumArgumentException>(() =>
        {
            ToolStripItem tsi = new NullToolStripItem();
            tsi.ImageAlign = (ContentAlignment)42;
        });
    }

    [Test]
    public void PropertyImageIndex()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.ImageIndex = 42;
        Assert.That((object?)tsi.ImageIndex, Is.EqualTo(42));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));

        ew.Clear();
        tsi.ImageIndex = 42;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyImageIndexAE()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            ToolStripItem tsi = new NullToolStripItem();
            tsi.ImageIndex = -2;
        });
    }

    [Test]
    public void PropertyImageKey()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.ImageKey = "open";
        Assert.That((object?)tsi.ImageKey, Is.EqualTo("open"));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));

        ew.Clear();
        tsi.ImageKey = "open";
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyImageScaling()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.ImageScaling = ToolStripItemImageScaling.None;
        Assert.That((object?)tsi.ImageScaling, Is.EqualTo(ToolStripItemImageScaling.None));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));

        ew.Clear();
        tsi.ImageScaling = ToolStripItemImageScaling.None;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyImageTransparentColor()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.ImageTransparentColor = Color.BurlyWood;
        Assert.That((object?)tsi.ImageTransparentColor, Is.EqualTo(Color.BurlyWood));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));

        ew.Clear();
        tsi.ImageTransparentColor = Color.BurlyWood;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyMergeAction()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.MergeAction = MergeAction.Replace;
        Assert.That((object?)tsi.MergeAction, Is.EqualTo(MergeAction.Replace));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));

        ew.Clear();
        tsi.MergeAction = MergeAction.Replace;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyMergeActionIEAE()
    {
        Assert.Throws<InvalidEnumArgumentException>(() =>
        {
            ToolStripItem tsi = new NullToolStripItem();
            tsi.MergeAction = (MergeAction)42;
        });
    }

    [Test]
    public void PropertyMergeIndex()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.MergeIndex = 42;
        Assert.That((object?)tsi.MergeIndex, Is.EqualTo(42));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));

        ew.Clear();
        tsi.MergeIndex = 42;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyName()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.Name = "MyName";
        Assert.That((object?)tsi.Name, Is.EqualTo("MyName"));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));

        ew.Clear();
        tsi.Name = "MyName";
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyOverflow()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.Overflow = ToolStripItemOverflow.Never;
        Assert.That((object?)tsi.Overflow, Is.EqualTo(ToolStripItemOverflow.Never));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));

        ew.Clear();
        tsi.Overflow = ToolStripItemOverflow.Never;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyOverflowIEAE()
    {
        Assert.Throws<InvalidEnumArgumentException>(() =>
        {
            ToolStripItem tsi = new NullToolStripItem();
            tsi.Overflow = (ToolStripItemOverflow)42;
        });
    }

    [Test]
    public void PropertyOwner()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        var ts = new ToolStrip();
        tsi.Owner = ts;
        Assert.That((object?)tsi.Owner, Is.SameAs(ts));
        Assert.That((object?)ew.ToString(), Is.EqualTo("OwnerChanged"));

        ew.Clear();
        tsi.Owner = ts;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.SameAs(expected));
    }

    [Test]
    public void PropertyPadding()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.Padding = new Padding(6);
        object expected = new Padding(6);
        Assert.That((object?)tsi.Padding, Is.EqualTo(expected));
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));

        ew.Clear();
        tsi.Padding = new Padding(6);
        object expected2 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected2));
    }

    [Test]
    public void PropertyRightToLeft()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.RightToLeft = RightToLeft.No;
        Assert.That((object?)tsi.RightToLeft, Is.EqualTo(RightToLeft.No));
        Assert.That((object?)ew.ToString(), Is.EqualTo("RightToLeftChanged"));

        ew.Clear();
        tsi.RightToLeft = RightToLeft.No;
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyRightToLeftAutoMirrorImage()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.RightToLeftAutoMirrorImage = true;
        Assert.That((object?)tsi.RightToLeftAutoMirrorImage, Is.EqualTo(true));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));

        ew.Clear();
        tsi.RightToLeftAutoMirrorImage = true;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertySize()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.Size = new Size(42, 42);
        object expected = new Size(42, 42);
        Assert.That((object?)tsi.Size, Is.EqualTo(expected));
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));

        ew.Clear();
        tsi.Size = new Size(42, 42);
        object expected2 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected2));
    }

    [Test]
    public void PropertyTag()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.Tag = "tag";
        Assert.That(tsi.Tag, Is.SameAs("tag"));
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));

        ew.Clear();
        tsi.Tag = "tag";
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.SameAs(expected));
    }

    [Test]
    public void PropertyText()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.Text = "Text";
        Assert.That((object?)tsi.Text, Is.EqualTo("Text"));
        Assert.That((object?)ew.ToString(), Is.EqualTo("TextChanged"));

        ew.Clear();
        tsi.Text = "Text";
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void PropertyTextAlign()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.TextAlign = ContentAlignment.TopRight;
        Assert.That((object?)tsi.TextAlign, Is.EqualTo(ContentAlignment.TopRight));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));

        ew.Clear();
        tsi.TextAlign = ContentAlignment.TopRight;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyTextAlignIEAE()
    {
        Assert.Throws<InvalidEnumArgumentException>(() =>
        {
            ToolStripItem tsi = new NullToolStripItem();
            tsi.TextAlign = (ContentAlignment)42;
        });
    }

    [Test]
    public void PropertyTextImageRelation()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.TextImageRelation = TextImageRelation.Overlay;
        Assert.That((object?)tsi.TextImageRelation, Is.EqualTo(TextImageRelation.Overlay));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));

        ew.Clear();
        tsi.TextImageRelation = TextImageRelation.Overlay;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyToolTipText()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.ToolTipText = "Text";
        Assert.That((object?)tsi.ToolTipText, Is.EqualTo("Text"));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));

        ew.Clear();
        tsi.ToolTipText = "Text";
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));
    }

    [Test]
    public void PropertyWidth()
    {
        ToolStripItem tsi = new NullToolStripItem();
        var ew = new EventWatcher(tsi);

        tsi.Width = 42;
        Assert.That((object?)tsi.Width, Is.EqualTo(42));
        object expected = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected));

        ew.Clear();
        tsi.Width = 42;
        object expected1 = string.Empty;
        Assert.That((object?)ew.ToString(), Is.EqualTo(expected1));
    }

    [Test]
    public void MethodDispose()
    {
        var ts = new ToolStrip();
        var tsi = new NullToolStripItem();

        ts.Items.Add(tsi);

        Assert.That((object?)ts.Items.Count, Is.EqualTo(1));
        Assert.That((object?)tsi.Owner, Is.EqualTo(ts));

        tsi.Dispose();
        Assert.That((object?)ts.Items.Count, Is.EqualTo(0));
        Assert.That((object?)tsi.Owner, Is.EqualTo(null));
    }

    [Test]
    public void BehaviorBackColor()
    {
        var ts = new ToolStrip();
        ToolStripItem tsi = new NullToolStripItem();

        ts.Items.Add(tsi);

        Assert.That((object?)ts.BackColor, Is.EqualTo(SystemColors.Control));
        Assert.That((object?)tsi.BackColor, Is.EqualTo(SystemColors.Control));

        ts.BackColor = Color.BlueViolet;

        Assert.That((object?)ts.BackColor, Is.EqualTo(Color.BlueViolet));
        Assert.That((object?)tsi.BackColor, Is.EqualTo(SystemColors.Control));

        tsi.BackColor = Color.Snow;

        Assert.That((object?)ts.BackColor, Is.EqualTo(Color.BlueViolet));
        Assert.That((object?)tsi.BackColor, Is.EqualTo(Color.Snow));

    }

    [Test]
    public void BehaviorEnabled()
    {
        var ts = new ToolStrip();
        ToolStripItem tsi = new NullToolStripItem();

        ts.Items.Add(tsi);

        Assert.That((object?)ts.Enabled, Is.EqualTo(true));
        Assert.That((object?)tsi.Enabled, Is.EqualTo(true));

        tsi.Enabled = false;

        Assert.That((object?)ts.Enabled, Is.EqualTo(true));
        Assert.That((object?)tsi.Enabled, Is.EqualTo(false));

        ts.Enabled = false;

        Assert.That((object?)ts.Enabled, Is.EqualTo(false));
        Assert.That((object?)tsi.Enabled, Is.EqualTo(false));

        tsi.Enabled = true;

        Assert.That((object?)ts.Enabled, Is.EqualTo(false));
        Assert.That((object?)tsi.Enabled, Is.EqualTo(false));
    }

    [Test]
    public void BehaviorImageList()
    {
        // Basically, this shows that whichever of [Image|ImageIndex|ImageKey]
        // is set last resets the others to their default state
        ToolStripItem tsi = new NullToolStripItem();

        var i1 = new Bitmap(16, 16);
        i1.SetPixel(0, 0, Color.Blue);
        var i2 = new Bitmap(16, 16);
        i2.SetPixel(0, 0, Color.Red);
        var i3 = new Bitmap(16, 16);
        i3.SetPixel(0, 0, Color.Green);

        Assert.That((object?)tsi.Image, Is.EqualTo(null));
        Assert.That((object?)tsi.ImageIndex, Is.EqualTo(-1));
        object expected = string.Empty;
        Assert.That((object?)tsi.ImageKey, Is.EqualTo(expected));

        var il = new ImageList();
        il.Images.Add("i2", i2);
        il.Images.Add("i3", i3);

        var ts = new ToolStrip();

        ts.Items.Add(tsi);

        tsi.ImageKey = "i3";
        Assert.That((object?)tsi.ImageIndex, Is.EqualTo(-1));
        Assert.That((object?)tsi.ImageKey, Is.EqualTo("i3"));
        object expected1 = i3.GetPixel(0, 0);
        Assert.That((object?)(tsi.Image as Bitmap)!.GetPixel(0, 0), Is.EqualTo(expected1));

        tsi.ImageIndex = 0;
        Assert.That((object?)tsi.ImageIndex, Is.EqualTo(0));
        object expected2 = string.Empty;
        Assert.That((object?)tsi.ImageKey, Is.EqualTo(expected2));
        object expected3 = i2.GetPixel(0, 0);
        Assert.That((object?)(tsi.Image as Bitmap)!.GetPixel(0, 0), Is.EqualTo(expected3));

        tsi.Image = i1;
        Assert.That((object?)tsi.ImageIndex, Is.EqualTo(-1));
        object expected4 = string.Empty;
        Assert.That((object?)tsi.ImageKey, Is.EqualTo(expected4));
        object expected5 = i1.GetPixel(0, 0);
        Assert.That((object?)(tsi.Image as Bitmap)!.GetPixel(0, 0), Is.EqualTo(expected5));

        tsi.Image = null;
        Assert.That((object?)tsi.Image, Is.EqualTo(null));
        Assert.That((object?)tsi.ImageIndex, Is.EqualTo(-1));
        object expected6 = string.Empty;
        Assert.That((object?)tsi.ImageKey, Is.EqualTo(expected6));

        // Also, Image is not cached, changing the underlying ImageList image is reflected
        tsi.ImageIndex = 0;
        il.Images[0] = i1;
        object expected7 = i1.GetPixel(0, 0);
        Assert.That((object?)(tsi.Image as Bitmap)!.GetPixel(0, 0), Is.EqualTo(expected7));
    }

    [Test]	// This should not crash
    public void BehaviorImageListBadIndex()
    {
        var f = new Form();
        var ts = new ToolStrip();
        ts.Items.Add("Hey").ImageIndex = 3;

        var i = ts.Items[0].Image;

        f.Controls.Add(ts);

        f.Show();
        f.Dispose();
    }

    private class EventWatcher
    {
        private string events = string.Empty;

        public EventWatcher(ToolStripItem tsi)
        {
            tsi.Click += delegate { events += ("Click;"); };
        }

        public override string ToString()
        {
            return events.TrimEnd(';');
        }

        public void Clear()
        {
            events = string.Empty;
        }
    }

    private class NullToolStripItem : ToolStripItem
    {
        public NullToolStripItem() : base() { }
        public NullToolStripItem(string text, Image image, EventHandler onClick) : base(text, image, onClick) { }
        public NullToolStripItem(string text, Image image, EventHandler onClick, string name) : base(text, image, onClick, name) { }
    }

}