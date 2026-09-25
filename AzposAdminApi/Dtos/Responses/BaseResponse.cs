namespace AzposAdminApi.Dtos.Responses
{
    public class BaseResponse
	{
        /// <summary>
        /// Api Request Success or Not
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// List Of Message
        /// </summary>
        public List<string>? Messages { get; set; }

        /// <summary>
        /// Object Data From The API Request
        /// </summary>
        public object? Data { get; set; }

        public BaseResponse(bool succeeded, string message)
        {
            Succeeded = succeeded;
            Data = null;
            Messages = new List<string> { message };
        }

        public BaseResponse(bool succeeded, List<string> messages)
        {
            Succeeded = succeeded;
            Data = null;
            Messages = messages;
        }

        public BaseResponse(bool succeeded, object data, string message)
        {
            Succeeded = succeeded;
            Data = data;
            Messages = new List<string> { message };
        }

        public BaseResponse(bool succeeded, object? data = null, List<string>? messages = null)
        {
            Succeeded = succeeded;
            Data = data;
            Messages = messages;
        }

        public BaseResponse() { }
    }
}