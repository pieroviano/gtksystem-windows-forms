using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace System.Windows.Forms
{
    internal class Formatter
    {
        private static Type stringType;

        private static Type booleanType;

        private static Type checkStateType;

        private static object parseMethodNotFound;

        private static object defaultDataSourceNullValue;

        static Formatter()
        {
            Formatter.stringType = typeof(string);
            Formatter.booleanType = typeof(bool);
            Formatter.checkStateType = typeof(CheckState);
            Formatter.parseMethodNotFound = new object();
            Formatter.defaultDataSourceNullValue = DBNull.Value;
        }

        public Formatter()
        {
        }

        private static object ChangeType(object value, Type type, IFormatProvider formatInfo)
        {
            object obj;
            try
            {
                if (formatInfo == null)
                {
                    formatInfo = CultureInfo.CurrentCulture;
                }
                obj = Convert.ChangeType(value, type, formatInfo);
            }
            catch (InvalidCastException invalidCastException1)
            {
                InvalidCastException invalidCastException = invalidCastException1;
                throw new FormatException(invalidCastException.Message, invalidCastException);
            }
            return obj;
        }

        private static bool EqualsFormattedNullValue(object value, object formattedNullValue, IFormatProvider formatInfo)
        {
            string str = formattedNullValue as string;
            string str1 = value as string;
            if (str == null || str1 == null)
            {
                return object.Equals(value, formattedNullValue);
            }
            if (str.Length != str1.Length)
            {
                return false;
            }
            return string.Compare(str1, str, true, Formatter.GetFormatterCulture(formatInfo)) == 0;
        }

        public static object FormatObject(object value, Type targetType, TypeConverter sourceConverter, TypeConverter targetConverter, string formatString, IFormatProvider formatInfo, object formattedNullValue, object dataSourceNullValue)
        {
            if (Formatter.IsNullData(value, dataSourceNullValue))
            {
                value = DBNull.Value;
            }
            Type type = targetType;
            targetType = Formatter.NullableUnwrap(targetType);
            sourceConverter = Formatter.NullableUnwrap(sourceConverter);
            targetConverter = Formatter.NullableUnwrap(targetConverter);
            bool flag = targetType != type;
            object obj = Formatter.FormatObjectInternal(value, targetType, sourceConverter, targetConverter, formatString, formatInfo, formattedNullValue);
            if (type.IsValueType && obj == null && !flag)
            {
                throw new FormatException(Formatter.GetCantConvertMessage(value, targetType));
            }
            return obj;
        }

        private static object FormatObjectInternal(object value, Type targetType, TypeConverter sourceConverter, TypeConverter targetConverter, string formatString, IFormatProvider formatInfo, object formattedNullValue)
        {
            if (value == DBNull.Value || value == null)
            {
                if (formattedNullValue != null)
                {
                    return formattedNullValue;
                }
                if (targetType == Formatter.stringType)
                {
                    return string.Empty;
                }
                if (targetType != Formatter.checkStateType)
                {
                    return null;
                }
                return CheckState.Indeterminate;
            }
            if (targetType == Formatter.stringType && value is IFormattable && !string.IsNullOrEmpty(formatString))
            {
                return (value as IFormattable).ToString(formatString, formatInfo);
            }
            Type type = value.GetType();
            TypeConverter converter = TypeDescriptor.GetConverter(type);
            if (sourceConverter != null && sourceConverter != converter && sourceConverter.CanConvertTo(targetType))
            {
                return sourceConverter.ConvertTo(null, Formatter.GetFormatterCulture(formatInfo), value, targetType);
            }
            TypeConverter typeConverter = TypeDescriptor.GetConverter(targetType);
            if (targetConverter != null && targetConverter != typeConverter && targetConverter.CanConvertFrom(type))
            {
                return targetConverter.ConvertFrom(null, Formatter.GetFormatterCulture(formatInfo), value);
            }
            if (targetType == Formatter.checkStateType)
            {
                if (type == Formatter.booleanType)
                {
                    return ((bool)value ? CheckState.Checked : CheckState.Unchecked);
                }
                if (sourceConverter == null)
                {
                    sourceConverter = converter;
                }
                if (sourceConverter != null && sourceConverter.CanConvertTo(Formatter.booleanType))
                {
                    return ((bool)sourceConverter.ConvertTo(null, Formatter.GetFormatterCulture(formatInfo), value, Formatter.booleanType) ? CheckState.Checked : CheckState.Unchecked);
                }
            }
            if (targetType.IsAssignableFrom(type))
            {
                return value;
            }
            if (sourceConverter == null)
            {
                sourceConverter = converter;
            }
            if (targetConverter == null)
            {
                targetConverter = typeConverter;
            }
            if (sourceConverter != null && sourceConverter.CanConvertTo(targetType))
            {
                return sourceConverter.ConvertTo(null, Formatter.GetFormatterCulture(formatInfo), value, targetType);
            }
            if (targetConverter != null && targetConverter.CanConvertFrom(type))
            {
                return targetConverter.ConvertFrom(null, Formatter.GetFormatterCulture(formatInfo), value);
            }
            if (!(value is IConvertible))
            {
                throw new FormatException(Formatter.GetCantConvertMessage(value, targetType));
            }
            return Formatter.ChangeType(value, targetType, formatInfo);
        }

        private static string GetCantConvertMessage(object value, Type targetType)
        {
            string str = (value == null ? "Formatter_CantConvertNull" : "Formatter_CantConvert");
            return string.Format(CultureInfo.CurrentCulture, str, new object[] { value, targetType.Name });
        }

        public static object GetDefaultDataSourceNullValue(Type type)
        {
            if (type != null && !type.IsValueType)
            {
                return null;
            }
            return Formatter.defaultDataSourceNullValue;
        }

        private static CultureInfo GetFormatterCulture(IFormatProvider formatInfo)
        {
            if (!(formatInfo is CultureInfo))
            {
                return CultureInfo.CurrentCulture;
            }
            return formatInfo as CultureInfo;
        }

        public static object InvokeStringParseMethod(object value, Type targetType, IFormatProvider formatInfo)
        {
            object obj;
            try
            {
                MethodInfo method = targetType.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, null, new Type[] { Formatter.stringType, typeof(NumberStyles), typeof(IFormatProvider) }, null);
                if (method == null)
                {
                    method = targetType.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, null, new Type[] { Formatter.stringType, typeof(IFormatProvider) }, null);
                    if (method == null)
                    {
                        method = targetType.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, null, new Type[] { Formatter.stringType }, null);
                        obj = (method == null ? Formatter.parseMethodNotFound : method.Invoke(null, new object[] { (string)value }));
                    }
                    else
                    {
                        obj = method.Invoke(null, new object[] { (string)value, formatInfo });
                    }
                }
                else
                {
                    obj = method.Invoke(null, new object[] { (string)value, NumberStyles.Any, formatInfo });
                }
            }
            catch (TargetInvocationException targetInvocationException1)
            {
                TargetInvocationException targetInvocationException = targetInvocationException1;
                throw new FormatException(targetInvocationException.InnerException.Message, targetInvocationException.InnerException);
            }
            return obj;
        }

        public static bool IsNullData(object value, object dataSourceNullValue)
        {
            if (value == null || value == DBNull.Value)
            {
                return true;
            }
            return object.Equals(value, Formatter.NullData(value.GetType(), dataSourceNullValue));
        }

        private static Type NullableUnwrap(Type type)
        {
            if (type == Formatter.stringType)
            {
                return Formatter.stringType;
            }
            return Nullable.GetUnderlyingType(type) ?? type;
        }

        private static TypeConverter NullableUnwrap(TypeConverter typeConverter)
        {
            NullableConverter nullableConverter = typeConverter as NullableConverter;
            if (nullableConverter == null)
            {
                return typeConverter;
            }
            return nullableConverter.UnderlyingTypeConverter;
        }

        public static object NullData(Type type, object dataSourceNullValue)
        {
            if (!type.IsGenericType || !(type.GetGenericTypeDefinition() == typeof(Nullable<>)))
            {
                return dataSourceNullValue;
            }
            if (dataSourceNullValue != null && dataSourceNullValue != DBNull.Value)
            {
                return dataSourceNullValue;
            }
            return null;
        }

        public static object ParseObject(object value, Type targetType, Type sourceType, TypeConverter targetConverter, TypeConverter sourceConverter, IFormatProvider formatInfo, object formattedNullValue, object dataSourceNullValue)
        {
            Type type = targetType;
            sourceType = Formatter.NullableUnwrap(sourceType);
            targetType = Formatter.NullableUnwrap(targetType);
            sourceConverter = Formatter.NullableUnwrap(sourceConverter);
            targetConverter = Formatter.NullableUnwrap(targetConverter);
            object obj = Formatter.ParseObjectInternal(value, targetType, sourceType, targetConverter, sourceConverter, formatInfo, formattedNullValue);
            if (obj != DBNull.Value)
            {
                return obj;
            }
            return Formatter.NullData(type, dataSourceNullValue);
        }

        private static object ParseObjectInternal(object value, Type targetType, Type sourceType, TypeConverter targetConverter, TypeConverter sourceConverter, IFormatProvider formatInfo, object formattedNullValue)
        {
            if (Formatter.EqualsFormattedNullValue(value, formattedNullValue, formatInfo) || value == DBNull.Value)
            {
                return DBNull.Value;
            }
            TypeConverter converter = TypeDescriptor.GetConverter(targetType);
            if (targetConverter != null && converter != targetConverter && targetConverter.CanConvertFrom(sourceType))
            {
                return targetConverter.ConvertFrom(null, Formatter.GetFormatterCulture(formatInfo), value);
            }
            TypeConverter typeConverter = TypeDescriptor.GetConverter(sourceType);
            if (sourceConverter != null && typeConverter != sourceConverter && sourceConverter.CanConvertTo(targetType))
            {
                return sourceConverter.ConvertTo(null, Formatter.GetFormatterCulture(formatInfo), value, targetType);
            }
            if (value is string)
            {
                object obj = Formatter.InvokeStringParseMethod(value, targetType, formatInfo);
                if (obj != Formatter.parseMethodNotFound)
                {
                    return obj;
                }
            }
            else if (value is CheckState state && state != CheckState.Unchecked)
            {
                CheckState checkState = (CheckState)value;
                if (checkState == CheckState.Indeterminate)
                {
                    return DBNull.Value;
                }
                if (targetType == Formatter.booleanType)
                {
                    return checkState == CheckState.Checked;
                }
                if (targetConverter == null)
                {
                    targetConverter = converter;
                }
                if (targetConverter != null && targetConverter.CanConvertFrom(Formatter.booleanType))
                {
                    return targetConverter.ConvertFrom(null, Formatter.GetFormatterCulture(formatInfo), checkState == CheckState.Checked);
                }
            }
            else if (value != null && targetType.IsAssignableFrom(value.GetType()))
            {
                return value;
            }
            if (targetConverter == null)
            {
                targetConverter = converter;
            }
            if (sourceConverter == null)
            {
                sourceConverter = typeConverter;
            }
            if (targetConverter != null && targetConverter.CanConvertFrom(sourceType))
            {
                return targetConverter.ConvertFrom(null, Formatter.GetFormatterCulture(formatInfo), value);
            }
            if (sourceConverter != null && sourceConverter.CanConvertTo(targetType))
            {
                return sourceConverter.ConvertTo(null, Formatter.GetFormatterCulture(formatInfo), value, targetType);
            }
            if (!(value is IConvertible))
            {
                throw new FormatException(Formatter.GetCantConvertMessage(value, targetType));
            }
            return Formatter.ChangeType(value, targetType, formatInfo);
        }
    }
}