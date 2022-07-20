using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadSpike.Core.Interfaces
{
    public interface IGet<T> where T : class
    {
        public T Get();
    }
}
