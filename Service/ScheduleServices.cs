using AutoMapper;
using Meeting_App.Data.Database.Context;
using Meeting_App.Data.Database.Tables;
using Meeting_App.Data.ResourceCenter.Tables;
using Meeting_App.Migrations;
using Meeting_App.Models;
using Meeting_App.Models.DTOs;
using Meeting_App.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Services.Description;

namespace Meeting_App.Service
{
	public class ScheduleServices
	{

		#region Fields
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _env;
		private readonly NotificationService _notificationService = new NotificationService();
		#endregion

		#region Constructors
		public ScheduleServices(IMapper mapper, IWebHostEnvironment env)
		{

			_mapper = mapper;
			_env = env;
		}
		#endregion

		#region Schedule
		public bool ChangeStatus(ParticipantStatusDto model)
		{

			using (var db = new IDDbContext())
			{
				using (var trans = db.Database.BeginTransaction())
				{
					try
					{
						var _entity = db.TrainingParticipant.Where(x => x.ScheduleId == model.ScheduleId && x.ProfileId == model.ProfileId).FirstOrDefault();

						if (_entity != null)
						{
							_entity.ParticipantStatus = model.Status;
							db.SaveChanges();
						}
						trans.Commit();
						return true;

					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
						trans.Rollback();
						return false;
						throw;
					}
				}


			}
		}
		public bool DeleteFromHRParticepant(ParticipantStatusDto model)
		{

			using (var db = new IDDbContext())
			{
				using (var trans = db.Database.BeginTransaction())
				{
					try
					{
						var _entity = db.TrainingParticipant.Where(x => x.ScheduleId == model.ScheduleId && x.ProfileId == model.ProfileId).FirstOrDefault();

						if (_entity != null)
						{
							db.TrainingParticipant.Remove(_entity);
							db.SaveChanges();
						}
						trans.Commit();
						return true;

					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
						trans.Rollback();
						return false;
						throw;
					}
				}


			}
		}
		public bool ChangeAttendanceStatus(ParticipantStatusDto model)
		{

			using (var db = new IDDbContext())
			{
				using (var trans = db.Database.BeginTransaction())
				{
					try
					{
						var _entity = db.TrainingParticipant.Where(x => x.ScheduleId == model.ScheduleId && x.ProfileId == model.ProfileId).FirstOrDefault();

						if (_entity != null)
						{
							_entity.AttendanceStatus = model.Status;
							if (model.Status == "Absent")
							{
								_entity.ObtainMarksAfterTraining = null;
								_entity.ObtainMarksBeforeTrainig = null;
							}
							db.SaveChanges();
						}
						trans.Commit();
						return true;

					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
						trans.Rollback();
						return false;
						throw;
					}
				}


			}
		}




		public bool changeExternalParticipantStatus(ParticipantStatusDto model)
		{

			using (var db = new IDDbContext())
			{
				using (var trans = db.Database.BeginTransaction())
				{
					try
					{
						var _entity = db.ExternalParticepantWithSchedule.Where(x => x.ScheduleId == model.ScheduleId && x.ParticipantId == model.ProfileId).FirstOrDefault();

						if (_entity != null)
						{
							_entity.AttendanceStatus = model.Status;
							db.SaveChanges();
						}
						trans.Commit();
						return true;

					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
						trans.Rollback();
						return false;
						throw;
					}
				}


			}
		}
		public bool DeleteExternalParticipant(ParticipantStatusDto model)
		{

			using (var db = new IDDbContext())
			{
				using (var trans = db.Database.BeginTransaction())
				{
					try
					{
						var _entity = db.ExternalParticepantWithSchedule.Where(x => x.ScheduleId == model.ScheduleId && x.ParticipantId == model.ProfileId).FirstOrDefault();

						if (_entity != null)
						{
							db.ExternalParticepantWithSchedule.Remove(_entity);
							db.SaveChanges();
						}
						trans.Commit();
						return true;

					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
						trans.Rollback();
						return false;
						throw;
					}
				}


			}
		}

