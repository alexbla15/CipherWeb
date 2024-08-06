namespace CipherWeb.Data
{
    public class Roles
    {
        public readonly static string Manager = "מנהל/ת";
        public readonly static string SysManager = "מנהל/ת מערכת";
        public readonly static string Authorizer = "מאשר/ת";
        public readonly static string Engineer = "מהנדס/ת";

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
