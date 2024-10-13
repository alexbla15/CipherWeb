namespace CipherData.ApiMode
{
    public class ProcessDefinitionsRequests : IProcessDefinitionsRequests
    {
        private static readonly string path = "/processDefinitions";

        public async Task<Tuple<List<IProcessDefinition>, ErrorResponse>> GetProcessDefinitions()
            => await GeneralAPIRequest.GetAll<IProcessDefinition, ProcessDefinition>(path);

        public async Task<Tuple<IProcessDefinition, ErrorResponse>> GetProcessDefinition(string id)
        {
            var result = await GeneralAPIRequest.Get<ProcessDefinition>($"{path}/{id}");

            IProcessDefinition obj = result.Item1 ?? new ProcessDefinition();
            return Tuple.Create(obj, result.Item2);
        }

        public async Task<Tuple<IProcessDefinition, ErrorResponse>> CreateProcessDefinition(IProcessDefinitionRequest objRequest)
        {
            var result = await GeneralAPIRequest.Post<ProcessDefinition>(path, objRequest);

            IProcessDefinition obj = result.Item1 ?? new ProcessDefinition();
            return Tuple.Create(obj, (ErrorResponse)result.Item2);
        }

        public async Task<Tuple<IProcessDefinition, ErrorResponse>> UpdateProcessDefinition(string id, IProcessDefinitionRequest objRequest)
        {
            var result = await GeneralAPIRequest.Put<ProcessDefinition>($"{path}/{id}", objRequest);

            IProcessDefinition obj = result.Item1 ?? new ProcessDefinition();
            return Tuple.Create(obj, result.Item2);
        }
    }
}
