using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyAPI.Application.Abstractions.Result;

namespace ToBuyAPI.Persistence.Services.ResultService
{
    public class Result : IResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
