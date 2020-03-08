namespace ThuanTanUmbraco.ClassHelper
{
    public static class UmbracoHelper
    {
        public static string GetDictionaryItem(string key)
        {
            string errorMessage = umbraco.library.GetDictionaryItem(key);
            if (string.IsNullOrEmpty(errorMessage))
            {
                errorMessage = key + " is missing";
            }
            return errorMessage;
        }
    }
}