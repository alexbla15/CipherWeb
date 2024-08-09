using CipherData.Models;

namespace CipherWeb.Data
{
    public class CipherNavLinks
    {
        public static MyNavLink Home = new MyNavLink() { Href = "", Icon = Icons.Home, Name = "מסך הבית", SubLinks = new List<MySubNavLink>() };
        public static MyNavLink Personal = new MyNavLink() { Href = "Personal", Icon = Icons.Account, Name = "אזור אישי", SubLinks = new List<MySubNavLink>() };

        public static MySubNavLink AddPackage = new MySubNavLink() { Href = "Forms/AddPackage", Name = "תעודה חדשה", Icon = Icons.Add };
        public static MySubNavLink TransferAmount = new MySubNavLink() { Href = "Forms/TransferAmount", Name = "העברת כמות", Icon = Icons.Transfer };
        public static MySubNavLink Relocation = new MySubNavLink() { Href = "Forms/Relocation", Name = "העברת מיקום", Icon = Icons.Location };
        public static MySubNavLink UpdatePackage = new MySubNavLink() { Href = "Forms/UpdatePackage", Name = "עדכון נתונים", Icon = Icons.Refresh };
        public static MySubNavLink Analysis = new MySubNavLink() { Href = "Forms/Analysis", Name = "מדידה", Icon = Icons.Analysis };
        public static MySubNavLink AddCategory = new MySubNavLink() { Href = "Forms/AddCategory", Name = "קטגוריה חדשה", Icon = Icons.Category };
        public static MySubNavLink AddProcess = new MySubNavLink() { Href = "Forms/AddProcess", Name = "תהליך חדש", Icon = Icons.Process };
        public static MySubNavLink AddVessel = new MySubNavLink() { Href = "Forms/AddVessel", Name = "כלי חדש", Icon = Icons.Vessel };
        public static MySubNavLink AddStorageSystem = new MySubNavLink() { Href = "Forms/AddStorageSystem", Name = "מערכת חדשה", Icon = Icons.Inventory };
        public static MySubNavLink AddDepartment = new MySubNavLink() { Href = "Forms/AddDepartment", Name = "תחום חדש", Icon = Icons.Department };
        public static MySubNavLink AddLocation = new MySubNavLink() { Href = "Forms/AddLocation", Name = "מבנה חדש", Icon = Icons.Home };
        public static MyNavLink Forms = new MyNavLink()
        {
            Href = "Forms",
            Icon = Icons.Edit,
            Name = "הזנה",
            SubLinks = new List<MySubNavLink>()
                { AddPackage, TransferAmount, Relocation, UpdatePackage, Analysis, 
                AddCategory, AddProcess, AddVessel, AddStorageSystem, AddDepartment, AddLocation }
        };

        public static MyNavLink Approval = new MyNavLink() { Href = "Approval", Icon = Icons.Done, Name = "אישור", SubLinks = new List<MySubNavLink>() };

        public static MySubNavLink CustomReport = new MySubNavLink() { Href = "Reports/CustomReport", Name = "יצירת דוח", Icon = Icons.Edit};
        public static MySubNavLink PackagesReport = new MySubNavLink() { Href = "Reports/Packages", Name = "תעודות", Icon = Icons.Package };
        public static MySubNavLink CategoriesReport = new MySubNavLink() { Href = "Reports/Categories", Name = "סוגים", Icon = Icons.Category };
        public static MySubNavLink EventsReport = new MySubNavLink() { Href = "Reports/Events", Name = "תנועות", Icon = Icons.DeviceHub };
        public static MySubNavLink DepartmentsReport = new MySubNavLink() { Href = "Reports/Departments", Name = "תחומים", Icon = Icons.Department };
        public static MySubNavLink UnitsReport = new MySubNavLink() { Href = "Reports/Units", Name = "יחידות", Icon = Icons.Unit };
        public static MySubNavLink LocationsReport = new MySubNavLink() { Href = "Reports/Locations", Name = "מיקומים", Icon = Icons.Location };
        public static MySubNavLink LinesReport = new MySubNavLink() { Href = "Reports/Lines", Name = "קוים", Icon = Icons.Line };
        public static MySubNavLink ProcessesReport = new MySubNavLink() { Href = "/Reports/ProcessSummary", Name = "תהליכים", Icon = Icons.Process };
        public static MyNavLink Reports = new MyNavLink()
        {
            Href = "Reports",
            Icon = Icons.Summary,
            Name = "דוחות",
            SubLinks = new List<MySubNavLink>()
                { CustomReport, PackagesReport, CategoriesReport, EventsReport, DepartmentsReport, UnitsReport, LocationsReport, LinesReport, ProcessesReport }
        };

        public static MySubNavLink FreeSearch = new MySubNavLink() { Href = "Search/FreeSearch", Name = "טקסט חופשי", Icon = Icons.Id };
        public static MySubNavLink AdvancedSearch = new MySubNavLink() { Href = "Search/Advanced", Name = "מתקדם", Icon = Icons.Advanced };
        public static MySubNavLink PackageSearch = new MySubNavLink() { Href = "Search/Package", Name = "תעודה", Icon = Icons.Package };
        public static MySubNavLink VesselSearch = new MySubNavLink() { Href = "Search/Vessel", Name = "כלי", Icon = Icons.Vessel };
        public static MySubNavLink DepartmentSearch = new MySubNavLink() { Href = "Search/Department", Name = "תחום", Icon = Icons.Department };
        public static MySubNavLink LocationSearch = new MySubNavLink() { Href = "Search/System", Name = "מיקום", Icon = Icons.Location };
        public static MyNavLink Search = new MyNavLink()
        {
            Href = "Searches",
            Icon = Icons.Search,
            Name = "חיפוש",
            SubLinks = new List<MySubNavLink>()
                { FreeSearch, AdvancedSearch, PackageSearch, VesselSearch, DepartmentSearch, LocationSearch }
        };

        public static MyNavLink Information = new MyNavLink() { Href = "Information", Icon = Icons.QA, Name = "הסברים ותמיכה", SubLinks = new List<MySubNavLink>() };

        public static List<MyNavLink> links = new()
        {
            Home, Personal,Forms, Approval, Reports, Search, Information
        };
    }
}
