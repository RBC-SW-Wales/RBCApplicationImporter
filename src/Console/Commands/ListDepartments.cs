using System;
using System.Data;
using RbcConsole.Helpers;
using RbcTools.Library;
using RbcTools.Library.Database;

namespace RbcConsole.Commands
{
	public class ListDepartments : CommandBase
	{
		public ListDepartments()
		{
			base.Slug = "list-departments";
			base.Description = "Show all a table of all Departments (ID and Name)";
			base.IsDatabaseCommand = true;
		}
		
		public override void Run()
		{
			ConsoleX.WriteDataTable(Departments.GetDepartmentsTable(), 30);
		}
	}
}
