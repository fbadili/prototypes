To setup Autofac in ASP.NET Core:
http://docs.autofac.org/en/latest/integration/aspnetcore.html

To Setup Autofac Interceptors:
http://docs.autofac.org/en/latest/advanced/interceptors.html

Autofac Interceptors Summary:
+ New class: CallLogger : IInterceptor

+ Associate Interceptors with Types to be Intercepted
	[Intercept(typeof(CallLogger))] or [Intercept("log-calls")] for target classes.

+ Register Interceptors with Autofac:
	// Named registration
	builder.Register(c => new CallLogger(Console.Out)).Named<IInterceptor>("log-calls");

	// Typed registration
	builder.Register(c => new CallLogger(Console.Out));

+ Enable Interceptors:
	builder.RegisterType<SampleBusiness>().As<ISampleBusiness>().EnableInterfaceInterceptors();
