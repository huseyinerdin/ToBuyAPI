namespace ToBuyAPI.Application.Abstractions.Result
{
    public interface IResult
    {
        bool IsSuccess { get; }
        string Message { get; }
    }
}
