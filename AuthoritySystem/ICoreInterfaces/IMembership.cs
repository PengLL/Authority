using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICoreInterfaces
{
    public interface IMembership
    {
        bool ValidateUser(string name,string pwd);
    }
}
