using System;
using System.Collections.Generic;

namespace Meeting_App.Data.Database.Tables
{
    public partial class ScheduleTrainingwithProfileView
    {
        public long Id { get; set; }
        public int? TrainingId { get; set; }
        public int? VenueId { get; set; }
        public string Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Venue { get; set; }
        public string TrainingType { get; set; }
        public string TraingCategore { get; set; }
        public string Departments { get; set; }
        public string EmployeeName { get; set; }
        public string CNIC { get; set; }
        public int? Designation_Id { get; set; }
        public string Designation_Name { get; set; }
        public string HealthFacility { get; set; }
        public string WorkingHFMISCode { get; set; }
        public string ParticipantType { get; set; }
        public int? ProfileId { get; set; }
        public bool? RecordStatus { get; set; }
        public string ParticipantStatus { get; set; }
        public string AttendanceStatus { get; set; }
        public string ParticipantTrainingStatus { get; set; }
        public string Description { get; set; }
        public int? ObtainMarksAfterTraining { get; set; }
        public int? ObtainMarksBeforeTrainig { get; set; }
        public int? TotalMarks { get; set; }
        public string WorkingPlaceAtTraining { get; set; }
        public string TrainingLevel { get; set; }
        public string OrganizedBy { get; set; }
        public string SupportedBy { get; set; }
        public int? TotalParticipants { get; set; }
        public string Cadre { get; set; }
        public bool? IsDigitized { get; set; }
        public string Month { get; set; }
        public int? Year { get; set; }
        public bool? Virtual { get; set; }
        public bool? VenueNotAvailable { get; set; }
        public string Division { get; set; }
        public string District { get; set; }
        public string Tehsil { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
