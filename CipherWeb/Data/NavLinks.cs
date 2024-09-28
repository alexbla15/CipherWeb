using CipherData.Models;
using System.Collections.Generic;

namespace CipherWeb.Data
{
    public class CipherNavLinks
    {
        public static readonly MyNavLink Home = new() { Href = "", Icon = Icons.Home.home, Name = "מסך הבית" };
        
        public static readonly MySubNavLink AddPackage = new() { Href = $"{nameof(Forms)}/{nameof(AddPackage)}", Name = "תעודה חדשה", Icon = Icons.Symbols.Plus.add_circle_outline };
        public static readonly MySubNavLink TransferAmount = new() { Href = $"{nameof(Forms)}/{nameof(TransferAmount)}", Name = "העברת כמות", Icon = Icons.Cipher.Transfer };
        public static readonly MySubNavLink Relocation = new() { Href = $"{nameof(Forms)}/{nameof(Relocation)}", Name = "העברת מיקום", Icon = Icons.Cipher.Location };
        public static readonly MySubNavLink AddCategory = new() { Href = $"{nameof(Forms)}/{nameof(AddCategory)}", Name = "קטגוריה חדשה", Icon = Icons.Symbols.category };
        public static readonly MySubNavLink AddProcessDefinition = new() { Href = $"{nameof(Forms)}/{nameof(AddProcessDefinition)}", Name = "תהליך חדש", Icon = Icons.Cipher.Process };
        public static readonly MySubNavLink AddVessel = new() { Href = $"{nameof(Forms)}/{nameof(AddVessel)}", Name = "כלי חדש", Icon = Icons.Cipher.Vessel };
        public static readonly MySubNavLink AddStorageSystem = new() { Href = $"{nameof(Forms)}/{nameof(AddStorageSystem)}", Name = "מערכת חדשה", Icon = Icons.Documents.inventory };
        public static readonly MySubNavLink AddUnit = new() { Href = $"{nameof(Forms)}/{nameof(AddUnit)}", Name = "יחידה חדשה", Icon = Icons.Cipher.Unit };
        public static readonly MyNavLink Forms = new()
        {
            Href = nameof(Forms),
            Icon = Icons.Documents.Edit.edit,
            Name = "הזנה",
            SubLinks = new()
                { AddPackage, TransferAmount, Relocation,
                AddCategory, AddProcessDefinition, AddVessel, AddStorageSystem, AddUnit }
        };

        public static readonly MySubNavLink UpdateCategory = new() { Href = $"{nameof(Forms)}/{nameof(Updates)}/Category", Name = "קטגוריה", Icon = Icons.Symbols.category };
        public static readonly MySubNavLink UpdatePackage = new() { Href = $"{nameof(Forms)}/{nameof(Updates)}/Package", Name = "תעודה", Icon = Icons.Cipher.Package };
        public static readonly MySubNavLink UpdateUnit = new() { Href = $"{nameof(Forms)}/{nameof(Updates)}/Unit", Name = "יחידה", Icon = Icons.Cipher.Unit };
        public static readonly MySubNavLink UpdateVessel = new() { Href = $"{nameof(Forms)}/{nameof(Updates)}/Vessel", Name = "כלי", Icon = Icons.Cipher.Vessel };
        public static readonly MySubNavLink UpdateSystem = new() { Href = $"{nameof(Forms)}/{nameof(Updates)}/System", Name = "מערכת", Icon = Icons.Cipher.Location };
        public static readonly MySubNavLink UpdateProcess = new() { Href = $"{nameof(Forms)}/{nameof(Updates)}/Process", Name = "תהליך", Icon = Icons.Cipher.Process };

        public static readonly MyNavLink Updates = new()
        {
            Href = $"{nameof(Forms)}/{nameof(Updates)}",
            Icon = Icons.Design.rebase_edit,
            Name = "עריכת נתונים",
            SubLinks = new() { UpdateCategory, UpdatePackage, UpdateUnit, UpdateVessel, UpdateSystem, UpdateProcess}
        };

        public static readonly MyNavLink Approval = new() { Href = "Approval", Icon = Icons.Symbols.V.done, Name = "אישור תנועות" };

