using CipherData.Models;

namespace CipherWeb.Data
{
    public class CipherNavLinks
    {
        public static MyNavLink Home = new() { Href = "", Icon = Icons.Home._Home, Name = "מסך הבית", SubLinks = new() };
        public static MyNavLink Personal = new() { Href = "Personal", Icon = Icons.Social.SinglePerson.AccountCircle, Name = "אזור אישי", SubLinks = new List<MySubNavLink>() };

        public static MySubNavLink AddPackage = new() { Href = "Forms/AddPackage", Name = "תעודה חדשה", Icon = Icons.Symbols.Plus.AddCircleOutline };
        public static MySubNavLink TransferAmount = new() { Href = "Forms/TransferAmount", Name = "העברת כמות", Icon = Icons.Cipher.Transfer };
        public static MySubNavLink Relocation = new() { Href = "Forms/Relocation", Name = "העברת מיקום", Icon = Icons.Cipher.Location };
        public static MySubNavLink UpdatePackage = new() { Href = "Forms/UpdatePackage", Name = "עדכון נתונים", Icon = Icons.Arrows.Rounded.Refresh };
        public static MySubNavLink Analysis = new() { Href = "Forms/Analysis", Name = "מדידה", Icon = Icons.Cipher.Analysis };
        public static MySubNavLink AddCategory = new() { Href = "Forms/AddCategory", Name = "קטגוריה חדשה", Icon = Icons.Symbols.Category };
        public static MySubNavLink AddProcess = new() { Href = "Forms/AddProcess", Name = "תהליך חדש", Icon = Icons.Cipher.Process };
        public static MySubNavLink AddVessel = new() { Href = "Forms/AddVessel", Name = "כלי חדש", Icon = Icons.Cipher.Vessel };
        public static MySubNavLink AddStorageSystem = new() { Href = "Forms/AddStorageSystem", Name = "מערכת חדשה", Icon = Icons.Documents.Inventory };
        public static MySubNavLink AddDepartment = new() { Href = "Forms/AddDepartment", Name = "תחום חדש", Icon = Icons.Cipher.Department };
        public static MySubNavLink AddLocation = new() { Href = "Forms/AddLocation", Name = "מבנה חדש", Icon = Icons.Home._Home };
        public static MyNavLink Forms = new()
        {
            Href = "Forms",
            Icon = Icons.Documents.Edit._Edit,
            Name = "הזנה",
            SubLinks = new()
                { AddPackage, TransferAmount, Relocation, UpdatePackage, Analysis, 
                AddCategory, AddProcess, AddVessel, AddStorageSystem, AddDepartment, AddLocation }
        };

        public static MyNavLink Approval = new() { Href = "Approval", Icon = Icons.Symbols.V.Done, Name = "אישור", SubLinks = new List<MySubNavLink>() };

        public static MySubNavLink CustomReport = new() { Href = "Reports/CustomReport", Name = "יצירת דוח", Icon = Icons.Documents.Edit._Edit};
        public static MySubNavLink PackagesReport = new() { Href = "Reports/Packages", Name = "תעודות", Icon = Icons.Cipher.Package };
        public static MySubNavLink CategoriesReport = new() { Href = "Reports/Categories", Name = "סוגים", Icon = Icons.Symbols.Category };
        public static MySubNavLink EventsReport = new() { Href = "Reports/Events", Name = "תנועות", Icon = Icons.Data.DeviceHub };
        public static MySubNavLink DepartmentsReport = new() { Href = "Reports/Departments", Name = "תחומים", Icon = Icons.Cipher.Department };
        public static MySubNavLink UnitsReport = new() { Href = "Reports/Units", Name = "יחידות", Icon = Icons.Cipher.Unit };
        public static MySubNavLink LocationsReport = new() { Href = "Reports/Locations", Name = "מיקומים", Icon = Icons.Cipher.Location };
        public static MySubNavLink LinesReport = new() { Href = "Reports/Lines", Name = "קוים", Icon = Icons.Cipher.Line };
        public static MySubNavLink ProcessesReport = new() { Href = "/Reports/ProcessSummary", Name = "תהליכים", Icon = Icons.Cipher.Process };
        public static MyNavLink Reports = new()
        {
            Href = "Reports",
            Icon = Icons.Cipher.Summary,
            Name = "דוחות",
            SubLinks = new()
                { CustomReport, PackagesReport, CategoriesReport, EventsReport, DepartmentsReport, UnitsReport, LocationsReport, LinesReport, ProcessesReport }
        };

        public static MySubNavLink FreeSearch = new() { Href = "Search/FreeSearch", Name = "טקסט חופשי", Icon = Icons.Cipher.Id };
        public static MySubNavLink AdvancedSearch = new() { Href = "Search/Advanced", Name = "מתקדם", Icon = Icons.Cipher.Advanced };
        public static MySubNavLink PackageSearch = new() { Href = "Search/Package", Name = "תעודה", Icon = Icons.Cipher.Package };
        public static MySubNavLink VesselSearch = new() { Href = "Search/Vessel", Name = "כלי", Icon = Icons.Cipher.Vessel };
        public static MySubNavLink DepartmentSearch = new() { Href = "Search/Department", Name = "תחום", Icon = Icons.Cipher.Department };
        public static MySubNavLink LocationSearch = new() { Href = "Search/System", Name = "מיקום", Icon = Icons.Cipher.Location };
        public static MyNavLink Search = new()
        {
            Href = "Searches",
            Icon = Icons.SearchAndFilter.Search,
            Name = "חיפוש",
            SubLinks = new()
                { FreeSearch, AdvancedSearch, PackageSearch, VesselSearch, DepartmentSearch, LocationSearch }
        };

        public static MyNavLink Information = new() { Href = "Information", Icon = Icons.Communication.Chat.QA, Name = "הסברים ותמיכה", SubLinks = new() };

        public static List<MyNavLink> links = new()
        {
            Home, Personal,Forms, Approval, Reports, Search, Information
        };
    }
}
