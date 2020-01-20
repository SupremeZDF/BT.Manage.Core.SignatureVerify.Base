using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BT.Manage.AspNet.Token.Base;

namespace ASPNET.WEBAPI
{
    public class Base
    {
        public static Result GetResult(T_User t_User) 
        {
            Result result = new Result();
            result.code = 0;
            //判断是否存在账号
            var sql = $"select * from T_User where FAccount_Number='{t_User.FAccount_Number}' and FPsword='{t_User.FPsword}'";
            if (BaseDataTable.Select(sql) == null || BaseDataTable.Select(sql).Rows.Count <= 0) 
            {
                return result;
            }
            var FID = (int)BaseDataTable.Select(sql).Rows[0]["FUserID"];
            if (FID > 0)
            {
                var userData = BaseDataTable.Select($"select * from T_UserGuid where FUserID={FID}");
                if (userData.Rows.Count > 0)
                {
                    result.@object = userData.Rows[0]["Guid"].ToString();
                    bool isUpdate = BaseDataTable.NoSelect($"update T_UserGuid set EndTime='{DateTime.Now.AddDays(30).ToString()}' where FUserID='{FID}'");
                    if (isUpdate)
                    {
                        result.code = 1;
                    }
                }
                else
                {
                    var guids = Guid.NewGuid().ToString();
                    var addGuidSql = $"insert into T_UserGuid values('{FID}','{DateTime.Now.ToString()}','{DateTime.Now.AddDays(30).ToString()}','{guids}')";
                    var isAdd = BaseDataTable.NoSelect(addGuidSql);
                    if (isAdd)
                    {
                        result.code = 1;
                    }
                }
            }
            return result;
        }
    }
}