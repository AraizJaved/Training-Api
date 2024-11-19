using Meeting_App.Data.Database.Tables;
using System.Collections.Generic;

namespace Meeting_App.Models
{
    public class ReportViewModel
    {
        public List<TrainingWiseReport> TrainingWiseReportList { get; set; }
        public List<TraineeWiseReport> TraineeWiseReportList { get; set; }
        public List<TrainerWiseReport> TrainerWiseReportList { get; set; }
        public List<TrainingTypeWiseReport> TrainingTypeWiseReportList { get; set; }
        public List<CadreWiseReport> CadreWiseReportList { get; set; }
        public List<TrainingLevelWiseReport> TrainingLevelWiseReportList { get; set; }
        public List<VenueWiseReport> VenueWiseReportList { get; set; }
        public List<DepartmentWiseReport> DepartmentWiseReportList { get; set; }
        public List<TrainingCategoryWiseReport> TrainingCategoryWiseReport { get; set; }
        public List<ExternalParticipantWiseReport> ExternalParticipantWiseReport { get; set; }
    }
}
