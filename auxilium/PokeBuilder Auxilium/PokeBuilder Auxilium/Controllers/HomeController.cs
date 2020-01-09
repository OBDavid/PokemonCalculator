using PokeBuilder_Auxilium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static PokeBuilder_Auxilium.Models.AllData;

namespace PokeBuilder_Auxilium.Controllers
{
    
    public class HomeController : Controller
    {
        private PokeBankEntities db = new PokeBankEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            

            return View();
        }

        public ActionResult PlayNow(String mlogin, String plogin)
        {            
            return View();
        }
        [HttpPost]
        public PartialViewResult AddTeam(string account, string tname)
        {
            acc_account acc = (from a in db.acc_account where a.acc_mail == account select a).First();
            te_team newTeam = new te_team();
            newTeam.acc_account_acc_mail = account;
            newTeam.acc_account = acc;
            newTeam.te_name = tname;
            db.te_team.Add(newTeam);
            db.SaveChanges();

            List<te_team> re = (from a in db.te_team where a.acc_account.acc_mail == account select a).ToList();
            return PartialView("Ajax/_LoadTeam", re);
        }
        [HttpPost]
        public PartialViewResult DeleteTeam(string account, int teamID)
        {
            acc_account acc = (from a in db.acc_account where a.acc_mail == account select a).First();
            te_team team = (from a in db.te_team where a.te_Id == teamID select a).First();
            if(team.acc_account_acc_mail==acc.acc_mail)
            {
                foreach(tp_teampokemon tp in (from a in db.tp_teampokemon where a.te_team_te_Id==team.te_Id select a).ToList())
                {
                    db.tp_teampokemon.Remove(tp);
                }
                db.te_team.Remove(team);
                db.SaveChanges();
            }
            List<te_team> re = (from a in db.te_team where a.acc_account.acc_mail == account select a).ToList();
            return PartialView("Ajax/_LoadTeam", re);
        }
        public ActionResult Teams()
        {
            ViewBag.Title = "Pokemon Calc - Teams";

            var cookie = HttpContext?.Request?.Cookies["LikeCoffeeWithMilk"];
            string mail = cookie?.Values["mail"];
            string password = cookie?.Values["password"];
            AllDataModel data = null;

            if((from a in db.acc_account where a.acc_mail==mail && a.acc_password==password select a).ToList().Count!=0)
            {
                data = new AllDataModel(db);
            }

            return View(data);
        }

        public ActionResult TypeChart()
        {
            List<te_typeEfficiacy> re = (from a in db.te_typeEfficiacy select a).ToList();
            return View(re);
        }

        public PartialViewResult LoadTeam(int teamID)
        {
            var re= (from a in db.te_team where a.te_Id == teamID select a).FirstOrDefault()?.tp_teampokemon;
            ViewBag.selectedID = teamID;
            return PartialView("Ajax/_LoadTeamPokemon", re);
        }
    }
}