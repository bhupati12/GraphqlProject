using GraphqlProject.API.CommonModel;

namespace GraphqlProject.API.GraphQL.Type;

public class CourseType
{
	public Guid Id { get; set; }

	public string Name { get; set; }

	public Subject Subject { get; set; }

	[GraphQLNonNullType]
	public InstructorType Instructor { get; set; }

	public IEnumerable<StudentType> Students { get; set; }
}
