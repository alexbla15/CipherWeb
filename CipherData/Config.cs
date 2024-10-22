namespace CipherData
{
    public static class Config
    {
        public const bool RandomMode = true;

        public static IAggregateItem AggregateItem() => new AggregateItem();

        public static IBooleanCondition BooleanCondition() => new BooleanCondition();

        public static ICustomObjectBooleanCondition CustomObjectBooleanCondition() =>
            new CustomObjectBooleanCondition();

        public static IGroupedBooleanCondition GroupedBooleanCondition(bool newObject = true)
            => GetObject<IGroupedBooleanCondition, RandomGroupedBooleanCondition, GroupedBooleanCondition>(newObject);

        public static IObjectFactory ObjectFactory() => new ObjectFactory();

        public static IOrderedItem OrderedItem() => new OrderedItem();

        public static ICategory Category(bool newObject = true) 
            => GetObject<ICategory, RandomCategory, Category>(newObject);

        public static ICategoryProperty CategoryProperty(bool newObject = true)
            => GetObject<ICategoryProperty, RandomCategoryProperty, CategoryProperty>(newObject);

        public static ICategoryRequest CategoryRequest() => new CategoryRequest();

        public static ICreateEvent CreateEvent() => new CreateEvent();

        public static ICreateRelocationEvent CreateRelocationEvent() => new CreateRelocationEvent();

        public static ICreateTranserAmountEvent CreateTranserAmountEvent() => new CreateTranserAmountEvent();
        public static IEvent Event(bool newObject = true) 
            => GetObject<IEvent, RandomEvent, Event>(newObject);

        public static IStorageSystem StorageSystem(bool newObject = true) 
            => GetObject<IStorageSystem, RandomStorageSystem, StorageSystem>(newObject);

        public static ISystemRequest SystemRequest() => new SystemRequest();

        public static IPackage Package(bool newObject = true)
            => GetObject<IPackage, RandomPackage, Package>(newObject);

        public static IPackageProperty PackageProperty() => new PackageProperty();

        public static IProcess Process(bool newObject = true)
            => GetObject<IProcess, RandomProcess, Process>(newObject);

        public static IProcessDefinition ProcessDefinition(bool newObject = true)
            => GetObject<IProcessDefinition, RandomProcessDefinition, ProcessDefinition>(newObject);

        public static IProcessDefinitionRequest ProcessDefinitionRequest() => 
            new ProcessDefinitionRequest();

        public static IProcessStepDefinition ProcessStepDefinition(bool newObject = true)
            => GetObject<IProcessStepDefinition, RandomProcessStepDefinition, ProcessStepDefinition>(newObject);

        public static IUnit Unit(bool newObject = true)
            => GetObject<IUnit, RandomUnit, Unit>(newObject);

        public static IUnitRequest UnitRequest() => new UnitRequest();

        public static IVessel Vessel(bool newObject = true) 
            => GetObject<IVessel, RandomVessel, Vessel>(newObject);

        public static IVesselRequest VesselRequest() => new VesselRequest();

        public static IWorker Worker(bool newObject = true) 
            => GetObject<IWorker, RandomWorker, Worker>(newObject);

        public static IQueryRequests QueryRequests(bool newObject = true) 
            => GetObject<IQueryRequests, RandomQueryRequests, QueryRequests>(newObject);

        public static TInterface GetObject<TInterface, TRandomClass, TApiClass>(bool newObject = true)
            where TRandomClass : class, TInterface, new()
            where TApiClass: class, TInterface, new()
        {
            if (RandomMode && !newObject) return new TRandomClass();
            return new TApiClass();
        }

        public static ICipherClass? GetNewObject<TInterface>()
        {
            if (typeof(TInterface) == typeof(ICategory))
                return Category();
            if (typeof(TInterface) == typeof(IPackage))
                return Package();
            if (typeof(TInterface) == typeof(IProcessDefinition))
                return ProcessDefinition();
            return null;
        }
    }
}
