using GraphqlProject.API.CommonModel;
using GraphqlProject.API.GraphQL.Type;

namespace GraphqlProject.API.GraphQL.Model;

public class CourseInput
{
	public string Name { get; set; }

	public Subject Subject { get; set; }

	public Guid InstructorId { get; set; }
}
