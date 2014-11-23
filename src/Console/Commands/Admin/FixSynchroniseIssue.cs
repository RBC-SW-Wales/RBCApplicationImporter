using System;
using RbcConsole.Helpers;

namespace RbcConsole.Commands.Admin
{
	public class FixSynchroniseIssue : CommandBase
	{
		public FixSynchroniseIssue()
		{
			base.Slug = "fix-sync-issue";
			base.Description = "Check the database for synchronisation issues and fix if possible.";
			base.IsAdminCommand = true;
		}
		
		public override void Run()
		{
			AccessFileHelper.CheckForAccessFile(this.ConsoleX);
			DatabaseStateHelper.TestDatabaseState(this.ConsoleX, showTestFeedback:true);
		}
	}
}
