using System.Reflection;

namespace CipherData.ApiMode
{
    /// <summary>
    /// Create a new unit or update it
    /// </summary>
    [HebrewTranslation(nameof(UnitRequest))]
    public class UnitRequest : CipherClass, IUnitRequest
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;

        [HebrewTranslation(typeof(Unit), nameof(Name))]
        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        [HebrewTranslation(typeof(Unit), nameof(Description))]
        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        [HebrewTranslation(typeof(Unit), nameof(Unit.Properties))]
        public string? Properties { get; set; }

        [HebrewTranslation(typeof(Unit), nameof(Unit.Parent))]
        public string? ParentId { get; set; }

        [HebrewTranslation(typeof(Unit), nameof(Unit.Conditions))]
        public IGroupedBooleanCondition? Conditions { get; set; }

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}
