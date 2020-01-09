using PokeBuilder_Auxilium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PokeBuilder_Auxilium.Controllers
{
    public class AccountController : Controller
    {
        PokeBankEntities db = new PokeBankEntities();

        [HttpPost]
        public PartialViewResult Login(string mail, string password, bool? rememberMe)
        {
            acc_account account;
            var erg = from s in db.acc_account
                      where s.acc_mail == mail && s.acc_password == password
                      select s;
            account = erg.FirstOrDefault();

            if (account != null)
            {

                HttpCookie login = new HttpCookie("LikeCoffeeWithMilk");
                login["mail"] = account.acc_mail;
                login["password"] = account.acc_password;
                HttpContext.Response.Cookies.Set(login);
            }
            return PartialView("_LoginMenu", false);
        }
        [HttpPost]
        public PartialViewResult Register([Bind(Include = "acc_Name, acc_mail, acc_password")] acc_account acc)
        {
            if (acc != null && (from a in db.acc_account where a.acc_mail == acc.acc_mail select a).Count() == 0)
            {
                db.acc_account.Add(acc);
                db.SaveChanges();
            }

            PartialViewResult re=Login(acc.acc_mail, acc.acc_password, false);
            acc = null;
            return re;
        }

        [HttpPost]
        public PartialViewResult LogOut()
        {
            if (Request.Cookies["LikeCoffeeWithMilk"] != null)
            {
                Response.Cookies["LikeCoffeeWithMilk"].Expires = DateTime.Now.AddDays(-1);
            }
            return PartialView("_LoginMenu", true);
        }

        
        public ActionResult DeleteAccount()
        {
            var cookie = HttpContext.Request.Cookies["LikeCoffeeWithMilk"];
            string mail = cookie.Values.GetValues("mail")?.First();
            Response.Cookies["LikeCoffeeWithMilk"].Expires = DateTime.Now.AddDays(-1);
            acc_account acc=(from a in db.acc_account where a.acc_mail == mail select a).First();

            List<te_team> deletesTE = new List<te_team>();
            foreach(te_team t in acc.te_team)
            {
                List<tp_teampokemon> deletesTP=new List<tp_teampokemon>();
                foreach(tp_teampokemon tp in t.tp_teampokemon)
                {
                    deletesTP.Add(tp);
                }
                db.tp_teampokemon.RemoveRange(deletesTP);
                deletesTE.Add(t);
            }
            db.te_team.RemoveRange(deletesTE);
            db.acc_account.Remove(acc);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}