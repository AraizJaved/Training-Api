using Meeting_App.Data.Database.Tables;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meeting_App.Models.DTOs
{
    public class EventCalenderView
    {
        public int? Id { get; set; }
        public int? TrainingId { get; set; }
        public string Title { get; set; }
        public string Cadre { get; set; }
        public string TrainingType { get; set; }
        public string TrainingCategory { get; set; }
        public string Description { get; set; }    
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string Attachment { get; set; }
        public bool? RecordStatus { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdationDate { get; set; }
        public List<IFormFile>? MeetingAttachments { get; set; }
        public string Organizer { get; set; }
        public int? VenueId { get; set; }
        public bool? IsDeleted { get; set; }
        public int? TotalParticipants { get; set; }
        public string? Month{ get; set; }
        public int? Year { get; set; }
        public bool? Virtual { get; set; }
        public bool? VenueNotAvailable { get; set; }
        public tbl_MeetingVenue? VenueName { get; set; }
      

        public List<EventParticipantView> EventParticipant = new List<EventParticipantView>();
        public string Departments { get; set; }
        public string Division { get; set; }
        public string District { get; set; }
        public string Tehsil { get; set; }
        public string OrganizedBy { get; set; }
        public string SupportedBy { get; set; }
        public string TrainingLevel { get; set; }
        public bool? IsVirtual { get; set; }
        public bool? IsVenue { get; set; }
        public bool? IsDigitized { get; set; }
    }

    public class EventParticipantView
    {
        public int? Id { get; set; }
        public int? EventId { get; set; }
        public Guid? ParticipantId { get; set; }
        public string ParticipantName { get; set; }
        public string ParticipantFullName { get; set; }
    }

    public class EventFilter
    {
        //public DateTime? StartDate { get; set; }
        //public DateTime? EndDate { get; set; }
        public Guid? UserId { get; set; }
        public bool? RecordStatus { get; set; }
        public bool ShowMyEvent { get; set; }
        public string MeetingStatus { get; set; }
        public bool? IsDeleted { get; set; }
    }



    public class MeetingVenueDTO
    {
        public int Id { get; set; }
        public string Venue { get; set; }
        public int? OderBy { get; set; }
        public bool? RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdationDate { get; set; }
    } 
    public class ScheduleDTO
    {
        public long Id { get; set; }
        public int? TrainingId { get; set; }
        public int? VenueId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Division { get; set; }
        public string District { get; set; }
        public string Tehsil { get; set; }
        public string OrganizedBy { get; set; }
        public string SupportedBy { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdationDate { get; set; }
        public bool? IsVirtual { get; set; }

    }   
    public class ExternalParticepantDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string CNIC { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string ParticipantType { get; set; }
        public string WorkingPlace { get; set; }
        public string JobTittle { get; set; }
        public string Profession { get; set; }
        public string Qualification { get; set; }
        public string Division { get; set; }
        public string District { get; set; }
        public string Tehsil { get; set; }
        public int ScheduleId { get; set; }

    }
    public class TrainingCategoryDTO
    {
        public int Id { get; set; }
        public string TraingCategoryName { get; set; }
        public int? OderBy { get; set; }
        public bool? RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdationDate { get; set; }
    }
    public class OrganizedByDTO
    {
        public int Id { get; set; }
        public string OrganizedBy { get; set; }
        public int? OderBy { get; set; }
        public bool? RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdationDate { get; set; }
    }public class TrainingLevelDTO
    {
        public int Id { get; set; }
        public string TrainingLevel { get; set; }
        public int? OderBy { get; set; }
        public bool? RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdationDate { get; set; }
    }
    public class SupportedByDTO
    {
        public int Id { get; set; }
        public string SupportedBy { get; set; }
        public int? OderBy { get; set; }
        public bool? RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdationDate { get; set; }
    } public class QualificationDTO
    {
        public int Id { get; set; }
        public string Qualification { get; set; }
        public int? OderBy { get; set; }
        public bool? RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdationDate { get; set; }
    } 
    public class TrainingTypeDTO
    {
        public int Id { get; set; }
        public string TrainingTypeName { get; set; }
        public int? OderBy { get; set; }
        public bool? RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdationDate { get; set; }
    }
    public class DocumentTypeDTO
    {
        public int Id { get; set; }
        public string DocumentType { get; set; }
        public int? OderBy { get; set; }
        public bool? RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdationDate { get; set; }
    }



    public class CadreDTO
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public bool? RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdationDate { get; set; }
    }

    public class MeetingOrganizerDTO
    {
        public int Id { get; set; }
        public string Organizer { get; set; }
        public int? OderBy { get; set; }
        public bool? RecordStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdationDate { get; set; }
    }


    public class DDLMeetingStatusModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }


    public class MeetingCount
    {

        public int Total { get; set; }
        public int Active { get; set; }
        public int InActive { get; set; }
        public int Postponed { get; set; }
        public int Rescheduled { get; set; }
        public int Cancelled { get; set; }
       
        public int Archived { get; set; }
    }


    public class MeetingDetailDTO
    {
        //public int Id { get; set; }
        public int MeetingId { get; set; }
       
      
       // public int MeetingStatus { get; set; }
        public int MeetingStatusId { get; set; }
       // public string MeetingStatusName { get; set; }
      
   
    }
}
