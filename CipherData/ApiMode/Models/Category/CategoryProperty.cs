namespace CipherData.ApiMode
{
    public class CategoryProperty : BaseCategoryProperty, ICategoryProperty
    {
        public override int GetHashCode() => HashCode.Combine(Name, Description, PropertyType, DefaultValue);
    }
}
