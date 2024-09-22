using CipherData.Models;
using Radzen;

namespace CipherWeb.Data
{
    public class Constants
    {
        public class Button
        {
            public static readonly ButtonStyle Color = ButtonStyle.Primary;
            public static readonly Shade Shade = Shade.Darker;
        }

        public static readonly bool CheckJsons = true;

        public static readonly Worker SetUser = new() { Name = "אלכס בלחמן" };

        public static readonly string CompanyName = "היחידה להנדסת תהליך";
        public static readonly string DeveloperNames = "אלכס בלחמן ושחר פייט";

        public static readonly int RowsPerPage = 12;

        public static readonly DialogOptions SetDialogOptions = new() { Width = "800px", Height = "500px", Resizable = true, Draggable = true };

        public static readonly List<string> Lines = new() { "קו א", "קו ב", "קו ג" };
        public static readonly List<string> Processes = new() { "תהליך א", "תהליך ב", "תהליך ג" };
        public static readonly List<string> Reactants = new() { "מגיב א", "מגיב ב", "מגיב ג" };
        public static readonly List<string> Products = new() { "תוצר א", "תוצר ב", "תוצר ג" };

        public static readonly List<char> ImproperChars =
            new () { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '[', ']', '_', '<', '>', '?', '/', '\\', '|', '{', '}', '~', ':' };

        public static readonly string[] UnallowedWords = { "SELECT", "INSERT", "UPDATE", "DELETE", "PUT", "POST", "GET" };
    }
}
