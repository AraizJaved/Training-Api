using AutoMapper;
using Meeting_App.Data.Database.Context;
using Meeting_App.Data.Database.Tables;
using Meeting_App.Models;
using Meeting_App.Models.DTOs;
using Meeting_App.Service.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.DirectoryServices.ActiveDirectory;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Meeting_App.Service
{
    public class EventServices
    {

        #region Fields
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly NotificationService _notificationService = new NotificationService();
        #endregion

        #region Constructors
        public EventServices(IMapper mapper, IWebHostEnvironment env)
        {

            _mapper = mapper;
            _env = env;
        }
        #endregion

        #region AddEvent
        public async Task<EventCalenderView> AddPreviousTraining(EventCalenderView model, string userid)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (model.Id != 0)
                        {
                            var res = db.EventCalender.Where(x => x.Id == model.TrainingId).FirstOrDefault();
                            res.Title = model.Title;
                            res.Cadre = model.Cadre;
                            res.TrainingType = model.TrainingType;
                            res.TraingCategore = model.TrainingCategory;
                            res.Departments = model.Departments;
                            res.IsDeleted = false;
                            res.Description = model.Description;
                            res.CreatedBy = Guid.Parse(userid);
                            res.CreationDate = UtilService.GetPkCurrentDateTime();
                            res.OrganizedBy = model.OrganizedBy;
                            res.SupportedBy = model.SupportedBy;
                            res.TrainingLevel = model.TrainingLevel;
                            //res.IsPrevious = true;
                            res.IsDigitized = model.IsDigitized;
                            res.TotalParticipants = model.TotalParticipants;
                            res.Month = model.Month;
                            res.Year = model.Year;
                            res.Virtual = model.Virtual;
                            res.VenueNotAvailable = model.VenueNotAvailable;
                            res.RecordStatus = true;


                            var schedule = db.ScheduleTraining.Where(x=>x.TrainingId == model.TrainingId).FirstOrDefault();
                            schedule.StartDate = model.StartDateTime;
                            schedule.EndDate = model.EndDateTime;
                            schedule.IsVirtual = model.IsVirtual;
                            if (model.IsVirtual == true)
                                schedule.VenueId = 84;
                            else
                                schedule.VenueId = model.VenueId;
                            if (model.IsVenue == true)
                                schedule.VenueId = 85;
                            else
                                schedule.VenueId = model.VenueId;



                            schedule.Division = model.Division;
                            schedule.District = model.District;
                            schedule.Tehsil = model.Tehsil;
                            schedule.OrganizedBy = model.OrganizedBy;
                            schedule.SupportedBy = model.SupportedBy;
                            schedule.RecordStatus = true;

                            //await db.EventCalender.AddAsync(res);

                        }
                        else
                        {
                            var eventCalender = new EventCalender();
                            eventCalender.Title = model.Title;
                            eventCalender.Cadre = model.Cadre;
                            eventCalender.TrainingType = model.TrainingType;
                            eventCalender.TraingCategore = model.TrainingCategory;
                            eventCalender.Departments = model.Departments;
                            eventCalender.IsDeleted = false;
                            eventCalender.Description = model.Description;
                            eventCalender.CreatedBy = Guid.Parse(userid);
                            eventCalender.CreationDate = UtilService.GetPkCurrentDateTime();
                            eventCalender.OrganizedBy = model.OrganizedBy;
                            eventCalender.SupportedBy = model.SupportedBy;
                            eventCalender.TrainingLevel = model.TrainingLevel;
                            eventCalender.IsPrevious = true;
                            eventCalender.IsDigitized = model.IsDigitized;
                            eventCalender.TotalParticipants = model.TotalParticipants;
                            eventCalender.Month = model.Month;
                            eventCalender.Year = model.Year;
                            eventCalender.Virtual = model.Virtual;
                            eventCalender.VenueNotAvailable = model.VenueNotAvailable;
                            eventCalender.RecordStatus = true;
                            await db.EventCalender.AddAsync(eventCalender);
                            db.SaveChanges();
                            var schedule = new ScheduleTraining();
                            schedule.TrainingId = eventCalender.Id;
                            schedule.StartDate = model.StartDateTime;
                            schedule.EndDate = model.EndDateTime;
                            schedule.IsVirtual = model.IsVirtual;
                            if (model.IsVirtual == true)
                                schedule.VenueId = 84;
                            else
                                schedule.VenueId = model.VenueId;
                            if (model.IsVenue == true)
                                schedule.VenueId = 85;
                            else
                                schedule.VenueId = model.VenueId;



                            schedule.Division = model.Division;
                            schedule.District = model.District;
                            schedule.Tehsil = model.Tehsil;
                            schedule.OrganizedBy = model.OrganizedBy;
                            schedule.SupportedBy = model.SupportedBy;
                            schedule.RecordStatus = true;
                            model.Id = eventCalender.Id;
                            schedule.IsPrevious = true;

                            await db.ScheduleTraining.AddAsync(schedule);
                        }
                       
                        db.SaveChanges();
                        trans.Commit();
                        return model;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        trans.Rollback();
                        throw;
                    }
                }


            }
        }
        public async Task<EventCalenderView> AddEvent(EventCalenderView model, string userid)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        if (model.Id != 0)
                        {
                            var res = db.EventCalender.Where(x=>x.Id== model.Id).FirstOrDefault();
                            res.Title = model.Title;
                            res.Cadre = model.Cadre;
                            res.TrainingType = model.TrainingType;
                            res.TraingCategore = model.TrainingCategory;
                            res.Departments = model.Departments;
                            res.Description = model.Description;
                            res.StartDateTime = model.StartDateTime;
                            res.CreatedBy = Guid.Parse(userid);
                            res.IsDeleted = false;
                            res.CreationDate = UtilService.GetPkCurrentDateTime();
                            res.OrganizedBy = model.OrganizedBy;
                            res.SupportedBy = model.SupportedBy;
                            res.TrainingLevel = model.TrainingLevel;
                            res.RecordStatus = true;
                            db.SaveChanges();
                            trans.Commit();

                        }
                        else
                        {
                            var eventCalender = new EventCalender();
                            if(string.IsNullOrEmpty(model.Title))
                            {
                                Console.WriteLine("Title is null ");
                                return null; 
                            }
                            var eventCalenderExist = db.EventCalender.Where(x=>x.Title == model.Title ).FirstOrDefault();
                            if(eventCalenderExist == null)
                            {
                                eventCalender.Title = model.Title;
                                eventCalender.Cadre = model.Cadre;
                                eventCalender.TrainingType = model.TrainingType;
                                eventCalender.TraingCategore = model.TrainingCategory;
                                eventCalender.Departments = model.Departments;
                                eventCalender.Description = model.Description;
                                eventCalender.StartDateTime = model.StartDateTime;
                                eventCalender.CreatedBy = Guid.Parse(userid);
                                eventCalender.IsDeleted = false;
                                eventCalender.CreationDate = UtilService.GetPkCurrentDateTime();
                                eventCalender.OrganizedBy = model.OrganizedBy;
                                eventCalender.SupportedBy = model.SupportedBy;
                                eventCalender.TrainingLevel = model.TrainingLevel;
                                eventCalender.RecordStatus = true;
                                await db.EventCalender.AddAsync(eventCalender);
                                model.Id = eventCalender.Id;
                                db.SaveChanges();
                                trans.Commit();
                            }
                            else
                            {
                                Console.WriteLine("Title is already exists");
                            }
                        }
                        return model;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        trans.Rollback();
                        throw;
                    }
                }


            }
        }
        #endregion
        #region UpdateAddEvent


        public List<EventCalender> GetEvents(EventFilter filter)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (string.IsNullOrEmpty(filter.MeetingStatus))
                        {
                            var result = db.EventCalender.Where(x => x.RecordStatus != false && x.IsDeleted != true && x.IsPrevious == null).ToList();
                            return result;
                        }
                        else
                        {
                            var result = db.EventCalender.Where(x => x.RecordStatus != false && x.IsDeleted != true && x.IsPrevious == null && x.Title.Contains(filter.MeetingStatus)).ToList();
                            return result;
                        }
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }


            }
        }
        public List<EventCalender> GetPreviousTrainings()
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        var result = db.EventCalender.Where(x => x.IsPrevious == true && x.RecordStatus == true).ToList();



                        return result;


                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }


            }
        }
        public List<EventCalender> GetTrainings(Guid userid ,string Sreach)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (string.IsNullOrEmpty(Sreach))
                        {
                            var result = db.EventCalender.Where(x => x.CreatedBy == userid && x.RecordStatus == true).ToList();
                            return result;
                        }
                        else
                        {
                            var result = db.EventCalender.Where(x => x.CreatedBy == userid && x.RecordStatus == true && x.Title.Contains(Sreach)).ToList();
                            return result;

                        }


                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }


            }
        }

        #endregion





        #region HelperMethods
        public async Task<string> UploadFile(IFormFile file)
        {
            try
            {
                string filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                filename = this.EnsureCorrectFilename($"{Guid.NewGuid().ToString("N")}_{DateTime.UtcNow.AddHours(5).ToString("ddMMyyyyHHmmssffffff")}");
                filename = filename + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string path = _env.WebRootPath + "/Uploads/" + filename;


                using (FileStream output = System.IO.File.Create(path))
                    await file.CopyToAsync(output);



                return filename;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }


        #endregion
        //public SMSViewModel SendSMSTelenor(SMSViewModel sms)
        //{
        //    string userName = "923464950571";
        //    string password = "Hisdu%40012_p%26shd-Hisdu%2Fp%26shd";

        //    SMSSendingService service = new SMSSendingService(userName, password);
        //    sms.Mask = sms.Mask == null ? "PSHD" : sms.Mask;
        //    sms.Sender = userName;
        //    sms = service.SendQuickMessage(sms);

        //    return sms;
        //}

        public SMSViewModel SendSMSUfone(SMSViewModel sms)
        {
            SMSSendingService service = new SMSSendingService();
            sms.Mask = sms.Mask == null ? "PSHD" : sms.Mask;
            //sms.Mask = sms.Mask == null ? "P%26SHD" : sms.Mask;
            //sms.Sender = "03315519747";
            sms.Sender = "03018482714";
            sms = service.SendQuickMessage(sms);
            return sms;
        }


        public SMSViewModel SendSMS(SMSViewModel sms)
        {
            return SendSMSUfone(sms);
        }


        #region Venue
        public int AddVenue(MeetingVenueDTO model, string userid)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        var newMeetingVenue = this._mapper.Map<tbl_MeetingVenue>(model);

                        newMeetingVenue.RecordStatus = true;
                        newMeetingVenue.CreatedBy = userid;
                        newMeetingVenue.CreationDate = UtilService.GetPkCurrentDateTime();

                        newMeetingVenue.Venue = model.Venue;
                        newMeetingVenue.OderBy = 100;








                        db.tbl_MeetingVenue.Add(newMeetingVenue);

                        db.SaveChanges();











                        trans.Commit();
                        return 1;




                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }


            }
        }

        public List<tbl_MeetingVenue> GetVenue()
        {
            try
            {
                using (var db = new IDDbContext())
                {

                    return db.tbl_MeetingVenue.ToList();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion

        #region TraingCategory
        public int AddTraingCategory(TrainingCategoryDTO model, string userid)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        var newTraingCategory = this._mapper.Map<tbl_TrainingCategory>(model);

                        newTraingCategory.RecordStatus = true;
                        newTraingCategory.CreatedBy = userid;
                        newTraingCategory.CreationDate = UtilService.GetPkCurrentDateTime();

                        newTraingCategory.TraingCategoryName = model.TraingCategoryName;
                        newTraingCategory.OderBy = 100;


                        db.tbl_TrainingCategory.Add(newTraingCategory);

                        db.SaveChanges();


                        trans.Commit();
                        return 1;




                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }


            }
        }

        #endregion 
        #region OrganizedBy
        public int AddOrganizedBy(OrganizedByDTO model, string userid)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        var newOrganizedBy = this._mapper.Map<tbl_TrainingOrganizedBy>(model);

                        newOrganizedBy.RecordStatus = true;
                        newOrganizedBy.CreatedBy = userid;
                        newOrganizedBy.CreationDate = UtilService.GetPkCurrentDateTime();
                        newOrganizedBy.OderBy = 100;


                        db.tbl_TrainingOrganizedBy.Add(newOrganizedBy);
                        db.SaveChanges();


                        trans.Commit();
                        return 1;




                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }


            }
        }
        public List<tbl_TrainingOrganizedBy> GetOrganizedBy()
        {
            try
            {
                using (var db = new IDDbContext())
                {

                    return db.tbl_TrainingOrganizedBy.ToList();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion 
        #region TrainingLevel
        public int AddTrainingLevel(TrainingLevelDTO model, string userid)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        var newTrainingLevel = this._mapper.Map<tbl_TrainingLevel>(model);

                        newTrainingLevel.RecordStatus = true;
                        newTrainingLevel.CreatedBy = userid;
                        newTrainingLevel.CreationDate = UtilService.GetPkCurrentDateTime();
                        newTrainingLevel.OrderBy = 100;


                        db.tbl_TrainingLevel.Add(newTrainingLevel);
                        db.SaveChanges();


                        trans.Commit();
                        return 1;




                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }


            }
        }
        public List<tbl_TrainingLevel> GetTrainingLevel()
        {
            try
            {
                using (var db = new IDDbContext())
                {

                    return db.tbl_TrainingLevel.ToList();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
        #region SupportedBy
        public int AddSupportedBy(SupportedByDTO model, string userid)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        var newSupportedBy = this._mapper.Map<tbl_SupportedBy>(model);

                        newSupportedBy.RecordStatus = true;
                        newSupportedBy.CreatedBy = userid;
                        newSupportedBy.CreationDate = UtilService.GetPkCurrentDateTime();
                        newSupportedBy.OderBy = 100;


                        db.tbl_SupportedBy.Add(newSupportedBy);
                        db.SaveChanges();


                        trans.Commit();
                        return 1;




                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }


            }
        }
        public List<tbl_SupportedBy> GetSupportedBy()
        {
            try
            {
                using (var db = new IDDbContext())
                {

                    return db.tbl_SupportedBy.ToList();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion



        #region DocumentType
        public int AddDocumentType(DocumentTypeDTO model, string userid)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        tbl_DocumentType _entity = new tbl_DocumentType();
                        _entity.DocumentType = model.DocumentType; 
                        _entity.RecordStatus = true;
                        _entity.CreatedBy = userid;
                        _entity.CreationDate = UtilService.GetPkCurrentDateTime();
                        _entity.OderBy = 100;


                        db.tbl_DocumentType.Add(_entity);

                        db.SaveChanges();


                        trans.Commit();
                        return 1;




                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        var message = ex.Message;
                        throw;
                    }
                }


            }
        }



        public List<tbl_DocumentType> GetDocumentType()
        {
            try
            {
                using (var db = new IDDbContext())
                {

                    return db.tbl_DocumentType.Where(x => x.RecordStatus == true).ToList() ;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion


    #region Cadre
        public int AddCadre(CadreDTO model, string userid)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        tbl_TrainingCadre _entity = new tbl_TrainingCadre();
                        _entity.Name = model.Name;
                        _entity.RecordStatus = true;
                        _entity.CreatedBy = userid;
                        _entity.CreationDate = UtilService.GetPkCurrentDateTime();

                        db.tbl_TrainingCadre.Add(_entity);

                        db.SaveChanges();


                        trans.Commit();
                        return 1;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        var message = ex.Message;
                        throw;
                    }
                }
            }
        }


        public List<tbl_TrainingCadre> GetCadre()
        {
            try
            {
                using (var db = new IDDbContext())
                {

                    return db.tbl_TrainingCadre.Where(x => x.RecordStatus == true).ToList();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }





        #endregion



        #region TraningType
        public int AddTrainingType(TrainingTypeDTO model, string userid)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        tbl_TraingType _entity = this._mapper.Map<tbl_TraingType>(model);

                        _entity.RecordStatus = true;
                        _entity.CreatedBy = userid;
                        _entity.CreationDate = UtilService.GetPkCurrentDateTime();

                        _entity.TrainingTypeName = model.TrainingTypeName;
                        _entity.OderBy = 100;


                        db.tbl_TraingType.Add(_entity);

                        db.SaveChanges();


                        trans.Commit();
                        return 1;




                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        var message = ex.Message;
                        throw;
                    }
                }


            }
        }


        public List<tbl_TrainingCategory> GetTrainingCategory()
        {
            try
            {
                using (var db = new IDDbContext())
                {

                    return db.tbl_TrainingCategory.ToList();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<tbl_TraingType> GetTrainingType()
        {
            try
            {
                using (var db = new IDDbContext())
                {

                    return db.tbl_TraingType.ToList();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region AddOrganizer
        public int AddOrganizer(MeetingOrganizerDTO model, string userid)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        var newMeetingOrganizer = this._mapper.Map<tbl_MeetingOrganizer>(model);

                        newMeetingOrganizer.RecordStatus = true;
                        newMeetingOrganizer.CreatedBy = userid;
                        newMeetingOrganizer.CreationDate = UtilService.GetPkCurrentDateTime();

                        newMeetingOrganizer.Organizer = model.Organizer;
                        newMeetingOrganizer.OderBy = 100;








                        db.tbl_MeetingOrganizer.Add(newMeetingOrganizer);

                        db.SaveChanges();











                        trans.Commit();
                        return 1;




                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }


            }
        }
        #endregion
        #region GetMeetingOrganizerList
        public List<tbl_MeetingOrganizer> GetMeetingOrganizerList()
        {
            try
            {
                using (var db = new IDDbContext())
                {

                    return db.tbl_MeetingOrganizer.ToList();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion


        #region GetMeetingVenueList
        public List<tbl_MeetingVenue> GetMeetingVenueList()
        {
            try
            {
                using (var db = new IDDbContext())
                {

                    return db.tbl_MeetingVenue.ToList();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion


        #region GetMeetingStatus
        public List<DDLMeetingStatusModel> GetMeetingStatus()
        {
            try
            {
                using var db = new IDDbContext();

                return db.tbl_MeetingStatus.Select(x => new DDLMeetingStatusModel { Id = x.Id, Status = x.Status }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion





        #region GetMeetingCount
        public MeetingCount GetMeetingCount(Guid? userid)
        {
            try
            {
                using (var db = new IDDbContext())
                {
                    var tasks = db.EventCalender.Where(x => x.IsDeleted == false && x.RecordStatus == true).AsQueryable();
                    var qury = db.EventParticipant.AsQueryable().Where(x => x.Event.IsDeleted == false);
                    if (userid != null)
                    {
                        tasks = tasks.Where(x => x.EventParticipant.Any(x => x.ParticipantId == userid) || x.CreatedBy == userid);


                    }


                    // old way var tasks = db.EventCalender.Where(x => x.RecordStatus == true).AsQueryable();
                    //List<EventCalenderView> tasks = new List<EventCalenderView>();

                    //if(userid  !=null) { 

                    //tasks = (from t in db.EventCalender
                    //         join mp in db.EventParticipant on t.Id equals mp.EventId
                    //         join c in db.tbl_MeetingStatus on t.MeetingStatusId equals c.Id
                    //         where ((t.CreatedBy == userid || mp.ParticipantId == userid) && ((
                    //          t.RecordStatus == true && t.IsDeleted == false)))
                    //             select new EventCalenderView
                    //             {
                    //                 Id = t.Id,
                    //                 Title = t.Title,
                    //                 Description = t.Description,

                    //                 StartDateTime = t.StartDateTime,

                    //                 MeetingStatusId = c.Id,
                    //                 RecordStatus = t.RecordStatus,
                    //                 ExternalParticipant = t.ExternalParticipants,
                    //                 ExternalParticipantsMobileNo = t.ExternalParticipantsMobileNo,
                    //                 //eventCalender.CreatedBy = Guid.Parse(userid);
                    //                 CreatedBy = t.CreatedBy,
                    //                 UpdatedBy = t.UpdatedBy,

                    //                 IsDeleted = t.IsDeleted,
                    //                 MeetingVenueId = t.MeetingVenueId,
                    //                 MeetingOrganizerId = t.MeetingOrganizerId,

                    //                 MeetingOrganizerIdName = t.MeetingOrganizer.Organizer,
                    //                 MeetingVenueIdName = t.MeetingVenue.Venue,
                    //                 MeetingStatusIdName = t.MeetingStatus.Status,
                    //                 MeetingAttendVia = t.MeetingAttendVia,




                    //             }).ToList();


                    //}
                    //else
                    //{
                    //    tasks = (from t in db.EventCalender


                    //             where (
                    //              t.RecordStatus == true && t.IsDeleted == false)
                    //             select new EventCalenderView
                    //             {
                    //                 Id = t.Id,
                    //                 Title = t.Title,
                    //                 Description = t.Description,

                    //                 StartDateTime = t.StartDateTime,

                    //                 MeetingStatusId = c.Id,
                    //                 RecordStatus = t.RecordStatus,
                    //                 ExternalParticipant = t.ExternalParticipants,
                    //                 ExternalParticipantsMobileNo = t.ExternalParticipantsMobileNo,
                    //                 //eventCalender.CreatedBy = Guid.Parse(userid);
                    //                 CreatedBy = t.CreatedBy,
                    //                 UpdatedBy = t.UpdatedBy,

                    //                 IsDeleted = t.IsDeleted,
                    //                 MeetingVenueId = t.MeetingVenueId,
                    //                 MeetingOrganizerId = t.MeetingOrganizerId,

                    //                 MeetingOrganizerIdName = t.MeetingOrganizer.Organizer,
                    //                 MeetingVenueIdName = t.MeetingVenue.Venue,
                    //                 MeetingStatusIdName = t.MeetingStatus.Status,
                    //                 MeetingAttendVia = t.MeetingAttendVia,




                    //             }).ToList();

                    //}
                    //if (userid != null)
                    //{
                    //    tasks = tasks.Where(x =>x.CreatedBy == userid.);


                    //}

                    var result = tasks.Where(x => x.EventParticipant.Count > 0).ToList();
                    var output = new MeetingCount
                    {
                        Total = result.Count(),
                        Active = result.Count(x => x.MeetingStatusId == 1 && x.RecordStatus == true),
                        InActive = result.Count(x => x.MeetingStatusId == 2 && x.RecordStatus == false),
                        Postponed = result.Count(x => x.MeetingStatusId == 3 && x.RecordStatus == true),

                        Rescheduled = result.Count(x => x.MeetingStatusId == 4 && x.RecordStatus == true),
                        Cancelled = result.Count(x => x.MeetingStatusId == 5 && x.RecordStatus == true),

                        Archived = result.Count(x => x.MeetingStatusId == 6 && x.RecordStatus == false)
                        //Archived = result.Count(x => x.StartDateTime < DateTime.Now )

                    };

                    return output;

                }
            }
            catch
            {
                throw;
            }
        }

        #endregion


        #region UpdateMeetingStatus
        public int UpdateMeetingStatus(MeetingDetailDTO model, string userid)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {


                        if (model.MeetingId != 0)
                        {
                            var task = db.EventCalender.Where(x => x.Id == model.MeetingId && x.RecordStatus == true).FirstOrDefault();
                            if (model.MeetingStatusId != 0)
                            {


                                task.MeetingStatusId = model.MeetingStatusId;
                                task.UpdatedBy = Guid.Parse(userid);
                                task.UpdationDate = DateTime.UtcNow.AddHours(5);

                                db.SaveChanges();

                            }


                            trans.Commit();

                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }

            }
        }
        #endregion
        #region Qualification
        public int AddQualification(QualificationDTO model, string userid)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        var newQualification = new tbl_Qualification();
                        newQualification.Qualification=model.Qualification;
                        newQualification.RecordStatus = true;
                        newQualification.CreatedBy = userid;
                        newQualification.CreationDate = UtilService.GetPkCurrentDateTime();
                        newQualification.OrderBy = 100;


                        db.tbl_Qualification.Add(newQualification);
                        db.SaveChanges();


                        trans.Commit();
                        return 1;




                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }


            }
        }
        public List<tbl_Qualification> GetQualification()
        {
            try
            {
                using (var db = new IDDbContext())
                {

                    return db.tbl_Qualification.ToList();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion



        #region DMS


        public async Task<int> AddFile(FileDTO model, string userid)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {


                        if (model.Fk_Folder != 0)
                        {

                            foreach (var item in model.FileAttachments)
                            {
                                var newFile = this._mapper.Map<Files>(model);
                                newFile.EnableFlag = true;
                                newFile.CreatedBy = userid;
                                newFile.CreatedAt = UtilService.GetPkCurrentDateTime();
                                newFile.Description = model.Description;
                                newFile.Name = model.Name;
                                newFile.Path = await UploadFile(item);
                                newFile.Fk_Folder = newFile.Fk_Folder;
                                newFile.Extension = item.ContentType;
                                newFile.FileSize = item.Length.ToString();
                                newFile.TrainingId = model.TrainingId;
                                db.Files.Add(newFile);
                                db.SaveChanges();
                            }

                            trans.Commit();


                            return 1;

                        }
                        else
                        {
                            foreach (var item in model.FileAttachments)
                            {

                                var newFile = this._mapper.Map<Files>(model);
                                newFile.EnableFlag = true;
                                newFile.CreatedBy = userid;
                                newFile.CreatedAt = UtilService.GetPkCurrentDateTime();
                                newFile.Description = model.Description;
                                newFile.Name = model.Name;
                                newFile.Path = await UploadFile(item);
                                newFile.Fk_Folder = null;
                                newFile.Extension = item.ContentType;
                                newFile.FileSize = item.Length.ToString();

                                db.Files.Add(newFile);

                                db.SaveChanges();
                            }

                            trans.Commit();

                            return 0;

                        }

                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw;
                    }
                }


            }
        }

        public List<FileDTO> FetchFiles(int id, string searchQuery, Guid userid)
        {
            var list = new List<FileDTO>();

            using (var db = new IDDbContext())
            {
                var queryDb = db.Files.Where(x => x.EnableFlag == true).AsQueryable();

                if (id == 0)
                {
                    queryDb = searchQuery?.Length > 1 ? queryDb.Where(o => o.Name.Contains(searchQuery)) : queryDb.Where(x => x.Fk_Folder == null);

                }
                else
                {
                    queryDb = searchQuery?.Length > 1 ? queryDb.Where(o => o.Name.Contains(searchQuery)) : queryDb.Where(x => x.Fk_Folder == id);

                }


                //if (sort != null)
                //{
                //    if (sort.By == "name")
                //    {
                //        queryDb = sort.Order == "asc"
                //            ? queryDb.OrderBy(x => x.Name)
                //            : queryDb.OrderByDescending(x => x.Name);
                //    }

                //    if (sort.By == "creationDate")
                //    {
                //        queryDb = sort.Order == "asc"
                //            ? queryDb.OrderBy(x => x.CreatedAt)
                //            : queryDb.OrderByDescending(x => x.CreatedAt);
                //    }
                //}

                list = queryDb.Select(
                    o => new FileDTO
                    {
                        Name = o.Name,
                        Description = o.Description,
                        FileAttachmentName = o.Name,
                        Path = "/Uploads/" + o.Path,
                        PkCode = o.PkCode


                    }).ToList();
            }

            //using (var db = new IDUSHAREEntities())
            //{
            //    list.AddRange(db.Folders.Where(x => x.EnableFlag == true && x.Fk_ParentId == id).Select(x => new FolderViewModel { Name = x.Name, UserId = x.CreatedBy, Id = x.PkCode, Description = x.Description }).ToList());
            //}

            return list;
        }


        #endregion




        public void DeleteMeeting(int Id)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        if (Id != 0)
                        {

                            var Meeting = db.EventCalender.Where(x => x.Id == Id && x.RecordStatus == true ).FirstOrDefault();

                            Meeting.RecordStatus = false;
                            Meeting.IsDeleted = true;

                            db.SaveChanges();

                            trans.Commit();

                        }

                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }

            }
        }


        public List<TotalTrainingMonthChartView> TotalTrainingMonthCount()
        {
            try
            {
                using (var db = new IDDbContext())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            List<TotalTrainingMonthChartView> TotalTrainingCount = db.TotalTrainingMonthChartView.ToList();
                            return TotalTrainingCount;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var Message = ex.Message;
                throw ex;
            }
        }



        public List<TrainingTypeWiseReport> TrainingTypeWiseReport()
        {
            try
            {
                using (var db = new IDDbContext())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            List<TrainingTypeWiseReport> TotalTrainingTypeWiseCount = db.TrainingTypeWiseReport.ToList();
                            return TotalTrainingTypeWiseCount;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var Message = ex.Message;
                throw ex;
            }
        }



        public bool DeleteTrainingCategory(int Id)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        if (Id > 0)
                        {

                            var data = db.tbl_TrainingCategory.Find(Id);
                            if (data != null)
                            {
                               db.tbl_TrainingCategory.Remove(data);
                               db.SaveChanges();

                                trans.Commit();
                                return true;
                            }
                            else
                            {
                                return false;
                            }


                        }
                        else
                        {
                            return false;
                        }

                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }

            }
        }


        public bool DeleteTrainingLevel(int Id)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        if (Id > 0)
                        {

                            var data = db.tbl_TrainingLevel.Find(Id);
                            if (data != null)
                            {
                                db.tbl_TrainingLevel.Remove(data);
                                db.SaveChanges();

                                trans.Commit();
                                return true;
                            }
                            else
                            {
                                return false;
                            }


                        }
                        else
                        {
                            return false;
                        }

                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }

            }
        }





        public bool DeleteTrainingType(int Id)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        if (Id > 0)
                        {

                            var data = db.tbl_TraingType.Find(Id);
                            if (data != null)
                            {
                                db.tbl_TraingType.Remove(data);
                                db.SaveChanges();

                                trans.Commit();
                                return true;
                            }
                            else
                            {
                                return false;
                            }


                        }
                        else
                        {
                            return false;
                        }

                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }

            }
        }
        public bool DeleteTrainingOrganizedBy(int Id)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        if (Id > 0)
                        {

                            var data = db.tbl_TrainingOrganizedBy.Find(Id);
                            if (data != null)
                            {
                                db.tbl_TrainingOrganizedBy.Remove(data);
                                db.SaveChanges();

                                trans.Commit();
                                return true;
                            }
                            else
                            {
                                return false;
                            }


                        }
                        else
                        {
                            return false;
                        }

                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }

            }
        }
        public bool DeleteTrainingSupportedBy(int Id)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        if (Id > 0)
                        {

                            var data = db.tbl_SupportedBy.Find(Id);
                            if (data != null)
                            {
                                db.tbl_SupportedBy.Remove(data);
                                db.SaveChanges();

                                trans.Commit();
                                return true;
                            }
                            else
                            {
                                return false;
                            }


                        }
                        else
                        {
                            return false;
                        }

                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }

            }
        }


        public bool DeleteTrainingVenue(int Id)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        if (Id > 0)
                        {

                            var data = db.tbl_MeetingVenue.Find(Id);
                            if (data != null)
                            {
                                db.tbl_MeetingVenue.Remove(data);
                                db.SaveChanges();

                                trans.Commit();
                                return true;
                            }
                            else
                            {
                                return false;
                            }


                        }
                        else
                        {
                            return false;
                        }

                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }

            }
        }


        public bool DeleteTrainingQualification(int Id)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        if (Id > 0)
                        {

                            var data = db.tbl_Qualification.Find(Id);
                            if (data != null)
                            {
                                db.tbl_Qualification.Remove(data);
                                db.SaveChanges();

                                trans.Commit();
                                return true;
                            }
                            else
                            {
                                return false;
                            }


                        }
                        else
                        {
                            return false;
                        }

                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }

            }
        }



        public bool DeleteTrainingDocumentType(int Id)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        if (Id > 0)
                        {

                            var data = db.tbl_DocumentType.Find(Id);
                            if (data != null)
                            {
                                db.tbl_DocumentType.Remove(data);
                                db.SaveChanges();

                                trans.Commit();
                                return true;
                            }
                            else
                            {
                                return false;
                            }


                        }
                        else
                        {
                            return false;
                        }

                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }

            }
        }





        public bool DeleteCadre(int Id)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        if (Id > 0)
                        {

                            var data = db.tbl_TrainingCadre.Find(Id);
                            if (data != null)
                            {
                                db.tbl_TrainingCadre.Remove(data);
                                db.SaveChanges();
                                trans.Commit();
                                return true;
                            }
                            else
                            {
                                return false;
                            }


                        }
                        else
                        {
                            return false;
                        }

                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }

            }
        }

        [Obsolete]
        public bool DeleteTrainingPastTraining(int Id)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        if (Id > 0)
                        {

                            var trainingData = db.ScheduleList.Where(x => x.Id == Id && x.RecordStatus == true && x.IsPrevious == true).FirstOrDefault();
                            if (trainingData != null)
                            {
                                using (var context = new IDDbContext())
                                {
                                    var commandText = "update ScheduleList set RecordStatus = 0 where id = " + Id + "";

                                    context.Database.ExecuteSqlCommand(commandText);

                                    commandText = "update EventCalender set RecordStatus = 0 where id = " + trainingData.TrainingId + "";

                                    context.Database.ExecuteSqlCommand(commandText);
                                }
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }

                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                    finally
                    {
                        db.SaveChanges();

                    }
                }

            }
        }




        public bool DeleteTrainingNewTraining(int Id)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        if (Id > 0)
                        {

                            var trainingData = db.EventCalender.Where(x => x.Id == Id && x.RecordStatus == true).FirstOrDefault();
                            if (trainingData != null)
                            {
                                trainingData.RecordStatus = false;
                                trans.Commit();
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }

                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                    finally
                    {
                        db.SaveChanges();

                    }
                }

            }
        }

        [Obsolete]
        public bool DeleteTrainingSchduleTraining(int Id)
        {
            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        if (Id > 0)
                        {
                            var data = db.ScheduleList.Where(x => x.Id == Id).FirstOrDefault();
                            if (data != null)
                            {
                                using (var context = new IDDbContext())
                                {
                                    var commandText = "update ScheduleList set RecordStatus = 0 where id = " + Id + "";

                                    context.Database.ExecuteSqlCommand(commandText);
                                }

                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }

                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }

            }
        }


        public bool DeleteTrainingExternalParticipant(int Id)
        {

            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {

                        if (Id > 0)
                        {

                            var trainingData = db.ExternalParticepant.Where(x => x.Id == Id && x.RecordStatus == true ).FirstOrDefault();
                            if (trainingData != null)
                            {
                                trainingData.RecordStatus = false;
                                trans.Commit();
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }

                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                    finally
                    {
                        db.SaveChanges();

                    }
                }

            }
        }

    }
}
