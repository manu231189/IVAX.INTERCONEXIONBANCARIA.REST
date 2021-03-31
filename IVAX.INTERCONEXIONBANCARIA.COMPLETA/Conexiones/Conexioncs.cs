using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IVAX.INTERCONEXIONBANCARIA.COMPLETA.Conexiones
{
    public class Conexioncs
    {
        public string QueryString { get; set; }
        public Conexioncs()
        {
            QueryString = System.Configuration.ConfigurationManager.ConnectionStrings["DbPaymentBcp"].ConnectionString;
        }
    }
}