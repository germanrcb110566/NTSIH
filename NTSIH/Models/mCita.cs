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
    
    public partial class mCita
    {
        public int registro_id { get; set; }
        public int paciente_id { get; set; }
        public int medico_id { get; set; }
        public int especialidad_id { get; set; }
        public System.DateTime fecha { get; set; }
        public System.DateTime hora { get; set; }
        public string motivo { get; set; }
        public bool estado { get; set; }
    
        public virtual Catalogo Catalogo { get; set; }
        public virtual mAtencion mAtencion { get; set; }
        public virtual mPersona mPersona { get; set; }
        public virtual mPersona mPersona1 { get; set; }
    }
}
