using System;
using System.Collections.Generic;

namespace Meeting_App.Data.Database.Tables
{
    public partial class CounterDashboardView
    {
        public int? TotalTrainings { get; set; }
        public int? ScheduleTrainings { get; set; }
        public int? UnScheduleTrainings { get; set; }
        public int? PendingTrainings { get; set; }
    }
}
