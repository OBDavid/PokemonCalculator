//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PokeBuilder_Auxilium.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class m_move
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public m_move()
        {
            this.p_pokemon = new HashSet<p_pokemon>();
            this.tp_teampokemon = new HashSet<tp_teampokemon>();
        }
    
        public int m_Id { get; set; }
        public string m_name { get; set; }
        public int t_type_t_Id { get; set; }
        public Nullable<int> m_power { get; set; }
        public Nullable<int> m_pp { get; set; }
        public Nullable<int> m_accuracy { get; set; }
        public Nullable<int> m_priority { get; set; }
        public int dc_damageClass_dc_Id { get; set; }
        public int me_moveEffect_me_Id { get; set; }
    
        public virtual dc_damageClass dc_damageClass { get; set; }
        public virtual me_moveEffect me_moveEffect { get; set; }
        public virtual t_type t_type { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<p_pokemon> p_pokemon { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tp_teampokemon> tp_teampokemon { get; set; }
    }
}
