﻿
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using RbcVolunteerApplications.Importer.Commands;
using RbcVolunteerApplications.Library;
using RbcVolunteerApplications.Library.Database;

namespace RbcVolunteerApplications.Importer
{
	class Program
	{
		public static List<CommandBase> CommandList = BuildCommandList();
		
		private static List<CommandBase> BuildCommandList()
		{
			var list = new List<CommandBase>();
			Program.CommandList.Add(new ImportFiles());
			Program.CommandList.Add(new CongregationLookup());
			Program.CommandList.Add(new QueryVolunteers());
			Program.CommandList.Add(new HelpCommand());
			return list;
		}
		
		[STAThread]
		public static void Main(string[] args)
		{
			// Get size right
			Console.SetWindowSize(Console.LargestWindowWidth - 2, Console.LargestWindowHeight);
			Console.BufferWidth = Console.WindowWidth;
			
			// Display application title
			ConsoleX.WriteTitle("RBC Application Form (S82) Importer");
			
			// Enter into loop that takes commands.
			var input = "";
			while(input != "exit")
			{
				ConsoleX.WriteLine("Enter command:");
				input = ConsoleX.ReadPromt();
				if(input != "exit")
				{
					var commandFound = false;
					foreach(var command in Program.CommandList)
					{
						if(command.Slug == input)
						{
							commandFound = true;
							command.Run();
						}
					}
					if(!commandFound)
						new HelpCommand().Run();
				}
			}
			
			ConsoleX.WriteLine("Good bye!");
			Thread.Sleep(1000);
		}
		
	}
}