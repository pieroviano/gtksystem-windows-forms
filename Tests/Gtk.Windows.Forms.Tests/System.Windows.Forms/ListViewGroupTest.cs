//
//  ListViewGroupTest.cs
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

using GtkTests.Helpers;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class ListViewGroupTest : TestHelper
{
    private ListView? lv;

    [TearDown]
    protected override void TearDown()
    {
        lv?.Dispose();
    }

    [SetUp]
    protected override void SetUp () {
        lv = new ListView ();
        base.SetUp ();
    }

    [Test]
    public void DefaultProperties ()
    {
        //default ListView properties for groups
        Assert.That((object?)lv!.ShowGroups, Is.EqualTo(true));
        Assert.That((object?)(lv.Groups != null), Is.EqualTo(true));
        Assert.That((object?)lv.Groups!.Count, Is.EqualTo(0));

        //default ListViewGroup properties
        var lg1 = new ListViewGroup ();
        Assert.That((object?)lg1.Header, Is.EqualTo("ListViewGroup"));
        Assert.That((object?)lg1.Name, Is.EqualTo(null));
        Assert.That((object?)lg1.HeaderAlignment, Is.EqualTo(HorizontalAlignment.Left));
        Assert.That((object?)lg1.Items.Count, Is.EqualTo(0));
        Assert.That((object?)lg1.ListView, Is.EqualTo(null));
        Assert.That(lg1.Tag, Is.EqualTo(null));
        Assert.That((object?)lg1.ToString(), Is.EqualTo(lg1.Header));
    }

    [Test]
    public void AddTest ()
    {
        var lg1 = new ListViewGroup ();
        lg1.Items.Add ("Item1");
        Assert.That((object?)lg1.Items.Count, Is.EqualTo(1));
        Assert.That((object?)lg1.Items[0].ListView, Is.EqualTo(null));

        lv!.Groups.Add (lg1);
        Assert.That((object?)lg1.Items[0].ListView, Is.EqualTo(null));
        Assert.That((object?)lv.Items.Contains(lg1.Items[0]), Is.EqualTo(false));

        var lvi = lg1.Items.Add ("Item1");
        Assert.That((object?)lvi.ListView, Is.EqualTo(null));
        Assert.That((object?)lvi.Group, Is.EqualTo(lg1));
    }

    [Test]
    public void RemoveTest ()
    {
        var lg1 = new ListViewGroup ();
        lg1.Items.Add ("Item1");
        lv!.Groups.Add (lg1);
        lv.Groups.Remove (lg1);

        Assert.That((object?)lg1.Items.Count, Is.EqualTo(1));
        Assert.That((object?)lv.Items.Count, Is.EqualTo(0));
        Assert.That((object?)lv.Items.Contains (lg1.Items [0]), Is.EqualTo(false));

        lg1.Items.Clear ();
        lv.Groups.Add (lg1);
        var lvi = lv.Items.Add ("Item1");
        lg1.Items.Add (lvi);

        Assert.That((object?)lg1.Items.Count, Is.EqualTo(1));
        Assert.That((object?)lv.Items.Count, Is.EqualTo(1));
        Assert.That((object?)lvi.ListView, Is.EqualTo(lv));
        Assert.That((object?)lvi.Group, Is.EqualTo(lg1));
				
        lg1.Items.Remove (lvi);
				
        Assert.That((object?)lg1.Items.Count, Is.EqualTo(0));
        Assert.That((object?)lv.Items.Count, Is.EqualTo(1));
        Assert.That((object?)lvi.ListView, Is.EqualTo(lv));
        Assert.That((object?)lvi.Group, Is.EqualTo(null));
    }
}