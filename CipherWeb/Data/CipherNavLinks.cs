using CipherData;
using CipherData.General;

namespace CipherWeb.Data
{
    public class CipherNavLinks
    {
        private static string Translate(string key) => Translator.GetTranslation(key);

        private static string FormHref(string? specific_form = null) => 
            specific_form is null ? nameof(Forms) : $"{nameof(Forms)}/{specific_form}";

        private static string UpdateHref(string? specific_form = null) =>
            specific_form is null ? $"{nameof(Forms)}/{nameof(Updates)}" : 
            $"{nameof(Forms)}/{nameof(Updates)}/{specific_form.Replace("Update","")}";

        private static string ReportHref(string? specific_form = null) =>
            specific_form is null ? nameof(Reports) : $"{nameof(Reports)}/{specific_form.Replace("Report", "")}";

        private static string SearchHref(string? specific_form = null) =>
            specific_form is null ? nameof(Searches) : $"{nameof(Searches)}/{specific_form}";

        private static string FormName(string specific_form) => Translate($"{nameof(Forms)}_{specific_form}");

        private static string UpdateName(string specific_form) => Translate($"{nameof(Updates)}_{specific_form}");

        private static string ReportName(string specific_form) => Translate($"{nameof(Reports)}_{specific_form}");

        private static string SearchName(string specific_form) => Translate($"{nameof(Searches)}_{specific_form}");

        public static readonly CipherNavLink Home = new() { Href = "", Icon = Icons.Home.home, Name = Translate(nameof(Home)) };

        // FORMS

        public static readonly CipherNavLink AddPackage = 
            new() { 
                Href = FormHref(nameof(AddPackage)), 
                Name = FormName(nameof(AddPackage)), 
                Icon = Icons.Symbols.Plus.add_circle_outline,
                RestrictionLevel = 1
            };

        public static readonly CipherNavLink TransferAmount = 
            new() { 
                Href = FormHref(nameof(TransferAmount)), 
                Name = FormName(nameof(TransferAmount)), 
                Icon = Icons.Cipher.Transfer,
                RestrictionLevel = 1
            };

        public static readonly CipherNavLink Relocation = 
            new() { 
                Href = FormHref(nameof(Relocation)), 
                Name = FormName(nameof(Relocation)), 
                Icon = Icons.Cipher.Location,
                RestrictionLevel = 1
            };

        public static readonly CipherNavLink AddCategory = 
            new() { 
                Href = FormHref(nameof(AddCategory)), 
                Name = FormName(nameof(AddCategory)), 
                Icon = Icons.Symbols.category,
                RestrictionLevel = 1
            };

        public static readonly CipherNavLink AddProcessDefinition = 
            new() { 
                Href = FormHref(nameof(AddProcessDefinition)), 
                Name = FormName(nameof(AddProcessDefinition)), 
                Icon = Icons.Cipher.Process,
                RestrictionLevel = 1
            };

        public static readonly CipherNavLink AddVessel = 
            new() { 
                Href = FormHref(nameof(AddVessel)),
                Name = FormName(nameof(AddVessel)),
                Icon = Icons.Cipher.Vessel,
                RestrictionLevel = 1
            };

        public static readonly CipherNavLink AddStorageSystem = 
            new() { 
                Href = FormHref(nameof(AddStorageSystem)),
                Name = FormName(nameof(AddStorageSystem)),
                Icon = Icons.Documents.inventory,
                RestrictionLevel = 1
            };

        public static readonly CipherNavLink AddUnit = 
            new() { 
                Href = FormHref(nameof(AddUnit)),
                Name = FormName(nameof(AddUnit)),
                Icon = Icons.Cipher.Unit,
                RestrictionLevel = 1
            };

        public static readonly CipherNavLink Forms = new()
        {
            Href = FormHref(),
            Icon = Icons.Documents.Edit.edit,
            Name = Translate(nameof(Forms)),
            RestrictionLevel = 1,
            SubLinks = new()
                { AddPackage, TransferAmount, Relocation,
                AddCategory, AddProcessDefinition, AddVessel, AddStorageSystem, AddUnit }
        };

        public static readonly CipherNavLink UpdateCategory = 
            new() 
            { 
                Href = UpdateHref(nameof(UpdateCategory)),
                Name = UpdateName(nameof(UpdateCategory)), 
                Icon = Icons.Symbols.category,
                RestrictionLevel = 1
            };

        public static readonly CipherNavLink UpdatePackage = 
            new() 
            { 
                Href = UpdateHref(nameof(UpdatePackage)),
                Name = UpdateName(nameof(UpdatePackage)),
                Icon = Icons.Cipher.Package,
                RestrictionLevel = 1
            };

        public static readonly CipherNavLink UpdateUnit = 
            new()
            {
                Href = UpdateHref(nameof(UpdateUnit)),
                Name = UpdateName(nameof(UpdateUnit)),
                Icon = Icons.Cipher.Unit,
                RestrictionLevel = 1
            };

        public static readonly CipherNavLink UpdateVessel = 
            new() 
            { 
                Href = UpdateHref(nameof(UpdateVessel)),
                Name = UpdateName(nameof(UpdateVessel)),
                Icon = Icons.Cipher.Vessel,
                RestrictionLevel = 1
            };

