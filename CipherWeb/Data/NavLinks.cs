using CipherData.Models;

namespace CipherWeb.Data
{
    public class CipherNavLinks
    {
        public static readonly CipherNavLink Home = new() { Href = "", Icon = Icons.Home.home, Name = "מסך הבית" };
        
        public static readonly CipherNavLink AddPackage = new() { Href = $"{nameof(Forms)}/{nameof(AddPackage)}", Name = "תעודה חדשה", Icon = Icons.Symbols.Plus.add_circle_outline };
        public static readonly CipherNavLink TransferAmount = new() { Href = $"{nameof(Forms)}/{nameof(TransferAmount)}", Name = "העברת כמות", Icon = Icons.Cipher.Transfer };
        public static readonly CipherNavLink Relocation = new() { Href = $"{nameof(Forms)}/{nameof(Relocation)}", Name = "העברת מיקום", Icon = Icons.Cipher.Location };
        public static readonly CipherNavLink AddCategory = new() { Href = $"{nameof(Forms)}/{nameof(AddCategory)}", Name = "קטגוריה חדשה", Icon = Icons.Symbols.category };
        public static readonly CipherNavLink AddProcessDefinition = new() { Href = $"{nameof(Forms)}/{nameof(AddProcessDefinition)}", Name = "תהליך חדש", Icon = Icons.Cipher.Process };
        public static readonly CipherNavLink AddVessel = new() { Href = $"{nameof(Forms)}/{nameof(AddVessel)}", Name = "כלי חדש", Icon = Icons.Cipher.Vessel };
        public static readonly CipherNavLink AddStorageSystem = new() { Href = $"{nameof(Forms)}/{nameof(AddStorageSystem)}", Name = "מערכת חדשה", Icon = Icons.Documents.inventory };
        public static readonly CipherNavLink AddUnit = new() { Href = $"{nameof(Forms)}/{nameof(AddUnit)}", Name = "יחידה חדשה", Icon = Icons.Cipher.Unit };
        public static readonly CipherNavLink Forms = new()
        {
            Href = nameof(Forms),
            Icon = Icons.Documents.Edit.edit,
            Name = "הזנה",
            SubLinks = new()
                { AddPackage, TransferAmount, Relocation,
                AddCategory, AddProcessDefinition, AddVessel, AddStorageSystem, AddUnit }
        };

        public static readonly CipherNavLink UpdateCategory = new() { Href = $"{nameof(Forms)}/{nameof(Updates)}/Category", Name = "קטגוריה", Icon = Icons.Symbols.category };
        public static readonly CipherNavLink UpdatePackage = new() { Href = $"{nameof(Forms)}/{nameof(Updates)}/Package", Name = "תעודה", Icon = Icons.Cipher.Package };
        public static readonly CipherNavLink UpdateUnit = new() { Href = $"{nameof(Forms)}/{nameof(Updates)}/Unit", Name = "יחידה", Icon = Icons.Cipher.Unit };
        public static readonly CipherNavLink UpdateVessel = new() { Href = $"{nameof(Forms)}/{nameof(Updates)}/Vessel", Name = "כלי", Icon = Icons.Cipher.Vessel };
        public static readonly CipherNavLink UpdateSystem = new() { Href = $"{nameof(Forms)}/{nameof(Updates)}/System", Name = "מערכת", Icon = Icons.Cipher.Location };
        public static readonly CipherNavLink UpdateProcess = new() { Href = $"{nameof(Forms)}/{nameof(Updates)}/Process", Name = "תהליך", Icon = Icons.Cipher.Process };

        public static readonly CipherNavLink Updates = new()
        {
            Href = $"{nameof(Forms)}/{nameof(Updates)}",
            Icon = Icons.Design.rebase_edit,
            Name = "עריכת נתונים",
            SubLinks = new() { UpdateCategory, UpdatePackage, UpdateUnit, UpdateVessel, UpdateSystem, UpdateProcess}
        };

        public static readonly CipherNavLink Approval = new() { Href = "Approval", Icon = Icons.Symbols.V.done, Name = "אישור תנועות" };

