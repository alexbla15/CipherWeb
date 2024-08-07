namespace CipherWeb.Data
{
    public enum Language
    {
        English,
        Hebrew
    }

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
        public readonly static string Pending = "מחכה לאישור";
        public readonly static string Warning = "תנועה תקולה";
        public readonly static string Accepted = "תנועה מאושרת";
        public readonly static string Denied = "תנועה נדחתה";
    }
}
