using CipherData.Requests;

namespace CipherData.Models
{
    public class Unit : Resource
    {
        private string _Name = string.Empty;

        /// <summary>
        /// Name of the Unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Name))]
        public string Name
        {
            get { return _Name; }
            set { _Name = value.Trim(); }
        }

        private string? _Description = string.Empty;

        /// <summary>
        /// Description of Unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Description))]
        public string? Description
        {
            get { return _Description; }
            set { _Description = value?.Trim(); }
        }

        /// <summary>
        /// JSON-like additional properties of the unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Properties))]
        public string? Properties { get; set; } = null;

        /// <summary>
        /// Parent system containing this one
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Parent))]
        public Unit? Parent { get; set; } = null;

        /// <summary>
        /// Child systems contained in this one
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Children))]
        public List<Unit>? Children { get; set; } = null;

        /// <summary>
        /// Systems under this unit
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Systems))]
        public List<StorageSystem>? Systems { get; set; } = null;

        /// <summary>
        /// Conditions on the unit to make sure it is valid.
        /// </summary>
        [HebrewTranslation(typeof(Unit), nameof(Conditions))]
        public GroupedBooleanCondition? Conditions { get; set; } = null;

        /// <summary>
        /// Instanciation of new unit.
        /// </summary>
        public Unit(string? id = null)
        {
            Id = id ?? GetNextId();
        }

        /// <summary>
        /// Check if this object and other object are exactly the same
        /// </summary>
        public bool Equals(Unit? OtherObject)
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
                    foreach (Unit u in Children.OrderBy(x => x.Id))
                    {
                        if (!u.Equals(OtherObject.Children[Children.IndexOf(u)])) return false;
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

            if (Systems is null)
            {
                if (OtherObject.Systems != null) return false;
            }
            else
            {
                if (!Systems.SequenceEqual(OtherObject.Systems)) return false;
            }


            if (Properties?.Count() != OtherObject.Properties?.Count()) return false;
            if (Properties != null && OtherObject.Properties != null)
            {
                if (!Properties.Equals(OtherObject.Properties)) return false;
            }

            return true;
        }

        public UnitRequest Request()
        {
            return new UnitRequest()
            {
                Name = Name,
                Description = Description,
                Properties = Properties,
                ParentId = Parent?.Id
            };
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
            return $"U{IdCounter:D3}";
        }

        /// <summary>
        /// Hebrew-english translation
        /// </summary>
        public new static HashSet<Tuple<string, string>> Headers()
        {
            List<Tuple<string, string>> result = new();

            result.AddRange(Resource.Headers());
            result.AddRange(GetHebrewTranslations<Unit>());

            return result.ToHashSet();
        }

        /// <summary>
        /// Get a random new object.
        /// </summary>
        /// <param name="id">only use if you want the object to have a specific id</param>
        public static Unit Random(string? id = null)
        {
            List<string> UnitDescriptions = new() { "תפעול", "אחסון", "תכנון" };

            return new Unit(id)
            {
                Name = GetNextId(),
                Description = RandomFuncs.RandomItem(UnitDescriptions)
            };
        }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// Get details about a single unit given unit ID
        /// </summary>
        /// <param name="id">unit ID</param>
        public static Tuple<Unit, ErrorResponse> Get(string id)
        {
            return UnitsRequests.GetUnit(id);
        }

        /// <summary>
        /// All objects
        /// </summary>
        public static Tuple<List<Unit>, ErrorResponse> All()
        {
            return UnitsRequests.GetUnits();
        }

        /// <summary>
        /// Fetch all units which contain the searched text
        /// </summary>
        public static Tuple<List<Unit>, ErrorResponse> Containing(string SearchText)
        {
            return GetObjects<Unit>(SearchText, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() {Attribute = $"{typeof(Unit).Name}.{nameof(Id)}", Value = SearchText },
                new() { Attribute = $"{typeof(Unit).Name}.{nameof(Name)}", Value = SearchText },
                new() {Attribute = $"{typeof(Unit).Name}.{nameof(Description)}", Value = SearchText },
                new() {Attribute = $"{typeof(Unit).Name}.{nameof(Properties)}", Value = SearchText },
                new() {Attribute = $"{typeof(Unit).Name}.{nameof(Parent)}.{nameof(Id)}", Value = SearchText },
                new() {Attribute = $"{typeof(Unit).Name}.{nameof(Children)}.{nameof(Id)}", Value = SearchText, Operator = Operator.Any },
                new() {Attribute = $"{typeof(Unit).Name}.{nameof(Systems)}.{nameof(Id)}", Value = SearchText, Operator = Operator.Any }
                                    },
                Operator = Operator.Any
            });
        }
    }
}

