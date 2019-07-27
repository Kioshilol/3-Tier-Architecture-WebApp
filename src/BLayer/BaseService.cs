using Core.Interfaces;
using System.Collections.Generic;

namespace BLayer
{
    public class BaseService<T,U>
    {
        protected IEnumerable<U> GetPaging(IMapper<T, U> mapper, IEnumerable<T> list)
        {
            IEnumerable<T> TList = list;
            var TListDTO = new List<U>();

            foreach (dynamic item in TList)
            {
                var itemDTO = mapper.Map(item);
                TListDTO.Add(itemDTO);
            }

            return TListDTO;
        }
    }
}
