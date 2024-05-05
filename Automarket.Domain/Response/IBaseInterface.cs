using Automarket.Domain.Enum;

namespace Automarket.Domain.Response
{
    public interface IBaseResponse<T>
    {
        public T Data { get; set; }

        public string Description { get; set; }

        public StatusCode StatusCode { get; set; }
    }
}