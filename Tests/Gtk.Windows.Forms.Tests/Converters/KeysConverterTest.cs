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
// Copyright (c) 2007 Novell, Inc.
//

using GtkTests.Helpers;
using System.Windows.Forms;

namespace GtkTests.Converters;

[TestFixture]
public class KeysConverterTest : TestHelper
{
    [Test]
    public void CanConvertTo ()
    {
        var c = new KeysConverter ();

        Assert.That((object?)c.CanConvertTo (null, typeof (string)), Is.EqualTo(true));
        Assert.That((object?)c.CanConvertTo (null, typeof (int)), Is.EqualTo(false));
        Assert.That((object?)c.CanConvertTo (null, typeof (float)), Is.EqualTo(false));
        Assert.That((object?)c.CanConvertTo (null, typeof (object)), Is.EqualTo(false));
        Assert.That((object?)c.CanConvertTo (null, typeof (Enum)), Is.EqualTo(false));
        Assert.That((object?)c.CanConvertTo (null, typeof (Enum [])), Is.EqualTo(true));
    }
}