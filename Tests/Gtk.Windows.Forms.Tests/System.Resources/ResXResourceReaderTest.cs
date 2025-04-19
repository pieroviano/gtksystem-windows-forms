//
// ResXResourceReaderTest.cs: Unit Tests for ResXResourceReader.
//
// Authors:
//     Gert Driesen <drieseng@users.sourceforge.net>
//     Olivier Dufour <olivier.duff@gmail.com>
//     Gary Barnett <gary.barnett.mono@gmail.com>

using System.Collections;
using System.Globalization;
using System.Resources;
using System.Text;
using System.Xml;
using System.Reflection;
using GtkTests.Helpers;
using GtkTests.TypeResolutionService_;
using GtkTests.Resources;

namespace GtkTests.System.Resources;

[TestFixture]
public class ResXResourceReaderTest : TestHelper
{
    private string _tempDirectory;
    private string _otherTempDirectory;

    [SetUp]
    protected override void SetUp()
    {
        _tempDirectory = Path.Combine(Path.GetTempPath(), "ResXResourceReaderTest");
        _otherTempDirectory = Path.Combine(_tempDirectory, "in");
        if (!Directory.Exists(_otherTempDirectory))
        {
            Directory.CreateDirectory(_otherTempDirectory);
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

    [Test] // ctor (Stream)
    public void Constructor1_Stream_InvalidContent()
    {
        var ms = new MemoryStream();
        ms.WriteByte(byte.MaxValue);
        ms.Position = 0;
        var r = new ResXResourceReader(ms);
        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                _ = r.GetEnumerator();
            }
            catch (ArgumentException ex)
            {
                // Invalid ResX input
                object expected = typeof(ArgumentException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNotNull(ex.Message);
                Assert.IsNull(ex.ParamName);
                Assert.IsNotNull(ex.InnerException);
                throw;
            }
        });
    }

    [Test] // ctor (Stream)
    [Category("NotDotNet")] // MS throws a NullReferenceException in GetEnumerator ()
    public void Constructor1_Stream_Null()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                _ = new ResXResourceReader((Stream?)null);
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

