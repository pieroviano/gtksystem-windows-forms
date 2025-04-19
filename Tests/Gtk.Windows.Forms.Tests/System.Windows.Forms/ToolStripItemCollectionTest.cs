//
// ToolStripItemCollectionTest.cs
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
// Copyright (c) 2007 Gert Driesen
//
// Authors:
//	Gert Driesen (drieseng@users.sourceforge.net)
//

using GtkTests.Helpers;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class ToolStripItemCollectionTests : TestHelper
{
    private List<ToolStripItem> itemsAdded;
    private List<ToolStripItem> itemsRemoved;

    [SetUp]
    protected override void SetUp () {
        itemsAdded = [];
        itemsRemoved = [];
        base.SetUp ();
    }

    [Test]
    public void Clear ()
    {
        var ts = new ToolStrip ();
        var coll = ts.Items;
        ToolStripItem item1 = new ToolStripLabel ("a");
        ToolStripItem item2 = new ToolStripLabel ("b");
        ToolStripItem item3 = new ToolStripLabel ("c");

        coll.Add (item1);
        coll.Add (item2);
        coll.Add (item3);

        Assert.That((object?)coll.Count, Is.EqualTo(3));
        Assert.That((object?)item1.Owner, Is.EqualTo(ts));
        Assert.That((object?)item2.Owner, Is.EqualTo(ts));
        Assert.That((object?)item3.Owner, Is.EqualTo(ts));

        coll.Clear ();
        Assert.That((object?)coll.Count, Is.EqualTo(0));
        Assert.That((object?)item1.Owner, Is.EqualTo(null));
        Assert.That((object?)item2.Owner, Is.EqualTo(null));
        Assert.That((object?)item3.Owner, Is.EqualTo(null));
    }

    private class MockToolStripButton : ToolStripButton
    {
        public MockToolStripButton (string text) : base (text)
        {
        }

        public ToolStripItem ParentToolStrip {
            get => base.Parent!;
            set => base.Parent = value;
        }
    }
}