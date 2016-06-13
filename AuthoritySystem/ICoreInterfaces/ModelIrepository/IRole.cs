using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICoreInterfaces.ModelIrepository
{
    public interface IRole
    {
        int Role_Id { get; set; }    
        string Role_Name { get; set; }
        bool SuperAdmin { get; set; }
    }
}
