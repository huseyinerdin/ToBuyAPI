namespace ToBuyAPI.Application.Abstractions.Result
{
    public interface IDataResult<T> : IResult where T : class
    {
        T Result { get; }
    }
}
