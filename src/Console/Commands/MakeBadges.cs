using System;
using System.Collections.Generic;
using System.Diagnostics;
using RbcTools.Library;
using RbcTools.Library.Badges;
using RbcTools.Library.Database;

namespace RbcConsole.Commands
{
	public class MakeBadges : CommandBase
	{
		#region Constructor
		
		public MakeBadges()
		{
			base.Slug = "make-badges";
			base.Description = "Generate a PDF that can be used to print site access badges.";
			base.IsDatabaseCommand = true;
		}
		
		#endregion
		
		#region Fields
		
		private bool skipCommand = false;
		
		#endregion
		
		#region Methods
		
		public override void Run()
		{
			var option = ConsoleX.WriteMultipleChoiceQuery(
				"What type of badges to you want to create?",
				"RBC Volunteer badges",
				"Local Volunteer badges");
			
			switch(option)
			{
				case 1:
					this.MakeRbcVolunteerBadges();
					break;
				case 2:
					this.MakeLocalVolunteerBadges();
					break;
				case int.MinValue:
					this.skipCommand = true;
					break;
			}
			
			if(this.skipCommand)
				ConsoleX.WriteLine("Make Badges Skipped", ConsoleColor.Red);
		}
		
		private void MakeRbcVolunteerBadges()
		{
			var department = SelectDepartment();
			if(department == null)
			{
				this.skipCommand = true;
			}
			else
			{
				List<Badge> badges = null;
				
				var allVolunteers = ConsoleX.WriteBooleanQuery("Do you want badges for ALL volunteers on this department?");
				
				if(allVolunteers)
				{
					// Get ALL badges for a deparment.
					badges = Volunteers.GetBadgesByDepartment(department);
				}
				else
				{
					ConsoleX.WriteLine("Here's a list of all the volunteers in this department.");
					ConsoleX.WriteDataTable(Volunteers.GetByDepartment(department));
					
					var volunteerIds = ConsoleX.WriteIntegerListQuery("Please enter the ID for each volunteer you want a badge for.");
					badges = Volunteers.GetBadgesByVolunteerIdList(volunteerIds);
				}
				
				if(badges != null && badges.Count > 0)
				{
					MakePdfFile(badges);
				}
				else
				{
					ConsoleX.WriteLine("No volunteers selected or found in chosen department.");
				}
			}
		}
		
		private void MakeLocalVolunteerBadges()
		{
			var badgeCount = ConsoleX.WriteIntegerQuery("How many local volunteer badges do you want?");
			if(badgeCount == int.MinValue)
			{
				this.skipCommand = true;
			}
			else
			{
				List<Badge> badges = new List<Badge>();
				
				for(var i = 0; i < badgeCount; i++)
					badges.Add(new Badge());
				
				this.MakePdfFile(badges, useLocalVolunteerDesign:true);
			}
		}
		
		private void MakePdfFile(List<Badge> badges, bool useLocalVolunteerDesign = false)
		{
			ConsoleX.WriteLine("Ok. Generating a PDF of badges for you...");
			// Use BadgePdfBuilder to create badges PDF
			// Create the file and return filename
			var builder = new BadgePdfBuilder(badges);
			builder.UseLocalVolunteerDesign = useLocalVolunteerDesign;
			var fileName = builder.CreatePdf();
			// Open the file.
			ConsoleX.WriteLine("Opening the file for you.");
			var process = Process.Start(fileName);
			ConsoleX.WriteLine("Done.");
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
		
		#endregion
	}
}
