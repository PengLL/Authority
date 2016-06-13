using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ICoreInterfaces.ModelIrepository;

namespace Domain.Model
{
    public class Authorities:IAuthorities
    {
        public int Authority_Id { get; set; }
        [Required(ErrorMessage="请输入权限名")]
        public string Authority_Name { get; set; }
        [Required(ErrorMessage="请输入Url")]
        public string Url { get; set; }
    }
}
