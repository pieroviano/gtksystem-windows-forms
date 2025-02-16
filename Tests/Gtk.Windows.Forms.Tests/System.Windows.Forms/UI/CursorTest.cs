//
// Copyright (c) 2005 Novell, Inc.
//
// Authors:
//      Miguel de Icaza
//

using System.Windows.Forms;
using GtkTests.Helpers;

namespace GtkTests.System.Windows.Forms.UI;

[TestFixture]
public class CursorTest : TestHelper
{
    [Test]
    public void LoadCursorKind2()
    {
        //
        // This test tries to load a cursor with type 1
        // this contains an and mask, it used to crash
        //

        using var memoryStream = new MemoryStream(Properties.Resources.a);
        var c = new Cursor(memoryStream);
        Assert.NotNull(c);
    }

    [Test]
    public void CursorPropertyTag()
    {
        using var memoryStream = new MemoryStream(Properties.Resources.a);
        Cursor.Current = new Cursor(memoryStream);
        var md = Cursor.Current;
        object s = "MyString";

        Assert.AreEqual(null, md?.Tag, "A1");

        md.Tag = s;
        Assert.AreSame(s, md.Tag, "A2");
    }
}