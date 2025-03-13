﻿using System.ComponentModel.DataAnnotations;

namespace SuperTransp.Models
{
	public class SecurityUserModel
	{
		[Key]
		public int SecurityUserId { get; set; }

		[Required(ErrorMessage = "Cedula es requerida")]
		public int? SecurityUserDocumentIdNumber { get; set; }

		[Required(ErrorMessage = "Login es requerido")]
		public string? Login { get; set; }

		public string? Password { get; set; }
		public string? NewPassword { get; set; }

		[Required(ErrorMessage = "El nombre completo es requerido")]
		public string? FullName { get; set; }

		[Required(ErrorMessage = "El grupo es requerido")]
		public int SecurityGroupId { get; set; }
		public string? SecurityGroupName { get; set; }
		public int SecurityStatusId { get; set; }
		public string? SecurityStatusName { get; set; }
		public int StateId { get; set; }
		public string? StateName { get; set; }
		public int SecurityAccessTypeId { get; set; }
		public string? SecurityAccessTypeName { get; set; }
		public string? SecurityModuleDescription { get; set; }
	}

	public class SecurityGroupModel
	{
		public int SecurityGroupId { get; set; }
		[Required(ErrorMessage = "El nombre del grupo es requerido")]
		public string? SecurityGroupName { get; set; }
		[Required(ErrorMessage = "La descripción del grupo es requerida")]
		public string? SecurityGroupDescription { get; set; }
	}

	public class SecurityModuleModel
	{
		public int SecurityModuleId { get; set; }
		[Required(ErrorMessage = "El nombre del modulo es requerido")]
		public string? SecurityModuleName { get; set; }
		[Required(ErrorMessage = "La descripción del modulo es requerida")]
		public string? SecurityModuleDescription { get; set; }
	}

	public class SecurityStatusUserModel
	{
		public int SecurityStatusId { get; set; }
		public string? SecurityStatusName { get; set; }
	}

	public class SecurityAccessTypeModel
	{
		public int SecurityAccessTypeId { get; set; }
		public string? SecurityAccessTypeName { get; set; }
	}

	public class SecurityGroupModuleModel
	{
		public int SecurityGroupModuleId { get; set; }
		public int SecurityGroupId { get; set; }
		public string? SecurityGroupName { get; set; }
		public int SecurityModuleId { get; set; }
		public string? SecurityModuleName { get; set; }
		public int SecurityAccessTypeId { get; set; }
		public string? SecurityAccessTypeName { get; set; }
	}
}