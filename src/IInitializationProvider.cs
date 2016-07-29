using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chuppy
{
    public interface IInitializationProvider : IInitializationContext
    {
        void PreparePostInit();
    }
}
