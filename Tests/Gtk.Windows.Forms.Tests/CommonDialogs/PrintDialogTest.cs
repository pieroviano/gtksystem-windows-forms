//
// PrintDialogTest.cs: Tests for PrintDialog class.
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
//	Carlos Alberto Cortez <calberto.cortez@gmail.com>
//

using GtkTests.Helpers;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace GtkTests.CommonDialogs;

[TestFixture]
public class PrintDialogTest : TestHelper
{
    [Test]
    public void DefaultValues()
    {
        var pd = new PrintDialog();

        Assert.IsTrue(pd.AllowPrintToFile);
        Assert.IsFalse(pd.AllowSelection);
        Assert.IsFalse(pd.AllowSomePages);
        Assert.IsNull(pd.Document);
        Assert.IsNotNull(pd.PrinterSettings);
        Assert.IsFalse(pd.PrintToFile);
        Assert.IsFalse(pd.ShowHelp);
        Assert.IsTrue(pd.ShowNetwork);
    }

    [Test]
    public void DocumentTest()
    {
        var pd = new PrintDialog();

        var pdoc1 = new PrintDocument();
        var ps1 = new PrinterSettings();
        pdoc1.PrinterSettings = ps1;
        pd.Document = pdoc1;
        Assert.That((object?)pd.Document, Is.EqualTo(pdoc1));
        Assert.That((object?)pd.PrinterSettings, Is.EqualTo(ps1));

        var ps2 = new PrinterSettings();
        pdoc1.PrinterSettings = ps2;
        pd.Document = pdoc1;
        Assert.That((object?)pd.Document, Is.EqualTo(pdoc1));
        Assert.That((object?)pd.PrinterSettings, Is.EqualTo(ps2));

        pd.Document = null;
        Assert.IsNull(pd.Document);
        Assert.IsNotNull(pd.PrinterSettings);
        Assert.True(pd.PrinterSettings != ps1);
    }

    [Test]
    public void PrinterSettingsTest()
    {
        var pd = new PrintDialog();

        var ps1 = new PrinterSettings();
        pd.PrinterSettings = ps1;
        Assert.That((object?)pd.PrinterSettings, Is.EqualTo(ps1));
        Assert.IsNull(pd.Document);

        pd.PrinterSettings = null;
        Assert.IsNotNull(pd.PrinterSettings);
        Assert.IsNull(pd.Document);
        Assert.False(pd.PrinterSettings == ps1) ;
    }
}