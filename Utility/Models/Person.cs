using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Models
{
    public class Person : BaseModel
    {
        public string MailTo { get; set; }

        public string SubjectsOfInterest { get; set; }

        public string MailContent { get; set; }

        public DateTime EntryDate { get; set; }

        public DateTime? DeletionDate { get; set;}
    }
}
