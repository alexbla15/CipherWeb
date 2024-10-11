namespace CipherData.Models
{
    /// <summary>
    /// An instance of a specific processes
    /// </summary>
    [HebrewTranslation(nameof(Process))]
    public class Process : Resource, IProcess
    {
        private List<IEvent> _Events = new();

        [HebrewTranslation(typeof(Process), nameof(Definition))]
        public IProcessDefinition Definition { get; set; } = new ProcessDefinition();

        [HebrewTranslation(typeof(Process), nameof(Events))]
        public List<IEvent> Events
        {
            get => _Events;
            set
            {
                _Events = value;
                Start = Events.Select(x => x.Timestamp).Min();
                End = Events.Select(x => x.Timestamp).Max();
            }
        }

        [HebrewTranslation(typeof(Process), nameof(UncompletedSteps))]
        public List<IProcessStepDefinition> UncompletedSteps { get; set; } = new();

        [HebrewTranslation(typeof(Process), nameof(Start))]
        public DateTime Start { get; set; }

        [HebrewTranslation(typeof(Process), nameof(End))]
        public DateTime End { get; set; }
    }
}
