using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadSpike.Core.Interfaces
{
    public interface IGet<T,I> where T : class
    {
        public T Get();

        public T GetBy(I param);
    }
}
