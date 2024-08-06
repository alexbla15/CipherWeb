using CipherData.Models;
using Radzen;

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

        public static string decimalFormat()
        {
            string format = "0.";
            for (int i = 0; i < decimalFigures; i++)
            {
                format += "0";
            }
            format += "; 0.";
            for (int i = 0; i < decimalFigures; i++)
            {
                format += "0";
            }

            return format + "-";
        }


        public class CipherUser
        {
            public string FirstName { get; set; }
            public string SurName { get; set; }

            public string FullName()
            {
                return FirstName + " " + SurName;
            }
        }

        public static CipherUser SetUser = new CipherUser() { FirstName = "אלכס", SurName = "בלחמן" };

        public static string CompanyName = "היחידה להנדסת תהליך";
        public static string DeveloperNames = "אלכס בלחמן ושחר פייט";

        public static int RowsPerPage = 12;

        public static DialogOptions SetDialogOptions = new DialogOptions() { Width = "800px", Height = "500px", Resizable = true, Draggable = true };

        public static List<string> Lines = new List<string> { "קו א", "קו ב", "קו ג" };
        public static List<string> Processes = new List<string> { "תהליך א", "תהליך ב", "תהליך ג" };
        public static List<string> Reactants = new List<string> { "מגיב א", "מגיב ב", "מגיב ג" };
        public static List<string> Products = new List<string> { "תוצר א", "תוצר ב", "תוצר ג" };


        public static List<Vessel> vessels = new List<Vessel>
    {
        new Vessel { Name = "V1", Id=1110},
        new Vessel { Name = "V2", Id=1112 } ,
        new Vessel { Name = "V3", Id=1113 }
    };

        public static List<StorageSystem> systems = new List<StorageSystem>
    {
        new StorageSystem { Name = "A", Id=1110 },
        new StorageSystem { Name = "B", Id=1111} ,
        new StorageSystem { Name = "C", Id=1112},
        new StorageSystem { Name = "D", Id=1113 },
        new StorageSystem { Name = "E", Id=1114 }
    };
        public static List<Package> packages = new List<Package> 
        { 
            new Package { Id=1, SerialNumber = "111.1", BrutMass=10M, NetMass=9M, Vessel="V1", Location="A"}, 
            new Package { Id=2, SerialNumber = "111.2", BrutMass=10.5M, NetMass=0.5M, Vessel="V1", Location="A" } ,
            new Package { Id=3, SerialNumber = "321.3", BrutMass=9.5M, NetMass=0.5M , Vessel = "V2", Location="A"},
            new Package { Id=4, SerialNumber = "145.4", BrutMass=0.5M, NetMass=0.5M , Vessel = "V3", Location="A"},
            new Package { Id=5, SerialNumber = "987.6", BrutMass=0.5M, NetMass=0.5M , Vessel = "V4", Location="B"},
            new Package { Id=6, SerialNumber = "1234.2", BrutMass=10.5M, NetMass=10M, Vessel="V4", Location="C" }
        };


        public static List<char> ImproperChars = 
            new List<char>() { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '[', ']', '_', '<', '>', '?', '/', '\\', '|', '{', '}', '~', ':' };
    }
}
