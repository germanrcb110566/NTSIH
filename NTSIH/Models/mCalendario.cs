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
    
    public partial class mCalendario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mCalendario()
        {
            this.rMedico_Calendario = new HashSet<rMedico_Calendario>();
        }
    
        public int registro_id { get; set; }
        public string nombre { get; set; }
        public bool entre_semana { get; set; }
        public bool fin_de_semana { get; set; }
        public System.TimeSpan horario_desde_m { get; set; }
        public System.TimeSpan horario_hasta_m { get; set; }
        public System.TimeSpan horario_desde_v { get; set; }
        public System.TimeSpan horario_hasta_v { get; set; }
        public int intervalo_citas { get; set; }
        public int intervalo_fechas { get; set; }
        public bool estado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rMedico_Calendario> rMedico_Calendario { get; set; }
    }
}
