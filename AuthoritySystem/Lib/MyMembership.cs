using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DataManager;
using Domain.Model;
using System.Configuration;
using ICoreInterfaces;

namespace Lib
{
    public class MyMembership : IMembership
    {
        private string connStirng = ConfigurationManager.ConnectionStrings["AuthorityData"].ToString();
        private SqlData sqldata = new SqlData();
        public bool ValidateUser(string name, string pwd)
        {
            if (name == null || pwd == null)
                return false;
            string sqlString="Select * from A_User where User_Name=@Name and Pwd=@pwd";
            List<Parameter> list =new List<Parameter>{
                new Parameter{ParameterName="@name",Value=name},
                new Parameter{ParameterName="@pwd",Value=pwd}
            };
            if (sqldata.HasThisData(sqlString, connStirng, list))
                return true;
            else
                return false;
        }
    }
}
