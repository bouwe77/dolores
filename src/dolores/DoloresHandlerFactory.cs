using System;
using System.Reflection;
using Dolores.Exceptions;
using Dolores.Requests;
using Dolores.Routing;
using System.Runtime.Loader;

namespace Dolores
{
   internal class DoloresHandlerFactory
   {
      public static DoloresHandler CreateDoloresHandler(IHttpMethodImplementation methodImplementation, Request request)
      {
         var assembly = LoadAssembly(methodImplementation.AssemblyName);

         var type = GetType(methodImplementation.FullyQualifiedClassName, assembly);

         var doloresHandler = CreateInstance(type);

         doloresHandler.Request = request;

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
            throw new HttpNotImplementedException($"Could not create instance of '{type?.FullName}'.", exception);
         }
      }

      private static Type GetType(string fullyQualifiedClassName, Assembly assembly)
      {
         try
         {
            return assembly.GetType(fullyQualifiedClassName);
         }
         catch (Exception exception)
         {
            throw new HttpNotImplementedException($"Could not get type '{fullyQualifiedClassName}' in assembly '{assembly?.FullName}'", exception);
         }
      }

      private static Assembly LoadAssembly(string assemblyName)
      {
         try
         {
           return AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(assemblyName));
//            return Assembly.Load(assemblyName);
         }
         catch (Exception exception)
         {
            throw new HttpNotImplementedException($"Could not load assembly '{assemblyName}'", exception);
         }
      }
   }
}
