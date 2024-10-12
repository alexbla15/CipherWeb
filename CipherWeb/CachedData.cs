using CipherData.Interfaces;
using CipherData.General;

namespace CipherWeb
{
    public static class CachedData
    {
        public static readonly Task<Tuple<List<ICategory>, ErrorResponse>> AllCategories = ICategory.All();
        public static readonly Task<Tuple<List<IEvent>, ErrorResponse>> AllEvents = IEvent.All();
        public static readonly Task<Tuple<List<IPackage>, ErrorResponse>> AllPackages = IPackage.All();
        public static readonly Task<Tuple<List<IProcess>, ErrorResponse>> AllProcesses = IProcess.All();
        public static readonly Task<Tuple<List<IProcessDefinition>, ErrorResponse>> AllProcessDefinitions = IProcessDefinition.All();
        public static readonly Task<Tuple<List<IStorageSystem>, ErrorResponse>> AllSystems = IStorageSystem.All();
        public static readonly Task<Tuple<List<IVessel>, ErrorResponse>> AllVessels = IVessel.All();
        public static readonly Task<Tuple<List<IUnit>, ErrorResponse>> AllUnits = IUnit.All();

        public static readonly List<Type> CipherTypes = CommonFuncs.GetCipherClasses();
    }
}
