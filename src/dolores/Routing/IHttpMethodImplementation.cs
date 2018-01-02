using Dolores.Http;

namespace Dolores.Routing
{
   /// <summary>
   /// Contains all information of your implementation of a specific HTTP method in combination with an URI template.
   /// </summary>
   internal interface IHttpMethodImplementation
   {
      /// <summary>
      /// Gets the HTTP method.
      /// </summary>
      /// <value>The HTTP method.</value>
      HttpMethod HttpMethod { get; }

      /// <summary>
      /// Gets the fully qualified class name.
      /// </summary>
      /// <value>The fully qualified class name.</value>
      string FullyQualifiedClassName { get; }

      /// <summary>
      /// Gets the assembly name that contains the <see cref="FullyQualifiedClassName"/>.
      /// </summary>
      /// <value>The assembly name.</value>
      string AssemblyName { get; }

      /// <summary>
      /// Gets the method name that belongs to the <see cref="FullyQualifiedClassName"/> that contains the implementation.
      /// </summary>
      /// <value>The method name.</value>
      string MethodName { get; }
   }
}
