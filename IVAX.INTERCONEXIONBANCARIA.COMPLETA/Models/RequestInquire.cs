using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IVAX.INTERCONEXIONBANCARIA.COMPLETA.Models
{
    public class RequestInquire
    {
        public string rqUUID { get; set; }
        public string operationDate { get; set; }
        public string operationNumber { get; set; }
        public string financialEntity { get; set; }
        public string channel { get; set; }
        public string serviceId { get; set; }
        public string customerId { get; set; }
    }
}