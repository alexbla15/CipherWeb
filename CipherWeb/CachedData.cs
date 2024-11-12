using CipherData.Interfaces;
using CipherData.General;
using CipherData;

namespace CipherWeb
{
    public static class CachedData
    {
        public static readonly Task<Tuple<List<ICategory>, ErrorResponse>> AllCategories = Config.Category(false).All();
        public static readonly Task<Tuple<List<IEvent>, ErrorResponse>> AllEvents = Config.Event(false).All();
        public static readonly Task<Tuple<List<IPackage>, ErrorResponse>> AllPackages = Config.Package(false).All();
        public static readonly Task<Tuple<List<IProcess>, ErrorResponse>> AllProcesses = Config.Process(false).All();
        public static readonly Task<Tuple<List<IProcessDefinition>, ErrorResponse>> AllProcessDefinitions = Config.ProcessDefinition(false).All();
        public static readonly Task<Tuple<List<IStorageSystem>, ErrorResponse>> AllSystems = Config.StorageSystem(false).All();
        public static readonly Task<Tuple<List<IVessel>, ErrorResponse>> AllVessels = Config.Vessel(false).All();
        public static readonly Task<Tuple<List<IUnit>, ErrorResponse>> AllUnits = Config.Unit(false).All();

        public static readonly List<Type> CipherTypes = CipherField.GetSubClasses(typeof(ICipherClass));
    }
}
