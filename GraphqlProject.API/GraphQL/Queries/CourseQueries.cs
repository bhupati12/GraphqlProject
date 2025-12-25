using GraphqlProject.API.GraphQL.Filters;
using GraphqlProject.API.GraphQL.Sorters;
using GraphqlProject.API.GraphQL.Type;
using GraphqlProject.API.Repositories;
using HotChocolate.Language;
using Microsoft.EntityFrameworkCore;

namespace GraphqlProject.API.GraphQL.Queries;

[ExtendObjectType(OperationType.Query)]
public class CourseQueries
{
	[UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
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

	[UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
	[UseProjection]
	[UseFiltering(typeof(CourseFilterType))]
	[UseSorting(typeof(CourseSortType))]
	public IQueryable<CourseType> GetPaginatedCourses(SchoolDbContext context)
	{
		var result =  context.Courses.AsNoTracking().Select(x => new CourseType
		{
			Id = x.Id,
			Name = x.Name,
			Subject = x.Subject,
			InstructorId = x.InstructorId
		});

		return result;
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
