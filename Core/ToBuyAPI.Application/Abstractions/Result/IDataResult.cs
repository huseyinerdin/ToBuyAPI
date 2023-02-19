using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToBuyAPI.Application.Abstractions.Result
{
    public interface IDataResult<T> : IResult where T : class
    {
        T Result { get; }
    }
}
