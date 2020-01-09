using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokeBuilder_Auxilium.Models
{
    public class AllData
    {
        public class AllDataModel
        {
            public AllDataModel(PokeBankEntities db)
            {
                abilities = db.a_ability.ToList();
                accounts = db.acc_account.ToList();
                damageClasses = db.dc_damageClass.ToList();
                items = db.i_item.ToList();
                moves = db.m_move.ToList();
                meffects = db.me_moveEffect.ToList();
                natures = db.n_nature.ToList();
                pokemons = db.p_pokemon.ToList();
                stats = db.s_stat.ToList();
                types = db.t_type.ToList();
                teams = db.te_team.ToList();
                tefficiacies = db.te_typeEfficiacy.ToList();
                teampokemons = db.tp_teampokemon.ToList();
            }
            public IList<a_ability> abilities { get; }
            public IList<acc_account> accounts { get; }
            public IList<dc_damageClass> damageClasses { get; }
            public IList<i_item> items { get; }
            public IList<m_move> moves { get; }
            public IList<me_moveEffect> meffects { get; }
            public IList<n_nature> natures { get; }
            public IList<p_pokemon> pokemons { get; }
            public IList<s_stat> stats { get; }
            public IList<t_type> types { get; }
            public IList<te_team> teams { get; }
            public IList<te_typeEfficiacy> tefficiacies { get; }
            public IList<tp_teampokemon> teampokemons { get; }
        }
    }
}