using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using PMDEvers.CQRS.Builder;
using PMDEvers.CQRS.Events;
using PMDEvers.CQRS.InMemory;
using PMDEvers.CQRS.Sample.Application.Handlers;
using PMDEvers.CQRS.Sample.Application.Queries;
using PMDEvers.CQRS.Sample.Application.Responses;
using PMDEvers.CQRS.Sample.Data.Handlers;
using PMDEvers.CQRS.Sample.Domain;
using PMDEvers.CQRS.Sample.Domain.Commands;
using PMDEvers.CQRS.Sample.Domain.Events;
using PMDEvers.CQRS.Sample.Domain.Handlers;
using PMDEvers.CQRS.Sample.Domain.Services;
using PMDEvers.Servicebus;

namespace PMDEvers.CQRS.Sample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServiceBus();
            
            services.AddCQRS(opt => opt.UsernameAccessor = () => "New User")
                    .AddAggregate<SampleAggregate>()
                    .AddQueryHandler<GetSample, SampleResponse, GetSampleHandler>()
                    .AddCommandHandler<CreateSample, Guid, CreateSampleHandler>()
                    .AddEventHandler<SampleCreated, SampleCreatedHandler>()
                    .AddInMemoryEventStore();

            services.AddTransient<IExampleService, ExampleService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
