using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace System.Windows.Forms;

internal class ListBindingHelper
{
    public static object GetList(object list)
    {
        if (!(list is IListSource))
        {
            return list;
        }
        return (list as IListSource).GetList();
    }

    private static PropertyDescriptorCollection GetListItemPropertiesByInstance(object target, PropertyDescriptor[] listAccessors, int startIndex)
    {
        PropertyDescriptorCollection properties;
        if (listAccessors == null || (int)listAccessors.Length <= startIndex)
        {
            properties = TypeDescriptor.GetProperties(target, ListBindingHelper.BrowsableAttributeList);
        }
        else
        {
            object value = listAccessors[startIndex].GetValue(target);
            if (value != null)
            {
                PropertyDescriptor[] propertyDescriptorArray = null;
                if ((int)listAccessors.Length > startIndex + 1)
                {
                    int length = (int)listAccessors.Length - (startIndex + 1);
                    propertyDescriptorArray = new PropertyDescriptor[length];
                    for (int i = 0; i < length; i++)
                    {
                        propertyDescriptorArray[i] = listAccessors[startIndex + 1 + i];
                    }
                }
                properties = ListBindingHelper.GetListItemProperties(value, propertyDescriptorArray);
            }
            else
            {
                properties = ListBindingHelper.GetListItemPropertiesByType(listAccessors[startIndex].PropertyType, listAccessors, startIndex);
            }
        }
        return properties;
    }

    private static PropertyDescriptorCollection GetListItemPropertiesByType(Type type, PropertyDescriptor[] listAccessors, int startIndex)
    {
        PropertyDescriptorCollection propertyDescriptorCollections = null;
        Type propertyType = listAccessors[startIndex].PropertyType;
        startIndex++;
        propertyDescriptorCollections = (startIndex < (int)listAccessors.Length ? ListBindingHelper.GetListItemPropertiesByType(propertyType, listAccessors, startIndex) : ListBindingHelper.GetListItemProperties(propertyType));
        return propertyDescriptorCollections;
    }

    public static PropertyDescriptorCollection GetListItemProperties(object list)
    {
        PropertyDescriptorCollection itemProperties;
        if (list == null)
        {
            return new PropertyDescriptorCollection(null);
        }
        if (!(list is Type))
        {
            object obj = ListBindingHelper.GetList(list);
            if (!(obj is ITypedList))
            {
                itemProperties = (!(obj is IEnumerable) ? TypeDescriptor.GetProperties(obj) : ListBindingHelper.GetListItemPropertiesByEnumerable(obj as IEnumerable));
            }
            else
            {
                itemProperties = (obj as ITypedList).GetItemProperties(null);
            }
        }
        else
        {
            itemProperties = ListBindingHelper.GetListItemPropertiesByType(list as Type);
        }
        return itemProperties;
    }

    private static PropertyDescriptorCollection GetListItemPropertiesByEnumerable(IEnumerable enumerable)
    {
        PropertyDescriptorCollection properties = null;
        Type type = enumerable.GetType();
        if (!typeof(Array).IsAssignableFrom(type))
        {
            ITypedList typedList = enumerable as ITypedList;
            if (typedList == null)
            {
                PropertyInfo typedIndexer = ListBindingHelper.GetTypedIndexer(type);
                if (typedIndexer != null && !typeof(ICustomTypeDescriptor).IsAssignableFrom(typedIndexer.PropertyType))
                {
                    properties = TypeDescriptor.GetProperties(typedIndexer.PropertyType, ListBindingHelper.BrowsableAttributeList);
                }
            }
            else
            {
                properties = typedList.GetItemProperties(null);
            }
        }
        else
        {
            properties = TypeDescriptor.GetProperties(type.GetElementType(), ListBindingHelper.BrowsableAttributeList);
        }
        if (properties == null)
        {
            object firstItemByEnumerable = ListBindingHelper.GetFirstItemByEnumerable(enumerable);
            if (enumerable is string)
            {
                properties = TypeDescriptor.GetProperties(enumerable, ListBindingHelper.BrowsableAttributeList);
            }
            else if (firstItemByEnumerable != null)
            {
                properties = TypeDescriptor.GetProperties(firstItemByEnumerable, ListBindingHelper.BrowsableAttributeList);
                if (!(enumerable is IList) && (properties == null || properties.Count == 0))
                {
                    properties = TypeDescriptor.GetProperties(enumerable, ListBindingHelper.BrowsableAttributeList);
                }
            }
            else
            {
                properties = new PropertyDescriptorCollection(null);
            }
        }
        return properties;
    }

