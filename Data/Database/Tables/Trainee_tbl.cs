using System;
using System.Collections.Generic;

namespace Meeting_App.Data.Database.Tables
{
    public partial class Trainee_tbl
    {
        public int Id { get; set; }
        public int ScheduleId { get; set; }
        public int ProfileId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
