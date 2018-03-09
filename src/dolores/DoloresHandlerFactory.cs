using Dolores.Exceptions;
using Dolores.Requests;
using Dolores.Routing;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using Dolores.Configuration;
using Microsoft.Extensions.Logging;

namespace Dolores
{
   internal class DoloresHandlerFactory
   {
      public static DoloresHandler CreateDoloresHandler(IHttpMethodImplementation methodImplementation, Request request, ILoggerFactory loggerFactory, DoloresSettings settings)
      {
         var assembly = LoadAssembly(methodImplementation.AssemblyName);

         var type = GetType(methodImplementation.FullyQualifiedClassName, assembly);

         var doloresHandler = CreateInstance(type);

         doloresHandler.Request = request;
         doloresHandler.Logger = loggerFactory.CreateLogger(type);
         doloresHandler.DoloresSettings = settings;
         doloresHandler.RouteHelper = new RouteHelper(settings);

         return doloresHandler;
      }

      private static DoloresHandler CreateInstance(Type type)
      {
         try
         {
            return (DoloresHandler)Activator.CreateInstance(type);
         }
         catch (Exception exception)
         {
            throw new HttpNotImplementedException($"Could not create instance of '{type?.FullName}'. Exception: '{exception.Message}'", exception);
         }
      }

      private static Type GetType(string fullyQualifiedClassName, Assembly assembly)
      {
         string message = $"Could not get type '{fullyQualifiedClassName}' in assembly '{assembly?.FullName}'";

         try
         {
            if (assembly == null)
            {
               throw new HttpNotImplementedException($"{message} Assembly not loaded");
            }

            var type = assembly.GetType(fullyQualifiedClassName);

            if (type == null)
            {
               throw new HttpNotImplementedException($"{message} Type not found");
            }

            return type;
         }
         catch (Exception exception)
         {
            throw new HttpNotImplementedException($"{message}. Exception: '{exception.Message}'", exception);
         }
      }

      private static Assembly LoadAssembly(string assemblyName)
      {
         try
         {
           return AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
         }
         catch (Exception exception)
         {
            throw new HttpNotImplementedException($"Could not load assembly '{assemblyName}'. Exception: '{exception.Message}'", exception);
         }
      }
   }
}
