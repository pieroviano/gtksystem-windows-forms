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
// Copyright (c) 2006 Novell, Inc.
//
// Authors:
//	Jackson Harper	jackson@ximian.com

using GtkTests.Helpers;
using System.Windows.Forms;

namespace GtkTests.DataBinding;

[TestFixture]
public class BindingMemberInfoTest : TestHelper
{

    [Test]
    public void CtorNullTest()
    {
        var bmi = new BindingMemberInfo(null);

        object expected = string.Empty;
        Assert.That((object?)bmi.BindingMember, Is.EqualTo(expected), "CTORNULL1");
        object expected1 = string.Empty;
        Assert.That((object?)bmi.BindingField, Is.EqualTo(expected1), "CTORNULL2");
        object expected2 = string.Empty;
        Assert.That((object?)bmi.BindingPath, Is.EqualTo(expected2), "CTORNULL3");
    }

    [Test]
    public void CtorMemberOnly()
    {
        var bmi = new BindingMemberInfo("Member");

        Assert.That((object?)bmi.BindingMember, Is.EqualTo("Member"), "CTORMEMBER1");
        Assert.That((object?)bmi.BindingField, Is.EqualTo("Member"), "CTORMEMBER2");
        object expected = string.Empty;
        Assert.That((object?)bmi.BindingPath, Is.EqualTo(expected), "CTORMEMBER3");
    }

    [Test]
    public void CtorMemberAndPathOnly()
    {
        var bmi = new BindingMemberInfo("Member.Path");

        Assert.That((object?)bmi.BindingMember, Is.EqualTo("Member.Path"), "CTMAF1");
        Assert.That((object?)bmi.BindingPath, Is.EqualTo("Member"), "CTMAF2");
        Assert.That((object?)bmi.BindingField, Is.EqualTo("Path"), "CTMAF3");
    }

    [Test]
    public void CtorAll()
    {
        var bmi = new BindingMemberInfo("Member.Path.Field");

        Assert.That((object?)bmi.BindingMember, Is.EqualTo("Member.Path.Field"), "CTALL1");
        Assert.That((object?)bmi.BindingPath, Is.EqualTo("Member.Path"), "CTALL2");
        Assert.That((object?)bmi.BindingField, Is.EqualTo("Field"), "CTALL3");
    }

    [Test]
    public void CtorEmpty()
    {
        var bmi = new BindingMemberInfo("...");

        Assert.That((object?)bmi.BindingMember, Is.EqualTo("..."), "CTEMPTY1");
        Assert.That((object?)bmi.BindingPath, Is.EqualTo(".."), "CTEMPTY2");
        object expected = string.Empty;
        Assert.That((object?)bmi.BindingField, Is.EqualTo(expected), "CTEMPTY3");
    }

    [Test]
    public void CtorSpecialChars()
    {
        var bmi = new BindingMemberInfo(",/';.[]-=!.$%&*~");

        Assert.That((object?)bmi.BindingMember, Is.EqualTo(",/';.[]-=!.$%&*~"), "CTORSPECIAL1");
        Assert.That((object?)bmi.BindingPath, Is.EqualTo(",/';.[]-=!"), "CTORSPECIAL2");
        Assert.That((object?)bmi.BindingField, Is.EqualTo("$%&*~"), "CTORSPECIAL3");
    }

    [Test]
    public void EqualsTest()
    {
        var a = new BindingMemberInfo("A.B.C");
        var b = new BindingMemberInfo("A.B.C");

        Assert.That((object?)a, Is.EqualTo(b), "EQUALS1");

        b = new BindingMemberInfo("A.B");
        Assert.IsFalse(a.Equals(b), "EQUALS2");
    }
}