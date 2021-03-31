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
    public class PaymentController : ApiController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public HttpResponseMessage Post(RequestPayment requestPayment, HttpRequestMessage requestMessage)
        {
            HttpResponseMessage response = null;
            try
            {
                var flag = true;
                var documentDetail = "";
                foreach(var documents in requestPayment.documents)
                {
                    if (documents.amounts.Count != 1)
                    {
                        flag = false;
                    }
                    else
                    {
                        documentDetail += documents.documentId + "|";
                        foreach (var montos in documents.amounts)
                        {
                            documentDetail += montos.amountType.Trim() + "|";
                            documentDetail += montos.amount;
                        }
                        documentDetail += "^";
                    }
                }
                if (flag)
                {
                    var respuesta = new PaymentSQL().PagoDocumentos(requestPayment, documentDetail);
                    response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(respuesta, Encoding.UTF8, "application/json");
                    return response;
                }
                else
                {
                    var dateFormat = "yyyy-MM-ddTHH:mm:ss";
                    string dateOperation = DateTimeOffset.UtcNow.ToString(dateFormat);
                    var request = new JavaScriptSerializer().Serialize(requestPayment);
                    var respuestaError = new ErrorResponse();
                    var rqUUID = string.Empty;
                    if (requestPayment != null)
                    {
                        if (requestPayment.rqUUID != null)
                        {
                            rqUUID = requestPayment.rqUUID;
                        }
                    }
                    respuestaError.rqUUID = rqUUID;
                    respuestaError.resultCode = "CP0138";
                    respuestaError.resultDescription = "ERROR AL PROCESAR TRANSACCION";
                    respuestaError.resultCodeCompany = "ERROR-15PROC";
                    respuestaError.resultDescriptionCompany = "ERROR EN EL REQUEST";
                    respuestaError.operationDate = dateOperation;
                    var respuesta = new JavaScriptSerializer().Serialize(respuestaError);
                    log.Info("Error al ejecutar el llamado del metodo Inquire: " + respuesta + "request enviado: " + request + " detalle del error: " + "error en los montos enviados por documentos");
                    response = Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(respuesta, Encoding.UTF8, "application/json");
                    return response;
                }
                
            }
            catch (Exception ex)
            {
                var dateFormat = "yyyy-MM-ddTHH:mm:ss";
                string dateOperation = DateTimeOffset.UtcNow.ToString(dateFormat);
                var request = new JavaScriptSerializer().Serialize(requestPayment);
                var respuestaError = new ErrorResponse();
                var rqUUID = string.Empty;
                if (requestPayment != null)
                {
                    if (requestPayment.rqUUID != null)
                    {
                        rqUUID = requestPayment.rqUUID;
                    }
                }
                respuestaError.rqUUID = rqUUID;
                respuestaError.resultCode = "CP0138";
                respuestaError.resultDescription = "ERROR AL PROCESAR TRANSACCION";
                respuestaError.resultCodeCompany = "ERROR-15PROC";
                respuestaError.resultDescriptionCompany = "ERROR EN EL REQUEST";
                respuestaError.operationDate = dateOperation;
                var respuesta = new JavaScriptSerializer().Serialize(respuestaError);
                log.Info("Error al ejecutar el llamado del metodo Inquire: " + respuesta + "request enviado: " + request + " detalle del error: " + ex.Message.ToString());
                response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(respuesta, Encoding.UTF8, "application/json");
                return response;
            }
        }
    }
}