using CipherData.ApiMode;

namespace CipherWeb
{
    public class HebrewDictionary
    {
        public static HashSet<Tuple<string, string?>> BuildDictionary()
        {
            List<Tuple<string, string?>> BuildHeaders = new();
            foreach (Type t in CommonFuncs.GetCipherClasses())
            {
                if (!t.IsAbstract)
                {
                    if (Activator.CreateInstance(t) is Resource r) BuildHeaders.AddRange(r.Headers());
                }
            }
            return BuildHeaders.ToHashSet();
        }

        public static readonly HashSet<Tuple<string, string?>> Headers = BuildDictionary().Distinct().ToHashSet();
    }
}
