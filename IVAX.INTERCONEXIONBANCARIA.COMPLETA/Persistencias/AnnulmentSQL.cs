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
    public class AnnulmentSQL
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string Extorno(RequestAnnulment requestAnnulment)
        {
            string json=string.Empty;
            try
            {
                var jsonRequestAnnulment = new JavaScriptSerializer().Serialize(requestAnnulment);
                var conn = new Conexioncs().QueryString;
                using(var cn = new SqlConnection(conn))
                {
                    cn.Open();
                    using (var cmd = new SqlCommand("GET_ANNULMENT", cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@rqUUID", SqlDbType.VarChar).Value = requestAnnulment.rqUUID;
                        cmd.Parameters.Add("@operationDate", SqlDbType.VarChar).Value = requestAnnulment.operationDate;
                        cmd.Parameters.Add("@operationNumber", SqlDbType.VarChar).Value = requestAnnulment.operationNumber;
                        cmd.Parameters.Add("@operationNumberAnnulment", SqlDbType.VarChar).Value = requestAnnulment.operationNumberAnnulment;
                        cmd.Parameters.Add("@financialEntity", SqlDbType.VarChar).Value = requestAnnulment.financialEntity;
                        cmd.Parameters.Add("@customerId", SqlDbType.VarChar).Value = requestAnnulment.customerId;
                        cmd.Parameters.Add("@channel", SqlDbType.VarChar).Value = requestAnnulment.channel;
                        cmd.Parameters.Add("@request", SqlDbType.VarChar).Value = jsonRequestAnnulment;
                        SqlDataReader rd = cmd.ExecuteReader();
                        while (rd.Read())
                        {
                            json = rd[0].ToString();
                            json = json.Substring(1, json.Length - 2);
                        }
                    }
                }
                return json;
            }catch(Exception ex)
            {
                var dateFormat = "yyyy-MM-ddTHH:mm:ss";
                string dateOperation = DateTimeOffset.UtcNow.ToString(dateFormat);
                var respuestaError = new ErrorResponse();
                respuestaError.rqUUID = requestAnnulment.rqUUID;
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