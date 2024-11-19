using AutoMapper;
using Meeting_App.Models;
using Meeting_App.Service.Common;
using Meeting_App.Service;
using Meeting_App.SIgnalRHub;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;
using Meeting_App.Data.Database.Context;
using System.Linq;
using Meeting_App.Models.DTOs;
using System.Collections.Generic;
using Meeting_App.Data.Database.Tables;

namespace Meeting_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        #region Fields
        private EventServices _eventServices;
        private ScheduleServices _scheduleServices;
        private CommonService _cService;
        private NotificationService _notificationService = new NotificationService();

        private IHubContext<NotificationHub> _hub;

        #endregion
        #region Constructor

        //private readonly IDDbContext db;
        public ScheduleController(ScheduleServices _scheduleServices, UserManager<Applicationuser> user, EventServices eventServices, IMapper mapper, IHubContext<NotificationHub> hub)
        {
            _eventServices = eventServices;
            this._scheduleServices = _scheduleServices;
            _hub = hub;
            _cService = new CommonService(user);
            //db = context;
        }
        #endregion
        #region Schedule
        [HttpPost]
        [Route("AddSchedule")]
        public IActionResult AddSchedule([FromForm] ScheduleDTO model)
        {
            try
            {
                long res = 0;

                ///
                string userid = this.GetUserId();

                res = _scheduleServices.AddSchedule(model, userid);

                string msg = "";

                if (res > 0)
                {
                    msg = "Saved Successfully";
                    _hub.Clients.All.SendAsync("transferchartdata", _notificationService.GetNotifications("*", userid, true));
                }
                else
                {
                    msg = "Updated Successfully";
                }
                return Ok(UtilService.GetResponse<Json>(null, msg));


            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetSchedule")]
        public async Task<IActionResult> GetSchedule(string Sreach="")
        {
            try
            {
                Guid userid = Guid.Parse(this.GetUserId());
                var isAdmin = await _cService.CheckRoleExists(userid, "Admin");

                var list = _scheduleServices.GetSchedule(userid, Sreach);

                return Ok(UtilService.GetResponse(list));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        [Authorize]
        [HttpGet]
        [Route("GetScheduleReport")]
        public async Task<IActionResult> GetScheduleReport()
        {
            try
            {
                Guid userid = Guid.Parse(this.GetUserId());
                var isAdmin = await _cService.CheckRoleExists(userid, "Admin");

                var list = _scheduleServices.GetScheduleReport();

                return Ok(UtilService.GetResponse(list));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }   
       
        [HttpGet]
        [Route("ParticipantByCnic")]
        public async Task<IActionResult> ParticipantByCnic(string CNIC)
        {
            try
            {
             

                var ParticipantByCnic = _scheduleServices.ParticipantByCnic(CNIC);

                if(ParticipantByCnic == null)
                {
                    return (Ok(false));
                }

                TraineeViewModel model = new TraineeViewModel();
                model.ProfileId = ParticipantByCnic.Id;
                model.FullName = String.Format("{0} ({1}) ({2})", ParticipantByCnic.EmployeeName, ParticipantByCnic.CNIC, ParticipantByCnic.Designation_Name);
                model.HfmisName = ParticipantByCnic.WorkingHealthFacility;
                model.HfmisCode = ParticipantByCnic.WorkingHFMISCode;


                return Ok(model);
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetScheduleWithStartdate")]
        public async Task<IActionResult> GetScheduleWithStartdate()
        {
            try
            {
                Guid userid = Guid.Parse(this.GetUserId());
                var isAdmin = await _cService.CheckRoleExists(userid, "Admin");

                var list = _scheduleServices.GetScheduleWithStartdate();

                return Ok(UtilService.GetResponse(list));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        [Authorize]
        [HttpGet]
        [Route("GetPeriviousSchedule")]
        public async Task<IActionResult> GetPeriviousSchedule()
        {
            try
            {
                Guid userid = Guid.Parse(this.GetUserId());
                var isAdmin = await _cService.CheckRoleExists(userid, "Admin");

                var list = _scheduleServices.GetPeriviousSchedule();

                return Ok(UtilService.GetResponse(list));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        [Authorize]
        [HttpGet]
        [Route("GetPreviousSchedule")]
        public async Task<IActionResult> GetPreviousSchedule(String Sreach)
       {
            try
            {
                Guid userid = Guid.Parse(this.GetUserId());
                var isAdmin = await _cService.CheckRoleExists(userid, "Admin");

                var list = _scheduleServices.GetPreviousSchedule(Sreach);

                return Ok(UtilService.GetResponse(list));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetSchedulebyUser")]
        public async Task<IActionResult> GetSchedulebyUser()
        {
            try
            {
                Guid userid = Guid.Parse(this.GetUserId());
                var isAdmin = await _cService.CheckRoleExists(userid, "Admin");

                var list = _scheduleServices.GetSchedulebyUser(userid);

                return Ok(UtilService.GetResponse(list));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        [Authorize]
        [HttpGet]
        [Route("GetScheduleByCnic")]
        public async Task<IActionResult> GetScheduleByCnic(string Cnic)
        {
            try
            {


                var list = await _scheduleServices.GetSchedule(Cnic);

                return Ok(UtilService.GetResponse(list));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetScheduleWithParticepants")]
        public async Task<IActionResult> GetScheduleWithParticepants(int Id)
        {
            try
            {
                Guid userid = Guid.Parse(this.GetUserId());
                var isAdmin = await _cService.CheckRoleExists(userid, "Admin");

                var res = _scheduleServices.GetScheduleWithParticepants(Id, this.GetUserId());

                return Ok(UtilService.GetResponse(res));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }


        [HttpPost]
        [Route("ChangeStatus")]
        public IActionResult ChangeStatus(ParticipantStatusDto model)
        {
            try
            {
                bool res = false;




                res = _scheduleServices.ChangeStatus(model);



                return Ok(res);


            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        [HttpPost]
        [Route("DeleteFromHRParticepant")]
        public IActionResult DeleteFromHRParticepant(ParticipantStatusDto model)
        {
            try
            {
                bool res = false;




                res = _scheduleServices.DeleteFromHRParticepant(model);



                return Ok(res);


            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        [HttpPost]
        [Route("ChangeAttendanceStatus")]
        public IActionResult ChangeAttendanceStatus(ParticipantStatusDto model)
        {
            try
            {
                bool res = false;




                res = _scheduleServices.ChangeAttendanceStatus(model);



                return Ok(res);


            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }


        [HttpPost]
        [Route("changeExternalParticipantStatus")]
        public IActionResult changeExternalParticipantStatus(ParticipantStatusDto model)
        {
            try
            {
                bool res = false;




                res = _scheduleServices.changeExternalParticipantStatus(model);



                return Ok(res);


            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        [HttpPost]
        [Route("DeleteExternalParticipant")]
        public IActionResult DeleteExternalParticipant(ParticipantStatusDto model)
        {
            try
            {
                bool res = false;




                res = _scheduleServices.DeleteExternalParticipant(model);



                return Ok(res);


            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }



        [HttpPost]
        [Route("SendEmail")]
        public IActionResult SendEmail(ParticipantStatusDto model)
        {
            try
            {
                bool res = false;




                res = _scheduleServices.SendEmail(model);



                return Ok(res);


            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }

        #endregion
        #region Trainee
        [HttpPost]
        [Route("AddTrainee")]
        public IActionResult AddTrainee(TraineeDto model)
        {
            try
            {
                bool data = false;

                ///
                string userid = this.GetUserId();

                data = _scheduleServices.AddTrainee(model, userid);




                return Ok(data);


            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        [HttpPost]
        [Route("ReplaceTrainee")]
        public IActionResult ReplaceTrainee(TraineeDto model)
        {
            try
            {
                bool data = false;

                ///
                string userid = this.GetUserId();

                data = _scheduleServices.ReplaceTrainee(model, userid);




                return Ok(data);


            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion
        #region Designation
        [Authorize]
        [HttpGet]
        [Route("GetDesignation")]
        public async Task<IActionResult> GetDesignation()
        {
            try
            {
                Guid userid = Guid.Parse(this.GetUserId());
                var isAdmin = await _cService.CheckRoleExists(userid, "Admin");

                var list = _scheduleServices.GetDesignation();

                return Ok(UtilService.GetResponse(list));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion
        #region ProfileFilter
        [HttpGet]
        [Route("GetDivisions")]
        public IActionResult GetDivisions()
        {
            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    return Ok(UtilService.GetResponse(db.Divisions.ToList()));
                }
            }
        }
        [HttpGet]
        [Route("GetDistricts/{code}")]
        public IActionResult GetDistricts(string code)
        {
            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    return Ok(UtilService.GetResponse((db.District.Where(x => x.Code.StartsWith(code)).ToList())));
                }
            }
        }

        [HttpGet]
        [Route("GetTehsils/{code}")]
        public IActionResult GetTehsils(string code)
        {
            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    return Ok(UtilService.GetResponse((db.Tehsils.Where(x => x.Code.StartsWith(code)).ToList())));
                }
            }
        }
        [HttpGet]
        [Route("GetHfTypes")]
        public IActionResult GetHfTypes()
        {
            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    return Ok(UtilService.GetResponse((db.Hftype.Where(x => x.Code == "011" || x.Code == "012" || x.Code == "013" || x.Code == "014").ToList())));
                }
            }
        }

        [HttpGet]
        [Route("GetServiceListFacility/{disCode}/{HfTypeCode}/{bhuName}")]
        public IActionResult GetServiceListFacility(string disCode, string HfTypeCode, string bhuName)
        {
            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    if (bhuName == "null")
                    {
                        var hf = db.HflistWithMode.Where(x => x.HFMISCode.StartsWith(disCode) && HfTypeCode == x.HFTypeCode).ToList();

                        return Ok(UtilService.GetResponse((hf)));
                    }
                    return Ok(UtilService.GetResponse((db.HflistWithMode.Where(x => x.HFMISCode.StartsWith(disCode) && HfTypeCode == x.HFTypeCode && x.ModeName.Contains(bhuName)).ToList())));
                }
            }

        }


        [HttpGet]
        [Route("GetHealthFacilityDivision/{disCode}/{HfTypeCode}/{bhuName}")]
        public IActionResult GetHealthFacilityDivision(string disCode, string HfTypeCode, string bhuName)
        {
            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    if (bhuName == "null")
                    {
                        var hf = db.HflistWithMode.Where(x => x.HFMISCode.StartsWith(disCode) && HfTypeCode == x.HFTypeCode).ToList();

                        return Ok(UtilService.GetResponse((hf)));
                    }
                    return Ok(UtilService.GetResponse((db.HflistWithMode.Where(x => x.HFMISCode.StartsWith(disCode) && HfTypeCode == x.HFTypeCode && x.ModeName.Contains(bhuName)).ToList())));
                }
            }

        }
        [HttpPost]
        [Route("getTrainee")]
        public IActionResult getTrainee(HmisCodeDesignationDto hmisCodeDesignationDto)
        {
            List<TraineeViewModel> _resultModel = new List<TraineeViewModel>();
            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    foreach (var item in hmisCodeDesignationDto.HfmisList)
                    {
                        var trainees = db.ProfileView.Where(x => x.WorkingHFMISCode == item && x.Designation_Id == hmisCodeDesignationDto.DesignationId).ToList();
                        foreach (var trainee in trainees)
                        {
                            TraineeViewModel model = new TraineeViewModel();
                            model.ProfileId = trainee.Id;
                            model.FullName = String.Format("{0} ({1}) ({2})", trainee.EmployeeName, trainee.CNIC, trainee.Designation_Name);
                            model.HfmisName = trainee.WorkingHealthFacility;
                            model.HfmisCode = trainee.WorkingHFMISCode;

                            _resultModel.Add(model);
                        }

                    }
                    return Ok(_resultModel);

                }
            }

        }

        [HttpGet]
        [Route("GetBhuList")]
        public IActionResult GetBhuList()
        {
            using (var db = new IDDbContext())
            {
                using (var trans = db.Database.BeginTransaction())
                {
                    return Ok(UtilService.GetResponse((db.Hfmode.GroupBy(n => new { n.ModeName }).Select(g => new
                    {
                        Name = g.Key.ModeName
                    })).ToList()));
                }
            }
        }
        #endregion
        #region Trainer
        [HttpPost]
        [Route("AddTrainer")]
        public IActionResult AddTrainer(TraineeDto model)
        {
            try
            {
                bool data = false;

                ///
                string userid = this.GetUserId();

                data = _scheduleServices.AddTrainer(model, userid);




                return Ok(data);


            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        [HttpPost]
        [Route("ReplaceTrainer")]
        public IActionResult ReplaceTrainer(TraineeDto model)
        {
            try
            {
                bool data = false;

                ///
                string userid = this.GetUserId();

                data = _scheduleServices.RelaceTrainer(model, userid);




                return Ok(data);


            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion
        #region ExternalParticipant
        [HttpPost]
        [Route("AddExternalParticipant")]
        public IActionResult AddExternalParticipant(List<ExternalParticepantDto> model)
        {
            try
            {
                long res = 0;

                ///
                string userid = this.GetUserId();

                res = _scheduleServices.AddExternalParticepant(model, userid);

                string msg = "";

                if (res > 0)
                {
                    msg = "Saved Successfully";
                    _hub.Clients.All.SendAsync("transferchartdata", _notificationService.GetNotifications("*", userid, true));
                }
                else
                {
                    msg = "Updated Successfully";
                }
                return Ok(UtilService.GetResponse<Json>(null, msg));


            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        [HttpPost]
        [Route("AddExternalParticepantwithScheduleId")]
        public IActionResult AddExternalParticepantwithScheduleId(List<ExternalParticepantDto> model)
        {
            try
            {
                bool res = false;

                ///
                string userid = this.GetUserId();

                res = _scheduleServices.AddExternalParticepantwithScheduleId(model, userid);

                string msg = "";

                if (res)
                {
                    msg = "Saved Successfully";
                    _hub.Clients.All.SendAsync("transferchartdata", _notificationService.GetNotifications("*", userid, true));
                    return Ok(UtilService.GetResponse<Json>(null, msg));
                }
                else
                {
                    msg = "Exteral Participant already added to this training";
                    return Ok(new { isException = true, Message = msg });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        [HttpPost]
        [Route("ReplaceExternalParticepantwithScheduleId")]
        public IActionResult ReplaceExternalParticepantwithScheduleId(TraineeDto model)
        {
            try
            {
                bool res = false;

                ///
                string userid = this.GetUserId();

                res = _scheduleServices.ReplaceExternalParticepantwithScheduleId(model, userid);

                string msg = "";

                if (res)
                {
                    msg = "Saved Successfully";
                    _hub.Clients.All.SendAsync("transferchartdata", _notificationService.GetNotifications("*", userid, true));
                    return Ok(UtilService.GetResponse<Json>(null, msg));
                }
                else
                {
                    msg = "External Paticipant cannot be replaced";
                }
                return Ok(new { isException = true, message = msg });


            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetExternalParticipant")]
        public async Task<IActionResult> GetExternalParticipant()
        {
            try
            {
                Guid userid = Guid.Parse(this.GetUserId());
                var isAdmin = await _cService.CheckRoleExists(userid, "Admin");

                var list = _scheduleServices.GetExternalParticepant();

                return Ok(UtilService.GetResponse(list));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }




        [Authorize]
        [HttpGet]
        [Route("GetExternalParticipants")]
        public async Task<IActionResult> GetExternalParticipants()
        {
            try
            {
                Guid userid = Guid.Parse(this.GetUserId());
                var isAdmin = await _cService.CheckRoleExists(userid, "Admin");

                var list = _scheduleServices.GetExternalParticepants();

                return Ok(UtilService.GetResponse(list));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }




        [HttpGet]
        [Route("getParticipantbyId")]
        public IActionResult getParticipantbyId(int Id)
        {
            try
            {

                var res = _scheduleServices.GetExternalParticepantbyid(Id);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }


        [HttpGet]
        [Route("CounterCard")]
        public TrainingDTO CounterCard()
        {
            return _scheduleServices.CounterCard();
        }


        #endregion
        #region Marks


        [HttpPost]
        [Route("ChangeParticipantMarks")]
        public IActionResult ChangeParticipantMarks(TrainingParticipant model)
        {
            try
            {
                bool res = false;




                res = _scheduleServices.ChangeParticipantMarks(model);



                return Ok(res);


            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }



        #endregion
    }
}
