using Radzen;

namespace CipherWeb.Data
{
    public class Constants
    {
        public class Button
        {
            public static ButtonStyle Color = ButtonStyle.Primary;
            public static Shade Shade = Shade.Darker;
        }

        public class Card
        {
            public static Variant Variant = Variant.Filled;
        }

        // VALUES
        public static int decimalFigures = 2;

        // STYLE
        public static string textStyle = "width: 400px; text-align:right";

        public static string decimalFormat()
        {
            string format = "0.";
            for (int i = 0; i < decimalFigures; i++)  {
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
        
        public static CipherUser SetUser = new CipherUser() { FirstName="אלכס", SurName="בלחמן"};

        public static string CompanyName = "היחידה להנדסת תהליך";
        public static string DeveloperNames = "אלכס בלחמן ושחר פייט";
    }
}
