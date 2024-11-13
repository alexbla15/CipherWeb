namespace CipherData.ApiMode
{
    public class Vessel : BaseVessel, IVessel
    {
        public override IVessel Copy()
        {
            var res = new Vessel();

            foreach (var prop in typeof(IVessel).GetProperties())
            {
                prop.SetValue(res, prop.GetValue(this));
            }

            return res;
        }

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

        public override async Task<Tuple<List<IPackage>, ErrorResponse>> Packages(string? SelectedVessel)
        {
            if (string.IsNullOrEmpty(SelectedVessel)) return Tuple.Create(new List<IPackage>(), ErrorResponse.BadRequest);

            var result = await GetObjects<Package>(SelectedVessel, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{typeof(Package).Name}.{nameof(IPackage.Vessel)}.{nameof(Id)}",
                    AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            },
                Operator = Operator.Any
            });
            return Tuple.Create(result.Item1.Select(x => x as IPackage).ToList(), result.Item2);
        }
    }
}
