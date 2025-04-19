//
//  PaddingTest.cs
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
// Copyright (c) 2006 Daniel Nauck
//
// Author:
//      Daniel Nauck    (dna(at)mono-project(dot)de)

using System.Drawing;
using System.Windows.Forms;
using GtkTests.Helpers;

namespace GtkTests.Models;

[TestFixture]
public class PaddingTest : TestHelper
{
    [Test]
    public void PaddingPropertiesTest()
    {
        var pad = new Padding();
        Assert.That((object?)pad.All, Is.EqualTo(-1));
        Assert.That((object?)pad.Top, Is.EqualTo(0));
        Assert.That((object?)pad.Left, Is.EqualTo(0));
        Assert.That((object?)pad.Right, Is.EqualTo(0));
        Assert.That((object?)pad.Bottom, Is.EqualTo(0));
        Assert.That((object?)pad.Horizontal, Is.EqualTo(0));
        Assert.That((object?)pad.Vertical, Is.EqualTo(0));
        Assert.That((object?)pad.ToString(), Is.EqualTo("{Left=0,Top=0,Right=0,Bottom=0}"));
        object expected = new Size(0,0);
        Assert.That((object?)pad.Size, Is.EqualTo(expected));

        var pad2 = new Padding(5);
        Assert.That((object?)pad2.All, Is.EqualTo(5));
        Assert.That((object?)pad2.Top, Is.EqualTo(5));
        Assert.That((object?)pad2.Left, Is.EqualTo(5));
        Assert.That((object?)pad2.Right, Is.EqualTo(5));
        Assert.That((object?)pad2.Bottom, Is.EqualTo(5));
        Assert.That((object?)pad2.Horizontal, Is.EqualTo(10));
        Assert.That((object?)pad2.Vertical, Is.EqualTo(10));
        Assert.That((object?)pad2.ToString(), Is.EqualTo("{Left=5,Top=5,Right=5,Bottom=5}"));
        object expected1 = new Size(10, 10);
        Assert.That((object?)pad2.Size, Is.EqualTo(expected1));

        var pad3 = new Padding(5, 5, 10, 10);
        Assert.That((object?)pad3.All, Is.EqualTo(-1));
        Assert.That((object?)pad3.Top, Is.EqualTo(5));
        Assert.That((object?)pad3.Left, Is.EqualTo(5));
        Assert.That((object?)pad3.Right, Is.EqualTo(10));
        Assert.That((object?)pad3.Bottom, Is.EqualTo(10));
        Assert.That((object?)pad3.Horizontal, Is.EqualTo(15));
        Assert.That((object?)pad3.Vertical, Is.EqualTo(15));
        Assert.That((object?)pad3.ToString(), Is.EqualTo("{Left=5,Top=5,Right=10,Bottom=10}"));
        object expected2 = new Size(15, 15);
        Assert.That((object?)pad3.Size, Is.EqualTo(expected2));

        var pad4 = new Padding(10, 10, 10, 10);
        Assert.That((object?)pad4.All, Is.EqualTo(10));

        var pad5 = Padding.Empty;
        Assert.That((object?)pad5.All, Is.EqualTo(0));
        Assert.That((object?)pad5.Top, Is.EqualTo(0));
        Assert.That((object?)pad5.Left, Is.EqualTo(0));
        Assert.That((object?)pad5.Right, Is.EqualTo(0));
        Assert.That((object?)pad5.Bottom, Is.EqualTo(0));
        Assert.That((object?)pad5.Horizontal, Is.EqualTo(0));
        Assert.That((object?)pad5.Vertical, Is.EqualTo(0));
        Assert.That((object?)pad5.ToString(), Is.EqualTo("{Left=0,Top=0,Right=0,Bottom=0}"));
        object expected3 = new Size(0, 0);
        Assert.That((object?)pad5.Size, Is.EqualTo(expected3));
    }

    [Test]
    public void PaddingOperatorTest()
    {
        var pad = new Padding(0);
        Assert.That((object?)pad, Is.EqualTo(Padding.Empty));

        var pad1 = new Padding(2, 4, 6, 8);
        var pad2 = new Padding(5, 5, 10, 11);
        var pad3 = pad1 + pad2;
        Assert.That((object?)pad3.All, Is.EqualTo(-1));
        Assert.That((object?)pad3.ToString(), Is.EqualTo("{Left=7,Top=9,Right=16,Bottom=19}"));

        pad3 = Padding.Add(pad1, pad2);
        Assert.That((object?)pad3.All, Is.EqualTo(-1));
        Assert.That((object?)pad3.ToString(), Is.EqualTo("{Left=7,Top=9,Right=16,Bottom=19}"));

        var pad4 = pad3 - pad1;
        Assert.That((object?)pad4.All, Is.EqualTo(-1));
        Assert.That((object?)pad4.ToString(), Is.EqualTo("{Left=5,Top=5,Right=10,Bottom=11}"));

        pad4 = Padding.Subtract(pad3, pad1);
        Assert.That((object?)pad4.All, Is.EqualTo(-1));
        Assert.That((object?)pad4.ToString(), Is.EqualTo("{Left=5,Top=5,Right=10,Bottom=11}"));
    }
}