		public bool ChangeParticipantMarks(TrainingParticipant model)
		{

			using (var db = new IDDbContext())
			{
				using (var trans = db.Database.BeginTransaction())
				{
					try
					{
						var _entity = db.TrainingParticipant.Where(x => x.ScheduleId == model.ScheduleId && x.ProfileId == model.ProfileId).FirstOrDefault();

						if (_entity != null)
						{
							if (model.ObtainMarksBeforeTrainig != null && model.ObtainMarksAfterTraining != null)
							{
								_entity.ObtainMarksBeforeTrainig = model.ObtainMarksBeforeTrainig;
								_entity.ObtainMarksAfterTraining = model.ObtainMarksAfterTraining;
								_entity.TotalMarks = model.TotalMarks;
								_entity.ParticipantTrainingStatus = null;
							}
							else
							{
								_entity.ObtainMarksBeforeTrainig = model.ObtainMarksBeforeTrainig;
								_entity.ObtainMarksAfterTraining = model.ObtainMarksAfterTraining;
								//_entity.TotalMarks = model.TotalMarks;
								_entity.ParticipantTrainingStatus = model.ParticipantTrainingStatus;
							}
							db.SaveChanges();
						}
						trans.Commit();
						return true;

					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
						trans.Rollback();
						return false;
						throw;
					}
				}


			}
		}