    private static bool IsListBasedType(Type type)
    {
        if (typeof(IList).IsAssignableFrom(type) || typeof(ITypedList).IsAssignableFrom(type) || typeof(IListSource).IsAssignableFrom(type))
        {
            return true;
        }
        if (type.IsGenericType && !type.IsGenericTypeDefinition && typeof(IList<>).IsAssignableFrom(type.GetGenericTypeDefinition()))
        {
            return true;
        }
        Type[] interfaces = type.GetInterfaces();
        for (int i = 0; i < (int)interfaces.Length; i++)
        {
            Type type1 = interfaces[i];
            if (type1.IsGenericType && typeof(IList<>).IsAssignableFrom(type1.GetGenericTypeDefinition()))
            {
                return true;
            }
        }
        return false;
    }
    private static PropertyInfo GetTypedIndexer(Type type)
    {
        PropertyInfo propertyInfo = null;
        if (!ListBindingHelper.IsListBasedType(type))
        {
            return null;
        }
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
        for (int i = 0; i < (int)properties.Length; i++)
        {
            if (properties[i].GetIndexParameters().Length != 0 && properties[i].PropertyType != typeof(object))
            {
                propertyInfo = properties[i];
                if (propertyInfo.Name == "Item")
                {
                    break;
                }
            }
        }
        return propertyInfo;
    }
    private static object GetFirstItemByEnumerable(IEnumerable enumerable)
    {
        object current = null;
        if (!(enumerable is IList))
        {
            try
            {
                IEnumerator enumerator = enumerable.GetEnumerator();
                enumerator.Reset();
                if (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                }
                enumerator.Reset();
            }
            catch (NotSupportedException notSupportedException)
            {
                current = null;
            }
        }
        else
        {
            IList lists = enumerable as IList;
            current = (lists.Count > 0 ? lists[0] : null);
        }
        return current;
    }

    private static PropertyDescriptorCollection GetListItemPropertiesByEnumerable(IEnumerable enumerable, PropertyDescriptor[] listAccessors)
    {
        PropertyDescriptorCollection listItemPropertiesByEnumerable = null;
        if (listAccessors == null || listAccessors.Length == 0)
        {
            listItemPropertiesByEnumerable = ListBindingHelper.GetListItemPropertiesByEnumerable(enumerable);
        }
        else
        {
            ITypedList typedList = enumerable as ITypedList;
            listItemPropertiesByEnumerable = (typedList == null ? ListBindingHelper.GetListItemPropertiesByEnumerable(enumerable, listAccessors, 0) : typedList.GetItemProperties(listAccessors));
        }
        return listItemPropertiesByEnumerable;
    }

    private static PropertyDescriptorCollection GetListItemPropertiesByEnumerable(IEnumerable iEnumerable, PropertyDescriptor[] listAccessors, int startIndex)
    {
        PropertyDescriptorCollection listItemPropertiesByInstance = null;
        object list = null;
        object firstItemByEnumerable = ListBindingHelper.GetFirstItemByEnumerable(iEnumerable);
        if (firstItemByEnumerable != null)
        {
            list = ListBindingHelper.GetList(listAccessors[startIndex].GetValue(firstItemByEnumerable));
        }
        if (list != null)
        {
            startIndex++;
            IEnumerable enumerable = list as IEnumerable;
            if (enumerable == null)
            {
                listItemPropertiesByInstance = ListBindingHelper.GetListItemPropertiesByInstance(list, listAccessors, startIndex);
            }
            else
            {
                listItemPropertiesByInstance = (startIndex != (int)listAccessors.Length ? ListBindingHelper.GetListItemPropertiesByEnumerable(enumerable, listAccessors, startIndex) : ListBindingHelper.GetListItemPropertiesByEnumerable(enumerable));
            }
        }
        else
        {
            listItemPropertiesByInstance = ListBindingHelper.GetListItemPropertiesByType(listAccessors[startIndex].PropertyType, listAccessors, startIndex);
        }
        return listItemPropertiesByInstance;
    }

