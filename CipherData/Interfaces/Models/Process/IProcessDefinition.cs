using System.Reflection;

namespace CipherData.Interfaces
{
    [HebrewTranslation(nameof(ProcessDefinition))]
    public interface IProcessDefinition : IResource
    {
        /// <summary>
        /// Description of process
        /// </summary>
        [HebrewTranslation(typeof(ProcessDefinition), nameof(Description))]
        string? Description { get; set; }

        /// <summary>
        /// Name of the process
        /// </summary>
        [HebrewTranslation(typeof(ProcessDefinition), nameof(Name))]
        string? Name { get; set; }

        /// <summary>
        /// All steps that are associated with this process
        /// </summary>
        [HebrewTranslation(typeof(ProcessDefinition), nameof(Steps))]
        List<IProcessStepDefinition> Steps { get; set; }

        // API-RELATED FUNCTIONS

        /// <summary>
        /// All objects
        /// </summary>
        Task<Tuple<List<IProcessDefinition>, ErrorResponse>> All();

        /// <summary>
        /// Method to create a new object from a request
        /// </summary>
        Task<Tuple<IProcessDefinition, ErrorResponse>> Create(IProcessDefinitionRequest req);

        /// <summary>
        /// Method to update object details 
        /// </summary>
        Task<Tuple<IProcessDefinition, ErrorResponse>> Update(string? id, IProcessDefinitionRequest req);

        /// <summary>
        /// Fetch all processes definitions which contain the searched text
        /// </summary>
        Task<Tuple<List<IProcessDefinition>, ErrorResponse>> Containing(string? SearchText);

        public IProcessDefinitionRequest Request() =>
            new ProcessDefinitionRequest()
            {
                Description= Description,
                Name= Name,
                Steps= Steps
            };

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }

    /// <summary>
    /// Definition of a process - 
    /// a collection of steps that make a single definition
    /// </summary>
    public abstract class BaseProcessDefinition : Resource, IProcessDefinition
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;

        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        public List<IProcessStepDefinition> Steps { get; set; } = new();

        // ABSTRACT METHODS

        protected abstract IProcessDefinitionsRequests GetRequests();

        public abstract Task<Tuple<List<IProcessDefinition>, ErrorResponse>> Containing(string? SearchText);

        // API RELATED FUNCTIONS

        public async Task<Tuple<IProcessDefinition, ErrorResponse>> Get(string? id) =>
            await GetRequests().GetById(id);

        public async Task<Tuple<List<IProcessDefinition>, ErrorResponse>> All() =>
            await GetRequests().GetAll();

        public async Task<Tuple<IProcessDefinition, ErrorResponse>> Create(IProcessDefinitionRequest req) =>
            await GetRequests().Create(req);

        public async Task<Tuple<IProcessDefinition, ErrorResponse>> Update(string? id, IProcessDefinitionRequest req)
            => await GetRequests().Update(id, req);
    }
}
