using System;
using RbcConsole.Helpers;

namespace RbcConsole.Commands
{
	public class CheckDatabase : CommandBase
	{
		public CheckDatabase()
		{
			base.Slug = "check-database";
			base.Description = "Check the database for synchronisation issues.";
		}
		
		public override void Run()
		{
			AccessFileHelper.CheckForAccessFile(this.ConsoleX);
			DatabaseStateHelper.TestDatabaseState(this.ConsoleX, showTestFeedback:true);
		}
	}
}
