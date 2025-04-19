//
// SelectionRangeTest.cs
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
// Copyright (c) 2008 Andy Hume
//
// Authors:
//   	Andy Hume  <andyhume32@yahoo.co.uk>

using GtkTests.Helpers;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class SelectionRangeTest : TestHelper
{

    [Test]
    public void DefaultConstructor ()
    {
        var sr = new SelectionRange ();
        Assert.That((object?)sr.Start, Is.EqualTo(DateTime.MinValue), "Start");
        // "9999-12-31 00:00:00", note not 23:59:59.
        Assert.That((object?)sr.End, Is.EqualTo(DateTime.MaxValue.Date), "End");

        Assert.That((object?)sr.Start.Kind, Is.EqualTo(DateTimeKind.Unspecified), "Start Kind");
        Assert.That((object?)sr.End.Kind, Is.EqualTo(DateTimeKind.Unspecified), "End Kind");
    }

    [Test]
    public void DefaultConstructor_ToString ()
    {
        var sr = new SelectionRange ();
        // "9999-12-31 00:00:00", note not 23:59:59.
        object expected = $"SelectionRange: Start: {new DateTime(1, 1, 1).ToString()}, End: {new DateTime(9999, 12, 31).ToString()}";
        Assert.That((object?)sr.ToString (), Is.EqualTo(expected), "ToString");
    }

    [Test]
    public void TwoDatesConstructor ()
    {
        var sr = new SelectionRange (new DateTime (2001, 1, 11), new DateTime (2008, 2, 17));
        object expected = new DateTime (2001, 1, 11);
        Assert.That((object?)sr.Start, Is.EqualTo(expected), "Start");
        object expected1 = new DateTime (2008, 2, 17);
        Assert.That((object?)sr.End, Is.EqualTo(expected1), "End");
    }

    [Test]
    public void TwoDatesConstructor_Backwards () // start > end
    {
        var sr = new SelectionRange (new DateTime (2008, 2, 17), new DateTime (2001, 1, 11));
        object expected = new DateTime (2001, 1, 11);
        Assert.That((object?)sr.Start, Is.EqualTo(expected), "Start");
        object expected1 = new DateTime (2008, 2, 17);
        Assert.That((object?)sr.End, Is.EqualTo(expected1), "End");
    }

    [Test]
    public void TwoDatesConstructor_WithTime ()
    {
        // Apparenly any time value is stripped, found while testing PropertyGrid.
        var sr = new SelectionRange (new DateTime (2001, 1, 11, 13, 14, 15), new DateTime (2008, 2, 17));
        object expected = new DateTime (2001, 1, 11);
        Assert.That((object?)sr.Start, Is.EqualTo(expected), "Start");
        object expected1 = new DateTime (2008, 2, 17);
        Assert.That((object?)sr.End, Is.EqualTo(expected1), "End");
    }

    [Test]
    public void TwoDatesConstructor_WithTime2 ()
    {
        // Apparenly any time value is stripped, found while testing PropertyGrid.
        var sr = new SelectionRange (new DateTime (2001, 1, 11), new DateTime (2008, 2, 17, 1, 2, 3));
        object expected = new DateTime (2001, 1, 11);
        Assert.That((object?)sr.Start, Is.EqualTo(expected), "Start");
        object expected1 = new DateTime (2008, 2, 17);
        Assert.That((object?)sr.End, Is.EqualTo(expected1), "End");
        Assert.That((object?)sr.Start.Kind, Is.EqualTo(DateTimeKind.Unspecified), "Start Kind");
        Assert.That((object?)sr.End.Kind, Is.EqualTo(DateTimeKind.Unspecified), "End Kind");
    }

    [Test]
    public void TwoDatesConstructor_WithTimeWithKindLocal ()
    {
        // Apparenly any time value is stripped, found while testing PropertyGrid.
        var sr = new SelectionRange (new DateTime (2001, 1, 11, 13, 14, 15, DateTimeKind.Local), new DateTime (2008, 2, 17));
        object expected = new DateTime (2001, 1, 11);
        Assert.That((object?)sr.Start, Is.EqualTo(expected), "Start");
        object expected1 = new DateTime (2008, 2, 17);
        Assert.That((object?)sr.End, Is.EqualTo(expected1), "End");
        //
        Assert.That((object?)sr.Start.Kind, Is.EqualTo(DateTimeKind.Local), "Start Kind");
        Assert.That((object?)sr.End.Kind, Is.EqualTo(DateTimeKind.Unspecified), "End Kind");
    }

    [Test]
    public void TwoDatesConstructor_WithTime2WithKindUtc ()
    {
        // Apparenly any time value is stripped, found while testing PropertyGrid.
        var sr = new SelectionRange (new DateTime (2001, 1, 11), new DateTime (2008, 2, 17, 1, 2, 3, DateTimeKind.Utc));
        object expected = new DateTime (2001, 1, 11);
        Assert.That((object?)sr.Start, Is.EqualTo(expected), "Start");
        object expected1 = new DateTime (2008, 2, 17);
        Assert.That((object?)sr.End, Is.EqualTo(expected1), "End");
        //
        Assert.That((object?)sr.Start.Kind, Is.EqualTo(DateTimeKind.Unspecified), "Start Kind");
        Assert.That((object?)sr.End.Kind, Is.EqualTo(DateTimeKind.Utc), "End Kind");
    }

    [Test]
    public void TwoDatesConstructor_WithTwoTimeWithTwoKinds ()
    {
        // Apparenly any time value is stripped, found while testing PropertyGrid.
        var sr = new SelectionRange (
            new DateTime (2001, 1, 11, 1, 2, 3, DateTimeKind.Utc),
            new DateTime (2008, 2, 17, 1, 2, 3, DateTimeKind.Local));
        object expected = new DateTime (2001, 1, 11);
        Assert.That((object?)sr.Start, Is.EqualTo(expected), "Start");
        object expected1 = new DateTime (2008, 2, 17);
        Assert.That((object?)sr.End, Is.EqualTo(expected1), "End");
        //
        Assert.That((object?)sr.Start.Kind, Is.EqualTo(DateTimeKind.Utc), "Start Kind");
        Assert.That((object?)sr.End.Kind, Is.EqualTo(DateTimeKind.Local), "End Kind");
    }

}