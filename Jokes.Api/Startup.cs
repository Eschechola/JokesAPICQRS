using AutoMapper;
using Jokes.Domain.Entities;
using Jokes.Infra.Context;
using Jokes.Infra.Interfaces;
using Jokes.Infra.Repositories;
using Jokes.Services.Commands.Requests;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Jokes.Api
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

            services.AddControllers();

            services.AddSingleton<IConfiguration>(di => Configuration);

            services.AddScoped<IJokesRepository, JokeRepository>();

            services.AddScoped<IContext, JokeContext>();

            services.AddMediatR(typeof(CreateJokeRequest));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Jokes.Api", Version = "v1" });
            });

            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Joke, CreateJokeRequest>().ReverseMap();
                cfg.CreateMap<Joke, UpdateJokeRequest>().ReverseMap();
            });

            services.AddSingleton(autoMapperConfig.CreateMapper());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Jokes.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
