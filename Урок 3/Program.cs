using MarketQL.Abstractions;
using MarketQL.GraphQLServices.Mutation;
using MarketQL.GraphQLServices.Query;
using MarketQL.Mapping;
using MarketQL.Repo;

namespace MarketQL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMemoryCache();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddSingleton<IProductRepo, ProductRepo>().AddGraphQLServer().AddQueryType<Query>().AddMutationType<Mutation>();
            builder.Services.AddSingleton<IStorageRepo, StorageRepo>();
            builder.Services.AddSingleton<IProductGroupRepo, ProductGroupRepo>();
            builder.Services.AddSingleton<IProductStorageRepo, ProductStorageRepo>();



            var app = builder.Build();

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
