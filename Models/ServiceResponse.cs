namespace WebOopPrac_Api.Models
{
    public class ServiceResponse
    {
        public class ServiceResponseModel<T>
        {
            public ServiceResponseStatusCode HttpCode { get; set; }
            public string DeveloperMessage { get; set; } = string.Empty;
            public string UserMessage { get; set; } = string.Empty;
            public dynamic ResponseCode { get; set; } = ServiceResponseCode.Default;
            public T? Data { get; set; }
            public bool Success { get; internal set; }
            public string Nickname { get; set; }
        } // End of class
        public enum ServiceResponseStatusCode
        {
            Success = 200,
            BadRequest = 400,
            InternalError = 500,
            Unauthorized = 401,
            NotFound = 404
        }
        public enum ServiceResponseCode
        {
            Default = 0,
            Success = 10,

            // List of Response Code for
            SPExcution = 80,
            NotFound = 90,
            SqlError = 91, // sql error
            InvalidFormat = 92, // invalid format
            Mapper = 98, // mapper error
            InternalException = 98, // common catch
            ProcessException = 99, // common catch
            GcashPayTimeout = 70,
            GcashQueryFail = 71,
            GcashQueryWaitOTP = 72
        }
    }
}

