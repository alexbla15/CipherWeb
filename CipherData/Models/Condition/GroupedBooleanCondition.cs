using System.Xml.Linq;

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
        public IEnumerable<Condition> Conditions { get; set; }

        /// <summary>
        /// Operator used to resolve the multiple condition results to a single boolean.
        /// </summary>
        [HebrewTranslation(typeof(GroupedBooleanCondition), nameof(Operator))]
        public Operator Operator { get; set; } = Operator.And;

        /// <summary>
        /// Instanciation of GroupedBooleanCondition
        /// </summary>
        /// <param name="conditions">Any of BooleanCondition / GroupedBooleadCondition</param>
        /// <param name="operator">Operator used to resolve the multiple condition results to a single boolean.</param>
        public GroupedBooleanCondition(IEnumerable<Condition> conditions, Operator @operator = Operator.And)
        {
            Conditions = conditions;
            Operator = @operator;
        }


        /// <summary>
        /// Checks for difference between this and another object
        /// </summary>
        /// <param name="OtherObject"></param>
        /// <returns></returns>
        public bool Compare(GroupedBooleanCondition? OtherObject)
        {

            bool different = false;

            different |= Operator != OtherObject?.Operator;

            if (Conditions.Count() == OtherObject?.Conditions.Count())
            {
                if (Conditions is List<BooleanCondition> boolConditions && OtherObject.Conditions is List<BooleanCondition> otherBoolConditions)
                {
                    // check for same step names
                    different |= !boolConditions.Select(x => x.Attribute).ToHashSet().SetEquals(otherBoolConditions.Select(x => x.Attribute).ToList());
                    // check for differences
                    if (!different)
                    {
                        foreach (BooleanCondition cond in boolConditions)
                        {
                            different |= cond.Compare(otherBoolConditions.Where(x => x.Attribute == cond.Attribute).First());
                        }
                    }
                }
                else if (Conditions is List<GroupedBooleanCondition> grouped_boolConditions && OtherObject.Conditions is List<GroupedBooleanCondition> grouped_otherBoolConditions)
                {
                    foreach (GroupedBooleanCondition cond in grouped_boolConditions)
                    {
                        different |= cond.Compare(grouped_otherBoolConditions[grouped_boolConditions.IndexOf(cond)]);
                    }
                }
            }
            else
            {
                different = true;
            }

            return different;
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
            return new GroupedBooleanCondition(
                conditions: new List<Condition>()
                );
        }

        /// <summary>
        /// Get an empty object scheme.
        /// </summary>
        public static GroupedBooleanCondition Empty()
        {
            return new GroupedBooleanCondition(
                conditions: new List<Condition>()
                );
        }

        public static string Translate(string searchedAttribute)
        {
            return Resource.Translate(typeof(GroupedBooleanCondition), searchedAttribute);
        }
    }
}
