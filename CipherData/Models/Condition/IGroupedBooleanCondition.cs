﻿namespace CipherData.Models
{
    public interface IGroupedBooleanCondition : ICondition
    {
        /// <summary>
        /// Any of BooleanCondition / GroupedBooleadCondition
        /// </summary>
        IEnumerable<ICondition> Conditions { get; set; }

        /// <summary>
        /// Operator used to resolve the multiple condition results to a single boolean.
        /// </summary>
        Operator Operator { get; set; }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckConditions()
        {
            if (Conditions.Any())
            {
                foreach (var cond in Conditions)
                {
                    Tuple<bool, string> result = Tuple.Create(true, string.Empty);
                    if (cond is BooleanCondition)
                    {
                        result = (cond as IBooleanCondition).Check();
                    }
                    else if (cond is GroupedBooleanCondition)
                    {
                        result = (cond as IGroupedBooleanCondition).Check();
                    }
                    return new CheckField(result.Item1, result.Item2);
                }
            }
            return new CheckField();

        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckConditions());
            return result.Check();
        }
    }
}