using System.Reflection;

namespace CipherData.Models
{
    /// <summary>
    /// Definition of a process - 
    /// a collection of steps that make a single definition
    /// </summary>
    [HebrewTranslation(nameof(ProcessDefinition))]
    public class ProcessDefinition : Resource, IProcessDefinition
    {
        private string? _Name = string.Empty;
        private string? _Description = string.Empty;

        [HebrewTranslation(typeof(ProcessDefinition), nameof(Name))]
        public string? Name
        {
            get => _Name;
            set => _Name = value?.Trim();
        }

        [HebrewTranslation(typeof(ProcessDefinition), nameof(Description))]
        public string? Description
        {
            get => _Description;
            set => _Description = value?.Trim();
        }

        [HebrewTranslation(typeof(ProcessDefinition), nameof(Steps))]
        public List<IProcessStepDefinition> Steps { get; set; } = new();

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}
