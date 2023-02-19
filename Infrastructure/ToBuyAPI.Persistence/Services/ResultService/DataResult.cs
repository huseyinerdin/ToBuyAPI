using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyAPI.Application.Abstractions.Result;

namespace ToBuyAPI.Persistence.Services.ResultService
{
    public class DataResult<T> : Result, IDataResult<T> where T : class
    {
        public T Result { get; set; }
    }
}