        public static readonly CipherNavLink AddReport = new() { Href = $"{nameof(Reports)}/{nameof(AddReport)}", Name = "יצירת דוח", Icon = Icons.Documents.assignment_add};
        public static readonly CipherNavLink UpdateReport = new() { Href = $"{nameof(Reports)}/{nameof(UpdateReport)}", Name = "עריכת דוח", Icon = Icons.Documents.Edit.edit_document};
        public static readonly CipherNavLink PackagesReport = new() { Href = $"{nameof(Reports)}/Packages", Name = "תעודות", Icon = Icons.Cipher.Package };
        public static readonly CipherNavLink CategoriesReport = new() { Href = $"{nameof(Reports)}/Categories", Name = "סוגים", Icon = Icons.Symbols.category };
        public static readonly CipherNavLink EventsReport = new() { Href = $"{nameof(Reports)}/Events", Name = "תנועות", Icon = Icons.Data.device_hub };
        public static readonly CipherNavLink DepartmentsReport = new() { Href = $"{nameof(Reports)}/Departments", Name = "תחומים", Icon = Icons.Cipher.Department };
        public static readonly CipherNavLink UnitsReport = new() { Href = $"{nameof(Reports)}/Units", Name = "יחידות", Icon = Icons.Cipher.Unit };
        public static readonly CipherNavLink LocationsReport = new() { Href = $"{nameof(Reports)}/Locations", Name = "מיקומים", Icon = Icons.Cipher.Location };
        public static readonly CipherNavLink LinesReport = new() { Href = $"{nameof(Reports)}/Lines", Name = "קוים", Icon = Icons.Cipher.Line };
        public static readonly CipherNavLink ProcessesReport = new() { Href = $"{nameof(Reports)}/ProcessSummary", Name = "תהליכים", Icon = Icons.Cipher.Process };
        public static readonly CipherNavLink Reports = new()
        {
            Href = nameof(Reports),
            Icon = Icons.Cipher.Summary,
            Name = "דוחות",
            SubLinks = new()
                { AddReport, UpdateReport, PackagesReport, CategoriesReport, EventsReport, DepartmentsReport, UnitsReport, LocationsReport, LinesReport, ProcessesReport }
        };

        public static readonly CipherNavLink FreeSearch = new() { Href = $"{nameof(Searches)}/{nameof(FreeSearch)}", Name = "טקסט חופשי", Icon = Icons.Cipher.Id };
        public static readonly CipherNavLink AdvancedSearch = new() { Href = $"{nameof(Searches)}/Advanced", Name = "מתקדם", Icon = Icons.Cipher.Advanced };
        public static readonly CipherNavLink CategorySearch = new() { Href = $"{nameof(Searches)}/Category", Name = "קטגוריה", Icon = Icons.Symbols.category };
        public static readonly CipherNavLink PackageSearch = new() { Href = $"{nameof(Searches)}/Package", Name = "תעודה", Icon = Icons.Cipher.Package };
        public static readonly CipherNavLink VesselSearch = new() { Href = $"{nameof(Searches)}/Vessel", Name = "כלי", Icon = Icons.Cipher.Vessel };
        public static readonly CipherNavLink SystemSearch = new() { Href = $"{nameof(Searches)}/System", Name = "מיקום", Icon = Icons.Cipher.Location };
        public static readonly CipherNavLink UnitSearch = new() { Href = $"{nameof(Searches)}/Unit", Name = "יחידה", Icon = Icons.Cipher.Unit };
        public static readonly CipherNavLink ProcessSearch = new() { Href = $"{nameof(Searches)}/Process", Name = "תהליך", Icon = Icons.Cipher.Process };
        public static readonly CipherNavLink Searches = new()
        {
            Href = nameof(Searches),
            Icon = Icons.SearchAndFilter.search,
            Name = "חיפוש",
            SubLinks = new()
                { FreeSearch, AdvancedSearch, CategorySearch, PackageSearch, VesselSearch, SystemSearch, UnitSearch, ProcessSearch }
        };

        public static readonly CipherNavLink Information = new() { Href = nameof(Information), Icon = Icons.Communication.Chat.question_answer, Name = "הסברים ותמיכה" };

        public static readonly List<CipherNavLink> links = new() { Home, Forms, Updates, Approval, Reports, Searches, Information };
    }
}
