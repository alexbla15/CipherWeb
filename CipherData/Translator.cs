namespace CipherData
{
    /// <summary>
    /// Config class of translations
    /// </summary>
    public static class Translator
    {
        private static readonly Dictionary<string, string> AggregateItemTranslator = new()
        {
            ["Aggregate.Attribute"] = "תכונה",
            ["Aggregate.As"] = "כינוי",
            ["Aggregate.Method"] = "פונקציה",
        };

        private static readonly Dictionary<string, string> CategoryTranslator = new()
        {
            ["Category.Name"] = "שם הקטגוריה",
            ["Category.Description"] = "סדרה",
            ["Category.Properties"] = "תכונות",
            ["Category.CreatingProcesses"] = "תהליכים יוצרים",
            ["Category.ConsumingProcesses"] = "תהליכים צורכים",
            ["Category.MaterialType"] = "סוג החומר",
            ["Category.Parent"] = "קטגוריית אב",
            ["Category.Children"] = "קטגוריות מוכלות",
        };

        private static readonly Dictionary<string, string> CategoryPropertyTranslator = new()
        {
            ["CategoryProperty.Name"] = "שם תכונה", 
            ["CategoryProperty.Description"] = "תיאור", 
            ["CategoryProperty.Type"] = "טיפוס", 
            ["CategoryProperty.Value"] = "ערך", 
        };

        private static readonly Dictionary<string, string> ConditionTranslator = new()
        {
            ["Conditions"] = "תנאים",
            ["Condition.Attribute"] = "תכונה",
            ["Condition.Relation"] = "יחס",
            ["Condition.Operator"] = "אופרטור",
            ["Condition.Value"] = "ערך",
        };

        private static readonly Dictionary<string, string> EventTranslator = new()
        {
            ["Event.ActionComment"] = "הערות הזנת טופס",
            ["Event.Actions"] = "תעודות מעורבות",
            ["Event.Type"] = "סוג תנועה",
            ["Event.ProcessId"] = "מספר תהליך",
            ["Event.Comments"] = "הערות",
            ["Event.Timestamp"] = "תאריך תנועה",
            ["Event.Status"] = "סטטוס",
            ["Event.Packages"] = "תעודות מעורבות",
            ["Event.Worker"] = "שם עובד",
        };

        private static readonly Dictionary<string, string> ErrorTranslator = new()
        {
            ["Error.Code"] = "קוד שגיאה",
            ["Error.Message"] = "הודעת שגיאה",
        };

        private static readonly Dictionary<string, string> ObjectFactoryTranslator = new()
        {
            ["ObjectFactory.Aggregate"] = "אגרגטים",
            ["ObjectFactory.Filter"] = "מסנן",
            ["ObjectFactory.GroupBy"] = "קיבוץ לפי",
            ["ObjectFactory.OrderBy"] = "סידור לפי",
        };

        private static readonly Dictionary<string, string> OrderedItemTranslator = new()
        {
            ["Order.Attribute"] = "תכונה",
            ["Order.Order"] = "אופן סידור"
        };

        private static readonly Dictionary<string, string> PackageTranslator = new()
        {
            ["Package.Id"] = "מספר סידורי",
            ["Package.BrutMass"] = "תאריך פתיחה",
            ["Package.Category"] = "קטגוריה",
            ["Package.Children"] = "תעודות מוכלות",
            ["Package.CreatedAt"] = "מסה ברוטו [גר']",
            ["Package.Description"] = "תיאור",
            ["Package.DestinationProcesses"] = "ייעוד",
            ["Package.NetMass"] = "מסה נטו [גר']",
            ["Package.Parent"] = "תעודת אב",
            ["Package.Properties"] = "תכונות",
            ["Package.System"] = "מערכת",
            ["Package.Vessel"] = "כלי"
        };

        private static readonly Dictionary<string, string> ProcessTranslator = new()
        {
            ["Process.Definition"] = "הגדרה",
            ["Process.End"] = "סיום",
            ["Process.Events"] = "תנועות",
            ["Process.Start"] = "התחלה",
            ["Process.UncompletedSteps"] = "שלבים שטרם הושלמו",
        };

        private static readonly Dictionary<string, string> ProcessDefinitionTranslator = new()
        {
            ["ProcessDefinition.Description"] = "תיאור",
            ["ProcessDefinition.Name"] = "תהליך",
            ["ProcessDefinition.Steps"] = "שלבים",
        };

        private static readonly Dictionary<string, string> ProcessStepDefinitionTranslator = new()
        {
            ["ProcessStepDefinition.Name"] = "שלב",
            ["ProcessStepDefinition.Description"] = "תיאור",
            ["ProcessStepDefinition.Condition"] = "תנאי",
        };

        private static readonly Dictionary<string, string> ResourceTranslator = new()
        {
            ["Resource.ClearenceLevel"] = "מידור",
            ["Resource.Id"] = "מספר סידורי",
            ["Resource.Uuid"] = "מספר סידורי אוניברסלי",
        };

        private static readonly Dictionary<string, string> RequestResultTranslator = new()
        {
            ["Result.200"] = "OK",
            ["Result.400"] = "שגיאה בבקשה",
            ["Result.401"] = "אין הרשאה",
            ["Result.404"] = "בעיה בכתובת המבוקשת"
        };

        private static readonly Dictionary<string, string> SystemTranslator = new()
        {
            ["System.Children"] = "מערכות מוכלות",
            ["System.Description"] = "תיאור",
            ["System.Name"] = "מערכת",
            ["System.Parent"] = "מערכת אב",
            ["System.Properties"] = "תכונות",
            ["System.Unit"] = "יחידה",
        };

        private static readonly Dictionary<string, string> UnitTranslator = new()
        {
            ["Unit.Children"] = "יחידות מוכלות",
            ["Unit.Conditions"] = "מגבלות",
            ["Unit.Description"] = "תיאור",
            ["Unit.Name"] = "שם יחידה",
            ["Unit.Parent"] = "יחידת אב",
            ["Unit.Properties"] = "תכונות",
            ["Unit.Systems"] = "מערכות",
        };

        private static readonly Dictionary<string, string> UserActionTranslator = new()
        {
            ["UserActions"] = "פעולות משתמש",
            ["UserAction.ActionParameters"] = "פרמטרים שהשתנו",
            ["UserAction.ActionType"] = "סוג פעולה",
            ["UserAction.At"] = "תאריך פעולה",
            ["UserAction.By"] = "שם מבצע/ת",
            ["UserAction.Comments"] = "הערות",
            ["UserAction.ObjectId"] = "מספר סידורי",
            ["UserAction.Status"] = "סטטוס",
        };

        private static readonly Dictionary<string, string> VesselTranslator = new()
        {
            ["Vessel.Name"] = "כלי",
            ["Vessel.Packages"] = "תעודות מוכלות",
            ["Vessel.System"] = "מערכת",
            ["Vessel.Type"] = "סוג",
        };

        private static readonly List<Dictionary<string, string>> AllTranslators =
            new() {
                AggregateItemTranslator,
                ConditionTranslator,
                CategoryTranslator,
                CategoryPropertyTranslator,
                EventTranslator,
                ErrorTranslator,
                ObjectFactoryTranslator,
                OrderedItemTranslator,
                PackageTranslator,
                ProcessTranslator,
                ProcessDefinitionTranslator,
                ProcessStepDefinitionTranslator,
                ResourceTranslator,
                RequestResultTranslator,
                SystemTranslator,
                UnitTranslator,
                UserActionTranslator,
                VesselTranslator
            };

        public static readonly Dictionary<string, string> EngToHebPairs =
            AllTranslators.SelectMany(dict => dict).ToDictionary(pair => pair.Key, pair => pair.Value);
    }
}
