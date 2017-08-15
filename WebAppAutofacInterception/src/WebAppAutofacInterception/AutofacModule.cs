using System;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;

namespace WebAppAutofacInterception
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Register Interceptors with Autofac
            // Named registration
            builder.Register(c => new CallLoggerInterceptor(Console.Out)).Named<IInterceptor>("log-calls");

            // Typed registration
            builder.Register(c => new CallLoggerInterceptor(Console.Out));

            builder.RegisterType<CacheInterceptor>().SingleInstance();
            
            builder.RegisterType<SampleBusiness>().As<ISampleBusiness>().EnableInterfaceInterceptors();
        }
    }
}