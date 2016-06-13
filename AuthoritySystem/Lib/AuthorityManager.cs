using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using System.Configuration;
using System.Web;
using Domain.DataManager;
using ICoreInterfaces;
using ICoreInterfaces.ModelIrepository;


namespace Lib
{
    public class AuthorityManager : IAuthorityManager
    {
        private SqlData sqlData = new SqlData();
        private string connString = ConfigurationManager.ConnectionStrings["AuthorityData"].ToString();
        private string sqlString = string.Empty;
        private List<Parameter> paraList;
        
        //public User GetUser()
        //{
        //    userName = HttpContext.Current.User.Identity.Name;
        //    paraList = new List<Parameter>();
        //    sqlString = "Select * from A_User where User_Name=@userName";
        //    paraList.Add(new Parameter { ParameterName = "@userName", Value = userName });
        //    return sqlData.SelectOneData<User>(sqlString, connString, paraList);
        //}
        public Iuser GetUser(string Name)
        {
            paraList = new List<Parameter>();
            sqlString = "Select * from A_User where User_Name=@userName";
            paraList.Add(new Parameter { ParameterName = "@userName", Value = Name });
            return sqlData.SelectOneData<User>(sqlString, connString, paraList);
        }
        public IRole GetRole(string roleName)
        {
            sqlString = "select * from A_Roles where Role_Name=@roleName";
            paraList = new List<Parameter>{
                new Parameter{ParameterName="@roleName",Value=roleName}
            };
            return sqlData.SelectOneData<Role>(sqlString, connString, paraList);
        }
        public Iuser[] GetAllUsers()
        {
            paraList = new List<Parameter>();
            sqlString = "select User_Name,User_Id from A_User";
            return sqlData.SelectDataList<User>(sqlString, connString, paraList).ToArray();
        }
        public IRole[] GetAllRoles()
        {
            paraList = new List<Parameter>();
            sqlString = "select * from A_Roles";
            return sqlData.SelectDataList<Role>(sqlString, connString, paraList).ToArray();
        }
        public IAuthorities[] GetAllAuthorities()
        {
            sqlString = "select * from A_Authority";
            paraList = new List<Parameter>();
            return sqlData.SelectDataList<Authorities>(sqlString, connString, paraList).ToArray();
        }
        //public List<Authorities> GetRolesAuthority()
        //{
        //  int roleId = GetUsersRole().Role_Id;
        //  sqlString = @"SELECT   a.Authority_Name FROM A_Authority  a
        //  INNER JOIN A_Roles_Authority  b
        //  ON a.Authority_Id = b.Authority_Id 
        //  where Role_Id=@roleId ";
        //  paraList = new List<Parameter>{
        //  new Parameter{ParameterName="@roleId",Value=roleId}
        //   
        //  List<Authorities> list = sqlData.SelectDataList<Authorities>(sqlString, connString, paraList);
        //  return list;
        //  }
        public IAuthorities[] GetRolesAuthority(string roleName)
        {
            sqlString = "SELECT   A_Authority.* FROM A_Authority INNER JOIN"+
                " A_Roles_Authority ON A_Authority.Authority_Id = A_Roles_Authority.Authority_Id INNER JOIN"+
                " A_Roles ON A_Roles_Authority.Role_Id = A_Roles.Role_Id where Role_Name=@roleName";
            paraList = new List<Parameter>{
                new Parameter{ParameterName="@roleName",Value=roleName}
            };
            List<Authorities> list = sqlData.SelectDataList<Authorities>(sqlString, connString, paraList);
            return list.ToArray();
        }
        //public Role GetUsersRole()
        //{
        //    paraList=new List<Parameter>{
        //        new Parameter{ParameterName="@userId",Value=GetUser().User_Id}
        //    };
        //    sqlString = "SELECT * FROM  A_Roles INNER JOIN A_UserInRole ON A_Roles.Role_Id = A_UserInRole.Role_Id where User_Id=@userId";
        //    Role role = sqlData.SelectOneData<Role>(sqlString, connString, paraList);
        //    return role;
        //}
        public IRole GetUsersRole(string name)
        {
            sqlString = "SELECT   A_Roles.* FROM A_Roles INNER JOIN"+
                " A_UserInRole ON A_Roles.Role_Id = A_UserInRole.Role_Id INNER JOIN"+
                " A_User ON A_UserInRole.User_Id = A_User.User_Id where User_Name=@name";
            paraList = new List<Parameter>{
                new Parameter{ParameterName="@name",Value=name}
            };
            Role role = sqlData.SelectOneData<Role>(sqlString, connString, paraList);
            return role;
        }
        public Iuser[] GetRolesUsers(string roleName)
        {
            sqlString = @"SELECT   a.User_Id,a.User_Name FROM A_User a INNER JOIN
                A_UserInRole b ON a.User_Id = b.User_Id INNER JOIN
                A_Roles c ON b.Role_Id = c.Role_Id where Role_Name=@roleName";
            paraList = new List<Parameter>{
                new Parameter{ParameterName="@roleName",Value=roleName}
            };
            List<User> list= sqlData.SelectDataList<User>(sqlString, connString, paraList);
            User[] users = list.ToArray();
            return users;
        }
        public IAuthorities GetAuthority(string authorityName)
        {
            sqlString = "select * from A_Authority where Authority_Name=@authorityName";
            paraList = new List<Parameter>{
                new Parameter{ParameterName="@authorityName",Value=authorityName}
            };
            return sqlData.SelectOneData<Authorities>(sqlString, connString, paraList);
        }
        public bool SaveAuthority(IAuthorities authority)
        {
            sqlString = "update A_Authority set Authority_Name=@authorityName, Url=@url where Authority_Id=@authorityId";
            paraList = new List<Parameter>{
                new Parameter{ParameterName="@authorityName",Value=authority.Authority_Name},
                new Parameter{ParameterName="@url",Value=authority.Url},
                new Parameter{ParameterName="@authorityId",Value=authority.Authority_Id}
            };
            return sqlData.InsertOrUpdateOrDelete(sqlString, connString, paraList);
        }
        public bool DeleteAuthority(string authorityName)
        {
            sqlString = "delete  from A_Authority where Authority_Name=@authorityName";
            paraList = new List<Parameter>{
                new Parameter{ParameterName="@authorityName",Value=authorityName}
            };
            return sqlData.InsertOrUpdateOrDelete(sqlString, connString, paraList);
        }
        //public bool HasAuthority(string url,string userName)
        //{
        //    bool flag = false;
        //    if (UrlRequireAuthority(url))
        //    {
        //        List<Authorities> list = GetRolesAuthority(GetUsersRole(userName).Role_Name);
        //        foreach (var item in list)
        //        {
        //            if (item.Url == url)
        //                flag = true;
        //        }
        //        return flag;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
        //public bool UrlRequireAuthority(string url)
        //{
        //    bool flag = false;
            
