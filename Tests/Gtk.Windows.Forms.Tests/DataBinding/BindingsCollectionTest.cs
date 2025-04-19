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
// Copyright (c) 2008 Novell, Inc.
//
// Authors:
// 	Carlos Alberto Cortez <calberto.cortez@gmail.com>
//

using GtkTests.Helpers;
using GtkTests.System.Windows.Forms;
using System.ComponentModel;
using System.Windows.Forms;

namespace GtkTests.DataBinding;

[TestFixture]
public class BindingsCollectionTest : TestHelper
{
    // 
    // CollectionChanging event test section
    //
    private bool collection_changing_called;
    private int collection_expected_count;
    private string collection_expected_assert;
    private CollectionChangeAction collection_action_expected;
    private object collection_element_expected;

    private void CollectionChangingHandler(object? o, CollectionChangeEventArgs args)
    {
        var coll = (BindingsCollection)o!;

        collection_changing_called = true;
        var message = collection_expected_assert + "-0";
        Assert.That((object?)coll.Count, Is.EqualTo(collection_expected_count));
        var message1 = collection_expected_assert + "-1";
        Assert.That((object?)args.Action, Is.EqualTo(collection_action_expected), message1);
        var message2 = collection_expected_assert + "-2";
        Assert.That(args.Element, Is.EqualTo(collection_element_expected), message2);
    }

    [Test]
    public void CollectionChangingTest()
    {
        var c = new Control();
        c.BindingContext = new BindingContext();
        c.CreateControl();

        var binding_coll = c.DataBindings;

        var binding = new Binding("Text", new MockItem("A", 0), "Text");
        var binding2 = new Binding("Name", new MockItem("B", 0), "Text");
        binding_coll!.Add(binding);

        binding_coll.CollectionChanging += CollectionChangingHandler;

        collection_expected_count = 1;
        collection_action_expected = CollectionChangeAction.Add;
        collection_element_expected = binding2;
        collection_expected_assert = "#A0";
        binding_coll.Add(binding2);
        Assert.IsTrue(collection_changing_called);

        collection_changing_called = false;
        collection_expected_count = 2;
        collection_action_expected = CollectionChangeAction.Remove;
        collection_element_expected = binding;
        collection_expected_assert = "#B0";
        binding_coll.Remove(binding);
        Assert.IsTrue(collection_changing_called);

        collection_changing_called = false;
        collection_expected_count = 1;
        collection_element_expected = null!;
        collection_action_expected = CollectionChangeAction.Refresh;
        collection_expected_assert = "#C0";
        binding_coll.Clear();
        Assert.IsTrue(collection_changing_called);
    }
}