namespace TokenBasedAuthentication.API.Domain.Responses
{
    public class BaseResponse<T> where T : class
    {
        public T Model { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public BaseResponse(T extra)
        {
            Success = true;
            Model = extra;
        }

        public BaseResponse(string message)
        {
            Success = false;
            Message = message;
        }
    }
}