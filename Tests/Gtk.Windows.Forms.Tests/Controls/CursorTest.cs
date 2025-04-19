//
// Copyright (c) 2005 Novell, Inc.
//
// Authors:
//      Miguel de Icaza
//

using System.Windows.Forms;
using GtkTests.Helpers;

namespace GtkTests.Controls;

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

        var c = Cursor.FromStream(new MemoryStream(Properties.Resources.a));
        Assert.NotNull(c);
    }

    [Test]
    public void CursorPropertyTag()
    {
        var md = Cursor.Current;
        object s = "MyString";

        Assert.That(md.Tag, Is.EqualTo(null));

        md.Tag = s;
        Assert.That(md.Tag, Is.SameAs(s));
    }
}