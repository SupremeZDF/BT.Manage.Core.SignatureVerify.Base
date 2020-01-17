using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NETCORE.WEAPI
{
    public class AddSwaggerParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();
            operation.Parameters.Add(new OpenApiParameter() 
            {
                Name = "UserID",  //参数名
                Description = "用户ID",  //描述
                In =ParameterLocation.Path ,//query header body path formData
                //Type = "string",  //类型
                Required = false //是否必选
            });
        }
    }
}
