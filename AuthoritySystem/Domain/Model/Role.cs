using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ICoreInterfaces.ModelIrepository;

namespace Domain.Model
{
    public class Role:IRole
    {
        public int Role_Id { get; set; }
        [Required]
        public string Role_Name { get; set; }
        public bool SuperAdmin { get; set; }
    }
}
