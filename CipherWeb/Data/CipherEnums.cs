namespace CipherWeb.Data
{
    public class Roles
    {
        public readonly static string Manager = "מנהל";
        public readonly static string SysManager = "מנהל מערכת";
        public readonly static string Authorizer = "מאשר";
        public readonly static string Engineer = "מהנדס";

        public static string CurrnetRole { get; set; } = SysManager;

        public static List<string> Get() => new() { Manager, SysManager, Authorizer, Engineer};
    }

    /// <summary>
    /// Additional colors to Radzen.Colors
    /// </summary>
    public static class CipherColors
    {
        public const string PrimaryLightest = "var(--rz-primary-lightest)";
    }

    /// <summary>
    /// Distinguish between 3 modes of each form
    /// </summary>
    public enum FormMode
    {
        /// <summary>
        /// values are editable (defaults to original value of component)
        /// </summary>
        Update,
        /// <summary>
        /// values are editable (defaults to empty value of component)
        /// </summary>
        Create,
        /// <summary>
        /// read-only option of the component
        /// </summary>
        ReadOnly
    }
}
