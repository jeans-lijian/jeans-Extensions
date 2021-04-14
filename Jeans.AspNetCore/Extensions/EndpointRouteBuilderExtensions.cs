using Grpc.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jeans.AspNetCore.Extensions
{
    /// <summary>
    /// Grpc服务批量注入扩展
    /// </summary>
    public static class EndpointRouteBuilderExtensions
    {
        public static void MapGrpcServices(this IEndpointRouteBuilder builder)
        {
            MethodInfo method = typeof(GrpcEndpointRouteBuilderExtensions).GetMethod("MapGrpcService");
            IEnumerable<TypeInfo> typeInfos = Assembly.GetEntryAssembly()!.DefinedTypes.Where(t => t.IsClass && !t.IsAbstract && t.BaseType != null && t.BaseType.CustomAttributes.Any(a => a.AttributeType == typeof(BindServiceMethodAttribute)));
            foreach (TypeInfo item in typeInfos)
            {
                method.MakeGenericMethod(item.AsType()).Invoke(null, new object[] { builder });
            }
        }
    }
}
