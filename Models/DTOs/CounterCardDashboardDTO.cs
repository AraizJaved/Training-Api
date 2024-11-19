namespace Meeting_App.Models.DTOs
{
    public class CounterCardDashboardDTO
    {
        public int TotalTrainings{ get; set; }
        public int ScheduleTrainings{ get; set; }
        public int UnScheduleTrainings{ get; set; }
        public int PendingTrainings{ get; set; }
    }
}
