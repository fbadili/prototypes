using System.IO;
using System.Linq;
using Castle.DynamicProxy;

namespace WebAppAutofacInterception
{
    public class CallLoggerInterceptor : IInterceptor
    {
        TextWriter _output;

        public CallLoggerInterceptor(TextWriter output)
        {
            _output = output;
        }

        public void Intercept(IInvocation invocation)
        {
            var beforeText = $">>> Calling method '{invocation.Method.ToString()}()' with parameters {string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray())}";
            _output.WriteLine(beforeText);

            invocation.Proceed();

            var afterText = $"<<< Done: result was: {invocation.ReturnValue}";
            _output.WriteLine(afterText);
        }
    }
}