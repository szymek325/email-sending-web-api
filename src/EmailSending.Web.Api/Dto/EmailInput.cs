using System.Collections.Generic;

namespace EmailSending.Web.Api.Dto
{
    public class EmailInput
    {
        public IList<string> To { get; set; }
        public string From { get; set; }
        public IList<string> CC { get; set; }
        public IList<string> BCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}