using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PokeBuilder_Auxilium.Models
{
    [MetadataType(typeof(PokemonMetaData))]
    public partial class p_pokemon
    {

    }

    public class PokemonMetaData
    {
        [Display(Name ="PokedexID")]
        public int p_Id { get; set; }
        [Display(Name ="Name")]
        public string p_name { get; set; }
        [Display(Name ="Interne Nr.")]
        public Nullable<int> p_orderNr { get; set; }

    }
}