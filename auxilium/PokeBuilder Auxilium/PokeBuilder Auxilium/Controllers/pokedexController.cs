using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PokeBuilder_Auxilium.Models;

namespace PokeBuilder_Auxilium.Controllers
{
    public class PokedexController : Controller
    {
        private PokeBankEntities db = new PokeBankEntities();

        // GET: p_pokemon
        public ActionResult Index()
        {
            ViewBag.filter = "";
            ViewBag.sort = "OrderByID";
            return View(db.p_pokemon);
        }
        
        public ActionResult ID(int? id)
        {
           
            if (id == null)
            {
                throw new HttpException(400,"");
            }
            p_pokemon p_pokemon = db.p_pokemon.Find(id);
            if (p_pokemon == null)
            {
                throw new HttpException(404, "");
            }
            return View(p_pokemon);
        }

        private IEnumerable<p_pokemon> OrderByID(IEnumerable<p_pokemon> list)
        {
            ViewBag.sort = "OrderByID";
            IEnumerable<p_pokemon> model=list;
            return model;
        }
        private IEnumerable<p_pokemon> OrderByName(IEnumerable<p_pokemon> list)
        {
            ViewBag.sort = "OrderByName";
            IEnumerable<p_pokemon> model = (from a in list orderby a.p_name select a).ToList();
            return  model;
        }
        private IEnumerable<p_pokemon> OrderByIntNr(IEnumerable<p_pokemon> list)
        {
            ViewBag.sort = "OrderByIntNr";
            IEnumerable<p_pokemon> model = (from a in list orderby a.p_orderNr select a).ToList();
            return  model;
        }
        private IEnumerable<p_pokemon> OrderByTier(IEnumerable<p_pokemon> list)
        {
            ViewBag.sort = "OrderByTier";
            IEnumerable<p_pokemon> AG = (from a in list where a.p_tier == "AG" orderby a.p_orderNr select a).ToList();
            IEnumerable<p_pokemon> Uber = (from a in list where a.p_tier == "Uber" orderby a.p_orderNr select a).ToList();
            IEnumerable<p_pokemon> OU = (from a in list where a.p_tier=="OU" orderby a.p_orderNr select a).ToList();
            IEnumerable<p_pokemon> UUBL = (from a in list where a.p_tier == "UUBL" orderby a.p_orderNr select a).ToList();
            IEnumerable<p_pokemon> UU = (from a in list where a.p_tier == "UU" orderby a.p_orderNr select a).ToList();
            IEnumerable<p_pokemon> RUBL = (from a in list where a.p_tier == "RUBL" orderby a.p_orderNr select a).ToList();
            IEnumerable<p_pokemon> RU = (from a in list where a.p_tier == "RU" orderby a.p_orderNr select a).ToList();
            IEnumerable<p_pokemon> NUBL = (from a in list where a.p_tier == "NUBL" orderby a.p_orderNr select a).ToList();
            IEnumerable<p_pokemon> NU = (from a in list where a.p_tier == "NU" orderby a.p_orderNr select a).ToList();
            IEnumerable<p_pokemon> PUBL = (from a in list where a.p_tier == "RU" orderby a.p_orderNr select a).ToList();
            IEnumerable<p_pokemon> PU = (from a in list where a.p_tier == "PU" orderby a.p_orderNr select a).ToList();
            IEnumerable<p_pokemon> LC = (from a in list where a.p_tier == "LC" orderby a.p_orderNr select a).ToList();
            IEnumerable<p_pokemon> NotKnown = (from a in list where a.p_tier == null orderby a.p_orderNr select a).ToList();
            IEnumerable<p_pokemon> model = (((((((((((AG.Union(Uber)).Union(OU)).Union(UUBL)).Union(UU)).Union(RUBL)).Union(RU)).Union(NUBL)).Union(NU)).Union(PUBL)).Union(PU)).Union(LC)).Union(NotKnown);
            

            return  model;
        }
        
