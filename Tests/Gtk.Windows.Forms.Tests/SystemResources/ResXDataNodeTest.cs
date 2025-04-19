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
// Copyright (c) 2007 Novell, Inc. (http://www.novell.com)
//
// ResXFileRefTest.cs: Unit Tests for ResXFileRef.
//
// Authors:
//		Andreia Gaita	(avidigal@novell.com)
//  		Gary Barnett	(gary.barnett.mono@gmail.com)

using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Resources;
using GtkTests.Resources;
using GtkTests.TypeResolutionService;

namespace GtkTests.SystemResources;

[TestFixture]
public class ResXDataNodeTest : ResourcesTestHelper
{
    private string _tempDirectory;
    private string _otherTempDirectory;

    [Test]
    public void ConstructorEx1()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var d = new ResXDataNode(null, (object?)null!);
        });
    }

    [Test]
    public void ConstructorEx2A()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var d = new ResXDataNode(null, new ResXFileRef("filename", "typename"));
        });
    }

    [Test]
    public void ConstructorEx2B()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var d = new ResXDataNode("aname", null!);
        });
    }

    [Test]
    public void ConstructorEx3()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var d = new ResXDataNode(string.Empty, (object?)null!);
        });
    }

    [Test]
    public void ConstructorEx4()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var d = new ResXDataNode(string.Empty, null!);
        });
    }

    [Test]
    public void ConstructorEx5()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var d = new ResXDataNode(string.Empty, new ResXFileRef("filename", "typename"));
        });
    }

    [Test]
    public void ConstructorEx6()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            var d = new ResXDataNode("name", new NotSerializable());
        });
    }

    [Test]
    public void Name()
    {
        var node = new ResXDataNode("startname", (object?)null!);
        Assert.That((object?)node.Name, Is.EqualTo("startname"));
        node.Name = "newname";
        Assert.That((object?)node.Name, Is.EqualTo("newname"));
    }

    [Test]
    public void NameCantBeNull()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var node = new ResXDataNode("startname", (object?)null!)
            {
                Name = null!
            };
        });
    }

    [Test]
    public void NameCantBeEmpty()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var node = new ResXDataNode("name", (object?)null!)
            {
                Name = string.Empty
            };
        });
    }

    [Test]
    public void FileRef()
    {
        var fileRef = new ResXFileRef("fileName", "Type.Name");
        var node = new ResXDataNode("name", fileRef);
        Assert.That((object?)node.FileRef, Is.EqualTo(fileRef));
    }

    [Test]
    public void Comment()
    {
        var node = new ResXDataNode("name", (object?)null!)
        {
            Comment = "acomment"
        };
        Assert.That((object?)node.Comment, Is.EqualTo("acomment"));
    }

    [Test]
    public void CommentNullToStringEmpty()
    {
        var node = new ResXDataNode("name", (object?)null!)
        {
            Comment = null
        };
        object expected = string.Empty;
        Assert.That((object?)node.Comment, Is.EqualTo(expected));
    }

    [Test]
    public void ConstructorResXFileRef()
    {
        var node = GetNodeFileRefToIcon();
        Assert.IsNotNull(node.FileRef);
        object? expected = typeof(Icon).AssemblyQualifiedName;
        Assert.That((object?)node.FileRef.TypeName, Is.EqualTo(expected));
        Assert.That((object?)node.Name, Is.EqualTo("test"));
    }

    [Test]
    public void NullObjectGetValueTypeNameIsNull()
    {
        var node = new ResXDataNode("aname", (object?)null!);
        Assert.IsNull(node.GetValueTypeName((AssemblyName[]?)null));
    }

    [Test]
    public void NullObjectWrittenToResXOK()
    {
        var node = new ResXDataNode("aname", (object?)null!);
        var returnedNode = GetNodeFromResXReader(node);
        Assert.IsNotNull(returnedNode);
        Assert.IsNull(returnedNode.GetValue((AssemblyName[]?)null));
    }

    [Test]
    public void NullObjectReturnedFromResXGetValueTypeNameReturnsObject()
    {
        var node = new ResXDataNode("aname", (object?)null!);
        var returnedNode = GetNodeFromResXReader(node);
        Assert.IsNotNull(returnedNode);
        Assert.IsNull(returnedNode.GetValue((AssemblyName[]?)null));
        var type = returnedNode.GetValueTypeName((AssemblyName[]?)null);
        object? expected = typeof(object).AssemblyQualifiedName;
        Assert.That((object?)type, Is.EqualTo(expected));
    }

    [Test]
    public void DoesNotRequireResXFileToBeOpen_TypeConverter()
    {
        var dn = new ResXDataNode("test", 34L);
        var resXFile = GetResXFileWithNode(dn, "resx.resx");

        var rr = new ResXResourceReader(resXFile);
        rr.UseResXDataNodes = true;
        var en = rr.GetEnumerator();
        en!.MoveNext();

        var node = ((DictionaryEntry)en.Current).Value as ResXDataNode;
        rr.Close();

        Assert.IsNotNull(node);

        var o = node.GetValue((AssemblyName[]?)null);
        Assert.True(typeof(long) == o?.GetType());
        Assert.That(o, Is.EqualTo(34L));
    }

    [Test]
    public void ITRSPassedToResourceReaderDoesNotAffectResXDataNode_TypeConverter()
    {
        var dn = new ResXDataNode("test", 34L);

        var resXFile = GetResXFileWithNode(dn, "resx.resx");

        var rr = new ResXResourceReader(resXFile, new ReturnIntITRS());
        rr.UseResXDataNodes = true;
        var en = rr.GetEnumerator();
        en!.MoveNext();

        var node = ((DictionaryEntry)en.Current).Value as ResXDataNode;

        Assert.IsNotNull(node);

        var o = node.GetValue((AssemblyName[]?)null);

        Assert.True(typeof(long) == o?.GetType());
        Assert.That(o, Is.EqualTo(34L));

        rr.Close();
    }

    [Test]
    public void BasePathSetOnResXResourceReaderDoesAffectResXDataNode()
    {
        var fileRef = new ResXFileRef("file.name", "type.name");
        var node = new ResXDataNode("anode", fileRef);
        var resXFile = GetResXFileWithNode(node, "afilename.xxx");

        using var rr = new ResXResourceReader(resXFile);
        rr.BasePath = "basePath";
        rr.UseResXDataNodes = true;
        var en = rr.GetEnumerator();
        en!.MoveNext();

        var returnedNode = ((DictionaryEntry)en.Current).Value as ResXDataNode;

        Assert.IsNotNull(node);
        object expected = Path.Combine("basePath", "file.name");
        Assert.That((object?)returnedNode!.FileRef?.FileName, Is.EqualTo(expected));
    }

    [TearDown]
    protected override void TearDown()
    {
        //teardown
        if (Directory.Exists(_tempDirectory))
            Directory.Delete(_tempDirectory, true);

        base.TearDown();
    }

    private string GetResXFileWithNode(ResXDataNode node, string filename)
    {
        _tempDirectory = Path.Combine(Path.GetTempPath(), "ResXDataNodeTest");
        _otherTempDirectory = Path.Combine(_tempDirectory, "in");
        if (!Directory.Exists(_otherTempDirectory))
        {
            Directory.CreateDirectory(_otherTempDirectory);
        }

        var fullfileName = Path.Combine(_tempDirectory, filename);

        using var writer = new ResXResourceWriter(fullfileName);
        writer.AddResource(node);

        return fullfileName;
    }

}