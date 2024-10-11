using System.Reflection;

namespace CipherData.Models
{

    /// <summary>
    /// Basic resource template for objects.
    /// </summary>
    [HebrewTranslation(nameof(Resource))]
    public abstract class Resource : CipherClass, IResource
    {
        [HebrewTranslation(typeof(Resource), nameof(Id))]
        public string? Id { get; set; } = string.Empty;

        [HebrewTranslation(typeof(Resource), nameof(ClearenceLevel))]
        public string ClearenceLevel { get; set; } = string.Empty;

        [HebrewTranslation(typeof(Resource), nameof(Uuid))]
        public int Uuid { get; set; }

        /// <summary>
        /// Method to get all (english, hebrew) translations of the above attributes.
        /// </summary>
        public HashSet<Tuple<string, string?>> Headers()
        {
            var translations = new HashSet<Tuple<string, string?>>();

            foreach (var prop in GetType().GetProperties())
            {
                var attribute = prop.GetCustomAttribute<HebrewTranslationAttribute>();
                if (attribute != null)
                {
                    translations.Add(Tuple.Create(prop.Name, attribute.Translation));
                }
            }
            return translations;
        }
    }
}
