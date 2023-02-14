﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToBuyApı.Domain.Entities.Common;

namespace ToBuyAPI.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(bool isTrack = true);
        IQueryable<T> GetWhere(Expression<Func<T,bool>> expression, bool isTrack = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool isTrack = true);
        Task<T> GetByIdAsync(string id, bool isTrack = true);
    }
}
