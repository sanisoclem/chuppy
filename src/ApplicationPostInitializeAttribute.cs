using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chuppy
{
    /// <summary>
    /// Defines a hook that will be called after all modules have been initialized.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class ApplicationPostInitializeAttribute : BaseApplicationHookAttribute
    {
        public ApplicationPostInitializeAttribute(Type type, string methodName) : base(type, methodName) { }
    }
}
