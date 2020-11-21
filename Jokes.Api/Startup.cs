using MediatR;
using AutoMapper;
using Jokes.Infra.Context;
using Jokes.Domain.Entities;
using Jokes.Infra.Interfaces;
using Jokes.Infra.Repositories;
using Jokes.Services.Queries;
using Microsoft.OpenApi.Models;
using Jokes.Services.Queries.DTO;
using Jokes.Services.Queries.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Jokes.Services.Commands.Requests;
using Microsoft.Extensions.Hosting;

namespace Jokes.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddSingleton<IConfiguration>(di => Configuration);

            services.AddScoped<IJokeQueries, JokeQueries>();

            services.AddScoped<IJokesRepository, JokeRepository>();

            services.AddScoped<IContext, JokeContext>();

            services.AddMediatR(typeof(CreateJokeRequest));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Jokes.Api", Version = "v1" });
            });

            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Joke, JokeDTO>().ReverseMap();
                cfg.CreateMap<Joke, CreateJokeRequest>().ReverseMap();
                cfg.CreateMap<Joke, UpdateJokeRequest>().ReverseMap();
            });

            services.AddSingleton(autoMapperConfig.CreateMapper());

        }

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
