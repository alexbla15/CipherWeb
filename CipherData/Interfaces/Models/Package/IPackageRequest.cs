namespace CipherData.Interfaces
{
    public interface IPackageRequest
    {
        /// <summary>
        /// ID of the package
        /// </summary>
        string? Id { get; set; }

        /// <summary>
        /// Vessel (Id) which contains the package
        /// </summary>
        string? VesselId { get; set; }

        /// <summary>
        /// Location (Id) which contains the package
        /// </summary>
        string? SystemId { get; set; }

        /// <summary>
        /// Category (Id) of package
        /// </summary>
        string? CategoryId { get; set; }

        /// <summary>
        /// JSON-like additional properties of the package
        /// </summary>
        List<IPackageProperty>? Properties { get; set; }

        /// <summary>
        /// Total mass of the package
        /// </summary>
        decimal BrutMass { get; set; }

        /// <summary>
        /// Net mass of the package
        /// </summary>
        decimal NetMass { get; set; }

        /// <summary>
        /// Parent (Id) containing this one
        /// </summary>
        string? ParentId { get; set; }

        /// <summary>
        /// Packages (Ids) contained in this one
        /// </summary>
        List<string?>? ChildrenIds { get; set; }

        public CheckField CheckId() =>
            CheckField.Required(Id, PackageRequest.Translate(nameof(Id)));
        public CheckField CheckCategoryId() =>
            CheckField.Required(CategoryId, PackageRequest.Translate(nameof(CategoryId)));
        public CheckField CheckSystemId() =>
            CheckField.Required(SystemId, PackageRequest.Translate(nameof(SystemId)));
        public CheckField CheckBrutMass() =>
            CheckField.GreaterEqual(BrutMass, 0, PackageRequest.Translate(nameof(BrutMass)));
        public CheckField CheckNetMass() =>
            CheckField.GreaterEqual(NetMass, 0, PackageRequest.Translate(nameof(NetMass)));

        public CheckField CheckMass()
        {
            CheckField result = CheckBrutMass();
            result = result.Succeeded ? CheckNetMass() : result;

            if (result.Succeeded)
            {
                result = CheckField.GreaterEqual(BrutMass, NetMass,
                    PackageRequest.Translate(nameof(BrutMass)), PackageRequest.Translate(nameof(NetMass)));
            }

            return result;
        }

        public CheckField CheckProperties()
        {
            CheckField result = new();

            if (Properties != null)
            {
                result = CheckField.Distinct(Properties.Select(x => x.Name).ToList(),
                    PackageRequest.Translate(nameof(Properties)));
                result = result.Succeeded ? CheckField.ListItems(Properties,
                    PackageRequest.Translate(nameof(Properties))) : result;
            }

            return result;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckId());
            result.Fields.Add(CheckCategoryId());
            result.Fields.Add(CheckSystemId());
            result.Fields.Add(CheckMass());
            result.Fields.Add(CheckProperties());

            return result.Check();
        }

        public IPackage Create(string? id = null)
            => new Package()
            {
                Id = id ?? Id,
                System = new StorageSystem() { Id = SystemId },
                BrutMass = BrutMass,
                NetMass = NetMass,
                CreatedAt = DateTime.Now,
                Category = new Category() { Id = CategoryId },
                Vessel = VesselId == null ? null : new Vessel() { Id = VesselId },
                Parent = new Package() { Id = ParentId ?? string.Empty },
                Children = ChildrenIds?.Select(x => new Package() { Id = x } as IPackage).ToList(),
                Properties = Properties
            };
    }
}
