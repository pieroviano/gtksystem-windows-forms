using System.Collections;
using System.Drawing;
using System.Resources;


namespace GtkTests;

internal class CompatTestHelper
{
    public static void TestReader(string fileName)
    {
        var r = new ResXResourceReader(fileName);
        var h = new Hashtable();
        foreach (DictionaryEntry e in r!)
        {
            h.Add(e.Key, e.Value);
        }
        r.Close();

        var message = fileName + "#1";
        Assert.That((object?)(string)h["String"]!, Is.EqualTo("hola"));
        var message1 = fileName + "#2";
        Assert.That((object?)(string)h["String2"]!, Is.EqualTo("hello"), message1);
        var message2 = fileName + "#3";
        Assert.That((object?)(int?)h["Int"], Is.EqualTo(42), message2);
        var message3 = fileName + "#4";
        Assert.That((object?)(PlatformID?)h["Enum"], Is.EqualTo(PlatformID.Win32NT), message3);
        var message4 = fileName + "#5";
        Assert.That((object?)((Point)h["Convertible"]!).X, Is.EqualTo(43), message4);
        var message5 = fileName + "#7";
        Assert.That((object?)((byte[])h["ByteArray"]!)[1], Is.EqualTo(13), message5);
        var message6 = fileName + "#8";
        Assert.That((object?)((byte[])h["ByteArray2"]!)[1], Is.EqualTo(16), message6);
        Assert.IsNull(h["InvalidMimeType"]);
        Assert.IsNotNull(h["Image"], fileName + "#12");
        object? expected = typeof(Bitmap).FullName;
        var message7 = fileName + "#13";
        Assert.That((object?)h["Image"].GetType().FullName, Is.EqualTo(expected), message7);
    }
}