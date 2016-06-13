using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICoreInterfaces.ModelIrepository;

namespace ICoreInterfaces
{
    public interface IAuthorityManager
    {

        // Summary:
        //     从数据库的表中获取一个用户的实例
        // Parameters:
        //   Name:
        //     用户的名字
        // Returns:
        //     用户实例
        Iuser GetUser(string Name);
        // Summary:
        //     从数据库中的表中获取一个角色的实例
        //
        // Parameters:
        //   Name:
        //     所需要获得的角色的名字    
        // Returns:
        //     角色的实例
        IRole GetRole(string roleName);
        // Summary:
        //    从数据库中获取某一个用户所属的角色
        //
        // Parameters:
        //   name:
        //     需要获取角色的用户的名字
        // Returns:
        //     角色的实例
        IRole GetUsersRole(string name);
        // Summary:
        //    获取当前数据库中所有的用户      
        // Returns:
        //    一个包含多个用户实例的数组
        Iuser[] GetAllUsers();
        // Summary:
        //   通过角色名获取当前角色下的所有的用户
        //
        // Parameters:
        //   roleName:
        //    角色名      
        // Returns:
        //     一个包含多个用户实例的数组
        Iuser[] GetRolesUsers(string roleName);
        // Summary:
        //     查找用户名包含指定字符串的所有用户
        // Parameters:
        //   text:
        //     一个字符串，来匹配用户名是否包含该字符串 
        // Returns:
        //     一个包含多个用户实例的数组
        Iuser[] SearchUsers(string text);
        // Summary:
        //     获取当前数据库中所有的角色
        // 
        // Returns:
        //     一个包含多个角色实例的数组
        IRole[] GetAllRoles();
        // Summary:
        //     查找用户名包含指定字符串的所有用户
        // Parameters:
        //   text:
        //     一个字符串，来匹配角色名是否包含该字符串 
        // Returns:
        //     一个包含多个角色实例的数组
        IRole[] SearchRoles(string text);
        // Summary:
        //     获取当前数据库中所设立的所有权限
        // 
        // Returns:
        //     一个包含多个权限实例的数组
        IAuthorities[] GetAllAuthorities();
        // Summary:
        //     获取某个角色所有的权限
        // Parameters:
        //   roleName:
        //     角色名
        // Returns:
        //     一个包含多个权限实例的数组
        IAuthorities[] GetRolesAuthority(string roleName);
        // Summary:
        //     通过权限名获取权限的一个实例
        // Parameters:
        //   authorityName:
        //     权限名 
        // Returns:
        //     权限的一个实例  
        IAuthorities GetAuthority(string authorityName);
        // Summary:
        //     将获取的权限实例保存至数据库的权限表中
        // Parameters:
        //   authorities:
        //     权限的一个实例 
        // Returns:
        //     如果保存成功就返回true，反之为false  
        bool SaveAuthority(IAuthorities authorities);
        // Summary:
        //     从设置的权限中删除一个权限
        // Parameters:
        //   authorities:
        //     权限名
        // Returns:
        //     如果删除成功就返回true，反之为false  
        bool DeleteAuthority(string authorityName);
        // Summary:
        //     从当前的未设置权限的URL中选择一个，设置访问该URL或执行某个操作，需要权限
        // Parameters:
        //   authorities:
        //     权限的一个实例 
        // Returns:
        //     如果添加权限成功就返回true，反之为false  
        bool AddAuthority(IAuthorities authorities);
        // Summary:
        //     给某个角色添加权限
        // Parameters:
        //   roleId:
        //     被添加角色的ID 
        // authorityId:
        //     被添加权限的Id
        // Returns:
        //     如果添加成功就返回true，反之为false  
        bool AddRolesAuthority(int roleId, int authorityId);
        // Summary:
        //     清除一个角色的所有权限，使该角色没有任何权限
        // Parameters:
        //   roelId:
        //     角色的ID 
        // Returns:
        //     如果清除成功就返回true，反之则返回fals  
        bool ClearRolesAuthority(int roleId);
        // Summary:
        //     添加一个新的角色，角色名不能和之前的角色重复
        // Parameters:
        //   roelName:
        //     角色的ID 
        // Returns:
        //     如果添加成功就返回true，反之则返回fals  
        bool AddRole(string roleName);
        // Summary:
        //     删除一个角色
        // Parameters:
        //   roelId:
        //     角色的ID 
        // Returns:
        //     如果删除成功就返回true，反之则返回fals  
        bool DeleteRole(int roleId);
        // Summary:
        //     把一个用户添加到一个角色中
        // Parameters:
        //   userId：
        //     用户的Id
        //   roelId:
        //     角色的Id 
        // Returns:
        //     如果添加成功就返回true，反之则返回fals  
        bool AddUserInRoles(int userId, int roleId);
        // Summary:
        //     添加一个用户
        // Parameters:
        //   user:
        //     用户的一个实例
        // Returns:
        //     如果添加成功就返回true，反之则返回fals  
        bool AddUser(Iuser user);
        // Summary:
        //     通过一个用户的Id删除用户
        // Parameters:
        //   userId:
        //     角色的ID 
        // Returns:
        //     如果删除成功就返回true，反之则返回fals  
        bool DeleteUser(int userId);
        // Summary:
        //     更改用户的角色
        // Parameters:
        //   userId:
        //     被更改的用户的Id
        //   roleId:
        //     用户将被更改为的角色Id
        // Returns:
        //     如果更新成功就返回true，反之则返回fals  
        bool UpdateUsersRole(int userId, int roleId);
        // Summary:
        //     保存当前用户的日志
        // Parameters:
        //   userId:
        //     当前登录的用户ID 
        //   date:
        //     现在的日期
        //   content:
        //     日志的内容
        // Returns:
        //     如果保存成功就返回true，反之则返回fals  
        bool SaveLog(int userId, DateTime date, string content);
    }
}
