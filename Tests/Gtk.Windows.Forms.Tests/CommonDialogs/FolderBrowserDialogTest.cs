//
// FolderBrowserDialogTest.cs: Test cases for FolderBrowserDialog.
//
// Author:
//	Gert Driesen (drieseng@users.sourceforge.net)
//
// (C) 2007 Gert Driesen
//

using GtkTests.Helpers;
using System.ComponentModel;
using System.Windows.Forms;

namespace GtkTests.CommonDialogs;

[TestFixture]
public class FolderBrowserDialogTest : TestHelper
{
    [Test]
    public void Description ()
    {
        var fbd = new FolderBrowserDialog ();
        object expected = string.Empty;
        Assert.That((object?)fbd.Description, Is.EqualTo(expected));
        fbd.Description = null!;
        object expected1 = string.Empty;
        Assert.That((object?)fbd.Description, Is.EqualTo(expected1));
        fbd.Description = "Select a folder";
        Assert.That((object?)fbd.Description, Is.EqualTo("Select a folder"));
        fbd.Description = null!;
        object expected2 = string.Empty;
        Assert.That((object?)fbd.Description, Is.EqualTo(expected2));
    }

    [Test]
    public void SelectedPath ()
    {
        var fbd = new FolderBrowserDialog ();
        object expected = string.Empty;
        Assert.That((object?)fbd.SelectedPath, Is.EqualTo(expected));
        fbd.SelectedPath = null!;
        object expected1 = string.Empty;
        Assert.That((object?)fbd.SelectedPath, Is.EqualTo(expected1));
        fbd.SelectedPath = "{}###()";
        Assert.That((object?)fbd.SelectedPath, Is.EqualTo("{}###()"));
        fbd.SelectedPath = null!;
        object expected2 = string.Empty;
        Assert.That((object?)fbd.SelectedPath, Is.EqualTo(expected2));
    }

    [Test]
    public void ShowNewFolderButton ()
    {
        var fbd = new FolderBrowserDialog ();
        Assert.IsTrue (fbd.ShowNewFolderButton);
        fbd.ShowNewFolderButton = false;
        Assert.IsFalse (fbd.ShowNewFolderButton);
        fbd.ShowNewFolderButton = true;
        Assert.IsTrue (fbd.ShowNewFolderButton);
    }

    [Test]
    public void RootFolder ()
    {
        var fbd = new FolderBrowserDialog ();
        Assert.That((object?)fbd.RootFolder, Is.EqualTo(Environment.SpecialFolder.Desktop));
        fbd.RootFolder = Environment.SpecialFolder.Personal;
        Assert.That((object?)fbd.RootFolder, Is.EqualTo(Environment.SpecialFolder.Personal));
    }

    [Test]
    public void RootFolder_Invalid ()
    {
        Assert.Throws<InvalidEnumArgumentException>(() =>
        {
            var fbd = new FolderBrowserDialog();
            try
            {
                fbd.RootFolder = (Environment.SpecialFolder)666;
            }
            catch (InvalidEnumArgumentException ex)
            {
                object expected = typeof(InvalidEnumArgumentException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                Assert.IsTrue(ex.Message.IndexOf("666", StringComparison.Ordinal) != -1);
                Assert.IsTrue(ex.Message.IndexOf("SpecialFolder", StringComparison.Ordinal) != -1);
                Assert.IsNotNull(ex.ParamName);
                Assert.That((object?)ex.ParamName, Is.EqualTo("value"));
                throw;
            }
        });
    }
}