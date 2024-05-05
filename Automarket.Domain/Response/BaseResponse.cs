using Automarket.Domain.Enum;

namespace Automarket.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        private T _data;
        private string _description;
        private StatusCode _statusCode;

        public T Data { get => _data; set => _data = value; }
        public string Description { get => _description; set => _description = value; }
        public StatusCode StatusCode { get => _statusCode; set => _statusCode = value; }
    }
}
