using System.Collections.Generic;

namespace Meeting_App.Models.DTOs
{
    public class TraineeDto
    {

        public int ScheduleId { get; set; }
        public int profileId { get; set; }
        public List<profiles> Trainees { get; set; }
        public List<ExternalParticepantDto> ExternalParticipants { get; set; }

    }
    public class profiles
    {

        public int profileId { get; set; }
        public int Id { get; set; }
 

    }
}
