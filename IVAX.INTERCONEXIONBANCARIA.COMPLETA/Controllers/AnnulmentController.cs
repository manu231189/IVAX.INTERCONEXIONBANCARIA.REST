using IVAX.INTERCONEXIONBANCARIA.COMPLETA.Models;
using IVAX.INTERCONEXIONBANCARIA.COMPLETA.Persistencias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace IVAX.INTERCONEXIONBANCARIA.COMPLETA.Controllers
{
    public class AnnulmentController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public HttpResponseMessage Post(RequestAnnulment requestAnnulment, HttpRequestMessage requestMessage)
        {
            HttpResponseMessage response = null;
            try
            {
                
                var respuesta = new AnnulmentSQL().Extorno(requestAnnulment);
                response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(respuesta, Encoding.UTF8, "application/json");
                return response;
            }catch(Exception ex)
            {
                var dateFormat = "yyyy-MM-ddTHH:mm:ss";
                string dateOperation = DateTimeOffset.UtcNow.ToString(dateFormat);
                var request = new JavaScriptSerializer().Serialize(requestAnnulment);
                var respuestaError = new ErrorResponse();
                var rqUUID = string.Empty;
                if (requestAnnulment != null)
                {
                    if (requestAnnulment.rqUUID != null)
                    {
                        rqUUID = requestAnnulment.rqUUID;
                    }
                }
                respuestaError.rqUUID = rqUUID;
                respuestaError.resultCode = "CP0138";
                respuestaError.resultDescription = "ERROR AL PROCESAR TRANSACCION";
                respuestaError.resultCodeCompany = "ERROR-15PROC";
                respuestaError.resultDescriptionCompany = "ERROR EN EL REQUEST";
                respuestaError.operationDate = dateOperation;
                var respuesta = new JavaScriptSerializer().Serialize(respuestaError);
                log.Info("Error al ejecutar el llamado del metodo Inquire: " + respuesta + "request enviado: "+request + " detalle del error: " + ex.Message.ToString() );
                response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(respuesta, Encoding.UTF8, "application/json");
                return response;
            }
            
        }
    }
}