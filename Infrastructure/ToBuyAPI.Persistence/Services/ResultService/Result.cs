using ToBuyAPI.Application.Abstractions.Result;

namespace ToBuyAPI.Persistence.Services.ResultService
{
    public class Result : IResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
