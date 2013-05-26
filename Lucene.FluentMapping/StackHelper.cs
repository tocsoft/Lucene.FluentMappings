using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Lucene.FluentMapping
{
    public static class StackHelper
    {
        public static Assembly GetCallingAssembly()
        {
            var stack = new StackTrace(Thread.CurrentThread, false);

            var thisAssembly = Assembly.GetCallingAssembly();

            for (var i = 0; i < stack.FrameCount; i++)
            {
                var assembly = GetAssembly(stack.GetFrame(i));

                if (assembly != thisAssembly)
                    return assembly;
            }

            return null;
        }

        private static Assembly GetAssembly(StackFrame frame)
        {
            var method = frame.GetMethod();

            if (method == null || method.DeclaringType == null)
                return null;

            return method.DeclaringType.Assembly;
        }
    }
}