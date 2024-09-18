using CipherData.Models;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
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

        public static CheckField ProperChars(string value, string field_name, string AllowedRegex = "^[a-zA-Z0-9א-ת. \n?]+$")
        {
            Regex regex = new(AllowedRegex, RegexOptions.IgnoreCase);
            var invalidChars = value.Where(c => !regex.IsMatch(c.ToString())).ToList();

            return invalidChars.Any() ? new CheckField(false, $"השדה \"{field_name}\" מכיל תו אסור - {invalidChars.First()}") : new CheckField();
        }

        public static CheckField ProperWords(string value, string field_name)
        {
            CheckField result = new();

            string[] UnallowedWords = { "SELECT", "INSERT", "UPDATE", "DELETE", "PUT", "POST", "GET" };

            result.Succeeded = !UnallowedWords.Any(x => value.ToUpper().Contains(x));

            if (!result.Succeeded)
            {
                result.Message = $"השדה \"{field_name}\" מכיל מילה אסורה - {UnallowedWords.Where(x => value.ToUpper().Contains(x)).First()}";
            }

            return result;
        }

        public static CheckField CheckString(string value, string field_name, string AllowedRegex = "^[a-zA-Z0-9א-ת., \n?]+$")
        {
            CheckField result = ProperChars(value, field_name, AllowedRegex);
            return (result.Succeeded)? ProperWords(value, field_name) : result;
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
            CheckField result = (typeof(T) == typeof(string)) ? CheckString(value.ToString(), field_name) : new();

            if (result.Succeeded)
            {
                string ErrorMessage = $"השדה \"{field_name}\" חייב להיות שונה מ{unwanted_value}.";
                bool condition = value is null;

                result.Succeeded = condition ? unwanted_value != null : !value.Equals(unwanted_value);
                result.Message = condition ? string.Empty : ErrorMessage;
            }

            return result;
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

        public static CheckField Required<T>(T value, string field_name, string AllowedRegex = "^[a-zA-Z0-9א-ת., \n?]+$")
        {
            CheckField result = new(); 
            
            string ErrorMessage = $"השדה \"{field_name}\" הוא חובה.";

            bool condition = !string.IsNullOrEmpty(value?.ToString());

            result.Succeeded = condition;
            result.Message = condition ? string.Empty : ErrorMessage;

            if (result.Succeeded)
            {
                result = (typeof(T) == typeof(string)) ? CheckString(value.ToString(), field_name, AllowedRegex) : result;
            }

            return result;
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
            string ErrorMessage = $"שגיאה בשדה \"{field_name}\".";

            // Get the type of the items in the list
            Type type = typeof(T);

            CheckField result = (type == typeof(string)) ? CheckString(value.ToString(), field_name) : new();

            if (result.Succeeded)
            {
                // Find the Check method in the item's type
                MethodInfo checkMethod = type.GetMethod("Check");

                if (checkMethod == null)
                {
                    result.Succeeded = false;
                    result.Message = "שגיאת מערכת";
                }
                else
                {
                    foreach (var item in value)
                    {
                        // Invoke the Check method and get the result
                        Tuple<bool, string> resultItem = (Tuple<bool, string>)checkMethod.Invoke(item, null);
                        if (!resultItem.Item1)
                        {
                            result.Succeeded = false;
                            result.Message =  ErrorMessage + " " + resultItem.Item2;
                        }
                    }
                }
            }

            return result;
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
