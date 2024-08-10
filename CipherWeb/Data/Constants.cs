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
        new Vessel { Id = "V1", Uuid=1110},
        new Vessel { Id = "V2", Uuid=1112 } ,
        new Vessel { Id = "V3", Uuid=1113 },
        new Vessel { Id = "V4", Uuid=1114 }
    };

        public static StorageSystem sysA = new StorageSystem { Description = "A", Uuid = 1110 };
        public static StorageSystem sysB = new StorageSystem { Description = "B", Uuid = 1111 };
        public static StorageSystem sysC = new StorageSystem { Description = "C", Uuid = 1112 };
        public static StorageSystem sysD = new StorageSystem { Description = "D", Uuid = 1113 };
        public static StorageSystem sysE = new StorageSystem { Description = "E", Uuid = 1114 };

        public static List<StorageSystem> systems = new List<StorageSystem>
    {
            sysA, sysB, sysC, sysD, sysE
    };
        public static List<Package> packages = new List<Package>
        {
            new Package { Uuid=1, Id = "111.1", BrutMass=10M, NetMass=9M, Vessel=vessels[0], System=sysA},
            new Package { Uuid=2, Id = "111.2", BrutMass=10.5M, NetMass=0.5M, Vessel=vessels[0], System=sysA } ,
            new Package { Uuid=3, Id = "321.3", BrutMass=9.5M, NetMass=0.5M , Vessel = vessels[1], System=sysA},
            new Package { Uuid=4, Id = "145.4", BrutMass=0.5M, NetMass=0.5M , Vessel = vessels[2], System=sysA},
            new Package { Uuid=5, Id = "987.6", BrutMass=0.5M, NetMass=0.5M , Vessel = vessels[3], System=sysA},
            new Package { Uuid=6, Id = "1234.2", BrutMass=10.5M, NetMass=10M, Vessel=vessels[3], System=sysA }
        };


        public static List<char> ImproperChars =
            new List<char>() { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '[', ']', '_', '<', '>', '?', '/', '\\', '|', '{', '}', '~', ':' };
    }
}
