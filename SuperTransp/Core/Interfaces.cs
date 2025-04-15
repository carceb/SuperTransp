﻿
using SuperTransp.Models;

namespace SuperTransp.Core
{
	public class Interfaces
	{
		public interface ISecurity
		{
			public SecurityUserViewModel GetValidUser(string login, string password);
			public List<SecurityUserViewModel> GetAllUsers();
			public List<SecurityUserViewModel> GetAllUsersByStateId(int stateId);
			public SecurityUserViewModel GetUserById(int securityUserId);
			public bool GroupHasAccessToModule(int securityGroupId, int securityModuleId);
			public List<SecurityGroupModel> GetAllGroups();
			public List<SecurityGroupModel> GetGroupById(int groupId);
			public List<SecurityModuleModel> GetModuleById(int securityModuleId);
			public List<SecurityModuleModel> GetAllModules();
			public List<SecurityStatusUserModel> GetAllUsersStatus();
			public List<SecurityAccessTypeModel> GetAllAccessTypes();
			public List<SecurityGroupModuleModel> GetAllSecurityGroupModuleDetail();
			public List<SecurityModuleModel> GetModulesByGroupId(int groupId);
			public int AddOrEditUser(SecurityUserViewModel model);
			public int RegisteredUser(string paramValue, string verifyBy);
			public int AddOrEditGroup(SecurityGroupModel model);
			public int AddOrEditModule(SecurityModuleModel model);
			public int AddOrEditGroupModules(SecurityGroupModuleModel model);
			public int DeleteGroupModules(int securityGroupModuleId);
			public int ChangePassword(SecurityUserViewModel model);
			public bool OldPasswordValid(int securityUserId, string oldPassword);
			public string? Encrypt(string plainText);
			public string? Decrypt(string encryptedText);
		}
		public interface IGeography
		{
			public List<GeographyViewModel> GetAllStates();
			public List<GeographyViewModel> GetStateById(int stateId);
			public List<GeographyViewModel> GetAllMunicipalities();
			public List<GeographyViewModel> GetMunicipalityByStateId(int stateId);
		}

		public interface IDesignation
		{
			public List<DesignationViewModel> GetAll();
			public int AddOrEdit(DesignationViewModel model);
		}

		public interface IMode
		{
			public int AddOrEdit(ModeViewModel model);
			public List<ModeViewModel> GetAll();
		}

		public interface IUnion
		{
			public int AddOrEdit(UnionViewModel model);
			public List<UnionViewModel> GetAll();
			public List<UnionViewModel> GetByStateId(int stateId);
		}

		public interface IVehicleData
		{
			public List<VehicleDataViewModel> GetAll();
		}

		public interface IPublicTransportGroup
		{
			public int AddOrEdit(PublicTransportGroupViewModel model);
			public PublicTransportGroupViewModel GetPublicTransportGroupById(int publicTransportGroupId);
			public string? RegisteredRif(string publicTransportGroupRif);
			public List<PublicTransportGroupViewModel> GetAll();
			public List<PublicTransportGroupViewModel> GetByStateId(int stateId);
		}
		public interface IDriver
		{
			public int AddOrEdit(DriverViewModel model);
			public List<DriverViewModel> GetByPublicTransportGroupId(int publicTransportGroupId);
			public DriverViewModel GetByDriverPublicTransportGroupId(int driverPublicTransportGroupId);
			public DriverViewModel GetByIdentityDocument(int driverIdentityDocument);
			public bool RegisteredDocumentId(int driverIdentityDocument, int publicTransportGroupId);
			public bool RegisteredPhone(string driverPhone, int publicTransportGroupId);
			public bool RegisteredPartnerNumber(int partnerNumber, int publicTransportGroupId);
			public List<DriverViewModel> GetAll();
			public bool Delete(int driverId);
		}

		public interface ISupervision
		{
			public int AddOrEdit(SupervisionViewModel model);
			public int AddSimple(SupervisionViewModel model);
			public List<PublicTransportGroupViewModel> GetDriverPublicTransportGroupByStateId(int stateId);
			public List<PublicTransportGroupViewModel> GetAllDriverPublicTransportGroup();
			public List<PublicTransportGroupViewModel> RegisteredPlate(string plate);
			public int AddOrEditSummary(SupervisionSummaryViewModel model);
		}

		public interface ICommonData
		{
			public List<CommonDataViewModel> GetYesNo();
			public List<CommonDataViewModel> GetYears();
			public List<CommonDataViewModel> GetMakesByYear(int year);
			public List<CommonDataViewModel> GetModelsByYearAndMake(int year, string make);
			public List<CommonDataViewModel> GetPassengers();
			public List<CommonDataViewModel> GetRims();
			public List<CommonDataViewModel> GetWheels();
			public List<CommonDataViewModel> GetFuelTypes();
			public List<CommonDataViewModel> GetTankCapacity();
			public List<CommonDataViewModel> GetBatteries();
			public List<CommonDataViewModel> GetNumberOfBatteries();
			public List<CommonDataViewModel> GetMotorOil();
			public List<CommonDataViewModel> GetOilLitters();
			public List<CommonDataViewModel> GetFailureType();
			public int AddOrEditMakeModel(CommonDataViewModel model);
		}
	}
}
