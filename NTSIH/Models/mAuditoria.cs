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

        



    }
}
