//
// ResXDataNodeFileRefGetValueTypeNameTests.cs
// 
// Author:
//	Gary Barnett (gary.barnett.mono@gmail.com)
// 
// Copyright (C) Gary Barnett (2012)
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

using System.Reflection;
using System.Drawing;
using System.Resources;
using System.ComponentModel.Design;
using GtkTests.TypeResolutionService_;
using GtkTests.Resources;

namespace GtkTests.System.Resources;

[TestFixture]
public class ResXDataNodeFileRefGetValueTypeNameTests : ResourcesTestHelper
{
    [Test]
    public void CanGetStrongNameFromGetValueTypeNameWithOnlyFullNameAsTypeByProvidingAssemblyName()
    {
        var aName = GetType().Assembly.FullName;
        AssemblyName[] assemblyNames = [new(aName!)];

        var originalNode = GetNodeFileRefToSerializable("ser.bbb", false);
        var returnedNode = GetNodeFromResXReader(originalNode);

        Assert.That((object?)returnedNode, Is.Not.Null);
        var typeName = returnedNode.GetValueTypeName(assemblyNames);
        object expected = "GtkTests.Resources.Serializable, " + aName;
        Assert.That((object?)typeName, Is.EqualTo(expected));
    }

    private void CanGetValueTypeNameWithOnlyFullNameAsType()
    {
        var originalNode = GetNodeFileRefToSerializable("ser.bbb", false);
        var returnedNode = GetNodeFromResXReader(originalNode);

        Assert.That((object?)returnedNode, Is.Not.Null);
        var typeName = returnedNode.GetValueTypeName((AssemblyName[]?)null);
        object? expected = (typeof(Serializable)).FullName;
        Assert.That((object?)typeName, Is.EqualTo(expected));
    }

    [Test]
    public void ITRSUsedWhenNodeFromReader()
    {
        var originalNode = GetNodeFileRefToSerializable("ser.bbb", true);
        var returnedNode = GetNodeFromResXReader(originalNode);

        Assert.That((object?)returnedNode, Is.Not.Null);
        var returnedType = returnedNode.GetValueTypeName(new ReturnSerializableSubClassITRS());
        object? expected = (typeof(SerializableSubClass)).AssemblyQualifiedName;
        Assert.That((object?)returnedType, Is.EqualTo(expected));
    }

    [Test]
    public void ITRSUsedWhenNodeCreatedNew()
    {
        var node = GetNodeFileRefToSerializable("ser.bbb", true);

        var returnedType = node.GetValueTypeName(new ReturnSerializableSubClassITRS());
        object? expected = (typeof(SerializableSubClass)).AssemblyQualifiedName;
        Assert.That((object?)returnedType, Is.EqualTo(expected));
    }

    [Test]
    public void IfTypeResolutionFailsReturnsOrigString()
    {
        var fileRef = new ResXFileRef("afile.name", "a.type.name");
        var node = new ResXDataNode("aname", fileRef);

        var returnedType = node.GetValueTypeName((AssemblyName[]?)null);
        Assert.That((object?)returnedType, Is.EqualTo("a.type.name"));
    }

    [Test]
    public void AttemptsTypeResolution()
    {
        var fileRef = new ResXFileRef("afile.name", "System.String");
        var node = new ResXDataNode("aname", fileRef);

        var returnedType = node.GetValueTypeName((AssemblyName[]?)null);
        object? expected = typeof(string).AssemblyQualifiedName;
        Assert.That((object?)returnedType, Is.EqualTo(expected));
    }

    #region Initial Exploratory Tests

    [Test]
    public void NullAssemblyNamesOK()
    {
        var node = GetNodeFileRefToIcon();

        var name = node.GetValueTypeName((AssemblyName[]?)null);
        object? expected = typeof(Icon).AssemblyQualifiedName;
        Assert.That((object?)name, Is.EqualTo(expected));
    }

    [Test]
    public void NullITRSOK()
    {
        var node = GetNodeFileRefToIcon();

        var name = node.GetValueTypeName((ITypeResolutionService?)null);
        object? expected = typeof(Icon).AssemblyQualifiedName;
        Assert.That((object?)name, Is.EqualTo(expected));
    }

    [Test]
    public void WrongITRSOK()
    {
        var node = GetNodeFileRefToIcon();

        var name = node.GetValueTypeName(new DummyITRS());
        object? expected = typeof(Icon).AssemblyQualifiedName;
        Assert.That((object?)name, Is.EqualTo(expected));
    }

    [Test]
    public void WrongAssemblyNamesOK()
    {
        var node = GetNodeFileRefToIcon();
        AssemblyName[] ass = [new("DummyAssembly")];

        var name = node.GetValueTypeName(ass);
        object? expected = typeof(Icon).AssemblyQualifiedName;
        Assert.That((object?)name, Is.EqualTo(expected));
    }

    #endregion
}