namespace CipherData.RandomMode
{
    public class RandomProcessesRequests : IProcessesRequests
    {
        public Task<Tuple<IProcess, ErrorResponse>> Create(IProcessDefinitionRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Tuple<List<IProcess>, ErrorResponse>> GetAll()
            => await new RandomGenericRequests().Request(RandomData.Processes);

        public async Task<Tuple<IProcess, ErrorResponse>> GetById(string? proc_id)
            => await new RandomGenericRequests().Request(RandomData.Process, canBeNotFound: true, canBadRequest: false);

        public Task<Tuple<IProcess, ErrorResponse>> Update(string? id, IProcessDefinitionRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
