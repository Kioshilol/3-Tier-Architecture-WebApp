using Core;
using Core.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Xml;

namespace BLayer
{
    public class BaseMapper
    {
        protected IEnumerable<U> Map<T, U>(IMapper<T, U> mapper, IEnumerable<T> list)
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
