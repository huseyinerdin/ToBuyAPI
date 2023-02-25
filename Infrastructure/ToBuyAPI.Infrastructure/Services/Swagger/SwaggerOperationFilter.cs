using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToBuyAPI.Infrastructure.Services.Swagger
{
	//public class SwaggerOperationFilter : IOperationFilter
	//{
	//	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	//	{
	//		if (operation.OperationId == "Post")
	//		{
	//			operation.Parameters = new List<IParameter>
	//			{
	//				new NonBodyParameter
	//				{
	//					Name = "myFile",
	//					Required = true,
	//					Type = "file",
	//					In = "formData"
	//				}
	//			};
	//		}
	//	}
	//}
}
