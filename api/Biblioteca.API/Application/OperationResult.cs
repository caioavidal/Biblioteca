namespace Biblioteca.API.Application
{
    public class OperationResult
    {
        public ErrorCode StatusCode { get; set; } = ErrorCode.None;
        public string ErrorMessage { get; set; }
        public bool Succedeed { get; set; }

        public static OperationResult Success => new OperationResult();
        public static OperationResult Fail(ErrorCode errorCode, string message = null) => new OperationResult()
        {
            StatusCode = errorCode,
            ErrorMessage = message
        };

    }

    public enum ErrorCode
    {
        None,
        NotFound,
        BadRequest,
        InternalServerError
    }
}