    [Test] // ctor (String)
    public void Constructor2_FileName_DoesNotExist()
    {
        var r = new ResXResourceReader((string)"definitelydoesnotexist.zzz");
        Assert.Throws<FileNotFoundException>(() =>
        {
            try
            {
                _ = r.GetEnumerator();
            }
            catch (FileNotFoundException ex)
            {
                object expected = typeof(FileNotFoundException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNotNull(ex.FileName);
                Assert.IsNotNull(ex.Message);
                Assert.IsNull(ex.InnerException);
                throw;
            }
        });
    }

    [Test] // ctor (TextReader)
    public void Constructor3_Reader_InvalidContent()
    {
        var sr = new StringReader("</definitelyinvalid<");
        var r = new ResXResourceReader(sr);
        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                _ = r.GetEnumerator();
            }
            catch (ArgumentException ex)
            {
                // Invalid ResX input
                object expected = typeof(ArgumentException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNotNull(ex.Message);
                Assert.IsNull(ex.ParamName);
                Assert.IsNotNull(ex.InnerException);
                object expected1 = typeof(XmlException);
                Assert.That((object?)ex.InnerException.GetType(), Is.EqualTo(expected1));
                throw;
            }
        });
    }

    [Test]
    public void Close_FileName()
    {
        var fileName = TestResourceHelper.GetFullPathOfResource("GtkTests.System.Resources.compat_1_1.resx");

        var r1 = new ResXResourceReader(fileName);
        r1.GetEnumerator();
        r1.Close();
        r1.GetEnumerator();

        var r2 = new ResXResourceReader(fileName);
        r2.Close();
        r2.GetEnumerator();
        r2.Close();
    }

    [Test]
    public void Close_Reader()
    {
        var fileName = TestResourceHelper.GetFullPathOfResource("GtkTests.System.Resources.compat_1_1.resx");

        using (var sr = new StreamReader(fileName))
        {
            var r = new ResXResourceReader(sr);
            Assert.IsFalse(sr.Peek() == -1);
            r.GetEnumerator();
            Assert.IsTrue(sr.Peek() == -1);
            r.Close();
            Assert.Throws<ObjectDisposedException>(() =>
            {
                sr.Peek();
            });
        }

        using (var sr = new StreamReader(fileName))
        {
            var r = new ResXResourceReader(sr);
            r.Close();
            Assert.Throws<ObjectDisposedException>(() =>
            {
                sr.Peek();
            });
        }
    }

    [Test]
    public void Close_Stream()
    {
        var fileName = TestResourceHelper.GetFullPathOfResource("GtkTests.System.Resources.compat_1_1.resx");

        using (var fs = File.OpenRead(fileName))
        {
            var r = new ResXResourceReader(fs);
            Assert.That((object?)fs.Position, Is.EqualTo(0));
            r.GetEnumerator();
            Assert.IsFalse(fs.Position == 0);
            Assert.IsTrue(fs.CanRead);
            r.Close();
            Assert.IsTrue(fs.CanRead);
            r.GetEnumerator()!.MoveNext();
        }

        using (var fs = File.OpenRead(fileName))
        {
            var r = new ResXResourceReader(fs);
            r.Close();
            Assert.That((object?)fs.Position, Is.EqualTo(0));
            r.GetEnumerator();
            Assert.IsFalse(fs.Position == 0);
        }
    }

    [Test]
    public void Namespaces()
    {
        const string resXTemplate =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
            "<o:Document xmlns:x=\"http://www.example.com\" xmlns:o=\"http://tempuri.org\">" +
            "	<o:Content>" +
            "		<x:DaTa name=\"name\">" +
            "			<o:value>de Icaza</o:value>" +
            "		</x:DaTa>" +
            "		<x:data name=\"firstName\">" +
            "			<x:value />" +
            "		</x:data>" +
            "		<o:data name=\"Address\" />" +
            "		<o:data name=\"city\">" +
            "			<x:value>Boston </x:value>" +
            "		</o:data>" +
            "		<o:data name=\"country\">" +
            "			 United States " +
            "		</o:data>" +
            "		<o:data name=\"\">" +
            "			BO    " +
            "		</o:data>" +
            "		<o:data name=\"country\">" +
            "			<x:value>Belgium</x:value>" +
            "		</o:data>" +
            "		<data name=\"zip\">" +
            "			<value><![CDATA[ <3510> ]]></value>" +
            "		</data>" +
            "	</o:Content>" +
            "	<o:Paragraph>" +
            "		<o:resheader name=\"resmimetype\">" +
            "			<o:value>{0}</o:value>" +
            "		</o:resheader>" +
            "		<x:resheader name=\"version\">" +
            "			<o:value>{1}</o:value>" +
            "		</x:resheader>" +
            "	</o:Paragraph>" +
            "	<x:Section>" +
            "		<o:resheader name=\"reader\">" +
            "			<x:value>System.Resources.ResXResourceReader, {2}</x:value>" +
            "		</o:resheader>" +
            "		<x:resheader name=\"writer\">" +
            "			<x:value>System.Resources.ResXResourceWriter, {2}</x:value>" +
            "		</x:resheader>" +
            "	</x:Section>" +
            "</o:Document>";

        var resxFile = Path.Combine(_tempDirectory, "resources.resx");
        using (var sw = new StreamWriter(resxFile, false, Encoding.UTF8))
        {
            sw.Write(string.Format(CultureInfo.InvariantCulture,
                resXTemplate, ResXResourceWriter.ResMimeType, "1.0",
                Consts.AssemblySystem_Windows_Forms));
        }

        // Stream
        using (var fs = new FileStream(resxFile, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            using (var r = new ResXResourceReader(fs))
            {
                var enumerator = r.GetEnumerator();
                var entries = 0;
                while (enumerator!.MoveNext())
                {
                    entries++;
                    switch ((string)enumerator.Key)
                    {
                        case "":
                            Assert.IsNotNull(enumerator.Value);
                            Assert.That(enumerator.Value, Is.EqualTo("BO"));
                            break;
                        case "Address":
                            Assert.IsNotNull(enumerator.Value);
                            object expected = string.Empty;
                            Assert.That(enumerator.Value, Is.EqualTo(expected));
                            break;
                        case "country":
                            Assert.IsNotNull(enumerator.Value);
                            object expected1 = string.Empty;
                            Assert.That(enumerator.Value, Is.EqualTo(expected1));
                            break;
                        case "firstName":
                            Assert.IsNull(enumerator.Value, "#D");
                            break;
                        case "zip":
                            Assert.IsNotNull(enumerator.Value);
                            Assert.That(enumerator.Value, Is.EqualTo(" <3510> "));
                            break;
                        default:
                            throw new ArgumentException(enumerator.Key?.ToString());
                            break;
                    }
                }
                Assert.That((object?)entries, Is.EqualTo(5), "#G");
            }
        }
    }

    [Test]
    public void ResHeader_Unknown()
    {
        const string resXTemplate =
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
            "<root>" +
            "	<resheader name=\"resmimetype\">" +
            "		{0}" +
            "	</resheader>" +
            "	<resheader name=\"version\">" +
            "		<value>{1}</value>" +
            "	</resheader>" +
            "	<resheader name=\"reader\">" +
            "		<value>  System.Resources.ResXResourceReader  , {2}</value>" +
            "	</resheader>" +
            "	<resheader name=\"writer\">" +
            "		<value>  System.Resources.ResXResourceWriter  , {2}</value>" +
            "	</resheader>" +
            "	<resheader name=\"UNKNOWN\">" +
            "		<value>whatever</value>" +
            "	</resheader>" +
            "</root>";

        var resXContent = string.Format(CultureInfo.InvariantCulture,
            resXTemplate, ResXResourceWriter.ResMimeType, "1.0",
            Consts.AssemblySystem_Windows_Forms);
        using var r = new ResXResourceReader(new StringReader(resXContent));
        r.GetEnumerator();
    }

    private static readonly string resXWithEmptyName =
        @"<?xml version=""1.0"" encoding=""utf-8""?>
<root>
  <resheader name=""resmimetype"">
	<value>text/microsoft-resx</value>
  </resheader>
  <resheader name=""version"">
	<value>2.0</value>
  </resheader>
  <resheader name=""reader"">
	<value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
  </resheader>
  <resheader name=""writer"">
	<value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
  </resheader>
  <data name="""">
	<value>a resource with no name</value>
  </data>
</root>";

    [Test]
    public void ResName_Empty()
    {
        using var sr = new StringReader(resXWithEmptyName);
        using var r = new ResXResourceReader(sr);
        var enumerator = r.GetEnumerator();
        enumerator!.MoveNext();
        Assert.That(enumerator.Key, Is.EqualTo(string.Empty));
        Assert.That((object?)(string?)enumerator.Value, Is.EqualTo("a resource with no name"));
    }

    [Test]
    public void ResValue()
    {
        var resXContent = string.Format(CultureInfo.CurrentCulture,
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
            "<root>" +
            "	<resheader name=\"resmimetype\">" +
            "		<value>{0}</value>" +
            "	</resheader>" +
            "	<resheader name=\"reader\">" +
            "		<value>System.Resources.ResXResourceReader, {1}</value>" +
            "	</resheader>" +
            "	<resheader name=\"writer\">" +
            "		<value>System.Resources.ResXResourceWriter, {1}</value>" +
            "	</resheader>" +
            "	<data name=\"name1\">" +
            "		<value><![CDATA[ <value1> ]]></value>" +
            "	</data>" +
            "	<data name=\"name2\">" +
            "		<value>  <![CDATA[<value2>]]>  </value>" +
            "	</data>" +
            "	<data name=\"name3\">" +
            "		  <![CDATA[<value3>]]>  " +
            "	</data>" +
            "	<data name=\"name4\">" +
            "		<value> value4 </value>" +
            "	</data>" +
            "	<data name=\"name5\">" +
            "		test<value>value5</value>" +
            "	</data>" +
            "	<data name=\"name6\">" +
            "		test1<value>value6</value>test2" +
            "	</data>" +
            "	<data name=\"name7\">" +
            "		<value>value7a</value>" +
            "		<whatever>value7b</whatever>" +
            "	</data>" +
            "	<data name=\"name8\">" +
            "		<whatever>value8</whatever>" +
            "	</data>" +
            "	<data name=\"name9\">" +
            "		<whatever>value9a</whatever>" +
            "		<whatever>value9b</whatever>" +
            "	</data>" +
            "	<data name=\"name10\">" +
            "		test<whatever>value10</whatever>" +
            "	</data>" +
            "	<data name=\"name11\">" +
            "		test1<whatever>value11</whatever>test2" +
            "	</data>" +
            "	<data name=\"name12\">" +
            "		<value> test  <![CDATA[<value12>]]>  </value>" +
            "	</data>" +
            "	<data name=\"name13\">" +
            "		 test <![CDATA[<value13>]]>  " +
            "	</data>" +
            "	<data name=\"name14\" />" +
            "	<data name=\"name15\"></data>" +
            "	<data name=\"name16\">value16</data>" +
            "	<data name=\"name17\">value17</data>" +
            "	<data name=\"name18\">" +
            "		<value>value18</value>" +
            "		<data name=\"name19\">" +
            "			<value>value18</value>" +
            "		</data>" +
            "	</data>" +
            "</root>",
            ResXResourceWriter.ResMimeType, Consts.AssemblySystem_Windows_Forms);

        using var sr = new StringReader(resXContent);
        using var r = new ResXResourceReader(sr);
        var enumerator = r.GetEnumerator();
        var entries = 0;
        while (enumerator!.MoveNext())
        {
            entries++;
            switch ((string)enumerator.Key)
            {
                case "name1":
                    Assert.IsNotNull(enumerator.Value);
                    Assert.That(enumerator.Value, Is.EqualTo(" <value1> "));
                    break;
                case "name2":
                    Assert.IsNotNull(enumerator.Value);
                    Assert.That(enumerator.Value, Is.EqualTo("<value2>"));
                    break;
                case "name3":
                    Assert.IsNotNull(enumerator.Value);
                    Assert.That(enumerator.Value, Is.EqualTo("<value3>"));
                    break;
                case "name4":
                    Assert.IsNotNull(enumerator.Value);
                    Assert.That(enumerator.Value, Is.EqualTo(" value4 "));
                    break;
                case "name5":
                    Assert.IsNotNull(enumerator.Value);
                    Assert.That(enumerator.Value, Is.EqualTo("value5"));
                    break;
                case "name6":
                    Assert.IsNotNull(enumerator.Value);
                    Assert.That(enumerator.Value, Is.EqualTo("test2"));
                    break;
                case "name7":
                    Assert.IsNotNull(enumerator.Value);
                    object expected = string.Empty;
                    Assert.That(enumerator.Value, Is.EqualTo(expected));
                    break;
                case "name8":
                    Assert.IsNotNull(enumerator.Value);
                    object expected1 = string.Empty;
                    Assert.That(enumerator.Value, Is.EqualTo(expected1));
                    break;
                case "name9":
                    Assert.IsNotNull(enumerator.Value);
                    object expected2 = string.Empty;
                    Assert.That(enumerator.Value, Is.EqualTo(expected2));
                    break;
                case "name10":
                    Assert.IsNotNull(enumerator.Value);
                    object expected3 = string.Empty;
                    Assert.That(enumerator.Value, Is.EqualTo(expected3));
                    break;
                case "name11":
                    Assert.IsNotNull(enumerator.Value);
                    Assert.That(enumerator.Value, Is.EqualTo("test2"));
                    break;
                case "name12":
                    Assert.IsNotNull(enumerator.Value);
                    Assert.That(enumerator.Value, Is.EqualTo(" test  <value12>"));
                    break;
                case "name13":
                    Assert.IsNotNull(enumerator.Value);
                    Assert.That(enumerator.Value, Is.EqualTo("<value13>"));
                    break;
                case "name14":
                    Assert.IsNull(enumerator.Value);
                    break;
                case "name16":
                    Assert.IsNotNull(enumerator.Value);
                    Assert.That(enumerator.Value, Is.EqualTo("value16"));
                    break;
                case "name17":
                    Assert.IsNotNull(enumerator.Value);
                    Assert.That(enumerator.Value, Is.EqualTo("value17"));
                    break;
                case "name18":
                    Assert.IsNotNull(enumerator.Value);
                    Assert.That(enumerator.Value, Is.EqualTo("value18"));
                    break;
                default:
                    throw new ArgumentException(enumerator.Key?.ToString());
            }
        }
        Assert.That((object?)entries, Is.EqualTo(17), "#Q");
    }

    [Test]
    public void EnumeratorOrderSameAsResx()
    {
        var resXContent = string.Format(CultureInfo.CurrentCulture,
            "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
            "<root>" +
            "	<resheader name=\"resmimetype\">" +
            "		<value>{0}</value>" +
            "	</resheader>" +
            "	<resheader name=\"reader\">" +
            "		<value>System.Resources.ResXResourceReader, {1}</value>" +
            "	</resheader>" +
            "	<resheader name=\"writer\">" +
            "		<value>System.Resources.ResXResourceWriter, {1}</value>" +
            "	</resheader>" +
            "	<data name=\"name2\">" +
            "		<value> value5 </value>" +
            "	</data>" +
            "	<data name=\"name1\">" +
            "		<value> value4 </value>" +
            "	</data>" +
            "	<data name=\"aaa\">" +
            "		<value> value3 </value>" +
            "	</data>" +
            "	<data name=\"zzzz\">" +
            "		<value> value2 </value>" +
            "	</data>" +
            "	<data name=\"bbbbbb\">" +
            "		<value> value1 </value>" +
            "	</data>" +
            "</root>",
            ResXResourceWriter.ResMimeType, Consts.AssemblySystem_Windows_Forms);

        using var sr = new StringReader(resXContent);
        using var r = new ResXResourceReader(sr);
        var enumerator = r.GetEnumerator();
        enumerator!.MoveNext();
        Assert.That(enumerator.Key, Is.EqualTo("name2"));
        enumerator.MoveNext();
        Assert.That(enumerator.Key, Is.EqualTo("name1"));
        enumerator.MoveNext();
        Assert.That(enumerator.Key, Is.EqualTo("aaa"));
        enumerator.MoveNext();
        Assert.That(enumerator.Key, Is.EqualTo("zzzz"));
        enumerator.MoveNext();
        Assert.That(enumerator.Key, Is.EqualTo("bbbbbb"));
    }

    [Test]
    public void FileRef_DeserializationFails()
    {
        Assert.Throws<TargetInvocationException>(() =>
        {
            var corruptFile = Path.GetTempFileName();
            var fileRef = new ResXFileRef(corruptFile, typeof(Serializable).AssemblyQualifiedName);

            File.AppendAllText(corruptFile, "corrupt");

            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                using (var writer = new ResXResourceWriter(sw))
                {
                    writer.AddResource("test", fileRef);
                }
            }

            using (var sr = new StringReader(sb.ToString()))
            {
                using (var reader = new ResXResourceReader(sr))
                {
                    reader.GetEnumerator();
                }
            }
        });
    }

    [Test]
    public void FileRef_TypeCantBeResolved()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var aFile = Path.GetTempFileName();
            var fileRef = new ResXFileRef(aFile, "a.type.doesnt.exist");

            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                using (var writer = new ResXResourceWriter(sw))
                {
                    writer.AddResource("test", fileRef);
                }
            }

            using (var sr = new StringReader(sb.ToString()))
            {
                using (var reader = new ResXResourceReader(sr))
                {
                    reader.GetEnumerator();
                }
            }
        });
    }

    [Test]
    public void TypeConverter_ITRSUsed()
    {
        var dn = new ResXDataNode("test", 34L);

        var sb = new StringBuilder();
        using (var sw = new StringWriter(sb))
        {
            using (var writer = new ResXResourceWriter(sw))
            {
                writer.AddResource(dn);
            }
        }

        using (var sr = new StringReader(sb.ToString()))
        {
            var rr = new ResXResourceReader(sr, new ReturnIntITRS());
            var en = rr.GetEnumerator();
            en!.MoveNext();

            var o = ((DictionaryEntry)en.Current).Value;
            Assert.IsNotNull(o);
            Assert.True(o is int);
            Assert.That(o, Is.EqualTo(34));
            rr.Close();
        }
    }
}