namespace GringottsBank.Model.DTO
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public ErrorDetails Error { get; set; }

        public static ServiceResponse<T> CreateSuccess(T data)
        {
            return new ServiceResponse<T> { Success = true, Data = data };
        }
        public static ServiceResponse<T> CreateError(string message)
        {
            return new ServiceResponse<T>
            {
                Success = false,
                Error = new ErrorDetails { Message = message }
            };
        }
        public class ErrorDetails
        {
            public string Message { get; set; }
        }
    }
}
