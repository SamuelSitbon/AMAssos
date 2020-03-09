//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AMAssos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sortie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sortie()
        {
            this.SortieAdherents = new HashSet<SortieAdherent>();
        }
    
        public int IdSortie { get; set; }
        public int IdAssociation { get; set; }
        public string Nom { get; set; }
        public decimal Prix { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public System.DateTime Date { get; set; }
        public int CapaciteMinimum { get; set; }
        public int CapaciteMaximum { get; set; }
    
        public virtual Association Association { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SortieAdherent> SortieAdherents { get; set; }
    }
}
