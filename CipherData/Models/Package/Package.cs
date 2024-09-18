using CipherData.Requests;
using System.Xml.Linq;

namespace CipherData.Models
{
    public class Package : Resource
    {
        /// <summary>
        /// Description of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Description))]
        public string? Description { get; set; }

        /// <summary>
        /// Dictionary of additional properties of the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Properties))]
        public List<PackageProperty>? Properties { get; set; }

        /// <summary>
        /// Vessel which contains the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Vessel))]
        public Vessel? Vessel { get; set; }

        /// <summary>
        /// Location which contains the package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(System))]
        public StorageSystem System { get; set; }

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
        public Package? Parent { get; set; }

        /// <summary>
        /// Packages contained in this one
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Children))]
        public List<Package>? Children { get; set; }

        /// <summary>
        /// Category of package
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(Category))]
        public Category Category { get; set; }

        /// <summary>
        /// List of processes definitions that may accept this package as input
        /// </summary>
        [HebrewTranslation(typeof(Package), nameof(DestinationProcesses))]
        public List<ProcessDefinition> DestinationProcesses { get; set; }

        /// <summary>
        /// Calculated from the ratio between net to brut mass
        /// </summary>
        public decimal Concentration;

        /// <summary>
        /// Instanciation of a new package
        /// </summary>
        /// <param name="properties">Dictionary of additional properties of the package</param>
        /// <param name="system">Location which contains the package</param>
        /// <param name="brutMass">Total mass of the package</param>
        /// <param name="netMass">Net mass of the package</param>
        /// <param name="createdAt">Timestamp when the package was created</param>
        /// <param name="category">Category of package</param>
        /// <param name="vessel">Vessel which contains the package</param>
        /// <param name="parent">Parent package containing this one.</param>
        /// <param name="children">Packages contained in this one</param>
        /// <param name="description">Description of the package</param>
        /// <param name="destinationProcesses">List of processes definitions that may accept this package as input</param>
        /// <param name="id">only use if you want the package to have a specific id</param>
        public Package(StorageSystem system, decimal brutMass, decimal netMass, DateTime createdAt, Category category,
            Vessel? vessel = null, Package? parent = null, List<Package>? children = null, List<ProcessDefinition>? destinationProcesses = null,
            string? description = null, string? id = null, List<PackageProperty>? properties = null)
        {
            Id = id ?? GetNextId();
            Description = description;
            Vessel = vessel;
            System = system;
            BrutMass = brutMass;
            NetMass = netMass;
            CreatedAt = createdAt;
            Parent = parent;
            Children = children;
            DestinationProcesses = destinationProcesses ?? category.ConsumingProcesses;
            Category = category;

            if (properties == null && Category.Properties != null)
            {
                Properties = new List<PackageProperty>();
                foreach (CategoryProperty prop in Category.Properties)
                {
                    if (!Properties.Any(x=>x.Name == prop.Name))
                    {
                        Properties.Add(new PackageProperty(prop.Name, prop.DefaultValue));
                    }
                }
            }
            else
            {
                Properties = properties;
            }

            Concentration = (brutMass > 0) ? netMass / brutMass : 0;
        }

        /// <summary>
        /// Transfrom package object to a PackageRequest object
        /// </summary>
        /// <returns></returns>
        public PackageRequest Request()
        {
            PackageRequest result = new(
                    id: Id,
                    brutMass: BrutMass,
                    netMass: NetMass,
                    properties: Properties,
                    parent: Parent?.Id,
                    children: Children?.Select(x => x.Id).ToList(),
                    system: System.Id,
                    vessel: Vessel?.Id,
                    category: Category.Id);

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
        /// Hebrew-english translation
        /// </summary>
        public new static HashSet<Tuple<string, string>> Headers()
        {
            List<Tuple<string, string>> result = new();

            result.AddRange(Resource.Headers());
            result.AddRange(GetHebrewTranslations<Package>());

            return result.ToHashSet();
        }

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static Package Random(string? id = null)
        {
            Random random = new();

            decimal curr_brutmass = Convert.ToDecimal(random.Next(0, 10)) / 10M;
            List<string> PackageDescriptions = new() { "נקייה", "מלוכלכת", "מלוכלכת מאוד", "חריג" };
            Category cat = Category.Random();

            List<Package> random_packs = RandomFuncs.FillRandomObjects(new Random().Next(0, 3), Random);
            Package? Parent = (random_packs.Count > 0) ? random_packs.First() : null;

            Package result = new(
                    id: id,
                    description: RandomFuncs.RandomItem(PackageDescriptions),
                    createdAt: RandomFuncs.RandomDateTime(),
                    brutMass: curr_brutmass,
                    netMass: curr_brutmass * (Convert.ToDecimal(random.Next(0, 10)) / 10M),
                    parent: Parent,
                    children: (Parent is null) ? null : random_packs.Where(x => x.Id != Parent.Id).ToList(),
                    system: StorageSystem.Random(),
                    vessel: Vessel.Random(),
                    category: cat,
                    destinationProcesses: cat.ConsumingProcesses); ;
            return result;
        }

        /// <summary>
        /// Get an empty new object.
        /// </summary>
        public static Package Empty()
        {
            Package result = new(
                    id: string.Empty,
                    createdAt: DateTime.Now,
                    brutMass: 0,
                    netMass: 0,
                    system: StorageSystem.Empty(),
                    category: Category.Empty());
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

            if (Properties?.Count() != OtherObject.Properties?.Count()) return false;
            if (Properties != null && OtherObject.Properties != null)
            {
                if (!Properties.Equals(OtherObject.Properties)) return false;
            }

            if (DestinationProcesses?.Count() != OtherObject.DestinationProcesses?.Count()) return false;
            if (DestinationProcesses != null && OtherObject.DestinationProcesses != null)
            {
                if (!DestinationProcesses.SequenceEqual(OtherObject.DestinationProcesses)) return false;
            }

            return true;
        }

        public static string Translate(string searchedAttribute)
        {
            return Translate(typeof(Package), searchedAttribute);
        }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// All events relevant for package.
        /// </summary>
        public Tuple<List<Event>, ErrorResponse> Events()
        {
            return GetObjects<Event>(Id, searchText => new GroupedBooleanCondition(conditions: new List<BooleanCondition>() {
                new (attribute: $"{typeof(Event).Name}.{nameof(RandomData.RandomEvent.Packages)}.Id", attributeRelation: AttributeRelation.Eq, value: searchText, @operator: Operator.Any)
                }, @operator: Operator.Any));
        }

        /// <summary>
        /// All events relevant for package.
        /// </summary>
        public Tuple<List<Process>, ErrorResponse> Processes()
        {
            return GetObjects<Process>(Id, searchText => new GroupedBooleanCondition(conditions: new List<BooleanCondition>() {
                new (attribute: $"{typeof(Process).Name}.{nameof(RandomData.RandomProcess.Events)}.Packages.Id", attributeRelation: AttributeRelation.Eq, value: searchText, @operator: Operator.Any)
                }));
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
                return new(Empty(), ErrorResponse.BadRequest);
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

            return GetObjects<Package>(SearchText, searchText => new GroupedBooleanCondition(conditions: new List<BooleanCondition>() {
                new (attribute: $"{typeof(Package).Name}.{nameof(Id)}", attributeRelation: AttributeRelation.Contains, value: searchText),
                new (attribute: $"{typeof(Package).Name}.{nameof(Description)}", attributeRelation: AttributeRelation.Contains, value: searchText),
                new (attribute: $"{typeof(Package).Name}.{nameof(Properties)}", attributeRelation: AttributeRelation.Contains, value: searchText),
                new (attribute: $"{typeof(Package).Name}.{nameof(Vessel)}.{nameof(Id)}", attributeRelation: AttributeRelation.Contains, value: searchText),
                new (attribute: $"{typeof(Package).Name}.{nameof(System)}.{nameof(Id)}", attributeRelation: AttributeRelation.Contains, value: searchText),
                new (attribute: $"{typeof(Package).Name}.{nameof(Children)}.{nameof(Id)}", attributeRelation: AttributeRelation.Contains, value: searchText, @operator: Operator.Any)
                }, @operator: Operator.Any));
        }
    }
}
