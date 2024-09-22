using CipherData.Requests;

namespace CipherData.Models
{
    public class Package : Resource
    {
        private string? _Description = null;

        /// <summary>
        /// Description of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Description))]
        public string? Description {
            get { return _Description; }
            set { _Description = value?.Trim(); } 
        }

        /// <summary>
        /// Dictionary of additional properties of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Properties))]
        public List<PackageProperty>? Properties { get; set; } = null;

        /// <summary>
        /// Vessel which contains the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Vessel))]
        public Vessel? Vessel { get; set; } = null;

        /// <summary>
        /// Location which contains the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(System))]
        public StorageSystem System { get; set; } = new();

        /// <summary>
        /// Total mass of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(BrutMass))]
        public decimal BrutMass { get; set; }

        /// <summary>
        /// Net mass of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(NetMass))]
        public decimal NetMass { get; set; }

        /// <summary>
        /// Timestamp when the package was created
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(CreatedAt))]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Parent package containing this one.
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Parent))]
        public Package? Parent { get; set; } = null;

        /// <summary>
        /// Packages contained in this one
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Children))]
        public List<Package>? Children { get; set; } = null;

        private Category _Category = new();

        /// <summary>
        /// Category of package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Category))]
        public Category Category { 
            get { return _Category; }
            set {
                _Category = value;
                DestinationProcesses = value.ConsumingProcesses;

                if (value.Properties != null)
                {
                    Properties = new List<PackageProperty>();
                    foreach (CategoryProperty prop in value.Properties)
                    {
                        if (!Properties.Any(x => x.Name == prop.Name))
                        {
                            Properties.Add(new PackageProperty() { Name = prop.Name ?? string.Empty, Value = prop.DefaultValue });
                        }
                    }
                }
            } 
        }

        /// <summary>
        /// List of processes definitions that may accept this package as input
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(DestinationProcesses))]
        public List<ProcessDefinition> DestinationProcesses { get; set; } = new();

        /// <summary>
        /// Calculated from the ratio between net to brut mass
        /// </summary>
        public decimal Concentration
        {
            get { return (BrutMass > 0) ? NetMass / BrutMass : 0; }
        }

        /// <summary>
        /// Instanciation of a new package
        /// </summary>
        /// <param name="id">only use if you want the package to have a specific id</param>
        public Package(string? id = null)
        {
            Id = id ?? GetNextId();
        }

        /// <summary>
        /// Transfrom package object to a PackageRequest object
        /// </summary>
        /// <returns></returns>
        public PackageRequest Request()
        {
            PackageRequest result = new()
            {
                Id = Id,
                BrutMass = BrutMass,
                NetMass = NetMass,
                Properties = Properties,
                ParentId = Parent?.Id,
                ChildrenIds = Children?.Select(x => x.Id).ToList(),
                SystemId = System.Id,
                VesselId = Vessel?.Id,
                CategoryId = Category.Id
            };

            return result;
        }

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new package
        /// </summary>
        /// <returns></returns>
        public static string GetNextId()
        {
            IdCounter += 1;
            return $"{DateTime.Now.Year}{new Random().Next(0, 3)}{new Random().Next(0, 999):D3}{IdCounter:D3}";
        }

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static Package Random(string? id = null)
        {
            Random random = new();

            decimal curr_brutmass = Convert.ToDecimal(random.Next(1, 10)) / 10M;
            List<string> PackageDescriptions = new() { "נקייה", "מלוכלכת", "מלוכלכת מאוד", "חריג" };
            Category cat = Category.Random();

            List<Package> random_packs = RandomFuncs.FillRandomObjects(new Random().Next(0, 3), Random);
            Package? Parent = (random_packs.Count > 0) ? random_packs.First() : null;

            Package result = new(id: id)
            {
                Description = RandomFuncs.RandomItem(PackageDescriptions),
                CreatedAt = RandomFuncs.RandomDateTime(),
                BrutMass = curr_brutmass,
                NetMass = curr_brutmass * (Convert.ToDecimal(random.Next(0, 10)) / 10M),
                Parent = Parent,
                Children = (Parent is null) ? null : random_packs.Where(x => x.Id != Parent.Id).ToList(),
                System = StorageSystem.Random(),
                Vessel = Vessel.Random(),
                Category = cat
            };
            return result;
        }

        /// <summary>
        /// Check if this object and other object are exactly the same
        /// </summary>
        public bool Equals(Package? OtherObject)
        {
            if (OtherObject is null) return false;
            if (Id != OtherObject.Id) return false;
            if (Description != OtherObject.Description) return false;
            if (BrutMass != OtherObject.BrutMass) return false;
            if (NetMass != OtherObject.NetMass) return false;

            if (Parent is null)
            {
                if (OtherObject.Parent != null) return false;
            }
            else
            {
                if (OtherObject.Parent is null) return false;
                if (Parent.Equals(OtherObject.Parent)) return false;
            }

            if (Children is null)
            {
                if (OtherObject.Children != null) return false;
            }
            else
            {
                if (OtherObject.Children is null) return false;
                if (Children.Count != OtherObject.Children.Count) return false;
                if (Children.Any())
                {
                    foreach (Package p in Children.OrderBy(x => x.Id))
                    {
                        if (!p.Equals(OtherObject.Children[Children.IndexOf(p)])) return false;
                    }
                }
            }

            if (System is null)
            {
                if (OtherObject.System != null) return false;
            }
            else
            {
                if (OtherObject.System is null) return false;
                if (!System.Equals(OtherObject.System)) return false;
            }

            if (Vessel is null)
            {
                if (OtherObject.Vessel != null) return false;
            }
            else
            {
                if (OtherObject.Vessel is null) return false;
                if (!Vessel.Equals(OtherObject.Vessel)) return false;
            }

            if (Category is null)
            {
                if (OtherObject.Category != null) return false;
            }
            else
            {
                if (OtherObject.Category is null) return false;
                if (!Category.Equals(OtherObject.Category)) return false;
            }

            if (Properties?.Count != OtherObject.Properties?.Count) return false;
            if (Properties != null && OtherObject.Properties != null)
            {
                if (!Properties.Equals(OtherObject.Properties)) return false;
            }

            if (DestinationProcesses?.Count != OtherObject.DestinationProcesses?.Count) return false;
            if (DestinationProcesses != null && OtherObject.DestinationProcesses != null)
            {
                if (!DestinationProcesses.SequenceEqual(OtherObject.DestinationProcesses)) return false;
            }

            return true;
        }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// All events relevant for package.
        /// </summary>
        public Tuple<List<Event>, ErrorResponse> Events()
        {
            return GetObjects<Event>(Id, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() { Attribute = $"{typeof(Event).Name}.{nameof(RandomData.RandomEvent.Packages)}.Id", AttributeRelation = AttributeRelation.Eq, Value = searchText, Operator = Operator.Any }
                },
                Operator = Operator.Any
            });
        }

        /// <summary>
        /// All events relevant for package.
        /// </summary>
        public Tuple<List<Process>, ErrorResponse> Processes()
        {
            return GetObjects<Process>(Id, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() {Attribute = $"{typeof(Process).Name}.{nameof(RandomData.RandomProcess.Events)}.{nameof(Event.Packages)}.{nameof(Id)}",
                    AttributeRelation = AttributeRelation.Eq, Value = searchText, Operator = Operator.Any}
                }
            });
        }

        /// <summary>
        /// Get details about a single package given package ID
        /// </summary>
        /// <param name="id">package ID</param>
        /// <returns></returns>
        public static Tuple<Package, ErrorResponse> Get(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new(new Package(), ErrorResponse.BadRequest);
            }

            return PackagesRequests.GetPackage(id);
        }

        /// <summary>
        /// All packages
        /// </summary>
        public static Tuple<List<Package>, ErrorResponse> All()
        {
            return PackagesRequests.GetPackages();
        }

        /// <summary>
        /// Fetch all packages which contain the searched text
        /// </summary>
        public static Tuple<List<Package>, ErrorResponse> Containing(string SearchText)
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                return new(new(), ErrorResponse.BadRequest);
            }

            return GetObjects<Package>(SearchText, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new () {Attribute = $"{typeof(Package).Name}.{nameof(Id)}", Value = searchText },
                new () {Attribute = $"{typeof(Package).Name}.{nameof(Description)}", Value = searchText },
                new() { Attribute = $"{typeof(Package).Name}.{nameof(Properties)}", Value = searchText },
                new () {Attribute = $"{typeof(Package).Name}.{nameof(Vessel)}.{nameof(Id)}", Value = searchText },
                new () {Attribute = $"{typeof(Package).Name}.{nameof(System)}.{nameof(Id)}", Value = searchText },
                new () {Attribute = $"{typeof(Package).Name}.{nameof(Children)}.{nameof(Id)}", Value = searchText, Operator = Operator.Any }
                },
                Operator = Operator.Any
            });
        }
    }
}
