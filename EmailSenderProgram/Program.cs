using EmailSenderProgram.ApplicationService.EmailSendModule;
using EmailSenderProgram.LogServices;
using System;
using static EmailSenderProgram.LogServices.LogService;

namespace EmailSenderProgram
{
	 
	internal class Program 
	{
		

		/// <summary>
		/// This application is run everyday
		/// </summary>
		/// <param name="args"></param>
		/// 

		private static void Main(string[] args  )
		{
			LogService logService = new LogService();

			try
			{
				logService.WriteLogMessage("Process Start", "Admin", "session", LoggerLevel.Error);

				EmailSendApplicationServices emailSendApplicationServices = new EmailSendApplicationServices();

				//Call the method that do the work for me, I.E. sending the mails
				Console.WriteLine("Send Welcomemail");

				bool success = emailSendApplicationServices.DoEmailWork();

		#if DEBUG
				//Debug mode, always send Comeback mail
				Console.WriteLine("Send Comebackmail");
				success = emailSendApplicationServices.DoEmailWork2("EYEPAXComebackToUs");
		#else
				//Every Sunday run Comeback mail
				if (DateTime.Now.DayOfWeek.Equals(DayOfWeek.Monday))
				{
						Console.WriteLine("Send Comebackmail");
						success = emailSendApplicationServices.DoEmailWork2("EYEPAXComebackToUs");
				}
		#endif

				//Check if the sending went OK
				if (success == true)
				{
				     Console.WriteLine("All mails are sent, I hope...");
				}
				//Check if the sending was not going well...
				if (success == false)
				{
					Console.WriteLine("Oops, something went wrong when sending mail (I think...)");
				}
				Console.ReadKey();


				logService.WriteLogMessage("Process End", "Admin", "session", LoggerLevel.Error);

			}
			catch (Exception ex)
			{
				logService.WriteLogMessage(ex.Message.ToString(), "Admin", "session", LoggerLevel.Error);
			}

		}
	}
}