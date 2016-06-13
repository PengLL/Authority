using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICoreInterfaces.ModelIrepository
{
    public interface Iuser
    {
        int User_Id { get; set; }
        string User_Name { get; set; }
        string Pwd { get; set; }
    }
}