        //Liefert ein PartialView mit gefilterten und/oder sortierten Daten zurück
        public PartialViewResult Filter(String filter,String sort,String last)
        {
            if(filter.Equals(last))
            {
                return PartialView("_Pokemon");
            }
            ViewBag.filter = filter;
            ViewBag.sort = sort;
            IEnumerable<p_pokemon> re=null;

            //liefert eine gefilterte Liste zurück
            if (filter != null&&filter!="")
            {
                ViewBag.filter = filter;
                String[] split= filter.Split(' ');
                bool first = true;
                foreach (var b in split)
                {
                    if (first)
                    {
                        re = (from a in db.p_pokemon where a.p_name.Contains(b.ToLower()) select a).ToList();
                        first = false;
                    }
                    else
                    {
                        re = re.Intersect((from a in db.p_pokemon where a.p_name.Contains(b.ToLower()) select a).ToList());
                    }
                }
            }
            else
            {
                re = db.p_pokemon;
            }
            switch(sort)
            {
                //ruft je nach sort Methode auf welche eine übergebene Liste sortiert und dann den PartialView übergibt welcher dann die Daten der Tabelle generiert
                case "OrderByName":
                    return PartialView("_Pokemon",OrderByName(re));
                    
                case "OrderByIntNr":
                    return PartialView("_Pokemon", OrderByIntNr(re));
                    
                case "OrderByTier":
                    return PartialView ("_Pokemon",OrderByTier(re));
                    
                default:
                    return PartialView("_Pokemon",OrderByID(re));
                    
            }
        }
        [HttpGet]
        public string GetItems()
        {
            List<Object> itemsList = new List<Object>();
            foreach (i_item i in db.i_item)
            {
                itemsList.Add(new { itemID = i.i_Id, itemName = i.i_name.ToUpperInvariant(), itemImportant = i.i_important });
            }
            return JsonConvert.SerializeObject(itemsList.ToArray());
        }
        [HttpGet]
        public string GetPokemons()
        {
            List<Object> pokemonsList = new List<Object>();
            foreach (p_pokemon p in db.p_pokemon)
            {
                if (p.p_Id < 10000)
                {
                    List<Object> statsList = new List<object>();
                    foreach (s_stat s in p.s_stat)
                    {
                        statsList.Add(new { stat = s.s_stat1, statValue = s.s_value });
                    }
                    List<Object> typeList = new List<object>();
                    foreach (t_type t in p.t_type)
                    {
                        typeList.Add(t.t_Id);
                    }
                    pokemonsList.Add(new { pokemonID = p.p_Id, pokemonName = p.p_name.ToUpperInvariant(),type= typeList.ToArray(), tier = p.p_tier, stats = statsList.ToArray(), logicalID = p.p_orderNr });
                }
            }
            return JsonConvert.SerializeObject(pokemonsList.ToArray());
        }
        [HttpGet]
        public string GetNatures()
        {
            List<Object> naturesList = new List<Object>();
            foreach (n_nature n in db.n_nature)
            {
                naturesList.Add(new { nature = n.n_name.ToUpperInvariant(), statBuffed = n.n_statBuffed, statNerfed = n.n_statNerfed });
            }
            return JsonConvert.SerializeObject(naturesList.ToArray());
        }
        [HttpGet]
        public string GetPokemonsMoves()
        {
            List<Object> pokemonsmovesList = new List<Object>();
            foreach (p_pokemon p in db.p_pokemon)
            {
                foreach (m_move m in p.m_move)
                {   
                    pokemonsmovesList.Add(new { pokemonID = p.p_Id, moveID = m.m_Id});
                }
            }
            return JsonConvert.SerializeObject(pokemonsmovesList.ToArray());
        }
        [HttpGet]
        public string GetMoves()
        {
            List<Object> movesList = new List<Object>();
            foreach (m_move m in db.m_move)
            {
                movesList.Add(new { moveID = m.m_Id, moveName = m.m_name.ToUpperInvariant(), moveStrength = m.m_power, moveType = m.t_type_t_Id,damageType=m.dc_damageClass_dc_Id , moveAcc = m.m_accuracy });
            }
            return JsonConvert.SerializeObject(movesList.ToArray());
        }
        [HttpGet]
        public string GetPokemonsAbilities()
        {
            List<Object> pokemonsabilititesList = new List<Object>();
            foreach (p_pokemon p in db.p_pokemon)
            {
                foreach (a_ability a in p.a_ability)
                {
                    pokemonsabilititesList.Add(new { pokemonID = p.p_Id, abilityID = a.a_Id });
                }
            }
            return JsonConvert.SerializeObject(pokemonsabilititesList.ToArray());
        }
        [HttpGet]
        public string GetAbilities()
        {
            List<Object> abilitiesList = new List<Object>();
            foreach (a_ability a in db.a_ability)
            {
                
                abilitiesList.Add(new { abilityID = a.a_Id, abilityName = a.a_name.ToUpperInvariant(), abilityDescription = a.a_description });
            }
            return JsonConvert.SerializeObject(abilitiesList.ToArray());
        }
        [HttpGet]
        public string GetTypeEfficiacy()
        {
            List<Object> typeEffecicyList = new List<Object>();
            foreach (te_typeEfficiacy t in db.te_typeEfficiacy)
            {

                typeEffecicyList.Add(new { atk=t.t_type_t_IdAt,def=t.t_type_t_IdDe,multiplier=(double)t.t_damagePercentage/100.0});
            }
            return JsonConvert.SerializeObject(typeEffecicyList.ToArray());
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    } 
}
