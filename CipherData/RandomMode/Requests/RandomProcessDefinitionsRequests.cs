namespace CipherData.RandomMode
{
    public class RandomProcessDefinitionsRequests : IProcessDefinitionsRequests
    {
        public async Task<Tuple<List<IProcessDefinition>, ErrorResponse>> GetAll()
            => await new RandomGenericRequests().Request(RandomData.ProcessDefinitions);

        public async Task<Tuple<IProcessDefinition, ErrorResponse>> Create(IProcessDefinitionRequest proc)
            => await new RandomGenericRequests().Request(proc.Create(RandomProcessDefinition.GetNextId()));

        public async Task<Tuple<IProcessDefinition, ErrorResponse>> GetById(string? proc_id)
            => await new RandomGenericRequests().Request(new RandomProcessDefinition() { Id=proc_id} as IProcessDefinition, canBeNotFound: true, canBadRequest: false);

        public async Task<Tuple<IProcessDefinition, ErrorResponse>> Update(string? id, IProcessDefinitionRequest proc)
            => await new RandomGenericRequests().Request(proc.Create(id), canBeNotFound: true);
    }
}
