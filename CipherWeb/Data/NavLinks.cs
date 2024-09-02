using CipherData.Models;
using System.Collections.Generic;

namespace CipherWeb.Data
{
    public class CipherNavLinks
    {
        public static readonly MyNavLink Home = new() { Href = "", Icon = Icons.Home._Home, Name = "מסך הבית", SubLinks = new() };
        public static readonly MyNavLink Personal = new() { Href = "Personal", Icon = Icons.Social.SinglePerson.AccountCircle, Name = "אזור אישי", SubLinks = new List<MySubNavLink>() };

        public static readonly MySubNavLink AddPackage = new() { Href = "Forms/AddPackage", Name = "תעודה חדשה", Icon = Icons.Symbols.Plus.AddCircleOutline };
        public static readonly MySubNavLink TransferAmount = new() { Href = "Forms/TransferAmount", Name = "העברת כמות", Icon = Icons.Cipher.Transfer };
        public static readonly MySubNavLink Relocation = new() { Href = "Forms/Relocation", Name = "העברת מיקום", Icon = Icons.Cipher.Location };
        public static readonly MySubNavLink UpdatePackage = new() { Href = "Forms/UpdatePackage", Name = "עדכון נתונים", Icon = Icons.Arrows.Rounded.Refresh };
        public static readonly MySubNavLink AddCategory = new() { Href = "Forms/AddCategory", Name = "קטגוריה חדשה", Icon = Icons.Symbols.Category };
        public static readonly MySubNavLink AddProcess = new() { Href = "Forms/AddProcess", Name = "תהליך חדש", Icon = Icons.Cipher.Process };
        public static readonly MySubNavLink AddVessel = new() { Href = "Forms/AddVessel", Name = "כלי חדש", Icon = Icons.Cipher.Vessel };
        public static readonly MySubNavLink AddStorageSystem = new() { Href = "Forms/AddSystem", Name = "מערכת חדשה", Icon = Icons.Documents.Inventory };
        public static readonly MyNavLink Forms = new()
        {
            Href = "Forms",
            Icon = Icons.Documents.Edit._Edit,
            Name = "הזנה",
            SubLinks = new()
                { AddPackage, TransferAmount, Relocation, UpdatePackage, 
                AddCategory, AddProcess, AddVessel, AddStorageSystem }
        };

        public static readonly MyNavLink Approval = new() { Href = "Approval", Icon = Icons.Symbols.V.Done, Name = "אישור", SubLinks = new List<MySubNavLink>() };

        public static readonly MySubNavLink CustomReport = new() { Href = "Reports/CustomReport", Name = "יצירת דוח", Icon = Icons.Documents.Edit._Edit};
        public static readonly MySubNavLink PackagesReport = new() { Href = "Reports/Packages", Name = "תעודות", Icon = Icons.Cipher.Package };
        public static readonly MySubNavLink CategoriesReport = new() { Href = "Reports/Categories", Name = "סוגים", Icon = Icons.Symbols.Category };
        public static readonly MySubNavLink EventsReport = new() { Href = "Reports/Events", Name = "תנועות", Icon = Icons.Data.DeviceHub };
        public static readonly MySubNavLink DepartmentsReport = new() { Href = "Reports/Departments", Name = "תחומים", Icon = Icons.Cipher.Department };
        public static readonly MySubNavLink UnitsReport = new() { Href = "Reports/Units", Name = "יחידות", Icon = Icons.Cipher.Unit };
        public static readonly MySubNavLink LocationsReport = new() { Href = "Reports/Locations", Name = "מיקומים", Icon = Icons.Cipher.Location };
        public static readonly MySubNavLink LinesReport = new() { Href = "Reports/Lines", Name = "קוים", Icon = Icons.Cipher.Line };
        public static readonly MySubNavLink ProcessesReport = new() { Href = "/Reports/ProcessSummary", Name = "תהליכים", Icon = Icons.Cipher.Process };
        public static readonly MyNavLink Reports = new()
        {
            Href = "Reports",
            Icon = Icons.Cipher.Summary,
            Name = "דוחות",
            SubLinks = new()
                { CustomReport, PackagesReport, CategoriesReport, EventsReport, DepartmentsReport, UnitsReport, LocationsReport, LinesReport, ProcessesReport }
        };

        public static readonly MySubNavLink FreeSearch = new() { Href = "Search/FreeSearch", Name = "טקסט חופשי", Icon = Icons.Cipher.Id };
        public static readonly MySubNavLink AdvancedSearch = new() { Href = "Search/Advanced", Name = "מתקדם", Icon = Icons.Cipher.Advanced };
        public static readonly MySubNavLink PackageSearch = new() { Href = "Search/Package", Name = "תעודה", Icon = Icons.Cipher.Package };
        public static readonly MySubNavLink VesselSearch = new() { Href = "Search/Vessel", Name = "כלי", Icon = Icons.Cipher.Vessel };
        public static readonly MySubNavLink DepartmentSearch = new() { Href = "Search/Department", Name = "תחום", Icon = Icons.Cipher.Department };
        public static readonly MySubNavLink LocationSearch = new() { Href = "Search/System", Name = "מיקום", Icon = Icons.Cipher.Location };
        public static readonly MyNavLink Search = new()
        {
            Href = "Searches",
            Icon = Icons.SearchAndFilter.Search,
            Name = "חיפוש",
            SubLinks = new()
                { FreeSearch, AdvancedSearch, PackageSearch, VesselSearch, DepartmentSearch, LocationSearch }
        };

        public static readonly MyNavLink Information = new() { Href = "Information", Icon = Icons.Communication.Chat.QA, Name = "הסברים ותמיכה", SubLinks = new() };

        public static readonly List<MyNavLink> links = new()
        {
            Home, Personal,Forms, Approval, Reports, Search, Information
        };
    }
}
