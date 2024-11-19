using System;
using System.Collections.Generic;

namespace Meeting_App.Data.Database.Tables
{
    public partial class TrainingTypeWiseReport
    {
        public string TrainingType { get; set; }
        public int? TotalTrainings { get; set; }
    }
}
