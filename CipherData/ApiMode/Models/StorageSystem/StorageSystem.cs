﻿namespace CipherData.ApiMode
{
    public class StorageSystem : BaseStorageSystem, IStorageSystem
    {
        // API RELATED FUNCTIONS

        protected override ISystemsRequests GetRequests() => new SystemsRequests();

        public override async Task<Tuple<List<IStorageSystem>, ErrorResponse>> Containing(string? SearchText)
        {
            var result = await GetObjects<StorageSystem>(SearchText, searchText => new GroupedBooleanCondition()
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

            return Tuple.Create(result.Item1.Select(x => x as IStorageSystem).ToList(), result.Item2);
        }

        public override async Task<Tuple<List<IEvent>, ErrorResponse>> Events(string? SelectedSystem)
        {
            if (string.IsNullOrEmpty(SelectedSystem)) return Tuple.Create(new List<IEvent>(), ErrorResponse.BadRequest);

            var result = await GetObjects<Event>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{nameof(Event)}.{nameof(IEvent.FinalStatePackages)}.{nameof(IPackage.System)}.{nameof(Id)}", AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            },
                Operator = Operator.Any
            });
            return Tuple.Create(result.Item1.Select(x => x as IEvent).ToList(), result.Item2);
        }

        public override async Task<Tuple<List<IProcess>, ErrorResponse>> Processes(string? SelectedSystem)
        {
            if (string.IsNullOrEmpty(SelectedSystem)) return Tuple.Create(new List<IProcess>(), ErrorResponse.BadRequest);

            var result = await GetObjects<Process>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{nameof(Process)}.{nameof(IProcess.Events)}.{nameof(IEvent.FinalStatePackages)}.{nameof(Package.System)}.{nameof(Id)}",
                    AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            },
                Operator = Operator.Any
            });
            return Tuple.Create(result.Item1.Select(x => x as IProcess).ToList(), result.Item2);
        }

        public override async Task<Tuple<List<IPackage>, ErrorResponse>> Packages(string? SelectedSystem)
        {
            if (string.IsNullOrEmpty(SelectedSystem)) return Tuple.Create(new List<IPackage>(), ErrorResponse.BadRequest);

            var result = await GetObjects<Package>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{typeof(Package).Name}.{nameof(IPackage.System)}.{nameof(Id)}",
                    AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            },
                Operator = Operator.Any
            });
            return Tuple.Create(result.Item1.Select(x => x as IPackage).ToList(), result.Item2);
        }
        
        public override async Task<Tuple<List<IVessel>, ErrorResponse>> Vessels(string? SelectedSystem)
        {
            if (string.IsNullOrEmpty(SelectedSystem)) return Tuple.Create(new List<IVessel>(), ErrorResponse.BadRequest);

            var result = await GetObjects<Vessel>(SelectedSystem, SelectedSystem => new GroupedBooleanCondition()
            {
                Conditions = new List<BooleanCondition>()
            {
                new() {Attribute = $"{typeof(Vessel).Name}.{nameof(IPackage.System)}.{nameof(Id)}",
                    AttributeRelation = AttributeRelation.Eq,
                    Value = SelectedSystem}
            },
                Operator = Operator.Any
            });
            return Tuple.Create(result.Item1.Select(x => x as IVessel).ToList(), result.Item2);
        }
    }
}
