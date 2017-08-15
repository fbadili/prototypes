using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;

namespace WebAppAutofacInterception
{
    public class Startup
    {
        // Exec. Order 1:
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }


        #region Method 1 - Using Autofac Modules

        // Exec. Order 2: Method 1 - Using Autofac Modules
        // This method gets called by the runtime. Use this method to add services to the container.
        //
        // ConfigureServices is where you register dependencies. This gets
        // called by the runtime before the ConfigureContainer method, below.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddMemoryCache();
        }


        // Exec. Order 3: Method 1 - Using Autofac Modules
        //
        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you. If you
        // need a reference to the container, you need to use the
        // "Without ConfigureContainer" mechanism shown later.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
        }


        // Exec. Order 4: Method 1 - Using Autofac Modules
        //
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //
        // Configure is where you add middleware. This is called after
        // ConfigureContainer. You can use IApplicationBuilder.ApplicationServices
        // here if you need to resolve things from the container.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }

        #endregion


        #region Method 2 - Not Using Autofac Modules

        // Method 2 - NOT using Autofac Modules
        //public IContainer ApplicationContainer { get; private set; }

        // Exec. Order 2: Method 2 - NOT using Autofac Modules
        // ConfigureServices is where you register dependencies. This gets
        // called by the runtime before the Configure method, below.
        //public IServiceProvider ConfigureServices(IServiceCollection services)
        //{
        //    // Add services to the collection.
        //    services.AddMvc();
        //    services.AddAutofac();

        //    // Create the container builder.
        //    var builder = new ContainerBuilder();


        //    #region Register Interceptors

        //    //Register Interceptors with Autofac
        //    // Named registration
        //    builder.Register(c => new CallLoggerInterceptor(Console.Out)).Named<IInterceptor>("log-calls");

        //    // Typed registration
        //    builder.Register(c => new CallLoggerInterceptor(Console.Out));

        //    #endregion



        //    // Register dependencies, populate the services from
        //    // the collection, and build the container. If you want
        //    // to dispose of the container at the end of the app,
        //    // be sure to keep a reference to it as a property or field.
        //    //
        //    // Note that Populate is basically a foreach to add things
        //    // into Autofac that are in the collection. If you register
        //    // things in Autofac BEFORE Populate then the stuff in the
        //    // ServiceCollection can override those things; if you register
        //    // AFTER Populate those registrations can override things
        //    // in the ServiceCollection. Mix and match as needed.
        //    builder.Populate(services);


        //    builder.RegisterType<SampleBusiness>().As<ISampleBusiness>().EnableInterfaceInterceptors();



        //    this.ApplicationContainer = builder.Build();

        //    // Create the IServiceProvider based on the container.
        //    return new AutofacServiceProvider(this.ApplicationContainer);
        //}

        // Exec. Order 4: Method 2 - NOT using Autofac Modules
        //
        // Configure is where you add middleware. This is called after
        // ConfigureServices. You can use IApplicationBuilder.ApplicationServices
        // here if you need to resolve things from the container.
        //public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        //{
        //    loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
        //    loggerFactory.AddDebug();

        //    app.UseMvc();

        //    // If you want to dispose of resources that have been resolved in the
        //    // application container, register for the "ApplicationStopped" event.
        //    // You can only do this if you have a direct reference to the container,
        //    // so it won't work with the above ConfigureContainer mechanism.
        //    appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());
        //}

        #endregion

    }
}