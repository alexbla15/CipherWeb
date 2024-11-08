using System;
using System.Reflection;

namespace CipherData.Interfaces
{
    /// <summary>
    /// An event of relocating several packages to a new location
    /// </summary>
    [HebrewTranslation(nameof(ICreateRelocationEvent))]
    public interface ICreateRelocationEvent : ICipherClass
    {
        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        [HebrewTranslation(typeof(IEvent), nameof(IEvent.Comments))]
        string? Comments { get; set; }

        /// <summary>
        /// Package that relocate in this event.
        /// </summary>
        [HebrewTranslation(typeof(ICreateRelocationEvent), nameof(Packages))]
        List<IPackage>? Packages { get; set; }

        /// <summary>
        /// System to which the packages are relocated.
        /// </summary>
        [HebrewTranslation(typeof(ICreateRelocationEvent), nameof(TargetSystem))]
        [Check(CheckRequirement.Required)]
        IStorageSystem? TargetSystem { get; set; }

        /// <summary>
        /// Timestamp when the event happend. Required
        /// </summary>
        [HebrewTranslation(typeof(IEvent), nameof(IEvent.Timestamp))]
        DateTime Timestamp { get; set; }

        /// <summary>
        /// Name of worker that fulfilled the form
        /// </summary>
        [HebrewTranslation(typeof(IEvent), nameof(IEvent.Worker))]
        string? Worker { get; set; }
        
        public CheckField CheckPackages()
        {
            CheckField result = CheckField.CheckList(Packages,
                Translate(nameof(Packages)), isFull: true, isRequired: true);
            return result.Succeeded ? CheckField.Distinct(Packages?.Select(x => x.Id).ToList(),
                Translate(nameof(Packages))) : result;
        }

        public CheckField CheckTargetSystem() => CheckProperty(this, nameof(TargetSystem));

        public CheckField CheckTargetSystemDifferent()
        {
            if (Packages is null) return CheckPackages();

            CheckField result = new();

            foreach (IPackage p in Packages)
            {
                result = result.Succeeded ? CheckField.NotEq(p?.System.Id, TargetSystem?.Id,
                    Translate(nameof(TargetSystem))) : result;
            }

            return result;
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            CheckClass result = new();
            result.Fields.Add(CheckPackages());
            result.Fields.Add(CheckTargetSystem());
            result.Fields.Add(CheckTargetSystemDifferent());

            Tuple<bool, string> SpecificEventCheck = result.Check();
            return SpecificEventCheck.Item1 ? Create().Check() : SpecificEventCheck;
        }

        /// <summary>
        /// Create a general CreateEvent object, out of this object parameters.
        /// </summary>
        public ICreateEvent Create()
        {
            var ev = new CreateEvent()
            {
                Worker = Worker,
                Timestamp = Timestamp,
                EventType = 24,
                Comments = Comments,
            };

            if (Packages != null && TargetSystem != null)
            {
                List<IPackage> changedPacks = ChangeLocations();
                ev.Actions = changedPacks.Select(x => x.Request()).ToList();
            }

            return ev;
        }

        public List<IPackage> ChangeLocations()
        {
            List<IPackage>? relocatedPacks = 
                Packages?.Select(x => Copy(x)).Where(x=>x != null).ToList();

            if (relocatedPacks != null && TargetSystem != null)
            {
                foreach (IPackage? p in relocatedPacks)
                {
                    if (p != null) p.System = TargetSystem;
                }
            }
            return relocatedPacks ?? new();
        }

        // STATIC METHODS

        public static string Translate(string text) => Translate(MethodBase.GetCurrentMethod()?.DeclaringType, text);
    }
}
