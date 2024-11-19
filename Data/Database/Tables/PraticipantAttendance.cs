using System;
using System.Collections.Generic;

namespace Meeting_App.Data.Database.Tables
{
    public partial class PraticipantAttendance
    {
        public int Id { get; set; }
        public int? ParticipantId { get; set; }
        public int? ScheduleId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? DayNumber { get; set; }
    }
}
