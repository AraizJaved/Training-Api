using System;
using System.Collections.Generic;

namespace Meeting_App.Data.Database.Tables
{
    public partial class TrainingWiseReport
    {
        public long Id { get; set; }
        public int? TrainingId { get; set; }
        public int? VenueId { get; set; }
        public string Title { get; set; }
        public string Cadre { get; set; }
        public string Venue { get; set; }
        public string Division { get; set; }
        public string District { get; set; }
        public string Tehsil { get; set; }
        public string TrainingType { get; set; }
        public string TrainingLevel { get; set; }
        public string TraingCategore { get; set; }
        public string SupportedBy { get; set; }
        public string OrganizedBy { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? StartDate { get; set; }
        public int? TotalTrainee { get; set; }
        public int? TotalTrainer { get; set; }
    }
}
