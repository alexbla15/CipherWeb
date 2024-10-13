using CipherData.Interfaces;
using CipherData.General;
using CipherData;

namespace CipherWeb
{
    public static class CachedData
    {
        public static readonly Task<Tuple<List<ICategory>, ErrorResponse>> AllCategories = Config.Category.All();
        public static readonly Task<Tuple<List<IEvent>, ErrorResponse>> AllEvents = Config.Event.All();
        public static readonly Task<Tuple<List<IPackage>, ErrorResponse>> AllPackages = Config.Package.All();
        public static readonly Task<Tuple<List<IProcess>, ErrorResponse>> AllProcesses = Config.Process.All();
        public static readonly Task<Tuple<List<IProcessDefinition>, ErrorResponse>> AllProcessDefinitions = Config.ProcessDefinition.All();
        public static readonly Task<Tuple<List<IStorageSystem>, ErrorResponse>> AllSystems = Config.StorageSystem.All();
        public static readonly Task<Tuple<List<IVessel>, ErrorResponse>> AllVessels = Config.Vessel.All();
        public static readonly Task<Tuple<List<IUnit>, ErrorResponse>> AllUnits = Config.Unit.All();

        public static readonly List<Type> CipherTypes = CommonFuncs.GetCipherClasses();
    }
}
