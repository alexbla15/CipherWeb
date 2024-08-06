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
                    new MySubNavLink() {Href="Forms/AddPackage", Name="תעודה חדשה", Icon=Icons.Add},
                    new MySubNavLink() {Href="Forms/TransferAmount", Name="העברת כמות", Icon=Icons.Transfer},
                    new MySubNavLink() {Href="Forms/Relocation", Name="העברת מיקום", Icon=Icons.Location},
                    new MySubNavLink() {Href="Forms/UpdatePackage", Name="עדכון נתונים", Icon=Icons.Refresh},
                    new MySubNavLink() {Href="Forms/Analysis", Name="מדידה", Icon=Icons.Analysis}
                }
            },
            new MyNavLink() {Href="Approval", Icon=Icons.Done, Name="אישור", SubLinks=new List<MySubNavLink>()},
            new MyNavLink() {Href="Reports", Icon=Icons.Summary, Name="דוחות", 
                SubLinks=new List<MySubNavLink>()
                {
                    new MySubNavLink() {Href="Reports/CustomReport", Name="יצירת דוח", Icon=Icons.Edit},
                    new MySubNavLink() {Href="Reports/Packages", Name="תעודות", Icon=Icons.Package},
                    new MySubNavLink() {Href="Reports/Categories", Name="סוגים", Icon=Icons.Category},
                    new MySubNavLink() {Href="Reports/Events", Name="תנועות", Icon=Icons.DeviceHub},
                    new MySubNavLink() {Href="Reports/Departments", Name="תחומים", Icon=Icons.Department},
                    new MySubNavLink() {Href="Reports/Units", Name="יחידות", Icon=Icons.Unit},
                    new MySubNavLink() {Href="Reports/Locations", Name="מיקומים", Icon=Icons.Location},
                    new MySubNavLink() {Href="Reports/Lines", Name="קוים", Icon=Icons.Line},
                    new MySubNavLink() {Href="Reports/Processes", Name="תהליכים", Icon=Icons.Process}
                }
            },
            new MyNavLink() {Href="Search", Icon=Icons.Search, Name="חיפוש", 
                SubLinks=new List<MySubNavLink>()
                {
                    new MySubNavLink() {Href="Search/FreeSearch", Name="טקסט חופשי", Icon=Icons.Id},
                    new MySubNavLink() {Href="Search/Advanced", Name="מתקדם", Icon=Icons.Advanced},
                    new MySubNavLink() {Href="Search/Package", Name="תעודה", Icon=Icons.Package},
                    new MySubNavLink() {Href="Search/Vessel", Name="כלי", Icon=Icons.Vessel},
                    new MySubNavLink() {Href="Search/Department", Name="תחום", Icon=Icons.Department},
                    new MySubNavLink() {Href="Search/System", Name="מיקום", Icon=Icons.Location}
                }
            },
            new MyNavLink() {Href="Information", Icon=Icons.QA, Name="הסברים ותמיכה", SubLinks=new List<MySubNavLink>()}
        };
    }
}
