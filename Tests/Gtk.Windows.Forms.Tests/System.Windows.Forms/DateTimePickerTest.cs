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
//	Rolf Bjarne Kvinge	RKvinge@novell.com

using System.Windows.Forms;
using System.Globalization;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;
using GtkTests.Helpers;
#if NET462_OR_GREATER
using GtkSystemColors = System.Drawing.SystemColors;
#endif

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class DateTimePickerTest : TestHelper
{

    [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, EntryPoint = "GetUserDefaultLCID")]
    private extern static int GetUserDefaultLCID();

    private static void CheckCulture()
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        if (Thread.CurrentThread.CurrentCulture.Name != "en-US")
        {
            Assert.Ignore("Must be called with us-english locale, current locale is: " + Thread.CurrentThread.CurrentCulture.Name);
        }
    }

    // On Windows this test must be called with en-US locale specified in the regional settings.
    // There is no way to change this programmatically for the test to run correctly on other locales
    // (see: http://www.microsoft.com/globaldev/getWR/steps/WRG_lclmdl.mspx#EOE)
    // To regenerate this test call GenerateCustomFormatTests() and paste the result here.
    [Test]
    public void CustomFormatTestGenerated()
    {
        if (!Environment.OSVersion.Platform.ToString().StartsWith("Win"))
        {
            return;
        }
        CheckCulture();

        using var frm = new Form();
        var dt = new DateTimePicker();
        frm.Controls.Add(dt);
        frm.Show();

        dt.Format = DateTimePickerFormat.Custom;
        dt.CustomFormat = "ddd";
        dt.Value = new DateTime(2007, 2, 8, 15, 30, 45, 60);

        dt.CustomFormat = @"dd/MM/yy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02/07"));
        dt.CustomFormat = @"dd/MM/yyyy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02/2007"));
        dt.CustomFormat = @"dd/MMMM/yyyy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/February/2007"));
        dt.CustomFormat = @"dddd, dd MMMM, yyyy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08 February, 2007"));
        dt.CustomFormat = @"dd/MMMM/yyyy hh:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/February/2007 03:30 PM"));
        dt.CustomFormat = @"dd/MMMM/yyyy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/February/2007 15:30"));
        dt.CustomFormat = @"dddd, dd MMMM, yyyy hh:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08 February, 2007 03:30 PM"));
        dt.CustomFormat = @"dddd, dd MMMM, yyyy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08 February, 2007 15:30"));
        dt.CustomFormat = @"dd/MMMM/yyyy hh:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/February/2007 03:30:45 PM"));
        dt.CustomFormat = @"dd/MMMM/yyyy HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/February/2007 15:30:45"));
        dt.CustomFormat = @"dddd, dd MMMM, yyyy hh:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08 February, 2007 03:30:45 PM"));
        dt.CustomFormat = @"dddd, dd MMMM, yyyy HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08 February, 2007 15:30:45"));
        dt.CustomFormat = @"dd/MM/yy hh:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02/07 03:30 PM"));
        dt.CustomFormat = @"dd/MM/yy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02/07 15:30"));
        dt.CustomFormat = @"dd/MM/yyyy hh:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02/2007 03:30 PM"));
        dt.CustomFormat = @"dd/MM/yyyy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02/2007 15:30"));
        dt.CustomFormat = @"dd/MM/yy hh:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02/07 03:30:45 PM"));
        dt.CustomFormat = @"dd/MM/yy HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02/07 15:30:45"));
        dt.CustomFormat = @"dd/MM/yyyy hh:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02/2007 03:30:45 PM"));
        dt.CustomFormat = @"dd/MM/yyyy HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02/2007 15:30:45"));
        dt.CustomFormat = @"dd MMMM";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08 February"));
        dt.CustomFormat = @"yyyy'-'MM'-'dd'T'HH':'mm':'ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007-02-08T15:30:45"));
        dt.CustomFormat = @"ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thu, 08 Feb 2007 15:30:45 GMT"));
        dt.CustomFormat = @"yyyy'-'MM'-'dd'T'HH':'mm':'ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007-02-08T15:30:45"));
        dt.CustomFormat = @"hh:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"03:30 PM"));
        dt.CustomFormat = @"HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"15:30"));
        dt.CustomFormat = @"hh:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"03:30:45 PM"));
        dt.CustomFormat = @"HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"15:30:45"));
        dt.CustomFormat = @"yyyy'-'MM'-'dd HH':'mm':'ss'Z'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007-02-08 15:30:45Z"));
        dt.CustomFormat = @"MMMM, yyyy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February, 2007"));
        dt.CustomFormat = @"dd.M.yyyy 'г.'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.2.2007 г."));
        dt.CustomFormat = @"d.M.yyyy 'г.'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.2.2007 г."));
        dt.CustomFormat = @"dd.MM.yyyy 'г.'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.02.2007 г."));
        dt.CustomFormat = @"yyyy-MM-dd";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007-02-08"));
        dt.CustomFormat = @"dd MMMM yyyy 'г.'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08 February 2007 г."));
        dt.CustomFormat = @"dddd, dd MMMM yyyy 'г.'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08 February 2007 г."));
        dt.CustomFormat = @"dd MMMM yyyy 'г.' HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08 February 2007 г. 15:30"));
        dt.CustomFormat = @"dd MMMM yyyy 'г.' H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08 February 2007 г. 15:30"));
        dt.CustomFormat = @"dddd, dd MMMM yyyy 'г.' HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08 February 2007 г. 15:30"));
        dt.CustomFormat = @"dddd, dd MMMM yyyy 'г.' H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08 February 2007 г. 15:30"));
        dt.CustomFormat = @"dd MMMM yyyy 'г.' HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08 February 2007 г. 15:30:45"));
        dt.CustomFormat = @"dd MMMM yyyy 'г.' H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08 February 2007 г. 15:30:45"));
        dt.CustomFormat = @"dddd, dd MMMM yyyy 'г.' HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08 February 2007 г. 15:30:45"));
        dt.CustomFormat = @"dddd, dd MMMM yyyy 'г.' H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08 February 2007 г. 15:30:45"));
        dt.CustomFormat = @"dd.M.yyyy 'г.' HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.2.2007 г. 15:30"));
        dt.CustomFormat = @"dd.M.yyyy 'г.' H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.2.2007 г. 15:30"));
        dt.CustomFormat = @"d.M.yyyy 'г.' HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.2.2007 г. 15:30"));
        dt.CustomFormat = @"d.M.yyyy 'г.' H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.2.2007 г. 15:30"));
        dt.CustomFormat = @"dd.MM.yyyy 'г.' HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.02.2007 г. 15:30"));
        dt.CustomFormat = @"dd.MM.yyyy 'г.' H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.02.2007 г. 15:30"));
        dt.CustomFormat = @"yyyy-MM-dd HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007-02-08 15:30"));
        dt.CustomFormat = @"yyyy-MM-dd H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007-02-08 15:30"));
        dt.CustomFormat = @"dd.M.yyyy 'г.' HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.2.2007 г. 15:30:45"));
        dt.CustomFormat = @"dd.M.yyyy 'г.' H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.2.2007 г. 15:30:45"));
        dt.CustomFormat = @"d.M.yyyy 'г.' HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.2.2007 г. 15:30:45"));
        dt.CustomFormat = @"d.M.yyyy 'г.' H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.2.2007 г. 15:30:45"));
        dt.CustomFormat = @"dd.MM.yyyy 'г.' HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.02.2007 г. 15:30:45"));
        dt.CustomFormat = @"dd.MM.yyyy 'г.' H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.02.2007 г. 15:30:45"));
        dt.CustomFormat = @"yyyy-MM-dd HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007-02-08 15:30:45"));
        dt.CustomFormat = @"yyyy-MM-dd H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007-02-08 15:30:45"));
        dt.CustomFormat = @"H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"15:30"));
        dt.CustomFormat = @"H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"15:30:45"));
        dt.CustomFormat = @"MMMM yyyy 'г.'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February 2007 г."));
        dt.CustomFormat = @"d/MM/yy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/02/07"));
        dt.CustomFormat = @"d/M/yy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/2/07"));
        dt.CustomFormat = @"dd-MM-yy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08-02-07"));
        dt.CustomFormat = @"dd.MM.yy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.02.07"));
        dt.CustomFormat = @"dddd, d' / 'MMMM' / 'yyyy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 8 / February / 2007"));
        dt.CustomFormat = @"d'/'MMMM'/'yyyy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/February/2007"));
        dt.CustomFormat = @"d' 'MMMM' 'yyyy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 February 2007"));
        dt.CustomFormat = @"dddd, d' / 'MMMM' / 'yyyy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 8 / February / 2007 15:30"));
        dt.CustomFormat = @"dddd, d' / 'MMMM' / 'yyyy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 8 / February / 2007 15:30"));
        dt.CustomFormat = @"dddd, d' / 'MMMM' / 'yyyy HH'H'mm'\''";
        Assert.That((object?)dt.Text, Is.EqualTo("Thursday, 8 / February / 2007 15H30'"));
        dt.CustomFormat = @"d'/'MMMM'/'yyyy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/February/2007 15:30"));
        dt.CustomFormat = @"d'/'MMMM'/'yyyy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/February/2007 15:30"));
        dt.CustomFormat = @"d'/'MMMM'/'yyyy HH'H'mm'\''";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/February/2007 15H30'"));
        dt.CustomFormat = @"d' 'MMMM' 'yyyy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 February 2007 15:30"));
        dt.CustomFormat = @"d' 'MMMM' 'yyyy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 February 2007 15:30"));
        dt.CustomFormat = @"d' 'MMMM' 'yyyy HH'H'mm'\''";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 February 2007 15H30'"));
        dt.CustomFormat = @"dddd, d' / 'MMMM' / 'yyyy HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 8 / February / 2007 15:30:45"));
        dt.CustomFormat = @"dddd, d' / 'MMMM' / 'yyyy H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 8 / February / 2007 15:30:45"));
        dt.CustomFormat = @"d'/'MMMM'/'yyyy HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/February/2007 15:30:45"));
        dt.CustomFormat = @"d'/'MMMM'/'yyyy H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/February/2007 15:30:45"));
        dt.CustomFormat = @"d' 'MMMM' 'yyyy HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 February 2007 15:30:45"));
        dt.CustomFormat = @"d' 'MMMM' 'yyyy H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 February 2007 15:30:45"));
        dt.CustomFormat = @"dd/MM/yyyy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02/2007 15:30"));
        dt.CustomFormat = @"dd/MM/yy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02/07 15:30"));
        dt.CustomFormat = @"d/MM/yy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/02/07 15:30"));
        dt.CustomFormat = @"d/MM/yy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/02/07 15:30"));
        dt.CustomFormat = @"d/M/yy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/2/07 15:30"));
        dt.CustomFormat = @"d/M/yy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/2/07 15:30"));
        dt.CustomFormat = @"dd-MM-yy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08-02-07 15:30"));
        dt.CustomFormat = @"dd-MM-yy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08-02-07 15:30"));
        dt.CustomFormat = @"dd.MM.yy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.02.07 15:30"));
        dt.CustomFormat = @"dd.MM.yy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.02.07 15:30"));
        dt.CustomFormat = @"dd-MM-yyyy tt hh:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08-02-2007 PM 03:30:45"));
        dt.CustomFormat = @"dd-MM-yyyy tt h:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08-02-2007 PM 3:30:45"));
        dt.CustomFormat = @"dd-MM-yy tt hh:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08-02-07 PM 03:30:45"));
        dt.CustomFormat = @"dd-MM-yy tt h:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08-02-07 PM 3:30:45"));
        dt.CustomFormat = @"d-M-yy tt hh:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8-2-07 PM 03:30:45"));
        dt.CustomFormat = @"d-M-yy tt h:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8-2-07 PM 3:30:45"));
        dt.CustomFormat = @"d.M.yy tt hh:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.2.07 PM 03:30:45"));
        dt.CustomFormat = @"d.M.yy tt h:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.2.07 PM 3:30:45"));
        dt.CustomFormat = @"d MMMM yyyy 'ж.'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 February 2007 ж."));
        dt.CustomFormat = @"dd MMMM yyyy 'ж.'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08 February 2007 ж."));
        dt.CustomFormat = @"d MMMM yyyy 'ж.' H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 February 2007 ж. 15:30"));
        dt.CustomFormat = @"d MMMM yyyy 'ж.' HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 February 2007 ж. 15:30"));
        dt.CustomFormat = @"dd MMMM yyyy 'ж.' H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08 February 2007 ж. 15:30"));
        dt.CustomFormat = @"dd MMMM yyyy 'ж.' HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08 February 2007 ж. 15:30"));
        dt.CustomFormat = @"d MMMM yyyy 'ж.' H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 February 2007 ж. 15:30:45"));
        dt.CustomFormat = @"d MMMM yyyy 'ж.' HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 February 2007 ж. 15:30:45"));
        dt.CustomFormat = @"dd MMMM yyyy 'ж.' H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08 February 2007 ж. 15:30:45"));
        dt.CustomFormat = @"dd MMMM yyyy 'ж.' HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08 February 2007 ж. 15:30:45"));
        dt.CustomFormat = @"d'-'MMMM yyyy'-ж.'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8-February 2007-ж."));
        dt.CustomFormat = @"d'-'MMMM yyyy'-ж.' H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8-February 2007-ж. 15:30"));
        dt.CustomFormat = @"d'-'MMMM yyyy'-ж.' H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8-February 2007-ж. 15:30:45"));
        dt.CustomFormat = @"MMMM yyyy'-ж.'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February 2007-ж."));
        dt.CustomFormat = @"dd/MM yyyy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02 2007"));
        dt.CustomFormat = @"yyyy 'yil' d-MMMM";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007 yil 8-February"));
        dt.CustomFormat = @"yyyy 'yil' d-MMMM HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007 yil 8-February 15:30"));
        dt.CustomFormat = @"yyyy 'yil' d-MMMM H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007 yil 8-February 15:30"));
        dt.CustomFormat = @"yyyy 'yil' d-MMMM HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007 yil 8-February 15:30:45"));
        dt.CustomFormat = @"yyyy 'yil' d-MMMM H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007 yil 8-February 15:30:45"));
        dt.CustomFormat = @"dd/MM yyyy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02 2007 15:30"));
        dt.CustomFormat = @"dd/MM yyyy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02 2007 15:30"));
        dt.CustomFormat = @"dd/MM yyyy HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02 2007 15:30:45"));
        dt.CustomFormat = @"dd/MM yyyy H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02 2007 15:30:45"));
        dt.CustomFormat = @"d-MMMM";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8-February"));
        dt.CustomFormat = @"dd MMMM yyyy dddd tt hh:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08 February 2007 Thursday PM 03:30"));
        dt.CustomFormat = @"dd MMMM yyyy dddd tt h:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08 February 2007 Thursday PM 3:30"));
        dt.CustomFormat = @"dd MMMM yyyy dddd H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08 February 2007 Thursday 15:30"));
        dt.CustomFormat = @"dd MMMM yyyy dddd tt hh:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08 February 2007 Thursday PM 03:30:45"));
        dt.CustomFormat = @"dd MMMM yyyy dddd tt h:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08 February 2007 Thursday PM 3:30:45"));
        dt.CustomFormat = @"dd MMMM yyyy dddd H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08 February 2007 Thursday 15:30:45"));
        dt.CustomFormat = @"yy.MM.dd";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07.02.08"));
        dt.CustomFormat = @"yyyy 'оны' MMMM d";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007 оны February 8"));
        dt.CustomFormat = @"yyyy 'оны' MMMM d H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007 оны February 8 15:30"));
        dt.CustomFormat = @"yyyy 'оны' MMMM d H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007 оны February 8 15:30:45"));
        dt.CustomFormat = @"yy.MM.dd H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07.02.08 15:30"));
        dt.CustomFormat = @"yy.MM.dd H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07.02.08 15:30:45"));
        dt.CustomFormat = @"yyyy 'он' MMMM";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007 он February"));
        dt.CustomFormat = @"dddd, dd' de 'MMMM' de 'yyyy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08 de February de 2007"));
        dt.CustomFormat = @"dddd d' de 'MMMM' de 'yyyy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday 8 de February de 2007"));
        dt.CustomFormat = @"dddd, dd' de 'MMMM' de 'yyyy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08 de February de 2007 15:30"));
        dt.CustomFormat = @"dddd, dd' de 'MMMM' de 'yyyy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08 de February de 2007 15:30"));
        dt.CustomFormat = @"dddd, dd' de 'MMMM' de 'yyyy hh:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08 de February de 2007 03:30 PM"));
        dt.CustomFormat = @"dddd, yyyy-MM-dd H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 2007-02-08 15:30:45"));
        dt.CustomFormat = @"dddd, yyyy-MM-dd HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 2007-02-08 15:30:45"));
        dt.CustomFormat = @"dddd, yyyy-MM-dd tt h:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 2007-02-08 PM 3:30:45"));
        dt.CustomFormat = @"dddd, yyyy-MM-dd tt hh:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 2007-02-08 PM 03:30:45"));
        dt.CustomFormat = @"dddd, yyyy'年'M'月'd'日' H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 2007年2月8日 15:30:45"));
        dt.CustomFormat = @"dddd, yyyy'年'M'月'd'日' HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 2007年2月8日 15:30:45"));
        dt.CustomFormat = @"dddd, yyyy'年'M'月'd'日' tt h:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 2007年2月8日 PM 3:30:45"));
        dt.CustomFormat = @"dddd, yyyy'年'M'月'd'日' tt hh:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 2007年2月8日 PM 03:30:45"));
        dt.CustomFormat = @"yyyy.M.d H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007.2.8 15:30"));
        dt.CustomFormat = @"yyyy.M.d HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007.2.8 15:30"));
        dt.CustomFormat = @"yyyy.M.d tt h:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007.2.8 PM 3:30"));
        dt.CustomFormat = @"yyyy.M.d tt hh:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007.2.8 PM 03:30"));
        dt.CustomFormat = @"yyyy.MM.dd tt h:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007.02.08 PM 3:30"));
        dt.CustomFormat = @"yyyy.MM.dd tt hh:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007.02.08 PM 03:30"));
        dt.CustomFormat = @"yy.M.d H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07.2.8 15:30"));
        dt.CustomFormat = @"yy.M.d HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07.2.8 15:30"));
        dt.CustomFormat = @"yy.M.d tt h:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07.2.8 PM 3:30"));
        dt.CustomFormat = @"yy.M.d tt hh:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07.2.8 PM 03:30"));
        dt.CustomFormat = @"yyyy.M.d H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007.2.8 15:30:45"));
        dt.CustomFormat = @"yyyy.M.d HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007.2.8 15:30:45"));
        dt.CustomFormat = @"yyyy.M.d tt h:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007.2.8 PM 3:30:45"));
        dt.CustomFormat = @"yyyy.M.d tt hh:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007.2.8 PM 03:30:45"));
        dt.CustomFormat = @"yyyy.MM.dd tt h:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007.02.08 PM 3:30:45"));
        dt.CustomFormat = @"yyyy.MM.dd tt hh:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007.02.08 PM 03:30:45"));
        dt.CustomFormat = @"yy.M.d H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07.2.8 15:30:45"));
        dt.CustomFormat = @"yy.M.d HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07.2.8 15:30:45"));
        dt.CustomFormat = @"yy.M.d tt h:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07.2.8 PM 3:30:45"));
        dt.CustomFormat = @"yy.M.d tt hh:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07.2.8 PM 03:30:45"));
        dt.CustomFormat = @"yyyy.M";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007.2"));
        dt.CustomFormat = @"dd. M. yy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08. 2. 07"));
        dt.CustomFormat = @"d. MMM yy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8. Feb 07"));
        dt.CustomFormat = @"dddd, d. MMMM yyyy H.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 8. February 2007 15.30 h"));
        dt.CustomFormat = @"dddd, d. MMMM yyyy HH.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 8. February 2007 15.30 h"));
        dt.CustomFormat = @"dddd, d. MMMM yyyy H.mm' Uhr'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 8. February 2007 15.30 Uhr"));
        dt.CustomFormat = @"d. MMMM yyyy H.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8. February 2007 15.30 h"));
        dt.CustomFormat = @"d. MMMM yyyy HH.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8. February 2007 15.30 h"));
        dt.CustomFormat = @"d. MMMM yyyy H.mm' Uhr'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8. February 2007 15.30 Uhr"));
        dt.CustomFormat = @"d. MMM yy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8. Feb 07 15:30"));
        dt.CustomFormat = @"d. MMM yy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8. Feb 07 15:30"));
        dt.CustomFormat = @"d. MMM yy H.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8. Feb 07 15.30 h"));
        dt.CustomFormat = @"d. MMM yy HH.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8. Feb 07 15.30 h"));
        dt.CustomFormat = @"d. MMM yy H.mm' Uhr'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8. Feb 07 15.30 Uhr"));
        dt.CustomFormat = @"d. MMM yy HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8. Feb 07 15:30:45"));
        dt.CustomFormat = @"d. MMM yy H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8. Feb 07 15:30:45"));
        dt.CustomFormat = @"dd.MM.yyyy H.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.02.2007 15.30 h"));
        dt.CustomFormat = @"dd.MM.yyyy HH.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.02.2007 15.30 h"));
        dt.CustomFormat = @"dd.MM.yyyy H.mm' Uhr'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.02.2007 15.30 Uhr"));
        dt.CustomFormat = @"dd.MM.yy H.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.02.07 15.30 h"));
        dt.CustomFormat = @"dd.MM.yy HH.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.02.07 15.30 h"));
        dt.CustomFormat = @"dd.MM.yy H.mm' Uhr'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.02.07 15.30 Uhr"));
        dt.CustomFormat = @"d.MM.yy H.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.02.07 15.30 h"));
        dt.CustomFormat = @"d.MM.yy HH.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.02.07 15.30 h"));
        dt.CustomFormat = @"d.MM.yy H.mm' Uhr'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.02.07 15.30 Uhr"));
        dt.CustomFormat = @"dd. M. yy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08. 2. 07 15:30"));
        dt.CustomFormat = @"dd. M. yy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08. 2. 07 15:30"));
        dt.CustomFormat = @"dd. M. yy H.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08. 2. 07 15.30 h"));
        dt.CustomFormat = @"dd. M. yy HH.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08. 2. 07 15.30 h"));
        dt.CustomFormat = @"dd. M. yy H.mm' Uhr'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08. 2. 07 15.30 Uhr"));
        dt.CustomFormat = @"d.M.yy H.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.2.07 15.30 h"));
        dt.CustomFormat = @"d.M.yy HH.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.2.07 15.30 h"));
        dt.CustomFormat = @"d.M.yy H.mm' Uhr'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.2.07 15.30 Uhr"));
        dt.CustomFormat = @"yyyy-MM-dd H.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007-02-08 15.30 h"));
        dt.CustomFormat = @"yyyy-MM-dd HH.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007-02-08 15.30 h"));
        dt.CustomFormat = @"yyyy-MM-dd H.mm' Uhr'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007-02-08 15.30 Uhr"));
        dt.CustomFormat = @"dd. M. yy HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08. 2. 07 15:30:45"));
        dt.CustomFormat = @"dd. M. yy H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08. 2. 07 15:30:45"));
        dt.CustomFormat = @"H.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"15.30 h"));
        dt.CustomFormat = @"HH.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"15.30 h"));
        dt.CustomFormat = @"H.mm' Uhr'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"15.30 Uhr"));
        dt.CustomFormat = @"d.M.yy hh:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.2.07 03:30 PM"));
        dt.CustomFormat = @"d.M.yy h:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.2.07 3:30 PM"));
        dt.CustomFormat = @"d.M.yy hh:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.2.07 03:30:45 PM"));
        dt.CustomFormat = @"d.M.yy h:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.2.07 3:30:45 PM"));
        dt.CustomFormat = @"dddd, dd' de 'MMMM' de 'yyyy h:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08 de February de 2007 3:30 PM"));
        dt.CustomFormat = @"dddd d' de 'MMMM' de 'yyyy h:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday 8 de February de 2007 3:30 PM"));
        dt.CustomFormat = @"d' de 'MMMM' de 'yyyy h:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 de February de 2007 3:30 PM"));
        dt.CustomFormat = @"dddd, dd' de 'MMMM' de 'yyyy h:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08 de February de 2007 3:30:45 PM"));
        dt.CustomFormat = @"dddd d' de 'MMMM' de 'yyyy h:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday 8 de February de 2007 3:30:45 PM"));
        dt.CustomFormat = @"d' de 'MMMM' de 'yyyy h:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 de February de 2007 3:30:45 PM"));
        dt.CustomFormat = @"d/MM/yy h:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/02/07 3:30 PM"));
        dt.CustomFormat = @"d/MM/yy h:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/02/07 3:30:45 PM"));
        dt.CustomFormat = @"dddd d MMMM yyyy H' h 'mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday 8 February 2007 15 h 30"));
        dt.CustomFormat = @"dddd d MMMM yyyy H' h 'm' min '";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday 8 February 2007 15 h 30 min "));
        dt.CustomFormat = @"d MMMM yyyy H' h 'mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 February 2007 15 h 30"));
        dt.CustomFormat = @"d MMMM yyyy H' h 'm' min '";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 February 2007 15 h 30 min "));
        dt.CustomFormat = @"dd-MMM-yy H.mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08-Feb-07 15.30"));
        dt.CustomFormat = @"dd-MMM-yy H' h 'mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08-Feb-07 15 h 30"));
        dt.CustomFormat = @"dd-MMM-yy H' h 'm' min '";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08-Feb-07 15 h 30 min "));
        dt.CustomFormat = @"dddd d MMMM yyyy H' h 'm' min 's' s '";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday 8 February 2007 15 h 30 min 45 s "));
        dt.CustomFormat = @"d MMMM yyyy H' h 'm' min 's' s '";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 February 2007 15 h 30 min 45 s "));
        dt.CustomFormat = @"dd-MMM-yy H' h 'm' min 's' s '";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08-Feb-07 15 h 30 min 45 s "));
        dt.CustomFormat = @"d/MM/yyyy H.mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/02/2007 15.30"));
        dt.CustomFormat = @"d/MM/yyyy H' h 'mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/02/2007 15 h 30"));
        dt.CustomFormat = @"d/MM/yyyy H' h 'm' min '";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/02/2007 15 h 30 min "));
        dt.CustomFormat = @"d/MM/yy H.mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/02/07 15.30"));
        dt.CustomFormat = @"d/MM/yy H' h 'mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/02/07 15 h 30"));
        dt.CustomFormat = @"d/MM/yy H' h 'm' min '";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/02/07 15 h 30 min "));
        dt.CustomFormat = @"dd.MM.yy H' h 'mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.02.07 15 h 30"));
        dt.CustomFormat = @"dd.MM.yy H' h 'm' min '";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.02.07 15 h 30 min "));
        dt.CustomFormat = @"yy/MM/dd H.mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07/02/08 15.30"));
        dt.CustomFormat = @"yy/MM/dd H' h 'mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07/02/08 15 h 30"));
        dt.CustomFormat = @"yy/MM/dd H' h 'm' min '";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07/02/08 15 h 30 min "));
        dt.CustomFormat = @"dd-MM-yy H' h 'mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08-02-07 15 h 30"));
        dt.CustomFormat = @"dd-MM-yy H' h 'm' min '";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08-02-07 15 h 30 min "));
        dt.CustomFormat = @"dd/MM/yyyy H' h 'mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02/2007 15 h 30"));
        dt.CustomFormat = @"dd/MM/yyyy H' h 'm' min '";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02/2007 15 h 30 min "));
        dt.CustomFormat = @"yyyy-MM-dd H' h 'mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007-02-08 15 h 30"));
        dt.CustomFormat = @"yyyy-MM-dd H' h 'm' min '";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007-02-08 15 h 30 min "));
        dt.CustomFormat = @"d/MM/yyyy H' h 'm' min 's' s '";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/02/2007 15 h 30 min 45 s "));
        dt.CustomFormat = @"d/MM/yy H' h 'm' min 's' s '";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/02/07 15 h 30 min 45 s "));
        dt.CustomFormat = @"dd.MM.yy H' h 'm' min 's' s '";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.02.07 15 h 30 min 45 s "));
        dt.CustomFormat = @"yy/MM/dd H' h 'm' min 's' s '";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07/02/08 15 h 30 min 45 s "));
        dt.CustomFormat = @"dd-MM-yy H' h 'm' min 's' s '";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08-02-07 15 h 30 min 45 s "));
        dt.CustomFormat = @"dd/MM/yyyy H' h 'm' min 's' s '";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02/2007 15 h 30 min 45 s "));
        dt.CustomFormat = @"yyyy-MM-dd H' h 'm' min 's' s '";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007-02-08 15 h 30 min 45 s "));
        dt.CustomFormat = @"H' h 'mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"15 h 30"));
        dt.CustomFormat = @"H' h 'm' min '";
        Assert.That((object?)dt.Text, Is.EqualTo(@"15 h 30 min "));
        dt.CustomFormat = @"H' h 'm' min 's' s '";
        Assert.That((object?)dt.Text, Is.EqualTo(@"15 h 30 min 45 s "));
        dt.CustomFormat = @"d-MMM-yy H.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8-Feb-07 15.30 h"));
        dt.CustomFormat = @"d MMMM yyyy H.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 February 2007 15.30 h"));
        dt.CustomFormat = @"dd. MM. yy H.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08. 02. 07 15.30 h"));
        dt.CustomFormat = @"d/M/yy H.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/2/07 15.30 h"));
        dt.CustomFormat = @"dd.M.yy H.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.2.07 15.30 h"));
        dt.CustomFormat = @"dd.M.yy H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.2.07 15:30:45"));
        dt.CustomFormat = @"dddd d MMMM yyyy H.mm' u.'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday 8 February 2007 15.30 u."));
        dt.CustomFormat = @"dd-MMM-yy H.mm' u.'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08-Feb-07 15.30 u."));
        dt.CustomFormat = @"d MMMM yyyy H.mm' u.'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 February 2007 15.30 u."));
        dt.CustomFormat = @"dd MMM yy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08 Feb 07 15:30"));
        dt.CustomFormat = @"dd MMM yy H.mm' u.'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08 Feb 07 15.30 u."));
        dt.CustomFormat = @"dd MMM yy H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08 Feb 07 15:30:45"));
        dt.CustomFormat = @"d/MM/yyyy H.mm' u.'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/02/2007 15.30 u."));
        dt.CustomFormat = @"d/MM/yy H.mm' u.'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/02/07 15.30 u."));
        dt.CustomFormat = @"dd-MM-yy H.mm' u.'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08-02-07 15.30 u."));
        dt.CustomFormat = @"dd.MM.yy H.mm' u.'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.02.07 15.30 u."));
        dt.CustomFormat = @"yyyy-MM-dd H.mm' u.'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007-02-08 15.30 u."));
        dt.CustomFormat = @"H.mm' u.'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"15.30 u."));
        dt.CustomFormat = @"d/MMM/yy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/Feb/07"));
        dt.CustomFormat = @"d.MMM.yy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.Feb.07"));
        dt.CustomFormat = @"dddd, d' de 'MMMM' de 'yyyy HH'H'mm'm'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 8 de February de 2007 15H30m"));
        dt.CustomFormat = @"d' de 'MMMM' de 'yyyy HH'H'mm'm'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 de February de 2007 15H30m"));
        dt.CustomFormat = @"d/MMM/yy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/Feb/07 15:30"));
        dt.CustomFormat = @"d/MMM/yy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/Feb/07 15:30"));
        dt.CustomFormat = @"d/MMM/yy HH'H'mm'm'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/Feb/07 15H30m"));
        dt.CustomFormat = @"d.MMM.yy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.Feb.07 15:30"));
        dt.CustomFormat = @"d.MMM.yy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.Feb.07 15:30"));
        dt.CustomFormat = @"d.MMM.yy HH'H'mm'm'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.Feb.07 15H30m"));
        dt.CustomFormat = @"d/MMM/yy H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/Feb/07 15:30:45"));
        dt.CustomFormat = @"d/MMM/yy HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/Feb/07 15:30:45"));
        dt.CustomFormat = @"d.MMM.yy H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.Feb.07 15:30:45"));
        dt.CustomFormat = @"d.MMM.yy HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.Feb.07 15:30:45"));
        dt.CustomFormat = @"dd-MM-yyyy HH'H'mm'm'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08-02-2007 15H30m"));
        dt.CustomFormat = @"yy.MM.dd HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07.02.08 15:30"));
        dt.CustomFormat = @"yy.MM.dd HH'H'mm'm'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07.02.08 15H30m"));
        dt.CustomFormat = @"d.M.yy HH'H'mm'm'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.2.07 15H30m"));
        dt.CustomFormat = @"dd/MM/yy HH'H'mm'm'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02/07 15H30m"));
        dt.CustomFormat = @"yyyy-MM-dd HH'H'mm'm'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007-02-08 15H30m"));
        dt.CustomFormat = @"yy.MM.dd HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07.02.08 15:30:45"));
        dt.CustomFormat = @"d/M";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/2"));
        dt.CustomFormat = @"HH'H'mm'm'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"15H30m"));
        dt.CustomFormat = @"d.M.yyyy 'kl 'H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.2.2007 kl 15:30"));
        dt.CustomFormat = @"dd.MM.yyyy 'kl 'H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.02.2007 kl 15:30"));
        dt.CustomFormat = @"d.M.yy 'kl 'H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.2.07 kl 15:30"));
        dt.CustomFormat = @"yyyy 'йил' d-MMMM";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007 йил 8-February"));
        dt.CustomFormat = @"yyyy 'йил' d-MMMM HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007 йил 8-February 15:30"));
        dt.CustomFormat = @"yyyy 'йил' d-MMMM H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007 йил 8-February 15:30"));
        dt.CustomFormat = @"yyyy 'йил' d-MMMM HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007 йил 8-February 15:30:45"));
        dt.CustomFormat = @"yyyy 'йил' d-MMMM H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007 йил 8-February 15:30:45"));
        dt.CustomFormat = @"dddd, d MMMM, yyyy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 8 February, 2007"));
        dt.CustomFormat = @"dddd yyyy MM dd";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday 2007 02 08"));
        dt.CustomFormat = @"dddd, d MMMM, yyyy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 8 February, 2007 15:30"));
        dt.CustomFormat = @"dddd, d MMMM, yyyy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 8 February, 2007 15:30"));
        dt.CustomFormat = @"dddd yyyy MM dd H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday 2007 02 08 15:30"));
        dt.CustomFormat = @"dddd yyyy MM dd HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday 2007 02 08 15:30"));
        dt.CustomFormat = @"dddd, d MMMM, yyyy H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 8 February, 2007 15:30:45"));
        dt.CustomFormat = @"dddd, d MMMM, yyyy HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 8 February, 2007 15:30:45"));
        dt.CustomFormat = @"dddd yyyy MM dd H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday 2007 02 08 15:30:45"));
        dt.CustomFormat = @"dddd yyyy MM dd HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday 2007 02 08 15:30:45"));
        dt.CustomFormat = @"dd.M.yyyy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.2.2007"));
        dt.CustomFormat = @"dddd, dd. MMMM yyyy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08. February 2007"));
        dt.CustomFormat = @"d.MMMM yyyy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.February 2007"));
        dt.CustomFormat = @"d.MMMyyyy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.Feb2007"));
        dt.CustomFormat = @"dddd, dd. MMMM yyyy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08. February 2007 15:30"));
        dt.CustomFormat = @"dddd, dd. MMMM yyyy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08. February 2007 15:30"));
        dt.CustomFormat = @"dddd, dd. MMMM yyyy HH:mm' Uhr'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08. February 2007 15:30 Uhr"));
        dt.CustomFormat = @"d.MMMM yyyy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.February 2007 15:30"));
        dt.CustomFormat = @"d.MMMM yyyy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.February 2007 15:30"));
        dt.CustomFormat = @"d.MMMM yyyy HH:mm' Uhr'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.February 2007 15:30 Uhr"));
        dt.CustomFormat = @"d.MMMyyyy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.Feb2007 15:30"));
        dt.CustomFormat = @"d.MMMyyyy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.Feb2007 15:30"));
        dt.CustomFormat = @"d.MMMyyyy HH:mm' Uhr'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.Feb2007 15:30 Uhr"));
        dt.CustomFormat = @"d MMM yyyy HH:mm' Uhr'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 Feb 2007 15:30 Uhr"));
        dt.CustomFormat = @"dddd, dd. MMMM yyyy HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08. February 2007 15:30:45"));
        dt.CustomFormat = @"dddd, dd. MMMM yyyy H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08. February 2007 15:30:45"));
        dt.CustomFormat = @"d.MMMM yyyy HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.February 2007 15:30:45"));
        dt.CustomFormat = @"d.MMMM yyyy H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.February 2007 15:30:45"));
        dt.CustomFormat = @"d.MMMyyyy HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.Feb2007 15:30:45"));
        dt.CustomFormat = @"d.MMMyyyy H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.Feb2007 15:30:45"));
        dt.CustomFormat = @"dd.MM.yyyy HH:mm' Uhr'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.02.2007 15:30 Uhr"));
        dt.CustomFormat = @"dd.MM.yy HH:mm' Uhr'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.02.07 15:30 Uhr"));
        dt.CustomFormat = @"dd.M.yyyy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.2.2007 15:30"));
        dt.CustomFormat = @"dd.M.yyyy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.2.2007 15:30"));
        dt.CustomFormat = @"dd.M.yyyy HH:mm' Uhr'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.2.2007 15:30 Uhr"));
        dt.CustomFormat = @"yyyy-MM-dd HH:mm' Uhr'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007-02-08 15:30 Uhr"));
        dt.CustomFormat = @"dd.M.yyyy HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.2.2007 15:30:45"));
        dt.CustomFormat = @"dd.M.yyyy H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08.2.2007 15:30:45"));
        dt.CustomFormat = @"HH:mm' Uhr'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"15:30 Uhr"));
        dt.CustomFormat = @"d/MM/yyyy h:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/02/2007 3:30 PM"));
        dt.CustomFormat = @"dd-MMMM-yyyy h:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08-February-2007 3:30 PM"));
        dt.CustomFormat = @"dd-MMMM-yyyy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08-February-2007 15:30"));
        dt.CustomFormat = @"d/MM/yyyy h:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/02/2007 3:30:45 PM"));
        dt.CustomFormat = @"dd-MMMM-yyyy h:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08-February-2007 3:30:45 PM"));
        dt.CustomFormat = @"dd-MMMM-yyyy H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08-February-2007 15:30:45"));
        dt.CustomFormat = @"d MMM yyyy H' h 'mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 Feb 2007 15 h 30"));
        dt.CustomFormat = @"yy-MM-dd H' h 'mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07-02-08 15 h 30"));
        dt.CustomFormat = @"yy MM dd H' h 'mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07 02 08 15 h 30"));
        dt.CustomFormat = @"dd/MM/yy H' h 'mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02/07 15 h 30"));
        dt.CustomFormat = @"dddd, d MMMM, yyyy tt h:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 8 February, 2007 PM 3:30"));
        dt.CustomFormat = @"dddd, d MMMM, yyyy tt hh:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 8 February, 2007 PM 03:30"));
        dt.CustomFormat = @"d MMMM, yyyy tt h:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 February, 2007 PM 3:30"));
        dt.CustomFormat = @"d MMMM, yyyy tt hh:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 February, 2007 PM 03:30"));
        dt.CustomFormat = @"dddd yyyy MM dd tt h:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday 2007 02 08 PM 3:30"));
        dt.CustomFormat = @"dddd yyyy MM dd tt hh:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday 2007 02 08 PM 03:30"));
        dt.CustomFormat = @"yyyy MM dd tt h:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007 02 08 PM 3:30"));
        dt.CustomFormat = @"yyyy MM dd tt hh:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007 02 08 PM 03:30"));
        dt.CustomFormat = @"dddd, d MMMM, yyyy tt h:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 8 February, 2007 PM 3:30:45"));
        dt.CustomFormat = @"dddd, d MMMM, yyyy tt hh:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 8 February, 2007 PM 03:30:45"));
        dt.CustomFormat = @"d MMMM, yyyy tt h:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 February, 2007 PM 3:30:45"));
        dt.CustomFormat = @"d MMMM, yyyy tt hh:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 February, 2007 PM 03:30:45"));
        dt.CustomFormat = @"dddd yyyy MM dd tt h:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday 2007 02 08 PM 3:30:45"));
        dt.CustomFormat = @"dddd yyyy MM dd tt hh:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday 2007 02 08 PM 03:30:45"));
        dt.CustomFormat = @"yyyy MM dd tt h:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007 02 08 PM 3:30:45"));
        dt.CustomFormat = @"yyyy MM dd tt hh:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007 02 08 PM 03:30:45"));
        dt.CustomFormat = @"d/M/yyyy tt h:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/2/2007 PM 3:30"));
        dt.CustomFormat = @"d/M/yyyy tt hh:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/2/2007 PM 03:30"));
        dt.CustomFormat = @"d/M/yy tt h:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/2/07 PM 3:30"));
        dt.CustomFormat = @"d/M/yy tt hh:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/2/07 PM 03:30"));
        dt.CustomFormat = @"dd/MM/yy tt h:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02/07 PM 3:30"));
        dt.CustomFormat = @"dd/MM/yy tt hh:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02/07 PM 03:30"));
        dt.CustomFormat = @"d/M/yyyy tt h:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/2/2007 PM 3:30:45"));
        dt.CustomFormat = @"d/M/yyyy tt hh:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/2/2007 PM 03:30:45"));
        dt.CustomFormat = @"d/M/yy tt h:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/2/07 PM 3:30:45"));
        dt.CustomFormat = @"d/M/yy tt hh:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/2/07 PM 03:30:45"));
        dt.CustomFormat = @"dd/MM/yy tt h:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02/07 PM 3:30:45"));
        dt.CustomFormat = @"dd/MM/yy tt hh:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08/02/07 PM 03:30:45"));
        dt.CustomFormat = @"M/dd/yy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2/08/07"));
        dt.CustomFormat = @"MMMM d, yyyy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February 8, 2007"));
        dt.CustomFormat = @"MMMM d, yyyy h:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February 8, 2007 3:30 PM"));
        dt.CustomFormat = @"MMMM d, yyyy hh:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February 8, 2007 03:30 PM"));
        dt.CustomFormat = @"MMMM d, yyyy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February 8, 2007 15:30"));
        dt.CustomFormat = @"MMMM d, yyyy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February 8, 2007 15:30"));
        dt.CustomFormat = @"d-MMM-yy h:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8-Feb-07 3:30 PM"));
        dt.CustomFormat = @"d-MMM-yy hh:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8-Feb-07 03:30 PM"));
        dt.CustomFormat = @"MMMM d, yyyy h:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February 8, 2007 3:30:45 PM"));
        dt.CustomFormat = @"MMMM d, yyyy hh:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February 8, 2007 03:30:45 PM"));
        dt.CustomFormat = @"MMMM d, yyyy HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February 8, 2007 15:30:45"));
        dt.CustomFormat = @"MMMM d, yyyy H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February 8, 2007 15:30:45"));
        dt.CustomFormat = @"d-MMM-yy h:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8-Feb-07 3:30:45 PM"));
        dt.CustomFormat = @"d-MMM-yy hh:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8-Feb-07 03:30:45 PM"));
        dt.CustomFormat = @"yy-MM-dd hh:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07-02-08 03:30 PM"));
        dt.CustomFormat = @"M/dd/yy h:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2/08/07 3:30 PM"));
        dt.CustomFormat = @"M/dd/yy hh:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2/08/07 03:30 PM"));
        dt.CustomFormat = @"M/dd/yy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2/08/07 15:30"));
        dt.CustomFormat = @"M/dd/yy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2/08/07 15:30"));
        dt.CustomFormat = @"yy-MM-dd hh:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07-02-08 03:30:45 PM"));
        dt.CustomFormat = @"M/dd/yy h:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2/08/07 3:30:45 PM"));
        dt.CustomFormat = @"M/dd/yy hh:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2/08/07 03:30:45 PM"));
        dt.CustomFormat = @"M/dd/yy HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2/08/07 15:30:45"));
        dt.CustomFormat = @"M/dd/yy H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2/08/07 15:30:45"));
        dt.CustomFormat = @"d/MM/yyyy hh:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/02/2007 03:30 PM"));
        dt.CustomFormat = @"d/MM/yyyy hh:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8/02/2007 03:30:45 PM"));
        dt.CustomFormat = @"d MMM yy HH.mm' h'";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8 Feb 07 15.30 h"));
        dt.CustomFormat = @"d.MM.yy h:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.02.07 3:30 PM"));
        dt.CustomFormat = @"d.MM.yy hh:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.02.07 03:30 PM"));
        dt.CustomFormat = @"d.MM.yy h:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.02.07 3:30:45 PM"));
        dt.CustomFormat = @"d.MM.yy hh:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"8.02.07 03:30:45 PM"));
        dt.CustomFormat = @"dddd, dd MMMM yyyy hh:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08 February 2007 03:30 PM"));
        dt.CustomFormat = @"dddd, dd MMMM yyyy hh:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Thursday, 08 February 2007 03:30:45 PM"));
        dt.CustomFormat = @"MM-dd-yyyy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"02-08-2007"));
        dt.CustomFormat = @"MM-dd-yyyy hh:mm tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"02-08-2007 03:30 PM"));
        dt.CustomFormat = @"MM-dd-yyyy HH:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"02-08-2007 15:30"));
        dt.CustomFormat = @"MM-dd-yyyy hh:mm:ss tt";
        Assert.That((object?)dt.Text, Is.EqualTo(@"02-08-2007 03:30:45 PM"));
        dt.CustomFormat = @"MM-dd-yyyy HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"02-08-2007 15:30:45"));
        dt.CustomFormat = @"MMMM d'. b. 'yyyy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February 8. b. 2007"));
        dt.CustomFormat = @"MMMM d'. b. 'yyyy HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February 8. b. 2007 15:30:45"));
        dt.CustomFormat = @"MMMM d'. b. 'yyyy H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February 8. b. 2007 15:30:45"));
        dt.CustomFormat = @"MMMM d'. b. 'yyyy HH.mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February 8. b. 2007 15.30"));
        dt.CustomFormat = @"MMMM d'. b. 'yyyy HH.mm.ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February 8. b. 2007 15.30.45"));
        dt.CustomFormat = @"MMMM d'. b. 'yyyy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February 8. b. 2007 15:30"));
        dt.CustomFormat = @"MMMM d'. p. 'yyyy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February 8. p. 2007"));
        dt.CustomFormat = @"MMMM d'. p. 'yyyy H:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February 8. p. 2007 15:30:45"));
        dt.CustomFormat = @"MMMM d'. p. 'yyyy HH:mm:ss";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February 8. p. 2007 15:30:45"));
        dt.CustomFormat = @"MMMM d'. p. 'yyyy H:mm";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February 8. p. 2007 15:30"));
        dt.CustomFormat = @"ddMMyyyy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"08022007"));
        dt.CustomFormat = @"yy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"07"));
        dt.CustomFormat = @"yyy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007"));
        dt.CustomFormat = @"yyyy";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2007"));
        dt.CustomFormat = @"MM";
        Assert.That((object?)dt.Text, Is.EqualTo(@"02"));
        dt.CustomFormat = @"MMM";
        Assert.That((object?)dt.Text, Is.EqualTo(@"Feb"));
        dt.CustomFormat = @"MMMM";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February"));
        dt.CustomFormat = @"MMMMM";
        Assert.That((object?)dt.Text, Is.EqualTo(@"February"));
        dt.CustomFormat = @"M-y-d-h-H-m-s-t";
        Assert.That((object?)dt.Text, Is.EqualTo(@"2-7-8-3-15-30-45-P"));
        dt.CustomFormat = @" yy-MM";
        Assert.That((object?)dt.Text, Is.EqualTo(@" 07-02"));
        dt.CustomFormat = @"-yy-MM";
        Assert.That((object?)dt.Text, Is.EqualTo(@"-07-02"));
    }

    [Test]
    public void CustomFormatTest()
    {
        CheckCulture();

        using var frm = new Form();
        var dt = new DateTimePicker();
        frm.Controls.Add(dt);
        frm.Show();

        dt.Format = DateTimePickerFormat.Custom;
        dt.Value = new DateTime(2007, 2, 8, 15, 30, 45, 60);

        /*
             * This is really weird and necessary, otherwise the tests won't succeed on windows.
             * other strings that can be used here: "a", "dddd", " ddd", "'a'", "'d'" + a probably a lot more.
             * seems like the first non-literal must be ddd and cannot be an empty string
             * Without this everytime a "y" or "yy" comes first in the format, it will always show as
             * a 4-digit string if this is not done.
             */

        dt.CustomFormat = Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
        Assert.That((object?)dt.Text, Is.EqualTo("2/8/2007"));

        dt.CustomFormat = "y-M-d-h-H-m-s-t";
        Assert.That((object?)dt.Text, Is.EqualTo("7-2-8-3-15-30-45-P"));

        dt.CustomFormat = "M/y";
        Assert.That((object?)dt.Text, Is.EqualTo("2/7"));

        dt.CustomFormat = "HHHHHHHH";
        Assert.That((object?)dt.Text, Is.EqualTo("15"));

        dt.CustomFormat = "yy-MM";
        Assert.That((object?)dt.Text, Is.EqualTo("07-02"));

        dt.CustomFormat = "MM-yy";
        Assert.That((object?)dt.Text, Is.EqualTo("02-07"));

        dt.CustomFormat = "M-y-d-h-H-m-s-t";
        Assert.That((object?)dt.Text, Is.EqualTo("2-7-8-3-15-30-45-P"));

    }

    [Test]
    public void CustomFormatNullTest()
    {
        var dt = new DateTimePicker();
        dt.CustomFormat = null!;
    }

    [Test]
    public void DefaultPropertiesTest()
    {
        var dt = new DateTimePicker();

        Assert.That((object?)dt.BackColor.Name, Is.EqualTo("Transparent"));
        Assert.That((object?)dt.BackgroundImage, Is.SameAs(null));
        Assert.That((object?)dt.BackgroundImageLayout, Is.EqualTo(ImageLayout.Tile));

        //Assert1.AreSame(null, dt.CalendarFont);
        Assert.That((object?)dt.CalendarForeColor, Is.EqualTo(GtkSystemColors.ControlText));
        Assert.That((object?)dt.CalendarMonthBackground, Is.EqualTo(GtkSystemColors.Window));
        Assert.That((object?)dt.CalendarTitleBackColor, Is.EqualTo(GtkSystemColors.ActiveCaption));
        Assert.That((object?)dt.CalendarTitleForeColor, Is.EqualTo(GtkSystemColors.ActiveCaptionText));
        Assert.That((object?)dt.CalendarTrailingForeColor, Is.EqualTo(GtkSystemColors.GrayText));

        Assert.That((object?)dt.ForeColor, Is.EqualTo(GtkSystemColors.WindowText));
        Assert.That((object?)dt.Format, Is.EqualTo(DateTimePickerFormat.Long));

        object expected = new Padding(0, 0, 0, 0);
        Assert.That((object?)dt.Padding, Is.EqualTo(expected));
        // PreferredHeight is Font dependent.

        Assert.That((object?)dt.Text, Is.EqualTo(string.Empty));

        Assert.That((object?)dt.Value.Date, Is.EqualTo(DateTime.Today));
    }

    [Test]
    public void MaxDate()
    {
        var dt = new DateTimePicker();
        object expected = new DateTime(9998, 12, 31);
        Assert.That((object?)dt.MaxDate, Is.EqualTo(expected));
        dt.Value = new DateTime(2007, 8, 13);
        object expected1 = new DateTime(9998, 12, 31);
        Assert.That((object?)dt.MaxDate, Is.EqualTo(expected1));
        object expected2 = new DateTime(2007, 8, 13);
        Assert.That((object?)dt.Value, Is.EqualTo(expected2));
        dt.MaxDate = new DateTime(2010, 2, 10);
        object expected3 = new DateTime(2010, 2, 10);
        Assert.That((object?)dt.MaxDate, Is.EqualTo(expected3));
        object expected4 = new DateTime(2007, 8, 13);
        Assert.That((object?)dt.Value, Is.EqualTo(expected4));
        dt.MaxDate = new DateTime(2005, 10, 15);
        object expected5 = new DateTime(2005, 10, 15);
        Assert.That((object?)dt.MaxDate, Is.EqualTo(expected5));
        object expected6 = new DateTime(2005, 10, 15);
        Assert.That((object?)dt.Value, Is.EqualTo(expected6));
        dt.MaxDate = new DateTime(2008, 1, 4);
        object expected7 = new DateTime(2008, 1, 4);
        Assert.That((object?)dt.MaxDate, Is.EqualTo(expected7));
        object expected8 = new DateTime(2005, 10, 15);
        Assert.That((object?)dt.Value, Is.EqualTo(expected8));
        dt.MaxDate = dt.MinDate;
        object expected9 = new DateTime(1753, 1, 1);
        Assert.That((object?)dt.MaxDate, Is.EqualTo(expected9));
        object expected10 = new DateTime(1753, 1, 1);
        Assert.That((object?)dt.Value, Is.EqualTo(expected10));
        object expected11 = new DateTime(1753, 1, 1);
        Assert.That((object?)dt.Value, Is.EqualTo(expected11));
    }

    [Test]
    public void MaxDate_Invalid()
    {
        var dt = new DateTimePicker();

        // not less or equal to MaxDateTime
        try
        {
            dt.MaxDate = new DateTime(9999, 1, 1);
            Assert.Fail("#A1");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // DateTimePicker does not support dates after 12/31/9998 12:00:00 AM
            object expected = typeof(ArgumentOutOfRangeException);
            Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
            Assert.IsNull(ex.InnerException);
            Assert.IsNotNull(ex.Message);
            Assert.IsNotNull(ex.ParamName);
            Assert.That((object?)ex.ParamName, Is.EqualTo("MaxDate"));
        }

        dt.MinDate = new DateTime(2007, 8, 13);

        // not less than MinDate
        try
        {
            dt.MaxDate = new DateTime(2007, 8, 12);
            Assert.Fail("#B1");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // '8/12/2007 12:00:00 AM' is not a valid value for 'MaxDate'.
            // 'MaxDate' must be greater than or equal to MinDate.
            object expected = typeof(ArgumentOutOfRangeException);
            Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
            Assert.IsNull(ex.InnerException);
            Assert.IsNotNull(ex.Message);
            Assert.IsNotNull(ex.ParamName);
            Assert.That((object?)ex.ParamName, Is.EqualTo("MaxDate"));
        }
    }

    [Test]
    public void MinDate()
    {
        var dt = new DateTimePicker();
        object expected = new DateTime(1753, 1, 1);
        Assert.That((object?)dt.MinDate, Is.EqualTo(expected));
        dt.Value = new DateTime(2007, 8, 13);
        object expected1 = new DateTime(1753, 1, 1);
        Assert.That((object?)dt.MinDate, Is.EqualTo(expected1));
        object expected2 = new DateTime(2007, 8, 13);
        Assert.That((object?)dt.Value, Is.EqualTo(expected2));
        dt.MinDate = new DateTime(2005, 1, 15);
        object expected3 = new DateTime(2005, 1, 15);
        Assert.That((object?)dt.MinDate, Is.EqualTo(expected3));
        object expected4 = new DateTime(2007, 8, 13);
        Assert.That((object?)dt.Value, Is.EqualTo(expected4));
        dt.MinDate = new DateTime(2008, 2, 5);
        object expected5 = new DateTime(2008, 2, 5);
        Assert.That((object?)dt.MinDate, Is.EqualTo(expected5));
        object expected6 = new DateTime(2008, 2, 5);
        Assert.That((object?)dt.Value, Is.EqualTo(expected6));
        dt.MinDate = new DateTime(2004, 8, 20);
        object expected7 = new DateTime(2004, 8, 20);
        Assert.That((object?)dt.MinDate, Is.EqualTo(expected7));
        object expected8 = new DateTime(2008, 2, 5);
        Assert.That((object?)dt.Value, Is.EqualTo(expected8));
        object expected9 = new DateTime(2008, 2, 5);
        Assert.That((object?)dt.Value, Is.EqualTo(expected9));
    }

    [Test]
    public void MinDate_Invalid()
    {
        var dt = new DateTimePicker();

        // less than MinDateTime
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            try
            {
                dt.MinDate = new DateTime(1752, 12, 31);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // DateTimePicker does not support dates before 1/1/1753 12:00:00 AM
                object expected = typeof(ArgumentOutOfRangeException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                Assert.IsNotNull(ex.ParamName);
                Assert.That((object?)ex.ParamName, Is.EqualTo("MinDate"));
                throw;
            }
        });

        dt.MaxDate = new DateTime(2007, 8, 13);

        // equal to MaxDate
        dt.MinDate = new DateTime(2007, 8, 13);
        object expected1 = new DateTime(2007, 8, 13);
        Assert.That((object?)dt.MinDate, Is.EqualTo(expected1));

        // not less than MaxDate
        try
        {
            dt.MinDate = new DateTime(2007, 8, 14);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // '8/14/2007 12:00:00 AM' is not a valid value for 'MinDate'.
            // 'MinDate' must be less than MaxDate
            object expected = typeof(ArgumentOutOfRangeException);
            Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
            Assert.IsNull(ex.InnerException);
            Assert.IsNotNull(ex.Message);
            Assert.IsNotNull(ex.ParamName);
            Assert.That((object?)ex.ParamName, Is.EqualTo("MinDate"));
            throw;
        }
    }

    [Test]
    public void InvalidTextTest()
    {
        Assert.Throws<FormatException>(() =>
        {
            var dt = new DateTimePicker();
            dt.Text = "abcdef";
        });
    }

    [Test]
    public void ValueTooBig()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var dt = new DateTimePicker();
            dt.MaxDate = DateTime.Now;

            dt.Value = DateTime.Now.AddDays(3);
        });
    }

    [Test]
    public void ValueTooSmall()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            var dt = new DateTimePicker();
            dt.MinDate = DateTime.Now;

            dt.Value = DateTime.Now.AddDays(-3);
        });
    }
}