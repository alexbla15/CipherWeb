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


        public static readonly List<char> ImproperChars =
            new () { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '[', ']', '_', '<', '>', '?', '/', '\\', '|', '{', '}', '~', ':' };
    }
}
