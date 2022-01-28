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
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public partial class mPersona
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mPersona()
        {
            this.mAuditoria = new HashSet<mAuditoria>();
            this.mCita = new HashSet<mCita>();
            this.mCita1 = new HashSet<mCita>();
            this.rMedico_Calendario = new HashSet<rMedico_Calendario>();
            this.rRol_Persona = new HashSet<rRol_Persona>();
        }
        private mAuditoria aud = new mAuditoria();
        public int registro_id { get; set; }
        
        
        [Required(ErrorMessage = "{0}  es requerida")]
        [Display(Name = "Seleccione Tipo de Identificación")]
        [DisplayFormat(NullDisplayText = "Tipo de Identificación No Seleccionada")]
        public int identificacion_tipo { get; set; }
        
        
        [Required(ErrorMessage = "{0}  es requerida")]
        [StringLength(13, MinimumLength = 10)]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        public string identificacion { get; set; }
        
        
        [Required(ErrorMessage = "{0}  es requerida")]
        [StringLength(100, MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Solo se permiten letras")]
        public string nombres { get; set; }
        
        [Required(ErrorMessage = "{0}  es requerida")]
        [StringLength(100, MinimumLength = 2)]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Solo se permiten letras")] 
        public string apellidos { get; set; }
        
        
        [Required(ErrorMessage = "{0}  es requerida")]
        [StringLength(100, MinimumLength = 10)]
        public string direccion { get; set; }
        
        
        [Required(ErrorMessage = "{0}  es requerida")]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")] 
        public string telefono { get; set; }
        
        
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")] 
        public string celular { get; set; }
        
        
        [Required(ErrorMessage ="{0}  es requerida")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        [Display(Name ="Fecha de Nacimiento")]
        public System.DateTime fecha_nacimiento { get; set; }
        
        [Required(ErrorMessage = "{0}  es requerida")]
        [RegularExpression(@"^\S*$", ErrorMessage = "El Correo Electrónico No puede tener espacios ni caracteres especiales")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string correo_electronico { get; set; }
        
        
        [Required(ErrorMessage = "{0}  es requerida")]
        public int genero { get; set; }
        
        
        [Required(ErrorMessage = "{0}  es requerida")]
        public int ciudad_residencia { get; set; }
        
        
        [Required(ErrorMessage = "{0}  es requerida")]
        public int nacionalidad { get; set; }
        
        
        [Required(ErrorMessage = "{0}  es requerida")]
        [DataType(DataType.Password)]
        public string clave { get; set; }
        
        public bool estado { get; set; }



        
        public virtual Catalogo Catalogo { get; set; }
        public virtual Catalogo Catalogo1 { get; set; }
        public virtual Catalogo Catalogo2 { get; set; }
        public virtual Catalogo Catalogo3 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mAuditoria> mAuditoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mCita> mCita { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mCita> mCita1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rMedico_Calendario> rMedico_Calendario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rRol_Persona> rRol_Persona { get; set; }


        public mPersona unRegistro(ref string Mensaje, string Identificacion)
        {
            var registro = new mPersona();
            try
            {
                using (var conexion = new SIHEntities())
                {
                    registro = conexion.mPersona.Where(a => a.identificacion == Identificacion).Single();
                    Mensaje = "0000IDENTIFICACIÓN EXISTE EN EL SISTEMA";
                }
            }
            catch (Exception ex)
            {
                Mensaje = "0010IDENTIFICACIÓN NO EXISTE EN EL SISTEMA" + ex.Message;
                //throw;
            }
            return registro;
        }
        public string ObtenerRol(ref string Mensaje, string registro_id)
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
                Mensaje = "0020" + ex.Message;
            }
            //string Log = aud.InsertarLog("public string ObtenerRol(ref string Mensaje , string registro_id)", SentenciaSQL, 1);
            return Rol;
        }
    }
}
