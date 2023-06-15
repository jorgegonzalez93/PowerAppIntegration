using Migration.Domain.Domain.Enums;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace Migration.Domain.Domain.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public static IEnumerable<string> GetListOfDescription<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<Enum>().Select(x => x.GetDescription()).ToList();
        }

        public static bool StringComparator(this string value, string searchValue)
        {
            if (string.IsNullOrEmpty(searchValue) && string.IsNullOrEmpty(value))
            {
                return true;
            }

            if (string.IsNullOrEmpty(searchValue))
            {
                return false;
            }

            return value!.Contains(searchValue, StringComparison.OrdinalIgnoreCase);
        }

        public static string GetValueData(this DataRow row, string fieldValue)
        {
            return row[fieldValue].ToString()!;
        }
    }
}
