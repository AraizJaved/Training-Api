using System;
using System.Collections.Generic;

namespace Meeting_App.Data.Database.Tables
{
    public partial class GetTrainingListView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TrainingType { get; set; }
        public string TrainingLevel { get; set; }
        public string TraingCategore { get; set; }
        public string Cadre { get; set; }
        public string Departments { get; set; }
        public int? TotalParticipants { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? RecordStatus { get; set; }
    }
}
