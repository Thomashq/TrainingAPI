using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models
{
    public class Mail : BaseModel
    {
        public string MailTo { get; set; }

        public string Subject { get; set; }

        public string MailContent { get; set; }

        public DateTime? SentDate { get; set; }
    }
}
