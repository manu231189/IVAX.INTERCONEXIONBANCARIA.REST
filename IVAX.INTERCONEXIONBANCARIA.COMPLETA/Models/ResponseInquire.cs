using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IVAX.INTERCONEXIONBANCARIA.COMPLETA.Models
{
    public class ResponseInquire
    {
        public string rqUUID { get; set; }
        public string resultCode { get; set; }
        public string resultDescription { get; set; }
        public string operationDate { get; set; }
        public string operationNumberCompany { get; set; }
        public string customerName { get; set; }
        public string merchanId { get; set; }
        public List<Document> documents { get; set; }
    }
}