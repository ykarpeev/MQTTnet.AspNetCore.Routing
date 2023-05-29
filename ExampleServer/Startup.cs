using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using ExampleServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MQTTnet.AspNetCore;
using MQTTnet.AspNetCore.Routing;
using MQTTnet.AspNetCore.Routing.Routing;
using MQTTnet.Server;

namespace Example
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure AspNetCore controllers
            services.AddControllers();

            services.AddSingleton<MqttServer>();

            // Identify and build routes for the current assembly
            services.AddMqttControllers(opt =>
            {
                opt.WithJsonSerializerOptions(new JsonSerializerOptions(JsonSerializerDefaults.Web) { Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) })
                   .WithRouteInvocationInterceptor<DemoRouteInvocationInterceptor>();
            });


            // Use specific deserialization option for MQTT payload deserialization
            //services.AddMqttDefaultJsonOptions(new JsonSerializerOptions(JsonSerializerDefaults.Web));

            services
                .AddHostedMqttServerWithServices(s =>
                {
                    // Optionally set server options here
                    s.WithoutDefaultEndpoint();
                    
                })
                .AddMqttConnectionHandler()
                .AddConnections();
        }

   

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // Root endpoint for MQTT - attribute routing picks up after this URL
                endpoints.MapConnectionHandler<MqttConnectionHandler>(
                    "/mqtt",
                    opts => opts.WebSockets.SubProtocolSelector = protocolList => protocolList.FirstOrDefault() ?? string.Empty);
            });

            app.UseMqttServer(server =>
            {
                // Enable Attribute routing
               // server.WithAttributeRouting(app.ApplicationServices, true);
            });
            app.UseAttributeRouting();
        }
    }
}