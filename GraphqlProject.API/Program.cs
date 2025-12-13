using GraphqlProject.API.GraphQL.Mutation;
using GraphqlProject.API.GraphQL.Queries;
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
	.AddTypeExtension<CourseMutation>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGraphQL();

app.MapControllers();

app.Run();
