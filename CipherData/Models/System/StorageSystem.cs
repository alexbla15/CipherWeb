using CipherData.Requests;

namespace CipherData.Models
{
    public class StorageSystem : Resource
    {
        private string _Name = string.Empty;

        /// <summary>
        /// Name of the system
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(Name))]
        public string Name
        {
            get { return _Name; }
            set { _Name = value.Trim(); }
        }

        private string _Description = string.Empty;

        /// <summary>
        /// Description of system
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(Description))]
        public string Description
        {
            get { return _Description; }
            set { _Description = value.Trim(); }
        }

        /// <summary>
        /// JSON-like additional properties of the system
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(Properties))]
        public Dictionary<string, string>? Properties { get; set; } = null;

        /// <summary>
        /// Parent system containing this one
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(Parent))]
        public StorageSystem? Parent { get; set; } = null;

        /// <summary>
        /// Child systems contained in this one
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(Children))]
        public List<StorageSystem>? Children { get; set; } = null;

        /// <summary>
        /// Unit responsible for this system.
        /// </summary>
        [HebrewTranslation(typeof(StorageSystem), nameof(Unit))]
        public Unit Unit { get; set; } = new();

        /// <summary>
        /// Instanciation of StorageSystem
        /// </summary>
        public StorageSystem(string? id = null)
        {
            Id = id ?? GetNextId();
        }

        /// <summary>
        /// Check if this object and other object are exactly the same
        /// </summary>
        public bool Equals(StorageSystem? OtherObject)
        {
            if (OtherObject is null) return false;
            if (Id != OtherObject.Id) return false;
            if (Name != OtherObject.Name) return false;
            if (Description != OtherObject.Description) return false;

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
                    foreach (StorageSystem sys in Children.OrderBy(x => x.Id))
                    {
                        if (!sys.Equals(OtherObject.Children[Children.IndexOf(sys)])) return false;
                    }
                }
            }
            
            if (Parent is null)
            {
                if (OtherObject.Parent != null) return false;
            }
            else
            {
                if (!Parent.Equals(OtherObject.Parent)) return false;
            }

            if (Unit is null)
            {
                if (OtherObject.Unit != null) return false;
            }
            else
            {
                if (!Unit.Equals(OtherObject.Unit)) return false;
            }


            if (Properties?.Count != OtherObject.Properties?.Count) return false;
            if (Properties != null && OtherObject.Properties != null)
            {
                if (!Properties.Keys.SequenceEqual(OtherObject.Properties.Keys)) return false;
                if (!Properties.Values.SequenceEqual(OtherObject.Properties.Values)) return false;
            }

            return true;
        }

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        /// <returns></returns>
        public static string GetNextId()
        {
            IdCounter += 1;
            return $"S{IdCounter:D3}";
        }

        /// <summary>
        /// Hebrew-english translation
        /// </summary>
        public new static HashSet<Tuple<string, string>> Headers()
        {
            List<Tuple<string, string>> result = new();

            result.AddRange(Resource.Headers());
            result.AddRange(GetHebrewTranslations<StorageSystem>());

            return result.ToHashSet();
        }

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static StorageSystem Random(string? id = null)
        {
            List<string> SystemsDescriptions = new() { "תחום", "מעבדה", "מבנה" };

            return new(id)
            {
                Name = id ?? GetNextId(),
                Description = RandomFuncs.RandomItem(SystemsDescriptions),
                Unit = Unit.Random(),
                Parent = (new Random().Next(0, 5) == 0) ? Random() : null
            };
        }

        public static string Translate(string searchedAttribute)
        {
            return Translate(typeof(StorageSystem), searchedAttribute);
        }

        // API related functions

        /// <summary>
        /// All events that took place in this system
        /// </summary>
        public Tuple<List<Event>, ErrorResponse> Events()
        {
            return Events(Id);
        }

        /// <summary>
        /// All packages that took place in this system
        /// </summary>
        public Tuple<List<Package>, ErrorResponse> Packages()
        {
            return Packages(Id);
        }

        /// <summary>
        /// Get details about a system vessel given system ID
        /// </summary>
        /// <param name="id">system ID</param>
        /// <returns></returns>
        public static Tuple<StorageSystem, ErrorResponse> Get(string id)
        {
            return SystemsRequests.GetSystem(id);
        }

        /// <summary>
        /// All systems that took place in a certain system
        /// </summary>
        public static Tuple<List<StorageSystem>, ErrorResponse> All()
        {
            return SystemsRequests.GetSystems();
        }

        /// <summary>
        /// Fetch all systems which contain the searched text
        /// </summary>
        public static Tuple<List<StorageSystem>, ErrorResponse> Containing(string SearchText)
        {
            return GetObjects<StorageSystem>(SearchText, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() {Attribute = $"{nameof(System)}.{nameof(Id)}", Value = SearchText },
                new() { Attribute = $"{nameof(System)}.{nameof(Name)}", Value = SearchText },
                new() {Attribute = $"{nameof(System)}.{nameof(Description)}", Value = SearchText },
                new() {Attribute = $"{nameof(System)}.{nameof(Properties)}", Value = SearchText },
                new() {Attribute = $"{nameof(System)}.{nameof(Parent)}.{nameof(Id)}", Value = SearchText },
                new() {Attribute = $"{nameof(System)}.{nameof(Children)}.{nameof(Id)}", Value = SearchText, Operator = Operator.Any },
                new() {Attribute = $"{nameof(System)}.{nameof(Unit)}.{nameof(Id)}", Value = SearchText }
            },
                Operator = Operator.Any
            });
        }

        /// <summary>
        /// All events that took place in a certain system
        /// </summary>
        /// <param name="SelectedSystem">selected system for query</param>
        public static Tuple<List<Event>, ErrorResponse> Events(string SelectedSystem)
        {
            return GetObjects<Event>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{nameof(Event)}.{nameof(Event.Packages)}.{nameof(Package.System)}.{nameof(Id)}", AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            },
                Operator = Operator.Any
            });
        }

        /// <summary>
        /// All processes that took place in a certain system
        /// </summary>
        /// <param name="SelectedSystem">selected system for query</param>
        /// <returns></returns>
        public static Tuple<List<Process>, ErrorResponse> Processes(string SelectedSystem)
        {
            return GetObjects<Process>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{nameof(Process)}.{nameof(Process.Events)}.{nameof(Event.Packages)}.{nameof(Package.System)}.{nameof(Id)}",
                    AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            },
                Operator = Operator.Any
            });
        }

        /// <summary>
        /// All packages that took place in a certain system
        /// </summary>
        public static Tuple<List<Package>, ErrorResponse> Packages(string SelectedSystem)
        {
            return GetObjects<Package>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{typeof(Package).Name}.{nameof(Package.System)}.{nameof(Id)}",
                    AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            }, Operator = Operator.Any 
            });
        }

        /// <summary>
        /// All vessels that are under this system
        /// </summary>
        public static Tuple<List<Vessel>, ErrorResponse> Vessels(string SelectedSystem)
        {
            return GetObjects<Vessel>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{typeof(Vessel).Name}.{nameof(Package.System)}.{nameof(Id)}",
                    AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            },
                Operator = Operator.Any
            });
        }
    }
}
