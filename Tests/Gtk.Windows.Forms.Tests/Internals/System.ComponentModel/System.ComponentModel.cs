using System.Globalization;

namespace System.ComponentModel
{
    internal class ComponentResourceManager : GTKSystem.ComponentModel.ComponentResourceManager
    {
        public ComponentResourceManager(Type form) : base(form)
        {

        }
        public new object GetObject(string name, CultureInfo culture)
        {

            return GetObject(name);
        }

        public new object GetObject(string name)
        {
            return base.GetObject(name);
        }
    }

}

