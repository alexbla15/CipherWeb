namespace CipherData
{
    public static class Config
    {
        public const bool RandomMode = true;

        public static readonly IAggregateItem AggregateItem = new AggregateItem();
        public static readonly IBooleanCondition BooleanCondition = new BooleanCondition();
        public static readonly IGroupedBooleanCondition GroupedBooleanCondition =
            RandomMode ? new RandomGroupedBooleanCondition() : new GroupedBooleanCondition();
        public static readonly IObjectFactory ObjectFactory = new ObjectFactory();
        public static readonly IOrderedItem OrderedItem = new OrderedItem();

        public static readonly ICategory Category = RandomMode ? new RandomCategory() : new Category();
        public static readonly ICategoryProperty CategoryProperty = 
            RandomMode ? new RandomCategoryProperty() : new CategoryProperty();
        public static readonly ICategoryRequest CategoryRequest = new CategoryRequest();

        public static readonly ICreateEvent CreateEvent = new CreateEvent();
        public static readonly ICreateRelocationEvent CreateRelocationEvent = new CreateRelocationEvent();
        public static readonly ICreateTranserAmountEvent CreateTranserAmountEvent = new CreateTranserAmountEvent();
        public static readonly IEvent Event = RandomMode ? new RandomEvent() : new Event();

        public static readonly IStorageSystem StorageSystem = RandomMode ? new RandomStorageSystem() : new StorageSystem();
        public static readonly ISystemRequest SystemRequest = new SystemRequest();

        public static readonly IPackage Package = RandomMode ? new RandomPackage() : new Package();
        public static readonly IPackageProperty PackageProperty = new PackageProperty();

        public static readonly IProcess Process = RandomMode ? new RandomProcess() : new Process();
        public static readonly IProcessDefinition ProcessDefinition = 
            RandomMode ? new RandomProcessDefinition() : new ProcessDefinition();
        public static readonly IProcessDefinitionRequest ProcessDefinitionRequest = new ProcessDefinitionRequest();
        public static readonly IProcessStepDefinition ProcessStepDefinition =
            RandomMode ? new RandomProcessStepDefinition() : new ProcessStepDefinition();

        public static readonly IUnit Unit = RandomMode ? new RandomUnit() : new Unit();
        public static readonly IUnitRequest UnitRequest = new UnitRequest();

        public static readonly IVessel Vessel = RandomMode ? new RandomVessel() : new Vessel();
        public static readonly IVesselRequest VesselRequest = new VesselRequest();

        public static readonly IWorker Worker = RandomMode ? new RandomWorker() : new Worker();

        public static readonly IQueryRequests QueryRequests = RandomMode ? new RandomQueryRequests() : new QueryRequests();
    }
}
