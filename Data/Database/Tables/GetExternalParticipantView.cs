using System;
using System.Collections.Generic;

namespace Meeting_App.Data.Database.Tables
{
    public partial class GetExternalParticipantView
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public string Title { get; set; }
        public string TrainingType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string CNIC { get; set; }
        public string ParticipantType { get; set; }
        public string MobileNo { get; set; }
        public string WorkingPlace { get; set; }
        public string CreatedBy { get; set; }
        public bool? RecordStatus { get; set; }
        public string ParticipantStatus { get; set; }
        public string AttendanceStatus { get; set; }
        public string ObtainMarksBeforeTrainig { get; set; }
        public string ObtainMarksAfterTraining { get; set; }
        public string ParticipantTrainingStatus { get; set; }
    }
}
