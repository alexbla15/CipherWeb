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
}
