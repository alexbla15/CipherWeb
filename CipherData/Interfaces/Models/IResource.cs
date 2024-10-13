namespace CipherData.Interfaces
{
    public interface IResource : ICipherClass
    {
        /// <summary>
        /// Required level of clearence to access this object
        /// </summary>
        [HebrewTranslation(typeof(Resource), nameof(ClearenceLevel))]
        string ClearenceLevel { get; set; }

        /// <summary>
        /// Searchable ID for the object
        /// </summary>
        [HebrewTranslation(typeof(Resource), nameof(Id))]
        string? Id { get; set; }

        /// <summary>
        /// Universal unique ID (UUID) for the object, unique over all objects
        /// </summary>
        [HebrewTranslation(typeof(Resource), nameof(Uuid))]
        int Uuid { get; set; }

        public Dictionary<string, object?> ToDictionary()
        {
            return new()
            {
                [nameof(Uuid)] = Uuid,
                [nameof(Id)] = Id,
                [nameof(ClearenceLevel)] = ClearenceLevel,
            };
        }

        // API RELATED FUNCTIONS

        /// <summary>
        /// Fetch all user actions that occured to this package.
        /// </summary>
        Task<Tuple<IUserActionResponse, ErrorResponse>> UserActions();
    }
}
