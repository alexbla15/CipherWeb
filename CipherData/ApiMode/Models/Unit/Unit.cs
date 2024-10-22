namespace CipherData.ApiMode
{
    public class Unit : BaseUnit, IUnit
    {
        protected override IUnitsRequests GetRequests() => new UnitsRequests();

        // API RELATED FUNCTIONS

        public override async Task<Tuple<List<IUnit>, ErrorResponse>> Containing(string? SearchText)
        {
            if (string.IsNullOrEmpty(SearchText)) 
                return Tuple.Create(new List<IUnit>(), ErrorResponse.BadRequest);

            var result = await GetObjects<Unit>(SearchText, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() {Attribute = $"{typeof(IUnit).Name}.{nameof(Id)}", Value = SearchText },
                new() { Attribute = $"{typeof(IUnit).Name}.{nameof(Name)}", Value = SearchText },
                new() {Attribute = $"{typeof(IUnit).Name}.{nameof(Description)}", Value = SearchText },
                new() {Attribute = $"{typeof(IUnit).Name}.{nameof(Properties)}", Value = SearchText },
                new() {Attribute = $"{typeof(IUnit).Name}.{nameof(Parent)}.{nameof(Id)}", Value = SearchText },
                new() {Attribute = $"{typeof(IUnit).Name}.{nameof(Children)}.{nameof(Id)}", Value = SearchText, Operator = Operator.Any },
                new() {Attribute = $"{typeof(IUnit).Name}.{nameof(Systems)}.{nameof(Id)}", Value = SearchText, Operator = Operator.Any }
                                    },
                Operator = Operator.Any
            });

            return Tuple.Create(result.Item1.Select(x => x as IUnit).ToList(), result.Item2);
        }
    }
}

