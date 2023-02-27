using ToBuyAPI.Application.Abstractions.Result;

namespace ToBuyAPI.Persistence.Services.ResultService
{
    public class DataResult<T> : Result, IDataResult<T> where T : class
    {
        public T Result { get; set; }
    }
}
