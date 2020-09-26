using IVAX.INTERCONEXIONBANCARIA.COMPLETA.Models;
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
        public HttpResponseMessage Post(RequestAnnulment requestAnnulment, HttpRequestMessage requestMessage)
        {
            HttpResponseMessage response = null;

            if (requestAnnulment is null)
            {
                var respuestaError = new ErrorRequestGeneric();
                respuestaError.message = "El request enviado es incorrecto";
                var jsonError = new JavaScriptSerializer().Serialize(respuestaError);
                response = Request.CreateResponse(HttpStatusCode.BadRequest);
                response.Content = new StringContent(jsonError, Encoding.UTF8, "application/json");

                return response;
            }
                var respuestaModelo = new RequestAnnulment();
                var json = new JavaScriptSerializer().Serialize(respuestaModelo);
                response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(json, Encoding.UTF8, "application/json");

                return response;
        }
    }
}