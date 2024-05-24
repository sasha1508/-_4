
using Autofac;
using Market.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StorageGraphQL.Abstraction;
using StorageGraphQL.DB;
using StorageGraphQL.GraphQLServices.Mutation;
using StorageGraphQL.GraphQLServices.Query;
using StorageGraphQL.Repo;

namespace StorageGraphQLGraphQL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMemoryCache();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            
            var config = new ConfigurationBuilder();
            config.AddJsonFile("appsettings.json");
            var cfg = config.Build();
            
            builder.Services.AddSingleton<ProductStorageRepo>().AddGraphQLServer().AddQueryType<Query>().AddMutationType<Mutation>(); ;
            builder.Services.AddSingleton<IProductStorageRepo, ProductStorageRepo>();

            builder.Services.AddSingleton<ProductStorageContext>(serviceProvider =>
            {
                var connectionString = cfg.GetConnectionString("db");
                return new ProductStorageContext(connectionString);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapGraphQL();

            app.Run();
        }
    }
}
