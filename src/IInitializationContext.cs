using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chuppy
{
    /// <summary>
    /// This is an application defined context that can be used by hooks.
    /// </summary>
    public interface IInitializationContext
    {
        void DeclareService<TContract, TImpl>();
    }
}