    public static PropertyDescriptorCollection GetListItemProperties(object list, PropertyDescriptor[] listAccessors)
    {
        PropertyDescriptorCollection listItemProperties;
        if (listAccessors == null || listAccessors.Length == 0)
        {
            listItemProperties = ListBindingHelper.GetListItemProperties(list);
        }
        else if (!(list is Type))
        {
            object obj = ListBindingHelper.GetList(list);
            if (!(obj is ITypedList))
            {
                listItemProperties = (!(obj is IEnumerable) ? ListBindingHelper.GetListItemPropertiesByInstance(obj, listAccessors, 0) : ListBindingHelper.GetListItemPropertiesByEnumerable(obj as IEnumerable, listAccessors));
            }
            else
            {
                listItemProperties = (obj as ITypedList).GetItemProperties(listAccessors);
            }
        }
        else
        {
            listItemProperties = ListBindingHelper.GetListItemPropertiesByType(list as Type, listAccessors);
        }
        return listItemProperties;
    }

    private static PropertyDescriptorCollection GetListItemPropertiesByType(Type type, PropertyDescriptor[] listAccessors)
    {
        PropertyDescriptorCollection propertyDescriptorCollections = null;
        propertyDescriptorCollections = (listAccessors == null || listAccessors.Length == 0 ? ListBindingHelper.GetListItemPropertiesByType(type) : ListBindingHelper.GetListItemPropertiesByType(type, listAccessors, 0));
        return propertyDescriptorCollections;
    }

    private static PropertyDescriptorCollection GetListItemPropertiesByType(Type type)
    {
        return TypeDescriptor.GetProperties(ListBindingHelper.GetListItemType(type), ListBindingHelper.BrowsableAttributeList);
    }

    public static Type GetListItemType(object list)
    {
        if (list == null)
        {
            return null;
        }
        Type propertyType = null;
        if (list is Type && typeof(IListSource).IsAssignableFrom(list as Type))
        {
            list = ListBindingHelper.CreateInstanceOfType(list as Type);
        }
        list = ListBindingHelper.GetList(list);
        Type type = (list is Type ? list as Type : list.GetType());
        object obj = (list is Type ? null : list);
        if (!typeof(Array).IsAssignableFrom(type))
        {
            PropertyInfo typedIndexer = ListBindingHelper.GetTypedIndexer(type);
            if (typedIndexer == null)
            {
                propertyType = (!(obj is IEnumerable) ? type : ListBindingHelper.GetListItemTypeByEnumerable(obj as IEnumerable));
            }
            else
            {
                propertyType = typedIndexer.PropertyType;
            }
        }
        else
        {
            propertyType = type.GetElementType();
        }
        return propertyType;
    }

    private static Type GetListItemTypeByEnumerable(IEnumerable iEnumerable)
    {
        object firstItemByEnumerable = ListBindingHelper.GetFirstItemByEnumerable(iEnumerable);
        if (firstItemByEnumerable == null)
        {
            return typeof(object);
        }
        return firstItemByEnumerable.GetType();
    }
    private static object CreateInstanceOfType(Type type)
    {
        object obj = null;
        Exception exception = null;
        try
        {
            obj = SecurityUtils.SecureCreateInstance(type);
        }
        catch (TargetInvocationException targetInvocationException)
        {
            exception = targetInvocationException;
        }
        catch (MethodAccessException methodAccessException)
        {
            exception = methodAccessException;
        }
        catch (MissingMethodException missingMethodException)
        {
            exception = missingMethodException;
        }
        if (exception != null)
        {
            throw new NotSupportedException("BindingSourceInstanceError", exception);
        }
        return obj;
    }
    private static Attribute[] browsableAttribute;

    private static Attribute[] BrowsableAttributeList
    {
        get
        {
            if (ListBindingHelper.browsableAttribute == null)
            {
                ListBindingHelper.browsableAttribute = new Attribute[] { new BrowsableAttribute(true) };
            }
            return ListBindingHelper.browsableAttribute;
        }
    }
}