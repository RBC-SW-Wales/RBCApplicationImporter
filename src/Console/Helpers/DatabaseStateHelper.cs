
using System;
using RbcTools.Library.Database;

namespace RbcConsole.Helpers
{
	public static class DatabaseStateHelper
	{
		public static void TestDatabaseState(ConsoleX consoleX, bool showTestFeedback = false)
		{
			if(showTestFeedback)
				consoleX.WriteLine("Ok, let's check the database for synchronisation issues...");
			
			if(!DatabaseState.IsDataSynchronised())
			{
				
				consoleX.WriteWarning("Warning: Database records have not been synchronised!", false);
				consoleX.WriteWarning("Attempting to synchronise now...");
				if(DatabaseState.TrySync())
				{
					consoleX.WriteWarning("Success!", true);
				}
				else
				{
					consoleX.WriteWarning("Failure! Could not synchronise at this time.", false);
				}
				consoleX.WriteWarning("If this message persists, please contact IT Support.");
			}
			else if(showTestFeedback)
			{
				consoleX.WriteLine("Great, no synchronisation issues found!");
			}
		}
	}
}
