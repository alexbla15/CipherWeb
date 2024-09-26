using CipherData.Models;

namespace CipherWeb
{
    public static class CachedData
    {
        public static readonly Tuple<List<Category>, ErrorResponse> AllCategories = Category.All();
        public static readonly Tuple<List<Event>, ErrorResponse> AllEvents = Event.All();
        public static readonly Tuple<List<Package>, ErrorResponse> AllPackages = Package.All();
        public static readonly Tuple<List<Process>, ErrorResponse> AllProcesses = Process.All();
        public static readonly Tuple<List<ProcessDefinition>, ErrorResponse> AllProcessDefinitions = ProcessDefinition.All();
        public static readonly Tuple<List<StorageSystem>, ErrorResponse> AllSystems = StorageSystem.All();
        public static readonly Tuple<List<Vessel>, ErrorResponse> AllVessels = Vessel.All();
        public static readonly Tuple<List<Unit>, ErrorResponse> AllUnits = Unit.All();

        public static readonly List<Type> CipherTypes = CommonFuncs.GetCipherClasses();
    }
}
