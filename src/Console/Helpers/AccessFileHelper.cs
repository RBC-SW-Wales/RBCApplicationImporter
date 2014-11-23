using System;
using RbcTools.Library.Database;

namespace RbcConsole.Helpers
{
	public static class AccessFileHelper
	{
		public static void CheckForAccessFile(ConsoleX consoleX)
		{
			if(!AccessFileDownloader.AccessFileExists)
			{
				consoleX.WriteIntro("Database requirements");
				
				consoleX.WriteWarning("It looks like you don't have the Access database file yet.");
				consoleX.WriteLine("Don't worry, I can download it for you and save it to the correct place.");
				
				var fileDownloaded = AccessFileHelper.DownloadAccessFile(consoleX);
				
				if(fileDownloaded)
					consoleX.WriteLine("You can now continue with your previous task.");
				
				consoleX.WriteHorizontalRule();
			}
		}
		
		public static bool DownloadAccessFile(ConsoleX consoleX)
		{
			bool fileDownloaded = false;
			do
			{
				var url = consoleX.WriteClipboardQuery("CORRECT URL");
				url = url.Trim(); // Remove any whitespace that may have been copied by mistake.
				
				if(!AccessFileDownloader.IsUrlCorrect(url))
				{
					consoleX.WriteLine("Sorry, incorrect URL. Please try again.");
				}
				else
				{
					try
					{
						consoleX.WriteLine("Thanks! Downloading file now. Please wait...");
						AccessFileDownloader.DownloadFile(url);
						consoleX.WriteLine("OK, Done.");
						fileDownloaded = true;
					}
					catch(Exception ex)
					{
						ExceptionHelper.HandleException(ex, consoleX);
					}
				}
			}
			while(!fileDownloaded);
			
			return fileDownloaded;
		}
		
	}
}
