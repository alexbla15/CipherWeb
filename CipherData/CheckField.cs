using CipherData.Models;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

namespace CipherData
{
    /// <summary>
    /// A class of checking functions for different objects.
    /// </summary>
    public class CheckField
    {
        public bool Succeeded { get; set; } = true;
        public string Message { get; set; } = string.Empty;

        public CheckField(bool succeeded = true, string message = "")
        {
            Succeeded = succeeded;
            Message = message;
        }

        public static CheckField Greater(decimal value, decimal min_value, string field_name, string? min_field_name = null)
        {
            string ErrorMessage = (min_field_name is null) ?
                $"השדה \"{field_name}\" חייב להיות גדול מ {min_value}." : $"השדה \"{field_name}\" חייב להיות גדול מ {min_field_name}.";

            bool condition = value > min_value;

            return new CheckField(
                succeeded: condition,
                message: condition ? string.Empty : ErrorMessage
                );
        }

        public static CheckField GreaterEqual(decimal value, decimal min_value, string field_name, string? min_field_name = null)
        {
            string ErrorMessage = (min_field_name is null) ? 
                $"השדה \"{field_name}\" חייב להיות גדול/שווה ל {min_value}." : $"השדה \"{field_name}\" חייב להיות גדול/שווה ל {min_field_name}.";

            bool condition = value >= min_value;

            return new CheckField(
                succeeded: condition,
                message: condition ? string.Empty : ErrorMessage
                );
        }

        public static CheckField LowerEqual(decimal value, decimal min_value, string field_name, string? min_field_name = null)
        {
            string ErrorMessage = (min_field_name is null) ?
                $"השדה \"{field_name}\" חייב להיות קטן/שווה ל {min_value}." : $"השדה \"{field_name}\" חייב להיות קטן/שווה ל {min_field_name}.";

            bool condition = value <= min_value;

            return new CheckField(
                succeeded: condition,
                message: condition ? string.Empty : ErrorMessage
                );
        }

        public static CheckField NotEq<T>(T value, T unwanted_value, string field_name)
        {
            string ErrorMessage = $"השדה \"{field_name}\" חייב להיות שונה מ{unwanted_value}.";

            bool condition = (value is null) ? unwanted_value != null : !value.Equals(unwanted_value);

            return new CheckField(
                succeeded: condition,
                message: condition ? string.Empty : ErrorMessage
                );
        }

        public static CheckField Between(DateTime value, DateTime min_value, DateTime max_value, string field_name)
        {
            string ErrorMessage = $"השדה \"{field_name}\" חייב להיות בטווח {min_value} - {max_value}.";

            bool condition = value >= min_value && value < max_value;

            return new CheckField(
                succeeded: condition,
                message: condition ? string.Empty : ErrorMessage
                );
        }

        public static CheckField Required<T>(T value, string field_name)
        {
            string ErrorMessage = $"השדה \"{field_name}\" הוא חובה.";

            bool condition = !string.IsNullOrEmpty(value?.ToString());

            return new CheckField(
                succeeded: condition,
                message: condition ? string.Empty : ErrorMessage
                );
        }

        public static CheckField FullList<T>(List<T> value, string field_name)
        {
            string ErrorMessage = $"השדה \"{field_name}\" הוא חובה. יש להוסיף לפחות איבר אחד לרשימה.";

            bool condition = value != null && value.Count > 0;

            return new CheckField(
                succeeded: condition,
                message: condition ? string.Empty : ErrorMessage
                );
        }

        public static CheckField ListItems<T>(List<T> value, string field_name)
        {
            bool condition = true;
            string ErrorMessage = $"שגיאה בשדה \"{field_name}\".";

            // Get the type of the items in the list
            Type type = typeof(T);

            // Find the Check method in the item's type
            MethodInfo checkMethod = type.GetMethod("Check");

            if (checkMethod == null)
            {
                condition = false;
                ErrorMessage = "שגיאת מערכת";
            }
            else
            {
                foreach (var item in value)
                {
                    // Invoke the Check method and get the result
                    Tuple<bool,string> result = (Tuple<bool, string>)checkMethod.Invoke(item, null);
                    if (!result.Item1)
                    {
                        condition = false;
                        ErrorMessage += " " + result.Item2;
                    }
                }
            }

            return new CheckField(
                succeeded: condition,
                message: condition ? string.Empty : ErrorMessage
                );
        }

        public static CheckField Distinct<T>(List<T> value, string field_name)
        {
            string ErrorMessage = $"ישנה כפילות בשדה \"{field_name}\".";

            bool condition = value.Distinct().Count() == value.Count;

            return new CheckField(
                succeeded: condition,
                message: condition ? string.Empty : ErrorMessage
                );
        }

        public static CheckField PropertyTypeValueCheck(PropertyType propType, string? value, string field_name)
        {
            CheckField result = new();

            if (value != null)
            {
                if (propType == PropertyType.Number)
                {
                    if (!decimal.TryParse(value, out _))
                    {
                        (result.Succeeded, result.Message) = Tuple.Create(false, $"תכונה {field_name} היא מספרית. הערך שהוזן אינו תואם לכך.");
                    }
                }
                else if (propType == PropertyType.Boolean)
                {
                    if (!bool.TryParse(value, out _))
                    {
                        (result.Succeeded, result.Message) = Tuple.Create(false, $"תכונה {field_name} היא בוליאנית. הערך שהוזן אינו תואם לכך.");
                    }
                }
            }

            return result;
        }
    }

    public class CheckClass
    {
        public List<CheckField> Fields { get; set; } = new List<CheckField>();

        public Tuple<bool, string> Check()
        {
            bool result = Fields.All(x => x.Succeeded);
            string message = (result) ? string.Empty : Fields.Where(x => !x.Succeeded).First().Message;
            return Tuple.Create(result, message);
        }
    }
}
