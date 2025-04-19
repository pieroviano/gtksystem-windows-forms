//
// SaveFileDialogTest.cs: Tests for SaveFileDialog class.
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
// Copyright (c) 2007 Gert Driesen
//
// Authors:
//	Gert Driesen (drieseng@user.sourceforge.net)
//

using GtkTests.Helpers;
using System.Text;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class SaveFileDialogTest : TestHelper
{
    [Test]
    public void AddExtension()
    {
        var sfd = new SaveFileDialog();
        Assert.IsTrue(sfd.AddExtension);
        sfd.AddExtension = false;
        Assert.IsFalse(sfd.AddExtension);
    }

    [Test]
    public void CheckFileExists()
    {
        var sfd = new SaveFileDialog();
        Assert.IsFalse(sfd.CheckFileExists);
        sfd.CheckFileExists = true;
        Assert.IsTrue(sfd.CheckFileExists);
    }

    [Test]
    public void CheckPathExists()
    {
        var sfd = new SaveFileDialog();
        Assert.IsTrue(sfd.CheckPathExists);
        sfd.CheckPathExists = false;
        Assert.IsFalse(sfd.CheckPathExists);
    }

    [Test]
    public void DefaultExt()
    {
        var sfd = new SaveFileDialog();
        Assert.IsNotNull(sfd.DefaultExt);
        object expected = string.Empty;
        Assert.That((object?)sfd.DefaultExt, Is.EqualTo(expected));

        sfd.DefaultExt = "txt";
        Assert.IsNotNull(sfd.DefaultExt);
        Assert.That((object?)sfd.DefaultExt, Is.EqualTo("txt"));

        sfd.DefaultExt = null!;
        Assert.IsNotNull(sfd.DefaultExt);
        object expected1 = string.Empty;
        Assert.That((object?)sfd.DefaultExt, Is.EqualTo(expected1));

        sfd.DefaultExt = ".Xml";
        Assert.IsNotNull(sfd.DefaultExt);
        Assert.That((object?)sfd.DefaultExt, Is.EqualTo("Xml"));

        sfd.DefaultExt = ".tar.gz";
        Assert.IsNotNull(sfd.DefaultExt);
        Assert.That((object?)sfd.DefaultExt, Is.EqualTo("tar.gz"));

        sfd.DefaultExt = "..Xml";
        Assert.IsNotNull(sfd.DefaultExt);
        Assert.That((object?)sfd.DefaultExt, Is.EqualTo(".Xml"));

        sfd.DefaultExt = "tar.gz";
        Assert.IsNotNull(sfd.DefaultExt);
        Assert.That((object?)sfd.DefaultExt, Is.EqualTo("tar.gz"));

        sfd.DefaultExt = ".";
        Assert.IsNotNull(sfd.DefaultExt);
        object expected2 = string.Empty;
        Assert.That((object?)sfd.DefaultExt, Is.EqualTo(expected2));
    }

    [Test]
    public void DereferenceLinks()
    {
        var sfd = new SaveFileDialog();
        Assert.IsTrue(sfd.DereferenceLinks);
        sfd.DereferenceLinks = false;
        Assert.IsFalse(sfd.DereferenceLinks);
    }

    [Test]
    public void FileName()
    {
        var sfd = new SaveFileDialog();
        Assert.IsNotNull(sfd.FileName);
        object expected = string.Empty;
        Assert.That((object?)sfd.FileName, Is.EqualTo(expected));

        sfd.FileName = "default.build";
        Assert.IsNotNull(sfd.FileName);
        Assert.That((object?)sfd.FileName, Is.EqualTo("default.build"));

        sfd.FileName = null!;
        Assert.IsNotNull(sfd.FileName);
        object expected1 = string.Empty;
        Assert.That((object?)sfd.FileName, Is.EqualTo(expected1));

        sfd.FileName = string.Empty;
        Assert.IsNotNull(sfd.FileName);
        object expected2 = string.Empty;
        Assert.That((object?)sfd.FileName, Is.EqualTo(expected2));
    }

    [Test]
    public void FileName_InvalidPathCharacter()
    {
        var sfd = new SaveFileDialog();
        sfd.FileName = Path.InvalidPathChars[0] + "file";
        Assert.IsNotNull(sfd.FileName);
        object expected = Path.InvalidPathChars[0] + "file";
        Assert.That((object?)sfd.FileName, Is.EqualTo(expected));
    }

    [Test]
    public void FileNames()
    {
        var sfd = new SaveFileDialog();
        Assert.IsNotNull(sfd.FileNames);
        Assert.That((object?)sfd.FileNames.Length, Is.EqualTo(0));

        sfd.FileName = "default.build";
        Assert.IsNotNull(sfd.FileNames);
        Assert.That((object?)sfd.FileNames.Length, Is.EqualTo(1));
        Assert.That((object?)sfd.FileNames[0], Is.EqualTo("default.build"));

        sfd.FileName = null!;
        Assert.IsNotNull(sfd.FileNames);
        Assert.That((object?)sfd.FileNames.Length, Is.EqualTo(0));
    }

    [Test]
    public void FileNames_InvalidPathCharacter()
    {
        var sfd = new SaveFileDialog();
        sfd.FileName = Path.InvalidPathChars[0] + "file";
        Assert.IsNotNull(sfd.FileNames);
        Assert.That((object?)sfd.FileNames.Length, Is.EqualTo(1));
        object expected = Path.InvalidPathChars[0] + "file";
        Assert.That((object?)sfd.FileNames[0], Is.EqualTo(expected));
    }

    [Test]
    public void Filter()
    {
        var sfd = new SaveFileDialog();
        Assert.IsNotNull(sfd.Filter);
        object expected = string.Empty;
        Assert.That((object?)sfd.Filter, Is.EqualTo(expected));

        sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        Assert.IsNotNull(sfd.Filter);
        Assert.That((object?)sfd.Filter, Is.EqualTo("Text files (*.txt)|*.txt|All files (*.*)|*.*"));

        sfd.Filter = null!;
        Assert.IsNotNull(sfd.Filter);
        object expected1 = string.Empty;
        Assert.That((object?)sfd.Filter, Is.EqualTo(expected1));

        sfd.Filter = string.Empty;
        Assert.IsNotNull(sfd.Filter);
        object expected2 = string.Empty;
        Assert.That((object?)sfd.Filter, Is.EqualTo(expected2));
    }

    [Test]
    public void Filter_InvalidFormat()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var sfd = new SaveFileDialog();
            try
            {
                sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)";
            }
            catch (ArgumentException ex)
            {
                // The provided filter string is invalid. The filter string
                // should contain a description of the filter, followed by the
                // vertical bar (|) and the filter pattern. The strings for
                // different filtering options should also be separated by the
                // vertical bar. Example: "Text files (*.txt)|*.txt|All files
                // (*.*)|*.*"
                object expected = typeof(ArgumentException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                Assert.IsNull(ex.ParamName);
                throw;
            }
        });
    }

    [Test]
    public void FilterIndex()
    {
        var sfd = new SaveFileDialog();
        Assert.That((object?)sfd.FilterIndex, Is.EqualTo(1));
        sfd.FilterIndex = 99;
        Assert.That((object?)sfd.FilterIndex, Is.EqualTo(99));
        sfd.FilterIndex = -5;
        Assert.That((object?)sfd.FilterIndex, Is.EqualTo(-5));
    }

    [Test]
    public void InitialDirectory()
    {
        var sfd = new SaveFileDialog();
        Assert.IsNotNull(sfd.InitialDirectory);
        object expected = string.Empty;
        Assert.That((object?)sfd.InitialDirectory, Is.EqualTo(expected));

        sfd.InitialDirectory = Path.GetTempPath();
        Assert.IsNotNull(sfd.InitialDirectory);
        object expected1 = Path.GetTempPath();
        Assert.That((object?)sfd.InitialDirectory, Is.EqualTo(expected1));

        sfd.InitialDirectory = null!;
        Assert.IsNotNull(sfd.InitialDirectory);
        object expected2 = string.Empty;
        Assert.That((object?)sfd.InitialDirectory, Is.EqualTo(expected2));

        var initialDir = Path.Combine(Path.GetTempPath(),
            "doesnotexistforsure");
        sfd.InitialDirectory = initialDir;
        Assert.IsNotNull(sfd.InitialDirectory);
        Assert.That((object?)sfd.InitialDirectory, Is.EqualTo(initialDir));

    }

    [Test]
    public void Reset()
    {
        var sfd = new SaveFileDialog();
        sfd.AddExtension = false;
        sfd.CheckFileExists = true;
        sfd.CheckPathExists = false;
        sfd.DefaultExt = "txt";
        sfd.DereferenceLinks = false;
        sfd.FileName = "default.build";
        sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        sfd.FilterIndex = 5;
        sfd.InitialDirectory = Path.GetTempPath();
        sfd.RestoreDirectory = true;
        sfd.ShowHelp = true;
        sfd.Title = "Saving";
        sfd.ValidateNames = false;
        sfd.Reset();

        Assert.IsTrue(sfd.AddExtension);
        Assert.IsFalse(sfd.CheckFileExists);
        Assert.IsTrue(sfd.CheckPathExists);
        Assert.IsNotNull(sfd.DefaultExt);
        object expected = string.Empty;
        Assert.That((object?)sfd.DefaultExt, Is.EqualTo(expected));
        Assert.IsTrue(sfd.DereferenceLinks);
        Assert.IsNotNull(sfd.FileName);
        object expected1 = string.Empty;
        Assert.That((object?)sfd.FileName, Is.EqualTo(expected1));
        Assert.IsNotNull(sfd.FileNames);
        Assert.That((object?)sfd.FileNames.Length, Is.EqualTo(0));
        Assert.IsNotNull(sfd.Filter);
        object expected2 = string.Empty;
        Assert.That((object?)sfd.Filter, Is.EqualTo(expected2));
        Assert.That((object?)sfd.FilterIndex, Is.EqualTo(1));
        Assert.IsNotNull(sfd.InitialDirectory);
        object expected3 = string.Empty;
        Assert.That((object?)sfd.InitialDirectory, Is.EqualTo(expected3));
        Assert.IsFalse(sfd.RestoreDirectory);
        Assert.IsFalse(sfd.ShowHelp);
        Assert.IsNotNull(sfd.Title);
        object expected4 = string.Empty;
        Assert.That((object?)sfd.Title, Is.EqualTo(expected4));
        Assert.IsTrue(sfd.ValidateNames);
    }

    [Test]
    public void RestoreDirectory()
    {
        var sfd = new SaveFileDialog();
        Assert.IsFalse(sfd.RestoreDirectory);
        sfd.RestoreDirectory = true;
        Assert.IsTrue(sfd.RestoreDirectory);
    }

    [Test]
    public void ShowHelp()
    {
        var sfd = new SaveFileDialog();
        Assert.IsFalse(sfd.ShowHelp);
        sfd.ShowHelp = true;
        Assert.IsTrue(sfd.ShowHelp);
    }

    [Test]
    public void Title()
    {
        var sfd = new SaveFileDialog();
        Assert.IsNotNull(sfd.Title);
        object expected = string.Empty;
        Assert.That((object?)sfd.Title, Is.EqualTo(expected));

        sfd.Title = "Saving";
        Assert.IsNotNull(sfd.Title);
        Assert.That((object?)sfd.Title, Is.EqualTo("Saving"));

        sfd.Title = null!;
        Assert.IsNotNull(sfd.Title);
        object expected1 = string.Empty;
        Assert.That((object?)sfd.Title, Is.EqualTo(expected1));
    }

    [Test]
    public void ToStringTest()
    {
        var sfd = new SaveFileDialog();
        sfd.CheckFileExists = true;
        sfd.DefaultExt = "txt";
        sfd.DereferenceLinks = false;
        sfd.FileName = "default.build";
        sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        sfd.FilterIndex = 5;
        sfd.InitialDirectory = Path.GetTempPath();
        sfd.RestoreDirectory = true;
        sfd.ShowHelp = true;
        sfd.Title = "Saving";
        sfd.ValidateNames = false;

        var sb = new StringBuilder();
        sb.Append(typeof(SaveFileDialog).FullName);
        sb.Append(": Title: ");
        sb.Append(sfd.Title);
        sb.Append(", FileName: ");
        sb.Append(sfd.FileName);

        Assert.That((object?)sfd.ToString(), Is.EqualTo(sb.ToString()));

        sfd.FileName = null!;
        sfd.Title = null!;

        sb.Length = 0;
        sb.Append(typeof(SaveFileDialog).FullName);
        sb.Append(": Title: ");
        sb.Append(sfd.Title);
        sb.Append(", FileName: ");
        sb.Append(sfd.FileName);

        Assert.That((object?)sfd.ToString(), Is.EqualTo(sb.ToString()));
    }

    [Test]
    public void ValidateNames()
    {
        var sfd = new SaveFileDialog();
        Assert.IsTrue(sfd.ValidateNames);
        sfd.ValidateNames = false;
        Assert.IsFalse(sfd.ValidateNames);
    }
}