namespace CipherData.Interfaces
{
    public interface IUnit : IResource
    {
        /// <summary>
        /// Description of Unit
        /// </summary>
        string? Description { get; set; }

        /// <summary>
        /// Name of the Unit
        /// </summary>
        string? Name { get; set; }

        /// <summary>
        /// Child systems contained in this one
        /// </summary>
        List<IUnit>? Children { get; set; }

        /// <summary>
        /// Conditions on the unit to make sure it is valid.
        /// </summary>
        IGroupedBooleanCondition? Conditions { get; set; }

        /// <summary>
        /// Parent system containing this one
        /// </summary>
        IUnit? Parent { get; set; }

        /// <summary>
        /// JSON-like additional properties of the unit
        /// </summary>
        string? Properties { get; set; }

        /// <summary>
        /// Systems under this unit
        /// </summary>
        List<IStorageSystem>? Systems { get; set; }

        public new Dictionary<string, object?> ToDictionary()
        {
            return new()
            {
                [nameof(Id)] = Id,
                [nameof(Name)] = Name,
                [nameof(Description)] = Description,
                [nameof(Properties)] = Properties,
                [nameof(Parent)] = Parent?.Name,
                [nameof(Children)] = Children is null ? null : string.Join("; ", Children.Select(x => x.Name).ToList()),
                [nameof(Systems)] = Systems is null ? null : string.Join("; ", Systems.Select(x => x.Name).ToList()),
            };
        }

        public IUnitRequest Request()
            => new UnitRequest()
            {
                Name = Name,
                Description = Description,
                Properties = Properties,
                ParentId = Parent?.Id
            };


        // API-RELATED FUNCTIONS

        /// <summary>
        /// Get details about a single unit given unit ID
        /// </summary>
        /// <param name="id">unit ID</param>
        public static async Task<Tuple<IUnit, ErrorResponse>> Get(string id) => await Config.UnitsRequests.GetUnit(id);

        /// <summary>
        /// All objects
        /// </summary>
        public static async Task<Tuple<List<IUnit>, ErrorResponse>> All() => await Config.UnitsRequests.GetUnits();

        /// <summary>
        /// Fetch all units which contain the searched text
        /// </summary>
        public static async Task<Tuple<List<IUnit>, ErrorResponse>> Containing(string SearchText)
        {
            var result = await GetObjects<Unit>(SearchText, searchText => new GroupedBooleanCondition()
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

            return Tuple.Create(result.Item1.Select(x => x as IUnit).ToList(), result.Item2);
        }
    }
}

