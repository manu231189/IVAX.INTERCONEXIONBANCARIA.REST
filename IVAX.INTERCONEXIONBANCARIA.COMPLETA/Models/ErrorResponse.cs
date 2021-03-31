using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IVAX.INTERCONEXIONBANCARIA.COMPLETA.Models
{
    public class ErrorResponse
    {
        public string rqUUID { get; set; }
        public string resultCode { get; set; }
        public string resultDescription { get; set; }
        public string resultCodeCompany { get; set; }
        public string resultDescriptionCompany { get; set; }
        public string operationDate { get; set; }
    }
}