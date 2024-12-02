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

namespace Meeting_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        #region Fields
        private EventServices _eventServices;
        private ScheduleServices _scheduleServices;
        private ReportServices _reportServices;
        private CommonService _cService;
        private NotificationService _notificationService = new NotificationService();

        private IHubContext<NotificationHub> _hub;

        #endregion

        #region Constructor

        //private readonly IDDbContext db;
        public ReportsController(ReportServices _reportServices, UserManager<Applicationuser> user, EventServices eventServices, IMapper mapper, IHubContext<NotificationHub> hub)
        {
            _eventServices = eventServices;
            this._reportServices = _reportServices;
            _hub = hub;
            _cService = new CommonService(user);
            //db = context;
        }
        #endregion
        [Authorize]
        [HttpGet]
        [Route("GetReport")]
        public async Task<IActionResult> GetReport()
        {
            try
            {
                Guid userid = Guid.Parse(this.GetUserId());
                var isAdmin = await _cService.CheckRoleExists(userid, "Admin");

                var list = _reportServices.GetReports(userid);

                return Ok(UtilService.GetResponse(list));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        //[Authorize]
        //[HttpGet]
        //[Route("GetTrainingTypeDetail")]
        //public async Task<IActionResult> GetTrainingTypeDetail(string TrainingType)
        //{
        //    try
        //    {
        //        Guid userid = Guid.Parse(this.GetUserId());
        //        var isAdmin = await _cService.CheckRoleExists(userid, "Admin");

        //        var list = _reportServices.GetTrainingTypeDetail(TrainingType);

        //        return Ok(UtilService.GetResponse(list));
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(UtilService.GetExResponse<Exception>(ex));
        //    }
        //}  
        [Authorize]
        [HttpGet]
        [Route("GetParticepantDetail")]
        public async Task<IActionResult> GetParticepantDetail(int ProfileId)
        {
            try
            {
                Guid userid = Guid.Parse(this.GetUserId());
                var isAdmin = await _cService.CheckRoleExists(userid, "Admin");

                var res = _reportServices.GetParticepantDetail(ProfileId);

                return Ok(UtilService.GetResponse(res));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }


        [Authorize]
        [HttpGet]
        [Route("GetExternalParticepantDetail")]
        public async Task<IActionResult> GetExternalParticepantDetail(int ProfileId)
        {
            try
            {
                Guid userid = Guid.Parse(this.GetUserId());
                var isAdmin = await _cService.CheckRoleExists(userid, "Admin");

                var res = _reportServices.GetExternalParticepantDetail(ProfileId);

                return Ok(UtilService.GetResponse(res));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }

        [Authorize]
        [HttpGet]
        [Route("FilterByTraining")]
        public async Task<IActionResult> FilterByTraining(int Id)
        {
            try
            {
                Guid userid = Guid.Parse(this.GetUserId());
                var isAdmin = await _cService.CheckRoleExists(userid, "Admin");

                var res = _reportServices.FilterByTraining(Id);

                return Ok(UtilService.GetResponse(res));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }


    }
}
