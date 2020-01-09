using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PokeBuilder_Auxilium.Models
{
    [MetadataType(typeof(AccountMetaData))]
    public partial class acc_account
    {

    }
    public class AccountMetaData
    {
        [Display(Name = "E-Mail")]
        public string acc_mail { get; set; }
        [Display(Name ="Password")]
        public string acc_password { get; set; }

       [Display(Name = "Username")]
        public string acc_Name { get; set; }
    }
}