        public static readonly CipherNavLink UpdateSystem = 
            new() 
            { 
                Href = UpdateHref(nameof(UpdateSystem)),
                Name = UpdateName(nameof(UpdateSystem)),
                Icon = Icons.Cipher.Location,
                RestrictionLevel = 1
            };

        public static readonly CipherNavLink UpdateProcess = 
            new() 
            { 
                Href = UpdateHref(nameof(UpdateProcess)),
                Name = UpdateName(nameof(UpdateProcess)),
                Icon = Icons.Cipher.Process,
                RestrictionLevel = 1
            };

        public static readonly CipherNavLink Updates = new()
        {
            Href = UpdateHref(),
            Icon = Icons.Design.rebase_edit,
            Name = Translate(nameof(Updates)),
            SubLinks = new() { UpdateCategory, UpdatePackage, UpdateUnit, UpdateVessel, UpdateSystem, UpdateProcess},
            RestrictionLevel = 1
        };

        public static readonly CipherNavLink Approval = new() { 
            Href = "Approval", 
            Icon = Icons.Symbols.V.done, 
            Name = Translate(nameof(Approval)),
            RestrictionLevel = 1
        };

        public static readonly CipherNavLink AddReport = 
            new() { 
                Href = ReportHref(nameof(AddReport)),
                Name = ReportName(nameof(AddReport)), 
                Icon = Icons.Documents.assignment_add,
                RestrictionLevel = 1
            };

        public static readonly CipherNavLink UpdateReport = 
            new() { 
                Href = ReportHref(nameof(UpdateReport)),
                Name = ReportName(nameof(UpdateReport)),
                Icon = Icons.Documents.Edit.edit_document,
                RestrictionLevel = 1
            };

        public static readonly CipherNavLink PackagesReport = 
            new() { 
                Href = ReportHref(nameof(PackagesReport)),
                Name = ReportName(nameof(PackagesReport)),
                Icon = Icons.Cipher.Package 
            };

        public static readonly CipherNavLink CategoriesReport = 
            new() { 
                Href = ReportHref(nameof(CategoriesReport)),
                Name = ReportName(nameof(CategoriesReport)),
                Icon = Icons.Symbols.category 
            };

        public static readonly CipherNavLink EventsReport = 
            new() { 
                Href = ReportHref(nameof(EventsReport)),
                Name = ReportName(nameof(EventsReport)),
                Icon = Icons.Data.device_hub 
            };

        public static readonly CipherNavLink ProcessesReport = 
            new() { 
                Href = ReportHref(nameof(ProcessesReport)),
                Name = ReportName(nameof(ProcessesReport)),
                Icon = Icons.Cipher.Process 
            };

        public static readonly CipherNavLink Reports = new()
        {
            Href = ReportHref(),
            Icon = Icons.Cipher.Summary,
            Name = Translate(nameof(Reports)),
            SubLinks = new()
                { AddReport, UpdateReport, PackagesReport, CategoriesReport, EventsReport, ProcessesReport }
        };

        public static readonly CipherNavLink FreeText = 
            new() { 
                Href = SearchHref(nameof(FreeText)),
                Name = SearchName(nameof(FreeText)), 
                Icon = Icons.Cipher.Id 
            };

        public static readonly CipherNavLink AdvancedSearch = 
            new() { 
                Href = SearchHref(nameof(AdvancedSearch)),
                Name = SearchName(nameof(AdvancedSearch)),
                Icon = Icons.Cipher.Advanced 
            };

        public static readonly CipherNavLink CategorySearch = 
            new() { 
                Href = SearchHref(nameof(CategorySearch)),
                Name = SearchName(nameof(CategorySearch)),
                Icon = Icons.Symbols.category 
            };

        public static readonly CipherNavLink PackageSearch = 
            new() { 
                Href = SearchHref(nameof(PackageSearch)),
                Name = SearchName(nameof(PackageSearch)),
                Icon = Icons.Cipher.Package 
            };

        public static readonly CipherNavLink VesselSearch = 
            new() { 
                Href = SearchHref(nameof(VesselSearch)),
                Name = SearchName(nameof(VesselSearch)),
                Icon = Icons.Cipher.Vessel 
            };

        public static readonly CipherNavLink SystemSearch = 
            new() { 
                Href = SearchHref(nameof(SystemSearch)),
                Name = SearchName(nameof(SystemSearch)),
                Icon = Icons.Cipher.Location 
            };

        public static readonly CipherNavLink UnitSearch = 
            new() { 
                Href = SearchHref(nameof(UnitSearch)),
                Name = SearchName(nameof(UnitSearch)),
                Icon = Icons.Cipher.Unit 
            };

        public static readonly CipherNavLink ProcessSearch = 
            new() { 
                Href = SearchHref(nameof(ProcessSearch)),
                Name = SearchName(nameof(ProcessSearch)),
                Icon = Icons.Cipher.Process 
            };

        public static readonly CipherNavLink Searches = new()
        {
            Href = SearchHref(),
            Icon = Icons.SearchAndFilter.search,
            Name = Translate(nameof(Searches)),
            SubLinks = new()
                { FreeText, AdvancedSearch, CategorySearch, PackageSearch, VesselSearch, SystemSearch, UnitSearch, ProcessSearch }
        };

        public static readonly List<CipherNavLink> links = 
            new() { Home, Forms, Updates, Approval, Reports, Searches };
    }
}
