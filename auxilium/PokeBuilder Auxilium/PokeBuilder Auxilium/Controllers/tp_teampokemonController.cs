using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PokeBuilder_Auxilium.Models;

namespace PokeBuilder_Auxilium.Controllers
{
    public class tp_teampokemonController : Controller
    {
        private PokeBankEntities db = new PokeBankEntities();


        public ActionResult AddTeampokemon(int teamID)
        {
            ViewBag.teamID = teamID;
            AllData.AllDataModel all = new AllData.AllDataModel(db);
            return View(all);
        }

        [HttpPost]
        public ActionResult AddTeampokemon([Bind(Include = "tp_nickname, p_pokemon_p_Id,a_ability_a_Id,n_natures_n_Id,i_item_i_Id,te_team_te_Id,tp_evHP,tp_dvHP,tp_evAtk,tp_dvAtk,tp_evSpA,tp_dvSpA,tp_evDef,tp_dvDef,tp_evSpD,tp_dvSpD,tp_evSpe,tp_dvSpe,tp_Level")] tp_teampokemon tp)
        {
            db.tp_teampokemon.Add(tp);
            db.SaveChanges();
            return RedirectToAction("Teams", "Home", null);
        }

        [HttpPost]
        public ActionResult RemovePokemon(int id)
        {
            tp_teampokemon remove = (from a in db.tp_teampokemon where a.tp_Id == id select a).First();
            db.tp_teampokemon.Remove(remove);
            db.SaveChanges();
            return null;
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
