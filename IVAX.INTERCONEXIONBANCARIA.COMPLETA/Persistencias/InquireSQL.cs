using IVAX.INTERCONEXIONBANCARIA.COMPLETA.Conexiones;
using IVAX.INTERCONEXIONBANCARIA.COMPLETA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace IVAX.INTERCONEXIONBANCARIA.COMPLETA.Persistencias
{
    public class InquireSQL
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string ConsultaDeuda(RequestInquire requestInquire)
        {
            string json = string.Empty;
            try
            {
                var jsonRequestInquire = new JavaScriptSerializer().Serialize(requestInquire);
                var conn = new Conexioncs().QueryString;
                using (var cn = new SqlConnection(conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand("GET_INQUIRE", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@rqUUID", SqlDbType.VarChar).Value = requestInquire.rqUUID;
                        cmd.Parameters.Add("@operationDate", SqlDbType.VarChar).Value = requestInquire.operationDate;
                        cmd.Parameters.Add("@operationNumber", SqlDbType.VarChar).Value = requestInquire.operationNumber;
                        cmd.Parameters.Add("@financialEntity", SqlDbType.VarChar).Value = requestInquire.financialEntity;
                        cmd.Parameters.Add("@channel", SqlDbType.VarChar).Value = requestInquire.channel;
                        cmd.Parameters.Add("@serviceId", SqlDbType.VarChar).Value = requestInquire.serviceId;
                        cmd.Parameters.Add("@customerId", SqlDbType.VarChar).Value = requestInquire.customerId;
                        cmd.Parameters.Add("@request", SqlDbType.VarChar).Value = jsonRequestInquire;
                        SqlDataReader rd = cmd.ExecuteReader();
                        while (rd.Read())
                        {
                            json = rd[0].ToString();
                            json = json.Substring(1, json.Length-2);
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
                respuestaError.rqUUID = requestInquire.rqUUID;
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