using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.Converters
{
    public interface IAdapter<TResult, TEntity> where TResult : class
            where TEntity : class
    {
        TResult Convert(TEntity e);
    }
}
