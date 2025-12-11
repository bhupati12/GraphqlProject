using GraphqlProject.API.GraphQL.Mutation;
using GraphqlProject.API.GraphQL.Queries;
using HotChocolate.Language;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


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
