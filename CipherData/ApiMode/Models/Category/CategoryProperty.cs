namespace CipherData.ApiMode
{
    /// <summary>
    /// Property scheme of one of the category's properties.
    /// Each package will use it by default, and than will be edited per package.
    /// </summary>
    public class CategoryProperty : BaseCategoryProperty, ICategoryProperty
    {
        public override int GetHashCode() => HashCode.Combine(Name, Description, PropertyType, DefaultValue);
    }
}
