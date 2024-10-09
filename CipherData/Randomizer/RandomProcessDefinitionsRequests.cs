using CipherData.Models;
using CipherData.RequestsInterface;
using CipherData.Models.Randomizers;

namespace CipherData.Randomizer
{
    public class RandomProcessDefinitionsRequests : IProcessDefinitionsRequests
    {
        public Tuple<List<IProcessDefinition>, ErrorResponse> GetProcessDefinitions()
            => new RandomGenericRequests().Request(RandomData.ProcessDefinitions);

        public Tuple<IProcessDefinition, ErrorResponse> CreateProcessDefinition(ProcessDefinitionRequest proc)
            => new RandomGenericRequests().Request(proc.Create(RandomProcessDefinition.GetNextId()));

        public Tuple<IProcessDefinition, ErrorResponse> GetProcessDefintion(string proc_id)
            => new RandomGenericRequests().Request(RandomData.ProcessDefinition, canBeNotFound: true, canBadRequest: false);

        public Tuple<IProcessDefinition, ErrorResponse> UpdateProcessDefinition(string id, ProcessDefinitionRequest proc)
            => new RandomGenericRequests().Request(proc.Create(id), canBeNotFound: true);
    }
}
