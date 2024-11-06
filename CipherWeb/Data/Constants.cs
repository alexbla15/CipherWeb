using Radzen;
using CipherData.Interfaces;
using CipherData.ApiMode;

namespace CipherWeb.Data
{
    public class Constants
    {
        public class Styles
        {
            public class Chips
            {
                public static readonly string Chip = "cipher-chip";
                public static readonly string CloseAll = "cipher-close";
                public static readonly string CloseItem = "cipher-close chip";
                public static readonly string Container = "cipher-chip-container";
            }

            public static readonly string TopNavBar = "cipher_topnavbar";

            public static readonly string Body = "cipher_body";

            public static readonly string Icon = "cipher_icon";
            public static readonly string SearchButtonIcon = "cipher_searchbtn_icon";

            public static readonly string ParagraphTitle = "cipher_paragraph_header";

            public static readonly string GroupHeader = "cipher_datagrid_group_header";

            public static readonly string CardTitle = "cipher_card_header";

            public static readonly string Restriction = "cipher_restriction";

            public static readonly string ComingSoon = "cipher-comingsoon";
            public static readonly string ComingSoonImage = "cipher-comingsoon-image-container";
            public static readonly string ComingSoonOverlay = "cipher-comingsoon-overlay";

            public static readonly string JsonCheck = "cipher-jsoncheck";

            public static readonly string DataTable = "cipher_datatable";
            public static readonly string DataGridEmpty = "cipher_datagrid_empty_text";

            public static readonly string LoadingText = "cipher_loading_text";

            public static readonly string ComponentCardContent = "cipher_component_card_content";
            public static readonly string ComponentCardHeader = "cipher_component_card_header";
            public static readonly string ComponentFooterContent = "cipher_component_card_content footer";
            public static readonly string ComponentResourceCardContent = "cipher_component_card_content resource_card";
            public static readonly string ComponentResourceCardContent_Centered = "cipher_component_card_content resource_card centered";
        }

        public class Button
        {
            public static readonly ButtonStyle Color = ButtonStyle.Primary;
            public static readonly Shade Shade = Shade.Darker;
        }

        public static readonly bool CheckJsons = true;

        public static readonly IWorker SetUser = new Worker() { Name = "אלכס בלחמן", Group=WorkerGroup.Manager};

        public static readonly string CompanyName = "היחידה להנדסת תהליך";
        public static readonly string DeveloperNames = "אלכס בלחמן ושחר פייט";

        public static readonly int RowsPerPage = 12;

        public static readonly DialogOptions SetDialogOptions = new() { Width = "800px", Height = "500px", Resizable = true, Draggable = true };

        public static readonly List<string> Lines = new() { "קו א", "קו ב", "קו ג" };
        public static readonly List<string> Processes = new() { "תהליך א", "תהליך ב", "תהליך ג" };
        public static readonly List<string> Reactants = new() { "מגיב א", "מגיב ב", "מגיב ג" };
        public static readonly List<string> Products = new() { "תוצר א", "תוצר ב", "תוצר ג" };

        public static readonly string[] UnallowedWords = { "SELECT", "INSERT", "UPDATE", "DELETE", "PUT", "POST", "GET" };

        public static readonly string StandardWidth = "300px";
    }
}
