using BusinessObjects.EmailSendModule;
using DataAccessLayer.EmailSendModule;
using DataAccessLayer.Interfaces.EmailSendModule;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmailSenderProgram.ApplicationService.EmailSendModule
{
	public class EmailSendApplicationServices
	{
		/// <summary>
		/// Send Welcome mail
		/// </summary>
		/// <returns></returns>
		public bool DoEmailWork()
		{
			try
			{
				IEmailSendDataService emailSendDataService = new EmailSendDataService();

				//List all customers
				List<Customer> customerDataList = emailSendDataService.ListCustomers();

				if (customerDataList != null && customerDataList.Count() > 0)
				{
					//loop through list of new customers
					for (int i = 0; i < customerDataList.Count; i++)
					{
						//If the customer is newly registered, one day back in time
						if (customerDataList[i].CreatedDateTime > DateTime.Now.AddDays(-1))
						{
							//Create a new MailMessage
							System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
							//Add customer to reciever list
							mail.To.Add(customerDataList[i].Email);
							//Add subject
							mail.Subject = "Welcome as a new customer at EYEPAX!";
							//Send mail from info@eyepax.com
							mail.From = new System.Net.Mail.MailAddress("info@eyepax.com");
							//Add body to mail
							mail.Body = "Hi " + customerDataList[i].Email +
									 "<br>We would like to welcome you as customer on our site!<br><br>Best Regards,<br>EYEPAX Team";
#if DEBUG
							//Don't send mails in debug mode, just write the emails in console
							Console.WriteLine("Send mail to:" + customerDataList[i].Email);
#else
	                //Create a SmtpClient to our smtphost: yoursmtphost
					System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("yoursmtphost");
					//Send mail
					smtp.Send(m);
#endif
						}
					}
					//All mails are sent! Success!
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				//Something went wrong :(
				throw ex;
			}
		}

		/// <summary>
		/// Send Customer ComebackMail
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public  bool DoEmailWork2(string message)
		{
			try
			{
				IEmailSendDataService emailSendDataService = new EmailSendDataService();

				//List all customers 
				List<Customer> customerDataList = emailSendDataService.ListCustomers();
				//List all orders
				List<Order> orderList = emailSendDataService.ListOrders();

				if (customerDataList != null && customerDataList.Count() > 0)
				{
					//loop through list of customers
					foreach (Customer customer in customerDataList)
					{
						// We send mail if customer hasn't put an order
						bool Send = true;

						if (orderList != null && orderList.Count() > 0) {
							List<Order> orderEmail = orderList.Where(x => x.CustomerEmail == customer.Email).ToList();

							// Email exists in order list
							if (orderEmail.Count() > 0)
							{
								//We don't send email to that customer
								Send = false;
							}
						}
						
						//Send if customer hasn't put order
						if (Send == true)
						{
							//Create a new MailMessage
							System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
							//Add customer to reciever list
							mail.To.Add(customer.Email);
							//Add subject
							mail.Subject = "We miss you as a customer";
							//Send mail from info@eyepax.com
							mail.From = new System.Net.Mail.MailAddress("infor@eyepax.com");
							//Add body to mail
							mail.Body = "Hi " + customer.Email +
									 "<br>We miss you as a customer. Our shop is filled with nice products. Here is a voucher that gives you 50 kr to shop for." +
									 "<br>Voucher: " + message +
									 "<br><br>Best Regards,<br>EYEPAX Team";
#if DEBUG
							//Don't send mails in debug mode, just write the emails in console
							Console.WriteLine("Send mail to:" + customer.Email);
#else
	                //Create a SmtpClient to our smtphost: yoursmtphost
					System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("yoursmtphost");
					//Send mail
					smtp.Send(m);
#endif
						}
					}
					//All mails are sent! Success!
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				//Something went wrong :(
				throw ex;
			}
		}
	}

}
