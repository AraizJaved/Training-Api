using AutoMapper;
using Meeting_App.Data.Database.Context;
using Meeting_App.Data.Database.Tables;
using Meeting_App.Models;
using Meeting_App.Models.DTOs;
using Meeting_App.Service;
using Meeting_App.Service.Common;
using Meeting_App.SIgnalRHub;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.StaticFiles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Meeting_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        #region Fields
        private EventServices _eventServices;
        private CommonService _cService;
        private NotificationService _notificationService = new NotificationService();

        private IHubContext<NotificationHub> _hub;
        private IHostingEnvironment Environment;
        private IConfiguration Configuration;


        #endregion

        #region Constructor
        public EventController(IHostingEnvironment _environment, IConfiguration _configuration ,UserManager<Applicationuser> user, EventServices eventServices, IMapper mapper, IHubContext<NotificationHub> hub)
        {
            _eventServices = eventServices;
            _hub = hub;
            _cService = new CommonService(user);
            Environment = _environment;
            Configuration = _configuration;

        }
        #endregion

        #region AddEvent
        [HttpPost]
        [Route("AddEvent")]
        public async Task<IActionResult> AddEvent([FromForm] EventCalenderView model)
        {
            try
            {
                string userid = this.GetUserId();
                

                var res = await _eventServices.AddEvent(model, userid);

                string msg = "";

                if (res.Id != 0)
                {
                    msg = "Saved Successfully";
                    await _hub.Clients.All.SendAsync("transferchartdata", _notificationService.GetNotifications("*", userid, true));
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
        [Route("AddPreviousTraining")]
        public async Task<IActionResult> AddPreviousTraining([FromForm] EventCalenderView model)
        {
            try
            {
                string userid = this.GetUserId();
                //string userid = "333C3DEB-F4E0-460F-0A8A-08D9C9F8DBEA";
                // model.EventParticipant = JsonConvert.DeserializeObject<List<EventParticipantView>>(model.EventParticipantData);

                var res = await _eventServices.AddPreviousTraining(model, userid);

                string msg = "";

                if (res.Id != 0)
                {
                    msg = "Saved Successfully";
                    await _hub.Clients.All.SendAsync("transferchartdata", _notificationService.GetNotifications("*", userid, true));
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


        #endregion




        #region GetTrainings
        [Authorize]
        [HttpGet]
        [Route("GetTrainings")]
        public async Task<IActionResult> GetTrainings(string Sreach)
        {
            try
            {
                Guid userid = Guid.Parse(this.GetUserId());
                var isAdmin = await _cService.CheckRoleExists(userid, "Admin");

                var list = _eventServices.GetTrainings(userid, Sreach);

                return Ok(UtilService.GetResponse(list));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        [Authorize]
        [HttpGet]
        [Route("GetPreviousTrainings")]
        public async Task<IActionResult> GetPreviousTrainings()
        {
            try
            {
                Guid userid = Guid.Parse(this.GetUserId());
                var isAdmin = await _cService.CheckRoleExists(userid, "Admin");

                var list = _eventServices.GetPreviousTrainings();

                return Ok(UtilService.GetResponse(list));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetAllEvents
        [Authorize]
        [HttpPost]
        [Route("GetAllEvents")]
        public async Task<IActionResult> GetAllEvents([FromBody] EventFilter filter)
    {
            try
            {
                Guid userid = Guid.Parse(this.GetUserId());
                var isAdmin = await _cService.CheckRoleExists(userid, "Admin");
                if (!isAdmin)
                {
                    filter.UserId = userid;
                }
                if (filter.ShowMyEvent)
                {
                    filter.UserId = userid;
                }
                var list = _eventServices.GetEvents(filter);

                return Ok(UtilService.GetResponse(list));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }

        #endregion


        #region Venue
        [HttpPost]
        [Route("AddVenue")]
        public IActionResult AddVenue([FromForm] MeetingVenueDTO model)
        {
            try
            {
                int res = 0;

                ///
                string userid = this.GetUserId();

                res = _eventServices.AddVenue(model, userid);

                string msg = "";

                if (res == 1)
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
        [Route("GetVenues")]
        public async Task<IActionResult> GetVenues()
        {
            try
            {
                Guid userid = Guid.Parse(this.GetUserId());
                var isAdmin = await _cService.CheckRoleExists(userid, "Admin");

                var list = _eventServices.GetVenue();

                return Ok(UtilService.GetResponse(list));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }



        #endregion
        #region TariningCategory
        [HttpPost]
        [Route("AddTariningCategory")]
        public IActionResult AddTariningCategory(TrainingCategoryDTO model)
        {
            try
            {
                int res = 0;

                ///
                string userid = this.GetUserId();

                res = _eventServices.AddTraingCategory(model, userid);

                string msg = "";

                if (res == 1)
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


        [HttpGet]
        [Route("GetTariningCategory")]
        public IActionResult GetTariningCategory()
        {
            try
            {
                return Ok(UtilService.GetResponse<List<tbl_TrainingCategory>>(_eventServices.GetTrainingCategory()));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion
        #region OrganizedBy

        [HttpPost]
        [Route("AddOrganizedBy")]
        public IActionResult AddOrganizedBy(OrganizedByDTO model)
        {
            try
            {
                int res = 0;

                ///
                string userid = this.GetUserId();

                res = _eventServices.AddOrganizedBy(model, userid);

                string msg = "";

                if (res == 1)
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


        [HttpGet]
        [Route("GetOrganizedBy")]
        public IActionResult GetOrganizedBy()
        {
            try
            {
                return Ok(UtilService.GetResponse<List<tbl_TrainingOrganizedBy>>(_eventServices.GetOrganizedBy()));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        } 
    
        #endregion
        #region SupportedBy

        [HttpPost]
        [Route("AddSupportedBy")]
        public IActionResult AddSupportedBy(SupportedByDTO model)
        {
            try
            {
                int res = 0;

                ///
                string userid = this.GetUserId();

                res = _eventServices.AddSupportedBy(model, userid);

                string msg = "";

                if (res == 1)
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


        [HttpGet]
        [Route("GetSupportedBy")]
        public IActionResult GetSupportedBy()
        {
            try
            {
                return Ok(UtilService.GetResponse<List<tbl_SupportedBy>>(_eventServices.GetSupportedBy()));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region TariningType
        [HttpPost]
        [Route("AddTariningType")]
        public IActionResult AddTariningType(TrainingTypeDTO model)
        {
            try
            {
                int res = 0;

                ///
                string userid = this.GetUserId();

                res = _eventServices.AddTrainingType(model, userid);

                string msg = "";

                if (res == 1)
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


        [HttpGet]
        [Route("GetTrainingType")]
        public IActionResult GetTrainingType()
        {
            try
            {
                return Ok(UtilService.GetResponse<List<tbl_TraingType>>(_eventServices.GetTrainingType()));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion






        #region Cadre
        [HttpPost]
        [Route("AddCadre")]
        public IActionResult AddCadre(CadreDTO model)
        {
            try
            {
                int res = 0;

                ///
                string userid = this.GetUserId();

                res = _eventServices.AddCadre(model, userid);

                string msg = "";

                if (res == 1)
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


        [HttpGet]
        [Route("GetCadre")]
        public IActionResult GetCadre()
        {
            try
            {
                return Ok(UtilService.GetResponse<List<tbl_TrainingCadre>>(_eventServices.GetCadre()));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion





        #region TrainingLevel

        [HttpPost]
        [Route("AddTrainingLevel")]
        public IActionResult AddTrainingLevel(TrainingLevelDTO model)
        {
            try
            {
                int res = 0;

                ///
                string userid = this.GetUserId();

                res = _eventServices.AddTrainingLevel(model, userid);

                string msg = "";

                if (res == 1)
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


        [HttpGet]
        [Route("GetTrainingLevel")]
        public IActionResult GetTrainingLevel()
        {
            try
            {
                return Ok(UtilService.GetResponse<List<tbl_TrainingLevel>>(_eventServices.GetTrainingLevel()));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion    
        #region Qualification

        [HttpPost]
        [Route("AddQualification")]
        public IActionResult AddQualification(QualificationDTO model)
        {
            try
            {
                int res = 0;

                ///
                string userid = this.GetUserId();

                res = _eventServices.AddQualification(model, userid);

                string msg = "";

                if (res == 1)
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


        [HttpGet]
        [Route("GetQualification")]
        public IActionResult GetQualification()
        {
            try
            {
                return Ok(UtilService.GetResponse<List<tbl_Qualification>>(_eventServices.GetQualification()));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion




        #region DocumentType
        [HttpPost]
        [Route("AddDocumentType")]
        public IActionResult AddDocumentType(DocumentTypeDTO model)
        {
            try
            {
                int res = 0;

                ///
                string userid = this.GetUserId();

                res = _eventServices.AddDocumentType(model, userid);

                string msg = "";

                if (res == 1)
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


        [HttpGet]
        [Route("GetDocumentType")]
        public IActionResult GetDocumentType()
        {
            try
            {
                return Ok(UtilService.GetResponse<List<tbl_DocumentType>>(_eventServices.GetDocumentType()));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion










        #region AddOrganizer
        [HttpPost]
        [Route("AddOrganizer")]
        public IActionResult AddOrganizer([FromForm] MeetingOrganizerDTO model)
        {
            try
            {
                int res = 0;

                ///
                string userid = this.GetUserId();

                res = _eventServices.AddOrganizer(model, userid);

                string msg = "";

                if (res == 1)
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
        #endregion


        #region GetMeetingOrganizerList
        [HttpGet]
        [Route("GetMeetingOrganizerList")]
        public IActionResult GetMeetingOrganizerList()
        {
            try
            {
                return Ok(UtilService.GetResponse<List<tbl_MeetingOrganizer>>(_eventServices.GetMeetingOrganizerList()));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }

        #endregion



        #region GetMeetingVenueList
        [HttpGet]
        [Route("GetMeetingVenueList")]
        public IActionResult GetMeetingVenueList()
        {
            try
            {
                return Ok(UtilService.GetResponse<List<tbl_MeetingVenue>>(_eventServices.GetMeetingVenueList()));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion

        #region GetMeetingStatus
        [HttpGet]
        [Route("GetMeetingStatus")]
        public IActionResult GetMeetingStatus()
        {
            try
            {
                return Ok(UtilService.GetResponse<List<DDLMeetingStatusModel>>(_eventServices.GetMeetingStatus()));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion







        #region GetMeetingCount

        [HttpGet]
        [Route("GetMeetingCount")]
        public async Task<IActionResult> GetMeetingCount()
        {
            try
            {
                ///Get userid
                Guid userid = Guid.Parse(this.GetUserId());




                MeetingCount MeetingCount;

                if (await _cService.CheckRoleExists(userid, "Admin"))
                {
                    MeetingCount = _eventServices.GetMeetingCount(null);
                }
                else
                {
                    MeetingCount = _eventServices.GetMeetingCount(userid);
                }


                return Ok(UtilService.GetResponse(MeetingCount));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }

        #endregion





        #region UpdateMeetingStatus
        [HttpPost]
        [Route("UpdateMeetingStatus")]
        public IActionResult UpdateMeetingStatus([FromForm] MeetingDetailDTO model)
        {
            try
            {
                int res = 0;


                string userid = this.GetUserId();
                //string userid = "4474BD83-AF36-42F8-3BB1-08D9CAAF2517";


                res = _eventServices.UpdateMeetingStatus(model, userid);

                string msg = "";

                if (res == 1)
                {
                    msg = "Saved Successfully";
                }
                else
                {
                    msg = "Meeting Id Should not be 0 or empty";
                }

                return Ok(UtilService.GetResponse<Json>(null, msg));


            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }
        #endregion



        #region DeleteMeeting
        //[Authorize]
        [HttpGet]
        [Route("DeleteMeeting")]
        public IActionResult DeleteTraining(int Id)
        {
            try
            {

                _eventServices.DeleteMeeting(Id);

                return Ok(UtilService.GetResponse("Meeting Deleted"));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }

        #endregion


        [HttpGet]
        [Route("TotalTrainingMonthCount")]
        public IActionResult TotalTrainingMonthCount()
        {
            try
            {
                return Ok(UtilService.GetResponse<List<TotalTrainingMonthChartView>>(_eventServices.TotalTrainingMonthCount()));
            }
            catch (Exception ex)
            {
                {
                    return BadRequest(UtilService.GetExResponse<Exception>(ex));
                }
            }

        }


        [DisableRequestSizeLimit]
        [HttpPost]
        [Route("AddFile")]
        public async Task<IActionResult> AddFile([FromForm] FileDTO model)
        {
            try
            {
                int res = 0;

                string userid = this.GetUserId();

                res = await _eventServices.AddFile(model, userid);

                string msg = "";

                if (res == 1)
                {
                    msg = "Saved Successfully";
                }
                else
                {
                    msg = "Folder Id Should not be 0 or empty";
                }

                return Ok(UtilService.GetResponse<Json>(null, msg));


            }
            catch (Exception ex)
            {
                return BadRequest(UtilService.GetExResponse<Exception>(ex));
            }
        }


        [HttpGet]
        [Route("GetFiles")]
        public IActionResult GetFiles(int id)
        {
            try
            {
                Guid userid = Guid.Parse(this.GetUserId());

                List<FileDTO> FilesList = _eventServices.FetchFiles(id, "", userid);

                return Ok(UtilService.GetResponse(FilesList));
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }
        [HttpGet]
        [Route("TrainingTypeWiseReport")]
        public IActionResult TrainingTypeWiseReport()
        {
            try
            {
                return Ok(UtilService.GetResponse<List<TrainingTypeWiseReport>>(_eventServices.TrainingTypeWiseReport()));
            }
            catch (Exception ex)
            {
                {
                    return BadRequest(UtilService.GetExResponse<Exception>(ex));
                }
            }

        }


        //[HttpGet]
        //[Route("DownloadFile")]
        //public async Task<IActionResult> DownloadFile(string fileName)
        //{
        //    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Uploads", fileName);


        //    if (System.IO.File.Exists(path))
        //    {
        //        return File(System.IO.File.OpenRead(path), "application/octet-stream", Path.GetFileName(path));
        //    }
        //    return NotFound();
        //}
   
        [HttpGet]
        [Route("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Uploads", fileName);
        
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(filePath), fileName);
        }
        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
        private MemoryStream DownloadSinghFile(string filename, string uploadPath)

        {

            var path = Path.Combine(Directory.GetCurrentDirectory(), uploadPath, filename);

            var memory = new MemoryStream();

            if (System.IO.File.Exists(path))

            {

                var net = new System.Net.WebClient();

                var data = net.DownloadData(path);

                var content = new System.IO.MemoryStream(data);

                memory = content;

            }

            memory.Position = 0;

            return memory;

        }


        #region ImportExcel

        [HttpPost]
        [Route("PostedFile")]
        [HttpPost]
        public IActionResult PostedFile(IFormFile postedFile)
        {
            if (postedFile != null)
            {
                //Create a Folder.
                string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //Save the uploaded Excel file.
                string fileName = Path.GetFileName(postedFile.FileName);
                string filePath = Path.Combine(path, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                //Read the connection string for the Excel file.
                string conString = this.Configuration.GetConnectionString("excelconnection");
                DataTable dt = new DataTable();
                conString = string.Format(conString, filePath);

                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;

                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();

                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }

                //Insert the Data read from the Excel file to Database Table.
                conString = this.Configuration.GetConnectionString("ConnectionString");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.Customers";

                        //[OPTIONAL]: Map the Excel columns with that of the database table.
                        sqlBulkCopy.ColumnMappings.Add("Name", "Name");
                        sqlBulkCopy.ColumnMappings.Add("Country", "Country");
                        

                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }

            return Ok();
        }

        #endregion




        #region DeleteTrainings


        [HttpGet]
        [Route("DeleteTrainingCategory")]
        public IActionResult DeleteTrainingCategory(int id)
        {
            try
            {

                bool res = _eventServices.DeleteTrainingCategory(id);
                if (res)
                {
                    return Ok(UtilService.GetResponse("", "Training Category Deleted Successfully"));
                }
                else
                {
                    return Ok(UtilService.GetResponse<Json>(null, "Failed to delete record"));

                }
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }


        [HttpGet]
        [Route("DeleteTrainingLevel")]
        public IActionResult DeleteTrainingLevel(int id)
        {
            try
            {

                bool res = _eventServices.DeleteTrainingLevel(id);
                if (res)
                {
                    return Ok(UtilService.GetResponse("", "Training Level Deleted Successfully"));
                }
                else
                {
                    return Ok(UtilService.GetResponse<Json>(null, "Failed to delete record"));

                }
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }




        [HttpGet]
        [Route("DeleteTrainingType")]
        public IActionResult DeleteTrainingType(int id)
        {
            try
            {

                bool res = _eventServices.DeleteTrainingType(id);
                if (res)
                {
                    return Ok(UtilService.GetResponse("", "Training Type Deleted Successfully"));
                }
                else
                {
                    return Ok(UtilService.GetResponse<Json>(null, "Failed to delete record"));

                }
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }






        [HttpGet]
        [Route("DeleteTrainingOrganizedBy")]
        public IActionResult DeleteTrainingOrganizedBy(int id)
        {
            try
            {

                bool res = _eventServices.DeleteTrainingOrganizedBy(id);
                if (res)
                {
                    return Ok(UtilService.GetResponse("", "Training Organized by Deleted Successfully"));
                }
                else
                {
                    return Ok(UtilService.GetResponse<Json>(null, "Failed to delete record"));

                }
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }


        [HttpGet]
        [Route("DeleteTrainingSupportedBy")]
        public IActionResult DeleteTrainingSupportedBy(int id)
        {
            try
            {

                bool res = _eventServices.DeleteTrainingSupportedBy(id);
                if (res)
                {
                    return Ok(UtilService.GetResponse("", "Training Supported by Deleted Successfully"));
                }
                else
                {
                    return Ok(UtilService.GetResponse<Json>(null, "Failed to delete record"));

                }
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }

        [HttpGet]
        [Route("DeleteTrainingVenue")]
        public IActionResult DeleteTrainingVenue(int id)
        {
            try
            {

                bool res = _eventServices.DeleteTrainingVenue(id);
                if (res)
                {
                    return Ok(UtilService.GetResponse("", "Training Venue Deleted Successfully"));
                }
                else
                {
                    return Ok(UtilService.GetResponse<Json>(null, "Failed to delete record"));

                }
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }



        [HttpGet]
        [Route("DeleteCadre")]
        public IActionResult DeleteCadre(int id)
        {
            try
            {

                bool res = _eventServices.DeleteCadre(id);
                if (res)
                {
                    return Ok(UtilService.GetResponse("", "Cadre Deleted Successfully"));
                }
                else
                {
                    return Ok(UtilService.GetResponse<Json>(null, "Failed to delete record"));

                }
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }


        [HttpGet]
        [Route("DeleteTrainingQualification")]
        public IActionResult DeleteTrainingQualification(int id)
        {
            try
            {

                bool res = _eventServices.DeleteTrainingQualification(id);
                if (res)
                {
                    return Ok(UtilService.GetResponse("", "Qualification Deleted Successfully"));
                }
                else
                {
                    return Ok(UtilService.GetResponse<Json>(null, "Failed to delete record"));

                }
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }


        [HttpGet]
        [Route("DeleteTrainingDocumentType")]
        public IActionResult DeleteTrainingDocumentType(int id)
        {
            try
            {

                bool res = _eventServices.DeleteTrainingDocumentType(id);
                if (res)
                {
                    return Ok(UtilService.GetResponse("", "Document Type Deleted Successfully"));
                }
                else
                {
                    return Ok(UtilService.GetResponse<Json>(null, "Failed to delete record"));

                }
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }

        [HttpGet]
        [Route("DeleteTrainingPastTraining/{id}")]
        public IActionResult DeleteTrainingPastTraining(int id)
        {
            try
            {

                bool res = _eventServices.DeleteTrainingPastTraining(id);
                if (res)
                {
                    return Ok(UtilService.GetResponse("", "Past Training Deleted Successfully"));
                }
                else
                {
                    return Ok(UtilService.GetResponse<Json>(null, "Failed to delete record"));

                }
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }



        [HttpGet]
        [Route("DeleteTrainingNewTraining/{id}")]
        public IActionResult DeleteTrainingNewTraining(int id)
        {
            try
            {

                bool res = _eventServices.DeleteTrainingNewTraining(id);
                if (res)
                {
                    return Ok(UtilService.GetResponse("", "New Training Deleted Successfully"));
                }
                else
                {
                    return Ok(UtilService.GetResponse<Json>(null, "Failed to delete record"));

                }
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }


        [HttpGet]
        [Route("DeleteTrainingSchduleTraining/{id}")]
        [Obsolete]
        public IActionResult DeleteTrainingSchduleTraining(int id)
        {
            try
            {

                bool res = _eventServices.DeleteTrainingSchduleTraining(id);
                if (res)
                {
                    return Ok(UtilService.GetResponse("", "Schedule Training Deleted Successfully"));
                }
                else
                {
                    return Ok(UtilService.GetResponse<Json>(null, "Failed to delete record"));

                }
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }


        [HttpGet]
        [Route("DeleteTrainingExternalParticipant/{id}")]
        public IActionResult DeleteTrainingExternalParticipant(int id)
        {
            try
            {

                bool res = _eventServices.DeleteTrainingExternalParticipant(id);
                if (res)
                {
                    return Ok(UtilService.GetResponse("", "External Participant Deleted Successfully"));
                }
                else
                {
                    return Ok(UtilService.GetResponse<Json>(null, "Failed to delete record"));

                }
            }
            catch (Exception ex)
            {
                return Ok(UtilService.GetExResponse<Exception>(ex));
            }
        }


        #endregion
    }

}
