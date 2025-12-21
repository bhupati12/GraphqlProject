using GraphqlProject.API.DataLoader;
using GraphqlProject.API.GraphQL.Mutation;
using GraphqlProject.API.GraphQL.Queries;
using GraphqlProject.API.GraphQL.Type;
using GraphqlProject.API.Repositories;
using HotChocolate.Language;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddPooledDbContextFactory<SchoolDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddGraphQLServer()
	.AddQueryType(q => q.Name(nameof(OperationType.Query)))
	.AddMutationType(q => q.Name(nameof(OperationType.Mutation)))
	.AddTypeExtension<ProductQueries>()
	.AddTypeExtension<CourseQueries>()
	.AddTypeExtension<CourseMutation>()
	.AddTypeExtension<CourseTypeResolver>()
	.AddDataLoader<InstructorDataLoader>()
	.RegisterDbContextFactory<SchoolDbContext>();

builder.Services.AddScoped<CourseRepository>();
builder.Services.AddScoped<InstructorRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.MapScalarApiReference();
}

using var scope = app.Services.CreateScope();
var dbcontext = scope.ServiceProvider.GetRequiredService<IDbContextFactory<SchoolDbContext>>();
var context = dbcontext.CreateDbContext();
context.Database.Migrate();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGraphQL();

app.MapControllers();

app.Run();
