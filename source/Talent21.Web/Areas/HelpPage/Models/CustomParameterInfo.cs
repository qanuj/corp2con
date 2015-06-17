using System.Reflection;

namespace Talent21.Web.Areas.HelpPage.Models
{
    internal class CustomParameterInfo : ParameterInfo
    {
        public PropertyInfo Prop { get; private set; }

        public CustomParameterInfo(PropertyInfo prop)
        {
            Prop = prop;
            base.NameImpl = prop.Name;
        }
    }
}