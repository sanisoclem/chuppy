using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chuppy
{
    /// <summary>
    /// Defines a hook that will be called upon application initialization
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class ApplicationInitializeAttribute : BaseApplicationHookAttribute
    {
        public ApplicationInitializeAttribute(Type type, string methodName) : base(type, methodName) { }
    }
}
