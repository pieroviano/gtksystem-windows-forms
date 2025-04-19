//
// CommonDialogsTest.cs: Tests for common dialogs.
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
// Copyright (c) 2006 Novell, Inc. (http://www.novell.com)
//
// Authors:
//	Alexander Olk <alex.olk@googlemail.com>
//

using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using GtkTests.Helpers;

namespace GtkTests.CommonDialogs;

[TestFixture]
public class CommonDialogsTest : TestHelper
{
    private OpenFileDialog? ofd;
#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
    private SaveFileDialog? sfd;
    private FontDialog? fd;
#pragma warning restore CS0649 // Field is never assigned to, and will always have its default value
    private FolderBrowserDialog? fbd;
    private ColorDialog? cd;

    [TearDown]
    protected override void TearDown()
    {
        ofd?.Dispose();
        sfd?.Dispose();
        fd?.Dispose();
        fbd?.Dispose();
        cd?.Dispose();
    }

    [Test]
    public void ColorDialogTest()
    {
        cd = new ColorDialog();

        Assert.That((object?)cd.Color, Is.EqualTo(Color.Black));
        Assert.IsTrue(cd.AllowFullOpen);
        Assert.IsFalse(cd.AnyColor);
        Assert.IsFalse(cd.FullOpen);
        Assert.IsNotNull(cd.CustomColors);
        Assert.IsFalse(cd.ShowHelp);
        Assert.IsFalse(cd.SolidColorOnly);
        Assert.That((object?)cd.ToString(), Is.EqualTo("System.Windows.Forms.ColorDialog,  Color: Color [Black]"));

        cd.Color = Color.Red;
        Assert.That((object?)cd.Color, Is.EqualTo(Color.Red));

        cd.AllowFullOpen = false;
        cd.FullOpen = true;
        Assert.IsTrue(cd.FullOpen);

        var custom_colors = new int[] { Color.Yellow.ToArgb(), Color.Red.ToArgb() };
        cd.CustomColors = custom_colors;
        Assert.IsNotNull(cd.CustomColors, "#10a");
        Assert.That((object?)cd.CustomColors.Length, Is.EqualTo(16), "#10aa");
        object expected = Color.Red.ToArgb();
        Assert.That((object?)cd.CustomColors[1], Is.EqualTo(expected), "#10ab");
        object expected1 = Color.FromArgb(0, 255, 255, 255).ToArgb();
        Assert.That((object?)cd.CustomColors[15], Is.EqualTo(expected1), "#10ac");

        cd.CustomColors = null!;
        Assert.IsNotNull(cd.CustomColors, "#10b");
        Assert.That((object?)cd.CustomColors.Length, Is.EqualTo(16), "#10bb");
        object expected2 = Color.FromArgb(0, 255, 255, 255).ToArgb();
        Assert.That((object?)cd.CustomColors[0], Is.EqualTo(expected2), "#10bc");

        cd.AllowFullOpen = true;
        cd.CustomColors = custom_colors;
        Assert.IsNotNull(cd.CustomColors, "#10c");
        Assert.That((object?)cd.CustomColors.Length, Is.EqualTo(16), "#10cc");
    }

