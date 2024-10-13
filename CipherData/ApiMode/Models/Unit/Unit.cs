namespace CipherData.ApiMode
{
    [HebrewTranslation(nameof(Unit))]
    public class Unit : Resource, IUnit
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;

        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        public string? Properties { get; set; }

        public IUnit? Parent { get; set; }

        public List<IUnit>? Children { get; set; }

        public List<IStorageSystem>? Systems { get; set; }

        public IGroupedBooleanCondition? Conditions { get; set; }

        // API RELATED FUNCTIONS

        public async Task<Tuple<IUnit, ErrorResponse>> Get(string? id)
            => await new UnitsRequests().GetUnit(id);

        public async Task<Tuple<List<IUnit>, ErrorResponse>> All()
            => await new UnitsRequests().GetUnits();

        public async Task<Tuple<IUnit, ErrorResponse>> Create(IUnitRequest req)
            => await new UnitsRequests().CreateUnit(req);

        public async Task<Tuple<IUnit, ErrorResponse>> Update(string? id, IUnitRequest req)
            => await new UnitsRequests().UpdateUnit(id, req);

        public async Task<Tuple<List<IUnit>, ErrorResponse>> Containing(string? SearchText)
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

