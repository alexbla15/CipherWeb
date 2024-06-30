using CipherData.Models;

namespace CipherWeb.Data
{
    public class CipherNavLinks
    {
        public static List<MyNavLink> links = new()
        {
            new MyNavLink() {Href="", Icon=Icons.Home, Name="מסך הבית", SubLinks=new List<MySubNavLink>()},
            new MyNavLink() {Href="Personal", Icon=Icons.Account, Name="אזור אישי", SubLinks=new List<MySubNavLink>()},

            new MyNavLink() {Href="Forms", Icon=Icons.Edit, Name="הזנה", 
                SubLinks=new List<MySubNavLink>()
                {
                    new MySubNavLink() {Href="Forms/AddPackage", Name="תעודה חדשה"},
                    new MySubNavLink() {Href="Forms/TransferAmount", Name="העברת כמות"},
                    new MySubNavLink() {Href="Forms/Relocation", Name="העברת מיקום"},
                    new MySubNavLink() {Href="Forms/UpdatePackage", Name="עדכון נתונים"},
                    new MySubNavLink() {Href="Forms/Analysis", Name="מדידה"}
                }
            },
            new MyNavLink() {Href="Approval", Icon=Icons.Done, Name="אישור", SubLinks=new List<MySubNavLink>()},
            new MyNavLink() {Href="Reports", Icon=Icons.Summary, Name="דוחות", 
                SubLinks=new List<MySubNavLink>()
                {
                    new MySubNavLink() {Href="Reports/Packages", Name="תעודות"},
                    new MySubNavLink() {Href="Reports/Categories", Name="סוגים"},
                    new MySubNavLink() {Href="Reports/Events", Name="תנועות"},
                    new MySubNavLink() {Href="Reports/Departments", Name="תחומים"},
                    new MySubNavLink() {Href="Reports/Units", Name="יחידות"},
                    new MySubNavLink() {Href="Reports/Locations", Name="מיקומים"},
                    new MySubNavLink() {Href="Reports/Assignments", Name="משימות"},
                    new MySubNavLink() {Href="Reports/Processes", Name="תהליכים"},
                    new MySubNavLink() {Href="Reports/Categories", Name="קטגוריות"}
                }
            },
            new MyNavLink() {Href="Search", Icon=Icons.Search, Name="חיפוש", 
                SubLinks=new List<MySubNavLink>()
                {
                    new MySubNavLink() {Href="Search/Id", Name="חיפוש לפי מספר סידורי"},
                    new MySubNavLink() {Href="Search/Advanced", Name="חיפוש מתקדם"},
                    new MySubNavLink() {Href="Search/Package", Name="תעודה"},
                    new MySubNavLink() {Href="Search/Vessel", Name="כלי"},
                    new MySubNavLink() {Href="Search/Department", Name="תחום"},
                    new MySubNavLink() {Href="Search/System", Name="מיקום"}
                }
            },

            new MyNavLink() {Href="/Reports/Schedule", Icon=Icons.Calendar, Name="גאנט", SubLinks=new List<MySubNavLink>()},
            new MyNavLink() {Href="Information", Icon=Icons.QA, Name="הסברים ותמיכה", SubLinks=new List<MySubNavLink>()}
        };
    }
}
