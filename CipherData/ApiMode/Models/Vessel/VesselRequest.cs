namespace CipherData.ApiMode
{
    /// <summary>
    /// Create a new unit or update it
    /// </summary>
    [HebrewTranslation(nameof(VesselRequest))]
    public class VesselRequest : CipherClass, IVesselRequest
    {
        private string? _Name;
        private string? _Type = string.Empty;

        [HebrewTranslation(typeof(Vessel), nameof(Name))]
        public string? Name { get => _Name; set => _Name = value?.Trim(); }

        [HebrewTranslation(typeof(Vessel), nameof(Type))]
        public string? Type { get => _Type; set => _Type = value?.Trim(); }

        [HebrewTranslation(typeof(Vessel), nameof(Vessel.System))]
        public string? SystemId { get; set; }
    }
}
