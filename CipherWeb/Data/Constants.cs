using CipherData.Models;
using Radzen;
using System.Collections.Generic;

namespace CipherWeb.Data
{
    public class Constants
    {
        public static Language SetLanguage = Language.English;

        public class Button
        {
            public static ButtonStyle Color = ButtonStyle.Primary;
            public static Shade Shade = Shade.Darker;
        }

        public class Card
        {
            public static Variant Variant = Variant.Filled;
        }

        // STYLE
        public static string textStyle = "width: 400px; text-align:right";

        // VALUES
        public static int decimalFigures = 2;

        public static readonly Worker SetUser = new("אלכס בלחמן");

        public static readonly string CompanyName = "היחידה להנדסת תהליך";
        public static readonly string DeveloperNames = "אלכס בלחמן ושחר פייט";

        public static int RowsPerPage = 12;

        public static readonly DialogOptions SetDialogOptions = new() { Width = "800px", Height = "500px", Resizable = true, Draggable = true };

        public static readonly List<string> Lines = new() { "קו א", "קו ב", "קו ג" };
        public static readonly List<string> Processes = new() { "תהליך א", "תהליך ב", "תהליך ג" };
        public static readonly List<string> Reactants = new() { "מגיב א", "מגיב ב", "מגיב ג" };
        public static readonly List<string> Products = new() { "תוצר א", "תוצר ב", "תוצר ג" };

        public static readonly StorageSystem sysA = new() { Description = "A", Uuid = 1110 };
        public static readonly StorageSystem sysB = new() { Description = "B", Uuid = 1111 };
        public static readonly StorageSystem sysC = new() { Description = "C", Uuid = 1112 };
        public static readonly StorageSystem sysD = new() { Description = "D", Uuid = 1113 };
        public static readonly StorageSystem sysE = new() { Description = "E", Uuid = 1114 };

        public static readonly List<StorageSystem> systems = new()
    {
            sysA, sysB, sysC, sysD, sysE
    };


        public static readonly List<char> ImproperChars =
            new () { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '[', ']', '_', '<', '>', '?', '/', '\\', '|', '{', '}', '~', ':' };
    }
}
