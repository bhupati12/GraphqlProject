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

public enum Subject
{
	Mathematics = 1,
	Science = 2,
	History = 3,
	Art = 4,
	Literature = 5
}