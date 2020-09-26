using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IVAX.INTERCONEXIONBANCARIA.COMPLETA.Models
{
    public class RequestAnnulment
    {
        public string rqUUID { get; set; }
        public string operationDate { get; set; }
        public string operationNumber { get; set; }
        public string operationNumberAnnulment { get; set; }
        public string financialEntity { get; set; }
        public string customerId { get; set; }
        public string channel { get; set; }
    }
}