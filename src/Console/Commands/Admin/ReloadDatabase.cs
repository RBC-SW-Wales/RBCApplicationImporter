
using System;
using System.Threading;
using RbcConsole.Helpers;
using RbcTools.Library.Database;

namespace RbcConsole.Commands.Admin
{
	public class ReloadDatabase : CommandBase
	{
		public ReloadDatabase()
		{
			base.Slug = "reload-database";
			base.Description = "Download the Access Database File";
			base.IsAdminCommand = true;
		}
		
		public override void Run()
		{
			if(AccessFileDownloader.AccessFileExists)
			{
				ConsoleX.WriteLine("This command will DELETE your existing database file first.", ConsoleColor.Yellow, false);
				ConsoleX.WriteLine("Any unsychronised records will be lost.", ConsoleColor.Yellow);
				
				if(ConsoleX.WriteBooleanQuery("Are you sure you want to continue?"))
				{
					ConsoleX.WriteLine("Okay. First we need to DELETE the existing file...", false);
					Thread.Sleep((int)TimeSpan.FromSeconds(3).TotalMilliseconds);
					AccessFileDownloader.DeleteExistingFile();
					ConsoleX.WriteLine("File Deleted!");
					
					ConsoleX.WriteLine("Now we need to download a fresh copy.", false);
					ConsoleX.WriteLine("I can download it for you and save it to the correct place.");
					AccessFileHelper.DownloadAccessFile(ConsoleX);
				}
			}
			else
			{
				ConsoleX.WriteWarning("It looks like you don't have the Access database file yet.");
				ConsoleX.WriteLine("Don't worry, I can download it for you and save it to the correct place.");
				AccessFileHelper.DownloadAccessFile(ConsoleX);
			}
		}
	}
}
