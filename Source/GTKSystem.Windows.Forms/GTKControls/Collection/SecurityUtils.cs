using System;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Forms
{
    internal static class SecurityUtils
    {
        private static bool HasReflectionPermission(Type type)
        {
            bool flag;
            try
            {
                flag = true;
            }
            catch (SecurityException securityException)
            {
                return false;
            }
            return flag;
        }

        internal static object SecureCreateInstance(Type type)
        {
            return SecurityUtils.SecureCreateInstance(type, null, false);
        }

        internal static object SecureCreateInstance(Type type, object[] args, bool allowNonPublic)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            BindingFlags bindingFlag = BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;
            if (!type.IsVisible)
            {
            }
            else if (allowNonPublic && !SecurityUtils.HasReflectionPermission(type))
            {
                allowNonPublic = false;
            }
            if (allowNonPublic)
            {
                bindingFlag |= BindingFlags.NonPublic;
            }
            return Activator.CreateInstance(type, bindingFlag, null, args, null);
        }
    }
}