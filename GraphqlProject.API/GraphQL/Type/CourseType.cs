using GraphqlProject.API.CommonModel;
using GraphqlProject.API.DataLoader;

namespace GraphqlProject.API.GraphQL.Type;

public class CourseType
{
	public Guid Id { get; set; }

	public string Name { get; set; }

	public Subject Subject { get; set; }

	public Guid InstructorId { get; set; }

	[GraphQLNonNullType]
	public async Task<InstructorType> Instructor([Service] InstructorDataLoader instructorDataLoader, CancellationToken cancellationToken)
	{
		var instructor = await instructorDataLoader.LoadAsync(InstructorId, cancellationToken);
		
		return new InstructorType
		{
			Id = instructor.Id,
			FirstName = instructor.FirstName,
			LastName = instructor.LastName,
			Salary = instructor.Salary
		};
	}

	public IEnumerable<StudentType> Students { get; set; }
}
