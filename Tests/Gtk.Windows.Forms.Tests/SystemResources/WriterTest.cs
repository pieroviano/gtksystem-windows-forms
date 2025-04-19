//
// WriterTest.cs: Unit Tests for ResXResourceWriter.
//
// Authors:
//     Robert Jordan <robertj@gmx.net>
//     Gary Barnett <gary.barnett.mono@gmail.com>

using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Text;
using GtkTests.Helpers;

namespace GtkTests.SystemResources;

[TestFixture]
public class WriterTest : TestHelper
{
    private string fileName;

    [SetUp]
    protected override void SetUp()
    {
        fileName = Path.GetTempFileName();
        base.SetUp();
    }

    [TearDown]
    protected override void TearDown()
    {
        File.Delete(fileName);
        base.TearDown();
    }

    [Test] // ctor (Stream)
    [NUnit.Framework.Category("NotDotNet")]
    public void Constructor1_Stream_Null()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                _ = new ResXResourceWriter((Stream)null!);
            }
            catch (ArgumentException ex)
            {
                object expected = typeof(ArgumentException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                throw;
            }
        });
    }

    [Test] // ctor (Stream)
    [NUnit.Framework.Category("NotDotNet")]
    public void Constructor1_Stream_NotWritable()
    {
        var ms = new MemoryStream([], false);

        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                _ = new ResXResourceWriter(ms);
            }
            catch (ArgumentException ex)
            {
                object expected = typeof(ArgumentException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                throw;
            }
        });
    }

    [Test] // ctor (TextWriter)
    [NUnit.Framework.Category("NotDotNet")]
    public void Constructor2_TextWriter_Null()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            try
            {
                _ = new ResXResourceWriter((TextWriter)null!);
            }
            catch (ArgumentNullException ex)
            {
                object expected = typeof(ArgumentNullException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                Assert.That((object?)ex.ParamName, Is.EqualTo("textWriter"));
                throw;
            }
        });
    }

    [Test] // ctor (String)
    [NUnit.Framework.Category("NotDotNet")]
    public void Constructor3_FileName_Null()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                _ = new ResXResourceWriter((string)null!);
            }
            catch (ArgumentException ex)
            {
                object expected = typeof(ArgumentException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                throw;
            }
        });
    }

    [Test]
    public void AddResource_WithComment()
    {
        var w = new ResXResourceWriter(fileName);
        var node = new ResXDataNode("key", "value")
        {
            Comment = "comment is preserved"
        };
        w.AddResource(node);
        w.Generate();
        w.Close();

        var r = new ResXResourceReader(fileName);
        ITypeResolutionService? typeres = null;
        r.UseResXDataNodes = true;

        var count = 0;
        foreach (DictionaryEntry o in r!)
        {
            var key = o.Key.ToString();
            node = (ResXDataNode?)o.Value;
            var value = node!.GetValue(typeres)!.ToString();
            var comment = node.Comment;

            Assert.That((object?)key, Is.EqualTo("key"), "key");
            Assert.That((object?)value, Is.EqualTo("value"), "value");
            Assert.That((object?)comment, Is.EqualTo("comment is preserved"), "comment");
            Assert.That((object?)count, Is.EqualTo(0), "too many nodes");
            count++;
        }
        r.Close();

        File.Delete(fileName);
    }

    [Test]
    public void TestWriter()
    {
        var w = new ResXResourceWriter(fileName);
        w.AddResource("String", "hola");
        w.AddResource("String2", (object)"hello");
        w.AddResource("Int", 42);
        w.AddResource("Enum", PlatformID.Win32NT);
        w.AddResource("Convertible", new Point(43, 45));
        w.AddResource("ByteArray", [12, 13, 14]);
        w.AddResource("ByteArray2", (object)new byte[] { 15, 16, 17 });
        w.AddResource("StrType", new MyStrType("hello"));
        w.AddResource("BinType", new MyBinType("world"));

        Assert.Throws<InvalidOperationException>(() =>
        {
            w.AddResource("NonSerType", new MyNonSerializableType());
        });

        w.Generate();
        w.Close();

        var r = new ResXResourceReader(fileName);
        var h = new Hashtable();
        foreach (DictionaryEntry e in r!)
        {
            h.Add(e.Key, e.Value);
        }
        r.Close();

        Assert.That((object?)(string?)h["String"], Is.EqualTo("hola"));
        Assert.That((object?)(string?)h["String2"], Is.EqualTo("hello"));
        Assert.That((object?)(int)h["Int"]!, Is.EqualTo(42));
        Assert.That((object?)(PlatformID?)h["Enum"], Is.EqualTo(PlatformID.Win32NT));
        Assert.That((object?)((Point)h["Convertible"]!).X, Is.EqualTo(43));
        Assert.That((object?)((byte[])h["ByteArray"]!)[1], Is.EqualTo(13));
        Assert.That((object?)((byte[])h["ByteArray2"]!)[1], Is.EqualTo(16));
        Assert.That((object?)((MyStrType)h["StrType"]!).Value, Is.EqualTo("hello"));
        Assert.That((object?)((MyBinType)h["BinType"]!).Value, Is.EqualTo("world"));

        File.Delete(fileName);
    }

    private ResXDataNode? GetNodeFromResXWithBasePath(ResXDataNode node, string basePath)
    {
        var sw = new StringWriter();
        using (var writer = new ResXResourceWriter(sw))
        {
            writer.BasePath = basePath;
            writer.AddResource(node);
        }

        var sr = new StringReader(sw.ToString());

        using (var reader = new ResXResourceReader(sr))
        {
            reader.UseResXDataNodes = true;
            var enumerator = reader.GetEnumerator();
            using var disposable = enumerator as IDisposable;
            enumerator?.MoveNext();
            return ((DictionaryEntry)enumerator?.Current!).Value as ResXDataNode;
        }
    }

    [Test]
    public void BasePath_ChangesAbsoluteFileRef_Node()
    {
        var node = new ResXDataNode("name", new ResXFileRef(@"/dir/dir/filename.ext", "System.String"));
        var returnedNode = GetNodeFromResXWithBasePath(node, @"/dir");
        Assert.IsNotNull(returnedNode);
        object expected = @$"dir{Path.DirectorySeparatorChar}filename.ext";
        Assert.That((object?)returnedNode?.FileRef!.FileName, Is.EqualTo(expected));
    }

    [Test]
    public void BasePath_ChangesAbsoluteFileRef_Object()
    {
        var fileref = new ResXFileRef(@"/dir/dir/filename.ext", "System.String");

        var basePath = @"/dir";

        var sw = new StringWriter();
        using (var writer = new ResXResourceWriter(sw))
        {
            writer.BasePath = basePath;
            writer.AddResource("name", fileref);
        }

        var sr = new StringReader(sw.ToString());

        using (var reader = new ResXResourceReader(sr))
        {
            reader.UseResXDataNodes = true;
            var enumerator = reader.GetEnumerator();
            using var disposable = enumerator as IDisposable;
            enumerator?.MoveNext();
            var returnedNode = ((DictionaryEntry)enumerator!.Current).Value as ResXDataNode;
            object expected = $@"{Path.DirectorySeparatorChar}dir{Path.DirectorySeparatorChar}dir{Path.DirectorySeparatorChar}filename.ext";
            Assert.That((object?)returnedNode?.FileRef!.FileName, Is.EqualTo(expected));
        }
    }

    [Test]
    public void BasePath_ChangesAbsoluteFileRef_Deeper()
    {
        var node = new ResXDataNode("name", new ResXFileRef(@"/adir/filename.ext", "System.String"));
        var returnedNode = GetNodeFromResXWithBasePath(node, @"/dir1/dir2");
        Assert.IsNotNull(returnedNode);
        object expected = @$"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}adir{Path.DirectorySeparatorChar}filename.ext";
        Assert.That((object?)returnedNode?.FileRef!.FileName, Is.EqualTo(expected));
    }

    [Test]
    public void BasePath_ComplexPath()
    {
        var node = new ResXDataNode("name", new ResXFileRef(@"/dir/dir/../filename.ext", "System.String"));
        var returnedNode = GetNodeFromResXWithBasePath(node, @"/dir");
        Assert.IsNotNull(returnedNode);
        object expected = @$"dir{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}filename.ext";
        Assert.That((object?)returnedNode?.FileRef!.FileName, Is.EqualTo(expected));
    }

    [Test]
    public void BasePath_IgnoresTrailingSeparator()
    {
        var node = new ResXDataNode("name", new ResXFileRef(@"/dir/filename.ext", "System.String"));
        var nonTrailNode = GetNodeFromResXWithBasePath(node, @"/dir");
        Assert.IsNotNull(nonTrailNode);
        Assert.That((object?)nonTrailNode?.FileRef!.FileName, Is.EqualTo(@"filename.ext"));

        var trailingNode = GetNodeFromResXWithBasePath(node, @"/dir/");
        Assert.IsNotNull(trailingNode);
        Assert.That((object?)trailingNode?.FileRef!.FileName, Is.EqualTo(@"filename.ext"));
    }

    [Test]
    public void BasePath_IgnoredIfNotPartOfPath() // not really a valid test on linux systems
    {
        var node = new ResXDataNode("name", new ResXFileRef(@"/dir/filename.ext", "System.String"));
        var returnedNode = GetNodeFromResXWithBasePath(node, @"D/");
        Assert.IsNotNull(returnedNode);
        object expected = $@"{Path.DirectorySeparatorChar}dir{Path.DirectorySeparatorChar}filename.ext";
        Assert.That((object?)returnedNode?.FileRef!.FileName, Is.EqualTo(expected));
    }

    [Test] // FIXME: this fails on mono ("/dir/filename.ext" is returned) but passes on .net
    [NUnit.Framework.Category("NotWorking")]
    public void BasePath_Root()
    {
        var node = new ResXDataNode("name", new ResXFileRef(@"/dir/filename.ext", "System.String"));
        var returnedNode = GetNodeFromResXWithBasePath(node, @"/");
        Assert.IsNotNull(returnedNode);
        object expected = $@"dir{Path.DirectorySeparatorChar}filename.ext";
        Assert.That((object?)returnedNode?.FileRef!.FileName, Is.EqualTo(expected));
    }

    [Test]
    public void BasePath_RelativeFileRef()
    {
        var node = new ResXDataNode("name", new ResXFileRef(@"../../filename.ext", "System.String"));
        var returnedNode = GetNodeFromResXWithBasePath(node, @"../");
        Assert.IsNotNull(returnedNode);
        object expected = $@"..{Path.DirectorySeparatorChar}filename.ext";
        Assert.That((object?)returnedNode?.FileRef!.FileName, Is.EqualTo(expected));
    }

    [Test]
    public void BasePath_DoesntAffectOriginalNode()
    {
        var node = new ResXDataNode("name", new ResXFileRef(@"/dir/dir/filename.ext", "System.String"));
        var returnedNode = GetNodeFromResXWithBasePath(node, @"/dir");
        Assert.IsNotNull(returnedNode);
        object expected = $@"dir{Path.DirectorySeparatorChar}filename.ext";
        Assert.That((object?)returnedNode?.FileRef!.FileName, Is.EqualTo(expected));
        object expected1 = $@"{Path.DirectorySeparatorChar}dir{Path.DirectorySeparatorChar}dir{Path.DirectorySeparatorChar}filename.ext";
        Assert.That((object?)node.FileRef!.FileName, Is.EqualTo(expected1));
    }
}

