using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chuppy
{
    /// <summary>
    /// Defines a hook that will be executed when the application shutsdown
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class ApplicationShutdownAttribute : BaseApplicationHookAttribute
    {
        public ApplicationShutdownAttribute(Type type, string methodName) : base(type, methodName) { }
    }
}
