using Autofac.Extras.DynamicProxy;

namespace WebAppAutofacInterception
{
    public interface ISampleBusiness
    {
        int Method1(int a, int b);

        string Method2(string a, string b);
    }

    [Intercept(typeof(CallLoggerInterceptor))]
    [Intercept(typeof(CacheInterceptor))]
    public class SampleBusiness : ISampleBusiness
    {
        public int Method1(int a, int b)
        {
            return a + b;
        }

        [MethodCache("TestKey1")]
        public string Method2(string a, string b)
        {
            return a + " - " + b;
        }
    }
}