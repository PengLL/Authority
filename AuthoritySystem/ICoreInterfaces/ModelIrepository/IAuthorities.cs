using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICoreInterfaces.ModelIrepository
{
    public interface IAuthorities
    {
        int Authority_Id { get; set; }      
        string Authority_Name { get; set; }      
        string Url { get; set; }
    }
}
