using GraphqlProject.API.GraphQL.Type;
using GraphqlProject.API.Repositories;
using HotChocolate.Language;

namespace GraphqlProject.API.GraphQL.Queries;

[ExtendObjectType(OperationType.Query)]
public class CourseQueries
{
	public async Task<IEnumerable<CourseType>> GetCourses([Service] CourseRepository courseRepository, CancellationToken cancellationToken)
	{
		var courses = await courseRepository.GetAll(cancellationToken);

		return [.. courses.Select(x => new CourseType
		{
			Id = x.Id,
			Name = x.Name,
			Subject = x.Subject,
			InstructorId = x.InstructorId
		})];
	}

	public async Task<CourseType> GetCourse(Guid id, [Service] CourseRepository courseRepository, CancellationToken cancellationToken)
	{
		var course = await courseRepository.GetById(id, cancellationToken);

		return new CourseType
		{
			Id = course.Id,
			Name = course.Name,
			Subject = course.Subject,
			InstructorId = course.InstructorId
		};
	}
}
