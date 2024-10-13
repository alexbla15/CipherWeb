using System.Reflection;

namespace CipherData.Interfaces
{
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

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}
