using System.Reflection;

namespace eCommerce.Products.Application;

public sealed class AssemblyReference
{
    public static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
}
