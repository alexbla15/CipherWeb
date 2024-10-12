namespace CipherData.RandomMode
{
    public static class RandomData
    {
        public static readonly List<string> MaterialTypes = new() { "Mg", "Na", "Ne" };
        public static readonly List<string> CategoriesNames = new() { "מוצק", "גז", "נוזל" };
        public static readonly List<string> CategoriesDescriptions = new() { "חומרים בפאזה מוצקה", "חומרים בפאזה גזית", "חומרים בפאזה נוזלית" };

        public static readonly ICustomObjectBooleanCondition CustomObjectBooleanCondition = new RandomCustomObjectBooleanCondition();
        public static readonly IGroupedBooleanCondition GroupedBooleanCondition = new RandomGroupedBooleanCondition();
        public static readonly IUserActionResponse UserActionResponse = new RandomUserActionResponse();

        public static readonly ICategory Category = new RandomCategory();
        public static readonly ICategoryProperty RandomCategoryProperty = new RandomCategoryProperty();
        public static readonly IEvent Event = new RandomEvent();
        public static readonly IPackage Package = new RandomPackage();
        public static readonly IProcess Process = new RandomProcess();
        public static readonly IProcessDefinition ProcessDefinition = new RandomProcessDefinition();
        public static readonly IStorageSystem System = new RandomStorageSystem();
        public static readonly IUnit Unit = new RandomUnit();
        public static readonly IVessel Vessel = new RandomVessel();

        public static readonly List<ICategory> Categories = GetRandomCategories();
        public static readonly List<ICategoryProperty> RandomCategoryProperties = GetRandomCategoryProperties();
        public static readonly List<IEvent> Events = GetRandomEvents();
        public static readonly List<IPackage> Packages = GetRandomPackages();
        public static readonly List<IProcess> Processes = GetRandomProcesses();
        public static readonly List<IProcessDefinition> ProcessDefinitions = GetRandomProcessDefinitions();
        public static readonly List<IStorageSystem> Systems = GetRandomSystems();
        public static readonly List<IVessel> Vessels = GetRandomVessels();
        public static readonly List<IUnit> Units = GetRandomUnits();

        public static List<TInterface> GetRandomObjects<TInterface, TConcrete>(int amount = 20) where TConcrete : TInterface
        {
            return RandomFuncs.FillRandomObjects(amount, () => (TInterface)typeof(TConcrete).GetConstructor(Type.EmptyTypes)?.Invoke(null));
        }

        public static List<ICategory> GetRandomCategories(int amount = 20) => GetRandomObjects<ICategory, RandomCategory>(amount);
        public static List<ICategoryProperty> GetRandomCategoryProperties(int amount = 3) => GetRandomObjects<ICategoryProperty, RandomCategoryProperty>(amount);
        public static List<IEvent> GetRandomEvents(int amount = 20) => GetRandomObjects<IEvent, RandomEvent>(amount);
        public static List<IPackage> GetRandomPackages(int amount = 20) => GetRandomObjects<IPackage, RandomPackage>(amount);
        public static List<IProcess> GetRandomProcesses(int amount = 20) => GetRandomObjects<IProcess, RandomProcess>(amount);
        public static List<IProcessDefinition> GetRandomProcessDefinitions(int amount = 20) => GetRandomObjects<IProcessDefinition, RandomProcessDefinition>(amount);
        public static List<IStorageSystem> GetRandomSystems(int amount = 20) => GetRandomObjects<IStorageSystem, RandomStorageSystem>(amount);
        public static List<IUnit> GetRandomUnits(int amount = 20) => GetRandomObjects<IUnit, RandomUnit>(amount);
        public static List<IUserAction> GetRandomUserActions(int amount = 20) => GetRandomObjects<IUserAction, RandomUserAction>(amount);
        public static List<IVessel> GetRandomVessels(int amount = 20) => GetRandomObjects<IVessel, RandomVessel>(amount);
    }
}
