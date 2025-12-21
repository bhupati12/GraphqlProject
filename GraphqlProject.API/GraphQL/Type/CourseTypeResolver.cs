using GraphqlProject.API.DataLoader;

namespace GraphqlProject.API.GraphQL.Type;

[ExtendObjectType(typeof(CourseType))]
public class CourseTypeResolver
{
	[GraphQLNonNullType]
	public async Task<InstructorType> Instructor([Parent] CourseType course, InstructorDataLoader instructorDataLoader, CancellationToken cancellationToken)
	{
		var instructor = await instructorDataLoader.LoadAsync(course.InstructorId, cancellationToken);

		return new InstructorType
		{
			Id = instructor.Id,
			FirstName = instructor.FirstName,
			LastName = instructor.LastName,
			Salary = instructor.Salary
		};
	}
}
