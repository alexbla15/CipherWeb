using CipherData.Models;
using CipherData.Randomizer;

namespace CipherData
{
    public static class RandomData
    {
        public static readonly List<string> MaterialTypes = new() { "Mg", "Na", "Ne" };
        public static readonly List<string> CategoriesNames = new() { "מוצק", "גז", "נוזל" };
        public static readonly List<string> CategoriesDescriptions = new() { "חומרים בפאזה מוצקה", "חומרים בפאזה גזית", "חומרים בפאזה נוזלית" };

        public static readonly CustomObjectBooleanCondition RandomCustomObjectBooleanCondition = CustomObjectBooleanCondition.Random();
        public static readonly GroupedBooleanCondition RandomGroupedBooleanCondition = GroupedBooleanCondition.Random();
        public static readonly UserActionResponse RandomUserActionResponse = UserActionResponse.Random();

        public static readonly Category RandomCategory = Category.Random();
        public static readonly CategoryProperty RandomCategoryProperty = CategoryProperty.Random();
        public static readonly Event RandomEvent = Event.Random();
        public static readonly Package RandomPackage = Package.Random();
        public static readonly Process RandomProcess = Process.Random();
        public static readonly ProcessDefinition RandomProcessDefinition = ProcessDefinition.Random();
        public static readonly StorageSystem RandomSystem = StorageSystem.Random();
        public static readonly Unit RandomUnit = Unit.Random();
        public static readonly UnitRequest RandomUnitRequest = Unit.Random().Request();
        public static readonly Vessel RandomVessel = Vessel.Random();

        public static readonly List<Category> RandomCategories = RandomFuncs.FillRandomObjects(20, Category.Random);
        public static readonly List<CategoryProperty> RandomCategoryProperties = RandomFuncs.FillRandomObjects(3, CategoryProperty.Random);
        public static readonly List<Event> RandomEvents = RandomFuncs.FillRandomObjects(20, Event.Random);
        public static readonly List<Package> RandomPackages = RandomFuncs.FillRandomObjects(20, Package.Random);
        public static readonly List<Process> RandomProcesses = RandomFuncs.FillRandomObjects(20, Process.Random);
        public static readonly List<ProcessDefinition> RandomProcessDefinitions = RandomFuncs.FillRandomObjects(20, ProcessDefinition.Random);
        public static readonly List<StorageSystem> RandomSystems = RandomFuncs.FillRandomObjects(20, StorageSystem.Random);
        public static readonly List<Vessel> RandomVessels = RandomFuncs.FillRandomObjects(20, Vessel.Random);
        public static readonly List<Unit> RandomUnits = RandomFuncs.FillRandomObjects(20, Unit.Random);
    }
}
