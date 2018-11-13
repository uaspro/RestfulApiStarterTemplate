using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestfulApiStarterTemplate.DataStore.Context;
using RestfulApiStarterTemplate.DataStore.Repository;
using RestfulApiStarterTemplate.DataStore.Services;
using RestfulApiStarterTemplate.Models.Dto;
using RestfulApiStarterTemplate.Models.Entities;

namespace RestfulApiStarterTemplate.Extensions
{
    public static class StartupExtensions
    {
        public const string DefaultConnectionStringKeyName = "Default";

        public static void AddDataStore(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString(DefaultConnectionStringKeyName);
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<DefaultDbContext>(options => options.UseSqlServer(connection));

            services.AddScoped<IRepository, DefaultRepository>();
        }

        public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDataStoreService<Test>, GenericDataStoreService<Test>>();
            services.AddScoped<IDataStoreService<TestChild>, GenericDataStoreService<TestChild>>();
        }

        public static void InitDataStore(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DefaultDbContext>();
                context.Database.Migrate();

                if (context.Tests.Any())
                {
                    return;
                }

                var tests = new List<Test>
                {
                    new Test
                    {
                        Name = "Test1",
                        Description = "Test description 1",
                        TestChildren = new List<TestChild>
                        {
                            new TestChild
                            {
                                Name = "Test 1 child 1",
                                Description = "Test 1 child description 1"
                            },
                            new TestChild
                            {
                                Name = "Test 1 child 2",
                                Description = "Test 1 child description 2"
                            }
                        }
                    },
                    new Test
                    {
                        Name = "Test2",
                        Description = "Test description 2",
                        TestChildren = new List<TestChild>
                        {
                            new TestChild
                            {
                                Name = "Test 2 child 1",
                                Description = "Test 2 child description 1"
                            },
                            new TestChild
                            {
                                Name = "Test 2 child 2",
                                Description = "Test 2 child description 2"
                            },
                            new TestChild
                            {
                                Name = "Test 2 child 3",
                                Description = "Test 2 child description 3"
                            }
                        }
                    }
                };

                context.AddRange(tests);
                context.SaveChanges();
            }
        }

        public static void InitAutoMapper(this IApplicationBuilder app)
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Test, TestDto>()
                    .ForMember(e => e.CountOfChildren, o => o.MapFrom(e => e.TestChildren.Count));
                cfg.CreateMap<TestChild, TestChildDto>();
            });
        }
    }
}
