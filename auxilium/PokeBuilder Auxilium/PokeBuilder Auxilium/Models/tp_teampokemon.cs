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
    
    public partial class tp_teampokemon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tp_teampokemon()
        {
            this.m_move = new HashSet<m_move>();
        }
    
        public int tp_Id { get; set; }
        public string tp_nickname { get; set; }
        public int p_pokemon_p_Id { get; set; }
        public int a_ability_a_Id { get; set; }
        public int n_natures_n_Id { get; set; }
        public int i_item_i_Id { get; set; }
        public int te_team_te_Id { get; set; }
        public Nullable<int> tp_evHP { get; set; }
        public Nullable<int> tp_dvHP { get; set; }
        public Nullable<int> tp_evAtk { get; set; }
        public Nullable<int> tp_dvAtk { get; set; }
        public Nullable<int> tp_evSpA { get; set; }
        public Nullable<int> tp_dvSpA { get; set; }
        public Nullable<int> tp_evDef { get; set; }
        public Nullable<int> tp_dvDef { get; set; }
        public Nullable<int> tp_evSpD { get; set; }
        public Nullable<int> tp_dvSpD { get; set; }
        public Nullable<int> tp_evSpe { get; set; }
        public Nullable<int> tp_dvSpe { get; set; }
        public Nullable<int> tp_Level { get; set; }
    
        public virtual a_ability a_ability { get; set; }
        public virtual i_item i_item { get; set; }
        public virtual n_nature n_nature { get; set; }
        public virtual p_pokemon p_pokemon { get; set; }
        public virtual te_team te_team { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<m_move> m_move { get; set; }
    }
}