[Serializable]
[TypeConverter(typeof(MyStrTypeConverter))]
public class MyStrType
{
    public string Value;

    public MyStrType(string s)
    {
        Value = s;
    }
}

public class MyStrTypeConverter : TypeConverter
{
    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        if (destinationType == typeof(string))
            return true;
        return base.CanConvertTo(context, destinationType);
    }

    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        if (sourceType == typeof(string))
            return true;
        return base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType == typeof(string))
            return ((MyStrType)value!).Value;
        return base.ConvertTo(context, culture, value, destinationType);
    }

    public override object ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string)
            return new MyStrType((string)value);
        return base.ConvertFrom(context, culture, value)!;
    }
}

[Serializable]
[TypeConverter(typeof(MyBinTypeConverter))]
public class MyBinType
{
    public string Value;

    public MyBinType(string s)
    {
        Value = s;
    }
}

public class MyBinTypeConverter : TypeConverter
{
    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        if (destinationType == typeof(byte[]))
            return true;
        return base.CanConvertTo(context, destinationType);
    }

    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        if (sourceType == typeof(byte[]))
            return true;
        return base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType == typeof(byte[]))
            return Encoding.Default.GetBytes(((MyBinType)value!).Value);
        return base.ConvertTo(context, culture, value, destinationType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value.GetType() == typeof(byte[]))
            return new MyBinType(Encoding.Default.GetString((byte[])value));
        return base.ConvertFrom(context, culture, value);
    }
}

[TypeConverter(typeof(MyNonSerializableTypeConverter))]
public class MyNonSerializableType
{
}

public class MyNonSerializableTypeConverter : TypeConverter
{
    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        if (destinationType == typeof(byte[]))
            return true;
        return base.CanConvertTo(context, destinationType);
    }

    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        if (sourceType == typeof(byte[]))
            return true;
        return base.CanConvertFrom(context, sourceType);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType == typeof(byte[]))
            return new byte[] { 0, 1, 2, 3 };
        return base.ConvertTo(context, culture, value, destinationType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value.GetType() == typeof(byte[]))
            return new MyNonSerializableType();
        return base.ConvertFrom(context, culture, value);
    }
}