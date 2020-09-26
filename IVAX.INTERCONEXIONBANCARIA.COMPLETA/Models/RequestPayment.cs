using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IVAX.INTERCONEXIONBANCARIA.COMPLETA.Models
{
    public class RequestPayment
    {
        public string rqUUID { get; set; }
        public string operationDate { get; set; }
        public string operationNumber { get; set; }
        public string financialEntity { get; set; }
        public string channel { get; set; }
        public string serviceId { get; set; }
        public string customerId { get; set; }
        public string paymentType { get; set; }
        public string amountTotal { get; set; }
        public List<CheckNumber> check { get; set; }
        public List<DocumentId> documents { get; set; }

    }
}