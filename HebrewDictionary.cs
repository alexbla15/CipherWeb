using CipherData.Models;
using static CipherWeb.Pages.Forms.AddPackage;

namespace CipherWeb
{
    public class HebrewDictionary
    {
        public static List<Tuple<string, string>> Headers = new List<Tuple<string, string>> ()
        {
            // EVENTS
            new("Id", "#"),
            new("Numerator", "מספר סידורי"),
            new("Type", "סוג"),
            new("Process", "תהליך"),
            new("Packages", "חבילות"),
            new("Parameters", "פרמטרים"),
            new("InitialValue", "ערך התחלתי"),
            new("FinalValue", "ערך סופי"),
            new("UpdatingWorker", "שם מזין"),
            new("AuthorizingWorker", "שם מאשר"),
            new("UpdatingDate", "תאריך עדכון"),
            new("ApprovingDate", "תאריך אישור"),
            new("EventDate", "תאריך תנועה"),
            new("Comments", "הערות"),

            // PACKAGES
            new("SerialNumber", "מספר סידורי"),
            new("SubCategory", "תת-קטגוריה"),
            new("OpenDate", "תאריך פתיחה"),
            new("Description", "תיאור") ,
            new("Destination", "הקצאה") ,
            new("Category", "קטגוריה"),
            new("Vessel", "כלי"),
            new("BrutMass", "מסה ברוטו"),
            new("NetMass", "מסה נטו"),


            // SUB-CATEGORIES
            new("Material", "חומר"),
            new("Name", "שם"),
            new("MainCategory", "קטגוריה"),
            new("Mask", "תבנית"),
            new("MaterialType", "סוג החומר"),
            new("Properties", "תכונות"),
            new("InProcesses", "תהליכי כניסה"),
            new("OutProcesses", "תהליכי יציאה"),
            new("UpdateDate", "תאריך עדכון"),

            // ANALYSIS
            new("Value", "ערך"),

            // OTHERS
            new ("Month", "חודש"),
            new ("Mass", "מסה"),
            new ("Reagent", "חומר גלם"),
            new ("Product", "תוצר")
        };
    }
}
