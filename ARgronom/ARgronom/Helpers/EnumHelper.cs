using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ARgronom.Contexts.Helpers
{
    public static class EnumHelper
    {

        public static Dictionary<string, T> GetAllValuesWithDescription<T>() where T : Enum
        {
            var values = Enum.GetValues(typeof(T));
            var valuesList = new Dictionary<string, T>();
            for (int i = 0; i < values.Length; i++)
            {
                var thisEl = (T)values.GetValue(i);
                valuesList.Add(thisEl.GetDescription(), thisEl);
            }
            return valuesList;
        }

        public static string GetDescription(this Enum value)
        {
            var result = string.Empty;

            if (value != null)
            {
                Type type = value.GetType();
                var names = value.ToString()
                    .Split(',')
                    .Select(v => v.Replace(" ", string.Empty))
                    .ToList();
                var descriptionsBuilder = new StringBuilder();

                if (names.Any())
                {
                    foreach (var name in names)
                    {
                        var field = type.GetField(name);
                        if (field != null)
                        {
                            if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attr)
                            {
                                descriptionsBuilder.Append(attr.Description);
                                if (name != names.Last())
                                {
                                    descriptionsBuilder.Append(", ");
                                }
                            }
                        }
                    }
                }
                result = descriptionsBuilder.ToString();
            }

            return result;
        }

    }
}
