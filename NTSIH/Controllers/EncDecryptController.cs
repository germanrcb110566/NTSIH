using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;
using NTSIH.Models;

namespace NTSIH.Controllers
{
    public class EncDecryptController : Controller
    {
        
        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public static string ObtenerRol(ref string Mensaje, string Persona_Id, int Registro_id)
        {           
            int Rol_Id = 0;
            string Rol = null;
            string SentenciaSQL = "select nombre from catalogo where  registro_id in (";
            SentenciaSQL += "select max(rol_id) from rrol_Persona where persona_id in ";
            SentenciaSQL += "(select registro_id from mPersona where identificacion='" + Persona_Id + "')";
            SentenciaSQL += ") order by descripcion desc";
            string SentenciaSQL2 = "select registro_id from catalogo where  registro_id in (";
            SentenciaSQL2 += "select  rol_id  from rrol_Persona where persona_id in ";
            SentenciaSQL2 += "(select registro_id from mPersona where identificacion='" + Persona_Id + "')";
            SentenciaSQL2 += ") order by descripcion desc";
            try
            {
                using (var conexion = new SIHEntities())
                {
                     Rol = conexion.Database.SqlQuery<string>(SentenciaSQL).FirstOrDefault();
                    if (Rol != null)
                    {
                        Mensaje = "0000SENTENCIA EJECUTADA CORRECTAMENTE";
                        Rol_Id = Convert.ToInt32(conexion.Database.SqlQuery<int>(SentenciaSQL2).FirstOrDefault());
                    }
                    else
                    {
                        Mensaje = "0099NO TIENE ASIGNADO UN ROL LA PERSONA";
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje = "0020" + ex.Message;
            }
            string Log = InsertarLog(Registro_id, Convert.ToInt32(Rol_Id), 1, SentenciaSQL);
            return Rol;
        }

        public static string ObtenerRol(ref string Mensaje, string registro_id)
        {
            string Rol = null;
            string SentenciaSQL = "select nombre from catalogo where  registro_id in (";
            SentenciaSQL += "select rol_id from rrol_Persona where persona_id in ";
            SentenciaSQL += "(select registro_id from mPersona where identificacion='" + registro_id + "')";
            SentenciaSQL += ") order by descripcion asc";

            try
            {
                using (var conexion = new SIHEntities())
                {
                    // var studentName = ctx.Students.SqlQuery("Select * from Courses").ToList();
                    // string studentName = ctx.Database.SqlQuery<string>("Select studentname from Student where studentid=1").FirstOrDefault();

                    Rol = conexion.Database.SqlQuery<string>(SentenciaSQL).FirstOrDefault();
                    if (Rol != null)
                    {
                        Mensaje = "0000SENTENCIA EJECUTADA CORRECTAMENTE";
                    }
                    else
                    {
                        Mensaje = "0099NO TIENE ASIGNADO UN ROL LA PERSONA";
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje = "0020Error no controlado.  Informacion adicional:".ToUpper() + ex.Message;
            }
            return Rol;
        }

        public static string InsertarLog(int Usuario_Id,
                                  int Rol_Id,
                                  int Modulo_Id,
                                  string sentenciasql)
        {
            if (sentenciasql == null)
            {
                sentenciasql = "NO SE RECIBIO SENTENCIA ";
            }
            string retorno = "";
            string cadena = "'" + DateTime.Now + "',";
            cadena += Usuario_Id + ",";
            cadena += Rol_Id + ",";
            cadena += Modulo_Id + ",";
            cadena += "'" + sentenciasql.Replace("'", "#") + "'";
            //cadena += "'" + sentenciasql.Replace("'", "#") + "'";
            try
            {
                using (var conexion = new SIHEntities())
                {
                    string sentenciaSQL = "INSERT INTO mAuditoria VALUES (" + cadena + ")";
                    int st = conexion.Database.ExecuteSqlCommand(sentenciaSQL);
                    if (st == 1)
                    {
                        retorno = "0000REGISTRO CREADO EXITOSAMENTE";
                    }
                }
            }
            catch (Exception ex)
            {
                retorno = "0010" + ex.Message;
            }
            return retorno;
        }

        public static mPersona unRegistro(ref string Mensaje, string Identificacion)
        {
            var registro = new mPersona();
            try
            {
                using (var conexion = new SIHEntities())
                {
                    registro = conexion.mPersona.Where(a => a.identificacion == Identificacion).Single();
                    Mensaje = "0000SENTENCIA EJECUTADA CORRECTAMENTE";
                }
            }
            catch (Exception ex)
            {
                Mensaje = "0010PERSONA NO ESTA REGISTRADA EN EL SISTEMA.  Información adicional:".ToUpper() + ex.Message;

            }
            return registro;
        }
        public static mPersona unRegistroId(ref string Mensaje, int? Identificacion)
        {
            var registro = new mPersona();
            try
            {
                using (var conexion = new SIHEntities())
                {
                    registro = conexion.mPersona.Where(a => a.registro_id == Identificacion).Single();
                    Mensaje = "0000SENTENCIA EJECUTADA CORRECTAMENTE";
                }
            }
            catch (Exception ex)
            {
                Mensaje = "0010PERSONA NO ESTA REGISTRADA EN EL SISTEMA.  Información adicional:".ToUpper() + ex.Message;

            }
            return registro;
        }

    }
    public class MiExcepciones : Exception
    {
        public MiExcepciones(String mensaje) : base(mensaje)
        {

        }
    }
}