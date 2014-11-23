using System;

namespace RbcConsole.Commands.Admin
{
	public class EnableAdminMode : CommandBase
	{
		public EnableAdminMode()
		{
			base.Slug = "-admin-mode";
			base.Description = "Switch Administrator Mode.";
		}
		
		public override void Run()
		{
			if(Program.UseAdminMode == false)
			{
				Program.UseAdminMode = true;
				ConsoleX.WriteLine("Administrator Mode enabled!", ConsoleColor.Yellow, blankAfter:false);
				ConsoleX.WriteLine("Use 'help' to see special commands highlights.", ConsoleColor.Yellow, true);
			}
			else
			{
				Program.UseAdminMode = false;
				ConsoleX.WriteLine("Administrator Mode disabled!", ConsoleColor.Yellow, true);
			}
		}
		
	}
}
