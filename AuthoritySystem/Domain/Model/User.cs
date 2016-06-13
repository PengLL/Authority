
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ICoreInterfaces.ModelIrepository;

namespace Domain.Model
{
    public class User:Iuser
    {
        public int User_Id { get; set; }
        [Required(ErrorMessage = "请填写用户名")]
        public string User_Name { get; set; }
        [Required(ErrorMessage = "请填写密码")]
        public string Pwd { get; set; }
    }
}
