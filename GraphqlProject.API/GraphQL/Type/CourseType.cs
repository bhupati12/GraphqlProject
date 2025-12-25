using GraphqlProject.API.CommonModel;

namespace GraphqlProject.API.GraphQL.Type;

public class CourseType
{
	public Guid Id { get; set; }

	public string Name { get; set; }

	public Subject Subject { get; set; }

	[IsProjected(true)]
	public Guid InstructorId { get; set; }

	public IEnumerable<StudentType> Students { get; set; }
}
