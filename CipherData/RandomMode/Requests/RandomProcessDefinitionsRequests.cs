namespace CipherData.RandomMode
{
    public class RandomProcessDefinitionsRequests : IProcessDefinitionsRequests
    {
        public async Task<Tuple<List<IProcessDefinition>, ErrorResponse>> GetProcessDefinitions()
            => await new RandomGenericRequests().Request(RandomData.ProcessDefinitions);

        public async Task<Tuple<IProcessDefinition, ErrorResponse>> CreateProcessDefinition(IProcessDefinitionRequest proc)
            => await new RandomGenericRequests().Request(proc.Create(RandomProcessDefinition.GetNextId()));

        public async Task<Tuple<IProcessDefinition, ErrorResponse>> GetProcessDefinition(string proc_id)
            => await new RandomGenericRequests().Request(RandomData.ProcessDefinition, canBeNotFound: true, canBadRequest: false);

        public async Task<Tuple<IProcessDefinition, ErrorResponse>> UpdateProcessDefinition(string id, IProcessDefinitionRequest proc)
            => await new RandomGenericRequests().Request(proc.Create(id), canBeNotFound: true);
    }
}
