//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Platform.Entity.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Integrante
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Integrante()
        {
            this.Actividad = new HashSet<Actividad>();
        }
    
        public int id { get; set; }
        public int Proyecto_id { get; set; }
        public int Cargo_id { get; set; }
        public int Usuario_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Actividad> Actividad { get; set; }
        public virtual Cargo Cargo { get; set; }
        public virtual Proyecto Proyecto { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
