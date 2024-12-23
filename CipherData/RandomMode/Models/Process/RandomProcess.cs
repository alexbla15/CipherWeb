﻿namespace CipherData.RandomMode
{
    public class RandomProcess : BaseProcess, IProcess
    {
        public RandomProcess()
        {
            Id = GetNextId();
            Definition = new RandomProcessDefinition();
            if (new Random().Next() == 0)
            {
                Events = Enumerable.Range(0, 3).Select(_ => new RandomTransferAmountEvent() as IEvent).ToList();
            }
            else
            {
                Events = Enumerable.Range(0, 3).Select(_ => new RandomRelocationEvent() as IEvent).ToList();
            }
            UncompletedSteps =
            Enumerable.Range(0, 3).Select(_ => new RandomProcessStepDefinition() as IProcessStepDefinition).ToList();
        }

        // STATIC METHODS

        /// <summary>
        /// Counts how many packages were created.
        /// </summary>
        private static int IdCounter { get; set; } = 0;

        /// <summary>
        /// Get the id of a new object
        /// </summary>
        /// <returns></returns>
        private static string GetNextId() => $"PR{++IdCounter:D3}";

        // API RELATED FUNCTIONS

        protected override IProcessesRequests GetRequests() => new RandomProcessesRequests();

        public override async Task<Tuple<List<IProcess>, ErrorResponse>> Containing(string? SearchText)
            => await All();

        public override async Task<Tuple<List<IProcess>, ErrorResponse>> ByDefinition(string definition_id)
            => await All();
    }
}
