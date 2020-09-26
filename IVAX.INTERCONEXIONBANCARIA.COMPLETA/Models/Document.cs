using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IVAX.INTERCONEXIONBANCARIA.COMPLETA.Models
{
    public class Document
    {
        public string documentId { get; set; }
        public string expirationDate { get; set; }
        public string documentReference { get; set; }
        public string paymentDetail { get; set; }
        public List<Amount> amounts { get; set; }
    }
}