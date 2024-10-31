namespace CipherData.ApiMode
{
    public class VesselRequest : CipherClass, IVesselRequest
    {
        private string? _Name;
        private string? _Type = string.Empty;

        public string? Name { get => _Name; set => _Name = value?.Trim(); }

        public string? Type { get => _Type; set => _Type = value?.Trim(); }

        public string? SystemId { get; set; }
    }
}
