using IVAX.INTERCONEXIONBANCARIA.COMPLETA.Conexiones;
using IVAX.INTERCONEXIONBANCARIA.COMPLETA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace IVAX.INTERCONEXIONBANCARIA.COMPLETA.Persistencias
{
    public class PaymentSQL
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string PagoDocumentos(RequestPayment requestPayment, string documentDetails)
        {
            string json = string.Empty;
            try
            {
                var jsonRequestPayment = new JavaScriptSerializer().Serialize(requestPayment);
                var conn = new Conexioncs().QueryString;
                using (var cn = new SqlConnection(conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand("GET_PAYMENT", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@rqUUID", SqlDbType.VarChar).Value = requestPayment.rqUUID;
                        cmd.Parameters.Add("@operationDate", SqlDbType.VarChar).Value = requestPayment.operationDate;
                        cmd.Parameters.Add("@operationNumber", SqlDbType.VarChar).Value = requestPayment.operationNumber;
                        cmd.Parameters.Add("@financialEntity", SqlDbType.VarChar).Value = requestPayment.financialEntity;
                        cmd.Parameters.Add("@channel", SqlDbType.VarChar).Value = requestPayment.channel;
                        cmd.Parameters.Add("@serviceId", SqlDbType.VarChar).Value = requestPayment.serviceId;
                        cmd.Parameters.Add("@customerId", SqlDbType.VarChar).Value = requestPayment.customerId;
                        cmd.Parameters.Add("@paymentType", SqlDbType.VarChar).Value = requestPayment.paymentType;
                        cmd.Parameters.Add("@amountTotal", SqlDbType.VarChar).Value = requestPayment.amountTotal;
                        if (requestPayment.check != null)
                        {
                            if(requestPayment.check.checkNumber!=null && requestPayment.check.financialEntity != null)
                            {
                                cmd.Parameters.Add("@checkNumber", SqlDbType.VarChar).Value = requestPayment.check.checkNumber;
                                cmd.Parameters.Add("@checkfinancialEntity", SqlDbType.VarChar).Value = requestPayment.check.financialEntity;
                            }

                        }
                        cmd.Parameters.Add("@documentDetail", SqlDbType.VarChar).Value = documentDetails;
                        cmd.Parameters.Add("@request", SqlDbType.VarChar).Value = jsonRequestPayment;
                        SqlDataReader rd = cmd.ExecuteReader();
                        while (rd.Read())
                        {
                            json = rd[0].ToString();
                            json = json.Substring(1, json.Length - 2);
                        }
                    }
                }
                return json;
            }
            catch (Exception ex)
            {
                var dateFormat = "yyyy-MM-ddTHH:mm:ss";
                string dateOperation = DateTimeOffset.UtcNow.ToString(dateFormat);
                var respuestaError = new ErrorResponse();
                respuestaError.rqUUID = requestPayment.rqUUID;
                respuestaError.resultCode = "CP0138";
                respuestaError.resultDescription = "ERROR AL PROCESAR TRANSACCION";
                respuestaError.resultCodeCompany = "ERROR-21DB";
                respuestaError.resultDescriptionCompany = ex.Message.ToString().ToUpper();
                respuestaError.operationDate = dateOperation;
                json = new JavaScriptSerializer().Serialize(respuestaError);
                log.Info("Error al ejecutar el llamado del metodo Inquire: " + json);
                return json;
            }
        }
    }
}