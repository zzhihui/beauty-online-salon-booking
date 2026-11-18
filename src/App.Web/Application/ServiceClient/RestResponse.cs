namespace App.Web.Application.ServiceClient
{
    public class RestResponse<T>:RestResponse
    {
        public T Content { get; set; }
    }

    public class RestResponse
    {
        public bool IsSucceed { get; set; }

        public string ErrorMessage { get; set; }
    }
}