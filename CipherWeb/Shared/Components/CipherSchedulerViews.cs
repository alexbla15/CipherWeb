using CipherWeb.Data;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace CipherWeb.Shared.Components
{
    public partial class CipherDayView : RadzenDayView
    {
        public override string Icon => Icons.Time.Event;

        /// <inheritdoc />
        [Parameter]
        public override string Text { get; set; } = "יום";
    }

    public partial class CipherWeekView : RadzenWeekView
    {
        public override string Icon => "date_range";

        /// <inheritdoc />
        [Parameter]
        public override string Text { get; set; } = "שבוע";
    }

    public partial class CipherMonthView : RadzenMonthView
    {
        public override string Icon => Icons.Time.CalanderMonth;

        /// <inheritdoc />
        [Parameter]
        public override string Text { get; set; } = "חודש";
    }


    public partial class CipherYearPlannerView : RadzenYearPlannerView
    {
        public override string Icon => Icons.Time.CalendarToday;

        /// <inheritdoc />
        [Parameter]
        public override string Text { get; set; } = "שנה";
    }
}
