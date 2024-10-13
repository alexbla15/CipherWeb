namespace CipherData.Interfaces
{
    public interface IProcess : IResource
    {
        /// <summary>
        /// a collection of steps that make a single definition
        /// </summary>
        IProcessDefinition Definition { get; set; }
        DateTime Start { get; set; }

        DateTime End { get; set; }

        /// <summary>
        /// Events taking place during a process
        /// </summary>
        List<IEvent> Events { get; set; }

        /// <summary>
        /// Uncompleted steps for completing the process
        /// </summary>
        List<IProcessStepDefinition> UncompletedSteps { get; set; }

        public new Dictionary<string, object?> ToDictionary()
        {
            return new()
            {
                [nameof(Id)] = Id,
                [nameof(Definition)] = Definition.Name,
                [nameof(Start)] = Start,
                [nameof(End)] = End,
                [nameof(UncompletedSteps)] = string.Join(";", UncompletedSteps.Select(x => x.Name).ToList()),
            };
        }

        public string Duration()
        {
            TimeSpan difference = End - Start;
            int days = difference.Days;
            int hours = difference.Hours;

            return $"{days} ימים, {hours} שעות";
        }

        // API-RELATED FUNCTIONS

        Task<Tuple<IProcess, ErrorResponse>> Get(string? id = null);

        /// <summary>
        /// All objects
        /// </summary>
        Task<Tuple<List<IProcess>, ErrorResponse>> All();

        /// <summary>
        /// Fetch all processes which contain the searched text
        /// </summary>
        Task<Tuple<List<IProcess>, ErrorResponse>> Containing(string SearchText);
    }
}
