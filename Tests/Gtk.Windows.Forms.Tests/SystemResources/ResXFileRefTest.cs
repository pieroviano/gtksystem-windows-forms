//
// ResXFileRefTest.cs: Unit Tests for ResXFileRef.
//
// Authors:
//     Gert Driesen <drieseng@users.sourceforge.net>
//

using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Text;
using GtkTests.Helpers;

namespace GtkTests.SystemResources;

[TestFixture]
public class ResXFileRefTest : TestHelper
{
    [Test]
    public void Constructor1()
    {
        var r = new ResXFileRef("mono.bmp", "Bitmap");
        RemoveWarning(r);
        Assert.That((object?)r.FileName, Is.EqualTo("mono.bmp"));
        Assert.That((object?)r.TypeName, Is.EqualTo("Bitmap"));
    }

    [Test]
    public void Constructor1_FileName_Null()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                new ResXFileRef(null, "Bitmap");
            }
            catch (ArgumentException ex)
            {
                object expected = typeof(ArgumentException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNotNull(ex.Message);
                Assert.IsNull(ex.ParamName);
                Assert.IsNull(ex.InnerException);
                throw;
            }
        });
    }

    [Test]
    public void Constructor1_TypeName_Null()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                new ResXFileRef("mono.bmp", null);
            }
            catch (ArgumentException ex)
            {
                object expected = typeof(ArgumentException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNotNull(ex.Message);
                Assert.IsNull(ex.ParamName);
                Assert.IsNull(ex.InnerException);
                throw;
            }
        });
    }

    [Test]
    public void Constructor2()
    {
        var utf8 = Encoding.UTF8;

        var r = new ResXFileRef("mono.bmp", "Bitmap", utf8);
        Assert.That((object?)r.FileName, Is.EqualTo("mono.bmp"));
        Assert.That((object?)r.TextFileEncoding, Is.SameAs(utf8));
        Assert.That((object?)r.TypeName, Is.EqualTo("Bitmap"));

        r = new ResXFileRef("mono.bmp", "Bitmap", null);
        Assert.That((object?)r.FileName, Is.EqualTo("mono.bmp"));
        Assert.IsNull(r.TextFileEncoding);
        Assert.That((object?)r.TypeName, Is.EqualTo("Bitmap"));
    }

    [Test]
    public void Constructor2_FileName_Null()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                _ = new ResXFileRef(null, "Bitmap", Encoding.UTF8);
            }
            catch (ArgumentException ex)
            {
                object expected = typeof(ArgumentException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNotNull(ex.Message);
                Assert.IsNull(ex.ParamName);
                Assert.IsNull(ex.InnerException);
                throw;
            }
        });
    }

    [Test]
    public void Constructor2_TypeName_Null()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                new ResXFileRef("mono.bmp", null, Encoding.UTF8);
            }
            catch (ArgumentException ex)
            {
                object expected = typeof(ArgumentException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNotNull(ex.Message);
                Assert.IsNull(ex.ParamName);
                Assert.IsNull(ex.InnerException);
                throw;
            }
        });
    }

    [Test]
    public void ToStringTest()
    {
        var r = new ResXFileRef("mono.bmp", "Bitmap");
        Assert.That((object?)r.ToString(), Is.EqualTo("mono.bmp;Bitmap"));

        r = new ResXFileRef("mono.bmp", "Bitmap", Encoding.UTF8);
        Assert.That((object?)r.ToString(), Is.EqualTo("mono.bmp;Bitmap;utf-8"));

        r = new ResXFileRef("mono.bmp", "Bitmap", null);
        Assert.That((object?)r.ToString(), Is.EqualTo("mono.bmp;Bitmap"));
    }
}

[TestFixture]
public class ResXFileRefConverterTest : TestHelper
{
    [SetUp]
    protected override void SetUp()
    {
        _converter = new ResXFileRef.Converter();
        _tempDirectory = Path.Combine(Path.GetTempPath(), "ResXResourceReaderTest");
        if (!Directory.Exists(_tempDirectory))
        {
            Directory.CreateDirectory(_tempDirectory);
        }
        _tempFileUTF7 = Path.Combine(_tempDirectory, "string_utf7.txt");
#pragma warning disable SYSLIB0001
        using (var sw = new StreamWriter(_tempFileUTF7, false, Encoding.UTF7))
#pragma warning restore SYSLIB0001
        {
            sw.Write("\u0021\u0026\u002A\u003B");
        }
        base.SetUp();
    }

    [TearDown]
    protected override void TearDown()
    {
        if (Directory.Exists(_tempDirectory))
            Directory.Delete(_tempDirectory, true);
        base.TearDown();
    }

    [Test]
    public void CanConvertFrom()
    {
        Assert.IsTrue(_converter.CanConvertFrom(typeof(string)));
        Assert.IsFalse(_converter.CanConvertFrom(typeof(byte[])));
    }

    [Test]
    public void CanConvertTo()
    {
        Assert.IsTrue(_converter.CanConvertTo(typeof(string)));
        Assert.IsFalse(_converter.CanConvertTo(typeof(MemoryStream)));
        Assert.IsFalse(_converter.CanConvertTo(typeof(Bitmap)));
    }

    [Test]
    public void ConvertFrom_File_DoesNotExist()
    {
        // file does not exist
        var fileRef = "doesnotexist.txt;" + typeof(string).AssemblyQualifiedName;
        Assert.Throws<FileNotFoundException>(() =>
        {
            try
            {
                _converter.ConvertFrom(fileRef);
            }
            catch (FileNotFoundException ex)
            {
                object expected = typeof(FileNotFoundException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.FileName);
                object expected1 = Path.Combine(Directory.GetCurrentDirectory(), "doesnotexist.txt");
                Assert.That((object?)ex.FileName, Is.EqualTo(expected1));
                Assert.IsNotNull(ex.Message);
                throw;
            }
        });
    }