		public bool SendEmail(ParticipantStatusDto model)
		{

			using (var db = new IDDbContext())
			{
				using (var trans = db.Database.BeginTransaction())
				{
					try
					{
						var _entity = db.TrainingParticipant.Where(x => x.ScheduleId == model.ScheduleId && x.ProfileId == model.ProfileId).FirstOrDefault();

						if (_entity != null)
						{
							//   _entity.AttendanceStatus = model.Status;
							db.SaveChanges();
						}
						trans.Commit();
						return true;

					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
						trans.Rollback();
						return false;
						throw;
					}
				}


			}
		}
		public long AddSchedule(ScheduleDTO model, string userid)
		{

			using (var db = new IDDbContext())
			{
				using (var trans = db.Database.BeginTransaction())
				{
					try
					{
						var _entity = new ScheduleTraining();
						_entity.TrainingId = model.TrainingId;
						if (model.IsVirtual == true)
						{
							var venue = db.tbl_MeetingVenue.Where(x => x.Venue == "Virtual").FirstOrDefault();
							_entity.VenueId = venue.Id;
						}
						else
						{
							_entity.VenueId = model.VenueId;
						}
						if (model.IsVirtual == false)
						{
							_entity.IsVirtual = false;
						}
						_entity.StartDate = model.StartDateTime;
						_entity.EndDate = model.EndDateTime;
						_entity.Division = model.Division;
						_entity.District = model.District;
						_entity.Tehsil = model.Tehsil;
						_entity.OrganizedBy = model.OrganizedBy;
						_entity.SupportedBy = model.SupportedBy;
						_entity.CreatedBy = Guid.Parse(userid);
						_entity.IsDone = false;
						_entity.CreationDate = UtilService.GetPkCurrentDateTime();
						_entity.RecordStatus = true;
						db.ScheduleTraining.AddAsync(_entity);
						db.SaveChanges();



						model.Id = _entity.Id;
						trans.Commit();
						return model.Id;

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


		public List<TrainingWiseReport> GetScheduleReport()
		{

			using (var db = new IDDbContext())
			{

				using (var trans = db.Database.BeginTransaction())
				{
					try
					{

						var result = db.TrainingWiseReport.ToList();








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




		public List<TrainingWiseReportWithDate> GetScheduleWithStartdate()
		{

			using (var db = new IDDbContext())
			{

				using (var trans = db.Database.BeginTransaction())
				{
					try
					{

						var result = db.TrainingWiseReportWithDate.ToList();


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
		public List<ScheduleList> GetSchedule(Guid userid, string Sreach)
		{

			using (var db = new IDDbContext())
			{

				using (var trans = db.Database.BeginTransaction())
				{
					try
					{
						if (string.IsNullOrEmpty(Sreach))
						{
							var result = db.ScheduleList.Where(x => x.RecordStatus == true).ToList();
							return result;
						}
						else
						{
							var result = db.ScheduleList.Where(x => x.RecordStatus == true && x.Title.Contains(Sreach)).ToList();
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
		public List<ScheduleList> GetPreviousSchedule(string Sreach)
		{

			using (var db = new IDDbContext())
			{

				using (var trans = db.Database.BeginTransaction())
				{
					try
					{
						if (string.IsNullOrEmpty(Sreach))
						{
							var result = db.ScheduleList.Where(x => x.IsPrevious == true && x.RecordStatus == true && x.IsPrevious != null).ToList();
							return result;
						}
						else
						{
							var result = db.ScheduleList.Where(x => x.IsPrevious == true && x.RecordStatus == true && x.IsPrevious != null && x.Title.Contains(Sreach)).ToList();
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

		public List<ScheduleList> GetPeriviousSchedule()
		{

			using (var db = new IDDbContext())
			{

				using (var trans = db.Database.BeginTransaction())
				{
					try
					{

						var result = db.ScheduleList.Where(x => x.IsPrevious == true).ToList();








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
		public List<ScheduleTrainingwithProfileView> GetSchedulebyUser(Guid UserId)
		{

			using (var db = new IDDbContext())
			{

				using (var trans = db.Database.BeginTransaction())
				{
					try
					{
						var user = db.AspNetUsers.Where(x => x.Id == UserId).FirstOrDefault();
						var result = db.ScheduleTrainingwithProfileView.Where(x => x.CNIC == user.UserName && x.ParticipantStatus != null).ToList();
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
		public List<ScheduleTrainingwithProfileView> GetParticepantBySchedule(int Id)
		{

			using (var db = new IDDbContext())
			{

				using (var trans = db.Database.BeginTransaction())
				{
					try
					{

						var result = db.ScheduleTrainingwithProfileView.Where(x => x.Id == Id).ToList();
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
		public async Task<List<ScheduleTrainingwithProfileView>> GetSchedule(string Cnic)
		{

			using (var db = new IDDbContext())
			{

				using (var trans = db.Database.BeginTransaction())
				{
					try
					{
						var result = await db.ScheduleTrainingwithProfileView.Where(x => x.CNIC == Cnic).ToListAsync();
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
		public ScheduleDto GetScheduleWithParticepants(int Id, string userId)
		{

			using (var db = new IDDbContext())
			{

				using (var trans = db.Database.BeginTransaction())
				{
					try
					{

						var result = db.ScheduleTrainingwithProfileView.Where(x => x.Id == Id).ToList();
						if (result.Count > 0)
						{


							var _schedule = result.FirstOrDefault();
							ScheduleDto _resultModel = new ScheduleDto();
							_resultModel.Id = _schedule.Id;
							_resultModel.TrainingId = _schedule.TrainingId;
							_resultModel.Title = _schedule.Title;
							_resultModel.Venue = _schedule.Venue;
							_resultModel.StartDate = _schedule.StartDate;
							_resultModel.EndDate = _schedule.EndDate;
							_resultModel.Departments = _schedule.Departments;
							_resultModel.Description = _schedule.Description;
							_resultModel.TraingCategore = _schedule.TraingCategore;
							_resultModel.TrainingType = _schedule.TrainingType;
							_resultModel.OrganizedBy = _schedule.OrganizedBy;
							_resultModel.SupportedBy = _schedule.SupportedBy;
							_resultModel.TrainingLevel = _schedule.TrainingLevel;
							_resultModel.TotalParticipants = _schedule.TotalParticipants;
							_resultModel.Cadre = _schedule.Cadre;
							_resultModel.IsDigitized = _schedule.IsDigitized;
							_resultModel.Division = _schedule.Division;
							_resultModel.District = _schedule.District;
							_resultModel.Tehsil = _schedule.Tehsil;
							_resultModel.Month = _schedule.Month;
							_resultModel.Year = _schedule.Year;
							_resultModel.Virtual = _schedule.Virtual;
							_resultModel.VenueNotAvailable = _schedule.VenueNotAvailable;
							_resultModel.District = _schedule.District;
							_resultModel.Division = _schedule.Division;
							_resultModel.Tehsil = _schedule.Tehsil;
							_resultModel.DesignationName = _schedule.Designation_Name;

							//model.VenueName = db.tbl_MeetingVenue.Where(x => x.Id == _schedule.Venue).FirstOrDefault();


							var _traineeResultModel = result.Where(x => x.ParticipantType == "Trainee").ToList();
							var _trainerResultModel = result.Where(x => x.ParticipantType == "Trainer").ToList();
							_resultModel.TraineeList = new List<TraineeInfo>();
							_resultModel.TrainerList = new List<TrainerInfo>();
							foreach (var item in _traineeResultModel)
							{
								TraineeInfo model = new TraineeInfo();
								model.ProfileId = item.ProfileId;
								model.ScheduleId = item.Id;
								model.Name = item.EmployeeName;
								model.Cnic = item.CNIC;
								model.WorkingPlace = item.HealthFacility;
								model.ObtainMarksBeforeTrainig = item.ObtainMarksBeforeTrainig;
								model.ObtainMarksAfterTraining = item.ObtainMarksAfterTraining;
								model.TotalMarks = item.TotalMarks;
								model.WorkedPlaceAtTraining = item.WorkingPlaceAtTraining;
								model.ParticipantTrainingStatus = item.ParticipantTrainingStatus;
								model.DesignationName = item.Designation_Name;
								if (string.IsNullOrEmpty(item.ParticipantStatus))
								{
									model.Status = "Ready For Request";
								}
								else
								{
									model.Status = item.ParticipantStatus;
								}
								if (string.IsNullOrEmpty(item.AttendanceStatus))
								{
									model.AttendanceStatus = "No Status";
								}
								else
								{
									model.AttendanceStatus = item.AttendanceStatus;
								}
								_resultModel.TraineeList.Add(model);



							}
							foreach (var item in _trainerResultModel)
							{
								TrainerInfo model = new TrainerInfo();
								model.ProfileId = item.ProfileId;
								model.ScheduleId = item.Id;
								model.Name = item.EmployeeName;
								model.Cnic = item.CNIC;
								model.WorkingPlace = item.HealthFacility;
								model.WorkedPlaceAtTraining = item.WorkingPlaceAtTraining;
								model.DesignationName = item.Designation_Name;

								if (string.IsNullOrEmpty(item.ParticipantStatus))
								{
									model.Status = "Ready For Request";
								}
								else
								{
									model.Status = item.ParticipantStatus;
								}
								if (string.IsNullOrEmpty(item.AttendanceStatus))
								{
									model.AttendanceStatus = "No Status";
								}
								else
								{
									model.AttendanceStatus = item.AttendanceStatus;
								}
								_resultModel.TrainerList.Add(model);
							}

							_resultModel.ExternalParticipants = db.GetExternalParticipantView.Where(x => x.CreatedBy == userId && x.ScheduleId == Id).ToList();
							int index = 0;
							foreach (var item in _resultModel.ExternalParticipants)
							{
								if (string.IsNullOrEmpty(item.AttendanceStatus))
								{
									_resultModel.ExternalParticipants[index].AttendanceStatus = "No Status";
								}
								else
								{
									_resultModel.ExternalParticipants[index].AttendanceStatus = item.AttendanceStatus;
								}
								if (string.IsNullOrEmpty(item.ParticipantStatus))
								{
									_resultModel.ExternalParticipants[index].ParticipantTrainingStatus = "Ready For Request";
								}
								else
								{
									_resultModel.ExternalParticipants[index].ParticipantTrainingStatus = item.ParticipantTrainingStatus;
								}
								++index;
							}

							return _resultModel;
						}
						else
						{
							return null;
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
		#region ExternalParticepant

		public long AddExternalParticepant(List<ExternalParticepantDto> resultModel, string userid)
		{


			using (var db = new IDDbContext())
			{
				using (var trans = db.Database.BeginTransaction())
				{
					try
					{
						foreach (var item in resultModel)
						{
							var externalParticipant = db.ExternalParticepant.Where(x => x.Id == item.ScheduleId).FirstOrDefault();
							if (externalParticipant != null)
							{
								var _entity = new ExternalParticepant();
								externalParticipant.Name = item.Name;
								externalParticipant.FatherName = item.FatherName;
								externalParticipant.CNIC = item.CNIC;
								externalParticipant.Department = item.Department;
								externalParticipant.Profession = item.Profession;
								externalParticipant.JobTittle = item.JobTittle;
								externalParticipant.Email = item.Email;
								externalParticipant.ParticipantType = item.ParticipantType;
								externalParticipant.WorkingPlace = item.WorkingPlace;
								externalParticipant.MobileNo = item.MobileNo;
								externalParticipant.RecordStatus = true;
								externalParticipant.Qualification = item.Qualification;
								externalParticipant.CreatedBy = userid;
								externalParticipant.CreationDate = DateTime.Now;
								if (!string.IsNullOrEmpty(item.Division))
								{
									externalParticipant.Division = item.Division.Substring(item.Division.Length - 3);
								}
								if (!string.IsNullOrEmpty(item.District))
								{
									externalParticipant.District = item.District.Substring(item.District.Length - 3);
								}
								if (!string.IsNullOrEmpty(item.Tehsil))
								{
									externalParticipant.Tehsil = item.Tehsil.Substring(item.Tehsil.Length - 3);
								}
								db.SaveChanges();
							}
							else
							{
								ExternalParticepantWithSchedule Exparticepant = db.ExternalParticepantWithSchedule.Where(x => x.ScheduleId == item.ScheduleId && x.ParticipantId == x.Id).FirstOrDefault();
								if (Exparticepant == null)
								{
									var _entity = new ExternalParticepant();
									_entity.Name = item.Name;
									_entity.FatherName = item.FatherName;
									_entity.CNIC = item.CNIC;
									_entity.Department = item.Department;
									_entity.Profession = item.Profession;
									_entity.JobTittle = item.JobTittle;
									_entity.Email = item.Email;
									_entity.ParticipantType = item.ParticipantType;
									_entity.WorkingPlace = item.WorkingPlace;
									_entity.MobileNo = item.MobileNo;
									_entity.RecordStatus = true;
									_entity.CreatedBy = userid;
									_entity.CreationDate = DateTime.Now;
									if (!string.IsNullOrEmpty(item.Division))
									{
										_entity.Division = item.Division.Substring(item.Division.Length - 3);
									}
									if (!string.IsNullOrEmpty(item.District))
									{
										_entity.District = item.District.Substring(item.District.Length - 3);
									}
									if (!string.IsNullOrEmpty(item.Tehsil))
									{
										_entity.Tehsil = item.Tehsil.Substring(item.Tehsil.Length - 3);
									}
									db.ExternalParticepant.AddAsync(_entity);
									db.SaveChanges();
								}
							}


						}


						//_entity.CreatedBy = Guid.Parse(userid);
						//_entity.IsDone = false;
						//_entity.CreationDate = UtilService.GetPkCurrentDateTime();





						trans.Commit();
						return 1;

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


		public bool AddExternalParticepantwithScheduleId(List<ExternalParticepantDto> resultModel, string userid)
		{

			using (var db = new IDDbContext())
			{
				using (var trans = db.Database.BeginTransaction())
				{
					try
					{
						foreach (var item in resultModel)
						{
							var particepant = db.ExternalParticepantWithSchedule.Where(x => x.ScheduleId == item.ScheduleId && x.ParticipantId == item.Id).FirstOrDefault();
							if (particepant == null)
							{
								var _entity = new ExternalParticepantWithSchedule();
								_entity.ScheduleId = item.ScheduleId;
								_entity.ParticipantId = item.Id;
								_entity.CreatedBy = userid;
								_entity.CreatedDate = DateTime.Now;
								db.ExternalParticepantWithSchedule.AddAsync(_entity);
								db.SaveChanges();
								trans.Commit();
								return true;
							}
							else
							{
								trans.Commit();
								return false;
							}
						}

						return false;

						//_entity.CreatedBy = Guid.Parse(userid);
						//_entity.IsDone = false;
						//_entity.CreationDate = UtilService.GetPkCurrentDateTime();






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
		public bool ReplaceExternalParticepantwithScheduleId(TraineeDto model, string userid)
		{

			bool IsDone = false;
			int count = 0;
			using (var db = new IDDbContext())
			{
				using (var trans = db.Database.BeginTransaction())
				{
					try
					{


						var _entity = new ExternalParticepantWithSchedule();
						_entity.Id = 0;


						foreach (var item in model.ExternalParticipants)
						{
							var exist = db.ExternalParticepantWithSchedule.Where(x => x.ScheduleId == model.ScheduleId && x.ParticipantId == item.Id).FirstOrDefault();
							if (exist == null)
							{

								_entity.ScheduleId = model.ScheduleId;
								_entity.ParticipantId = item.Id;
								if (_entity.Id == 0)
								{
									var profile = db.ExternalParticepant.Where(x => x.Id == model.profileId).FirstOrDefault();

									var _TrainingParticipant = db.ExternalParticepantWithSchedule.Where(x => x.ScheduleId == model.ScheduleId && x.ParticipantId == model.profileId).FirstOrDefault();

									if (_TrainingParticipant != null)
									{
										var Replaceprofile = db.ExternalParticepant.Where(x => x.Id == model.profileId).FirstOrDefault();
										_TrainingParticipant.AttendanceStatus = string.Format("Replace");
										_TrainingParticipant.ParticipantStatus = string.Format("Replace with {0}", profile.Name);
										db.SaveChanges();
									}
								}

								_entity.CreatedBy = userid;
								_entity.CreatedDate = DateTime.UtcNow.AddHours(5);
								db.ExternalParticepantWithSchedule.Add(_entity);
								db.SaveChanges();
								count++;
							}
						}
						trans.Commit();
						if (count > 0)
						{
							IsDone = true;
						}
						else
						{
							IsDone = false;
						}
						return IsDone;

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
		public List<ExternalParticepant> GetExternalParticepant()
		{

			using (var db = new IDDbContext())
			{

				using (var trans = db.Database.BeginTransaction())
				{
					try
					{

						var result = db.ExternalParticepant.Where(x => x.RecordStatus == true).ToList();

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




		public List<ExternalParticepant> GetExternalParticepants()
		{

			using (var db = new IDDbContext())
			{

				using (var trans = db.Database.BeginTransaction())
				{
					try
					{

						var result = db.ExternalParticepant.Where(x => x.RecordStatus == true).OrderByDescending(x => x.CreationDate).ToList();








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




		public ExternalParticepant GetExternalParticepantbyid(int Id)
		{

			using (var db = new IDDbContext())
			{

				using (var trans = db.Database.BeginTransaction())
				{
					try
					{

						var result = db.ExternalParticepant.Where(x => x.RecordStatus == true && x.Id == Id).FirstOrDefault();


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

						var result = db.EventCalender.ToList();



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
		public List<EventCalender> GetTrainings()
		{

			using (var db = new IDDbContext())
			{
				using (var trans = db.Database.BeginTransaction())
				{
					try
					{

						var result = db.EventCalender.ToList();



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



		#endregion
		public List<HrDesignation> GetDesignation()
		{

			using (var db = new IDDbContext())
			{
				using (var trans = db.Database.BeginTransaction())
				{
					try
					{

						var result = db.HrDesignation.ToList();



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

							var Meeting = db.EventCalender.Where(x => x.Id == Id).FirstOrDefault();

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
		public ProfileView ParticipantByCnic(string CNIC)
		{
			using (var db = new IDDbContext())
			{
				ProfileView profileView = db.ProfileView.Where(x => x.CNIC == CNIC).FirstOrDefault();
				return profileView;
			}
		}





		public TrainingDTO CounterCard()
		{

			using (var db = new IDDbContext())
			{
				using (var trans = db.Database.BeginTransaction())
				{
					try
					{
						var training = new TrainingDTO();
						training.TotalTrainings = db.EventCalender.Where(x => x.RecordStatus == true).Select(x => new EventCalender
						{
							Id = x.Id,
							Title = x.Title,
							Description = x.Description,
							TrainingType = x.TrainingType,
							TrainingLevel = x.TrainingLevel,
							TraingCategore = x.TraingCategore,
							Cadre = x.Cadre,
							Departments = x.Departments,
							TotalParticipants = x.TotalParticipants,
							StartDateTime = x.StartDateTime,
							EndDateTime = x.EndDateTime,

						}).ToList();
						training.ScheduleTrainings = db.GetTrainingListView.Where(x => x.StartDate != null && x.EndDate != null).ToList();
						training.UnScheduleTrainings = db.GetTrainingListView.Where(x => x.StartDate == null && x.EndDate == null).ToList();
						training.PendingTrainings = db.GetTrainingListView.Where(x => x.StartDate != null && x.StartDate > DateTime.Now).ToList();
						return training;
					}
					catch (Exception)
					{
						trans.Rollback();
						throw;
					}
				}

			}
		}

		#region Trainee




		public bool AddTrainee(TraineeDto model, string userid)
		{

			using (var db = new IDDbContext())
			{
				using (var trans = db.Database.BeginTransaction())
				{
					try
					{
						bool IsDone = false;
						int count = 0;
						var _entity = new TrainingParticipant();


						foreach (var item in model.Trainees)
						{
							var existtrainee = db.TrainingParticipant.Where(x => x.ScheduleId == model.ScheduleId && x.ProfileId == item.profileId).FirstOrDefault();
							if (existtrainee == null)
							{
								_entity.Id = 0;
								_entity.ScheduleId = model.ScheduleId;
								_entity.ProfileId = item.profileId;
								if (_entity.ProfileId != 0)
								{
									var profile = db.ProfileView.Where(x => x.Id == _entity.ProfileId).FirstOrDefault();
									_entity.WorkingPlaceAtTraining = profile.HealthFacility;
								}
								_entity.ParticipantType = "Trainee";
								_entity.CreatedBy = userid;
								_entity.CreatedDate = DateTime.UtcNow.AddHours(5);
								db.TrainingParticipant.Add(_entity);
								db.SaveChanges();
								count++;
							}

						}
						if (count > 0)
						{
							IsDone = true;
						}
						else
						{
							IsDone = false;
						}

						trans.Commit();
						return IsDone;

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
		public bool ReplaceTrainee(TraineeDto model, string userid)
		{
			bool IsDone = false;
			int count = 0;

			using (var db = new IDDbContext())
			{
				using (var trans = db.Database.BeginTransaction())
				{
					try
					{

						var _entity = new TrainingParticipant();


						foreach (var item in model.Trainees)
						{
							var exist = db.TrainingParticipant.Where(x => x.ScheduleId == model.ScheduleId && x.ProfileId == item.profileId).FirstOrDefault();
							if (exist == null)
							{
								_entity.Id = 0;
								_entity.ScheduleId = model.ScheduleId;
								_entity.ProfileId = item.profileId;
								if (_entity.ProfileId != 0)
								{
									var profile = db.ProfileView.Where(x => x.Id == _entity.ProfileId).FirstOrDefault();
									_entity.WorkingPlaceAtTraining = profile.HealthFacility;
									var _TrainingParticipant = db.TrainingParticipant.Where(x => x.ScheduleId == model.ScheduleId && x.ProfileId == model.profileId).FirstOrDefault();

									if (_TrainingParticipant != null)
									{
										var Replaceprofile = db.ProfileView.Where(x => x.Id == model.profileId).FirstOrDefault();
										_TrainingParticipant.AttendanceStatus = string.Format("Replace");
										_TrainingParticipant.ParticipantStatus = string.Format("Replace with {0}", profile.EmployeeName);
										db.SaveChanges();
									}
								}
								_entity.ParticipantType = "Trainee";
								_entity.CreatedBy = userid;
								_entity.CreatedDate = DateTime.UtcNow.AddHours(5);
								db.TrainingParticipant.Add(_entity);
								db.SaveChanges();
								count++;
							}
						}
						if (count > 0)
						{
							IsDone = true;
						}
						else
						{
							IsDone = false;
						}
						trans.Commit();
						return IsDone;

					}
					catch (Exception ex)
					{
						IsDone = false;
						Console.WriteLine(ex.ToString());
						trans.Rollback();
						return IsDone;
					}
				}


			}
		}
		#endregion
		#region Trainer
		public bool AddTrainer(TraineeDto model, string userid)
		{
			using (var db = new IDDbContext())
			{
				using (var trans = db.Database.BeginTransaction())
				{
					try
					{
						bool IsDone = false;
						int count = 0;
						var _entity = new TrainingParticipant();


						foreach (var item in model.Trainees)
						{
							var exist = db.TrainingParticipant.Where(x => x.ScheduleId == model.ScheduleId && x.ProfileId == item.profileId).FirstOrDefault();
							if (exist == null)
							{
								_entity.Id = 0;
								_entity.ScheduleId = model.ScheduleId;
								_entity.ProfileId = item.profileId;
								if (_entity.ProfileId != 0)
								{
									var profile = db.ProfileView.Where(x => x.Id == _entity.ProfileId).FirstOrDefault();
									_entity.WorkingPlaceAtTraining = profile.HealthFacility;
								}
								_entity.ParticipantType = "Trainer";
								_entity.CreatedBy = userid;
								_entity.CreatedDate = DateTime.UtcNow.AddHours(5);
								db.TrainingParticipant.Add(_entity);
								db.SaveChanges();
								count++;
							}

						}

						if (count > 0)
						{
							IsDone = true;
						}
						else
						{
							IsDone = false;
						}

						trans.Commit();
						return IsDone;

					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
						trans.Rollback();

						return false;
						throw;
					}
				}


			}
		}
		public bool RelaceTrainer(TraineeDto model, string userid)
		{
			using (var db = new IDDbContext())
			{
				using (var trans = db.Database.BeginTransaction())
				{
					try
					{
						bool IsDone = false;
						var _entity = new TrainingParticipant();


						trans.Commit();

						foreach (var item in model.Trainees)
						{
							var exist = db.TrainingParticipant.Where(x => x.ScheduleId == model.ScheduleId && x.ProfileId == item.profileId).FirstOrDefault();
							if (exist == null)
							{
								_entity.Id = 0;
								_entity.ScheduleId = model.ScheduleId;
								_entity.ProfileId = item.profileId;
								if (_entity.ProfileId != 0)
								{
									var profile = db.ProfileView.Where(x => x.Id == _entity.ProfileId).FirstOrDefault();
									_entity.WorkingPlaceAtTraining = profile.HealthFacility;
									var _TrainingParticipant = db.TrainingParticipant.Where(x => x.ScheduleId == model.ScheduleId && x.ProfileId == model.profileId).FirstOrDefault();

									if (_TrainingParticipant != null)
									{
										var Replaceprofile = db.ProfileView.Where(x => x.Id == model.profileId).FirstOrDefault();
										_TrainingParticipant.AttendanceStatus = string.Format("Replace");
										_TrainingParticipant.ParticipantStatus = string.Format("Replace with {0}", profile.EmployeeName);
										db.SaveChanges();
									}
								}
								_entity.ParticipantType = "Trainer";
								_entity.CreatedBy = userid;
								_entity.CreatedDate = DateTime.UtcNow.AddHours(5);
								db.TrainingParticipant.Add(_entity);
								db.SaveChanges();

								IsDone = true;
							}

						}


						return IsDone;

					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.ToString());
						trans.Rollback();

						return false;
						throw;
					}
				}


			}
		}
		#endregion
	}
}
