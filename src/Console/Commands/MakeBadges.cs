using System;
using System.Diagnostics;
using RbcTools.Library;
using RbcTools.Library.Badges;
using RbcTools.Library.Database;

namespace RbcConsole.Commands
{
	public class MakeBadges : CommandBase
	{
		public MakeBadges()
		{
			base.Slug = "make-badges";
			base.Description = "Generate a PDF that can be used to print site access badges.";
			base.IsDatabaseCommand = true;
		}
		
		public override void Run()
		{
			var department = SelectDepartment();
			
			if(department != null)
			{
				ConsoleX.WriteLine("Ok. Generating a PDF of badges for that department...");
				
				// Get ALL badges for a deparment.
				var badges = Volunteers.GetBadgesByDepartment(department);
				
				// Use BadgePdfBuilder to create badges PDF
				var builder = new BadgePdfBuilder(badges);
				// Create the file and return filename
				var fileName = builder.CreatePdf();
				
				// Open the file.
				ConsoleX.WriteLine("Opening the file for you.");
				var process = Process.Start(fileName);
				ConsoleX.WriteLine("Done.");
			}
			else
				ConsoleX.WriteLine("Import Files Skipped", ConsoleColor.Red);
		}
		
		private Department SelectDepartment()
		{
			Department department = null;
			ConsoleX.WriteLine("First, please enter the ID of the Department you want the badges for.", false);
			ConsoleX.WriteLine("(If you need to check the Department ID, you can skip this and run the 'list-departments' command.)");
			var requiresInput = true;
			do
			{
				var id = ConsoleX.WriteIntegerQuery("Enter Department ID:", allowSkip: true);
				if(id == int.MinValue)
				{
					requiresInput = false; // Skipped
				}
				else
				{
					department = Departments.GetById(id);
					if(department != null)
					{
						
						ConsoleX.WriteLine("Selected department:" + department.Name);
						if(ConsoleX.WriteBooleanQuery("Is this correct?"))
							requiresInput = false;
						else
							ConsoleX.WriteLine("No, okay. Please try again.");
					}
					else
					{
						ConsoleX.WriteLine("No Department found with that ID. Please try again.", ConsoleColor.Red);
					}
				}
			}
			while(requiresInput);
			return department;
		}
		
	}
}
