using System;
using System.Collections.Generic;
using System.Text;

namespace BLayer.Mapper
{
    public interface IBaseMapper<TIn, TOut>
    {
        TIn Map(TOut item);
        TOut Map(TIn item);
    }
}
