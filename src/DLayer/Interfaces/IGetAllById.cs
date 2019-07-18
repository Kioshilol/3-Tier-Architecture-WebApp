using System;
using System.Collections.Generic;
using System.Text;

namespace DLayer.Interfaces
{
    public interface IGetAllById<T>
    {
        IEnumerable<T> GettAllById(int id);
    }
}
