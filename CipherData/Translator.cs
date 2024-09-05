using System.Reflection;

namespace CipherData
{
    /// <summary>
    /// Config class of translations
    /// </summary>
    public static class Translator
    {
        // GENERAL
        private const string _Attribute = "תכונה";
        private const string _Category = "קטגוריה";
        private const string _Comments = "הערות";
        private const string _Description = "תיאור";
        private const string _Id = "מספר סידורי";
        private const string _Operator = "אופרטור";
        private const string _Packages = "תעודות מוכלות";
        private const string _Process = "תהליך";
        private const string _Properties = "תכונות";
        private const string _Property = "תכונה";
        private const string _Status = "סטטוס";
        private const string _System = "מערכת";
        private const string _Value = "ערך";
        private const string _Vessel = "כלי";
        private const string _Unit = "יחידה";
        private const string _Worker = "שם עובד/ת";

        // AGGREGATE ITEM
        public const string AggregateItem_Attribute = _Attribute;
        public const string AggregateItem_As = "כינוי";
        public const string AggregateItem_Method = "פונקציה";

        // CATEGORY
        public const string Category_Children = "קטגוריות מוכלות";
        public const string Category_ConsumingProcesses = "תהליכים יוצרים";
        public const string Category_CreatingProcesses = "תהליכים צורכים";
        public const string Category_Description = _Description;
        public const string Category_IdMask = "סדרה";
        public const string Category_MaterialType = "סוג החומר";
        public const string Category_Name = _Category;
        public const string Category_Parent = "קטגוריית אב";
        public const string Category_Properties = _Properties;

        // CATEGORY PROPERTY
        public const string CategoryProperty_Name = _Property;
        public const string CategoryProperty_Description = _Description;
        public const string CategoryProperty_Type = "טיפוס";
        public const string CategoryProperty_Value = _Value;

        // CONDITION
        public const string Conditions = "תנאים";
        public const string Condition_Attribute = _Attribute;
        public const string Condition_Operator = _Operator;
        public const string Condition_Relation = "יחס";
        public const string Condition_Value = _Value;

        // EVENT
        public const string Event_ActionComments = "הערות הזנת טופס";
        public const string Event_Actions = "תעודות מעורבות";
        public const string Event_Comments = _Comments;
        public const string Event_Packages = "תעודות מעורבות";
        public const string Event_ProcessId = "מספר תהליך";
        public const string Event_Status = _Status;
        public const string Event_Timestamp = "סוג תנועה";
        public const string Event_Type = "תהליך תנועה";
        public const string Event_Worker = _Worker;

        // ERROR 
        public const string Error_Code = "קוד שגיאה";
        public const string Error_Message = "הודעת שגיאה";

        // OBJECT FACTORY
        public const string ObjectFactory_Aggregate = "אגרגטים";
        public const string ObjectFactory_Filter = "סינון לפי";
        public const string ObjectFactory_OrderBy = "סידור לפי";
        public const string ObjectFactory_GroupBy = "קיבוץ לפי";

        // ORDERED ITEM
        public const string OrderedItem_Attribute = _Attribute;
        public const string OrderedItem_Order = "אופן סידור";

        // PACKAGE
        public const string Package_BrutMass = "מסה ברוטו [גר']";
        public const string Package_Category = _Category;
        public const string Package_Children = _Packages;
        public const string Package_CreatedAt = "תאריך פתיחה";
        public const string Package_Description = _Description;
        public const string Package_DestinationProcesses = "ייעוד";
        public const string Package_Id = _Id;
        public const string Package_NetMass = "מסה נטו [גר']";
        public const string Package_Parent = "תעודת אב";
        public const string Package_Properties = _Properties;
        public const string Package_System = _System;
        public const string Package_Vessel = _Vessel;

        // PROCESS
        public const string Process_Definition = "הגדרה";
        public const string Process_End = "סיום";
        public const string Process_Events = "תנועות";
        public const string Process_Start = "התחלה";
        public const string Process_UncompletedSteps = "שלבים שטרם הושלמו";

        // PROCESS DEFINITION
        public const string ProcessDefinition_Description = _Description;
        public const string ProcessDefinition_Name = _Process;
        public const string ProcessDefinition_Steps = "שלבים";

        // PROCESS STEP DEFINITION
        public const string ProcessStepDefinition_Condition = "תנאי";
        public const string ProcessStepDefinition_Description = _Description;
        public const string ProcessStepDefinition_Name = "שלב";

        // RESOURCE
        public const string Resource_ClearenceLevel = "מידור";
        public const string Resource_Id = _Id;
        public const string Resource_Uuid = "מספר סידורי אוניברסלי";

        // REQUEST RESULT
        public const string RequestResult_200 = "OK";
        public const string RequestResult_400 = "שגיאה בבקשה";
        public const string RequestResult_401 = "אין הרשאה";
        public const string RequestResult_404 = "בעיה בכתובת המבוקשת";

        // SYSTEM
        public const string System_Children = "מערכות מוכלות";
        public const string System_Name = _System;
        public const string System_Description = _Description;
        public const string System_Parent = "מערכת אב";
        public const string System_Properties = _Properties;
        public const string System_Unit = _Unit;

        // UNIT
        public const string Unit_Children = "יחידות מוכלות";
        public const string Unit_Conditions = "מגבלות";
        public const string Unit_Description = _Description;
        public const string Unit_Name = _Unit;
        public const string Unit_Parent = "יחידת אב";
        public const string Unit_Properties = _Properties;
        public const string Unit_Systems = "מערכות";

        // USER ACTIONS
        public const string UserActions = "פעולות משתמש";
        public const string UserAction_ActionParameters = "פרמטרים שהשתנו";
        public const string UserAction_ActionType = "סוג פעולה";
        public const string UserAction_At = "תאריך פעולה";
        public const string UserAction_By = _Worker;
        public const string UserAction_Comments = _Comments;
        public const string UserAction_ObjectId = _Id;
        public const string UserAction_Status = _Status;

        // VESSEL
        public const string Vessel_Name = _Vessel;
        public const string Vessel_Packages = _Packages;
        public const string Vessel_System = _System;
        public const string Vessel_Type = "סוג";

        public static readonly Dictionary<string, string> EngToHebPairs = GetAllPairs();

        public static Dictionary<string,string> GetAllPairs()
        {
            Dictionary<string, string> result = new();

            // Get all public static fields (const fields are also static)
            var fields = typeof(Translator).GetFields(BindingFlags.Public | BindingFlags.Static);

            foreach (var field in fields)
            {
                // Check if the field is a constant (const fields are marked IsLiteral and not IsInitOnly)
                if (field.IsLiteral && !field.IsInitOnly && field.FieldType == typeof(string))
                {
                    var fieldName = field.Name;
                    var fieldValue = (string)field.GetRawConstantValue();

                    result.Add(fieldName, fieldValue);
                }
            }

            return result;
        }
    }
}
