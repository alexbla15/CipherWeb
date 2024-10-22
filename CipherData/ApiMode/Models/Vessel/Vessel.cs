namespace CipherData.ApiMode
{
    public class Vessel : BaseVessel, IVessel
    {
        // API RELATED METHODS

        protected override IVesselsRequests GetRequests() => new VesselsRequests();

        public override async Task<Tuple<List<IVessel>, ErrorResponse>> Containing(string? SearchText)
        {
            var result = await GetObjects<Vessel>(SearchText, searchText => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>() {
                new() {Attribute = $"{typeof(Vessel).Name}.{nameof(Id)}", Value= SearchText },
                new() { Attribute = $"{typeof(Vessel).Name}.{nameof(Name)}", Value= SearchText },
                new() {Attribute = $"{typeof(Vessel).Name}.{nameof(Type)}", Value= SearchText },
                new() {Attribute = $"{typeof(Vessel).Name}.{nameof(System)}.{nameof(Id)}", Value= SearchText },
                new() {Attribute = $"{typeof(Vessel).Name}.{nameof(ContainingPackages)}.{nameof(Id)}", Value= SearchText, Operator=Operator.Any }
                    },
                Operator = Operator.Any
            });

            return Tuple.Create(result.Item1.Select(x => x as IVessel).ToList(), result.Item2);
        }
    }
}
