namespace Core.Common.Resources
{
    public static class ResourceHelper
    {
        public static string GetLocaleString(string resourceName)
        {
            return ErrorDescription.ResourceManager.GetString(resourceName);
        }
    }
}