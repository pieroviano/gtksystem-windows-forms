//
// PropertyGrid_GridEntryTest.cs: Test cases for GridEntry placed in PropertyGrid.
//
// Author:
//   Nikita Voronchev (nikita.voronchev@ru.axxonsoft.com)
//
// (C) 2018 AxxonSoft (http://www.axxonsoft.com)
//

using GtkTests.Helpers;
using System.ComponentModel;
using System.Windows.Forms;

namespace GtkTests.System.Windows.Forms;

[TestFixture]
public class PropertyGrid_GridEntryTest : TestHelper
{
    public void CheckGridItem(INestedObj ownerObject, string propertyName, GridItem gridItem)
    {
        var context = gridItem as ITypeDescriptorContext;
        Assert.NotNull(gridItem, "gridItem is null (propertyName={0})", propertyName);
        Assert.NotNull(context, "gridItem is not ITypeDescriptorContext (propertyName={0})", propertyName);

        Assert.That((object?)propertyName, Is.EqualTo(gridItem.Label));

        Assert.That((object?)ownerObject, Is.SameAs(context.Instance));
        Assert.That((object?)ownerObject.PropertyAsINestedObj.GetType(), Is.EqualTo(context.PropertyDescriptor?.PropertyType));
        Assert.That((object?)propertyName, Is.EqualTo(context.PropertyDescriptor?.Name));
    }

    [Test]
    public void ITypeDescriptorContextTest()
    {
        var pg = new PropertyGrid();

        var rootObj = new NestedObj0
        {
            Property1 = new NestedObj1
            {
                Property2 = new NestedObj2
                {
                    Property3 = new NestedObj3()
                }
            }
        };
        pg.SelectedObject = rootObj;

        var gridItem_Property1 = pg.GetRootItem();
        INestedObj ownerOf_Property1 = rootObj;
        CheckGridItem(ownerOf_Property1, "Property1", gridItem_Property1);

        var gridItem_Property2 = gridItem_Property1.GridItems?["Property2"];
        INestedObj ownerOf_Property2 = rootObj.Property1;
        CheckGridItem(ownerOf_Property2, "Property2", gridItem_Property2!);

        var gridItem_Property3 = gridItem_Property2?.GridItems?["Property3"];
        INestedObj ownerOf_Property3 = rootObj.Property1.Property2;
        CheckGridItem(ownerOf_Property3, "Property3", gridItem_Property3!);
    }

    [Test]
    public void CustomExpandableConverterTest()
    {
        var pg = new PropertyGrid();

        var rootObj = new ConverterTestRootObject();
        pg.SelectedObject = rootObj;

        var customExpandableGridItem = pg.GetRootItem();
        Assert.That((object?)customExpandableGridItem.Label, Is.EqualTo("CustomExpandableProperty"));

        var substitutedGridItems = customExpandableGridItem.GridItems;
        Assert.That((object?)substitutedGridItems!.Count, Is.EqualTo(1));
        Assert.NotNull(substitutedGridItems["SomeProperty"]);
    }
}

public static class PropertyGridExtentions
{
    // Returns non-Category root `GridItem`.
    public static GridItem GetRootItem(this PropertyGrid pg)
    {
        var gridItem = pg.SelectedGridItem;
        Assert.NotNull(gridItem, "No one GridItem is Selected in the PropertyGrid");

        while (gridItem.Parent is { GridItemType: GridItemType.Property })
        {
            gridItem = gridItem.Parent;
        }

        return gridItem;
    }
}

#region Test Environment: ITypeDescriptorContextTest

[TypeConverter(typeof(ExpandableObjectConverter))]
public interface INestedObj
{
    INestedObj PropertyAsINestedObj { get; }
}

// Root object.
internal class NestedObj0 : INestedObj
{
    public NestedObj1 Property1 { get; set; }

    [Browsable(false)]
    public INestedObj PropertyAsINestedObj => Property1;
}

internal class NestedObj1 : INestedObj
{
    public NestedObj2 Property2 { get; set; }

    [Browsable(false)]
    public INestedObj PropertyAsINestedObj => Property2;
}

internal class NestedObj2 : INestedObj
{
    public NestedObj3 Property3 { get; set; }

    [Browsable(false)]
    public INestedObj PropertyAsINestedObj => Property3;
}

internal class NestedObj3 : INestedObj
{
    [Browsable(false)]
    public INestedObj PropertyAsINestedObj => null!;
}

#endregion  // Test Environment: ITypeDescriptorContextTest

#region Test Environment: CustomExpandableConverter

[TypeConverter(typeof(ExpandableObjectConverter))]
public class ConverterTestRootObject
{
    public ConverterTestPropertiesHolder propertiesHolder = new();

    [TypeConverter(typeof(CustomExpandableConverter))]
    public string CustomExpandableProperty { get; set; }

    public ConverterTestRootObject()
    {
        CustomExpandableProperty = string.Empty;
    }
}

public class ConverterTestPropertiesHolder
{
    public string SomeProperty { get; set; }
}

public class CustomExpandableConverter : TypeConverter
{
    public override bool GetPropertiesSupported(ITypeDescriptorContext? context)
    {
        return true;
    }

    public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext? context, object value, Attribute[]? attributes)
    {
        var testObject = context!.Instance as ConverterTestRootObject;
        return TypeDescriptor.GetProperties(testObject!.propertiesHolder);
    }
}

#endregion  // Test Environment: CustomExpandableConverter