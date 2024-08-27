using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CipherData.Models
{
    /// <summary>
    /// Create new event
    /// </summary>
    public class CreateEvent
    {
        /// <summary>
        /// Type of event. Required
        /// </summary>
        public int EventType { get; set; }

        /// <summary>
        /// Process ID of process containing to this even. If null, tries to estimate it from event details
        /// </summary>
        public int? ProcessId { get; set; }

        /// <summary>
        /// Free-text comments on the event
        /// </summary>
        public string? Comments { get; set; }

        /// <summary>
        /// Timestamp when the event happend. Required
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// List of affected packages from actions, the items present the state of each package after the event
        /// </summary>
        public HashSet<PackageRequest> Actions { get; set; }

        /// <summary>
        /// Create new event
        /// </summary>
        /// <param name="eventType">Type of event. Required</param>
        /// <param name="processId">Process ID of process containing to this even. If null, tries to estimate it from event detailst</param>
        /// <param name="comments">Free-text comments on the event</param>
        /// <param name="timestamp">Timestamp when the event happend. Required</param>
        /// <param name="actions">List of affected packages from actions, the items present the state of each package after the event</param>
        public CreateEvent(DateTime timestamp, int eventType, HashSet<PackageRequest> actions, int? processId = null, string? comments = null)
        {
            EventType = eventType;
            ProcessId = processId;
            Comments = comments;
            Timestamp = timestamp;
            Actions = actions;
        }

        /// <summary>
        /// Return an empty CreateEvent object scheme.
        /// </summary>
        public static CreateEvent Empty()
        {
            return new CreateEvent(timestamp: DateTime.Now, eventType: 0, actions: new HashSet<PackageRequest>());
        }

        /// <summary>
        /// Create new event (Use for one-package-event)
        /// </summary>
        /// <param name="eventType">Type of event. Required</param>
        /// <param name="processId">Process ID of process containing to this even. If null, tries to estimate it from event detailst</param>
        /// <param name="comments">Free-text comments on the event</param>
        /// <param name="timestamp">Timestamp when the event happend. Required</param>
        /// <param name="action">affected package, the item present the state of package after the event</param>
        public CreateEvent(DateTime timestamp, int eventType, PackageRequest action, int? processId = null, string? comments = null)
        {
            EventType = eventType;
            ProcessId = processId;
            Comments = comments;
            Timestamp = timestamp;
            Actions = new HashSet<PackageRequest>() { action };
        }

        /// <summary>
        /// Check if all required values are within the request, before sending it to the api.
        /// Item1 is the validity answer, Item2 is the problematic attribute.
        /// </summary>
        /// <returns></returns>
        public Tuple<bool, string> Check()
        {
            Tuple<bool, string> result = new Tuple<bool, string>(true, string.Empty);
            List<Tuple<bool, string>> actionsCheck = Actions.Select(x=>x.Check()).ToList();

            result = (EventType > 0) ? result : Tuple.Create(false, Event.Translate(nameof(RandomData.RandomEvent.EventType))); // event type is required
            result = (Timestamp > DateTime.Parse("01/01/1900") && Timestamp <= DateTime.Now) ? result : 
                Tuple.Create(false, Event.Translate(nameof(RandomData.RandomEvent.Timestamp)));
            result = (Actions.Count > 0 && !actionsCheck.Any(x=>x.Item1 == false)) ? result :
                Tuple.Create(false, actionsCheck.Where(x => x.Item1 == false).First().Item2);

            return result;
        }

        /// <summary>
        /// Transfrom this object to JSON, readable by API
        /// </summary>
        /// <returns></returns>
        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true, // Pretty print
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // Ensure special characters are preserved
                Converters = { new JsonDateTimeConverter() } // Include custom DateTime converter
            };

            return JsonSerializer.Serialize(this, options);
        }

        // Custom DateTime converter
        public class JsonDateTimeConverter : JsonConverter<DateTime>
        {
            private readonly string _dateTimeFormat = "yyyy-MM-dd HH:mm"; // Format excluding seconds

            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                return DateTime.ParseExact(reader.GetString(), _dateTimeFormat, CultureInfo.InvariantCulture);
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString(_dateTimeFormat));
            }
        }
    }
}
