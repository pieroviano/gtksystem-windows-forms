using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
#if NETSTANDARD
using System.Drawing.Gtk;
#endif
using System.Globalization;
using System.Reflection;

namespace System.Drawing;

/// <summary>Converts colors from one data type to another. Access this class through the <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
/// <filterpriority>1</filterpriority>
public class ColorConverter : TypeConverter
{
    private static readonly string ColorConstantsLock;

    private static Hashtable? colorConstants;

    private static readonly string SystemColorConstantsLock;

    private static Hashtable? systemColorConstants;

    private static readonly string ValuesLock;

    private static StandardValuesCollection? values;

    private static Hashtable Colors
    {
        get
        {
            if (colorConstants == null)
            {
                lock (ColorConstantsLock)
                {
                    if (colorConstants == null)
                    {
                        Hashtable hashtables = new Hashtable(StringComparer.OrdinalIgnoreCase);
                        FillConstants(hashtables, typeof(Color));
                        colorConstants = hashtables;
                    }
                }
            }
            return colorConstants;
        }
    }

    private static Hashtable SystemColors
    {
        get
        {
            if (systemColorConstants == null)
            {
                lock (SystemColorConstantsLock)
                {
                    if (systemColorConstants == null)
                    {
                        Hashtable hashtables = new Hashtable(StringComparer.OrdinalIgnoreCase);
                        FillConstants(hashtables, typeof(SystemColors));
                        systemColorConstants = hashtables;
                    }
                }
            }
            return systemColorConstants;
        }
    }

    static ColorConverter()
    {
        ColorConstantsLock = "colorConstants";
        SystemColorConstantsLock = "systemColorConstants";
        ValuesLock = "values";
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.ColorConverter" /> class.</summary>
    public ColorConverter()
    {
    }

    /// <summary>Determines if this converter can convert an object in the given source type to the native type of the converter.</summary>
    /// <returns>true if this object can perform the conversion; otherwise, false.</returns>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. You can use this object to get additional information about the environment from which this converter is being invoked. </param>
    /// <param name="sourceType">The type from which you want to convert. </param>
    /// <filterpriority>1</filterpriority>
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
        if (sourceType == typeof(string))
        {
            return true;
        }
        return base.CanConvertFrom(context, sourceType);
    }

