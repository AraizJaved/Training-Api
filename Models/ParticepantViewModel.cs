using Meeting_App.Data.Database.Tables;
using System;
using System.Collections.Generic;

namespace Meeting_App.Models
{
    public class ParticepantViewModel
    {
        public long Id { get; set; }       
        
        public string Departments { get; set; }
        public string EmployeeName { get; set; }
        public string CNIC { get; set; }

        public string Designation_Name { get; set; }
        public string WorkingHealthFacility { get; set; }       

        public string ParticipantStatus { get; set; }
        public List<ScheduleTrainingwithProfileView> TrainingList { get; set; }

    }
}
