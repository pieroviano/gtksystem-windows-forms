//
//  ListViewGroupCollectionTest.cs
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
//      Carlos Alberto Cortez <calberto.cortez@gmail.com>

using System.Windows.Forms;
using System.Collections;
using GtkTests.Helpers;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class ListViewGroupCollectionTest : TestHelper
{
    private ListViewGroupCollection? grpCol;
    private ListView? lv;

    [TearDown]
    protected override void TearDown()
    {
        lv?.Dispose();
    }

    [SetUp]
    protected override void SetUp()
    {
        lv = new ListView();
        grpCol = lv.Groups;
        base.SetUp();
    }

    [Test]
    public void DefaultProperties()
    {
        Assert.That((object?)((IList)grpCol!).IsReadOnly, Is.EqualTo(false));
        Assert.That((object?)((IList)grpCol).IsFixedSize, Is.EqualTo(false));
        Assert.That((object?)((ICollection)grpCol).IsSynchronized, Is.EqualTo(true));
        Assert.That(((ICollection)grpCol).SyncRoot, Is.EqualTo(grpCol));
        Assert.That((object?)grpCol.Count, Is.EqualTo(0));
    }

    [Test]
    public void AddTest()
    {
        var group1 = new ListViewGroup("Item1");
        var group2 = new ListViewGroup("Item2");
        grpCol!.Add(group1);
        grpCol.Add(group2);

        Assert.That((object?)grpCol.Count, Is.EqualTo(2));
        Assert.That((object?)group1.ListView, Is.EqualTo(lv));
        Assert.That((object?)group2.ListView, Is.EqualTo(lv));
    }

    [Test]
    public void ClearTest()
    {
        var group1 = new ListViewGroup("Item1");
        var group2 = new ListViewGroup("Item2");
        grpCol!.Add(group1);
        grpCol.Add(group2);
        grpCol.Clear();

        Assert.That((object?)grpCol.Count, Is.EqualTo(0));
        Assert.That((object?)group1.ListView, Is.EqualTo(null));
        Assert.That((object?)group2.ListView, Is.EqualTo(null));
    }

    [Test]
    public void ContainsTest()
    {
        var obj = new ListViewGroup("Item1");
        var obj2 = new ListViewGroup("Item2");
        grpCol!.Add(obj);
        Assert.That((object?)grpCol.Contains(obj), Is.EqualTo(true));
        Assert.That((object?)grpCol.Contains(obj2), Is.EqualTo(false));
    }

    [Test]
    public void IndexOfTest()
    {
        var obj = new ListViewGroup("Item1");
        var obj2 = new ListViewGroup("Item2");
        grpCol!.Add(obj);
        grpCol.Add(obj2);
        Assert.That((object?)grpCol.IndexOf(obj2), Is.EqualTo(1));
    }

    [Test]
    public void RemoveTest()
    {
        var obj = new ListViewGroup("Item1");
        var obj2 = new ListViewGroup("Item2");
        grpCol!.Add(obj);
        grpCol.Add(obj2);
        grpCol.Remove(obj);
        Assert.That((object?)grpCol.Count, Is.EqualTo(1));
        Assert.That((object?)obj.ListView, Is.EqualTo(null));
        Assert.That((object?)obj2.ListView, Is.EqualTo(lv));
    }

    [Test]
    public void RemoveAtTest()
    {
        var obj = new ListViewGroup("Item1");
        var obj2 = new ListViewGroup("Item2");
        grpCol!.Add(obj);
        grpCol.Add(obj2);
        grpCol.RemoveAt(0);
        Assert.That((object?)grpCol.Count, Is.EqualTo(1));
        Assert.That((object?)grpCol.Contains(obj2), Is.EqualTo(true));
        Assert.That((object?)obj.ListView, Is.EqualTo(null));
        Assert.That((object?)obj2.ListView, Is.EqualTo(lv));
    }

    [Test]
    public void IndexerTest()
    {
        var group1 = new ListViewGroup("Item1");

        grpCol!.Add(group1);
        Assert.That((object?)grpCol[0], Is.EqualTo(group1));
        Assert.That((object?)group1.ListView, Is.EqualTo(lv));
        Assert.That((object?)grpCol.Count, Is.EqualTo(1));

        grpCol[0] = null!;
        Assert.That((object?)grpCol[0], Is.EqualTo(null));
        Assert.That((object?)grpCol.Count, Is.EqualTo(1));

        var group2 = new ListViewGroup("Item2");
        grpCol[0] = group2;
        Assert.That((object?)grpCol[0], Is.EqualTo(group2));
        Assert.That((object?)group2.ListView, Is.EqualTo(null));
        Assert.That((object?)grpCol.Count, Is.EqualTo(1));
    }

    [Test]
    public void IndexerNullTest()
    {
        var group1 = new ListViewGroup("Item1");
        grpCol!.Add(group1);
        grpCol[0] = null!;

        Assert.That((object?)grpCol[0], Is.EqualTo(null));
        Assert.That((object?)grpCol.Count, Is.EqualTo(1));
    }

    /* There's an inconsistency between other collections using
     * Key methods and the impl of this collection */
    [Test]
    public void IndexerKeyTest()
    {
        var group1 = new ListViewGroup("Item1");
        var group2 = new ListViewGroup("Item2");
        var group3 = new ListViewGroup("Item3");
        grpCol!.Add(group1);
        grpCol.Add(group2);
        grpCol.Add(group3);

        group1.Name = string.Empty;
        group2.Name = "A";
        group3.Name = "A";

        Assert.That((object?)grpCol[string.Empty], Is.EqualTo(group1)); /* Inconsistent */
        Assert.That((object?)grpCol[null!], Is.EqualTo(null));
        Assert.That((object?)grpCol["A"], Is.EqualTo(group2));
        Assert.That((object?)grpCol["a"], Is.EqualTo(null)); /* Inconsistent, again */

        var group4 = new ListViewGroup("Item4")
        {
            Name = "A"
        };

        grpCol[string.Empty] = group4;
        Assert.That((object?)grpCol[0], Is.EqualTo(group4)); /* First position */
        Assert.That((object?)grpCol["A"], Is.EqualTo(group4));
        Assert.That((object?)group4.ListView, Is.EqualTo(null));

        grpCol["A"] = null!;
        Assert.That((object?)grpCol[0], Is.EqualTo(null));
    }

    [Test]
    public void IndexerOutOfRangeTest()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            grpCol!.Add(new ListViewGroup("Item1"));
            grpCol[10] = null!;
        });
    }

    [Test]
    public void IndexerOutOfRangeTest2()
    {   //.NET 2.0 don't throw a exception here
        grpCol!.Add(new ListViewGroup("Item1"));
        grpCol["TestItemThatDoesNotExist"] = null!;
        Assert.IsNotNull(grpCol[0]);
    }
}