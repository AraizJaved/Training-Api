using System;
using System.Collections.Generic;

namespace Meeting_App.Data.Database.Tables
{
    public partial class ExternalParticepantWithSchedule
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int? ParticipantId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ParticipantStatus { get; set; }
        public string AttendanceStatus { get; set; }
        public string ObtainMarksBeforeTrainig { get; set; }
        public string ObtainMarksAfterTraining { get; set; }
        public string ParticipantTrainingStatus { get; set; }
    }
}
