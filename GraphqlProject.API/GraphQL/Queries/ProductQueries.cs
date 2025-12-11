using HotChocolate.Language;

namespace GraphqlProject.API.GraphQL.Queries;

[ExtendObjectType(OperationType.Query)]
public class ProductQueries
{
	[GraphQLDeprecated("This method is depercated using this instead")]
	public string Hello() => "Hello, GraphQL!";
}
