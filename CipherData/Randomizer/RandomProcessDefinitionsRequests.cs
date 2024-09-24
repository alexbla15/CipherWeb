using CipherData.Models;
using CipherData.RequestsInterface;

namespace CipherData.Randomizer
{
    public class RandomProcessDefinitionsRequests : IProcessDefinitionsRequests
    {
        public Tuple<List<ProcessDefinition>, ErrorResponse> GetProcessDefinitions()
        {
            return new RandomGenericRequests().Request(RandomData.RandomProcessDefinitions);
        }

        public Tuple<ProcessDefinition, ErrorResponse> CreateProcessDefinition(ProcessDefinitionRequest proc)
        {
            return new RandomGenericRequests().Request(proc.Create(ProcessDefinition.GetNextId()));
        }

        public Tuple<ProcessDefinition, ErrorResponse> GetProcessDefintion(string proc_id)
        {
            return new RandomGenericRequests().Request(RandomData.RandomProcessDefinition, canBeNotFound: true, canBadRequest: false);
        }

        public Tuple<ProcessDefinition, ErrorResponse> UpdateProcessDefinition(string id, ProcessDefinitionRequest proc)
        {
            return new RandomGenericRequests().Request(proc.Create(id), canBeNotFound: true);
        }
    }
}
