//
// PropertyGridTest.cs: Test cases for PropertyGrid.
//
// Author:
//   Gert Driesen (drieseng@users.sourceforge.net)
//
// (C) 2006 Novell, Inc. (http://www.novell.com)
//

using GtkTests.Helpers;
using System.Windows.Forms;
using CategoryAttribute = NUnit.Framework.CategoryAttribute;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class PropertyGridTest : TestHelper
{
    [Test]
    public void SelectedObject()
    {
        var pg = new PropertyGrid();
        var button1 = new Button();
        Assert.IsNull(pg.SelectedObject);
        Assert.IsNotNull(pg.SelectedObjects);
        Assert.That((object?)pg.SelectedObjects.Length, Is.EqualTo(0));
        pg.SelectedObject = button1;
        Assert.IsNotNull(pg.SelectedObject);
        Assert.That(pg.SelectedObject, Is.SameAs(button1));
        Assert.IsNotNull(pg.SelectedObjects);
        Assert.That((object?)pg.SelectedObjects.Length, Is.EqualTo(1));
        Assert.That(pg.SelectedObjects[0], Is.SameAs(button1));
        Assert.IsNotNull(pg.SelectedGridItem);
    }

    [Test] // bug #81796
    public void SelectedObject_NoProperties()
    {
        var propertyGrid = new PropertyGrid();
        propertyGrid.SelectedObject = new Button();
        propertyGrid.SelectedObject = new object();
        propertyGrid.SelectedObject = new Button();
    }

    [Test]
    public void SelectedObject_Null()
    {
        var pg = new PropertyGrid();
        Assert.IsNull(pg.SelectedObject);
        Assert.IsNotNull(pg.SelectedObjects);
        Assert.That((object?)pg.SelectedObjects.Length, Is.EqualTo(0));
        pg.SelectedObject = null;
        Assert.IsNull(pg.SelectedObject);
        Assert.IsNotNull(pg.SelectedObjects);
        Assert.That((object?)pg.SelectedObjects.Length, Is.EqualTo(0));
    }

    [Test]
    public void SelectedGridItem_Null()
    {
        var pg = new PropertyGrid();
        pg.SelectedObject = new TextBox();
        Assert.IsNotNull(pg.SelectedGridItem);
        Assert.Throws<ArgumentException>(() =>
        {
            try
            {
                pg.SelectedGridItem = null;
            }
            catch (ArgumentException ex)
            {
                // GridItem specified to PropertyGrid.SelectedGridItem must be
                // a valid GridItem
                object expected = typeof(ArgumentException);
                Assert.That((object?)ex.GetType(), Is.EqualTo(expected));
                Assert.IsNull(ex.InnerException);
                Assert.IsNotNull(ex.Message);
                Assert.IsNull(ex.ParamName);
                throw;
            }
        });
    }

    [Test] // bug #79615
    public void SelectedObjects_Multiple()
    {
        var button1 = new Button();
        var button2 = new Button();

        var pg = new PropertyGrid();
        pg.SelectedObjects = [button1, button2];
        Assert.IsNotNull(pg.SelectedObjects);
        Assert.That((object?)pg.SelectedObjects.Length, Is.EqualTo(2));
        Assert.That(pg.SelectedObjects[0], Is.SameAs(button1));
        Assert.That(pg.SelectedObjects[1], Is.SameAs(button2));
        Assert.IsNotNull(pg.SelectedObject);
        Assert.That(pg.SelectedObject, Is.SameAs(button1));
    }

    [Test]
    public void SelectedObjects_Null()
    {
        var pg = new PropertyGrid();
        var button1 = new Button();
        pg.SelectedObjects = [button1];
        Assert.IsNotNull(pg.SelectedObjects);
        Assert.That((object?)pg.SelectedObjects.Length, Is.EqualTo(1));
        Assert.That(pg.SelectedObjects[0], Is.SameAs(button1));
        Assert.That(pg.SelectedObject, Is.SameAs(button1));
        pg.SelectedObjects = null!;
        Assert.IsNotNull(pg.SelectedObjects);
        Assert.That((object?)pg.SelectedObjects.Length, Is.EqualTo(0));
        Assert.IsNull(pg.SelectedObject);
    }

    [Test]
    public void SelectedObjects_Null_Item()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            var pg = new PropertyGrid();
            var button1 = new Button();
            pg.SelectedObjects = [button1, null!];
        });
    }

    [Test]
    [Category("NotWorking")]
    public void PropertyGrid_MergedTest()
    {
        var pg = new PropertyGrid();
        pg.SelectedObjects = [new Button(), new Label()];

        Assert.IsNotNull(pg.SelectedGridItem, "1");
        Assert.That((object?)pg.SelectedGridItem.Label, Is.EqualTo("Accessibility"), "2");
        Assert.That((object?)pg.SelectedGridItem.GridItemType, Is.EqualTo(GridItemType.Category), "3");
    }

    [Test]
    [Category("NotWorking")]
    public void PropertyGrid_MergedRootTest()
    {
        object[] selected_objects = [new Button(), new Label()];
        var pg = new PropertyGrid();

        pg.SelectedObjects = selected_objects;

        Assert.IsNotNull(pg.SelectedGridItem.Parent, "1");
        Assert.That((object?)pg.SelectedGridItem.Parent.Label, Is.EqualTo("System.Object[]"), "2");
        Assert.That((object?)pg.SelectedGridItem.Parent.GridItemType, Is.EqualTo(GridItemType.Root), "3");

        Assert.That(pg.SelectedGridItem.Parent.Value, Is.EqualTo(selected_objects), "4");

        Assert.IsNull(pg.SelectedGridItem.Parent.Parent, "5");
    }

    private class ArrayTest_object
    {
        private readonly int[] array;
        public ArrayTest_object()
        {
            array = new int[10];
            for (var i = 0; i < array.Length; i++)
                array[i] = array.Length - i;
        }
        public int[] Array => array;
    }

    [Test]
    public void PropertyGrid_ArrayTest()
    {
        var pg = new PropertyGrid();

        pg.SelectedObject = new ArrayTest_object();

        // selected object
        Assert.That((object?)pg.SelectedGridItem.Label, Is.EqualTo("Array"), "1");
        Assert.IsTrue(pg.SelectedGridItem.Value is Array, "2");
        Assert.That((object?)pg.SelectedGridItem.GridItems.Count, Is.EqualTo(10), "3");
        Assert.That((object?)pg.SelectedGridItem.GridItemType, Is.EqualTo(GridItemType.Property), "4");
    }

    [Test]
    public void PropertyGrid_ArrayParentTest()
    {
        var pg = new PropertyGrid();

        pg.SelectedObject = new ArrayTest_object();

        // parent
        Assert.IsNotNull(pg.SelectedGridItem.Parent, "1");
        Assert.That((object?)pg.SelectedGridItem.Parent.Label, Is.EqualTo("Misc"), "2");
        Assert.That((object?)pg.SelectedGridItem.Parent.GridItemType, Is.EqualTo(GridItemType.Category), "3");
        Assert.That((object?)pg.SelectedGridItem.Parent.GridItems.Count, Is.EqualTo(1), "4");
    }

    [Test]
    public void PropertyGrid_ArrayRootTest()
    {
        var obj = new ArrayTest_object();
        var pg = new PropertyGrid();

        pg.SelectedObject = obj;

        // grandparent
        Assert.IsNotNull(pg.SelectedGridItem.Parent?.Parent, "1");
        object expected = typeof(ArrayTest_object).ToString();
        Assert.That((object?)pg.SelectedGridItem.Parent.Parent.Label, Is.EqualTo(expected), "2");
        Assert.That((object?)pg.SelectedGridItem.Parent.Parent.GridItemType, Is.EqualTo(GridItemType.Root), "3");
        Assert.That((object?)pg.SelectedGridItem.Parent.Parent.GridItems.Count, Is.EqualTo(1), "4");
        Assert.That(pg.SelectedGridItem.Parent.Parent.Value, Is.EqualTo(obj), "5");

        Assert.IsNull(pg.SelectedGridItem.Parent.Parent.Parent, "6");
    }

    [Test]
    public void PropertyGrid_ArrayChildrenTest()
    {
        var pg = new PropertyGrid();

        pg.SelectedObject = new ArrayTest_object();

        // children
        Assert.That((object?)pg.SelectedGridItem.GridItems[0].Label, Is.EqualTo("[0]"), "1");
        Assert.That((object?)pg.SelectedGridItem.GridItems[0].GridItemType, Is.EqualTo(GridItemType.Property), "2");
        Assert.That(pg.SelectedGridItem.GridItems[0].Value, Is.EqualTo(10), "3");
        Assert.That((object?)pg.SelectedGridItem.GridItems[0].GridItems.Count, Is.EqualTo(0), "4");
    }

    [Test]
    public void PropertyGrid_ItemSelectTest()
    {
        var pg = new PropertyGrid();

        pg.SelectedObject = new ArrayTest_object();

        // the selected grid item is the "Array" property item.
        var array_item = pg.SelectedGridItem;
        var misc_item = array_item.Parent;
        var root_item = misc_item!.Parent;

        Assert.That((object?)pg.SelectedGridItem, Is.EqualTo(array_item), "1");

        Assert.IsTrue(misc_item.Select(), "2");
        Assert.That((object?)pg.SelectedGridItem, Is.EqualTo(misc_item), "3");

        Assert.IsTrue(array_item.Select(), "4");
        Assert.That((object?)pg.SelectedGridItem, Is.EqualTo(array_item), "5");

        Assert.IsFalse(root_item!.Select(), "6");
        Assert.That((object?)pg.SelectedGridItem, Is.EqualTo(array_item), "7");
    }
}