namespace AzposAdminApi.Extensions
{
    public static class ErrorMessageConstantExtension
    {
        /// <summary>
        /// Replace Data to text eg. Data Tidak Boleh Kosong to Produk Tidak Boleh Kosong
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ReplaceData(this string errorMessage, string text)
        {
            string message = errorMessage.ToString();

            string newMessage = message.Replace("Data", text);

            return newMessage;
        }

        public static string ReplaceFitur(this string errorMessage, string text)
        {
            string message = errorMessage.ToString();

            string newMessage = message.Replace("Fitur", text);

            return newMessage;
        }

        public static string ReplaceParameter(this string errorMessage, string text)
        {
            string message = errorMessage.ToString();

            string newMessage = message.Replace("Parameter", text);

            return newMessage;
        }
    }
}