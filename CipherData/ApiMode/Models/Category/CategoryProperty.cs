namespace CipherData.ApiMode
{
    /// <summary>
    /// Property scheme of one of the category's properties.
    /// Each package will use it by default, and than will be edited per package.
    /// </summary>
    [HebrewTranslation(nameof(CategoryProperty))]
    public class CategoryProperty : CipherClass, ICategoryProperty
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;

        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        public PropertyType PropertyType { get; set; } = PropertyType.Text;

        public string? DefaultValue { get; set; } = null;

        public override int GetHashCode() => HashCode.Combine(Name, Description, PropertyType, DefaultValue);
    }
}
