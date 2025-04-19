//
// CultureTest.cs: Test cases for culture-invariant string convertions
//
// Authors:
//     Robert Jordan <robertj@gmx.net>
//

using System.Globalization;
using System.Drawing;
using System.Resources;
using GtkTests.Helpers;

namespace GtkTests.System.Resources;

[TestFixture]
public class CultureTest : TestHelper
{
    private string? fileName;

    [SetUp]
    protected override void SetUp ()
    {
        fileName = Path.GetTempFileName ();
        base.SetUp ();
    }

    [TearDown]
    protected override void TearDown ()
    {
        if (fileName != null)
        {
            File.Delete(fileName);
        }

        base.TearDown ();
    }

    [Test]
    public void Test ()
    {
        Thread.CurrentThread.CurrentCulture =
            Thread.CurrentThread.CurrentUICulture = new CultureInfo ("de-DE");

        var w = new ResXResourceWriter (fileName);
        w.AddResource ("point", new Point (42, 43));
        w.Generate ();
        w.Close ();

        var count = 0;
        var r = new ResXResourceReader (fileName);
        var e = r.GetEnumerator ();
        using var disposable = e as IDisposable;
        while (e!.MoveNext ()) {
            if ((string) e.Key == "point") {
                object? expected = typeof (Point).FullName;
                Assert.That((object?)e.Value!.GetType().FullName, Is.EqualTo(expected));
                var p = (Point) e.Value;
                Assert.That((object?)p.X, Is.EqualTo(42));
                Assert.That((object?)p.Y, Is.EqualTo(43));
                count++;
            }
        }
        r.Close ();
        Assert.That((object?)count, Is.EqualTo(1));
    }
}