    [Test]
    public void ConvertFrom_Type_NotSet()
    {
        var fileRef = "doesnotexist.txt";
        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                _converter.ConvertFrom(fileRef);
            }
            catch (ArgumentException ex)
            {
                object expected = typeof(ArgumentException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                Assert.That((object?)ex.Message, Is.EqualTo("value"));
                Assert.IsNull(ex.ParamName);
                throw;
            }
        });
    }

    [Test]
    public void ConvertFrom_NotString()
    {
        Assert.IsNull(_converter.ConvertFrom(null!));
        Assert.IsNull(_converter.ConvertFrom(1));
        Assert.IsNull(_converter.ConvertFrom(true));
    }

    [Test]
    public void ConvertFrom_Type_String()
    {
        // read UTF-7 content without setting encoding
        var fileRef = _tempFileUTF7 + ";" + typeof(string).AssemblyQualifiedName;
        var result = _converter.ConvertFrom(fileRef) as string;
        Assert.IsNotNull(result);
        Assert.IsFalse(result == "\u0021\u0026\u002A\u003B");

        // read UTF-7 content using UTF-7 encoding
        fileRef = _tempFileUTF7 + ";" + typeof(string).AssemblyQualifiedName + ";utf-7";
        result = _converter.ConvertFrom(fileRef) as string;
        Assert.IsNotNull(result);
        Assert.That((object?)result, Is.EqualTo("\u0021\u0026\u002A\u003B"));

        // invalid encoding
        fileRef = _tempFileUTF7 + ";" + typeof(string).AssemblyQualifiedName + ";utf-99";
        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                _converter.ConvertFrom(fileRef);
            }
            catch (ArgumentException ex)
            {
                object expected = typeof(ArgumentException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                Assert.IsTrue(ex.Message.IndexOf("'utf-99'") != -1);
                Assert.IsNotNull(ex.ParamName);
                Assert.That((object?)ex.ParamName, Is.EqualTo("name"));
                throw;
            }
        });
    }

    [Test]
    public void ConvertFrom_Type_String_FilePathWithBackslashes()
    {
        if (Path.DirectorySeparatorChar == '\\')
            // non-windows test
            return;

        var fileContents = "foobar";
        var fileName = "foo.txt";
        var filePath = Path.Combine(_tempDirectory, fileName);
        File.WriteAllText(filePath, fileContents);

        filePath = _tempDirectory + "\\.\\" + fileName;

        var fileRef = filePath + ";" + typeof(string).AssemblyQualifiedName;
        var result = _converter.ConvertFrom(fileRef) as string;
        Assert.IsNotNull(result);
        Assert.That((object?)fileContents, Is.EqualTo(result));
    }

    [Test]
    public void ConvertFrom_Type_StreamReader()
    {
        // read UTF-7 content without setting encoding
        var fileRef = _tempFileUTF7 + ";" + typeof(StreamReader).AssemblyQualifiedName;
        using (var sr = (StreamReader?)_converter.ConvertFrom(fileRef))
        {
            var result = sr!.ReadToEnd();
            Assert.IsTrue(result.Length > 0);
            Assert.IsFalse(result == "\u0021\u0026\u002A\u003B");
        }

        // UTF-7 encoding is set, but not used
        fileRef = _tempFileUTF7 + ";" + typeof(StreamReader).AssemblyQualifiedName + ";utf-7";
        using (var sr = (StreamReader?)_converter.ConvertFrom(fileRef))
        {
            var result = sr!.ReadToEnd();
            Assert.IsTrue(result.Length > 0);
            Assert.IsFalse(result == "\u0021\u0026\u002A\u003B");
        }

        // invalid encoding is set, no error
        fileRef = _tempFileUTF7 + ";" + typeof(StreamReader).AssemblyQualifiedName + ";utf-99";
        using (var sr = (StreamReader?)_converter.ConvertFrom(fileRef))
        {
            var result = sr!.ReadToEnd();
            Assert.IsTrue(result.Length > 0);
            Assert.IsFalse(result == "\u0021\u0026\u002A\u003B");
        }
    }

    [Test]
    public void ConvertFrom_Type_MemoryStream()
    {
        var fileRef = _tempFileUTF7 + ";" + typeof(MemoryStream).AssemblyQualifiedName;
        using var ms = (MemoryStream?)_converter.ConvertFrom(fileRef);
        Assert.IsTrue(ms!.Length > 0);
    }

    [Test]
    public void ConvertTo()
    {
        var r = new ResXFileRef("mono.bmp", "Bitmap");
        Assert.That((object?)(string?)_converter.ConvertTo(
            r, typeof(string)), Is.EqualTo("mono.bmp;Bitmap"));

        r = new ResXFileRef("mono.bmp", "Bitmap", Encoding.UTF8);
        Assert.That((object?)(string?)_converter.ConvertTo(
            r, typeof(string)), Is.EqualTo("mono.bmp;Bitmap;utf-8"));

        r = new ResXFileRef("mono.bmp", "Bitmap", null);
        Assert.That((object?)(string?)_converter.ConvertTo(
            r, typeof(string)), Is.EqualTo("mono.bmp;Bitmap"));
    }

    private TypeConverter _converter;
    private string _tempDirectory;
    private string _tempFileUTF7;
}