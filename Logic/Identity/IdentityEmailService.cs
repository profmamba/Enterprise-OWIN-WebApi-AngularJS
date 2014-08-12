using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Acutela.Frasset.Logic.Identity
{
	public class IdentityEmailService : IIdentityMessageService
	{
		public Task SendAsync(IdentityMessage message)
		{
			MailMessage mail = new MailMessage();
			MailAddress address = new MailAddress(message.Destination);

			mail.IsBodyHtml = true;
			mail.Body = message.Body;
			mail.Subject = message.Subject;
			mail.To.Add(address);

			SmtpClient smtp = new SmtpClient();

			return smtp.SendMailAsync(mail);
		}
	}
}