    [Test]
    public void OpenFileDialogTest()
    {
        ofd = new OpenFileDialog();

        Assert.IsTrue(ofd.AddExtension);
        Assert.IsTrue(ofd.CheckFileExists);
        Assert.IsTrue(ofd.CheckPathExists);
        Assert.That((object?)ofd.DefaultExt, Is.EqualTo(string.Empty));
        Assert.IsTrue(ofd.DereferenceLinks);
        Assert.That((object?)ofd.FileName, Is.EqualTo(string.Empty));
        Assert.IsNotNull(ofd.FileNames);
        Assert.That((object?)ofd.FileNames.Length, Is.EqualTo(0), "#17a");
        Assert.That((object?)ofd.Filter, Is.EqualTo(string.Empty));
        Assert.That((object?)ofd.FilterIndex, Is.EqualTo(1));
        Assert.That((object?)ofd.InitialDirectory, Is.EqualTo(string.Empty));
        Assert.IsFalse(ofd.Multiselect);
        Assert.IsFalse(ofd.ReadOnlyChecked);
        Assert.IsFalse(ofd.RestoreDirectory);
        Assert.IsFalse(ofd.ShowHelp);
        Assert.IsFalse(ofd.ShowReadOnly);
        Assert.That((object?)ofd.Title, Is.EqualTo(string.Empty));
        Assert.IsTrue(ofd.ValidateNames);
        Assert.That((object?)ofd.ToString(), Is.EqualTo("System.Windows.Forms.OpenFileDialog: Title: , FileName: "));

        ofd.DefaultExt = ".TXT";
        Assert.That((object?)ofd.DefaultExt, Is.EqualTo("TXT"));

        ofd.Filter = null!;
        Assert.That((object?)ofd.Filter, Is.EqualTo(string.Empty));

        ofd.Filter = "Text (*.txt)|*.txt|All (*.*)|*.*";

        Assert.Throws<ArgumentException>(() =>
        {
            ofd.Filter = "abcd";
        });

        Assert.That((object?)ofd.Filter, Is.EqualTo("Text (*.txt)|*.txt|All (*.*)|*.*"), "#30a");

        ofd.FilterIndex = 10;
        Assert.That((object?)ofd.FilterIndex, Is.EqualTo(10), "#30aa");

        ofd.Filter = null!;
        Assert.That((object?)ofd.Filter, Is.EqualTo(string.Empty), "#30b");
        Assert.That((object?)ofd.FilterIndex, Is.EqualTo(10), "#30ba");

        var current_path = Environment.CurrentDirectory;
        var current_file = Path.Combine(current_path, "test_file");
        if (!File.Exists(current_file))
            File.Create(current_file);

        ofd.FileName = current_file;

        Assert.That((object?)ofd.FileName, Is.EqualTo(current_file));

        string[] file_names = ofd.FileNames;
        Assert.That((object?)file_names[0], Is.EqualTo(current_file));

        ofd.Title = "Test";
        object expected = "System.Windows.Forms.OpenFileDialog: Title: Test, FileName: " + current_file;
        Assert.That((object?)ofd.ToString(), Is.EqualTo(expected));

        ofd.FileName = null!;
        Assert.That((object?)ofd.FileName, Is.EqualTo(string.Empty), "#33a");
        Assert.IsNotNull(ofd.FileNames, "#33b");
        Assert.That((object?)ofd.FileNames.Length, Is.EqualTo(0), "#33c");

        ofd.Reset();

        // check again
        Assert.IsTrue(ofd.AddExtension);
        Assert.IsTrue(ofd.CheckFileExists);
        Assert.IsTrue(ofd.CheckPathExists);
        Assert.That((object?)ofd.DefaultExt, Is.EqualTo(string.Empty));
        Assert.IsTrue(ofd.DereferenceLinks);
        Assert.That((object?)ofd.FileName, Is.EqualTo(string.Empty));
        Assert.IsNotNull(ofd.FileNames);
        Assert.That((object?)ofd.Filter, Is.EqualTo(string.Empty));
        Assert.That((object?)ofd.FilterIndex, Is.EqualTo(1));
        Assert.That((object?)ofd.InitialDirectory, Is.EqualTo(string.Empty));
        Assert.IsFalse(ofd.Multiselect);
        Assert.IsFalse(ofd.ReadOnlyChecked);
        Assert.IsFalse(ofd.RestoreDirectory);
        Assert.IsFalse(ofd.ShowHelp);
        Assert.IsFalse(ofd.ShowReadOnly);
        Assert.That((object?)ofd.Title, Is.EqualTo(string.Empty));
        Assert.IsTrue(ofd.ValidateNames);
        Assert.That((object?)ofd.ToString(), Is.EqualTo("System.Windows.Forms.OpenFileDialog: Title: , FileName: "));
    }

    [Test]
    public void FileDialogFilterArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            if (ofd == null)
                ofd = new OpenFileDialog();

            ofd.Filter = "xyafj";
        });
    }

    [Test]
    [NUnit.Framework.Category("NotWorking")]
    public void FolderBrowserDialogTest()
    {
        fbd = new FolderBrowserDialog();

        Assert.That((object?)fbd.Description, Is.EqualTo(string.Empty));

        Assert.That((object?)fbd.RootFolder, Is.EqualTo(Environment.SpecialFolder.Desktop));

        Assert.That((object?)fbd.SelectedPath, Is.EqualTo(string.Empty));

        Assert.IsTrue(fbd.ShowNewFolderButton);

        Assert.That((object?)fbd.ToString(), Is.EqualTo("System.Windows.Forms.FolderBrowserDialog"));

        var current_path = Environment.CurrentDirectory;
        fbd.SelectedPath = current_path;

        Assert.That((object?)fbd.SelectedPath, Is.EqualTo(current_path));
    }

    [Test]
    public void FolderBrowserDialogInvalidEnumArgumentExceptionTest()
    {
        Assert.Throws<InvalidEnumArgumentException>(() =>
        {
            if (fbd == null)
                fbd = new FolderBrowserDialog();

            fbd.RootFolder = (Environment.SpecialFolder)12;
        });
    }

    [Test]
    public void CommonDialogPropertyTag()
    {
        var md = new MyDialog();
        object s = "MyString";

        Assert.That(md.Tag, Is.EqualTo(null));

        md.Tag = s;
        Assert.That(md.Tag, Is.SameAs(s));
    }

    private class MyDialog : CommonDialog
    {
        public override void Reset()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override bool RunDialog(IWin32Window? owner)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}