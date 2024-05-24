
using GraphQLProj.Abstraction;
using GraphQLProj.GraphQLServices.Mutation;
using GraphQLProj.GraphQLServices.Query;
using GraphQLProj.Mapping;
using GraphQLProj.Repo;

namespace GraphQLProj
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
            builder.Services.AddSingleton<ProductRepo>().AddGraphQLServer().AddQueryType<Query>().AddMutationType<Mutation>();
            builder.Services.AddSingleton<ICategoryRepo, CategoryRepo>();
            builder.Services.AddSingleton<IProductRepo, ProductRepo>();

            
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
            //app.MapControllers();

            app.Run();
        }
    }
}
