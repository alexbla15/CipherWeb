namespace CipherData.Models
{
    /// <summary>
    /// Groups of boolean conditions on a single object
    /// </summary>
    public class GroupedBooleanCondition : Condition
    {
        /// <summary>
        /// Any of BooleanCondition / GroupedBooleadCondition
        /// </summary>
        [HebrewTranslation(typeof(GroupedBooleanCondition), nameof(Conditions))]
        public IEnumerable<Condition> Conditions { get; set; } = new List<Condition>();

        /// <summary>
        /// Operator used to resolve the multiple condition results to a single boolean.
        /// </summary>
        [HebrewTranslation(typeof(GroupedBooleanCondition), nameof(Operator))]
        public Operator Operator { get; set; } = Operator.All;

        /// <summary>
        /// Create an identical object to this one.
        /// </summary>
        public GroupedBooleanCondition Copy()
        {
            return (GroupedBooleanCondition)MemberwiseClone();
        }

        /// <summary>
        /// Check if this object and other object are exactly the same
        /// </summary>
        public bool Equals(GroupedBooleanCondition? OtherObject)
        {
            if (OtherObject is null) return false;
            if (Operator != OtherObject.Operator) return false;

            if (Conditions.Count() != OtherObject.Conditions.Count()) return false;
            if (Conditions is List<BooleanCondition> boolConditions && OtherObject.Conditions is List<BooleanCondition> otherBoolConditions)
            {
                // check for same step names
                if (!boolConditions.Select(x => x.Attribute).ToHashSet().SetEquals(otherBoolConditions.Select(x => x.Attribute).ToList())) return false;
                // check for differences
                
                foreach (BooleanCondition cond in boolConditions)
                {
                    if (!cond.Equals(otherBoolConditions.Where(x => x.Attribute == cond.Attribute).First())) return false;
                }
            }
            else if (Conditions is List<GroupedBooleanCondition> grouped_boolConditions && OtherObject.Conditions is List<GroupedBooleanCondition> grouped_otherBoolConditions)
            {
                foreach (GroupedBooleanCondition cond in grouped_boolConditions)
                {
                    if (!cond.Equals(grouped_otherBoolConditions[grouped_boolConditions.IndexOf(cond)])) return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Method to check if field is applicable for this request
        /// </summary>
        public CheckField CheckConditions()
        {
            if (Conditions.Any())
            {
                if (Conditions.ToList()[0] is BooleanCondition)
                {
                    return CheckField.ListItems((List<BooleanCondition>)(object)Conditions, Translate(nameof(Conditions)));
                }
                else if(Conditions.ToList()[0] is GroupedBooleanCondition)
                {
                    return CheckField.ListItems((List<GroupedBooleanCondition>)(object)Conditions, Translate(nameof(Conditions)));
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

        /// <summary>
        /// Create a random object.
        /// </summary>
        public static GroupedBooleanCondition Random()
        {
            return new GroupedBooleanCondition();
        }
    }
}
