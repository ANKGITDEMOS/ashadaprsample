using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AshaAppCommon.Models
{
    public record struct VisitSchedule
    {

        public VisitSchedule()
        {

        }

        public VisitSchedule(string citizenid, string ashaworkerid,string id)
        {
            this.citizenid = citizenid;
            this.AshaWorkerId = ashaworkerid;  
            this.Id = id;
        }

        public string Id { get;   set; }

        public string citizenid { get;  set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set;}

        public string AshaWorkerId { get;  set; }

        public string Address { get; set; }
    }



}
