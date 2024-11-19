﻿using System;
using System.Collections.Generic;

namespace Meeting_App.Data.Database.Tables
{
    public partial class ProfileView
    {
        public int Id { get; set; }
        public long? Srno { get; set; }
        public string EmployeeName { get; set; }
        public string FatherName { get; set; }
        public string CNIC { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Province { get; set; }
        public string MaritalStatus { get; set; }
        public string BloodGroup { get; set; }
        public string SeniorityNo { get; set; }
        public string PersonnelNo { get; set; }
        public int? JoiningGradeBPS { get; set; }
        public int? CurrentGradeBPS { get; set; }
        public string PresentPostingOrderNo { get; set; }
        public DateTime? PresentPostingDate { get; set; }
        public string AdditionalQualification { get; set; }
        public string Status { get; set; }
        public DateTime? DateOfFirstAppointment { get; set; }
        public int? LengthOfService { get; set; }
        public DateTime? SuperAnnuationDate { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public DateTime? LastPromotionDate { get; set; }
        public string PermanentAddress { get; set; }
        public string CorrespondenceAddress { get; set; }
        public string LandlineNo { get; set; }
        public string MobileNo { get; set; }
        public string EMaiL { get; set; }
        public string PrivatePractice { get; set; }
        public string PresentStationLengthOfService { get; set; }
        public string HfmisCode { get; set; }
        public string Tenure { get; set; }
        public string Remarks { get; set; }
        public byte[] Photo { get; set; }
        public int? Cadre_Id { get; set; }
        public string HighestQualification { get; set; }
        public string MobileNoOfficial { get; set; }
        public int? Postaanctionedwithscale { get; set; }
        public string Faxno { get; set; }
        public string HoD { get; set; }
        public string Fp { get; set; }
        public string Hfac { get; set; }
        public DateTime? DateOfRegularization { get; set; }
        public string Tbydeo { get; set; }
        public string DateOfCourse { get; set; }
        public string RtmcNo { get; set; }
        public string PmdcNo { get; set; }
        public string CourseDuration { get; set; }
        public string PgSpecialization { get; set; }
        public string Category { get; set; }
        public string RemunerationStatus { get; set; }
        public string PgFlag { get; set; }
        public string CourseName { get; set; }
        public bool? AddToEmployeePool { get; set; }
        public long? EntityLifecycle_Id { get; set; }
        public string ProfilePhoto { get; set; }
        public int? Disability_Id { get; set; }
        public string DisablityType { get; set; }
        public string Disability { get; set; }
        public string FileNumber { get; set; }
        public string AttachedWith { get; set; }
        public int? AttachedWith_Id { get; set; }
        public DateTime? PresentJoiningDate { get; set; }
        public string VacCertificate { get; set; }
        public int? PPSCMeritNumber { get; set; }
        public DateTime? FirstJoiningDate { get; set; }
        public int? ModeId { get; set; }
        public string ModeName { get; set; }
        public bool? IsVerified { get; set; }
        public string VerifiedBy { get; set; }
        public DateTime? VerifiedDatetime { get; set; }
        public string VerifiedUserId { get; set; }
        public DateTime? FirstOrderDate { get; set; }
        public string FirstOrderNumber { get; set; }
        public string RegularOrderNumber { get; set; }
        public string PromotionOrderNumber { get; set; }
        public string ContractOrderNumber { get; set; }
        public DateTime? ContractOrderDate { get; set; }
        public string OtherContract { get; set; }
        public int? PeriodofContract { get; set; }
        public DateTime? PromotionJoiningDate { get; set; }
        public string RegisterThumb { get; set; }
        public bool? IsRegistered { get; set; }
        public int? Sub_StatusId { get; set; }
        public int? Is_AdditionalCharge { get; set; }
        public string AdditionalCharge { get; set; }
        public string SubStatusName { get; set; }
        public string Division { get; set; }
        public string DivisionCode { get; set; }
        public string District { get; set; }
        public string DistrictCode { get; set; }
        public string Tehsil { get; set; }
        public string TehsilCode { get; set; }
        public string WorkingDivision { get; set; }
        public string WorkingDivisionCode { get; set; }
        public string WorkingDistrict { get; set; }
        public string WorkingDistrictCode { get; set; }
        public string WorkingTehsil { get; set; }
        public string WorkingTehsilCode { get; set; }
        public string DivisionOld { get; set; }
        public string DistrictOld { get; set; }
        public string TehsilOld { get; set; }
        public string HealthFacility { get; set; }
        public string HFTypeCode { get; set; }
        public string HFTypeName { get; set; }
        public int? HealthFacility_Id { get; set; }
        public string WorkingHealthFacility { get; set; }
        public string WHFTypeCode { get; set; }
        public string WHFTypeName { get; set; }
        public int? WorkingHealthFacility_Id { get; set; }
        public string WorkingHFMISCode { get; set; }
        public string StatusName { get; set; }
        public int? Status_Id { get; set; }
        public string QualificationName { get; set; }
        public int? Qualification_Id { get; set; }
        public string SpecializationName { get; set; }
        public int? Specialization_Id { get; set; }
        public int? Designation_Id { get; set; }
        public string Designation_Name { get; set; }
        public int? Designation_HrScale_Id { get; set; }
        public int? PostTypeModeId { get; set; }
        public string HrPostName { get; set; }
        public int? PostTypeValueId { get; set; }
        public string PostSubTypeName { get; set; }
        public int? JDesignation_Id { get; set; }
        public string JDesignation_Name { get; set; }
        public int? JDesignation_HrScale_Id { get; set; }
        public string Cadre_Name { get; set; }
        public int? WDesignation_Id { get; set; }
        public string WDesignation_Name { get; set; }
        public int? WDesignation_HrScale_Id { get; set; }
        public int? Department_Id { get; set; }
        public string Department_Name { get; set; }
        public int? EmpMode_Id { get; set; }
        public string EmpMode_Name { get; set; }
        public int? Domicile_Id { get; set; }
        public string Domicile_Name { get; set; }
        public int? Posttype_Id { get; set; }
        public string PostType_Name { get; set; }
        public int? Religion_Id { get; set; }
        public string Religion_Name { get; set; }
        public int? Language_Id { get; set; }
        public string Language_Name { get; set; }
        public string DesignationVacancy { get; set; }
        public string WDesignationVacancy { get; set; }
        public string JDesignationVacancy { get; set; }
    }
}
