using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadSpike.Core.Interfaces
{
    public interface ICreate<T> where T : class
    {
        public T Post(T obj);
    }
}
