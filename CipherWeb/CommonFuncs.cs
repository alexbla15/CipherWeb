using CipherData.General;
using CipherWeb.Data;

namespace CipherWeb
{
    public class CommonFuncs
    {
        /// <summary>
        /// Method to check if user's classification is enough to view page
        /// </summary>
        public static bool CanView(CipherNavLink link)
            => Constants.SetUser.CanView(link);

        public static List<Tuple<string, string>>? GetChip(string? text, string? id) 
            => (text is null || id is null) ? null : new() { Tuple.Create(text, id) };
    }
}
