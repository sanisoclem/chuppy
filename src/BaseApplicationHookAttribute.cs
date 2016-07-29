using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace chuppy
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public abstract class BaseApplicationHookAttribute : Attribute
    {
        private Type _type;
        private string _methodName;

        public BaseApplicationHookAttribute(Type type, string methodName)
        {
            _type = type;
            _methodName = methodName;
        }

        public Type Type { get { return _type; } }
        public string MethodName { get { return _methodName; } }
        public int Order { get; set; }

        public void InvokeMethod() {
            var method = Type.GetMethod(MethodName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
            if (method == null) {
                throw new ArgumentException(
                    String.Format("The type {0} doesn't have a static method named {1}",
                        Type, MethodName));
            }
            method.Invoke(null, null);
        }

        public void InvokeMethod<T>(T param) {
            var method = Type.GetMethod(MethodName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public,
                null, new Type[] { typeof(T) }, null);

            if (method == null) {
                throw new ArgumentException(
                    String.Format("The type {0} doesn't have a static method named {1}",
                        Type, MethodName));
            }

            method.Invoke(null, new object[] { param });
        }
    }
}
