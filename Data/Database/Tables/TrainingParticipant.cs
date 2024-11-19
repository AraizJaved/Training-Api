using System;
using System.Collections.Generic;

namespace Meeting_App.Data.Database.Tables
{
    public partial class TrainingParticipant
    {
        public long Id { get; set; }
        public int ScheduleId { get; set; }
        public int ProfileId { get; set; }
        public string ParticipantType { get; set; }
        public string ParticipantStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AttendanceStatus { get; set; }
        public string HFatthetimetaining { get; set; }
        public int? ObtainMarksBeforeTrainig { get; set; }
        public int? ObtainMarksAfterTraining { get; set; }
        public int? TotalMarks { get; set; }
        public bool? IsReplaced { get; set; }
        public string ParticipantTrainingStatus { get; set; }
        public string WorkingPlaceAtTraining { get; set; }
        public bool? RecordStatus { get; set; }
    }
}
