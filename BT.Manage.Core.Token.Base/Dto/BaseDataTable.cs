using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BT.Manage.AspNet.Token.Base
{
    public class BaseDataTable
    {

        public static string connectionString = ReadConfig.GetKeyValue(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "DataSqlConfig.config", "userTokenRead").@object.ToString();

        /// <summary>
        /// 根据相应的 Token 查找相应的数据
        /// </summary>
        /// <param name="AccessToken"></param>
        /// <returns></returns>
        public static Result<List<T_UserToken>> GetUserID(string AccessToken)
        {
            try
            {
                string sql = $@"select * from [dbo].[T_UserGuid] where Guid='{AccessToken}'";
                DataTable data = Select(sql);
                return DataTableToList(data);
            }
            catch (Exception ex)
            {
                var xx = ex.Message;
                return null;
            }
        }

        public static bool IsDataTimePastDue(DateTime dateTime)
        {
            if (dateTime == null)
                return false;
            if (DateTime.Now < dateTime)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 数据库操作
        /// </summary>
        /// <returns></returns>
        public static DataTable Select(string sql)
        {
            try
            {
                DataTable data = new DataTable();
                using (SqlConnection sqlConnection = new SqlConnection())
                {
                    sqlConnection.ConnectionString = connectionString;
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = sql;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Connection = sqlConnection;
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                    adapter.Fill(data);
                }
                return data;
            }
            catch (Exception ex)
            {

                return null ;
            }
        }

        public static bool NoSelect(string sql)
        {
            try
            { 
                int AffectCount = 0;
                using (SqlConnection sqlConnection = new SqlConnection())
                {
                    sqlConnection.ConnectionString = connectionString;
                    sqlConnection.Open();
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.CommandText = sql;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.Connection = sqlConnection;
                    AffectCount = sqlCommand.ExecuteNonQuery();
                }
                return AffectCount > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// dataTable 转化为List 集合
        /// </summary>
        public static Result<List<T_UserToken>> DataTableToList(DataTable data)
        {
            Result<List<T_UserToken>> result = new Result<List<T_UserToken>>();
            try
            {
                result.code = 1;
                if (data == null || data.Rows.Count <= 0)
                    return result;
                result.@object = (from i in data.AsEnumerable()
                                  select (new T_UserToken()
                                  {
                                      FID = i.Field<int>("FID"),
                                      FUserID = i.Field<int>("FUserID"),
                                      StartTime = i.Field<DateTime>("StartTime"),
                                      EndTime = i.Field<DateTime>("EndTime"),
                                      Guid = i.Field<string>("Guid")
                                  })).ToList();
                return result;
            }
            catch (Exception ex)
            {
                result.code = 0;
                result.message = ex.Message;
                return result;
            }
        }
    }
}