        public static readonly MySubNavLink CustomReport = new() { Href = $"{nameof(Reports)}/{nameof(CustomReport)}", Name = "יצירת דוח", Icon = Icons.Documents.Edit.edit};
        public static readonly MySubNavLink PackagesReport = new() { Href = $"{nameof(Reports)}/Packages", Name = "תעודות", Icon = Icons.Cipher.Package };
        public static readonly MySubNavLink CategoriesReport = new() { Href = $"{nameof(Reports)}/Categories", Name = "סוגים", Icon = Icons.Symbols.category };
        public static readonly MySubNavLink EventsReport = new() { Href = $"{nameof(Reports)}/Events", Name = "תנועות", Icon = Icons.Data.device_hub };
        public static readonly MySubNavLink DepartmentsReport = new() { Href = $"{nameof(Reports)}/Departments", Name = "תחומים", Icon = Icons.Cipher.Department };
        public static readonly MySubNavLink UnitsReport = new() { Href = $"{nameof(Reports)}/Units", Name = "יחידות", Icon = Icons.Cipher.Unit };
        public static readonly MySubNavLink LocationsReport = new() { Href = $"{nameof(Reports)}/Locations", Name = "מיקומים", Icon = Icons.Cipher.Location };
        public static readonly MySubNavLink LinesReport = new() { Href = $"{nameof(Reports)}/Lines", Name = "קוים", Icon = Icons.Cipher.Line };
        public static readonly MySubNavLink ProcessesReport = new() { Href = $"{nameof(Reports)}/ProcessSummary", Name = "תהליכים", Icon = Icons.Cipher.Process };
        public static readonly MyNavLink Reports = new()
        {
            Href = nameof(Reports),
            Icon = Icons.Cipher.Summary,
            Name = "דוחות",
            SubLinks = new()
                { CustomReport, PackagesReport, CategoriesReport, EventsReport, DepartmentsReport, UnitsReport, LocationsReport, LinesReport, ProcessesReport }
        };

        public static readonly MySubNavLink FreeSearch = new() { Href = $"{nameof(Searches)}/{nameof(FreeSearch)}", Name = "טקסט חופשי", Icon = Icons.Cipher.Id };
        public static readonly MySubNavLink AdvancedSearch = new() { Href = $"{nameof(Searches)}/Advanced", Name = "מתקדם", Icon = Icons.Cipher.Advanced };
        public static readonly MySubNavLink CategorySearch = new() { Href = $"{nameof(Searches)}/Category", Name = "קטגוריה", Icon = Icons.Symbols.category };
        public static readonly MySubNavLink PackageSearch = new() { Href = $"{nameof(Searches)}/Package", Name = "תעודה", Icon = Icons.Cipher.Package };
        public static readonly MySubNavLink VesselSearch = new() { Href = $"{nameof(Searches)}/Vessel", Name = "כלי", Icon = Icons.Cipher.Vessel };
        public static readonly MySubNavLink SystemSearch = new() { Href = $"{nameof(Searches)}/System", Name = "מיקום", Icon = Icons.Cipher.Location };
        public static readonly MySubNavLink UnitSearch = new() { Href = $"{nameof(Searches)}/Unit", Name = "יחידה", Icon = Icons.Cipher.Unit };
        public static readonly MySubNavLink ProcessSearch = new() { Href = $"{nameof(Searches)}/Process", Name = "תהליך", Icon = Icons.Cipher.Process };
        public static readonly MyNavLink Searches = new()
        {
            Href = nameof(Searches),
            Icon = Icons.SearchAndFilter.search,
            Name = "חיפוש",
            SubLinks = new()
                { FreeSearch, AdvancedSearch, CategorySearch, PackageSearch, VesselSearch, SystemSearch, UnitSearch, ProcessSearch }
        };

        public static readonly MyNavLink Information = new() { Href = nameof(Information), Icon = Icons.Communication.Chat.question_answer, Name = "הסברים ותמיכה" };

        public static readonly List<MyNavLink> links = new() { Home, Forms, Updates, Approval, Reports, Searches, Information };
    }
}
