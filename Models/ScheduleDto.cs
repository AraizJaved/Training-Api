﻿using Meeting_App.Data.Database.Tables;
using Meeting_App.Migrations;
using System;
using System.Collections.Generic;

namespace Meeting_App.Models
{
    public class ScheduleDto
    {

        public long Id { get; set; }
        public int? TrainingId { get; set; }
        public string Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Venue { get; set; }
        public string TrainingType { get; set; }
        public string TraingCategore { get; set; }
        public string Departments { get; set; }
        public string EmployeeName { get; set; }
        public string CNIC { get; set; }
        public string ParticipantType { get; set; }
        public string Description { get; set; }
        public List<TraineeInfo> TraineeList { get; set; }
        public List<TrainerInfo> TrainerList { get; set; }
        public List<GetExternalParticipantView> ExternalParticipants { get; set; }
        public string OrganizedBy { get; set; }
        public string SupportedBy { get; set; }
        public string TrainingLevel { get; set; }

        public int? TotalParticipants { get; set; }
        public string Cadre { get; set; }
        public bool? IsDigitized { get; set; }
        public string Division { get; set; }
        public string District { get; set; }
        public string Tehsil { get; set; }
        public string Month { get; set; }
        public int? Year { get; set; }
        public bool? Virtual { get; set; }
        public bool? VenueNotAvailable { get; set; }
        public string DesignationName { get; set; }

    }
    public class TraineeInfo
    {
        public int? ProfileId { get; set; }
        public long ScheduleId { get; set; }

        public string Cnic { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string AttendanceStatus { get; set; }
        public string WorkingPlace { get; set; }
        public string WorkedPlaceAtTraining { get; set; }
        public int? ObtainMarksAfterTraining { get; set; }
        public int? ObtainMarksBeforeTrainig { get; set; }
        public int? TotalMarks { get; set; }
        public string ParticipantTrainingStatus { get; set; }
        public string DesignationName { get; set; }


    }
    public class TrainerInfo
    {
        public int? ProfileId { get; set; }
        public long ScheduleId { get; set; }
        public string Cnic { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string AttendanceStatus { get; set; }
        public string WorkingPlace { get; set; }
        public string WorkedPlaceAtTraining { get; set; }
        public string DesignationName { get; set; }

    }
}
