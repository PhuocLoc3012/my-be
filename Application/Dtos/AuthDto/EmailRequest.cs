using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.AuthDto
{
    public class EmailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public EmailRequest(string toEmail, string subject, string body)
        {
            ToEmail = toEmail;  
            Subject = subject;
            Body = body;
        }
    }
}
