﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SuperTransp.Core;
using SuperTransp.Models;
using System.Security;
using static SuperTransp.Core.Interfaces;

namespace SuperTransp.Controllers
{
	public class PublicTransportGroupController : Controller
	{
		private ISecurity _security;
		private IGeography _geography;
		private IDesignation _designation;
		private IUnion _union;
		private IMode _mode;
		private IPublicTransportGroup _publicTransportGroup;

		public PublicTransportGroupController(IPublicTransportGroup publicTransportGroup, ISecurity security, IGeography geography, IDesignation designation, IUnion union, IMode mode)
		{
			_publicTransportGroup = publicTransportGroup;
			_security = security;
			_geography = geography;
			_designation = designation;
			_union = union;
			_mode = mode;
		}

		public IActionResult Index()
		{
			ViewBag.EmployeeName = (string)HttpContext.Session.GetString("FullName");
			ViewBag.SecurityGroupId = (int)HttpContext.Session.GetInt32("SecurityGroupId");

			return View();
		}

		public IActionResult Add()
		{
			try
			{
				if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SecurityUserId")))
				{
					var model = new PublicTransportGroupModel
					{
						PublicTransportGroupId = 0,
						PublicTransportGroupIdModifiedDate = DateTime.Now
					};

					int? securityGroupId = HttpContext.Session.GetInt32("SecurityGroupId");
					int? stateId = HttpContext.Session.GetInt32("StateId");
					int supervisorsGroupId = 3;

					if (securityGroupId.HasValue)
					{
						if (securityGroupId != 1)
						{
							if (_security.GroupHasAccessToModule((int)securityGroupId, 6))
							{
								ViewBag.States = new SelectList(_geography.GetAllStates(), "StateId", "StateName");
							}
							else
							{
								if (stateId.HasValue)
								{
									ViewBag.States = new SelectList(_geography.GetStateById((int)stateId), "StateId", "StateName");
								}
							}
						}
						else
						{
							ViewBag.States = new SelectList(_geography.GetAllStates(), "StateId", "StateName");
						}
					}

					ViewBag.Designation = new SelectList(_designation.GetAll(), "DesignationId", "DesignationName");
					ViewBag.Mode = new SelectList(_mode.GetAll(), "ModeId", "ModeName");
					ViewBag.Union = new SelectList(_union.GetAll(), "UnionId", "UnionName");

					return View(model);
				}

				return RedirectToAction("Login", "Security");
			}

			catch (Exception ex)
			{
				return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
			}
		}

		[HttpPost]
		public IActionResult Add(PublicTransportGroupModel model)
		{
			try
			{
				if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SecurityUserId")) && ModelState.IsValid)
				{
					int publicTransportGroupId = _publicTransportGroup.AddOrEdit(model);

					if (publicTransportGroupId > 0)
					{
						return RedirectToAction("Edit", new { publicTransportGroupId = publicTransportGroupId });
					}
				}

				return RedirectToAction("Login", "Security");
			}
			catch (Exception ex)
			{
				return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
			}
		}

		[HttpGet]
		public IActionResult Edit(int publicTransportGroupId)
		{
			if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SecurityUserId")))
			{
				var model = _publicTransportGroup.GetPublicTransportGroupById(publicTransportGroupId);
				int? securityGroupId = HttpContext.Session.GetInt32("SecurityGroupId");
				int? stateId = HttpContext.Session.GetInt32("StateId");
				int supervisorsGroupId = 3;

				if (securityGroupId.HasValue)
				{
					if (securityGroupId != 1)
					{
						if (_security.GroupHasAccessToModule((int)securityGroupId, 6))
						{
							ViewBag.States = new SelectList(_geography.GetAllStates(), "StateId", "StateName");
						}
						else
						{
							if (stateId.HasValue)
							{
								ViewBag.States = new SelectList(_geography.GetStateById((int)stateId), "StateId", "StateName");
							}
						}

						if (_security.GroupHasAccessToModule((int)securityGroupId, 7))
						{
							ViewBag.Groups = new SelectList(_security.GetAllGroups(), "SecurityGroupId", "SecurityGroupName");
						}
						else
						{
							//Coordinadores group
							if (securityGroupId == 2)
							{
								ViewBag.Groups = new SelectList(_security.GetGroupById((int)supervisorsGroupId), "SecurityGroupId", "SecurityGroupName");
							}
							else
							{
								ViewBag.Groups = new SelectList(_security.GetGroupById((int)securityGroupId), "SecurityGroupId", "SecurityGroupName");
							}
						}
					}
					else
					{
						ViewBag.States = new SelectList(_geography.GetAllStates(), "StateId", "StateName");
					}

					ViewBag.Designation = new SelectList(_designation.GetAll(), "DesignationId", "DesignationName");
					ViewBag.Mode = new SelectList(_mode.GetAll(), "ModeId", "ModeName");
					ViewBag.Union = new SelectList(_union.GetAll(), "UnionId", "UnionName");
					ViewBag.Municipality = new SelectList(_geography.GetAllMunicipalities(), "MunicipalityId", "MunicipalityName");
				}

				return View(model);
			}

			return RedirectToAction("Login", "Security");
		}

		public IActionResult Edit(PublicTransportGroupModel model)
		{
			try
			{
				if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SecurityUserId")) && ModelState.IsValid)
				{
					_publicTransportGroup.AddOrEdit(model);

					return RedirectToAction("Edit", new { publicTransportGroupId = model.PublicTransportGroupId });
				}

				return RedirectToAction("Login", "Security");
			}
			catch (Exception ex)
			{
				return RedirectToAction("Error", "Home", new { errorMessage = ex.Message.ToString() });
			}
		}


		[HttpGet]
		public JsonResult GetMunicipality(int stateId)
		{
			var municipality = _geography.GetMunicipalityByStateId(stateId).ToList();

			return Json(municipality);
		}

		public JsonResult CheckRifExist(string paramValue1)
		{
			if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SecurityUserId")))
			{
				var registeredRif = _publicTransportGroup.RegisteredRif(paramValue1);

				if (!string.IsNullOrEmpty(registeredRif))
				{

					return Json($"La línea con el RIF {paramValue1} ya está registrada.");					
				}

				return Json("OK");
			}

			return Json("ERROR");
		}

		public JsonResult CheckRifExistOnEdit(string paramValue1, int paramValue2)
		{
			if (!string.IsNullOrEmpty(HttpContext.Session.GetString("SecurityUserId")))
			{
				var registeredRif = _publicTransportGroup.RegisteredRif(paramValue1);

				if(!string.IsNullOrEmpty(registeredRif))
				{
					var currentRif = _publicTransportGroup.GetPublicTransportGroupById(paramValue2);

					if(currentRif.PublicTransportGroupRif != registeredRif)
					{
						return Json($"La línea con el RIF {paramValue1} ya está registrada.");
					}
				}

				return Json("OK");
			}

			return Json("ERROR");
		}
	}
}
