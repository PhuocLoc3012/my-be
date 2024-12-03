using Application.IServices;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Text;
using System.Threading.Tasks;
using FluentEmail.Core;

namespace Infrastructure.Email
{
    public class EmailSender : IEmailSender
    {
        private readonly IFluentEmail _fluentEmail;
        public EmailSender(IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var email =  _fluentEmail
                .To(toEmail)
                .Subject(subject)
                .Body(body, isHtml: true)
                .SendAsync();
        }
    }
}
