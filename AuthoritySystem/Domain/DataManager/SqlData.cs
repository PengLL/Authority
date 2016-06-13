using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;
using System.Data.SqlClient;
using System.Reflection;
using System.Data;

namespace Domain.DataManager
{
    public class SqlData 
    {
        private SqlConnection conn;
        bool flag = false;
        public bool HasThisData(string sqlString,string connString, List<Parameter> paraList)
        {         
            SqlDataReader rd;
            using(conn=new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlString, conn);
                foreach (var item in paraList)
                {
                    cmd.Parameters.AddWithValue(item.ParameterName, item.Value);
                }
                rd = cmd.ExecuteReader();
                if(rd.Read())
                    flag=true;
                conn.Close();
            }
            if (flag)
                return true;
            else
                return false;
        }
        public string SelectOneDataProperty(string sqlString, string connString, List<Parameter> paraList,string property)
        {          
            string result="";
            using (conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlString, conn);
                foreach (var item in paraList)
                {
                    cmd.Parameters.AddWithValue(item.ParameterName, item.Value);
                }
                SqlDataReader rd = cmd.ExecuteReader();
                if(rd.Read())
                    result=rd[property].ToString();
                conn.Close();
            }
            return result;
        }
        public T SelectOneData<T>(string sqlString, string connString, List<Parameter> paraList)where T:new()
        {
            string tempName = string.Empty;
            T temp = new T();
            using (conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlString, conn);
                foreach (var item in paraList)
                {
                    cmd.Parameters.AddWithValue(item.ParameterName, item.Value);
                }
                SqlDataReader rd = cmd.ExecuteReader();              
                PropertyInfo[] properties = temp.GetType().GetProperties();
                if(rd.Read())
                {
                    foreach (PropertyInfo pi in properties)
                    {                                  
                        tempName = pi.Name;//将属性名称赋值给临时变量   
                        if (!pi.CanWrite)// 判断此属性是否有Setter   
                            continue;//该属性不可写，直接跳出    
                        var value = rd[tempName];
                        if (value != DBNull.Value)//如果非空，则赋给对象的属性   
                            pi.SetValue(temp, value, null); //取值   
                    }                                                                                         
                }
                conn.Close();
            }
            return temp;
        }
        public bool InsertOrUpdateOrDelete(string sqlString, string connString, List<Parameter> paraList)
        {          
            using (conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlString, conn);
                foreach (var item in paraList)
                {
                    cmd.Parameters.AddWithValue(item.ParameterName, item.Value);
                }
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                if (i > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<string> SelectDataList(string sqlString, string connString, List<Parameter> paraList,string property)
        {
            List<string> list = new List<string>();          
            using (conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlString, conn);
                foreach (var item in paraList)
                {
                    cmd.Parameters.AddWithValue(item.ParameterName, item.Value);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable("DataList");
                ds.Tables.Add(dt);
                da.Fill(ds.Tables["DataList"]);                      
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(row[property].ToString());            
                }
                conn.Close();
            }
            return list;
        }
        public List<T> SelectDataList<T>(string sqlString, string connString, List<Parameter> paraList) where T:new()
        {
            List<T> list = new List<T>();
            string tempName = string.Empty;

            using (conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlString, conn);
                foreach (var item in paraList)
                {
                    cmd.Parameters.AddWithValue(item.ParameterName, item.Value);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable("DataList");
                ds.Tables.Add(dt);
                da.Fill(ds.Tables["DataList"]);
                T temp = new T();
                PropertyInfo[] properties = temp.GetType().GetProperties();
                foreach (DataRow row in dt.Rows)
                {
                    T tempObj = new T();
                    foreach (PropertyInfo pi in properties)
                    {
                        tempName = pi.Name;//将属性名称赋值给临时变量   
                        if (dt.Columns.Contains(tempName)) //检查DataTable是否包含此列（列名==对象的属性名）    
                        {
                            if (!pi.CanWrite)// 判断此属性是否有Setter   
                                continue;//该属性不可写，直接跳出     
                            var value = row[tempName];//取值                                                 
                            if (value != DBNull.Value)//如果非空，则赋给对象的属性   
                                pi.SetValue(tempObj, value, null);
                        }
                    }
                    list.Add(tempObj);
                }
                conn.Close();
            }
            return list;
        } 
        public bool ExcuteProcedure(string sqlString,string connString,List<Parameter> paraList)
        {
            using(conn=new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlString, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                foreach(var item in paraList)
                {
                    cmd.Parameters.AddWithValue(item.ParameterName, item.Value);
                }
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            return true;
        }
    }
}
