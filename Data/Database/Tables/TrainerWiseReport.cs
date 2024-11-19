using System;
using System.Collections.Generic;

namespace Meeting_App.Data.Database.Tables
{
    public partial class TrainerWiseReport
    {
        public int? ProfileId { get; set; }
        public string EmployeeName { get; set; }
        public string cnic { get; set; }
        public int? Designation_Id { get; set; }
        public string Designation_Name { get; set; }
        public string WorkingHealthFacility { get; set; }
        public int? TotalTrainings { get; set; }
    }
}
