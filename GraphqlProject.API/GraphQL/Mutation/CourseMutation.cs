using GraphqlProject.API.DTOs;
using GraphqlProject.API.GraphQL.Model;
using GraphqlProject.API.Repositories;
using HotChocolate.Language;

namespace GraphqlProject.API.GraphQL.Mutation;

[ExtendObjectType(OperationType.Mutation)]
public class CourseMutation
{
	public async Task<CourseResult> CreateCourse(CourseInput courseInput, [Service] CourseRepository courseRepository, CancellationToken cancellationToken)
	{
		var course = new CourseDTO
		{
			Name = courseInput.Name,
			Subject = courseInput.Subject,
			InstructorId = courseInput.InstructorId
		};

		course = await courseRepository.Create(course, cancellationToken);

		var courseResult = new CourseResult
		{
			Id = course.Id,
			Name = course.Name,
			Subject = course.Subject,
			InstructorId = course.InstructorId
		};
		return courseResult;
	}

	public async Task<CourseResult> UpdateCourse(Guid id, CourseInput courseInput, [Service] CourseRepository courseRepository, CancellationToken cancellationToken)
	{
		var course = new CourseDTO
		{
			Id = id,
			Name = courseInput.Name,
			Subject = courseInput.Subject,
			InstructorId = courseInput.InstructorId,
		};

		course = await courseRepository.Update(course, cancellationToken);

		if (course == null)
		{
			throw new GraphQLException(ErrorBuilder.New()
				.SetMessage("Course not found.")
				.SetCode("COURSE_NOT_FOUND")
				.Build());
		}

		var courseResult = new CourseResult
		{
			Id = course.Id,
			Name = course.Name,
			Subject = course.Subject,
			InstructorId = course.InstructorId
		};
		return courseResult;
	}

	public async Task<bool> DeleteCourse(Guid id, [Service] CourseRepository courseRepository, CancellationToken cancellationToken)
	{
		var result = await courseRepository.Delete(id, cancellationToken);

		if(!result)
		{
			throw new GraphQLException(ErrorBuilder.New()
				.SetMessage("Course not found.")
				.SetCode("COURSE_NOT_FOUND")
				.Build());
		}

		return result;
	}
}
