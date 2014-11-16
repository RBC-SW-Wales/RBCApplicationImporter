
using System;
using System.Data;

using RbcTools.Library.Database;


namespace RbcConsole.Commands.Admin
{
	public class ListUnsynchronisedData : CommandBase
	{
		public ListUnsynchronisedData()
		{
			base.Slug = "unsynchronised-data";
			base.Description = "List the unsynchronised Volunteer data";
			base.IsDatabaseCommand = true;
			base.SkipTestDatabaseState = true;
			base.IsAdminCommand = true;
		}
		
		public override void Run()
		{
			var activty = DatabaseState.GetUnsynchronisedActivity();
			
			if(activty.Rows.Count == 0)
			{
				ConsoleX.WriteLine("No unsynchronised activity found!", ConsoleColor.Green);
			}
			else
			{
				ConsoleX.WriteLine("Here's the unsynchronised activity log:");
				
				ConsoleX.WriteDataTable(activty, 100, false, true);
				
				if(activty.Rows.Count >= 100)
					ConsoleX.WriteWarning("There are at least 100 unsynchronised activity log entries. There could be more!", false);
			}
			
			var volunteers = DatabaseState.GetUnsynchronisedVolunteers();
			
			if(volunteers.Rows.Count == 0)
			{
				ConsoleX.WriteLine("No unsynchronised NEW Volunteers found!", ConsoleColor.Green);
			}
			else
			{
				ConsoleX.WriteLine("Here's the NEW Volunteers that have not been synchronised:");
				
				ConsoleX.WriteDataTable(volunteers);
				
				if(volunteers.Rows.Count >= 100)
					ConsoleX.WriteWarning("There are at least 100 unsynchronised new Volunteers. There could be more!", false);
			}
		}
	}
}
