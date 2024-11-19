using System;
using System.Collections.Generic;

namespace Meeting_App.Data.Database.Tables
{
    public partial class ExternalParticipantWiseReport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CNIC { get; set; }
        public string WorkingHealthFacility { get; set; }
        public string ParticipantType { get; set; }
        public int? TotalTrainings { get; set; }
    }
}
