using System.Reflection;

namespace CodeBridge.Services
{
    public static class AssemblyHelper
    {
        public static string GetAssemblyVersion() => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public static string GetAssemblyName() => Assembly.GetEntryAssembly().GetName().Name;
    }
}
