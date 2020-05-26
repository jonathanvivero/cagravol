using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Cagravol.Domain.POCO.ServiceModels.Email
{
    public class EmailMessageServiceModel
    {

        public string TargetUserEmail { get; set; }
        public IList<string> UserEmailList { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
		public string ReplyToEmail { get; set; }
		public string FromEmail { get; set; }
		public string CcoEmail { get; set;}
		public bool IsHtml { get; set; }
		public bool DontReplyThisMessage { get; set; }
		public bool SendCopyToSystem { get; set; }
        public bool SendToSeveral { get; set; }


		public EmailMessageServiceModel() {
            this.UserEmailList = new List<string>();
            this.SendToSeveral = false;
        }

        public EmailMessageServiceModel(
			string targetUserEmail, 
			string subject, 
			string body, 
			bool isHtml = false,			
			string fromEmail = null,
			string replyToEmail = null,
			bool dontReplyThisMessage = false,
			string ccoEmail = null) 
		{
			this.Subject = subject;
			this.Body = body;
			this.TargetUserEmail = targetUserEmail;		
			this.IsHtml = isHtml;
			this.SendCopyToSystem = false;

            this.UserEmailList = new List<string>();
            this.SendToSeveral = false;

			string[] rm;

			if (dontReplyThisMessage)
			{
				if (!string.IsNullOrWhiteSpace(replyToEmail))
				{
					rm = replyToEmail.Split('@');
					this.ReplyToEmail = string.Format("no-reply-this-message@{0}", rm[1]);
				}
			}
			else
			{
				if (!string.IsNullOrWhiteSpace(replyToEmail))
				{
					this.ReplyToEmail = replyToEmail;
				}				
			}

			if (!string.IsNullOrWhiteSpace(fromEmail))
			{
				this.FromEmail = fromEmail;
			}

			if (!string.IsNullOrWhiteSpace(ccoEmail))
			{
				this.CcoEmail = ccoEmail;
			}						
		}

        public EmailMessageServiceModel(
            string[] userEmailList,
            string subject,
            string body,
            bool isHtml = false,
            string fromEmail = null,
            string replyToEmail = null,
            bool dontReplyThisMessage = false,
            string ccoEmail = null)
        {
            this.Subject = subject;
            this.Body = body;
            //this.TargetUserEmail = targetUserEmail;

            this.IsHtml = isHtml;
            this.SendCopyToSystem = false;

            this.UserEmailList = new List<string>(userEmailList);
            this.SendToSeveral = true;

            string[] rm;

            if (dontReplyThisMessage)
            {
                if (!string.IsNullOrWhiteSpace(replyToEmail))
                {
                    rm = replyToEmail.Split('@');
                    this.ReplyToEmail = string.Format("no-reply-this-message@{0}", rm[1]);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(replyToEmail))
                {
                    this.ReplyToEmail = replyToEmail;
                }
            }

            if (!string.IsNullOrWhiteSpace(fromEmail))
            {
                this.FromEmail = fromEmail;
            }

            if (!string.IsNullOrWhiteSpace(ccoEmail))
            {
                this.CcoEmail = ccoEmail;
            }
        }
    }
}
