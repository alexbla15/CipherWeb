namespace CipherData.ApiMode
{
    public class ProcessesRequests : IProcessesRequests
    {
        private static readonly string path = "/processes";

        public async Task<Tuple<List<IProcess>, ErrorResponse>> GetAll()
            => await GeneralAPIRequest.GetAll<IProcess, Process>(path);

        public async Task<Tuple<IProcess, ErrorResponse>> GetById(string? id)
        {
            var result = await GeneralAPIRequest.Get<Process>($"{path}/{id}");

            IProcess obj = result.Item1 ?? new Process();
            return Tuple.Create(obj, result.Item2);
        }

        public Task<Tuple<IProcess, ErrorResponse>> Create(IProcessDefinitionRequest request)
            => throw new NotImplementedException();

        public Task<Tuple<IProcess, ErrorResponse>> Update(string? id, IProcessDefinitionRequest request)
            => throw new NotImplementedException();
    }
}
