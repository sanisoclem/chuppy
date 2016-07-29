using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace chuppy
{
    /// <summary>
    /// Exposes functionality to execute any hooks depending on the application lifecycle
    /// All methods in this class are not thread safe.
    /// </summary>
    public class ChuppyManager
    {
        private static bool _hasInit;
        private static List<Assembly> _assemblies;

        public static void Initialize(IInitializationProvider provider) {
            if (_hasInit)
                return;
            // -- run init code
            RunHook<ApplicationInitializeAttribute, IInitializationContext>(provider);
            // -- signal caller that init is complete
            provider.PreparePostInit();
            // -- run post init
            RunHook<ApplicationPostInitializeAttribute, IInitializationContext>(provider);
        }
        public static void Shutdown() {
            if (!_hasInit)
                return;

            RunHook<ApplicationShutdownAttribute>();
        }

        private static IEnumerable<Assembly> Assemblies {
            get {
                return _assemblies ??
                    (_assemblies = Directory.GetFiles(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath), "*.dll")
                        .Select(a => {
                            try {
                                return Assembly.LoadFrom(a);
                            }
                            catch (Win32Exception) { }
                            catch (ArgumentException) { }
                            catch (FileNotFoundException) { }
                            catch (PathTooLongException) { }
                            catch (BadImageFormatException) { }
                            catch (SecurityException) { }
                            return null;
                        }).Where(a => a != null).ToList());
            }
        }

        private static IEnumerable<TAttrib> GetAttributes<TAttrib>() where TAttrib : BaseApplicationHookAttribute {
            return Assemblies.SelectMany(assembly => assembly.GetCustomAttributes(typeof(TAttrib), false).OfType<TAttrib>().ToList())
                                    .OrderBy(att => att.Order);
        }

        private static void RunHook<TAttrib, TParam>(TParam parameter) where TAttrib : BaseApplicationHookAttribute {
            var attribs = GetAttributes<TAttrib>();

            foreach (var activationAttrib in attribs) {
                activationAttrib.InvokeMethod(parameter);
            }
        }
        private static void RunHook<TAttrib>() where TAttrib : BaseApplicationHookAttribute {
            var attribs = GetAttributes<TAttrib>();

            foreach (var activationAttrib in attribs) {
                activationAttrib.InvokeMethod();
            }
        }
    }
}