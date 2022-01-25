//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NTSIH.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class mAuditoria
    {
        public int registro_id { get; set; }
        public System.DateTime fecha { get; set; }
        public int usuario_id { get; set; }
        public int rol_id { get; set; }
        public int modulo_id { get; set; }
        public string sentencia { get; set; }
    
        public virtual mPersona mPersona { get; set; }



        public string InsertarLog(string accion,
                                  string sentenciasql,
                                  int persona_id)
        {
            string retorno = "";
            string cadena = "'" + DateTime.Now + "',";
            cadena += "'" + accion.ToUpper() + "',";
            cadena += persona_id + ",";
            cadena += "'" + sentenciasql.ToUpper() + "'";

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
    }
}
