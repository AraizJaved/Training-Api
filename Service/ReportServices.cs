using AutoMapper;
using Meeting_App.Data.Database.Context;
using Meeting_App.Data.Database.Tables;
using Meeting_App.Models;
using Meeting_App.Models.DTOs;
using Meeting_App.Service.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
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
    public class ReportServices
    {

        #region Fields
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly NotificationService _notificationService = new NotificationService();
        #endregion

        #region Constructors
        public ReportServices(IMapper mapper, IWebHostEnvironment env)
        {

            _mapper = mapper;
            _env = env;
        }
        #endregion


        public ReportViewModel GetReports(Guid userid)
        {

            using (var db = new IDDbContext())
            {

                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        ReportViewModel _resultModel = new ReportViewModel();

                        _resultModel.TrainingWiseReportList = new List<TrainingWiseReport>();
                        _resultModel.CadreWiseReportList = new List<CadreWiseReport>();
                        _resultModel.TrainingLevelWiseReportList = new List<TrainingLevelWiseReport>();
                        _resultModel.TrainingTypeWiseReportList = new List<TrainingTypeWiseReport>();
                        _resultModel.DepartmentWiseReportList = new List<DepartmentWiseReport>();
                        _resultModel.VenueWiseReportList = new List<VenueWiseReport>();
                        _resultModel.TraineeWiseReportList = new List<TraineeWiseReport>();
                        _resultModel.TrainerWiseReportList = new List<TrainerWiseReport>();
                        _resultModel.TrainingCategoryWiseReport = new List<TrainingCategoryWiseReport>();
                        _resultModel.TrainingWiseReportList = db.TrainingWiseReport.ToList();
                        _resultModel.TraineeWiseReportList = db.TraineeWiseReport.ToList();
                        var ExternalParticipantTrainee = db.ExternalParticipantWiseReport.Where(x=>x.ParticipantType== "Trainee" ).ToList();
                        foreach (var item in ExternalParticipantTrainee)
                        {
                            TraineeWiseReport model = new TraineeWiseReport();
                            model.ProfileId= item.Id;
                            model.EmployeeName = item.Name;
                            model.WorkingHealthFacility = item.WorkingHealthFacility;
                            model.TotalTrainings=item.TotalTrainings;
                            model.cnic=item.CNIC;
                            _resultModel.TraineeWiseReportList.Add(model);

                        }
                        var ExternalParticipantTrainer = db.ExternalParticipantWiseReport.Where(x => x.ParticipantType == "Trainer").ToList();

                        _resultModel.TrainerWiseReportList = db.TrainerWiseReport.ToList();
                        foreach (var item in ExternalParticipantTrainer)
                        {
                            TrainerWiseReport model = new TrainerWiseReport();
                            model.ProfileId = item.Id;
                            model.EmployeeName = item.Name;
                            model.TotalTrainings = item.TotalTrainings;
                            model.WorkingHealthFacility = item.WorkingHealthFacility;
                            model.cnic = item.CNIC;
                            _resultModel.TrainerWiseReportList.Add(model);

                        }
                        _resultModel.ExternalParticipantWiseReport = db.ExternalParticipantWiseReport.ToList();



                        return _resultModel;


                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }


            }
        }
       

        //public List<TrainingWiseReport> GetTrainingTypeDetail(string TrainingType)
        //{

        //    using (var db = new IDDbContext())
        //    {

        //        using (var trans = db.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                var _resultModel = db.TrainingWiseReport.Where(x => x.TrainingType == TrainingType).ToList();
        //                return _resultModel;


        //            }
        //            catch (Exception)
        //            {
        //                trans.Rollback();
        //                throw;
        //            }
        //        }


        //    }
        //}
        public ParticepantViewModel GetParticepantDetail(int ProfileId)
        {

            using (var db = new IDDbContext())
            {

                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        ParticepantViewModel _resultModel = new ParticepantViewModel();
                        _resultModel.TrainingList = new List<ScheduleTrainingwithProfileView>();
                        _resultModel.TrainingList = db.ScheduleTrainingwithProfileView.Where(x => x.ProfileId == ProfileId).ToList();
                        if (_resultModel.TrainingList.Count > 0)
                        {
                            var model = _resultModel.TrainingList.FirstOrDefault();

                            _resultModel.CNIC = model.CNIC;
                            _resultModel.EmployeeName = model.EmployeeName;
                            _resultModel.WorkingHealthFacility = model.HealthFacility;
                            _resultModel.Designation_Name = model.Designation_Name;

                            return _resultModel;
                        }
                        else
                        {
                            
                             var TrainingList = db.GetExternalParticipantView.Where(x => x.Id == ProfileId).ToList();
                            foreach (var item in TrainingList)
                            {
                                ScheduleTrainingwithProfileView _training = new ScheduleTrainingwithProfileView();
                                _training.StartDate = item.StartDate;
                                _training.EndDate=item.EndDate;
                                _training.Title = item.Title;
                                _training.TrainingType= item.TrainingType;
                                
                                _resultModel.TrainingList.Add(_training );
                            }

                            var model = TrainingList.FirstOrDefault();

                            _resultModel.CNIC = model.CNIC;
                            _resultModel.EmployeeName = model.Name;
                            _resultModel.WorkingHealthFacility = model.WorkingPlace;                       

                            return _resultModel;

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
        public ParticepantViewModel GetExternalParticepantDetail(int ProfileId)
        {

            using (var db = new IDDbContext())
            {

                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        ParticepantViewModel _resultModel = new ParticepantViewModel();
                        _resultModel.TrainingList = new List<ScheduleTrainingwithProfileView>();
                         var TrainingList = db.GetExternalParticipantView.Where(x => x.Id == ProfileId).ToList();
                            foreach (var item in TrainingList)
                            {
                                ScheduleTrainingwithProfileView _training = new ScheduleTrainingwithProfileView();
                                _training.StartDate = item.StartDate;
                                _training.EndDate = item.EndDate;
                                _training.Title = item.Title;
                                _training.TrainingType = item.TrainingType;

                                _resultModel.TrainingList.Add(_training);
                            }

                            var model = TrainingList.FirstOrDefault();

                            _resultModel.CNIC = model.CNIC;
                            _resultModel.EmployeeName = model.Name;
                            _resultModel.WorkingHealthFacility = model.WorkingPlace;

                            return _resultModel;

                  

                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }


            }
        }
        public List<TrainingWiseReport> FilterByTraining(int Id)
        {

            using (var db = new IDDbContext())
            {

                using (var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        var TrainingWiseReportList = db.TrainingWiseReport.Where(x => x.TrainingId == Id).ToList();

                        return TrainingWiseReportList;


                    }
                    catch (Exception)
                    {
                        trans.Rollback();
                        throw;
                    }
                }


            }
        }


    }
}
