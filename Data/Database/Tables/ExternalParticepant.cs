using System;
using System.Collections.Generic;

namespace Meeting_App.Data.Database.Tables
{
    public partial class ExternalParticepant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string CNIC { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string ParticipantType { get; set; }
        public string WorkingPlace { get; set; }
        public string JobTittle { get; set; }
        public string Profession { get; set; }
        public string Qualification { get; set; }
        public string Division { get; set; }
        public string District { get; set; }
        public string Tehsil { get; set; }
        public int? ScheduleId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdationDate { get; set; }
        public bool? RecordStatus { get; set; }
    }
}
