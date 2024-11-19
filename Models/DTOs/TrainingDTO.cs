using Meeting_App.Data.Database.Tables;
using System.Collections.Generic;

namespace Meeting_App.Models.DTOs
{
    public class TrainingDTO
    {
        public List<EventCalender> TotalTrainings { get; set; }
        public List<GetTrainingListView> ScheduleTrainings{ get; set; }
        public List<GetTrainingListView> UnScheduleTrainings { get; set; }
        public List<GetTrainingListView> PendingTrainings { get; set; }
    }
}