        //    List<Authorities> existAuthorities = GetAllAuthorities();
        //    foreach (var item in existAuthorities)
        //    {
        //        if (item.Url == url)
        //        {
        //            flag = true;
        //            break;
        //        }
        //    }
        //    return flag;
        //}
        //public bool IsInRole(string userName)
        //{
        //    userName = HttpContext.Current.User.Identity.Name;
        //    paraList = new List<Parameter>();
        //    HttpRequest request = HttpContext.Current.Request;
        //    string url = request.Path;
        //    sqlString = " SELECT   A_Authority.Url" +
        //            " FROM A_Authority INNER JOIN " +
        //            " A_Roles_Authority ON A_Authority.Authority_Id = A_Roles_Authority.Authority_Id INNER JOIN " +
        //            " A_Roles ON A_Roles_Authority.Role_Id = A_Roles.Role_Id INNER JOIN " +
        //            " A_UserInRole ON A_Roles.Role_Id = A_UserInRole.Role_Id INNER JOIN " +
        //            " A_User ON A_UserInRole.User_Id = A_User.User_Id " +
        //            " where A_User.User_Name =@userName";
        //    paraList.Add(new Parameter { ParameterName = "@userName", Value = userName });
        //    List<string> list = sqlData.SelectDataList(sqlString, connString, paraList, "Url");
        //    foreach (string str in list)
        //    {
        //        if (str == url)
        //            return true;
        //    }
        //    return false;
        //}
        public bool AddRole(string RoleName)
        {
            sqlString = "insert into A_Roles(Role_Name) values (@roleName)";
            List<Parameter> paraList = new List<Parameter>{
                new Parameter{ParameterName="@roleName",Value=RoleName},            
            };
            if (sqlData.InsertOrUpdateOrDelete(sqlString, connString, paraList))
                return true;
            else
                return false;
        }
        //public bool DeleteRole(int roleId)
        //{
        //    sqlString = "Delete from A_Roles where Role_Id=@roleId";
        //    paraList = new List<Parameter>{
        //        new Parameter{ParameterName="@roleId",Value=roleId}
        //    };
        //    return sqlData.InsertOrUpdateOrDelete(sqlString, connString, paraList);
        //}
        public bool DeleteRole(int roleId)
        {
            sqlString = "DeleteRole";
            paraList = new List<Parameter>{
                new Parameter{ParameterName="@roleId",Value=roleId}
            };
            return sqlData.ExcuteProcedure(sqlString, connString, paraList);
        }
        public bool AddUserInRoles(int userId,int roleId)
        {
            sqlString = "insert into A_UserInRole (User_Id,Role_Id) values (@userId,@roleId)";
            paraList = new List<Parameter>{
                new Parameter{ParameterName="@userId",Value=userId},
                new Parameter{ParameterName="@roleId",Value=roleId}
            };
            return sqlData.InsertOrUpdateOrDelete(sqlString, connString, paraList);
        }
        public bool AddRolesAuthority(int roleId,int authorityId)
        {
            sqlString = "insert into A_Roles_Authority (Role_Id,Authority_Id) values (@roleId,@AuthorityId)";
            paraList = new List<Parameter>{
                    new Parameter{ParameterName="@roleId",Value=roleId},
                    new Parameter{ParameterName="@AuthorityId",Value=authorityId}            
            };
            return sqlData.InsertOrUpdateOrDelete(sqlString, connString, paraList);
        }
        public bool ClearRolesAuthority(int roleId)
        {
            sqlString = "delete from A_Roles_Authority where Role_Id=@roleId";
            paraList = new List<Parameter>{
                new Parameter{ParameterName="@roleId",Value=roleId}
            };
            if (sqlData.InsertOrUpdateOrDelete(sqlString, connString, paraList))
                return true;
            else
                return false;
        }
        public bool AddUser(Iuser user)
        {         
            sqlString = "insert into A_User(User_Name,Pwd) values (@userName,@pwd)";
            List<Parameter> paraList = new List<Parameter>{
                new Parameter{ParameterName="@userName",Value=user.User_Name},
                new Parameter{ParameterName="@pwd",Value=user.Pwd}
            };
            if (sqlData.InsertOrUpdateOrDelete(sqlString, connString, paraList))
                return true;
            else
                return false;
        }
        public bool AddAuthority(IAuthorities authority)
        {
            sqlString = "insert into A_Authority (Authority_Name,Url) values (@name,@url)";
            paraList = new List<Parameter>{
                new Parameter{ParameterName="@name",Value=authority.Authority_Name},
                new Parameter{ParameterName="@url",Value=authority.Url}
            };
            return sqlData.InsertOrUpdateOrDelete(sqlString, connString, paraList);
        }
        public bool DeleteUser(int userId)
        {
            sqlString = "delete from A_User where User_Id=@userId";
            paraList = new List<Parameter>{
                new Parameter{ParameterName="@userId",Value=userId}
            };
            if (sqlData.InsertOrUpdateOrDelete(sqlString, connString, paraList))
                return true;
            else
                return false;
        }
        public IRole[] SearchRoles(string text)
        {
            sqlString = "Select * from A_Roles where Role_Name like '%' + @text +'%'";
            paraList = new List<Parameter>{
                new Parameter{ParameterName="@text",Value=text}
            };
            return sqlData.SelectDataList<Role>(sqlString, connString, paraList).ToArray();
        }
        public Iuser[] SearchUsers(string text)
        {
            sqlString = "select * from A_User where User_Name like '%'+@text+'%'";
            paraList = new List<Parameter>{
                new Parameter{ParameterName="@text",Value=text}
            };
            return sqlData.SelectDataList<User>(sqlString, connString, paraList).ToArray();           
        }
        public bool UpdateUsersRole(int userId,int roleId)
        {
            sqlString = "update A_UserInRole set Role_Id=@roleId where User_Id=@userId ";
            paraList = new List<Parameter>{
                new Parameter{ParameterName="@roleId",Value=roleId},
                new Parameter{ParameterName="@userId",Value=userId}
            };
            return sqlData.InsertOrUpdateOrDelete(sqlString, connString, paraList);
        }
        public bool SaveLog(int userId,DateTime date,string content)
        {
            sqlString = "insert into A_Log (User_Id,Date,Content) values (@userId,@date,@content)";
            paraList = new List<Parameter>{
                new Parameter{ParameterName="@userId",Value=userId},
                new Parameter{ParameterName="@date",Value=date},
                new Parameter{ParameterName="@content",Value=content}
            };
            return sqlData.InsertOrUpdateOrDelete(sqlString, connString, paraList);
        }
        public Iuser GetUserById(string id)
        {
            return new User { User_Name="test4interface", Pwd="223", User_Id=556 };
        }

        //IEnumerable<User> IUserRepository<User>.GetAllUsers()
        //{
        //    return new List<User> {
        //     new User { User_Name="test4interface", Pwd="223", User_Id=556 },
        //     new User { User_Name="test4interface", Pwd="223", User_Id=556 },
        //     new User { User_Name="test4interface", Pwd="223", User_Id=556 },
        //    };
        //}
    }
}

