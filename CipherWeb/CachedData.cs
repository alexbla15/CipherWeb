using CipherData.Models;

namespace CipherWeb
{
    public static class CachedData
    {
        public static readonly Tuple<List<ICategory>, ErrorResponse> AllCategories = Category.All();
        public static readonly Tuple<List<IEvent>, ErrorResponse> AllEvents = Event.All();
        public static readonly Tuple<List<IPackage>, ErrorResponse> AllPackages = Package.All();
        public static readonly Tuple<List<IProcess>, ErrorResponse> AllProcesses = Process.All();
        public static readonly Tuple<List<IProcessDefinition>, ErrorResponse> AllProcessDefinitions = ProcessDefinition.All();
        public static readonly Tuple<List<IStorageSystem>, ErrorResponse> AllSystems = StorageSystem.All();
        public static readonly Tuple<List<IVessel>, ErrorResponse> AllVessels = Vessel.All();
        public static readonly Tuple<List<IUnit>, ErrorResponse> AllUnits = Unit.All();

        public static readonly List<Type> CipherTypes = CommonFuncs.GetCipherClasses();
    }
}
