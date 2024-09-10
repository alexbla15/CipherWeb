namespace CipherWeb.Data
{
    public class Roles
    {
        public readonly static string Manager = "מנהל";
        public readonly static string SysManager = "מנהל מערכת";
        public readonly static string Authorizer = "מאשר";
        public readonly static string Engineer = "מהנדס";

        public static string CurrnetRole { get; set; } = SysManager;

        public static List<string> Get()
        {
            return new List<string>() { Manager, SysManager, Authorizer, Engineer};
        }
    }

    public class EventStatus
    {
        public const string Pending = "מחכה לאישור";
        public const string Warning = "תנועה תקולה";
        public const string Accepted = "תנועה מאושרת";
        public const string Denied = "תנועה נדחתה";
    }

    /// <summary>
    /// Additional colors to Radzen.Colors
    /// </summary>
    public static class CipherColors
    {
        public const string PrimaryLightest = "var(--rz-primary-lightest)";
    }
}
