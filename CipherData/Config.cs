using CipherData.Randomizer;
using CipherData.RequestsInterface;

namespace CipherData
{
    public static class Config
    {
        public static readonly ICategoriesRequests CategoriesRequests = new RandomCategoriesRequests();
        public static readonly IEventsRequests EventsRequests = new RandomEventsRequests();
        public static readonly IGenericRequests GenericRequests = new RandomGenericRequests();
        public static readonly ILogsRequests logsRequests = new RandomLogsRequests();
        public static readonly IPackagesRequests PackagesRequests = new RandomPackagesRequests();
        public static readonly IProcessesRequests ProcessesRequests = new RandomProcessesRequests();
        public static readonly IProcessDefinitionsRequests ProcessesDefinitionsRequests = new RandomProcessDefinitionsRequests();
        public static readonly IQueryRequests QueryRequests = new RandomQueryRequests();
        public static readonly ISystemsRequests SystemsRequests = new RandomSystemsRequests();
        public static readonly IUnitsRequests UnitsRequests = new RandomUnitsRequests();
        public static readonly IVesselsRequests VesselsRequests = new RandomVesselsRequests();
    }
}