    /// <summary>Returns a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
    /// <returns>true if this converter can perform the operation; otherwise, false.</returns>
    /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
    /// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type to which you want to convert. </param>
    /// <filterpriority>1</filterpriority>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
        if (destinationType == typeof(InstanceDescriptor))
        {
            return true;
        }
        return base.CanConvertTo(context, destinationType);
    }

    /// <summary>Converts the given object to the converter's native type.</summary>
    /// <returns>An <see cref="T:System.Object" /> representing the converted value.</returns>
    /// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> that provides a format context. You can use this object to get additional information about the environment from which this converter is being invoked. </param>
    /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to represent the color. </param>
    /// <param name="value">The object to convert. </param>
    /// <exception cref="T:System.ArgumentException">The conversion cannot be performed.</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public override object? ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object? value)
    {
        string? str = value as string;
        if (str == null)
        {
            return base.ConvertFrom(context, culture, value);
        }
        object? namedColor;
        string str1 = str.Trim();
        if (str1.Length != 0)
        {
            namedColor = GetNamedColor(str1);
            if (namedColor == null)
            {
                if (culture == null)
                {
                    culture = CultureInfo.CurrentCulture;
                }
                char listSeparator = culture.TextInfo.ListSeparator[0];
                bool flag = true;
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
                if (str1.IndexOf(listSeparator) == -1)
                {
                    if (str1.Length >= 2 && (str1[0] == '\'' || str1[0] == '\"') && str1[0] == str1[str1.Length - 1])
                    {
                        string str2 = str1.Substring(1, str1.Length - 2);
                        namedColor = Color.FromName(str2);
                        flag = false;
                    }
                    else if (str1.Length == 7 && str1[0] == '#' || str1.Length == 8 && (str1.StartsWith("0x") || str1.StartsWith("0X")) || str1.Length == 8 && (str1.StartsWith("&h") || str1.StartsWith("&H")))
                    {
                        namedColor = Color.FromArgb(-16777216 | (int)converter.ConvertFromString(context, culture, str1));
                    }
                }
                if (namedColor == null)
                {
                    string[] strArrays = str1.Split(listSeparator);
                    int[] numArray = new int[strArrays.Length];
                    for (int i = 0; i < numArray.Length; i++)
                    {
                        numArray[i] = (int)converter.ConvertFromString(context, culture, strArrays[i]);
                    }
                    switch (numArray.Length)
                    {
                        case 1:
                        {
                            namedColor = Color.FromArgb(numArray[0]);
                            goto case 2;
                        }
                        case 2:
                        {
                            flag = true;
                            break;
                        }
                        case 3:
                        {
                            namedColor = Color.FromArgb(numArray[0], numArray[1], numArray[2]);
                            goto case 2;
                        }
                        case 4:
                        {
                            namedColor = Color.FromArgb(numArray[0], numArray[1], numArray[2], numArray[3]);
                            goto case 2;
                        }
                        default:
                        {
                            goto case 2;
                        }
                    }
                }
                if (namedColor != null & flag)
                {
                    int argb = ((Color)(namedColor??Color.Empty)).ToArgb();
                    foreach (Color color in Colors.Values)
                    {
                        if (color.ToArgb() != argb)
                        {
                            continue;
                        }
                        namedColor = color;
                        goto Label1;
                    }
                }
            }
            Label1:
            if (namedColor == null)
            {
                throw new ArgumentException(string.Format("InvalidColor {0}", str1));
            }
        }
        else
        {
            namedColor = Color.Empty;
        }
        return namedColor;
    }

    /// <summary>Converts the specified object to another type. </summary>
    /// <returns>An <see cref="T:System.Object" /> representing the converted value.</returns>
    /// <param name="context">A formatter context. Use this object to extract additional information about the environment from which this converter is being invoked. Always check whether this value is null. Also, properties on the context object may return null. </param>
    /// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to represent the color. </param>
    /// <param name="value">The object to convert. </param>
    /// <param name="destinationType">The type to convert the object to. </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="destinationtype" /> is null.</exception>
    /// <exception cref="T:System.NotSupportedException">The conversion cannot be performed.</exception>
    /// <filterpriority>1</filterpriority>
    /// <PermissionSet>
    ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
    /// </PermissionSet>
    public override object? ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
        if (destinationType == null)
        {
            throw new ArgumentNullException("destinationType");
        }
        if (value is Color)
        {
            if (destinationType == typeof(string))
            {
                Color color = (Color)value;
                if (color == Color.Empty)
                {
                    return string.Empty;
                }
#if NET462_OR_GREATER
                    if (color.IsKnownColor)
                    {
                        return color.Name;
                    }
#endif
                if (color.IsNamedColor)
                {
                    return string.Concat("'", color.Name, "'");
                }
                if (culture == null)
                {
                    culture = CultureInfo.CurrentCulture;
                }
                string str1 = string.Concat(culture.TextInfo.ListSeparator, " ");
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));
                int num = 0;
                string[] str;
                if (color.A >= 255)
                {
                    str = new string[3];
                }
                else
                {
                    str = new string[4];
                    int num1 = num;
                    num = num1 + 1;
                    str[num1] = converter.ConvertToString(context, culture, color.A)!;
                }
                int num2 = num;
                num = num2 + 1;
                str[num2] = converter.ConvertToString(context, culture, color.R)!;
                int num3 = num;
                num = num3 + 1;
                str[num3] = converter.ConvertToString(context, culture, color.G)!;
                int num4 = num;
                num = num4 + 1;
                str[num4] = converter.ConvertToString(context, culture, color.B)!;
                return string.Join(str1, str);
            }
            if (destinationType == typeof(InstanceDescriptor))
            {
                MemberInfo? field = null;
                object[]? a = null;
                Color color1 = (Color)value;
                if (color1.IsEmpty)
                {
                    field = typeof(Color).GetField("Empty");
                }
#if NET462_OR_GREATER
                    else if (color1.IsSystemColor)
                    {
                        field = typeof(SystemColors).GetProperty(color1.Name);
                    }
                    else if (color1.IsKnownColor)
                    {
                        field = typeof(Color).GetProperty(color1.Name);
                    }
#endif
                else if (color1.A != 255)
                {
                    field = typeof(Color).GetMethod("FromArgb", new[] { typeof(int), typeof(int), typeof(int), typeof(int) });
                    a = new object[] { color1.A, color1.R, color1.G, color1.B };
                }
                else if (!color1.IsNamedColor)
                {
                    field = typeof(Color).GetMethod("FromArgb", new[] { typeof(int), typeof(int), typeof(int) });
                    a = new object[] { color1.R, color1.G, color1.B };
                }
                else
                {
                    field = typeof(Color).GetMethod("FromName", new[] { typeof(string) });
                    a = new object[] { color1.Name };
                }
                if (field == null)
                {
                    return null;
                }
                return new InstanceDescriptor(field, a);
            }
        }
        return base.ConvertTo(context, culture, value, destinationType) ?? throw new InvalidOperationException("ConvertTo");
    }

    private static void FillConstants(Hashtable? hash, Type enumType)
    {
        MethodAttributes methodAttribute = MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Public | MethodAttributes.Static;
        PropertyInfo[] properties = enumType.GetProperties();
        for (int i = 0; i < properties.Length; i++)
        {
            PropertyInfo value = properties[i];
            if (value.PropertyType == typeof(Color))
            {
                MethodInfo getMethod = value.GetGetMethod();
                if (getMethod != null && (getMethod.Attributes & methodAttribute) == methodAttribute)
                {
                    object[]? objArray = null;
                    if (hash != null)
                    {
                        hash[value.Name] = value.GetValue(null, objArray);
                    }
                }
            }
        }
    }

    internal static object GetNamedColor(string name)
    {
        object? item = null;
        item = Colors[name];
        if (item != null)
        {
            return item;
        }
        item = SystemColors[name];
        return item;
    }

    /// <summary>Retrieves a collection containing a set of standard values for the data type for which this validator is designed. This will return null if the data type does not support a standard set of values.</summary>
    /// <returns>A collection containing null or a standard set of valid values. The default implementation always returns null.</returns>
    /// <param name="context">A formatter context. Use this object to extract additional information about the environment from which this converter is being invoked. Always check whether this value is null. Also, properties on the context object may return null. </param>
    /// <filterpriority>1</filterpriority>
    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    {
        if (values == null)
        {
            lock (ValuesLock)
            {
                if (values == null)
                {
                    ArrayList arrayLists = new ArrayList();
                    arrayLists.AddRange(Colors.Values);
                    arrayLists.AddRange(SystemColors.Values);
                    int count = arrayLists.Count;
                    for (int i = 0; i < count - 1; i++)
                    {
                        for (int j = i + 1; j < count; j++)
                        {
                            if (arrayLists[i].Equals(arrayLists[j]))
                            {
                                arrayLists.RemoveAt(j);
                                count--;
                                j--;
                            }
                        }
                    }
                    arrayLists.Sort(0, arrayLists.Count, new ColorComparer());
                    values = new StandardValuesCollection(arrayLists.ToArray());
                }
            }
        }
        return values;
    }

    /// <summary>Determines if this object supports a standard set of values that can be chosen from a list.</summary>
    /// <returns>true if <see cref="Overload:System.Drawing.ColorConverter.GetStandardValues" /> must be called to find a common set of values the object supports; otherwise, false.</returns>
    /// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided. </param>
    /// <filterpriority>1</filterpriority>
    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    {
        return true;
    }

    private class ColorComparer : IComparer
    {
        public int Compare(object? left, object? right)
        {
            Color color = (Color)(left ?? Color.Empty);
            Color color1 = (Color)(right ?? Color.Empty);
            return string.Compare(color.Name, color1.Name, false, CultureInfo.InvariantCulture);
        }
    }
}