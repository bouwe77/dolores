using Dolores.Http;

namespace Dolores.Routing
{
   /// <summary>
   /// Contains all information of your implementation of a specific HTTP method in combination with an URI template.
   /// </summary>
   /// <seealso cref="IHttpMethodImplementation" />
   /// <seealso cref="Route" />
   internal class HttpMethodImplementation : IHttpMethodImplementation
   {
      /// <summary>
      /// Gets the HTTP method.
      /// </summary>
      /// <value>The HTTP method.</value>
      public HttpMethod HttpMethod { get; }

      /// <summary>
      /// Gets the fully qualified class name.
      /// </summary>
      /// <value>The fully qualified class name.</value>
      public string FullyQualifiedClassName { get; }

      /// <summary>
      /// Gets the assembly name that contains the <see cref="FullyQualifiedClassName"/>.
      /// </summary>
      /// <value>The assembly name.</value>
      public string AssemblyName { get; }

      /// <summary>
      /// Gets the method name that belongs to the <see cref="FullyQualifiedClassName"/> that contains the implementation.
      /// </summary>
      /// <value>The method name.</value>
      public string MethodName { get; }

      /// <summary>
      /// Initializes a new instance of the <see cref="HttpMethodImplementation"/> class.
      /// </summary>
      /// <param name="httpMethod">The HTTP method.</param>
      /// <param name="assemblyName">The assembly name.</param>
      /// <param name="fullyQualifiedClassName">The fully qualified name of the class.</param>
      /// <param name="methodName">The method name.</param>
      public HttpMethodImplementation(HttpMethod httpMethod, string assemblyName, string fullyQualifiedClassName, string methodName)
      {
         HttpMethod = httpMethod;
         AssemblyName = assemblyName;
         FullyQualifiedClassName = fullyQualifiedClassName;
         MethodName = methodName;
      }
   }
}
